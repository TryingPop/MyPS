using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 9번 문제
 * 
 * 크로아티아 알파벳
 */

namespace BaekJoon._06
{
    internal class _06_09
    {
        static void Main9(string[] args)
        {
            string inputs = Console.ReadLine();
            int result = inputs.Length;

            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == '-')
                {
                    result--;
                }
                else if (inputs[i] == 'j')
                {
                    if (i >= 1)
                    {
                        if (inputs[i - 1] == 'n' || inputs[i - 1] == 'l')
                        {
                            result--;
                        }
                    }
                }
                else if (inputs[i] == '=')
                {
                    result--;

                    if (inputs[i - 1] == 'z')
                    {
                        if (i >= 2)
                        {
                            if (inputs[i - 2] == 'd')
                            {
                                result--;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
