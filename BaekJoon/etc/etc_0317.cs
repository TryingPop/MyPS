using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 사다리 타기
    문제번호 : 2469번

    구현 문자열 문제다
    아이디어는 다음과 같다

    시작은 알파벳 순이다
    시작 문자를 사다리가 ???가 나올 때 까지 계속해서 변환하며 간다
    그리고 ???가 나오면 변환한 문자열 s을 저장하고 그만둔다

    그리고 조작할 문자를 사다리 위로 변환하며 간다
    마찬가지로 ???가 나오면 그만하고 사다리로 변환된 문자열 find을 저장한다

    두 문자를 비교해 사다리를 만든다
    여기서 --가 나올 수 없으므로 s[i] == find[i + 1]인 경우 
    사다리를 이어준다 왼쪽에서부터 탐색하므로 사다리가 존재하는 경우에는 
    해당 방법으로 이상없이 만들어진다!

    그리고 만든 사다리가 유효한지 확인하기 위해 문자열 s를 사다리로 이동시켜
    find가 되는지 검증한다

    여기서 다르다면 사다리는 존재할 수 없고, 같다면 검증된 사다리이다!
    이 아이디어로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0317
    {

        static void Main317(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());
            int len = int.Parse(sr.ReadLine());

            char[] find = new char[n];
            for (int i = 0; i < n; i++)
            {

                find[i] = (char)sr.Read();
            }

            sr.ReadLine();

            int[,] board = new int[len, n - 1];
            int qLine = -1;
            // 사다리 입력받기
            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < n - 1; j++)
                {

                    int cur = sr.Read();
                    if (cur == '-') cur = 1;
                    else if (cur == '?')
                    {

                        cur = -1;
                        qLine = i;
                    }
                    else cur = 0;
                    board[i, j] = cur;
                }

                sr.ReadLine();
            }

            sr.Close();

            // 시작문자 - 디버깅하면서 했기에, char로 했다
            char[] s = new char[n];
            for (int i = 0; i < n; i++)
            {

                s[i] = (char)('A' + i);
            }

            // 사다리 타고 ???까지 내려간다
            for (int i = 0; i < qLine; i++)
            {

                for (int j = 0; j < n - 1; j++)
                {

                    // 이동이 필요한 경우!
                    if (board[i, j] == 1)
                    {

                        char temp = s[j];
                        s[j] = s[j + 1];
                        s[j + 1] = temp;
                    }
                }
            }

            // 찾을 문자를 사다리 역순으로 타고 올라간다!
            for (int i = len - 1; i > qLine; i--)
            {

                for (int j = 0; j < n - 1; j++)
                {

                    if (board[i, j] == 1)
                    {

                        char temp = find[j];
                        find[j] = find[j + 1];
                        find[j + 1] = temp;
                    }
                }
            }

            // 사다리 만들기
            bool possible = true;
            int[] ret = new int[n - 1];
            for (int i = 0; i < n - 1; i++)
            {

                // 문제 조건에서 --가 연달아 나올 수 없기에 
                // - 인 조건만 확인했다
                if (s[i] == find[i + 1] && s[i + 1] == find[i]) ret[i] = '-';
                else ret[i] = '*';
            }

            // 사다리 검증
            // 여기서 못만들면, 사다리를 만들 수 없음과 동형이다!
            for (int i = 0; i < n - 1; i++)
            {

                if (ret[i] == '-')
                {

                    char temp = s[i];
                    s[i] = s[i + 1];
                    s[i + 1] = temp;
                }
            }

            for (int i = 0; i < n; i++)
            {

                if (s[i] == find[i]) continue;
                possible = false;
                break;
            }

            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            // 사다리가 존재
            if (possible)
            {

                for (int i = 0; i < n - 1; i++)
                {

                    sw.Write((char)ret[i]);
                }
            }
            else
            {

                // 없음
                for (int i = 0; i < n - 1; i++)
                {

                    sw.Write('x');
                }
            }

            sw.Close();

        }
    }
}
