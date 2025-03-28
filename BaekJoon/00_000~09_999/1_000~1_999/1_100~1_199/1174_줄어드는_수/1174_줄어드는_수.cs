using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 줄어드는 수
    문제번호 : 1174번

    브루트포스, 백트래킹 문제다.
    for문방향으로 수를 만들어 풀었다.
    힌트를 보니, 백트래킹 방법이 더 유효해보인다.
*/

namespace BaekJoon.etc
{
    internal class etc_1473
    {

        static void Main1473(string[] args)
        {

            long[] arr = new long[1023];

            for (int i = 1; i < 10; i++)
            {

                arr[i] = i;
            }

            int s = 0, e = 9;
            int len = 10;
            long digit = 1;
            
            while (true)
            {

                long chk = digit;
                digit *= 10;

                bool flag = true;
                for (int i = s; i <= e; i++)
                {

                    long front = arr[i] / chk;
                    for (long j = front + 1; j < 10; j++)
                    {

                        arr[len++] = arr[i] + digit * j;
                        flag = false;
                    }
                }

                if (flag) break;
                s = e + 1;
                e = len - 1;
            }

            Array.Sort(arr, 0, len);
            int n = int.Parse(Console.ReadLine()) - 1;
            if (n < arr.Length) Console.Write(arr[n]);
            else Console.Write(-1);
        }
    }
}
