using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 17
이름 : 배성훈
내용 : 내리막 길
    문제번호 :  1520번

    먼저 항상 낮은 높이의 길로만 간다는 조건에 의해 (자연수의 정렬성)
    경로에서 지나갔던 길을 다시 들릴 수 없다 
    즉, 루프 걱정은 안해도 된다

    DFS_WRONG 은 DP를 이용하지 않았다
    DP 단원인만큼 100% 시간 초과 혹은 메모리 초과 나는 반례가 있을 것이다

    작성한 DFS 함수의 로직은 예를 들어 확인해보자
    고등학교 때 최단 경로 길찾기 경우의 수 아이디어를 적용 시켰다

    조건에 맞춰 4 * 3맵의 높이를 다음과 같이 하자
    22 21 20 19
    18 17 16 15
    14 13 12 11

    그러면 상하좌우 순으로 탐색한다고 하면
    첫 번째 경로를 발견할 때는
    하단으로 쭉 갔다가 오른쪽으로 가는 아래 O가 표기된 경로이다
    O X X X
    O X X X
    O O O O

    두 번째 경로를 찾을 때까지 거슬러 올라가면서
    여기쯤에서 1 값을 채우는게 멈출 것이다
    00  00  00  00
    01  00  00  00
    01  01  01  01
    
    그리고 두 번째 경로는
    O X X X
    O O X X
    X O O O

    일 것이다

    다만 chk > -1 구문에서
    O X X X
    O O X X
    X O  <<< 여기 단계에서 멈출 것이다
    탈출 하며 세 번째 경로를 찾을 수 있는 곳 까지 1을 추가로 채울 것이다
    
    00  00  00  00
    01 '01' 00  00
    01  01  01  01
    이렇게 된다 따옴표 친 구간만 00 -> 01로 바꼈다


    세 번째 경로도 보면 맟찬가지로 큰 변화가 없다
    O X X X     00  00  00  00 
    O O O X ->  01  01  01  00   
    X X O O     01  01  01  01
    
    네 번째 경로 여기서 부터 변화가 생긴다
    O X X X     
    O O O O   
    X X X O     
    
    여기까지는 쉽게 진행되는 것을 알 수있다
    00  00  00  00      
    01  01  01 '01'     
    01  01  01  01      
    
    따옴표 친 구간에서 더 이상 경로를 찾지 못하고 이전 단계로 돌아간다
    00  00  00  00
    01  01 '02' 01
    01  01  01  01
    돌아가면서 따옴표 친 구간이 1 증가하게 된다

    실상 '02'는 다음 두 경로를 찾았다고 보면 된다
    O X X X             O X X X
    O O O X     &&      O O O O
    X X O O             X X X O
    
    그리고 또한 따옴표 친 구간에서 다른 길을 찾지 못했으므로 이전 단계로 돌아간다
    00  00  00  00
    01 '03' 02  01
    01  01  01  01   
    마찬가지로 따옴표 친 구간이 앞의 2 경우의 수를 받아와서 2 증가한다
    
    여기도 03은 다음과 같은 경로를 찾았다는 의미이다
    O X X X             O X X X             O X X X
    O O O O     &&      O O O X     &&      O O X X
    X X X O             X X O O             X O O O

    앞과 같이 증가할 것이다
    00  00  00  00      04  00  00  00
   '04' 03  02  01  ->  04  03  02  01
    01  01  01  01      01  01  01  01
    
    그리고 다섯 번째 경로는 이거인데 
    O O X X
    X O X X
    X O O O

    아마도 여기서 멈출 것이다
    O O X X
    X O <<<

    그리고 dp는 다음과 같이 변형될 것이다
    04 '03' 00  00
    04  03  02  01
    01  01  01  01

    마찬가지로 여섯 번째 경로는
    O O O X     04  03  02  00
    X X O X ->  04  03  02  01
    X X O O     01  01  01  01

    그리고 마지막 7번째 경로는
    O O O O     04  03  02 '01'
    X X X O ->  04  03  02  01
    X X X O     01  01  01  01
    까지 오고 따옴표 친 구간에서 이전 단계로 갈 것이다
    
    그러면
    04  03 '03' 01      04 '06' 03  01     '10' 06  03  01
    04  03  02  01  ->  04  03  02  01  ->  04  03  02  01
    01  01  01  01      01  01  01  01      01  01  01  01
    
    모든 상하좌우 이동을 해서 처음 위치로 다시 돌아왔고 해당 위치의 값이 10이므로
    10을 반환할 것이다

    마지막 표만 놓고 보면
    10  06  03  01
    04  03  02  01
    01  01  01  01
    고등학교 최단 경로 길찾기 경우의 수를 찾을 때 쓴 로직이다
    여기서는 최단 경로가 아닌 중복을 허용하지 않는 가능한 경로의 수로 바뀌긴 했다

    처음에는 해당 방법 처럼 했으나 추후에 약간의 수정을 했다
    매번 dp의 주소를 참조하는 것 보단 way 변수를 둬서 해당 변수에 연산하며 저장한 뒤 
    해당 좌표를 탈출 할 때(마지막) dp에 넣는 방식을 사용했다
