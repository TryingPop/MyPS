using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 14
이름 : 배성훈
내용 : 돌멩이 제거
    문제번호 : 1867번

    이분 매칭 문제다
    이분매칭 힌트를 알고 있어 이분매칭과 엮을려고 했다
    해당 돌이 주어졌을 때, 가로나 세로로 한 번 청소를 해야한다
    그래서 가로와 세로로 엮으면 되지 않을까 생각했고,
    질문 게시판에 다른 사람이 적은 예시로 적용하니 얼추 맞아보였다
    그래서 귀납적으로 정답을 제출하니 이상없이 통과했다

    뒤에 생각해보니 타당한 추론이라 생각이 들었다
    우선 각자 돌에 대해 가로 세로 달리기가 존재한다

    이제 달리기를 간선으로 보면 간선이 최소가 되게 선택해야 한다
    달리기 간선이 최소는 쾨닉의 정리로 이분매칭에서 최대한 매칭한 값과 동형이다

    아래는 사용한 예제이다
        5 8
        1 1
        2 2
        4 4
        5 5
        1 3
        2 3
        4 3
        5 3
    
        x.x..
        .xx..
        .....
        ..xx.
        ..x.x

        답은 4번

        ========

        4 6
        1 1
        1 4
        2 1 
        2 3
        3 1
        3 2

        x..x
        x.x.
        xx..
        ....

        답은 3번

        =======

        5 8
        1 3
        2 4
        3 2
        3 3
        3 4
        3 5
        4 3
        5 3

        ..x..
        ...x.
        .xxxx
        ..x..
        ..x..

        답은 3번
*/

namespace BaekJoon._50
{
    internal class _50_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr;
            List<int>[] line;
            int n;

            int[] match;
            bool[] visit;

            Solve();

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

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                match = new int[n + 1];
                visit = new bool[n + 1];

                int k = ReadInt();
                for (int i = 0; i < k; i++)
                {

                    int r = ReadInt();
                    int c = ReadInt();

                    line[r].Add(c);
                }
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
            
            string[] nk = sr.ReadLine().Split();
            int n = int.Parse(nk[0]);
            int k = int.Parse(nk[1]);
            graph = new List<List<int>>(){new List<int>()};
            d = new int[n+1];
            vis = new bool[n+1];
            int count = 0;
            for(int i = 1; i<=n; i++)
                graph.Add(new List<int>());
            for(int i = 0; i < k; i++)
            {
                List<int> input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse).ToList();
                graph[input[0]].Add(input[1]);
            }
            
            for(int i = 1; i <= n; i++)
            {
                vis = new bool[n+1];
                if(DFS(i))
                    count++;
            }
            
            sb.Append(count);
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
