using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 9
이름 : 배성훈
내용 : LCS 2
    문제번호 : 9252번

    처음에는 보통적인 풀이법인 끝에서부터 접근했다 앞부터 해도 상관없다
        즉, 문자열 str1, str2에대해 n = str1.Length, m = str2.Length라 하자
        둘의 최장 공통 부분 수열은 다음 규칙을 따르면 만들 수 있을 것이다
            1) str1과 str2의 끝 값이 같다면,
                즉, str1[n - 1] == str2[m - 1] 이면 
                끝 값을 제외한 부분의 최장 공통 부분 수열을 구하고 뒤에 str1[n - 1]을이어 붙이면 될 것이다

            2) str1과 str2의 끝 값이 다르다면
                즉, str1[n - 1] != str2[m - 1] 이면 둘이 다르므로 
                어느 경우던 적어도 둘 중 하나는 최장 공통 부분수열에서 끝 값이 될 수 없다
                그래서 str1에서 끝 부분을 제외하는 경우와, str2에서 끝 부분을 제외하는 경우로 나누고 둘 중 반환값이 긴 것을 사용해야한다
            
            3) 1), 2) 과정을 반복해서 str1, str2 둘 중 하나의 길이가 0이된다면 끝 문자열을 "\0"(= "")를 반환한다

            그러면 마지막에 문자열이 나올 것이고, 이 중에 길이가 긴 것이 최장 공통 부분 수열 중 하나가 될 것이다
        이를 함수로 만든 것이 전처리어 Wrong_TimeOut 안에 있는 LCS 함수이다
    
    이 방법은 입력된 문자열 중 적어도 하나의 길이가 0이될때까지 실행한다
    그래서 완전히 다른 경우 남은 시행횟수가 계속해서 2배로 늘어나기에 지수까지 갈 수 있다
    그리고 문자열 합연산을 남발해서 개선이 필요하다

    해당 방법의 과정을 직접 따라가보면서 겹치는 부분을 찾아보려고 했는데, 어떤 것을 메모해야 하는지 감이 잘 안왔다
    그래서 해당 유튜브를 참고해서 로직을 세워 풀었다
    https://www.youtube.com/watch?v=z8KVLz9BFIo

    초기부분은 같으나 dp 부분만 참고를 했다
    영상에서는 찾는 방법을 따로 기록하는 배열을 다시 할당했는데,
    해당 배열을 할당하는 것보다 N*M 의 메모리를 더 쓰는거보다 N + M으로 찾아가는게 더 효율적이라고 생각해서이다

    아이디어는 다음과 같다
    정방향 탐색이다 역방향으로 탐색해도 상관없다
    여기서 저장하는 배열의 이름은 dp가 아닌 c이다
    c에는 str1, str2의 길이 0부터 i, 0부터 j 까지 최장 공통부분수열의 길이가 담긴다
    그래서 c에 저장하는데 다음과 같은 방법을 이용한다
        1) str1의 i, str2의 j 번째 문자를 비교한다 
            같은 경우 str1의 i - 1, str2의 j - 1의 최장 길이에 + 1을 해준게 현재 최장 길이가 된다
            다른 경우 str1 i - 1과 str2의 j번째 까지 최장 값과 str1 i와 str2의 j - 1번째까지의 최장 값 중 하나가 현재 최장 값이 될 것이다
        2) i or j가 0인 경우 0을 반환
        (앞의 역방향을 정방향으로 바꾼꺼 뿐이다!)

    그래서 기록해 나가면 마지막에 최장 공통 부분 수열의 길이가 나온다
    str1의 i를 기준으로 볼 때 i - 1과 i의 값이 달라지는 부분은 같은 값이 존재하는 부분이다
    j역시 마찬가지이다 그런데 j를 끝값으로 고정시킨 채 i만으로 갱신하면 최장 수열이 안나올 수도 있다

    예를 들어
        str1 = CABD, str2 = ABCD라하면
        위 로직대로 돌리면 c는 다음과 같다
                        A   B   C   D   
                    0   1   2   3   4   - j
                0   0   0   0   0   0
            C   1   0   0   0   1  '1'
            A   2   0   1   1   1   1
            B   3   0   1   2   2  '2'
            D   4   0   1   2   2   2
                i
        그러면 j = 4로 고정하고, i 를 4에서 시작해서 하나씩 줄여가자
        4에서 1로 이동하므로 변화가 있는 곳은 i = 3에서 2 = c[3][4] != c[2][4] = 1가된다
        실제로 B는 같은 부분이 존재한다

        그리고 다음으로 i = 1에서 변화가 있다
        1 = c[1][4] != c[0][4] = 0, 실제로 C는 공통 부분이 있다
        그러나 CB는 최장 공통 부분수열이 아니다!


    반면 1) 중 같으면 c[i][j] = c[i - 1][j - 1] + 1 조건으로 인해
    끝에서 대각선으로 값이 변한 곳을 뒤에서부터 담으면 최장 공통 부분 수열을 찾을 수 있다

                        A   B   C   D   
                    0   1   2   3   4   - j
                0   0   0   0   0   0
            C   1  '0'  0   0   1   1
            A   2   0  '1'  1   1   1
            B   3   0   1  '2'  2   2
            D   4   0   1   2   2   2
                i

        실제로 i = 3, j = 2 인 경우 2 = c[3][2] != c[2][1] = 1, 이고 해당 값은 B
        또한 i = 2, j = 1 인 경우 1 = c[2][1] != c[1][0] = 0,이고 해당 값은 A
        이어 붙이면 AB이고 최장 공통 부분수열 중 하나이다!(AB, CD가 되게 설계했다!)

    아래는 코드이다
