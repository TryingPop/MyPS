using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 민균이의 계략
    문제번호 : 11568번

    가장 긴 증가하는 부분수열 문제다
    범위가 1000이라 n^2 방법도 이용가능하나 이분 탐색으로 n log n으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0620
    {

        static void Main620(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    int l = 0;
                    int r = ret - 1;
                    while(l <= r)
                    {

                        int mid = (l + r) / 2;

                        if (arr[mid] < cur) l = mid + 1;
                        else r = mid - 1;
                    }

                    r++;
                    arr[r] = cur;
                    r++;
                    ret = r < ret ? ret : r;
                }
                sr.Close();

                Console.WriteLine(ret);
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

#if other
using System;
class MainApp
{
    static int[] cache;
    static int binarySearch(int left, int right, int target)
    {
        int mid;
        while (left < right)
        {
            mid = (left + right) / 2;
            if (cache[mid] < target) left = mid + 1;
            else right = mid;
        }
        return right;
    }
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        int[] array = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        cache = new int[N];
        int i = 1;
        int j = 0;
        cache[0] = array[0];
        while (i < N)
        {
            if (cache[j] < array[i])
            {
                j++;
                cache[j] = array[i];
            }
            else
            {
                int idx = binarySearch(0, j, array[i]);
                cache[idx] = array[i];
            }
            i++;
        }
        Console.WriteLine(j + 1);
    }
}
#endif
}
