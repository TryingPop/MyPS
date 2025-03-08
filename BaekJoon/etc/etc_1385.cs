using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 8
이름 : 배성훈
내용 : 가장 긴 감소하는 부분 수열
    문제번호 : 11722번

    lis 문제다.
    가장 긴 감소하는 부분 수열은 뒤집어서 보면 가장 긴 증가하는 부분 수열과 같다.
    그래서 뒤집고 lis 를 찾았다.
    다만 이분 탐색에서 비교를 잘못해 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1385
    {

        static void Main1385(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void Reverse()
            {

                int i = 0;
                int j = n - 1;
                for (; i < j; i++, j--)
                {

                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            void GetRet()
            {

                Reverse();

                int[] lis = new int[n];
                int len = 0;

                for (int i = 0; i < n; i++)
                {

                    int idx = BinarySearch(arr[i]);
                    lis[idx++] = arr[i];
                    len = Math.Max(len, idx);
                }

                Console.Write(len);

                int BinarySearch(int _num)
                {

                    int l = 0, r = len - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (lis[mid] < _num) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            }
        }
    }
}
