using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 8
이름 : 배성훈
내용 : 비 단조성
    문제번호 : 4237번

    그리디, 애드혹 문제다.
    NZEC로 계속해서 틀렸다.
    이유는 모르겠다.

    찾아보니 비슷한 현상을 겪는 사람들이 많았고,
    이는 백준 채점 서버 문제일 확률이 높다고 한다.
    서버 고치고나서 다시 제출하니 이상없이 통과한다.

    아이디어는 다음과 같다.
    i가 짝수 b[i - 1] < b[i] > b[i + 1]이 성립하고
    그리고 i가 홀수면 b[i - 1] > b[i] < b[i + 1]이 성립하는 
    가장 긴 부분 수열 b를 찾아야한다.
   
    이는 가장 긴 증가하는 부분 수열에서 사용한 아이디어를 썼다.
    즉, 해당 자리에서 가장 긴 경우를 이어 나가면 최대가 되는 것이다.
    이는 최대 길이 중 하나임을 수학적 귀납법으로 보일 수 있다.
    그래서 해당 아이디어로 찿아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1319
    {

        static void Main1319(string[] args)
        {

            int MAX = 30_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            int[] a, b;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();
                while (t-- > 0)
                {

                    Input();

                    int ret = GetRet();

                    sw.Write($"{ret}\n");
                }
            }

            int GetRet()
            {

                int ret = 1;
                b[0] = a[0];

                for (int i = 1; i < n; i++)
                {

                    if ((ret & 1) == 0)
                    {

                        if (b[ret - 1] < a[i]) b[ret++] = a[i];
                        else b[ret - 1] = a[i];
                    }
                    else
                    {

                        if (b[ret - 1] > a[i]) b[ret++] = a[i];
                        else b[ret - 1] = a[i];
                    }
                }

                return ret;
            }

            void Init()
            {

                a = new int[MAX];
                b = new int[MAX + 1];
            }

            void Input()
            {

                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    a[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
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