*/

namespace BaekJoon._30
{
    internal class _30_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] size = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] board = new int[size[0]][];
            int[][] dp = new int[size[0]][];

            

            for (int i = 0; i < size[0]; i++)
            {

                board[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                dp[i] = new int[size[1]];

                for (int j = 0; j < size[1]; j++)
                {

                    dp[i][j] = -1;
                }
            }
            sr.Close();

            int result = DFS(size, dp, board, 0, 0);

            Console.WriteLine(result);
            
            // 재귀형식으로 작성한거라 시간도 많이 걸리고 메모리도 많이 잡아먹는다
            // DFS_WRONG(ref result, size, board, 0, 0);
        }

        public static int DFS(int[] _size, int[][] _dp, int[][] _board, int _curPosX, int _curPosY)
        {

            // 길 찾은 경우 1 반환
            if (_curPosX == _size[1] - 1 && _curPosY == _size[0] - 1) return 1;

            int chk = _dp[_curPosY][_curPosX];
            // 아직 들린 곳이 없는 경우
            if (chk > -1)
            {

                return chk;
            }

            // _dp[_curPosY][_curPosX] = 0;
            int way = 0;

            // 탐색 - 상하좌우 순
            // 길 찾은 경우 뒤에서 부터 1 추가된다
            // 그리고 경로가 겹치면 
            int height = _board[_curPosY][_curPosX];

            // 상
            if (_curPosY > 0 && height > _board[_curPosY - 1][_curPosX])
                way += DFS(_size, _dp, _board, _curPosX, _curPosY - 1);
                // _dp[_curPosY][_curPosX] += DFS(_size, _dp, _board, _curPosX, _curPosY - 1);

            // 하
            if (_curPosY + 1 < _size[0] && height > _board[_curPosY + 1][_curPosX])
                way += DFS(_size, _dp, _board, _curPosX, _curPosY + 1);
                // _dp[_curPosY][_curPosX] += DFS(_size, _dp, _board, _curPosX, _curPosY + 1);

            // 좌
            if (_curPosX > 0 && height > _board[_curPosY][_curPosX - 1])
                way += DFS(_size, _dp, _board, _curPosX - 1, _curPosY);
                // _dp[_curPosY][_curPosX] += DFS(_size, _dp, _board, _curPosX - 1, _curPosY);

            // 우
            if (_curPosX + 1 < _size[1] && height > _board[_curPosY][_curPosX + 1])
                way += DFS(_size, _dp, _board, _curPosX + 1, _curPosY);
                // _dp[_curPosY][_curPosX] += DFS(_size, _dp, _board, _curPosX + 1, _curPosY);

            // 길 찾은 경우 값 입력
            _dp[_curPosY][_curPosX] = way;

            return _dp[_curPosY][_curPosX];
        }

        public static void DFS_WRONG(ref int _cnt, int[] _size, int[][] _board, int _curPosX, int _curPosY)
        {

            // 먼저 도착이면 경우의 수 1 증가하고 탈출!
            if (_curPosX == _size[1] - 1
                && _curPosY == _size[0] - 1)
            {

                _cnt++;
                return;
            }

            // 항상 높이가 더 낮은 지점으로만 이동한다는 조건이 있고
            // 자연수의 정렬성으로 인해 중복으로 들릴 수는 없다
            int curHeight = _board[_curPosY][_curPosX];

            // 위로 이동
            if (_curPosY > 0
                && curHeight > _board[_curPosY - 1][_curPosX]) DFS_WRONG(ref _cnt, _size, _board, _curPosX, _curPosY - 1);

            // 아래 이동
            if (_curPosY < _size[0] - 1
                && curHeight > _board[_curPosY + 1][_curPosX]) DFS_WRONG(ref _cnt, _size, _board, _curPosX, _curPosY + 1);

            // 왼쪽 이동
            if (_curPosX > 0
                && curHeight > _board[_curPosY][_curPosX - 1]) DFS_WRONG(ref _cnt, _size, _board, _curPosX - 1, _curPosY);

            // 오른쪽 이동
            if (_curPosX < _size[1] - 1 
                && curHeight > _board[_curPosY][_curPosX + 1]) DFS_WRONG(ref _cnt, _size, _board, _curPosX + 1, _curPosY);
        }
    }
}
