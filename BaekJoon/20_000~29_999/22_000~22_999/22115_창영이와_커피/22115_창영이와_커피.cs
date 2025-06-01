using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 1
이름 : 배성훈
내용 : 창영이와 커피
    문제번호 : 22115번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1665
    {

        static void Main1665(string[] args)
        {

            int n, k;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] cnt = new int[k + 1];
                Array.Fill(cnt, -1);
                Array.Sort(arr);
                cnt[0] = 0;
                int max = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = Math.Min(max, k - arr[i]); j >= 0; j--)
                    {

                        if (cnt[j] == -1) continue;
                        int next = j + arr[i];
                        if (next > k) continue;
                        if(cnt[next] == -1 || cnt[j] + 1 < cnt[next]) cnt[next] = cnt[j] + 1;
                    }

                    max += arr[i];
                }

                Console.Write(cnt[k]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();
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
