using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 14
이름 : 배성훈
내용 : 알고리즘 수업 - 알고리즘의 수행 시간 1
    문제번호 : 24262번
*/

namespace BaekJoon._16
{
    internal class _16_01
    {

        static void Main1(string[] args)
        {

            Console.WriteLine(1);       // 코드는 i = n / 2만 실행하므로 1회이다
            Console.WriteLine(0);       // 어떤 수를 넣어도 1회만 실행하기에 시간복잡도는 상수이다
                                        // 그래서 상수의 다항식 차수는 0
        }


        public static T MenOfPassion<T>(T[] array, int n)
        {

            int i = n / 2;
            return array[i];
        }
    }
}
