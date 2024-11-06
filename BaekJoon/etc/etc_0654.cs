using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 29
이름 : 배성훈
내용 : 경로 찾기
    문제번호 : 11403번

    최단 경로, 플로이드 워셜 문제다
    범위가 100이하 이고 모든 점에 대해서 경로를 조사해야하므로
    플로이드 워셜로 경로가 존재하는지 확인했다
    만약 한점에 관해서였다면 다익스트라로 찾을거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0654
    {

        static void Main654(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[,] fw;

            Solve();

            void Solve()
            {

                Input();

                for (int mid = 0; mid < n; mid++)
                {

                    for (int start = 0; start < n; start++)
                    {

                        if (fw[start, mid] == 0) continue;
                        for (int end = 0; end < n; end++)
                        {

                            if (fw[mid, end] == 0 || fw[start, end] == 1) continue;
                            fw[start, end] = 1;
                        }
                    }
                }

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput());
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        sw.Write($"{fw[i, j]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                fw = new int[n, n];
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        fw[i, j] = ReadInt();
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
using System.IO;

var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int c, n = 0;
while ((c = sr.Read()) != '\n')
{
    if (c == '\r')
    {
        sr.Read();
        break;
    }
    n = 10 * n + c - '0';
}

var linked = new bool[n, n];
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        linked[i, j] = sr.Read() == '1';
        if (sr.Read() == '\r')
            sr.Read();
    }
}
sr.Close();

for (int k = 0; k < n; k++)
{
    for (int i = 0; i < n; i++)
    {
        if (!linked[i, k])
            continue;
        for (int j = 0; j < n; j++)
        {
            if (!linked[k, j])
                continue;
            linked[i, j] = true;
        }
    }
}
var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        sw.Write(linked[i, j] ? 1 : 0);
        sw.Write(j < n - 1 ? ' ' : '\n');
    }
}
sw.Close();
#elif other2
public static class Graph
{
    private static int n;
    private static List<int>[] nodes;
    private static bool[,] visited;

    static Graph()
    {
        n = int.Parse(Console.ReadLine());
        nodes = new List<int>[n + 1];
        int[] temp;
        
        for (int i = 1; i <= n; i++)
        {
            nodes[i] = new();
            temp = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            for (int j = 0; j < n; j++)
            {
                if (temp[j] == 1)
                    nodes[i].Add(j + 1);
            }
        }

        visited = new bool[n + 1, n + 1];
    }

    public static void Main()
    {
        StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

        for (int i = 1; i <= n; i++)
        {
            DFS(i, i);
            
            for (int j = 1; j <= n; j++)
            {
                sw.Write(visited[i, j] ? 1 : 0);
                sw.Write(' ');
            }

            sw.Write('\n');
        }

        sw.Close();
    }

    private static void DFS(int i, int j)
    {
        foreach (int neighbor in nodes[j])
        {
            if (!visited[i, neighbor])
            {
                visited[i, neighbor] = true;
                DFS(i, neighbor);
            }
        }
    }
}
#endif
}
