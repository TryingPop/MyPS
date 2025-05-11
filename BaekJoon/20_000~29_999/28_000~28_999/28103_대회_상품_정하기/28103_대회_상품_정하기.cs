using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 11
이름 : 배성훈
내용 : 대회 상품 정하기
    문제번호 : 28103번

    이분 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1625
    {

        static void Main1625(string[] args)
        {

            long n, x;
            int m;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int E = m - 1;
                for (int i = 0; i < m; i++)
                {

                    long cnt = BinarySearch(n, x, i);

                    sw.Write($"{cnt} ");

                    n -= cnt;
                    x -= cnt * arr[i];
                }

                long BinarySearch(long _n, long _x, int _idx)
                {

                    long l = 0;
                    long r = _n;

                    while (l <= r)
                    {

                        long mid = (l + r) >> 1;

                        long price = mid * arr[_idx] + (_n - mid) * arr[E];
                        if (price <= _x) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l - 1;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadLong();
                m = ReadInt();
                x = ReadLong();

                arr = new int[m];
                for (int i = 0; i < m; i++)
                {

                    arr[i] = ReadInt();
                }

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