*/

namespace BaekJoon._35
{
    internal class _35_03
    {

        static void Main3(string[] args)
        {

            string str1 = Console.ReadLine();
            string str2 = Console.ReadLine();

            int len1 = str1.Length + 1;
            int len2 = str2.Length + 1;

            // 최장 길이 값을 저장하는 배열
            int[][] c = new int[len1][];
            // 반례 처리 대신에 메모리 조금 더 사용했다
            c[0] = new int[len2];

            // LCS
            for (int i = 1; i < len1; i++)
            {

                // 따로 채우지 않았으므로 넣어준다
                c[i] = new int[len2];

                for (int j = 1; j < len2; j++)
                {

                    // 만약 값이 같은 경우면 최장 길이 + 1
                    // 앞번에 최고 길이에 + 1 을 해준다
                    if (str1[i - 1] == str2[j - 1]) c[i][j] = c[i - 1][j - 1] + 1;
                    else
                    {

                        // 값이 다른 경우면 앞의꺼나 위에꺼 중 큰걸로 값을 채운다
                        if (c[i - 1][j] > c[i][j - 1]) c[i][j] = c[i - 1][j];
                        else c[i][j] = c[i][j - 1];
                    }
                }
            }

            // 찾기
            char[] result = new char[c[len1 - 1][len2 - 1]];

            int y = len2 - 1;

            for (int i = len1 - 1; i >= 1; i--)
            {

                int chk = c[i - 1][y];
                // 값이 변한 경우 == 같은 부분이 나온 경우
                if (c[i][y] > chk)
                {

                    result[chk] = str1[i - 1];

                    while (y > 1)
                    {

                        if (c[i - 1][y - 1] != chk) break;
                        y--;
                    }

                    if (chk == 0) break;
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(Console.OpenStandardOutput()))
            {

                sw.WriteLine(result.Length);

                for (int i = 0; i < result.Length; i++)
                {

                    sw.Write(result[i]);
                }
            }
        }


#if Wrong_TimeOut
        static string LCS(string str1, string str2)
        {

            int n = str1.Length;
            int m = str2.Length;
            if (n == 0 || m == 0) return "";

            if (str1[n - 1] == str2[m - 1])
            {

                return LCS(str1.Substring(0, n - 1), str2.Substring(0, m - 1)) + str1[n - 1];
            }
            else
            {

                string chk1 = LCS(str1, str2.Substring(0, m - 1));
                string chk2 = LCS(str1.Substring(0, n - 1), str2);

                if (chk1.Length > chk2.Length) return chk1;
                else return chk2;
            }
        }
#endif
    }
}
