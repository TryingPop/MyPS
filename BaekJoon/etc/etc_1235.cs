using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 1
이름 : 배성훈
내용 : XOR삼형제 1, XOR삼형제 2
    문제번호 : 10728번, 10736번

    애드 혹, 해 구성하기 문제다.
    https://nicotina04.tistory.com/139
    https://ps.mjstudio.net/boj-10736

    해당 문제 증명을 참고해서 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1235
    {

        static void Main1235(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int num = ReadInt();

                int s = 1;
                while ((s << 2) <= num) 
                {

                    s <<= 1;
                }

                int e = Math.Min(3 * s - 1, num);

                sw.Write($"{e - s + 1}\n");
                for (; s <= e; s++)
                {

                    sw.Write($"{s} ");
                }

                sw.Write('\n');
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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

#if other
using System.Numerics;
using System.Text;

class Program
{
    static void Solve(String[] str, StreamReader sr, StringBuilder sb)
    {
        int T = int.Parse(str[0]);
        for (int iter = 0; iter < T; iter++)
        {
            str = spaceReadLine(sr);
            int N = int.Parse(str[0]);
            int count = 0;
            List<int> result = new List<int>();

            if (N == 1)
            {
                count = 1;
                result.Add(1);
                sb.Append(count).Append("\n");
                foreach (int v in result)
                {
                    sb.Append(v).Append(" ");
                }
                sb.Append("\n");
                continue;
            }
            N++;
            int readingOne = 1;
            for (; readingOne < N; readingOne <<= 1) { }
            readingOne >>= 1;
            int preventOne = readingOne >> 1;
            for (int i = preventOne; i < N; i++)
            {
                if (i < readingOne)
                {
                    count++;
                    result.Add(i);
                }
                else
                {
                    if ((i & preventOne) == 0)
                    {
                        count++;
                        result.Add(i);
                    }
                }
            }

            result.Sort();








            sb.Append(count).Append("\n");
            foreach(int v  in result)
            {
                sb.Append(v).Append(" ");
            }
            sb.Append("\n");
        }
    }
    
 //   #region base
    public static void Main(String[] args)
    {
        StreamReader sr = new StreamReader(Console.OpenStandardInput());
        StringBuilder sb = new StringBuilder();
        String[] str = spaceReadLine(sr);
        Solve(str, sr, sb);
        Console.WriteLine(sb.ToString());
        sr.Close();
    }
    static string[] spaceReadLine(StreamReader sr, char seperator = ' ')
    {
        return sr.ReadLine().Split(seperator);
    }
    static char[] sequentialReadLine(StreamReader sr)
    {
        return sr.ReadLine().ToCharArray();
    }
//    #endregion base
}
#endif
}
