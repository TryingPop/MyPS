using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 23
이름 : 배성훈
내용 : 집으로
    문제번호 : 1069번

    문제를 잘못 읽어서 몇 번 틀렸다.
    처음에는 X, Y축 이동으로만 점프 가능한줄 알아서
    BFS 탐색으로 들어갔다

    문제에서는 축이아닌 모든 방향으로 이동가능하고, 
    단순하게 해당 방향으로 가다가 나머지는 걸어가면 되는거 아닌가 생각했다

    그러나 오답이 발생했고, 조금 고민해보니 점프거리를 d라하면 현재에서 d ~ -d만큼 이동할 수 있다
    
*/

namespace BaekJoon._40
{
    internal class _40_08
    {

        static void Main8(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

#if Wrong
            // 이제 점프인지 판별하기?
            // BFS 탐색하자 
            Queue<(int x, int y, double dis)> q = new Queue<(int x, int y, double dis)>();

            int[] dirX = { 1, -1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            q.Enqueue((input[0], input[1], 0));
            double min = 1_000_000;
            while(q.Count > 0)
            {

                var node = q.Dequeue();
                double curDis = ZeroToCurDis(node.x, node.y) + node.dis;

                if (curDis > min) continue;

                for (int i = 0; i < 4; i++)
                {

                    int x = dirX[i] * input[2] + node.x;
                    int y = dirY[i] * input[2] + node.y;

                    double dis = node.dis + input[3];
                    double chkDis = dis + ZeroToCurDis(x, y);
                    if (chkDis >= curDis) continue;

                    if (min > chkDis) min = chkDis;
                    q.Enqueue((x, y, dis));
                }
            }

            Console.WriteLine(min);

#else

            double dis = Math.Sqrt(input[0] * input[0] + input[1] * input[1]);
            // 점프하는게 손해인경우 그냥 가는게 이득이므로 바로 거리를 출력!
            if (input[2] <= input[3]) 
            { 
                
                Console.WriteLine(dis);
                return;
            }

            // 사용한 시간
            double time = 0;

            while (true)
            {

                // 값이 input[2]보다 작게 직선으로 점프를 한다!
                if (dis - input[2] < 0) break;

                // 아직 크니깐 이동한 결과를 조정
                dis -= input[2];
                time += input[3];
            }

            // 이제는 거리가 dis < input[2]인 경우다!
            if (time == 0)
            {

                // 애초에 점프를 안한경우!
                // 남은 거리에서 가상으로 점프했다고 가정
                double chkDis = input[2] - dis;
                if (dis > chkDis + input[3])
                {

                    // 현재 남은 거리가 점프하고 남은거리보다 작은 경우면 점프를 한다!
                    // 점프 했다고 시간 추가
                    time += input[3];
                    // 다시 점프할지 걸어갈지 비교한다
                    if (chkDis < input[3]) time += chkDis;
                    else time += input[3];
                }
                else
                {

                    // 점프 한번으로는 손해지만, 두번해서 도착가능하면 점프를 실행한다
                    if (dis > input[3] * 2) time += input[3] * 2;
                    // 점프 두번이 아니면 그냥 가는게 나으므로 그냥간다!
                    else time += dis;
                }
            }
            else
            {

                // 점프를 한 번이라도 한 경우
                // 그냥 가는게 빠른 경우 그냥 간다
                if (dis < input[3]) time += dis;
                // 점프 간격을 잘 조절하면 점프해서 완주할 수 있다
                // 그래서 점프 하는게 이득이면 바로 점프한다!
                else time += input[3];
            }

            // 시간 출력
            Console.WriteLine(time);
#endif
        }

#if Wrong
        static double ZeroToCurDis(int _curX, int _curY)
        {

            return Math.Sqrt(_curX * _curX + _curY * _curY);
        }
#endif
    }
}
