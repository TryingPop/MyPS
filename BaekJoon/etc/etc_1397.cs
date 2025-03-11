using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 11
이름 : 배성훈
내용 : 보석 상자
    문제번호 : 2792번

    이분 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1397
    {

        static void Main1397(string[] args)
        {

            int n, k;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int l = 1;
                int r = 1_000_000_000;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (n < Chk(mid)) l = mid + 1;
                    else r = mid - 1;
                }

                Console.Write(r + 1);

                long Chk(int _val)
                {

                    long ret = 0;

                    for (int i = 0; i < k; i++)
                    {

                        int add = arr[i] / _val;
                        if (arr[i] % _val != 0) add++;
                        ret += add;
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                arr = new int[k];
                for (int i = 0; i < k; i++)
                {

                    arr[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
internal class Program
{
    private static void Main(string[] args)
    {
        int[] tokens = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);

        int n = tokens[0];
        int m = tokens[1];
        
        int[] marbleCountArray = new int[m];
        for (int i = 0; i < m; ++i)
        {
            marbleCountArray[i] = int.Parse(Console.ReadLine()!);
        }

        int low = 1 - 1;
        int high = marbleCountArray.Max() + 1;
        while (low + 1 < high)
        {
            int mid = (low + high) / 2;

            int bundles = 0;
            for (int i = 0; i < marbleCountArray.Length; ++i)
            {
                bundles += ((marbleCountArray[i] - 1) / mid) + 1;
            }

            if (bundles > n)
            {
                low = mid;
            }
            else
            {
                high = mid;
            }
        }
        Console.Write(high);
    }
}
#endif
}
