using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

/*
날짜 : 2024. 7. 16 
이름 : 배성훈
내용 : 특공대
    문제번호 : 4008번

    dp, 볼록껍질을 이용한 최적화 문제다
    기존에는 기본적인 누적합이였는데
    여기서는 2차원 함수로 이루어진 누적합이다
    하지만 식을 풀면 치환으로 컨벡스 헐 트릭을 적용할 수 있다
    
    입력이 최대 100만개이므로 이분 탐색으로 찾는 O(N log N)은 지양했다
    대신 찾아보니 덱을 이용해 O(N)으로 줄일 수 있었다
    (여기서 덱은 큐와 스택의 기능을 합친 자료구조라 보면된다)

    코드는 해당 사이트를 찾아서 참고했다
    https://justicehui.github.io/apio/2018/08/20/BOJ4008/
*/

namespace BaekJoon._58
{
    internal class _58_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr;
            int n, a, b, c;
            long[] dp, sum;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Console.Write(dp[n]);
            }

            void GetRet()
            {

                dp = new long[n + 1];
                int[] dq = new int[n + 1];
                long[] x = new long[n + 1];
                int cnt = 0;
                int chk = 1;

                for (int i = 1; i <= n; i++)
                {

                    dp[i] = F(sum[i]);
                    if (cnt > 0)
                    {

                        while(chk < cnt && x[chk + 1] < sum[i])
                        {

                            chk++;
                        }

                        int j = dq[chk];
                        dp[i] = Math.Max(dp[i], dp[j] + F(sum[i] - sum[j]));

                        while(cnt > 1 && GetCross(dq[cnt], i)< x[cnt])
                        {

                            cnt--;
                        }

                        dq[++cnt] = i;
                        x[cnt] = GetCross(dq[cnt - 1], i);
                        if (cnt < chk) chk = cnt;
                    }
                    else
                    {

                        dq[++cnt] = i;
                        x[cnt] = -1_000_000_000;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                a = ReadInt();
                b = ReadInt();
                c = ReadInt();

                sum = new long[n + 1];
                for (int i = 0; i < n; i++)
                {

                    sum[i + 1] = sum[i] + ReadInt();
                }

                sr.Close();
            }

            long F(long _x)
            {

                return a * _x * _x + b * _x + c;
            }

            long GetCross(int _idx1, int _idx2)
            {

                long x1 = -2 * a * sum[_idx1];
                long y1 = a * sum[_idx1] * sum[_idx1] - b * sum[_idx1] + dp[_idx1];

                long x2 = -2 * a * sum[_idx2];
                long y2 = a * sum[_idx2] * sum[_idx2] - b * sum[_idx2] + dp[_idx2];

                return (y2 - y1) / (x1 - x2);
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';

                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
