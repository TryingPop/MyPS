using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

/*
날짜 : 2023. 12. 27
이름 : 배성훈
내용 : 숨박꼭질
    문제번호 : 1697번

    BFS 단원이라 BFS로 풀었다
    이동할 수 있는 좌표를 모두 저장한 뒤 해당 좌표로 이동한다

    조금만 더 찾아보면 BFS가 아닌 log2의 시간으로 풀 수 있을거 같다
*/

namespace BaekJoon._32
{
    internal class _32_10
    {

        static void Main10(string[] args)
        {

            int[] pos = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] chk = new int[200_000 + 1];

            BFS(chk, pos);

            Console.WriteLine(chk[pos[1]]);
        }

        static void BFS(int[] _chk, int[] _pos)
        {

            // 이건 변하지 않는다!
            if (_pos[0] - _pos[1] >= 0)
            {

                _chk[_pos[1]] = _pos[0] - _pos[1];
                return;
            }


            // 일단 BFS 탐색하자!
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(_pos[0]);
            while (queue.Count > 0)
            {

                if (_chk[_pos[1]] != 0)
                {

                    queue.Clear();
                    break;
                }

                int node = queue.Dequeue();
                int curStep = _chk[node] + 1;

                for (int i = 0; i < 3; i++)
                {

                    int next = GetNextNum(node, i);

                    if (ChkInValid(next)
                        || _chk[next] != 0) continue;
                    _chk[next] = curStep;
                    queue.Enqueue(next);
                }
            }
        }

        static bool ChkInValid(int _num)
        {

            if (_num > 200_000) return true;
            else if (_num < 0) return true;

            return false;
        }

        static int GetNextNum(int _num, int _idx)
        {

            if (_idx == 0) return _num - 1;
            if (_idx == 1) return _num + 1;
            return _num * 2;
        }

    }
}
