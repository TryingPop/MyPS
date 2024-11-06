using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 아우으 우아으이야!!
    문제번호 : 15922번

    스위핑 문제다
    그리디하게 풀었다
    문제에서 x 증가 -> y 증가 순서로 정렬된 선분을 준다
    그래서 그냥 그리디하게 선분의 길이만 누적하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0656
    {

        static void Main656(string[] args)
        {

            StreamReader sr;
            int ret = 0;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                int len = ReadInt();

                ret = ReadInt();
                int bE = ReadInt();

                ret = bE - ret;
                for (int i = 1; i < len; i++)
                {

                    int cS = ReadInt();
                    int cE = ReadInt();

                    if (cE <= bE) continue;
                    if (cS <= bE)
                    {

                        ret += cE - bE;
                        bE = cE;
                    }
                    else
                    {

                        ret += cE - cS;
                        bE = cE;
                    }
                }

                sr.Close();
                Console.WriteLine(ret);
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baekjoon.Gold
{
    class _15922
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
                arr[i] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int start = arr[0][0];
            int end = arr[0][1];
            long sum = 0;
            for (int i = 1; i < n; i++)
            {
                if (end < arr[i][0])
                {
                    sum += end - start;
                    end = arr[i][1];
                    start = arr[i][0];
                }
                else
                    end = Math.Max(end, arr[i][1]);
            }

            sum += end - start;
            Console.WriteLine(sum);
        }
    }
}

#endif
}
