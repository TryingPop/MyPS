using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.22
 * 내용 : 백준 10단계 5번 문제
 * 
 * 체스판 다시 칠하기
 */

namespace BaekJoon._10
{
    internal class _10_04
    {
        static void Main4(string[] args)
        {

            string[] strinputs1 = Console.ReadLine().Split(" ");

            int row = int.Parse(strinputs1[0]);
            int col = int.Parse(strinputs1[1]);
            int chk = 64;
            int result = chk;

            string[] inputarray = new string[row];

            for (int i = 0; i < row; i++)
            {
                inputarray[i] = Console.ReadLine();
                inputarray[i] = inputarray[i].Replace('B', '0');
                inputarray[i] = inputarray[i].Replace('W', '1');
            }


            for (int i = 0; i < row - 7; i++)
            {
                for (int j = 0; j < col - 7; j++)
                {
                    chk = 0;
                    for (int n = 0; n < 8; n++)
                    {
                        for (int m = 0; m < 8; m++)
                        {
                            chk += Math.Abs(inputarray[i + n][j + m] - char.Parse(((m + n) % 2).ToString()));
                        }
                    }
                    chk = Math.Min(chk, 64 - chk);

                    if (chk <= result)
                    {
                        result = chk;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}
