using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 30
이름 : 배성훈
내용 : 컵라면
    문제번호 : 1781번

    그리디, 정렬, 우선순위 큐 문제다
    뒤에서부터 사용하는식으로 풀었다

    가장 큰 데드라인이 k라면
    k부터 시작한다

    다음 데드라인이 m이면
    k - m개의 문제를 풀지 정한다
    여기서 k - m 개에 들어가는 문제는
    데드라인이 m일을 초과하고,
    컵라면을 많이 주는것부터 푼다

    그리고 남은 문제는 우선순위 큐에 저장한다
    그러면서 기간을 줄이면서 최대한 푼다

    이러면 최적해가 보장된다
*/

namespace BaekJoon.etc
{
    internal class etc_0926
    {

        static void Main926(string[] args)
        {

            StreamReader sr;
            int n;
            (int d, int c)[] p;
            PriorityQueue<int, int> pq1, pq2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                while(pq1.Count > 0)
                {

                    int cur = pq1.Dequeue();

                    int r = pq1.Count > 0 ? p[cur].d - p[pq1.Peek()].d : p[cur].d;

                    bool flag = true;
                    while (r > 0)
                    {

                        if (pq2.Count > 0 && p[pq2.Peek()].c >= p[cur].c)
                        {

                            r--;
                            ret += p[pq2.Dequeue()].c;
                        }
                        else
                        {

                            ret += p[cur].c;
                            r--;
                            flag = false;
                            break;
                        }
                    }

                    while(r > 0 && pq2.Count > 0)
                    {

                        r--;
                        ret += p[pq2.Dequeue()].c;
                    }

                    if (flag) pq2.Enqueue(cur, cur);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                p = new (int d, int c)[n];

                pq1 = new(n, Comparer<int>.Create((x, y) =>
                {

                    int ret = p[y].d.CompareTo(p[x].d);
                    if (ret == 0) return p[y].c.CompareTo(p[x].c);
                    return ret;
                }));

                pq2 = new(n, Comparer<int>.Create((x, y) =>
                {

                    int ret = p[y].c.CompareTo(p[x].c);
                    if (ret == 0) return p[y].d.CompareTo(p[x].d);
                    return ret;
                }));

                for (int i = 0; i < n; i++)
                {

                    p[i] = (ReadInt(), ReadInt());
                    pq1.Enqueue(i, i);
                }

                sr.Close();
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
namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int n = int.Parse(input.ReadLine());
            List<(int, int)> list = new();
            for(int i = 0; i < n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '),int.Parse);
                list.Add((temp[0], temp[1]));
            }
            list.Sort((x,y) =>
            {
                int result = x.Item1.CompareTo(y.Item1);
                if(result == 0)
                    result = y.Item2.CompareTo(x.Item2);
                return result;
            });
            PriorityQueue<int,int> pq = new();
            int time = 1;
            int answer = 0;
            for(int i = 0; i < n; i++)
            {
                if (list[i].Item1 >= time)
                {
                    time++;
                    pq.Enqueue(list[i].Item2, list[i].Item2);
                }
                else
                {
                    if(pq.Peek() < list[i].Item2)
                    {
                        pq.Dequeue();
                        pq.Enqueue(list[i].Item2, list[i].Item2);
                    }
                }
            }
            while(pq.Count> 0)
            {
                answer += pq.Dequeue();
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
    }
}
#endif
}
