using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 15
이름 : 배성훈
내용 : 순열 정렬
    문제번호 : 25287번

    그리디 문제다.
    i번째 수가 가장 작으면서 가장 긴 감소하지 않는 수열로 만들자
    그러면 i = n일 때 이는 가장 작은 가장 긴 감소하지 않는 수열임이 그리디로 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1885
    {

        static void Main1885(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            int[] arr = new int[50_000];
            while (t-- > 0)
            {

                int n = ReadInt();
                bool ret = true;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    int change = n - cur + 1;
                    int min = Math.Min(cur, change);
                    int max = Math.Max(cur, change);

                    if (i == 0) arr[0] = min;
                    else
                    {

                        if (arr[i - 1] <= min) arr[i] = min;
                        else if (arr[i - 1] <= max) arr[i] = max;
                        else
                        {

                            ret = false;
                            arr[i] = max;
                        }
                    }
                }

                sw.Write(ret ? Y : N);
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
