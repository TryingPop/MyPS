using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 22
이름 : 배성훈
내용 : 안테나
    문제번호 : 18310번

    수학 문제다.
    최솟값인 지점을 찾아야 한다.
    이는 극점을 찾는 것과 같고 절댓값 1차함수들의 합이므로
    중앙값에서 최소를 가짐을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1947
    {

        static void Main1947(string[] args)
        {

            int n;
            int[] arr;

            Input();

            Console.Write(arr[(n - 1)>> 1]);

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());
                arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                Array.Sort(arr);
            }
        }
    }
}
