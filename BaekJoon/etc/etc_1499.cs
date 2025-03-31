using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 노래 악보
    구현 문제다.

    누적합과 이분탐색을 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1499
    {

        static void Main1499(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int n, m;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = 0; i < m; i++)
                {

                    int time = int.Parse(sr.ReadLine());

                    sw.Write(BinarySearch(time));
                    sw.Write('\n');
                }

                int BinarySearch(int _val)
                {

                    int l = 1;
                    int r = n;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        // _val 초 이상이되면 다음 l은 다음 인덱스를 가리킨다.
                        if (arr[mid] <= _val) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
            }

            void Input()
            {

                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = int.Parse(sr.ReadLine());
                }

                for (int i = 2; i <= n; i++)
                {

                    arr[i] += arr[i - 1];
                }
            }
        }
    }
}
