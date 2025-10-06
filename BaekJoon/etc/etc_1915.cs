using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 24
이름 : 배성훈
내용 : Code Guessing
    문제번호 : 24586번

    브루트포스, 많은 조건 분기 문제다.
    찾을 수 있는 상황을 나눠서 정답을 제출했다.

    먼저 B의 위치를 모두 조사하면
    BBAA, BABA, BAAB, ABBA, ABAB, AABB 이다.
    
    이제 각 경우 자리가 유일한 경우를 찾아봤다.
    BBAA의 경우 A중 작은 값이 3이 있어야지 유일하게 결정된다.
    다음 BABA의 경우 A가 2, 4인 경우만 유일하게 결정된다.
    BAAB의 경우 A가 2, 8인 경우만 유일하다.

    다음으로 ABBA인 경우 A의 두값 차이가 3인 경우만 유일하다.
    ABAB인 경우 6 8인 경우만 유일하게 결정된다.
    AABB인 경우 A의 큰 값이 7인 경우 유일하게 결정된다.
    
    이외 경우는 유일하지 않으므로 모두 -1이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1915
    {

        static void Main1915(string[] args)
        {

            int a1, a2;
            string str;

            Input();

            GetRet();

            void GetRet()
            {

                int p1 = a1 - 1, p2 = a2 - a1 - 1, p3 = 9 - a2;
                if (str[0] == 'B' && str[1] == 'B')
                {

                    if (p1 == 2)
                        Console.Write("1 2");
                    else
                        Console.Write(-1);
                }
                else if (str[0] == 'B')
                {

                    if (p1 == 1)
                    {

                        if (str[2] == 'B')
                        {

                            if (p2 == 1)
                                Console.Write($"1 3");
                            else
                                Console.Write(-1);
                        }
                        else
                        {

                            if (p3 == 1)
                                Console.Write("1 9");
                            else
                                Console.Write(-1);
                        }
                    }
                    else
                        Console.Write(-1);
                }
                else if (str[1] == 'B')
                {

                    if (str[2] == 'B')
                    {

                        if (p2 == 2)
                            Console.Write($"{a1 + 1} {a1 + 2}");
                        else
                            Console.Write(-1);
                    }
                    else
                    {

                        if (p2 == 1 && p3 == 1)
                            Console.Write($"{a1 + 1} 9");
                        else
                            Console.Write(-1);
                    }
                }
                else
                {

                    if (p3 == 2)
                        Console.Write("8 9");
                    else
                        Console.Write(-1);
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                int f = int.Parse(temp[0]);
                int t = int.Parse(temp[1]);

                a1 = Math.Min(f, t);
                a2 = Math.Max(f, t);

                str = Console.ReadLine();
            }
        }
    }
}
