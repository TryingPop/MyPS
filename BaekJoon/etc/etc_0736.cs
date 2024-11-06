using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 29
이름 : 배성훈
내용 : 즉흥 여행 (Easy)
    문제번호 : 26146번

    SCC, DFS 문제다
    처음에는 SCC가 1개인지 확인하는 줄알아서
    코사라주 알고리즘으로 SCC확인을 했다

    이후에 조금 더 생각해보니 그냥 1번점에서 모든 점을 방문 했는지 확인하고
    역방향도 제대로 오는지 확인만 했다(코사라주)

    이렇게 제출하니 500ms에 통과된다
*/

namespace BaekJoon.etc
{
    internal class etc_0736
    {

        static void Main736(string[] args)
        {

            string YES = "Yes";
            string NO = "No";

            StreamReader sr;
            List<int>[] line;
            List<int>[] revLine;

            Stack<int> s;
            int n, m;
            bool[] visit;

            Solve();

            void Solve()
            {

                Input();

                DFS1(1);
                for (int i = 2; i <= n; i++)
                {

                    if (visit[i]) continue;

                    Console.Write(NO);
                    return;
                }

                DFS2(s.Pop());

                for (int i = 1; i <= n; i++)
                {

                    if (!visit[i]) continue;

                    Console.Write(NO);
                    return;
                }

                Console.Write(YES);
            }

            void DFS1(int _n)
            {

                if (visit[_n]) return;
                visit[_n] = true;

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;

                    DFS1(next);
                }

                s.Push(_n);
            }

            void DFS2(int _n)
            {

                if (!visit[_n]) return;
                visit[_n] = false;

                for (int i = 0; i < revLine[_n].Count; i++)
                {

                    int next = revLine[_n][i];
                    if (!visit[next]) continue;

                    DFS2(next);
                }

                return;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                line = new List<int>[n + 1];
                revLine = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                    revLine[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Add(b);
                    revLine[b].Add(f);
                }

                s = new();
                visit = new bool[n + 1];
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
StreamReader reader = new(Console.OpenStandardInput());
StreamWriter writer = new(Console.OpenStandardOutput());

var input = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
int dotcount = input[0], linecount = input[1];

LinkedList<int>[] lines = new LinkedList<int>[dotcount];

for (int i = 0; i < dotcount; i++)
{
    lines[i] = new();
}

for (int i = 0; i < linecount; i++)
{
    input = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
    lines[input[0]-1].AddLast(input[1]-1);
}

Stack<int> stack = new();
bool[] finished = new bool[dotcount];
int[] id = new int[dotcount];

int next = 0,counting = 0;
int dfs(int n)
{
    int parent = id[n] = ++next;
    stack.Push(n);

    foreach (var l in lines[n])
    {
        if (id[l] is 0) parent = Math.Min(parent, dfs(l));
        else if (!finished[l]) parent = Math.Min(parent, id[l]); 
    }

    if (parent == id[n])
    {
        int ret;
        do
        {
            ret = stack.Pop();
            finished[ret] = true;
        } while (ret != n);

        counting++;
    }

    return parent;
}


for (int i = 0; i < dotcount; i++)
{
    if (finished[i] || id[i] is not 0) continue;
    dfs(i);
}
writer.WriteLine(counting == 1  ?"Yes" : "No");
writer.Flush();
#endif
}
