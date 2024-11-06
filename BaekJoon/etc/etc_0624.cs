using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 온라인 판매
    문제번호 : 1246번

    그리디, 정렬 문제다
    우선순위 큐를 써서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0624
    {

        static void Main624(string[] args)
        {

            StreamReader sr;
            PriorityQueue<int, int> q;

            int n, m;

            Solve();

            void Solve()
            {

                Input();

                int ret1 = 0;
                int ret2 = 0;
                while(q.Count > 0)
                {

                    int cur = q.Peek() * q.Count;
                    if (ret2 < cur)
                    {

                        ret1 = q.Peek();
                        ret2 = cur;
                    }
                    q.Dequeue();
                }

                Console.Write($"{ret1} {ret2}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                q = new(n);

                for (int i = 0; i < m; i++)
                {

                    int cur = ReadInt();
                    if (q.Count < n)
                    {

                        q.Enqueue(cur, cur);
                        continue;
                    }

                    if (q.Peek() < cur)
                    {

                        q.Dequeue();
                        q.Enqueue(cur, cur);
                    }
                }

                sr.Close();
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

class baek1246
{
    static void Main()
    {
        int [] n0_m1 =  Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
        int [] array = new int [n0_m1[1] + 1];
        int answer = 0;
        int answer_index = 1;
        int temp; 
        for (int i = 1; i <= n0_m1[1]; i++)
            array[i] =  int.Parse(Console.ReadLine());
        Array.Sort(array);
        for (int i = 1; i <= n0_m1[1]; i++)
        {
            if (n0_m1[1] - i + 1 > n0_m1[0])
                temp = n0_m1[0]*array[i];
            else
                temp = (n0_m1[1] - i + 1)*array[i];
            if (answer < temp)
            {
                answer = temp;
                answer_index = i;
            }
        }
        Console.WriteLine("{0} {1}",array[answer_index],answer);
    }
}
#elif other2
using System;
using System.IO;

namespace 그리디
{
    class 온라인판매
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            
            int[] nm = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int n = nm[0];
            int m = nm[1];
            
            int[] price = new int[m];
            
            for (int i = 0; i < m; i++)
            {
                int p = int.Parse(sr.ReadLine());
                price[i] = p;
            }
            
            Array.Sort(price);
            Array.Reverse(price);
            
            int max = price[0];
            int mPrice = price[0];
            int cnt = 1;
            for (int i = 1; i < m; i++)
            {
                if(n == cnt)
                {
                    break;
                }
                
                cnt++;
                int egg = price[i] * cnt;
                if (egg >= max)
                {
                    max = egg;
                    mPrice = price[i];
                }
            }
            
            sw.WriteLine($"{mPrice} {max}");
            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}
#endif
}
