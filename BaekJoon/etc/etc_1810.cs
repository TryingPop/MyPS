using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 6
이름 : 배성훈
내용 : 로마의 휴일
    문제번호 : 33678번

    매개 변수 탐색, 누적합 문제다.
    우선 i일이 쉬는게 가능하면 i - 1일 쉬는게 가능함은 자명하다.
    그래서 쉬는날에 대해 매개 변수 탐색으로 접근했다.

    이후 가장 큰 돈을 버는 것은 연속적으로 i일을 강제로 쉬어야 하기에
    누적합을 이용하면 O(N)에 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1810
    {

        static void Main1810(string[] args)
        {

            int n, k, x;
            int[] arr;
            long[] sum;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                // BinarySearch!
                int ret = BinarySearch();
                Console.Write(ret == 0 ? -1 : ret);

                int BinarySearch()
                {

                    int l = 1;
                    int r = n;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        // 길이 가능하면 늘려보기!
                        if (Chk(mid)) l = mid + 1;
                        else r = mid - 1;
                    }

                    // 가능한 가장 큰 값 찾기
                    return l - 1;
                }

                bool Chk(int _len)
                {

                    int rS = 1;
                    int rE = _len;

                    long max = 0;
                    for (; rE <= n; rS++, rE++)
                    {

                        max = Math.Max(max, x * sum[rS - 1] + sum[n] - sum[rE]);
                    }

                    return max >= k;
                }
            }

            void SetArr()
            {

                sum = new long[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    sum[i] = sum[i - 1] + arr[i - 1];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();
                x = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
