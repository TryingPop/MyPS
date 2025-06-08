using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 8
이름 : 배성훈
내용 : 짝수? 홀수?
    문제번호 : 11815번

    수학, 이분탐색 문제다.
    제곱수인 경우 약수의 갯수는 홀수다.
    반면 제곱수가 아니면 약수의 갯수는 짝수다.

    해당 사실을 이용하여 이분 탐색으로 제곱수인지 판별하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1688
    {

        static void Main1688(string[] args)
        {

            string O = "1 ";
            string E = "0 ";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            long[] arr = Array.ConvertAll(sr.ReadLine().Split(), long.Parse);
            for (int i = 0; i < n; i++)
            {

                if (Chk(arr[i])) sw.Write(O);
                else sw.Write(E);
            }

            bool Chk(long _val)
            {

                long chk = BinarySearch(_val);
                return chk * chk == _val;
            }

            long BinarySearch(long _val)
            {

                long l = 1;
                long r = 1_000_000_000;

                while (l <= r)
                {

                    long mid = (l + r) >> 1;
                    if (mid * mid <= _val) l = mid + 1;
                    else r = mid - 1;
                }

                return l - 1;
            }
        }
    }
}
