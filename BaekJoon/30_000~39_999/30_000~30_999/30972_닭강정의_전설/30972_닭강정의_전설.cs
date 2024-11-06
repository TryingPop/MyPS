using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 9
이름 : 배성훈
내용 : 닭강정의 전설
    문제번호 : 30972번

    누적합 문제다
    입출력이 많다
*/

namespace BaekJoon.etc
{
    internal class etc_1039
    {

        static void Main1039(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[][] sum;
            int n;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                int q = ReadInt();

                for (int i = 0; i < q; i++)
                {

                    int r1 = ReadInt();
                    int c1 = ReadInt();
                    int r2 = ReadInt();
                    int c2 = ReadInt();

                    int m = F(r1, c1, r2, c2);
                    int p = F(r1 + 1, c1 + 1, r2 - 1, c2 - 1);

                    sw.Write($"{p * 2 - m}\n");
                }

                sw.Close();
                sr.Close();
            }

            int F(int _r1, int _c1, int _r2, int _c2)
            {

                return sum[_r2][_c2] 
                    - sum[_r2][_c1 - 1] 
                    - sum[_r1 - 1][_c2]
                    + sum[_r1 - 1][_c1 - 1];
            }

            void SetArr()
            {

                for (int r = 1; r <= n; r++)
                {

                    for (int c = 1; c <= n; c++)
                    {

                        sum[r][c] += sum[r][c - 1];
                    }
                }

                for (int c = 1; c <= n; c++)
                {

                    for (int r = 1; r <= n; r++)
                    {

                        sum[r][c] += sum[r - 1][c];
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();

                sum = new int[n + 1][];
                sum[0] = new int[n + 1];
                for (int r = 1; r <= n; r++)
                {

                    sum[r] = new int[n + 1];
                    for (int c = 1; c <= n; c++)
                    {

                        sum[r][c] = ReadInt();
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

#nullable disable

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
            using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

            Solve(sr, sw);
        }

        public static void Solve(StreamReader sr, StreamWriter sw)
        {
            var n = Int32.Parse(sr.ReadLine());

            var sum = new int[1 + n][];

            sum[0] = new int[1 + n];
            for (var idx = 1; idx <= n; idx++)
                sum[idx] = sr.ReadLine().Split(' ').Select(Int32.Parse).Prepend(0).ToArray();

            for (var idx = 0; idx <= n; idx++)
                for (var jdx = 1; jdx <= n; jdx++)
                    sum[idx][jdx] += sum[idx][jdx - 1];

            for (var idx = 0; idx <= n; idx++)
                for (var jdx = 1; jdx <= n; jdx++)
                    sum[jdx][idx] += sum[jdx - 1][idx];

            var q = Int32.Parse(sr.ReadLine());
            while (q-- > 0)
            {
                var rcrc = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                var r1 = rcrc[0];
                var c1 = rcrc[1];
                var r2 = rcrc[2];
                var c2 = rcrc[3];

                sw.WriteLine(2 * Sum(sum, r1 + 1, c1 + 1, r2 - 1, c2 - 1) - Sum(sum, r1, c1, r2, c2));
            }
        }

        private static int Sum(int[][] s, int y1, int x1, int y2, int x2)
        {
            return s[y2][x2] - s[y2][x1 - 1] - s[y1 - 1][x2] + s[y1 - 1][x1 - 1];
        }
    }
}
#endif
}
