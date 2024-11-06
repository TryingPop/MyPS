using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 7번 문제
 * 
 * 상수
 */

namespace BaekJoon._06
{
    internal class _06_07
    {
        static void Main7(string[] args)
        {
            string[] strinputs = Console.ReadLine().Split(" ");
            StringBuilder sb = new StringBuilder();
            string bf;

            for (int i = 0; i < strinputs.Length; i++)
            {
                sb = new StringBuilder();
                bf = strinputs[i];
                for (int j = bf.Length - 1; j >= 0; j--)
                {
                    sb.Append(bf[j].ToString());
                }
                strinputs[i] = sb.ToString();
            }
            int[] calcs = Array.ConvertAll(strinputs, int.Parse);
            Console.WriteLine(calcs.Max());
        }
    }
}
