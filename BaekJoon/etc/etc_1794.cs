using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 29
이름 : 배성훈
내용 : 케이크 자르기
    문제번호 : 17179번

    이분 탐색, 매개 변수 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1794
    {

        static void Main1794(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();
            int l = ReadInt();

            int[] arr = new int[m + 2];
            for (int i = 1; i <= m; i++)
            {

                // 조각의 위치
                arr[i] = ReadInt();
            }

            arr[m + 1] = l;

            for (int i = 0; i < n; i++)
            {

                int q = ReadInt();

                // 자르는 조각의 개수
                int ret = BinarySearch(q + 1);

                sw.Write($"{ret}\n");
            }

            int BinarySearch(int _chk)
            {

                // 자르는 개수 확인
                int l = 0;
                int r = 4_000_000;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    // 길이 mid 일 때 조각 수가 _chk개 이상?
                    if (ChkValid(mid, _chk)) l = mid + 1;
                    // 미만 이면 r값 줄이기
                    else r = mid - 1;
                }

                // 이상인 가장 작은값이 된다.
                return l - 1;
            }

            // 길이가 _len 이상인 조각의 수가 _chk개 이상인지 판별하는 함수
            // Greedy 알고리즘으로 판별한다.
            bool ChkValid(int _len, int _chk)
            {

                int cnt = 0;
                int left = 0;
                int right = 0;

                while (right < arr.Length)
                {

                    int len = arr[right] - arr[left];
                    if (len >= _len)
                    {

                        cnt++;
                        left = right;
                    }

                    right++;
                }

                return _chk <= cnt;
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
                return ret;
            }

        }
    }
}
