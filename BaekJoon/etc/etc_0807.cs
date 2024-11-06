using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 10
이름 : 배성훈
내용 : Maximum Subarray
    문제번호 : 10211번

    dp, 누적합, 브루트포스 알고리즘 문제다
    누적합으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0807
    {

        static void Main807(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    n = ReadInt();

                    int sum = -1_000;
                    int ret = -1_000;

                    for (int i = 0; i < n; i++)
                    {

                        int cur = ReadInt();

                        sum = Math.Max(cur, sum + cur);
                        if (ret < sum) ret = sum;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }


            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
