using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : Barsik
    문제번호 : 20751번

    수학, 구현, 애드혹 문제다
    아이디어는 다음과 같다
    골인 지점에 못가는 경우인지 확인해야하는데,
    골인 지점이 |로 막혀있는지, -로 막혀있는지 / 막혀 있는지 확인하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0634
    {

        static void Main634(string[] args)
        {

            string NO = "Tuzik\n";
            string YES = "Barsik\n";

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int test = ReadInt();

                while (test-- > 0)
                {

                    int n = ReadInt();
                    int m = ReadInt();
                    int r = ReadInt();
                    int c = ReadInt();
                    int s = ReadInt();

                    if ((r - s <= 1 && r + s >= n) 
                        || (c - s <= 1 && c + s >= m)
                        || (r - s <= 1 && c - s <= 1)
                        || (r + s >= n && c + s >= m)) sw.Write(NO);
                    else sw.Write(YES);
                }

                sr.Close();
                sw.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n') 
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// cs20751 - rby
// 2023-03-22 오전 12:15:54
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs20751
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int T = int.Parse(sr.ReadLine());
            int N, M, R, C, S;
            int[] line;

            for (int t = 1; t <= T; t++)
            {
                line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                (N, M, R, C, S) = (line[0], line[1], line[2], line[3], line[4]);

                bool go = false;
                if (R - S > 1 && C + S < M)
                    go = true;
                if (R + S < N && C - S > 1)
                    go = true;

                sw.WriteLine(go ? "Barsik" : "Tuzik");
            }


            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
