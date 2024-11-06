using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 26
이름 : 배성훈
내용 : 알고리즘 수업 - 너비 우선 탐색 2
    문제번호 : 24445번

    조건만 내림차순으로 다를 뿐 32_03과 같다
*/

namespace BaekJoon._32
{
    internal class _32_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            List<int>[] root = new List<int>[info[0] + 1];

            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int pos1 = int.Parse(temp[0]);
                int pos2 = int.Parse(temp[1]);

                root[pos1] = root[pos1] ?? new();
                root[pos2] = root[pos2] ?? new();

                root[pos1].Add(pos2);
                root[pos2].Add(pos1);
            }

            sr.Close();

            Comparison<int> dec = new Comparison<int>((a, b) => b.CompareTo(a));
            for (int i = 1; i < root.Length; i++)
            {

                root[i]?.Sort(dec);
            }

            int[] visit = new int[info[0] + 1];

            BFS(visit, root, info[2]);

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 1; i < visit.Length; i++)
                {

                    sw.WriteLine(visit[i]);
                }
            }
        }

        static void BFS(int[] _visit, List<int>[] _root, int _start)
        {

            int order = 1;
            _visit[_start] = order++;

            Queue<List<int>> queue = new Queue<List<int>>();

            if (_root[_start] != null) queue.Enqueue(_root[_start]);

            while (queue.Count > 0)
            {

                List<int> node = queue.Dequeue();

                for (int i = 0; i < node.Count; i++)
                {

                    int next = node[i];
                    if (_visit[next] == 0)
                    {

                        _visit[next] = order++;
                        if (_root[next] != null) queue.Enqueue(_root[next]);
                    }
                }
            }
        }
    }
}
