using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 숏코딩의 왕 브실이
    문제번호 : 29726번

    누적 합, 그리디 문제다.
    누적 합을 응용해서 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1594
    {

        static void Main1594(string[] args)
        {
            
            int n, m;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int s = 0;
                int e = n - 1;

                int[] min = new int[n];
                int[] max = new int[n];

                min[0] = arr[0];
                for (int i = 1; i < n; i++)
                {

                    // 0 ~ i까지 중 최솟값
                    min[i] = Math.Min(arr[i], min[i - 1]);
                }

                max[n - 1] = arr[n - 1];

                for (int i = n - 2; i >= 0; i--)
                {

                    // n - 1 ~ i까지의 최댓값
                    max[i] = Math.Max(arr[i], max[i + 1]);
                }

                int ret = -1_234_567;

                for (int i = 0; i <= m; i++)
                {

                    // 끝값을 0개 제거, 1개 제거, ..., m개 제거한 경우 확인
                    // 그러면 시작값은 m - 끝값에서 제거한 것의 갯수가 된다.
                    int chk = max[n - 1 - i] - min[m - i];

                    ret = Math.Max(ret, chk);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

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
