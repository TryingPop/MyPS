using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 10
이름 : 배성훈
내용 : 물병
    문제번호 : 1052번

    그리디, 비트마스킹 문제다.
    k개로 만드는 줄 알아 조건을 잘못해석해 한 번 틀렸다.
    k개 이하로 만드는게 목표이다.

    먼저 k = 0이하면 불가능하다.
    k 가 n보다 큰 경우는 자동으로 k이하이므로 끝난다.
    그리고 같은 물의 양만 합칠 수 있고 초기에 1이므로
    그래서 물의 양은 2^i 형태이다.
    이에 물을 만들어가면 비트마스킹에 1의 갯수만큼 줄일 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1325
    {

        static void Main1325(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int ret = 0;
            int min = Cnt(input[0]);
                
            while (input[1] < min)
            {

                for (int i = 0; i < 25; i++)
                {

                    if (((1 << i) & input[0]) == 0) continue;
                    input[0] += 1 << i;
                    ret += 1 << i;
                    break;
                }

                min = Cnt(input[0]);
            }

            Console.Write(ret);

            int Cnt(int _num)
            {

                int ret = 0;
                // 2^25 > 10_000_000
                for (int i = 0; i < 25; i++)
                {

                    if (((1 << i) & _num) == 0) continue;
                    ret++;
                }

                return ret;
            }
        }
    }
}
