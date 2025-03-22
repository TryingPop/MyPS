using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 23
이름 : 배성훈
내용 : 걷기
    문제번호 : 1459번

    수학 문제다.
    대각선 이동이 2번이동 하는거보다 가격이 비싸다면
    대각선 이동 없이 가는게 가장 싸다.
    
    이외의 경우 최대한 대각선 이동하는게 좋다.
    그래서 x, y중 작은만큼 대각선으로 이동한다.
    이후 x + y 의 값이 홀수인 경우 짝수가되게 x또는 y로 1번 이동한다.
    이후 대각선으로 이동하는게 좋은기 그냥 대각선이 아닌 방법으로 이동하는지 좋은쪽을 택하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1451
    {

        static void Main1451(string[] args)
        {

            long x, y, w, s;

            Input();

            GetRet();

            void GetRet()
            {

                bool dia = s < w + w;
                long ret = 0;
                if (dia)
                {

                    long min = Math.Min(x, y);
                    ret = min * s;
                    x -= min;
                    y -= min;

                    if ((x + y) % 2 == 1)
                    {

                        if (x > y) x--;
                        else y--;
                        ret += w;
                    }

                    long chk1 = Math.Max(x, y) * s;
                    long chk2 = (x + y) * w;
                    if (chk1 < chk2) ret += chk1;
                    else ret += chk2;
                }
                else ret = (x + y) * w;


                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                x = int.Parse(temp[0]);
                y = int.Parse(temp[1]);
                w = int.Parse(temp[2]);
                s = int.Parse(temp[3]);
            }
        }
    }
}
