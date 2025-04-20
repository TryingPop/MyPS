using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 경로 찾기
    문제번호 : 2479번

    비트마스킹, 탐색 문제다
    경로를 이어주는 것을 비트마스킹으로 했다
    그리고 길 탐색은 DFS로 이전 노드들을 기록하며 찾아갔다
    그런데 BFS로 찾아가는게 더 좋아보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0277
    {

        static void Main277(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            List<int>[] lines = new List<int>[n];
            for (int i = 0; i < n; i++)
            {

                lines[i] = new();
            }

            int len = ReadInt(sr);
            HashSet<int> set = new(len);
            for (int i = 0; i < len; i++)
            {

                set.Add(1 << i);
            }

            int[] num = new int[n];

            for (int i = 0; i < n; i++)
            {

                num[i] = ReadBit(sr);
            }

            int f = ReadInt(sr) - 1;
            int t = ReadInt(sr) - 1;

            sr.Close();

            if (f == t)
            {

                // 시작 끝이 같은 경우 노드 출력하고 종료
                Console.WriteLine(f + 1);
                return;
            }

            for (int i = 0; i < n - 1; i++)
            {
                
                for (int j = i + 1; j < n; j++)
                {

                    // 경로 이어주기
                    int calc = num[i] ^ num[j];
                    if (set.Contains(calc))
                    {

                        lines[i].Add(j);
                        lines[j].Add(i);
                    }
                }
            }

            int[] dis = new int[n];
            bool[] visit = new bool[n];
            Array.Fill(dis, -1);

            // 갈 수 있는지 탐색
            DFS(lines, dis, f, visit, t);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            if (dis[t] == -1) sw.WriteLine(-1);
            else
            {

                Stack<int> ret = new Stack<int>(n);
                int cur = t;
                while (cur != -1)
                {

                    ret.Push(cur + 1);
                    cur = dis[cur];
                }

                while(ret.Count > 0)
                {

                    sw.Write(ret.Pop());
                    sw.Write(' ');
                }
            }

            sw.Close();
        }
        
        static void DFS(List<int>[] _lines, int[] _before, int _cur, bool[] _visit, int _end)
        {

            if (_visit[_end]) return;
            _visit[_cur] = true;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];
                if (_visit[next]) continue;

                _before[next] = _cur;
                DFS(_lines, _before, next, _visit, _end);
            }
        }

        static int ReadBit(StreamReader _sr)
        {

            int c, ret = 0;
            int idx = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                if (c == '1') ret |= 1 << idx;
                idx++;
            }

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// #nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nk[0];
        var k = nk[1];

        var forward = new ulong[n];
        var reverse = new Dictionary<ulong, int>();
        for (var idx = 0; idx < n; idx++)
        {
            var c = sr.ReadLine().Select((ch, idx) => (ulong)(ch - '0') << idx).Aggregate((l, r) => l | r);

            forward[idx] = c;
            reverse.Add(c, idx);
        }

        var srcdst = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var src = srcdst[0] - 1;
        var dst = srcdst[1] - 1;

        var visited = new bool[n];
        var prevArr = new int?[n];

        var q = new Queue<(int prev, int curr)>();
        q.Enqueue((src, src));

        while (q.TryDequeue(out var s))
        {
            var (prev, curr) = s;

            if (visited[curr])
                continue;

            visited[curr] = true;
            prevArr[curr] = prev;

            for (var idx = 0; idx < 32; idx++)
            {
                var next = (1UL << idx) ^ forward[curr];
                if (!reverse.TryGetValue(next, out var nextIdx))
                    continue;

                if (visited[nextIdx])
                    continue;

                q.Enqueue((curr, nextIdx));
            }
        }

        var path = new List<int>();
        var exists = true;

        path.Clear();
        do
        {
            path.Add(dst);
            if (!prevArr[dst].HasValue)
            {
                exists = false;
                break;
            }

            dst = prevArr[dst].Value;
        } while (src != dst);

        path.Reverse();

        if (exists)
        {
            sw.Write($"{1 + src} ");
            foreach (var v in path)
                sw.Write($"{1 + v} ");

            sw.WriteLine();
        }
        else
        {
            sw.WriteLine(-1);
        }
    }
}

#endif
}
