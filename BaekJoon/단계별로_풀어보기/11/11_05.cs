using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 대지
    문제번호 : 9063
*/

namespace BaekJoon._11
{
    internal class _11_05
    {

        static void Main5(string[] args)
        {

            int length;
            int[] temp;
            int[] min = new int[2]; 
            int[] max = new int[2];

            int result;

            length = int.Parse(Console.ReadLine());

            temp = Array.ConvertAll(Console.ReadLine().Split(' '), input => int.Parse(input));
            min = (int[])temp.Clone();
            max = (int[])temp.Clone();
            
            for (int i = 1; i < length; i++)
            {

                temp = Array.ConvertAll(Console.ReadLine().Split(' '), input => int.Parse(input));

                if (min[0] > temp[0])
                {

                    min[0] = temp[0];
                }
                else if (max[0] < temp[0])
                {

                    max[0] = temp[0];
                }

                if (min[1] > temp[1])
                {

                    min[1] = temp[1];
                }
                else if (max[1] < temp[1])
                {

                    max[1] = temp[1];
                }
            }

            result = (max[0] - min[0]) * (max[1] - min[1]);
            Console.WriteLine(result); 
        }
    }
}
