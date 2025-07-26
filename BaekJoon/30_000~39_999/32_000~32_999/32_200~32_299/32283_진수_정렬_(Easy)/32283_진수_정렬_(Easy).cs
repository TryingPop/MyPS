using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 21
이름 : 배성훈
내용 : 진수 정렬 (Easy)
    문제번호 : 32283번

    브루트포스, 정렬 문제다.
    조건대로 정렬해서 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1722
    {

        static void Main1722(string[] args)
        {

            int n, s;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                arr = new int[1 << n];
                for (int i = 1; i < arr.Length; i++)
                {

                    arr[i] = i;
                }

                Array.Sort(arr, (x, y) =>
                {

                    int cnt1 = 0;
                    int cnt2 = 0;

                    for (int i = 0, j = 1; i < n; i++, j <<= 1)
                    {

                        if ((x & j) != 0) cnt1++;
                        if ((y & j) != 0) cnt2++;
                    }

                    int ret = cnt1.CompareTo(cnt2);
                    if (ret == 0) ret = x.CompareTo(y);
                    return ret;
                });

                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i] == s)
                    {

                        Console.Write(i);
                        return;
                    }
                }
            }

            void Input()
            {

                n = int.Parse(Console.ReadLine());
                string temp = Console.ReadLine();
                s = 0;
                for (int i = 0; i < n; i++)
                {

                    if (temp[i] == '0') continue;
                    s |= 1 << i;
                }
            }
        }
    }
}
