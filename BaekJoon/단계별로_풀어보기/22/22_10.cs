using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 창문 닫기
    문제번호 : 13909번
*/
namespace BaekJoon._22
{
    internal class _22_10
    {

        static void Main10(string[] args)
        {

            // 해당 위치의 값에 대해 약수의 개수가 짝수이면 0이고, 약수의 개수가 홀수이면 1이다
            // 순서공리와 체공리(*은 잘 정의되어져 있다)에 의해 제곱수면 약수의 개수가 홀수이고,
            // 아니면 약수의 개수가 짝수이다

            int input = int.Parse(Console.ReadLine());

            // 제곱수 찾기 1 = 1 * 1 이므로 카운트 하고 들어간다
            int result = 1;

            // 제곱수 찾기
            for (int i = 2; i * i <= input; i++)
            {

                result++;
            }

            // 위는 이를 풀어쓴 것이다
            // result = (int)Math.Sqrt(input);

            Console.WriteLine(result);
        }
    }
}
