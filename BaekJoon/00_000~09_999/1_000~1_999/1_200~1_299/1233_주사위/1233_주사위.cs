using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 주사위
    문제번호 : 1233번

    브루트포스 문제다.
    브루트포스로 접근해도 20 x 20 x 40 = 1600번 밖에 안된다.
    합의 갯수를 세어 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1477
    {

        static void Main1477(string[] args)
        {

            int[] cnt = new int[81];
            int[] area = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            
            for (int i = 1; i <= area[0]; i++)
            {

                for (int j = 1; j <= area[1]; j++)
                {

                    for (int k = 1; k <= area[2]; k++)
                    {

                        // 합의 갯수 세기
                        cnt[i + j + k]++;
                    }
                }
            }

            int max = 0;
            for (int i = 3; i <= 80; i++)
            {

                if (max < cnt[i]) max = cnt[i];
            }

            for (int i = 3; i <= 80; i++)
            {

                if (max != cnt[i]) continue;
                Console.Write(i);
                break;
            }
        }
    }
}
