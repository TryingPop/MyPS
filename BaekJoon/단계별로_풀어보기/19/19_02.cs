using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 17
이름 : 배성훈
내용 : 제로
    문제번호 : 10773번
*/

namespace BaekJoon._19
{
    internal class _19_02
    {

        static void Main2(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            Stack<int> stk = new Stack<int>();
            int sum = 0;
            for (int i = 0; i < len; i++)
            {

                int input = int.Parse(Console.ReadLine());

                if (input == 0)
                {

                    sum -= stk.Pop();
                }
                else
                {
                    sum += input;
                    stk.Push(input);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
