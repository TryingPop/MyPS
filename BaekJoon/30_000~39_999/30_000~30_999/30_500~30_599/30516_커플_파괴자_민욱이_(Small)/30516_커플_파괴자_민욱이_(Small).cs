using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 20
이름 : 배성훈
내용 : 커플 파괴자 민욱이 (Small)
    문제번호 : 30516번

    애드 혹, 해 구성하기 문제다.
    아이디어는 다음과 같다.

    먼저 커플끼리 인접한 곳이 0개인 경우 해당 배열을 그대로 반환하면 인접하지 않는다.
    먼저 인접한 곳이 2개 이상인 경우를 본다. 그러면 인접한 곳을 나눈다.
    그리고 역순으로 출력하면 된다. 그러면 커플끼리 만나는 경우는 없다.
    i번 그룹의 맨 앞을 si, 맨 뒤를 ei라 하자.
    그러면 2번 이상의 그룹 i에 대해 si = e(i-1)이다.
    그리고 같은 것은 오로지 한쌍만 존재한다고 했고 si = e(i-1)로 존재하므로
    ei != s(i - 1)일 수 밖에 없다.

    그리고 인접한게 1개인 경우이다.
    만약 크기가 2라면 어떻게 쪼개고 재배치해도 불가능하다.

    이제 1개인데 3개 이상인 경우 만약 맨 뒤와 맨 처음이 같은 경우를 본다.
    이 경우 원소는 최대 4개임을 알 수 있다.
    그리고 인접한 것을 자른 경우 뒤는 2개이상으로 이루어진다.
    해당 경우 뒤집어도 불가능하다.
    반면 맨 뒤의 원소 1개만 떼어내고 그룹을 만든다.
    그리고 이를 1번 뒤에 이동시키면 인접하지 않게 가능하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1717
    {

        static void Main1717(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int m = 1;
                for (int i = 1; i < n; i++) 
                {

                    if (arr[i - 1] == arr[i] && arr[i - 1] != 0) m++;
                }

                int[] g = new int[m];
                int idx = 0;
                int cnt = 1;

                for (int i = 1; i < n; i++)
                {

                    if (arr[i - 1] == arr[i] && arr[i - 1] != 0)
                    {

                        g[idx++] = cnt;
                        cnt = 1;
                    }
                    else cnt++;
                }

                g[idx] = cnt;
                int[] ret = new int[m];
                idx = m;

                for (int i = 0; i < m; i++)
                {

                    ret[i] = idx--;
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                if (m == 2 && n == 2) sw.Write(-1);
                else if (m == 2 && arr[0] == arr[n - 1] && arr[0] != 0)
                    sw.Write($"{3}\n{g[0]} {g[1] - 1} {1}\n2 1 3");
                else
                {

                    sw.Write($"{m}\n");
                    for (int i = 0; i < m; i++)
                    {

                        sw.Write($"{g[i]} ");
                    }

                    sw.Write('\n');
                    for (int i = 0; i < m; i++)
                    {

                        sw.Write($"{ret[i]} ");
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
