using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 14
이름 : 배성훈
내용 : 부분배열 고르기
    문제번호 : 2104번

    스택 문제다.
    문제를 보면 히스토그램 문제와 같다.
    그래서 해당 방법처럼 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1186
    {

        static void Main1186(string[] args)
        {

            StreamReader sr;

            int[] arr;
            long[] sum;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] stk = new int[n + 2];

                int len = 0;
                long ret = 0;

                for (int i = 1; i < stk.Length; i++)
                {

                    while (len > 0 && arr[i] <= arr[stk[len - 1]]) 
                    { 
                        
                        len--;

                        // 가장 작은 값인 높이는 stk[len]
                        // 오른쪽은 i - 1
                        // 왼쪽은 stk[len - 1] + 1이 된다.
                        int height = stk[len];
                        int left = len == 0 ? 0 : stk[len - 1];
                        long cur = (sum[i - 1] - sum[left]) * arr[height];
                        ret = Math.Max(ret, cur);
                    }

                    stk[len++] = i;
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n + 2];
                sum = new long[n + 2];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    sum[i] = sum[i - 1] + arr[i];
                }

                sr.Close();
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
}
