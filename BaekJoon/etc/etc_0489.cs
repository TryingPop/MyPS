using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : Musical Chairs
    문제번호 : 17857번

    큐, 시뮬레이션 문제다
    조건대로 시뮬레이션 돌리면서 풀었다
    속도를 더 빠르게 내고 싶다면 세그먼트 트리를 이용하면 될거 같다

    다른 사람 풀이를 보니, 리스트로 푼게 있어 다시 따라 풀었다
    리스트가 72ms로 큐 316ms 보다 빠르다 
*/

namespace BaekJoon.etc
{
    internal class etc_0489
    {

        static void Main489(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int n = ReadInt();

#if Queue

            // Queue를 이용한 풀이
            Queue<(int val, int idx)> q = new(n);
            for (int i = 0; i < n; i++)
            {

                q.Enqueue((ReadInt(), i));
            }
            sr.Close();

            while(q.Count > 1)
            {

                int find = q.Peek().val;
                find = (find - 1) % q.Count;
                int cur = 0;

                while(cur++ < find)
                {

                    var temp = q.Dequeue();
                    q.Enqueue(temp);
                }

                q.Dequeue();
            }

            Console.WriteLine(q.Dequeue().idx + 1);
#else

            List<(int val, int idx)> list = new(n);
            for (int i = 0; i < n; i++)
            {

                list.Add((ReadInt(), i));
            }

            int idx = 0;
            while(list.Count > 1)
            {

                idx = idx % list.Count;
                int find = list[idx].val;
                find = (find - 1) % list.Count;

                idx = (idx + find) % list.Count;
                list.RemoveAt(idx);
            }

            Console.WriteLine(list[0].idx + 1);
#endif
            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other
// cs17857 - rby
// 2023-06-28 00:04:33
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs17857
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

            List<int> list = new List<int>();
            for (int i = 0; i < N; i++)
                list.Add(i);
            

            int idx = 0;
            while(list.Count> 1)
            {
                idx = (idx + line[list[idx]] - 1) % list.Count;
                //Console.WriteLine("REMOVE {0}", list[idx] + 1);
                list.RemoveAt(idx);
                idx %= list.Count;
            }


            sw.Write(list[0] + 1);
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
