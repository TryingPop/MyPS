using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 개수 세기
    문제번호 : 10807번
*/

namespace BaekJoon._23
{
    internal class _23_11
    {

        static void Main11(string[] args)
        {

            // 입력받는 범위 -100 ~ 100
            const int min = -100;
            const int max = 100;
            int[] nums = new int[max + 1 - min];

            // 문자열의 길이 여기서는 안써서 생략
            Console.ReadLine();

            // Select나 Foreach는 안되는거 같다?
            // 입력 받은 문자열에 대해 입력된 횟수 저장
            foreach(string s in Console.ReadLine().Split(' '))
            {

                nums[int.Parse(s) + 100] += 1;
            }

            // 입력된 값의 출력
            Console.WriteLine(nums[int.Parse(Console.ReadLine()) +100]);
        }
    }
}
