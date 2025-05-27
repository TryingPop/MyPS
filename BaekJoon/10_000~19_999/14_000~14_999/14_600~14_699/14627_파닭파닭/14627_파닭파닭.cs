using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 26
이름 : 배성훈
내용 : 파닭파닭
    문제번호 : 14627번

    매개 변수 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1645
    {

        static void Main1645(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int c = ReadInt();
            int[] arr = new int[n];
            long sum = 0;
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
                sum += arr[i];
            }

            int pa = BinarySearch();

            long ret = sum - 1L * pa * c;

            Console.Write(ret);

            int BinarySearch()
            {

                int l = 1;
                int r = 1_000_000_000;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (GetCnt(mid) >= c) l = mid + 1;
                    else r = mid - 1;
                }

                return l - 1;

                long GetCnt(int _val)
                {

                    long ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        ret += arr[i] / _val;
                    }

                    return ret;
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
