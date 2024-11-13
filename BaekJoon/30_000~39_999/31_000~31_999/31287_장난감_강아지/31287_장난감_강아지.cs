using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 장난감 강아지
    문제번호 : 31287번

    처음에는 한 사이클돌고
    끝지점을 확인했다
    그리고 각점에 대해 벡터 연산으로 할려고 했는데
    조건만 복잡해지고 중간에 실수가 잦아 여러 번 틀렸다

    그래서 다른 아이디어인가 고민을하고
    한번 명령을 수행 후 끝나는 위치는
    많아야 시작지점과 info[0] 차이가 남을 알았다

    그래서 n번의 명령을 수행하고 끝나는 위치가 0, 0과 택시거리로 
    info[0]보다 큰 경우 시작 위치로 절대 못간다!
    그래서 사이클을 탈출하면 된다는 원리로 브루트포스로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0130
    {

        static void Main130(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            string str = sr.ReadLine().Trim();
            sr.Close();

            (int x, int y)[] pos = new (int x, int y)[info[0]];

            bool ret = false;
            for (int t = 0; t < info[1]; t++)
            {

                for (int i = 0; i < info[0]; i++)
                {

                    if (i != 0) pos[i] = pos[i - 1];
                    else pos[i] = pos[info[0] - 1];
                    switch (str[i])
                    {

                        case 'U':
                            pos[i].y += 1;
                            break;

                        case 'D':
                            pos[i].y -= 1;
                            break;

                        case 'L':
                            pos[i].x -= 1;
                            break;

                        case 'R':
                            pos[i].x += 1;
                            break;

                        default:
                            break;
                    }

                    if (!ret && pos[i].x == 0 && pos[i].y == 0)
                    {

                        ret = true;
                        break;
                    }
                }

                int calc1 = pos[info[0] - 1].x;
                calc1 = calc1 < 0 ? -calc1 : calc1;
                int calc2 = pos[info[0] - 1].y;
                calc2 = calc2 < 0 ? -calc2 : calc2;

                if (ret || calc1 + calc2 > info[0]) break;
            }


            if (ret) Console.WriteLine("YES");
            else Console.WriteLine("NO");
        }

    }
}
