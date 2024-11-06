using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 18
이름 : 배성훈
내용 : 최소 스패닝 트리 
    문제번호 : 1197번

    스패닝 트리를 신장 트리라고 한다!
    즉, 최소 신장 트리! 만들기다!

    크루스칼 알고리즘으로 최소신장 트리를 만들었다
    입력할 때부터 경로의 가중치가 최소가 담기게 우선순위 큐를 썼다

    최소신장 트리가 완성되어도 큐에 있는 원소를 다뺄 때까지 연산하고,
    매번 파인드하기에 많이 느리다

    실제 속도는 224ms로 통과 빠른사람의 기준 2배 이상 걸린다
*/

namespace BaekJoon._38
{
    internal class _38_02
    {

        static void Main2(string[] args)
        {

            int MAX_VERTEX = 10_000;
            int MAX_LINES = 100_000;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 정점 1부터 시작
            int[] groups = new int[info[0] + 1];
            for (int i = 1; i <= info[0]; i++)
            {

                groups[i] = i;
            }

            PriorityQueue<(int pos1, int pos2, int weight), int> q = new PriorityQueue<(int pos1, int pos2, int weight), int>(info[1]);

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                q.Enqueue((temp[0], temp[1], temp[2]), temp[2]);
            }

            sr.Close();

            Stack<int> s = new Stack<int>();
            int result = 0;
            // Kruskal 알고리즘
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                // Find정의해야한다~.~;
                int pos1 = Find(groups, node.pos1, s);
                int pos2 = Find(groups, node.pos2, s);

                // 이미 연결된 경우
                if (pos1 == pos2) continue;

                // 연결 안되었다면 연결한다
                result += node.weight;
                if (pos1 < pos2) groups[pos2] = pos1;
                else groups[pos1] = pos2;
            }
           
            Console.WriteLine(result);
        }

        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            int chk = _chk;

            while (_groups[chk] != chk)
            {

                _calc.Push(chk);
                chk = _groups[chk];
            }

            while(_calc.Count > 0)
            {

                _groups[_calc.Pop()] = chk;
            }

            return chk;
        }
    }
}
