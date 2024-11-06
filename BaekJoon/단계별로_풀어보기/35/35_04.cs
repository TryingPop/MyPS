using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 10
이름 : 배성훈
내용 : 경찰차
    문제번호 : 2618번

    길 추적은 따로 메모리를 사용하는게 아닌 기존의 표를 읽는 방식을 택했다
    
    처음에 무엇을 메모리에 저장해야할지 몰라서 많이 고민했다
    그래서 직접 작은 표(5*5)로 좌표를 4개 정도 지정해서 직접 시행횟수들을 알아봤다

    그러니 해당 좌표로 police0가 이동 또는 police1가 이동되는 것을 표로 표현할 수 있게 되었다
    여기서 board[i][j]가 그 역할을 한다
    board[i][j]는 police[0]은 pos[i - 1]로 이동된 상태, police[1]은 pos[j - 1]로 이동된 상태
    pos[k - 1] 를 입력된 좌표들의 k번째 좌표이다
        예를들어 맵의 사이즈가 5 * 5라고 하고, 입력이 이동해야할 좌표 3번인데
        이동해야할 좌표는 순서대로 (1, 2), (4, 3), (2, 3)이라 하자
        그러면 board[1][2]는
            police0이 좌표의 1번에 이동 즉, (1, 2)위치에
            police1은 좌표의 2번에 이동 즉, (4, 3)위치에
        있다는 것을 나타낸다
        board[i][j]의 값은 최단 시간을 의미한다
            여기서는 경우의 수가 1개이므로 police0이 pos[0] 에 이동 한 거리는 (1,1)에서 (1, 2)이므로
                1 - 1 + 2 - 1 = 1,

            그리고 police1이 pos[1]에 이동한 거리는 (5,5)에서 (4,3)이므로
                5 - 4 + 5 - 3 = 3,
            총 4(1 + 3)의 거리를 이동했다 그래서 board[1][2] = 4

    그런데 문제의 경우의 수는 pos[i]에 police0, police1이 이동하는지 경우를 물으므로 총 경우의 수는
    문제 수를 M이라하면 2^M이다 그런데, board방법으로는 M * M밖에 못담는다
    즉, 최단 경로에 맞춰 중복 경우를 만들고 제거해야한다!

    그러면 중복 경우는 언제 나타날까가 의문이다
    1, 2 까지는 1^1 = 1 * 1, 2^2 = 2 * 2라 경우의 수가 같다
    3부터 중복되는 경우를 합쳐서 표현해야한다

    위의 조건에 맞춰 표를 한 번 채우면서 확인해보자
    조건은 아래 1), 2) 번이다!
        1) 맵의 사이즈가 5 * 5라고 하고, 입력이 이동해야할 좌표 3번인데
        2) 이동해야할 좌표는 순서대로 (1, 2), (4, 3), (2, 3)이라 하자

        먼저 좌표들의 배열 pos는 pos = { (1, 2), (4, 3), (2, 3) }이다
        솔직히 인덱스가 뒤죽박죽이면 헷갈리니 여기 예제에서는 pos[1] = (1, 2)이라 한다
        (아래 코드는 0번부터 시작했다)
        
        그리고 맵의 크기는 5 * 5 이므로 police0의 초기 위치는 1, 1이고 police1의 초기 위치는 5,5가된다
        입력된 조건이 3개이므로 0, 1, 2, 3을 표현할 수 있어야하니 저장할 보드의 크기는 (3 + 1) * (3 + 1) 즉, 4 * 4 이다
        
        즉 board는 다음과 같다
            여기서 0은 초기 위치에 있음을 의미한다
                    0       1       2       3       - police1의 위치
            0       0       0       0       0
            1       0       0       0       0
            2       0       0       0       0
            3       0       0       0       0
            police0의 위치

            -1- 이제 pos[1]의 경우를 살펴 보자!
            경우의 수는 2가지이다!

            police0이 pos[1]로 이동하는 경우, police1이 pos[2]로 이동하는 경우
            police0이 pos[1]로 이동하는 경우 거리는 1 - 1 + 2 - 1 = 1,
            경우의 수가 하나뿐이므로 최단 거리는 1
            그리고 policce0은 pos[1]에 있어 앞의 인덱스는 1, police1은 초기위치에 있으므로 뒤의 인덱스가 0
            즉, board[1][0] = 1이다

                    0       1       2       3       - police1의 위치
            0       0       0       0       0
            1      '1'      0       0       0
            2       0       0       0       0
            3       0       0       0       0
            police0의 위치            

            police1이 pos[1]로 이동하는 경우 거리는 5 - 1 + 5 - 2 = 7
            앞과 마찬가지로 police0은 초기위치에 있어 앞의 인덱스가 0, police1은 pos[1]에 있으므로 뒤의 인덱스가 1
            즉, board[0][1] = 7이된다
                    0       1       2       3       - police1의 위치
            0       0      '7'      0       0
            1       1       0       0       0
            2       0       0       0       0
            3       0       0       0       0
            police0의 위치            

            -2- 이제 pos2로 이동하는 경우를 보자
            pos2로 이동하는 방법 역시 2가지이다
            police0이 이동하는 경우, police1이 이동하는 경우
            그런데 앞의 경우와 합치면 2 * 2 = 4가지가 나온다
            즉, 
                i)      police0이 초기 위치에 있고, police1이 pos[2]에 있는 경우
                ii)     police0은 pos[2]에 있고, police1이 초기 위치에 있는 경우
                iii)    police0은 pos[2]에 있고, police1은 pos[1]에 있는 경우
                iv)     police0은 pos[1]에 있고, police1이 pos[2]에 있는 경우

            먼저 i) 경우부터 보자
                이 경우는 police1이 pos[1]과 pos[2]를 간 경우이다
                즉, police1이 초기위치에서, pos[1]로 이동
                그리고 pos[1]에서 pos[2]로 이동한 경우다
                이 경우의 수는 하나이다!
                
                그러면 최단 거리는 초기 위치에서 pos[1]까지의 거리 board[0][1] = 7,
                pos[1]에서 pos[2]까지의 거리 4 - 1 + 3 - 2 = 4,
                둘이 합치면 board[0][2] = 11 = 7 + 4 = board[0][1] + TaxiDis(pos[1], pos[2])이다
                여기서 TaxiDis는 택시 거리함수이다 여기의 거리 조건 계산 법이라 보면 된다

                        0       1       2       3       - police1의 위치
                0       0       7     '11'      0
                1       1       0       0       0
                2       0       0       0       0
                3       0       0       0       0
                police0의 위치

            ii)의 경우도 똑같이 하면
                board[2][0] = board[1][0] + TaxiDis(pos[1], pos[2]) = 1 + 4 = 5를 얻는다

                        0       1       2       3       - police1의 위치
                0       0       7      11       0
                1       1       0       0       0
                2      '5'      0       0       0
                3       0       0       0       0
                police0의 위치            

            iii)의 경우를 보면,
                현재는 police0이 pos[1]로 이동하고, police1이 이후 pos[2]로 이동한 경우 밖에 없다
                police0은 pos[1]에 위치하고!, police1은 pos[2]에 위치하므로 board[1][2]이다
                그래서 board[1][2] = board[1][0] + TaxiDis((5, 5), pos[2]) = 1 + (5 - 4 + 5 - 3) = 4

                        0       1       2       3       - police1의 위치
                0       0       7      11       0
                1       1       0      '4'      0
                2       5       0       0       0
                3       0       0       0       0
                police0의 위치
                다음 장소에 4가 기록된다

            iv)도 똑같이 하면
                police0은 pos[2]에 위치하고, police1은 pos[1]에 위치하는 경우 밖에 없으므로 최단 경로는
                board[2][1] = board[0][1] + TaxiDis((1, 1), pos[2]) = 7 + (4 - 1 + 3 - 1) = 12

                        0       1       2       3       - police1의 위치
                0       0       7      11       0
                1       1       0       4       0
                2       5     '12'      0       0
                3       0       0       0       0
                police0의 위치

            -3- 이제 pos[3] = (2, 3)으로 이동하는 경우이다
                앞과 조합하면 총 경우는 8가지(4 * 2)가 존재해야한다
                그런데 결과로만 해서 경우의 수를 보면 6개 이다! 즉 2개의 경우의 수는 숨어있다!
                
                i)      police0은 초기위치이고 police1이 pos[3]위치인 경우
                ii)     police0은 pos[3]위치이고 police1이 초기위치인 경우

                iii)    police0은 pos[1]위치이고 police1이 pos[3]위치인 경우
                iv)     police0은 pos[3]위치이고 police1이 pos[1]위치인 경우

                --------------------- 여기 숨어있어요! ---------------------
                v)      police0은 pos[2]위치이고 police1이 pos[3]위치인 경우
                vi)     police0은 pos[3]위치이고 police1이 pos[2]위치인 경우
                ------------------------------------------------------------

            줄낭비를 줄이기 위해 i), ii), iii), iv) 묶어서 보자!
            수학적 사고력이 좋다면 -2-에서 바로 묶어서 계산하는게 보일 것이다! (저는 수학적 사고력이 안좋아서 바로 못찾았습니다)
            
            i)의 경우 police0은 안움직이고 police1만 pos[1], pos[2], pos[3]으로 이동했다
            즉, pos[2]에 있는 애가 pos[3]으로 이동했다

            ii)의 경우 police1은 안움직이고 police0만 pos[1], pos[2], pos[3]으로 이동했다
            마찬가지로 pos[2]에 있는 애가 pos[3]으로 이동했다

            iii) police0은 pos[1]로 이동하고 police1은 pos[2], pos[3]으로 이동했다
            마찬가지로 pos[2]에 있는 애가 pos[3]으로 이동했다!
            
            iv) police1은 pos[1]에 이동하고 police0은 pos[2], pos[3]으로 이동했다
            마찬가지로 pos[2]에 있는 애가 pos[3]으로 이동했다!

            즉 i), ii), iii), iv) 모두 TaxiDis(pos[2], pos[3]) 거리만 추가되었다
            TaxiDis(pos[2], pos[3]) = 4 - 2 + 3 - 3 = 2
            
            i)      board[0][3] = board[0][2] + TaxiDis(pos[2], pos[3]) = 11 + 2 = 13
            ii)     board[3][0] = board[2][0] + TaxiDis(pos[2], pos[3]) = 5 + 2 = 7
            iii)    board[1][3] = board[1][2] + TaxiDis(pos[2], pos[3]) = 4 + 2 = 6
            iv)     board[3][1] = board[2][1] + TaxiDis(pos[2], pos[3]) = 12 + 2 = 14

                        0       1       2       3       - police1의 위치
                0       0       7      11     '13'
                1       1       0       4      '6'
                2       5      12       0       0
                3      '7'    '14'      0       0
                police0의 위치

            이를 일반화 해보자
            board[i][j]에 대해 i, j가 2이상 차이난다고 하자
            만약 i > j + 1 인 경우,
                police1은 j에서 멈추고 police0이 j + 1, j + 2, ... i 까지 이동한 상황이다!
                그래서 police0이 pos[j + 1] 좌표로 이동하고, police1이 pos[j] 좌표로 이동한 최단 경로에서
                police0이 pos[j + 1]에서 pos[j + 2]로, pos[j + 2]에서 pos[j + 3]으로 이동한 것이 최단 경로가 된다
                그래서 
                    board[j + 2][j] = board[j + 1][j] + TaxiDis(pos[j + 1], pos[j + 2]),

                    board[j + 3][j] = board[j + 2][j] + TaxiDis(pos[j + 2], pos[j + 3])
                                    = board[j + 1][j] + TaxiDis(pos[j + 1], pos[j + 2]) + TaxiDis(pos[j + 2], pos[j + 3])

                    ... 귀납적으로(하나씩 늘려가면서) 보면 
                    board[i][j] = board[j + 1][j] + TaxiDis(pos[j + 1], pos[j + 2]) + ... + TaxiDis(pos[i - 1], pos[i])

            j > i + 1 인 경우도 똑같이하면
                board[i][j] = board[i][j - 1] + TaxiDis(pos[j], pos[j - 1])
                이하 귀납적으로 얻을 수 있다

            이를 적용한게 아래 코드에서 탐색 중 2 이상 차이나는 구간이다

            이제 중복이 포함된 v), vi) 구간이다
                
                vi)     police0은 pos[3]위치이고 police1이 pos[2]위치인 경우
            
                        0       1       2       3       - police1의 위치
                0       0       7      11      13 
                1       1       0       4       6 
                2       5      12      '0'      0
                3       7      14       0       0
                police0의 위치
            
            i), ii), iii), iv)처럼 보기에는 표에서 보듯이 비워져 있다! 
            그리고 동시에 이동하는 경우는 최단 경로가 될 수 없으므로 배제되는 경로다!

            v)를 예를 들어보자
                v)      police0은 pos[2]위치이고 police1이 pos[3]위치인 경우
                
                이전에 police0이 pos[2]에 위치해야하는 것은 알고 있다
                police1은? 어디에 있어야 하는지 정할 수 없다
                그래서 남는 경우를 비교하면서 최단 경로를 넣어야한다!

                남은 경우는 
                    (1) police1이 초기위치에서 pos[3]으로 이동한 경우,
                    (2) police1이 pos[1]에서 pos[3]으로 이동한 경우이다

                (1) 의 최단 경로는
                    police0는 pos[2]에 있고, police1은 초기위치에 있는 최단 경로에서
                    police1을 pos[3]으로 이동시키는 경우이다
                    chk1 = board[2][0] + TaxiDis((5, 5), pos[3]) = 5 + (5 - 2 + 5 - 3) = 10
                    즉 chk1 = 10이다
                비슷하게 (2)의 최단 경로는
                    police0은 pos[2]에 있고, police1은 pos[1]에 있는 최단 경로에서
                    police1을 pos[3]으로 이동시키는 경우이다
                    chk2 = board[2][1] + TaxiDis(pos[1], pos[3]) = 12 + (2 - 1 + 3 - 2) = 14
                    즉 chk2 = 14

                board에는 최단 경로를 기록해야하므로 14 > 10이므로 board[2][3] = 10 이 되어야한다

                        0       1       2       3       - police1의 위치
                0       0       7      11      13 
                1       1       0       4       6 
                2       5      12       0     '10'
                3       7      14       0       0
                police0의 위치

            똑같이 vi)를 하면
                chk1 = board[0][2] + TaxiDis((1, 1), pos[3]) = 11 + (2 - 1 + 3 - 1) = 13
                chk2 = board[1][2] + TaxiDis(pos[1], pos[3]) = 4 + 2 = 6
                chk1 = 13 > 6 = chk2이므로
                        0       1       2       3       - police1의 위치
                0       0       7      11      13 
                1       1       0       4       6 
                2       5      12       0      10
                3       7      14      '6'      0
                police0의 위치
                board[3][2] = 6이되어야한다

            이를 일반화하면 다음과 같다
            board[i][i-1]의 최단 경로는 board[0][i - 1] + TaxiDis(police[0], pos[i]), board[1][i - 1] + TaxiDis(pos[1], pos[i]),
            ..., board[i - 2][i - 1] + TaxiDis(pos[i - 2], pos[i]) 중 최소값이 된다

            board[j - 1][j]는 board[j - 1][0] + TaxiDis(police[1], pos[j]), board[j - 1][1] + TaxiDis(pos[1], pos[j]),
            ..., board[j - 1][j - 2] + TaxiDis(pos[j - 2], pos[j]) 중 최소값이 된다

            이는 아래 코드에서 1 차이나는 구간이다!

            이제 각 좌표에대한 모든 최단 경로를 기록했다
                        0       1       2       3       - police1의 위치
                0       0       7      11      13 
                1       1       0       4       6 
                2       5      12       0      10
                3       7      14       6       0
                police0의 위치            

            pos[1], pos[2], pos[3]를 이동하는 최단 경로는 board[3][0], board[3][1], board[3][2], board[0][3], board[1][3], board[2][3] 중에 있다
            여기서는 board[1][3] = board[3][2] = 6이 최단 경로가 된다!
            최단 경로 거리 찾는 코드의 아이디어이다!

            이제 읽는 법을 보자! 먼저 board[1][3]을 보자 (어느게 되어도 상관없다)
            board의 정의를 보면 쉽게 해석된다
                board[1][3]이 의미하는 것은
                police0은 pos[1]에있다, police1은 pos[3]에 있다
                그러면, police[0]은 pos[1]에 멈춰 있으므로 pos[2], pos[3]은 police[1]이 이동했다 
                즉 이동 순서를 보면 뒤에서 2개는 police[1]이 이동했다 아직 모르는 것을 -1, police[0]이 이동한 것을 1, police[1]이 이동한 것을 2라하면
                -1, 2, 2가 된다
                        
                그리고 police[0] pos[1]에 있으므로 pos[1]은 police[0]이 갔다
                따라서 1, 2, 2이다!
        
        이제 board[3][2]인 경우를 보자
        pos[3]은 police[0]이 이동한 것을 알고 pos[2]는 police[1]이 이동한 것을 안다 pos[1]은 누가 이동했는지 모른다!
        -1, 2, 1이 된다

        그런데 board[3][2]를 구하는 과정에서 board[3][2] = board[1][2] + 2로 연산했으므로
        board[1][2]로 pos[1]은 police[0]이 이동한 것을 알 수 있다
        그래서 1, 2, 1이다

        이를 보면 board[i][j]에서 i, j 가 1 차이나는 구간을 기록할 필요가 있어 보인다
        그리고 어느 인덱스에서 왔는지 기록 해줘야한다!

        이러한 아이디어를 코드로 쓴게 최단 경로 순찰한차 찾기 구간이다!

    그리고 해당 좌표 저장도 필요해 보인다!

    해당 코드로 88ms로 통과했다
