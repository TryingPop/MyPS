using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : Range Sum Query
    문제번호 : 9780번

    누적 합 문제다.
    입력 범위가 10억까지 들어와서 long으로 잡아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1146
    {

        static void Main1146(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n, q;
            long[] arr;
            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sw.Close();
                sr.Close();
            }

            void GetRet()
            {

                for (int i = 0; i < q; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt() + 1;

                    long ret = arr[t] - arr[f];
                    sw.Write($"{ret}\n");
                }

                sw.Write('\n');
                sw.Flush();
            }

            void Input()
            {

                n = ReadInt();
                q = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                for (int i = 1; i <= n; i++)
                {

                    arr[i] += arr[i - 1];
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                arr = new long[100_001];
            }

            int ReadInt()
            {

                int ret = 0;
                while(TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
