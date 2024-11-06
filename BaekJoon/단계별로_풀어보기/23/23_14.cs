using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 과제 안 내신분..?
    문제번호 : 5597번
*/

namespace BaekJoon._23
{
    internal class _23_14
    {

        static void Main14(string[] args)
        {

            const int total = 30;
            const int num = 28;
#if false


            HashSet<string> nums = new HashSet<string>(30);

            // 출석부
            for (int i = 1; i <= total; i++)
            {

                nums.Add(i.ToString());
            }

            // 과제 제출자 제외
            for (int i = 0; i < num; i++)
            {

                nums.Remove(Console.ReadLine());
            }

            // 남은 인원 출력
            foreach (string s in nums.OrderBy(item => int.Parse(item)))
            {

                Console.WriteLine(s);
            }
#else

            // 0번은 유령
            bool[] nums = new bool[total + 1];

            // 제출 인원 확인
            for (int i = 0; i < num; i++)
            {

                nums[int.Parse(Console.ReadLine())] = true;
            }

            // 미제출자 확인
            for (int i = 1; i <= total; i++)
            {

                if (!nums[i])
                {

                    Console.WriteLine(i);
                }
            }
#endif
        }
    }
}
