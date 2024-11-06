using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 유미
    문제번호 : 17286번

    완전탐색해서 풀면 된다
    다만 그리디로 합치는 접근을 해서 엄청 틀렸다
    하나하나 예제로 확인하다가 로직이 
    잘못되었음을 알고 다시짜니 바로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0135
    {

        static void Main135(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            (int x, int y) yumi = (ReadInt(sr), ReadInt(sr));

            (int x, int y)[] humans = new (int x, int y)[3];

            for (int i = 0; i < 3; i++)
            {

                humans[i].x = ReadInt(sr);
                humans[i].y = ReadInt(sr);
            }

            sr.Close();

            // dis[idx]는 idx 좌표가 아닌 남은 두 점을 지나는 거리
            float[] dis = new float[3];
            dis[0] = GetDis(humans[1], humans[2]);
            dis[1] = GetDis(humans[2], humans[0]);
            dis[2] = GetDis(humans[0], humans[1]);

            float ret = 200.0f;
            for (int i = 0; i < 3; i++)
            {

                float totalDis = 200f;

                // 0으로 먼저 간다고 생각하자
                // 다음으로 1, 2 중 하나를 가게되고
                // 이후 나머지를 가기에
                // 그러면 1, 2 직선의 거리를 무조건 지나가야한다!
                float firDis = GetDis(yumi, humans[i]) + dis[i];

                
                for (int j = 0; j < 3; j++)
                {

                    if (i == j) continue;
                    float calc = firDis + dis[j];

                    if (totalDis > calc) totalDis = calc;
                }
                

                if (ret > totalDis) ret = totalDis;
            }

            Console.WriteLine($"{MathF.Truncate(ret):0}");
        }

        static float GetDis((float x, float y) pos1, (float x, float y) pos2)
        {

            float ret;
            ret = (pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y);
            ret = MathF.Sqrt(ret);
            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }
                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
