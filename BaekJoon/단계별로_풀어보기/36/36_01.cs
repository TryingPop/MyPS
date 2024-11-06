using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 13
이름 : 배성훈
내용 : 트리의 부모 찾기
    문제번호 : 11725번

    처음에 다르게 입력 받는 법이 있는지 고민했으나,
    양방향 길찾기의 하나로 보고 풀었다
    루트(맨 꼭대기의 부모노드)가 1로 설정되어져 있고 부모 노드를 찾는 것이기에 
    BFS로 자식들을 탐색하며 넣었다
*/

namespace BaekJoon._36
{
    internal class _36_01
    {

        static void Main1(string[] args)
        {

            // 트리의 연결 선
            List<int>[] roots;
            // 부모노드 정보
            int[] result;

            // 입력
            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                int len = int.Parse(sr.ReadLine());

                roots = new List<int>[len + 1];
                result = new int[len + 1];

                for (int i = 1; i < len + 1; i++)
                {

                    roots[i] = new();
                }

                for (int i = 0; i < len - 1; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    roots[temp[0]].Add(temp[1]);
                    roots[temp[1]].Add(temp[0]);
                }
            }

            // 탐색
            // 다른 사람을 보니 DFS로 했다
            BFS(roots, result);

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 2; i < result.Length; i++)
                {

                    sw.Write(result[i]);
                    sw.Write('\n');
                }
            }
        }

        static void BFS(List<int>[] _roots, int[] _result)
        {

            Queue<int> q = new Queue<int>();

            q.Enqueue(1);
            // root = -1
            _result[1] = -1;

            while(q.Count > 0)
            {

                var cur = q.Dequeue();

                for (int i = 0; i < _roots[cur].Count; i++)
                {

                    int next = _roots[cur][i];

                    // 부모노드면 패스
                    if (_result[next] != 0) continue;

                    // 자식 노드면 부모 등록
                    _result[next] = cur;
                    q.Enqueue(next);
                }
            }
        }
    }
}
