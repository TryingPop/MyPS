using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 28
이름 : 배성훈
내용 : 문제집
    문제번호 : 1766번

    dfs만으로 풀릴거 같은 느낌을 받아 dfs로 여러차례 도전했다
    여러 차례 도전해보고 dfs 만으로는 불가능함을 느꼈다

    다음을 입력받으면
        4 3
        4 1
        4 2
        1 3

    4 1 2 3을 출력해야하는데,
    dfs 탐색을 할 경우 4 -> 1로 이동하고 다음 들어오는 것은 2이다 
    즉, 현재 것을 확인하고 다음 비교할 것은 현재꺼 뿐만 아니라 아직 진행안된 부모노드의 값도 비교해야한다
    ... 그래서 결국 우선순위 큐와 BFS를 위상 정렬으로 해결했다
*/

namespace BaekJoon._43
{
    internal class _43_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            List<int>[] lines = new List<int>[info[0] + 1];
            

            for (int i = 1; i <= info[0]; i++)
            {

                lines[i] = new();
            }

            int[] degree = new int[info[0] + 1];
            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int b = int.Parse(temp[1]);
                lines[int.Parse(temp[0])].Add(b);
                degree[b]++;
            }

            sr.Close();

            /*
            for (int i = 1; i <= info[0]; i++)
            {

                lines[i].Sort();
            }
            */

            // bool[] visit = new bool[info[0] + 1];
            int[] result = new int[info[0]];
            int idx = 0;

            PriorityQueue<int, int> q = new PriorityQueue<int, int>();

            for (int i = 1; i <= info[0]; i++)
            {

                // DFS(lines, degree, i, i, result, ref idx);

                if (degree[i] == 0) q.Enqueue(i, i);
            }


            while (q.Count > 0)
            {

                var node = q.Dequeue();
                result[idx++] = node;

                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i];

                    if (--degree[next] == 0)
                    {

                        q.Enqueue(next, next);
                    }
                }
            }


            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < info[0]; i++)
            {

                sw.Write(result[i]);
                sw.Write(' ');
            }

            sw.Close();
        }

        /*
        static void DFS(List<int>[] _lines, int[] _degree, int _cur, int _chk, int[] _result, ref int _idx)
        {

            if (_degree[_cur] != 0) return;
            _result[_idx++] = _cur;
            _degree[_cur]--;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];
                if (--_degree[next] == 0 && _chk > next)
                {

                    DFS(_lines, _degree, next, _chk, _result, ref _idx);
                }
            }
        }
        */
    }
}
