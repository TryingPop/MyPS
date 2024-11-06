using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.22
 * 내용 : 백준 10단계 1번 문제
 * 
 * 블랙잭
 */

namespace BaekJoon._10
{
    internal class _10_01
    {
        static void Main1(string[] args)
        {
            string[] strinputs1 = Console.ReadLine().Split(" ");
            int[] inputs1 = Array.ConvertAll(strinputs1, int.Parse);

            int len = inputs1[0];
            int max = inputs1[1];

            string[] strinputs2 = Console.ReadLine().Split(" ");
            int[] inputs = Array.ConvertAll(strinputs2, int.Parse);

            int result = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = i+1; j <len; j++)
                {
                    for (int k = j+1; k < len; k++)
                    {
                        int chk = inputs[i] + inputs[j] + inputs[k];
                        if (chk > result && chk <= max)
                        {
                            result = chk;
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
