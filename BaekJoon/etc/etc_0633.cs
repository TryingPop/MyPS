using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 점수 계산
    문제번호 : 2822번

    정렬 문제다
    상위 점수를 구하는데 정렬(1번),
    그리고 인덱스를 오름차순으로 나타내는데 정렬(2번)
    총 2번의 정렬으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0633
    {

        static void Main633(string[] args)
        {

            Solve();

            void Solve()
            {

                (int n, int idx)[] arr = new (int n, int idx)[8];
                for (int i = 0; i < 8; i++)
                {

                    int n = int.Parse(Console.ReadLine());
                    arr[i] = (n, i + 1);
                }

                Array.Sort(arr, (x, y) => y.n.CompareTo(x.n));

                int ret1 = 0;
                int[] ret2 = new int[5];
                for (int i = 0; i < 5; i++)
                {

                    ret2[i] = arr[i].idx;
                    ret1 += arr[i].n;
                }

                Array.Sort(ret2);
                Console.WriteLine(ret1);
                for (int i = 0; i < 5; i++)
                {

                    Console.Write($"{ret2[i]} ");
                }
            }
        }
    }

#if other
// cs2822 - rby
// 2023-07-02 14:47:16
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace cs2822
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int[] mem = new int[9];
            mem[0] = -1;
            for(int i = 1; i <= 8; i++)
            {
                mem[i] = int.Parse(sr.ReadLine());
            }


            for(int tt = 0; tt < 3; tt++)
            {
                int min = int.MaxValue;
                int idx = 0;
                for(int i = 1; i <= 8; i++)
                {
                    if(mem[i] >= 0 && min > mem[i])
                    {
                        idx = i;
                        min = mem[i];
                    }
                }

                mem[idx] = -1;
            }

            int sum = 0;
            for(int i = 1; i <= 8; i++)
            {
                if (mem[i] == -1)
                    continue;

                sum += mem[i];
                sb.Append(i).Append(' ');
            }

            sw.WriteLine(sum);
            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }
}

#elif other2
using System;
using System.Text;

namespace 초보
{
    class 점수계산
    {
        static void Main()
        {
            StringBuilder answer = new StringBuilder();
            
            int[] n = new int[8];
            int[] sor = new int[8];
            for (int i = 0; i < 8; i++)
            {
                n[i] = int.Parse(Console.ReadLine());
                sor[i] = n[i];
            }
            Array.Sort(sor);
            
            int max = sor[3] + sor[4] + sor[5] + sor[6] + sor[7];
            answer.AppendLine(max.ToString());
            
            for (int i = 0; i < 8; i++)
            {
                if (n[i] == sor[3] || n[i] == sor[4] || n[i] == sor[5] || n[i] == sor[6] || n[i] == sor[7])
                {
                    answer.Append((i + 1).ToString());
                    answer.Append(" ");
                }
            }
            Console.WriteLine(answer);
        }
    }
}
#endif
}
