using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 노트북의 주인을 찾아서
    문제번호 : 1298번

    이분 매칭 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0579
    {

        static void Main579(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            int n;
            List<int>[] line;
            int[] match;
            bool[] visit;

            Solve();
            sr.Close();

            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                n = ReadInt();

                line = new List<int>[n + 1];
                match = new int[n + 1];
                visit = new bool[n + 1];

                for (int i = 1; i<=n; i++)
                {

                    line[i] = new();
                }

                int m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Add(b);
                }
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Data;

namespace Algorithm
{ 
    
    class Program
    {
        static List<List<int>> graphs = new List<List<int>>();
        static int[] d;
        static bool[] vis;

        static bool DFS(int x)
        {
            for(int i = 0; i < graphs[x].Count();i++)
            {
                int tmp = graphs[x][i];
                if(vis[tmp])
                    continue;
                vis[tmp] = true;
                if(d[tmp] == 0 || DFS(d[tmp])) 
                {
                    d[tmp] = x;
                    return true;
                }

            }
            return false;
        }
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            string[] nm = sr.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            d = new int[n+1];
            vis = new bool[n+1];
            for(int i = 0; i <=n; i++)
                graphs.Add(new List<int>());
            for(int i = 0; i < m; i++)
            {
                string[] ab = sr.ReadLine().Split();
                int a = int.Parse(ab[0]);
                int b = int.Parse(ab[1]);
                graphs[a].Add(b);
            }
            int cnt = 0;
            for(int i = 1; i<=n; i++)
            {
                Array.Clear(vis,0,vis.Length);
                if(DFS(i))
                    cnt++;
            }
            sb.Append(cnt);
            sw.WriteLine(sb);  
            sw.Close();
            sr.Close();
            sw.Close(); 
        }
        static int LIS(List<int> seq)
        {
            List<double> lis = new List<double>();
            for(int i = 0; i< seq.Count(); i++)
            {
                int index = lis.BinarySearch(seq[i]);
                if(index < 0)
                    index = Math.Abs(index)-1;
                /*
                else
                {
                    lis[index]-=0.5f;
                    index++;
                }
                */
                if(index >= lis.Count())
                    lis.Add(seq[i]);
                else
                    lis[index] = seq[i];
            }
            return lis.Count();
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onekara
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string[] ss = s.Split();

            int n = int.Parse(ss[0]);
            int m = int.Parse(ss[1]);

            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            for (int i = 0; i < m; i++)
            {
                s = Console.ReadLine();
                ss = s.Split();

                int x = int.Parse(ss[0]);
                int y = int.Parse(ss[1]) + 1000;

                if (!graph.ContainsKey(x))
                {
                    graph[x] = new List<int>();
                }

                graph[x].Add(y);
            }

            int ans = 0;
            Dictionary<int, int> toOwner = new Dictionary<int, int>();
            for (int i = 1; i <= n; i++)
            {
                if (!graph.ContainsKey(i))
                    continue;
                if (AddPoint(i, graph, toOwner))
                    ans++;
            }

            Console.WriteLine(ans);
        }

        static bool AddPoint(int point, Dictionary<int, List<int>> graph, Dictionary<int, int> toOwner)
        {
            Queue<List<int>> queue = new Queue<List<int>>();
            List<int> firstPL = new List<int>();
            firstPL.Add(point);

            List<int> list = new List<int>();

            queue.Enqueue(firstPL);

            while (queue.Count > 0)
            {
                List<int> pl = queue.Dequeue();
                int p = pl.Last();

                for (int i = 0; i < graph[p].Count; i++)
                {
                    int q = graph[p][i];
                    if (list.Contains(q))
                    {
                        continue;
                    }
                    else
                    {
                        if (toOwner.ContainsKey(q))
                        {
                            list.Add(q);
                            List<int> nextPL = new List<int>(pl);
                            nextPL.Add(q);
                            nextPL.Add(toOwner[q]);
                            queue.Enqueue(nextPL);
                        }
                        else
                        {
                            for (int j = 0; j < pl.Count - 1; j += 2)
                            {
                                toOwner[pl[j + 1]] = pl[j];
                            }
                            toOwner[q] = pl.Last();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
#elif other3
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
class Program {
    public static void Coding() {
        (int count, int expect) = Cin;
        LinkedList<int>[] predicts = new LinkedList<int>[count];
        for(int i=0;i<count;i++) predicts[i]=new();
        for(int i=0;i<expect;i++) {
            (int a,int b) = Cin;
            predicts[--a].AddLast(--b);
        }
        int?[] book = new int?[count];
        bool[] visited;
        bool dfs(int me,int exclude) {
            if (visited[me]) return false;
            visited[me]=true;
            foreach(var target in predicts[me]) {
                if (target == exclude) continue;
                if (book[target] is int other) {
                    if (dfs(other,target)) {
                        book[target] = me;
                        return true;
                    }
                    continue;
                } else {
                    book[target] = me;
                    return true;
                }
            }
            return false;
        }
        for(int x=0;x<count;x++) {
            visited = new bool[count];
            dfs(x,-1);
        }
        Cout = book.Count(x=>x is not null);
    }
}
#endif
}
