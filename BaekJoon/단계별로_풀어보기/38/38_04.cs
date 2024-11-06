using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 18
이름 : 배성훈
내용 : 우주신과의 교감
    문제번호 : 1774번

    38_03과 같은 문제이다
    다만 여기서는 이미 이어진 노드들이 존재한다

    다른 사람 풀이를 봤는데 만약 q의 모든 연산을 하지않고 모두 연결되었을 때 바로 종료하려고 하면
    연결된 갯수를 세는 코드를 따로 작성해야한다
    그리고, 연결 될때 마다 다되었는지 확인해서 탈출해줘야한다

    Dis의 자료형 범위를 잘못 설정해서 여러 번 틀렸다
    입력값을 Dis의 인자를 int로 하는 경우 long x = _x * _x;
    에서 x범위가 100만 까지 가므로 오버플로우가 날 수 있고
    4%에서 검증하는거 같다!

    그리고 Math.Round가 꽤나 비싼 연산인거 같다
    출력에서 Math.Round과 보간으로 두번 연산하게 했는데 처음에는 312ms속도로
    이후 보간으로만 하니 292ms로 속도 향상이 되었다
*/

namespace BaekJoon._38
{
    internal class _38_04
    {

        static void Main4(string[] args)
        {

            Stack<int> s = new();
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 점 갯수, 이미 이어진 선!
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[][] pos = new int[info[0] + 1][];

            for (int i = 1; i <= info[0]; i++)
            {

                pos[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }

            int[] groups = new int[info[0] + 1];

            for (int i = 1; i <= info[0]; i++)
            {

                groups[i] = i;
            }

            // 이미 이어진 곳
            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int chk1 = Find(groups, int.Parse(temp[0]), s);
                int chk2 = Find(groups, int.Parse(temp[1]), s);

                if (chk1 == chk2) continue;

                groups[chk1] = chk2;
            }

            sr.Close();

            // 이제 이어진곳에 맞춰서 ..? 거리 입력해야할거 같다
            PriorityQueue<(int pos1, int pos2, double dis), double> q = new();

            for (int i = 1; i <= info[0]; i++)
            {

                int chk1 = Find(groups, i, s);

                for (int j = 1; j < i; j++)
                {

                    int chk2 = Find(groups, j, s);

                    if (chk1 == chk2) continue;

                    double dis = Dis(pos[i][0] - pos[j][0], pos[i][1] - pos[j][1]);
                    q.Enqueue((i, j, dis), dis);
                }
            }

            double result = 0;

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int chk1 = Find(groups, node.pos1, s);
                int chk2 = Find(groups, node.pos2, s);

                if (chk1 == chk2) continue;

                result += node.dis;

                groups[chk1] = chk2;
            }

            Console.WriteLine($"{result:0.00}");
        }

        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            while(_chk != _groups[_chk])
            {

                _calc.Push(_chk);
                _chk = _groups[_chk];
            }

            while(_calc.Count> 0)
            {

                _groups[_calc.Pop()] = _chk;
            }

            return _chk;
        }

        static double Dis(long _x, long _y)
        {

            if (_x == 0 || _y == 0)
            {

                double result = _x + _y;
                return result < 0 ? -result : result;
            }

            long x = _x * _x;
            long y = _y * _y;

            return Math.Sqrt(x + y);
        }
    }
}
