using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 쉽게 푸는 문제
    문제번호 : 1292번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1433
    {

        static void Main1433(string[] args)
        {

            int A, B;
            int[] arr;

            SetArr();

            GetRet();

            void GetRet()
            {

                string[] temp = Console.ReadLine().Split();
                A = int.Parse(temp[0]);
                B = int.Parse(temp[1]);

                for (int i = 1; i <= 1_000; i++)
                {

                    arr[i] += arr[i - 1];
                }

                Console.Write(arr[B] - arr[A - 1]);
            }

            void SetArr()
            {

                arr = new int[1_001];
                int idx = 1;
                for (int i = 1; true; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        arr[idx++] = i;
                        if (1_000 < idx) return;
                    }
                }
            }
        }
    }
}
