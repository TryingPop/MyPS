using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 10
이름 : 배성훈
내용 : 잃어버린 괄호
    문제번호 : 1541번
*/
namespace BaekJoon._13
{
    internal class _13_04
    {

        static void Main4(string[] args)
        {

            string[] inputs = Console.ReadLine().Split('-');        // a - b + c를
                                                                    // (+a) + (-b) + (+c)로 해석
                                                                    // 그래서 음수 뒤에 +를 최대한 합쳐서 계산한다

            int result = 0;

            for (int i = 0; i < inputs.Length; i++)
            {

                int[] nums = Array.ConvertAll(inputs[i].Split('+'), item => int.Parse(item));

                result += i == 0 ? nums.Sum() : -nums.Sum();
            }

            Console.WriteLine(result);
        }
    }
}
