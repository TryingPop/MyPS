using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 부재중 전화
    문제번호 : 1333번

    구현, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1436
    {

        static void Main1436(string[] args)
        {

            int n, l, d;
            bool[] music;

            Input();

            SetMusicTime();

            GetRet();

            void GetRet()
            {

                for (int i = 0; ; i += d)
                {

                    if (music[i]) continue;
                    Console.Write(i);
                    return;
                }
            }

            void SetMusicTime()
            {

                music = new bool[4_000 + 1];
                int time = 0;

                for (int i = 0; i < n; i++)
                {


                    for (int j = 0; j < l; j++)
                    {

                        if (4_000 < time) return;
                        music[time++] = true;
                    }

                    for (int j = 0; j < 5; j++)
                    {

                        time++;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                l = int.Parse(temp[1]);
                d = int.Parse(temp[2]);
            }
        }
    }
}
