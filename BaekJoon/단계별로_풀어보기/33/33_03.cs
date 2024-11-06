using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 1
이름 : 배성훈
내용 : 숨박꼭질 3
    문제번호 : 13549번

    현재 코드가 많이 복잡하다
    추후에 다시 풀어서 알기 쉽게 짤 필요가 있다!!

    다른 사람 C++ 풀이를 보니 덱이랑 우선순위 큐를 이용했다
    다익스트라 풀이도 있던데 이것도 추후에 해봐야겠다
*/

namespace BaekJoon._33
{
    internal class _33_03
    {

        static void Main3(string[] args)
        {

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int result = BFS(info);
            Console.WriteLine(result);
        }

        static bool ChkInValidPos(int _x, int _size)
        {

            if (_x < 0 || _x >= _size) return true;
            return false;
        }

        static int BFS(int[] _info)
        {

            // 조건에 의해 가능한 경우의 수 절반 날리기!
            if (_info[1] <= _info[0]) return _info[0] - _info[1];

            // 최대값 2 * _info[1] 까지만 메모리 할당해서 계산할거다
            int MAX = 2 * _info[1] + 1;

            // 방향
            int[] dir = new int[2] { -1, 1 };

            // 해당 지점 도착하는데 걸리는 최소 시간 보관소
            int[] _result = new int[MAX];
            // MAX를 2 * info[1] + 1 로 해도 된다
            // +1 이동한게 가질 수 있는 아래 while 문에서 나올 수 있는 가장 큰 값이다
            Array.Fill(_result, MAX);

            Queue<int> q = new Queue<int>(MAX);
            q.Enqueue(_info[0]);

            // 시작 좌표는 도착하는데 0초
            _result[_info[0]] = 0;

            while(q.Count > 0)
            {

                var node = q.Dequeue();
                int cur = _result[node];

                // 좌우 연산하고 2배수하면서 루프 돈다
                while (node < MAX)
                {

                    // BFS 연산이므로 cur 오름차순이 보장된다
                    // 따라서 아래 if문은 필요없다
                    // if (_result[node] < cur) break;
                    _result[node] = cur;

                    // 좌우 연산
                    for (int i = 0; i < 2; i++)
                    {

                        int x = node + dir[i];
                        if (ChkInValidPos(x, MAX)) continue;
                        // 이미 최단 루트 있으면 다시 안넣는다!
                        if (_result[x] <= cur + 1) continue;

                        _result[x] = cur + 1;
                        q.Enqueue(x);
                    }

                    // 0인 경우면 2배수 해도 0이므로 반복만한다
                    if (node != 0) node *= 2;
                    else break;
                }
#if first
                // 먼저 2배수 계산해서 넣는다
                int chk = 2 * node;
                while (chk < MAX)
                {

                    // 먼저 계산한 경우 1에서  2, 4, 8을 넣었을 때
                    // 2에서 4, 8을 다시 안넣는다
                    // 그리고 0인 경우도 방지 된다!
                    if (_result[chk] <= cur) break;
                    _result[chk] = cur;
                    q.Enqueue(chk);
                    chk *= 2;
                }

                // 현재 노드의 좌우 계산
                for (int i = 0; i < 2; i++)
                {

                    int x = node + dir[i];
                    if (ChkInValidPos(x, MAX)) continue;

                    if (_result[x] <= cur + 1) continue;

                    _result[x] = cur + 1;
                    q.Enqueue(x);
                }
#endif
                // 만약 목적지 값이 계산 되었다면 탈출!
                if (_result[_info[1]] != MAX) q.Clear();
            }

            return _result[_info[1]];
        }
    }
}
