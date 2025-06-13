using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 13
이름 : 배성훈
내용 : 약수의 합
    문제번호 : 17425번

    누적합, 에라토스테네스의 체 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1110
    {

        static void Main1110(string[] args)
        {

            int MAX = 1_000_000;

            StreamReader sr;
            long[] g;

            Solve();
            void Solve()
            {

                SetArr();

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    int n = ReadInt();

                    for (int i = 0; i < n; i++)
                    {

                        sw.Write($"{g[ReadInt()]}\n");
                    }
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

            void SetArr()
            {

                g = new long[MAX + 1];

                for (int i = 1; i <= MAX; i++)
                {

                    for (int j = i; j <= MAX; j += i)
                    {

                        g[j] += i;
                    }
                }

                for (int i = 1; i <= MAX; i++)
                {

                    g[i] += g[i - 1];
                }
            }
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
// #include <cstdlib>
// #include <cmath>
// #include <climits>
// #include <cstring>
// #include <string>
// #include <vector>
// #include <queue>
// #include <numeric>
// #include <functional>
// #include <set>
// #include <map>
// #include <unordered_map>
// #include <unordered_set>
// #include <memory>
// #include <thread>
// #include <tuple>
// #include <limits>
// #include <iostream>

using namespace std;

int main() {
  vector<long long> sigma(1'000'001, 1);
  sigma[1] = 1;
  const int limit = 1'000'000;
  for (int i = 2; i <= limit; i++) {
    if (sigma[i] > 1) continue;
    long long sump = 1;
    for (long long p = i; p <= limit; p *= i) {
      int r = 0;
      sump += p;
      for (long long j = p; j <= limit; j += p) {
        r++;
        if (r >= i) r -= i;
        if (r == 0) continue;
        sigma[j] *= sump;
      }
    }
  }
  for (int i = 2; i <= limit; i++) {
    sigma[i] += sigma[i - 1];
  }

  ios_base::sync_with_stdio(false);
  cin.tie(nullptr);
  int T;
  cin >> T;
  while (T-- > 0) {
    int n;
    cin >> n;
    cout << sigma[n] << "\n";
  }
  return 0;
}

#endif
}
