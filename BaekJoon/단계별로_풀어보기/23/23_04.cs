using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 중앙 이동 알고리즘
    문제번호 : 2903번
*/

namespace BaekJoon._23
{
    internal class _23_04
    {

        static void Main4(string[] args)
        {

            int num = int.Parse(Console.ReadLine());
#if true
            int[] sols = new int[15] { 9, 25, 81, 289, 1089, 
                4225, 16641, 66049, 263169, 1050625, 
                4198401, 16785409, 67125249, 268468225, 1073807361 };
            int result = sols[num - 1];
            Console.WriteLine(result);

#else
            int result = 0;

            {
                
                // 문제를 풀면 계차가 2^(i-1)씩 추가된다
                int n = 2;
                for (int i = 0; i < num; i++)
                {

                    n += (int)Math.Pow(2, i);
                }

                result = n * n;
            }

            Console.WriteLine(result);
#endif
        }
    }
}
