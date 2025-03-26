using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 25
이름 : 배성훈
내용 : 킥다운
    문제번호 : 1195번

    구현, 브루트포스 문제다.
    왼쪽에 붙이는 부분을 캐치 못해 1번 틀렸다.
    왼쪽, 오른쪽에 이어붙여 만족하는 가장 짧은 길이를 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1469
    {

        static void Main1469(string[] args)
        {

            bool[] up, down;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = down.Length + up.Length;

                for (int i = 0; i < down.Length; i++)
                {

                    bool flag = false;
                    for (int j = 0; j < up.Length && i + j < down.Length; j++)
                    {

                        if (up[j] || down[i + j]) continue;
                        flag = true;
                        break;
                    }

                    if (flag) continue;
                    ret = Math.Max(i + up.Length, down.Length);
                    break;
                }

                for (int i = 1; i < up.Length; i++)
                {

                    bool flag = false;

                    for (int j = 0; i + j < up.Length; j++)
                    {

                        if (up[i + j] || down[j]) continue;
                        flag = true;
                        break;
                    }

                    if (flag) continue;
                    ret = Math.Min(ret, i + down.Length);
                    break;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string temp1 = Console.ReadLine();
                string temp2 = Console.ReadLine();

                if (temp2.Length < temp1.Length)
                {

                    string temp = temp1;
                    temp1 = temp2;
                    temp2 = temp;
                }

                up = new bool[temp1.Length];
                down = new bool[temp2.Length];

                for (int i = 0; i < temp1.Length; i++)
                {

                    up[i] = temp1[i] == '1';
                }

                for (int i = 0; i < temp2.Length; i++)
                {

                    down[i] = temp2[i] == '1';
                }
            }
        }
    }
}
