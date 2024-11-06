using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 평범한 배낭 2
    문제번호 : 12920번

    dp, 배낭문제다
    같은 물건이 k개 있는 경우, n = sig ki이므로 (ki는 i번째 k를 의미하고, sig는 덧셈 합의 기호를 의미한다)
    최대한 2의 제곱수 묶어서 새로운 물품을 만들어 k의 크기를 줄인다
    제곱수로 묶는 부분의 실수로 3번 틀렸다

    이후에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0610
    {

        static void Main610(string[] args)
        {

            StreamReader sr;
            int n, m;

            List<int> v;
            List<int> w;
            List<int> k;

            int[] dp;

            Solve();

            void Solve()
            {

                Input();
                PreCalc();

                dp[0] = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = m - w[i]; j >= 0; j--)
                    {

                        if (dp[j] == -1) continue;
                        dp[j + w[i]] = Math.Max(dp[j + w[i]], dp[j] + v[i]);
                    }
                }

                int ret = 0;
                for (int i = 0; i <= m; i++)
                {

                    if (ret < dp[i]) ret = dp[i];
                }

                Console.WriteLine(ret);
            }

            ///
            /// 2제곱수로 묶어 k번 돌아야하는 것을 2 * log 단위의 개수로 줄인다
            /// 
            /// 예를들어 10개 있는 경우
            /// 1개를 살수도, 2개를 살수도,... 10개를 살 수 있다
            /// 여기서 4개를 사는걸 4개짜리 1개를 샀다고 볼 수 있다
            /// 7개는 1개 짜리 + 2개짜리 + 4개짜리 샀다고 볼 수 있다
            /// 즉 7개 사는 것을 3개 사는 것으로 줄인다
            ///
            /// 수가 작은 경우에는 크게 차이 안난다
            /// 다만, 1000인 경우
            /// 1, 2, 4, 8, 16, ..., 256, -> 총합 : 511
            /// 256, 128, 64, 32, 8, 1 -> 총합 : 489
            /// 1000개가 15개로 줄어든다
            ///
            void PreCalc()
            {

                int[] sq2 = new int[14];
                sq2[0] = 1;

                for (int i = 1; i < sq2.Length; i++)
                {

                    sq2[i] = sq2[i - 1] * 2;
                }

                for (int i = 0; i < n; i++)
                {

                    int stop = -1;
                    for (int j = 1; j < sq2.Length; j++)
                    {

                        if (sq2[j] < k[i])
                        {

                            k[i] -= sq2[j];
                            w.Add(w[i] * sq2[j]);
                            v.Add(v[i] * sq2[j]);
                            k.Add(1);
                            continue;
                        }

                        stop = j - 1;
                        break;
                    }

                    for (int j = stop; j >= 0; j--)
                    {

                        if (sq2[j] >= k[i]) continue;

                        k[i] -= sq2[j];
                        w.Add(w[i] * sq2[j]);
                        v.Add(v[i] * sq2[j]);
                        k.Add(1);
                    }
                }

                dp = new int[m + 1];
                Array.Fill(dp, -1);
                n = v.Count;
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                v = new(10 * n);
                w = new(10 * n);
                k = new(10 * n);

                for (int i = 0; i < n; i++)
                {

                    w.Add(ReadInt());
                    v.Add(ReadInt());
                    k.Add(ReadInt());
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

#if other
int[] NM = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
List<int[]> list = new List<int[]>();
for (int i = 0; i < NM[0]; i++)
{
    int[] VCK = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    int k = VCK[2];
    for(int j=1;k>0; j*=2)//[1,2,4,...나머지]의 배율로 묶음상품
    {
        int k_ = Math.Min(k, j);
        list.Add(new int[] { VCK[0] * k_, VCK[1]*k_});
        k -= j;
    }
}
int[] DP_ = new int[NM[1]+1];

for(int i=0;i< list.Count; i++)
{
    for(int j = DP_.Length-1; j >=0; j--)
    {//1차원 배열에서 냅색은 역순
        if (list[i][0] <= j)
        {
            DP_[j] =
                Math.Max(DP_[j - list[i][0]] + list[i][1], DP_[j]);
        }
    }
}
Console.WriteLine(DP_[DP_.Length - 1]);
#elif other2
using System;
using System.Collections.Generic;

namespace BOJ
{
    internal class Boj
    {
        public static void Main()
        {
            var nm = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            var item = new List<Tuple<int, int>>();
            item.Add(new Tuple<int, int>(0, 0));

            for (int i=1; i <= nm[0]; i++)
            {
                var arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                for(int j=0; arr[2] > 0; j++)
                {
                    var pow = (int)Math.Min(Math.Pow(2, j), arr[2]);

                    item.Add(new Tuple<int, int>(arr[0] * pow, arr[1] * pow));

                    arr[2] -= pow;
                }
            }


            var dp = new int[item.Count + 1, nm[1] + 1];

            for(int i=1; i <= item.Count; i++)
            {
                for(int j=1; j <= nm[1]; j++)
                {
                    if (j - item[i-1].Item1 < 0)
                        dp[i, j] = dp[i - 1, j];
                    else
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - item[i-1].Item1] + item[i-1].Item2);

                }
            }

            Console.WriteLine(dp[item.Count, nm[1]]);

        }
    }
}

#endif
}
