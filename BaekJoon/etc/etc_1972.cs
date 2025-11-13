using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 13
이름 : 배성훈
내용 : 물 주기
    문제번호 : 23351번

    시뮬레이션 문제다.
    n의 크기가 작고 a x b < n이므로.
    2 x n일 안에 끝남을 알 수 있다.
    그래서 시뮬레이션으로 접근했다.

    그리고 연속한 a개에 물을 준다.
    글너데 a개에 최대한 꽃을 주는경우 
*/

namespace BaekJoon.etc
{
    internal class etc_1972
    {

        static void Main1972(string[] args)
        {

            int n, k, a, b;

            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Fill(arr, k);
                int ret = 0;
                bool flag = true;
                bool dir = true;
                int s = 0;

                while (true)
                {

                    ret++;

                    Add();
                    Next();

                    if (IsDead(ret)) break;
                }

                void Add()
                {

                    for (int i = 0; i < a; i++)
                    {

                        arr[s + i] += b;
                    }
                }

                void Next()
                {

                    s += dir ? a : -a;
                    if (s < 0)
                    {

                        dir = true;
                        s = 0;
                    }
                    else if (n <= s)
                    {

                        dir = false;
                        s = n - a;
                    }
                }

                Console.Write(ret);

                bool IsDead(int dead)
                {

                    for (int i = 0; i < n; i++)
                    {

                        if (arr[i] == dead) return true;
                    }

                    return false;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);
                a = int.Parse(temp[2]);
                b = int.Parse(temp[3]);

                arr = new int[n];
            }
        }
    }
}
