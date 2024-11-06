using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 팀원 모집
    문제번호 : 11578번

    브루트포스, 비트마스킹 문제다
    처음 풀 때, 비트마스킹을 생각못했다 그래서 배열로 입력받았다
    다 풀고나서 힌트를 보니 비트마스킹이 가능함을 알았다
    모든 경우를 확인하는데 DFS 탐색을 했다

    이후 비트마스킹으로 풀어 제출했다
*/

namespace BaekJoon.etc
{
    internal class etc_0295
    {

        static void Main295(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int m = ReadInt(sr);
            int n = ReadInt(sr);

            // int[][] can = new int[n][];
            int[] can = new int[n];
            for (int i = 0; i < n; i++)
            {

                int sol = ReadInt(sr);

                // can[i] = new int[sol];
                can[i] = 0;
                for (int j = 0; j < sol; j++)
                {

                    // can[i][j] = ReadInt(sr) - 1;
                    can[i] |= (1 << (ReadInt(sr) - 1));
                }
            }

            sr.Close();

            bool[] visit = new bool[n];
            bool[] problem = new bool[m];

            int ret = DFS(can, problem, visit, 0, n);
            if (ret != 100) Console.WriteLine(ret);
            else Console.WriteLine(-1);
        }

        // static int DFS(int[][] _can, bool[] _problem, bool[] _visit, int _s, int _e)
        static int DFS(int[] _can, bool[] _problem, bool[] _visit, int _s, int _e)
        {

            int ret = 100;
            if (_s == _e)
            {

                int p = 0;
                for (int i = 0; i < _visit.Length; i++)
                {

                    if (!_visit[i]) continue;
                    /*
                    for (int j = 0; j < _can[i].Length; j++)
                    {

                        _problem[_can[i][j]] = true;
                    }
                    */

                    p |= _can[i];
                }

                // int solve = _problem.Length;
                int sol = 0;
                for (int i = 0; i < _problem.Length; i++)
                {

                    /*
                    if (!_problem[i]) solve--;

                    _problem[i] = false;
                    */

                    sol |= 1 << i;
                }

                // if (solve < _problem.Length) return ret;
                if (p != sol) return ret;

                ret = 0;
                for (int i = 0; i < _visit.Length; i++)
                {

                    if (_visit[i]) ret++;
                }

                return ret;
            }
            int calc;
            _visit[_s] = true;
            calc = DFS(_can, _problem, _visit, _s + 1, _e);
            ret = calc < ret ? calc : ret;

            _visit[_s] = false;
            calc = DFS(_can, _problem, _visit, _s + 1, _e);
            ret = calc < ret ? calc : ret;

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
