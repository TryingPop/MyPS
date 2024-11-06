using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 비밀 편지
    문제번호 : 5426번

    수학, 구현, 문자열 문제다
    회전 이동만 해주면 된다
    당연하게 암호 설정의 반대로 회전해야한다
*/

namespace BaekJoon.etc
{
    internal class etc_0446
    {

        static void Main446(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 256 * 128);
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 256 * 128);

            int n = int.Parse(sr.ReadLine());
            char[] ret = new char[10_000];

            while(n-- > 0)
            {

                string str = sr.ReadLine();
                int len = str.Length;

                for (int i = 1; i <= 100; i++)
                {

                    if (i * i != len) continue;
                    len = i;
                    break;
                }

                for (int r = 0; r < len; r++)
                {

                    for (int c = 0; c < len; c++)
                    {

                        int nR = len - 1 - c;
                        int nC = r;

                        ret[len * nR + nC] = str[r * len + c];
                    }
                }

                for (int i = 0; i < str.Length; i++)
                {

                    sw.Write(ret[i]);
                }
                sw.Write('\n');
                sw.Flush();
            }

            sr.Close();
            sw.Close();
        }
    }

#if other
// cs5426 - rby
// 2023-04-12 10:50:18
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs5426
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int T = int.Parse(sr.ReadLine());
            int N;
            string line;

            for(int t = 1; t <= T; t++)
            {
                line = sr.ReadLine();
                N = (int)Math.Sqrt(line.Length);
                for(int i = N-1; i >= 0; i--)
                {
                    for (int j = 0; j < N; j++)
                        sb.Append(line[i + N * j]);
                }
                sb.AppendLine();
            }
            sw.Write(sb);


            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
