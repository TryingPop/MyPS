using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 8
이름 : 배성훈
내용 : Nine Packs
    문제번호 : 13295번

    배낭 문제다.
    문제를 잘못 이해해 한 번 틀렸다.
    찾아야할 것은 핫도그와 빵을 같은 무게로 만드는데 들어가는 최솟값이다.
    둘 다 0인 경우를 제외한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1387
    {

        static void Main1387(string[] args)
        {

            int[] h, b, dp1, dp2;
            int max;

            Input();

            GetRet();

            void GetRet()
            {

                int IMPO = 234;
                Array.Fill(dp1, IMPO);
                Array.Fill(dp2, IMPO);
                dp1[0] = 0;
                dp2[0] = 0;

                FillDp(h, dp1);
                FillDp(b, dp2);

                int ret = IMPO;
                for (int i = 1; i <= max; i++)
                {

                    ret = Math.Min(ret, dp1[i] + dp2[i]);
                }

                if (ret >= IMPO) Console.Write("impossible");
                else Console.Write(ret);

                void FillDp(int[] _food, int[] _dp)
                {

                    int e = 0;
                    for (int i = 1; i < _food.Length; i++)
                    {

                        for (int cur = e; cur >= 0; cur--)
                        {

                            if (_dp[cur] == IMPO) continue;
                            int add = cur + _food[i];
                            _dp[add] = Math.Min(_dp[add], _dp[cur] + 1);
                        }

                        e += _food[i];
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                h = Array.ConvertAll(sr.ReadLine().Trim().Split(), int.Parse);
                b = Array.ConvertAll(sr.ReadLine().Trim().Split(), int.Parse);

                int sumB = 0, sumH = 0;
                for (int i = 1; i < h.Length; i++)
                {

                    sumH += h[i];
                }

                for (int i = 1; i < b.Length; i++)
                {

                    sumB += b[i];
                }

                max = Math.Max(sumH, sumB);
                dp1 = new int[max + 1];
                dp2 = new int[max + 1];
            }
        }
    }
}
