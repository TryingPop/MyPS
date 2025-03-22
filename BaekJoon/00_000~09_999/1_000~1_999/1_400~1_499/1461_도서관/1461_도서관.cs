using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 21
이름 : 배성훈
내용 : 도서관
    문제번호 : 1461번

    그리디, 정렬 문제다.
    이동하는 방법을 보면 양수만 들고 갔을 때,
    돌아오는 것을 고려하면 최단 거리는 가장 큰것의 두배가 된다.

    그리고 양수는 양수끼리, 음수는 음수끼리 묶어 가는게 좋다.
    이에 가장 큰 것은 돌아올 필요가 없게하면 최단 이동이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1353
    {

        static void Main1353(string[] args)
        {

            int n, m;
            int[] plus, minus;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                int max = 0;

                for (int i = 0; i < plus.Length; i += m)
                {

                    max = Math.Max(max, plus[i]);
                    ret += plus[i] * 2;
                }

                for (int i = 0; i < minus.Length; i += m)
                {

                    max = Math.Max(max, minus[i]);
                    ret += minus[i] * 2;
                }


                Console.Write(ret - max);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                n = input[0];
                m = input[1];
                int[] arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

                Array.Sort(arr);

                int chk = n;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] < 0) continue;
                    chk = i;
                    break;
                }

                minus = new int[chk];
                for (int i = 0; i < chk; i++)
                {

                    minus[i] = -arr[i];
                }

                plus = new int[n - chk];
                for (int i = chk; i < n; i++)
                {

                    plus[i - chk] = arr[i];
                }

                Array.Reverse(plus);
            }
        }
    }
}
