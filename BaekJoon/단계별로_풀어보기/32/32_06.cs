using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 26
이름 : 배성훈
내용 : DFS와 BFS
    문제번호 : 1260번

    그냥 앞에 방법대로 풀었다
    DFS를 재귀말고 스택으로 하려고 했으나 List의 원소를 pop할 수 없기에 연산량만 늘어날거 같았다
    그래서 주석처리된 재귀로 해서 제출했다
*/

namespace BaekJoon._32
{
    internal class _32_06
    {

        static void Main6(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 경로
            List<int>[] root = new List<int>[info[0] + 1];

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                root[temp[0]] = root[temp[0]] ?? new();
                root[temp[1]] = root[temp[1]] ?? new();

                root[temp[0]].Add(temp[1]);
                root[temp[1]].Add(temp[0]);
            }

            sr.Close();

            // 조건에 맞게 오름차순 정렬
            for (int i = 1; i < root.Length; i++)
            {

                root[i]?.Sort();
            }

            // 입력
            StringBuilder dfs = new StringBuilder();
            StringBuilder bfs = new StringBuilder();

            // 탐색
            DFS(root, info[2], info[0], dfs);
            BFS(root, info[2], info[0], bfs);

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                // DFS 먼저 출력하고 BFS 출력
                sw.WriteLine(dfs);
                sw.Write(bfs);
            }
        }

        // 탐색 숫자 기록
        static void Record(StringBuilder _sb, int _record)
        {

            _sb.Append(_record.ToString());
            _sb.Append(' ');
        }

        static void DFS(List<int>[] _root, int _start, int _num, StringBuilder _sb, bool[] chk = null)
        {

            if (chk == null) chk = new bool[_num + 1];
            chk[_start] = true;

            // 처음 기록
            Record(_sb, _start);

            /*
            // 제출에는 아래 재귀 이용
            int len = _root[_start] == null ? 0 : _root[_start].Count;

            for (int i = 0; i < len; i++)
            {

                int next = _root[_start][i];
                if (!chk[next])
                {

                    DFS(_root, next, _num, _sb, chk);
                }
            }
            */

            // 스택 방법
            Stack<List<int>> saved = new Stack<List<int>>();

            if (_root[_start] != null) saved.Push(_root[_start]);

            while (saved.Count > 0)
            {

                List<int> node = saved.Pop();

                for (int i = 0; i < node.Count; i++)
                {

                    int next = node[i];
                    // 이미 탐색했다면 다음으로
                    // 리스트에서 pop할 수 있으면 pop하는게 좋다!
                    if (chk[next]) continue;

                    chk[next] = true;
                    saved.Push(node);
                    if (_root[next] != null) saved.Push(_root[next]);
                    Record(_sb, next);
                    break;
                }
            }
        }

        static void BFS(List<int>[] _root, int _start, int _num, StringBuilder _sb)
        {

            bool[] chk = new bool[_num + 1];
            chk[_start] = true;

            // 처음 기록
            Record(_sb, _start);

            Queue<List<int>> saved = new Queue<List<int>>();

            if (_root[_start] != null) saved.Enqueue(_root[_start]);

            while (saved.Count > 0)
            {

                List<int> node = saved.Dequeue();

                for (int i = 0; i < node.Count; i++)
                {

                    int next = node[i];

                    if (!chk[next])
                    {

                        chk[next] = true;
                        if (_root[next] != null) saved.Enqueue(_root[next]);

                        Record(_sb, next);
                    }
                }
            }
        }
    }
}
