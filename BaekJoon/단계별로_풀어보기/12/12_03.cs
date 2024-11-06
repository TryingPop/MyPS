using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 8
이름 : 배성훈
내용 : 붙임성 좋은 총총이
    문제번호 : 26069번
*/

namespace BaekJoon._12
{
    internal class _12_03
    {

        static void Main3(string[] args)
        {

            int length = int.Parse(Console.ReadLine());
            HashSet<string> dancers = new HashSet<string>();
            bool find = false;
            int result;

            for (int i = 0; i < length; i++)
            {

                string[] inputs = Console.ReadLine().Split(' ');

                if (!find)
                {

                    if (inputs.Contains("ChongChong"))
                    {

                        find = true;
                        dancers.Add(inputs[0]);
                        dancers.Add(inputs[1]);
                    }
                }
                else
                {

                    if (dancers.Contains(inputs[0]))
                    {

                        dancers.Add(inputs[1]);
                    }
                    else if (dancers.Contains(inputs[1]))
                    {

                        dancers.Add(inputs[0]);
                    }
                }
            }

            result = dancers.Count;
            Console.WriteLine(result);
        }
    }
}
