using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 19
이름 : 배성훈
내용 : 상어의 저녁식사
    문제번호 : 1671번

    이분 매칭 문제다
    어떻게 이분매칭을 해야할지 몰라 이분매칭 개념을 영상으로 다시 살펴보는 중 
    해당 문제의 설명글이 있어 이를 보고 풀었다...

    아이디어는 다음과 같다
    A 상어가 A가 아닌 B 상어를 먹을 때를 두 상어가 매칭되었다고 보면 된다
    그러면, 최대 이분 매칭은 살아남은 최소 상어가 된다

    그래서 이전에는 모든 간선이 있던 반면 
    이제는 특정 간선만 가는 라인이 필요하다
    이분 매칭은 최대한 매칭시켜준다 그래서 따로 정렬할 필요가 없다
    어디까지나 간선의 가중치가 같은 경우에 한해서다

    그리고 앞에서 2번 일하는 문제(열혈 강호 2문제) 노드를 2배로 확장시키는 개념이
    이분 매칭 포문을 2번으로 해결된다
*/
namespace BaekJoon.etc
{
    internal class etc_0576
    {

        static void Main576(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n;
            (int size, int speed, int intel)[] shark;
            List<int>[] line;
            int[] match;
            bool[] visit;

            Solve();

            sr.Close();

            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 0; i < 2; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        Array.Fill(visit, false);
                        if (DFS(j)) ret++;
                    }
                }

                Console.WriteLine(n - ret);
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

            void Input()
            {

                n = ReadInt();

                shark = new (int size, int speed, int intel)[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    shark[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    for (int j = i + 1; j <= n; j++)
                    {

                        if (shark[i].size >= shark[j].size && shark[i].speed >= shark[j].speed && shark[i].intel >= shark[j].intel) line[i].Add(j);
                        else if (shark[i].size <= shark[j].size && shark[i].speed <= shark[j].speed && shark[i].intel <= shark[j].intel) line[j].Add(i);
                    }
                }

                match = new int[n + 1];
                visit = new bool[n + 1];
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
        static List<List<int>> graph;
        static int[] d;
        static bool[] vis;
        static bool DFS(int x)
        {
            for(int i = 0; i < graph[x].Count; i++)
            {
                int tmp = graph[x][i];
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
            
            int n = int.Parse(sr.ReadLine());
            graph = new List<List<int>>(){new List<int>()};
            d = new int[n+1];
            vis = new bool[n+1];
            int count = 0;
            List<List<int>> shark = new List<List<int>>();
            for(int i = 0; i < n; i++)
            {
                List<int> input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse).ToList();

                shark.Add(input.ToList());
            }
            for(int i = 0; i < n; i++)
            {
                graph.Add(new List<int>());
                for(int j = 0; j < n; j++)
                {
                    if(i == j)
                        continue;
                    if(shark[i][0]== shark[j][0] && shark[i][1]== shark[j][1] && shark[i][2]== shark[j][2])
                    {
                        if(i > j)
                            graph[i+1].Add(j+1);
                    }
                    else if(shark[i][0]>= shark[j][0] && shark[i][1]>= shark[j][1] && shark[i][2]>= shark[j][2])
                        graph[i+1].Add(j+1);
                }
            }
            for(int i = 1; i <= n; i++)
            {
                vis = new bool[n+1];
                if(DFS(i))
                    count++;
                vis = new bool[n+1];
                if(DFS(i))
                    count++;
            }
            
            sb.Append(n-count);
            sw.WriteLine(sb);
            sr.Close();
            sw.Close(); 
        }
        static int LIS(List<int> seq)
        {
            List<double> lis = new List<double>();
            for(int i = 0; i< seq.Count; i++)
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
                if(index >= lis.Count)
                    lis.Add(seq[i]);
                else
                    lis[index] = seq[i];
            }
            return lis.Count;
        }
    }
}
#endif
}
