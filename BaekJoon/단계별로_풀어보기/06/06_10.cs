using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 10번 문제
 * 
 * 그룹 단어 체커
 */

namespace BaekJoon._06
{
    internal class _06_10
    {
        static void Main10(string[] args)
        {
            int num = 0;
            int length = int.Parse(Console.ReadLine());
            int result = length;
            int idx = 0;

            for (int i = 0; i < length; i++)
            {
                string str = Console.ReadLine();
                int[] chkarr = new int[26];
                idx = 0;

                for (int j = 0; j < str.Length; j++)
                {
                    num = (int)(str[j] - 'a');
                    chkarr[num] += 1;
                }

                while (chkarr.Max() > 1)
                {
                    idx = Array.IndexOf(chkarr, chkarr.Max());

                    if (chkarr[idx] != str.LastIndexOf((char)(idx + 'a')) - str.IndexOf((char)(idx + 'a')) + 1)
                    {
                        result--;
                        break;
                    }
                    else
                    {
                        chkarr[idx] = 0;
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
