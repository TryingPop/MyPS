using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 26
이름 : 배성훈
내용 : 알고리즘 수업 - 깊이 우선 탐색 2
    문제번호 : 24480번

    앞의 문제를 리스트 배열로 해서 경로 집합 생성한 경우이다!
    조건도 내림차순으로 하는거만 빼면 같다
*/

namespace BaekJoon._32
{
    internal class _32_02
    {

        static void Main2(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 경로 배열 설정
            List<int>[] root = new List<int>[info[0] + 1];

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // 없으면 생성
                root[temp[0]] = root[temp[0]] ?? new();
                root[temp[1]] = root[temp[1]] ?? new();

                root[temp[0]].Add(temp[1]);
                root[temp[1]].Add(temp[0]);
            }

            sr.Close();

            // 정렬 규칙 - 내림차순
            Comparison<int> dec = new Comparison<int>((a, b) => b.CompareTo(a));

            for (int i = 0; i < root.Length; i++)
            {

                // root[i]?.Sort();
                // root[i]?.Reverse();
                root[i]?.Sort(dec);
            }

            int[] visit = new int[info[0] + 1];
            int order = 1;

            // 탐색
            DFS(visit, root, info[2], ref order);

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 1; i < visit.Length; i++)
                {

                    sw.WriteLine(visit[i]);
                }
            }

        }
        
        static void DFS(int[] _visit, List<int>[] _root, int _start, ref int _order)
        {

            _visit[_start] = _order++;

            int len = _root[_start] == null ? 0 : _root[_start].Count;
            
            for (int i = 0; i < len; i++)
            {

                int next = _root[_start][i];
                if (_visit[next] == 0) DFS(_visit, _root, next, ref _order);
            }
        }

    }
}
