using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 23
이름 : 배성훈
내용 : 책정리
    문제번호 : 1818번

    LIS 문제다.
    길이 갱신을 잘못해 한 번 틀렸다.

    책을 최소로 옮기는 건 
    고정시킬 책의 최댓값을 찾는 것과 같다.
    그러면 고정시킬 책의 최댓값은 LIS와 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1359
    {

        static void Main1359(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            int[] lis = new int[n];
            int len = 0;

            int max = 0;
            for (int i = 0; i < n; i++)
            {

                len = BinarySearch(arr[i]);
                lis[len++] = arr[i];
                max = Math.Max(max, len);
            }

            Console.Write(n - max);

            int BinarySearch(int _num)
            {

                int l = 0;
                int r = max - 1;
                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (lis[mid] < _num) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }

            int ReadInt()
            {

                int c, ret = 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
