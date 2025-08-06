using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 6
이름 : 배성훈
내용 : 게임의 신
    문제번호 : 33902번

    수학, dp, 유클리드 호제법, 게임 이론 문제다.
    X, Y의 범위가 1000으로 작아 모두 조사할만하다.
    그래서 서로소인 경우를 모두 조사하고 dp를 이용해 최적의 이기는 경로를 찾아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1809
    {

        static void Main1809(string[] args)
        {

            string Y = "khj20006";
            string N = "putdata";

            int x, y;
            bool[][] disjoint;
            int[] win;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                win = new int[y + 1];
                win[y] = -1;
                for (int i = y - 1; i >= x; i--)
                {

                    for (int j = i + 1; j <= y; j++)
                    {

                        if (disjoint[i][j] && win[j] == -1)
                        {

                            win[i] = 1;
                            break;
                        }
                    }

                    if (win[i] == 0) win[i] = -1;
                }

                Console.Write(win[x] == 1 ? Y : N);
            }

            void SetArr()
            {

                // 서로 소인 경우 찾기
                for (int i = x; i <= y; i++)
                {

                    for (int j = i + 1; j <= y; j++)
                    {

                        int gcd = GetGCD(i, j);
                        if (gcd == 1) disjoint[i][j] = true;
                    }
                }
            }

            int GetGCD(int _a, int _b)
            {

                while (_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                x = int.Parse(temp[0]);
                y = int.Parse(temp[1]);

                disjoint = new bool[y + 1][];
                for (int i = 0; i <= y; i++)
                {

                    disjoint[i] = new bool[y + 1];
                }
            }
        }
    }
}
