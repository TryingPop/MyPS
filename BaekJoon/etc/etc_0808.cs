using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 11
이름 : 배성훈
내용 : 요세푸스 문제
    문제번호 : 1158번

    구현, 자료구조, 큐 문제다
    시뮬레이션 돌리면서 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0808
    {

        static void Main808(string[] args)
        {

            StreamWriter sw;

            Queue<int> q;
            int n, k;

            Solve();
            void Solve()
            {

                Read();

                q = new(n);
                for (int i = 1; i <= n; i++)
                {

                    q.Enqueue(i);
                }

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write('<');
                k--;
                while(q.Count > 0)
                {

                    for (int i = 0; i < k; i++)
                    {

                        q.Enqueue(q.Dequeue());
                    }

                    sw.Write(q.Dequeue());
                    if (q.Count > 0) sw.Write(", ");
                    else sw.Write('>');
                }

                sw.Close();
            }

            void Read()
            {

                string[] temp = Console.ReadLine().Split();

                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);
            }
        }
    }
#if other
using System;
using System.Collections.Generic;

namespace 콘솔테스트
{
    class Program
    {
        static void Main()
        {
            int[] t = Array.ConvertAll(Console.ReadLine().Split(' '), s => int.Parse(s)), R = new int[t[0]];
            List<int> N = new List<int>(t[0]);
            for (int i = 1; i <= t[0]; i++)
                N.Add(i);
            int j = t[1] - 1, k = 0;
            while (true)
            {
                R[k++] = N[j]; N.RemoveAt(j);
                j += t[1] - 1;
                int cnt = N.Count;
                if (cnt == 0)
                    break;
                while (j >= cnt)
                    j = j - cnt;
            }
            Console.Write("<" + String.Join(", ", R) + ">");
        }
    }
}
#endif
}
