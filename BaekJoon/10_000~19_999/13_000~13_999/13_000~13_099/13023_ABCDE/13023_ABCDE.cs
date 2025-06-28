using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : ABCDE
    문제번호 : 13023번

    그래프 이론, 그래프 탐색, 깊이 우선 탐색, 백트래킹 문제다

    브루트 포스 + 백트래킹으로 풀었다
    DFS 탐색으로 탐색했다
*/

namespace BaekJoon.etc
{
    internal class etc_0245
    {

        static void Main245(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            List<int>[] lines = new List<int>[n];

            for(int i = 0; i < n; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                lines[f].Add(b);
                lines[b].Add(f);
            }

            sr.Close();

            bool[] visit = new bool[n];

            bool ret = false;
            for (int i = 0; i < n; i++)
            {

                visit[i] = true;
                ret = DFS(lines, visit, 1, i);
                visit[i] = false;

                if (ret) break;
            }

            Console.WriteLine(ret ? 1 : 0);
        }

        static bool DFS(List<int>[] _lines, bool[] _visit, int _depth, int _start)
        {

            if (_depth == 5)
            {

                return true;
            }

            bool ret = false;
            for (int i = 0; i < _lines[_start].Count; i++)
            {

                int next = _lines[_start][i];
                if (_visit[next]) continue;
                _visit[next] = true;
                ret |= DFS(_lines, _visit, _depth + 1, next);
                _visit[next] = false;
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
}
