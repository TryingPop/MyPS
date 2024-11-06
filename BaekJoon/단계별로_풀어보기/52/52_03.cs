using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 31
이름 : 배성훈
내용 : 책 구매하기
    문제번호 : 11405번

    최소 비용 최대 유량 문제다
    간선 잇는데 인덱스 문제로 2번 틀렸다;
    1 ~ m 을 서점의 인덱스로 했고, m + 1 ~ n + m 을 사람의 인덱스로 했다
    이에 서점에서 람을 잇는데, m + 1 ~ n + m의 정점으로 향하는 간선을 이어야하는데
    n + 1 ~ n + m으로 이어 서점끼리 이어질 수도, 혹은 일부 사람은 소외될 수도 있다;
*/

namespace BaekJoon._52
{
    internal class _52_03
    {

        static void Main3(string[] args)
        {

            int INF = 10_000_001;

            StreamReader sr;
            int n, m;
            int[,] c, f, d;
            Queue<int> q;
            int source, sink;
            int[] dis, before;
            bool[] inQ;

            int ret;
            List<int>[] line;

            Solve();

            void Solve()
            {

                Input();

                Init();

                MCMF();

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                source = 0;
                sink = n + m + 1;
                line = new List<int>[sink + 1];
                line[source] = new(m);
                line[sink] = new(n);

                c = new int[sink + 1, sink + 1];
                f = new int[sink + 1, sink + 1];
                d = new int[sink + 1, sink + 1];
                for (int i = 1; i <= m; i++)
                {

                    line[i] = new(n + 1);

                    line[source].Add(i);
                    line[i].Add(source);

                    for (int j = m + 1; j <= n + m; j++)
                    {

                        line[i].Add(j);
                        c[i, j] = INF;
                    }
                }

                for (int i = m + 1; i <= n + m; i++)
                {

                    line[i] = new(m + 1);
                    line[sink].Add(i);
                    line[i].Add(sink);
                    for (int j = 1; j <= m; j++)
                    {

                        line[i].Add(j);
                    }
                }

                for (int i = m + 1; i <= m + n; i++)
                {

                    c[i, sink] = ReadInt();
                }

                for (int i = 1; i <= m; i++)
                {

                    c[source, i] = ReadInt();
                }

                for (int i = 1; i <= m; i++)
                {

                    for (int j = m + 1; j <= n + m; j++)
                    {

                        int cost = ReadInt();
                        d[i, j] = cost;
                        d[j, i] = -cost;
                    }
                }

                sr.Close();
            }

            void Init()
            {

                q = new(sink + 1);

                dis = new int[sink + 1];
                before = new int[sink + 1];
                inQ = new bool[sink + 1];
            }

            void MCMF()
            {

                ret = 0;

                while (true)
                {

                    Array.Fill(before, -1);
                    Array.Fill(dis, INF);
                    Array.Fill(inQ, false);
                    dis[source] = 0;

                    q.Enqueue(source);
                    inQ[source] = true;
                    before[source] = source;

                    while (q.Count > 0)
                    {

                        int node = q.Dequeue();

                        inQ[node] = false;

                        for (int i = 0; i < line[node].Count; i++)
                        {

                            int next = line[node][i];
                            int nDis = d[node, next];

                            if (c[node, next] - f[node, next] > 0 && dis[next] > dis[node] + nDis)
                            {

                                dis[next] = dis[node] + nDis;
                                before[next] = node;

                                if (!inQ[next])
                                {

                                    inQ[next] = true;
                                    q.Enqueue(next);
                                }
                            }
                        }
                    }

                    if (before[sink] == -1) break;

                    int flow = INF;
                    for (int i = sink; i != source; i = before[i])
                    {

                        flow = Math.Min(flow, c[before[i], i] - f[before[i], i]);
                    }

                    for (int i = sink; i != source; i = before[i])
                    {

                        ret += flow * d[before[i], i];
                        f[before[i], i] += flow;
                        f[i, before[i]] -= flow;
                    }
                }
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
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class FlowLine
{
    public int Flow { get; set; } = 0;
    public int Limit { get; init; }
    public int Cost { get; init; }
    public int Remaining => Limit - Flow;
    public int Starting { get; init; }
    public int Destination { get; init; }
    public FlowLine Reverse { get; init; }

    public FlowLine(int start , int end , int limit , int cost=0 , FlowLine? reverse = null)
    {
        this.Starting = start;
        this.Destination = end;
        this.Limit = limit;
        this.Cost = cost;
        this.Reverse = reverse ?? new(end , start , 0 , -cost , this);
    }
}
class Program {
    public static void Coding() {
        (int customer_count,int store_count) = Cin;
        // 0~customer : 고객
        // customer ~ store : 상점
        // 고객/상점/소스/싱크 순
        int source = customer_count+store_count;
        int sink = source+1;
        LinkedList<FlowLine>[] lines = new LinkedList<FlowLine>[sink+1];
        for(int i=0;i<=sink;i++) lines[i]=new();
        void Push(FlowLine line) {
            lines[line.Starting].AddLast(line);
            lines[line.Destination].AddLast(line.Reverse);
        }
        //고객 -> 싱크
        int[] customer_buy = Cin;
        for(int me=0;me<customer_count;me++) {
            FlowLine line = new(me,sink,customer_buy[me]);
            Push(line);
        }
        //소스 -> 상점
        int[] store_inventory = Cin;
        for(int store=0;store<store_count;store++) {
            FlowLine line = new(source,store+customer_count,store_inventory[store]);
            Push(line);
        }
        //상점 -> 고객
        for(int store=0;store<store_count;store++) {
            int[] expense = Cin;
            int store_index = store + customer_count;
            for(int customer=0;customer<customer_count;customer++) {
                FlowLine line = new(store_index,customer,6145020,expense[customer]);
                Push(line);
            }
        }
        //mcmf
        Queue<int> queue = new();
        int result = 0;
        while(true) {
            int[] dist = new int[sink+1];
            Array.Fill(dist,int.MaxValue>>1);
            FlowLine?[] visited = new FlowLine[sink+1];
            bool[] exist_queue = new bool[sink+1];
            dist[source]=0;
            exist_queue[source]=true;
            queue.Clear();
            queue.Enqueue(source);

            while(queue.Count > 0) {
                int me = queue.Dequeue();
                exist_queue[me] = false;
                foreach(var line in lines[me]) {
                    int other = line.Destination;
                    int next_dist = dist[me]+line.Cost;
                    if (line.Remaining > 0 && next_dist < dist[other]) {
                        dist[other] = next_dist;
                        visited[other] = line;
                        if (exist_queue[other]) continue;
                        queue.Enqueue(other);
                        exist_queue[other] = true;
                    }
                }
            }

            if (visited[sink] is null) break;

            int flow = int.MaxValue;
            for(int p=sink;p!=source;p=visited[p].Starting) {
                flow = Math.Min(flow, visited[p].Remaining);
            }
            for(int p=sink;p!=source;p=visited[p].Starting) {
                result += flow * visited[p].Cost;
                visited[p].Flow += flow;
                visited[p].Reverse.Flow -= flow;
            }
        }

        Cout = result;
    }
}
#endif
}
