using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 1
이름 : 배성훈
내용 : Сложная задача
    문제번호 : 28893번

    누적합 문제다.
    0인 경우를 고려안해 한 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1924
    {

        static void Main1924(string[] args)
        {

            int n, m;
            int[] arr1, arr2;
            int[] cnt1, cnt2;
            int len1, len2;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                int len = Math.Min(len1, len2);
                int ret = 0;
                for (int i = 0; i <= len; i++)
                {

                    int cur = i + Math.Min(cnt1[i], cnt2[i]);
                    ret = Math.Max(ret, cur);
                }

                Console.Write(ret);
            }

            void SetArr()
            {

                /// ----------------------------------
                /// 1의 개수 세기

                int[] sum1 = new int[n + 2];
                for (int i = n; i >= 1; i--)
                {

                    sum1[i] = sum1[i + 1] + arr1[i];
                }

                int[] sum2 = new int[m + 2];
                for (int i = m; i >= 1; i--)
                {

                    sum2[i] = sum2[i + 1] + arr2[i];
                }

                /// ----------------------------------


                cnt1 = new int[n + 2];
                len1 = 0;
                cnt1[0] = sum1[1];
                for (int i = 1; i <= n; i++)
                {

                    if (arr1[i] == 1) continue;
                    cnt1[++len1] = sum1[i];
                }

                cnt2 = new int[m + 2];
                len2 = 0;
                cnt2[0] = sum2[1];
                for (int i = 1; i <= m; i++)
                {

                    if (arr2[i] == 1) continue;
                    cnt2[++len2] = sum2[i];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr1 = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr1[i] = ReadInt();
                }

                m = ReadInt();
                arr2 = new int[m + 1];
                for (int i = 1; i <= m; i++)
                {

                    arr2[i] = ReadInt();
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