*/

namespace BaekJoon._35
{
    internal class _35_04
    {

        static void Main4(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int size = int.Parse(sr.ReadLine());
            int len = int.Parse(sr.ReadLine());

            int[][] pos = new int[len][];

            for (int i = 0; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                pos[i] = temp;
            }

            sr.Close();

            int[][] police = new int[2][];

            police[0] = new int[2] { 1, 1 };
            police[1] = new int[2] { size, size };

            int[] idx0 = new int[len + 1];
            int[] idx1 = new int[len + 1];

            // 앞의 idx는 police[0], 뒤의 idx는 police[1]
            int[][] board = new int[len + 1][];
            
            for (int i = 0; i < len + 1; i++)
            {

                board[i] = new int[len + 1];
            }

            // 탐색 시작
            board[1][0] = TaxiDis(police[0], pos[0]);
            board[0][1] = TaxiDis(police[1], pos[0]);

            // 최단 거리 채운다
            for (int cur = 2; cur <= len; cur++)
            {

                /// -------------------------*--------------------------------
                /// 2 이상 차이나는 구간
                int dis = TaxiDis(pos[cur - 2], pos[cur - 1]);

                for (int other = 0; other < cur - 1; other++)
                {

                    board[cur][other] = board[cur - 1][other] + dis;
                    board[other][cur] = board[other][cur - 1] + dis;
                }
                /// -------------------------*--------------------------------

                /// -------------------------*--------------------------------
                /// 1 차이나는 구간
                int min1 = board[0][cur - 1] + TaxiDis(pos[cur - 1], police[0]);
                int min2 = board[cur - 1][0] + TaxiDis(pos[cur - 1], police[1]);
                
                for (int i = 1; i < cur - 1; i++)
                {

                    int chkDis = TaxiDis(pos[i - 1], pos[cur - 1]);
                    int chk1 = board[i][cur - 1] + chkDis;
                    int chk2 = board[cur - 1][i] + chkDis;

                    if (min1 > chk1)
                    {

                        min1 = chk1;
                        idx0[cur] = i;
                    }

                    if (min2 > chk2)
                    {

                        min2 = chk2;
                        idx1[cur] = i;
                    }
                }

                board[cur][cur - 1] = min1;
                board[cur - 1][cur] = min2;
                /// -------------------------*--------------------------------
            }

            /// 최단 경로 거리 찾기
            // 최단 경로 거리
            int result = board[0][len];
            // 해당 좌표도 저장
            int p0 = 0;
            int p1 = len;
            for (int i = 0; i < len; i++)
            {

                if (result > board[i][len])
                {

                    result = board[i][len];
                    p0 = i;
                    p1 = len;
                }

                if (result > board[len][i])
                {

                    result = board[len][i];
                    p0 = len;
                    p1 = i;
                }
            }

            /// 최단 경로 순찰한차 찾기
            int[] chk = new int[len];
            for (int i = len - 1; i >= 0; i--)
            {

                int diff = p0 - p1;

                if (diff > 0)
                {

                    chk[i] = 1;

                    if (diff == 1)
                    {

                        p0 = idx0[i + 1];
                    }
                    else p0--;
                }
                else 
                {

                    chk[i] = 2;

                    if (diff == -1)
                    {

                        p1 = idx1[i + 1];
                    }
                    else p1--;
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(result);

                for (int i = 0; i < chk.Length; i++)
                {

                    sw.WriteLine(chk[i]);
                }
            }
        }
        
        static int TaxiDis(int[] _pos1, int[] _pos2)
        {

            int result = 0;

            if (_pos1[0] > _pos2[0]) result += _pos1[0] - _pos2[0];
            else result += _pos2[0] - _pos1[0];

            if (_pos1[1] > _pos2[1]) result += _pos1[1] - _pos2[1];
            else result += _pos2[1] - _pos1[1];

            return result;
        }
    }
}
