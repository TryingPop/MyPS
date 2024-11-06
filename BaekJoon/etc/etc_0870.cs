using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 7
이름 : 배성훈
내용 : 유전자 조합
    문제번호 : 25758번

    비둘기집 원리, 해시, 브루트포스 문제다
    입력이 10만개다 유전자로 가질 수 있는 경우는 
    최대 576이다 10만개가 들어오면 중복이 엄청나게 많으니
    유전자로 가질 수 있는 개수에 초점을 맞춰 풀었다

    그리고 자기 자신 1개로는 합성이 안되기에 1개인 경우 
    자기자신인지만 체크하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0870
    {

        static void Main870(string[] args)
        {

            StreamReader sr;
            HashSet<int> set;
            int[] f, b;
            bool[] ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = new bool[128];
                int cnt = 0;
                for (int i = 'A'; i <= 'Z'; i++)
                {

                    if (f[i] == 0) continue;

                    for (int j = 'A'; j <= 'Z'; j++)
                    {

                        if (b[j] == 0) continue;
                        int idx = Math.Max(i, j);
                        if (ret[idx]) continue;

                        if (f[i] == 1 && b[j] == 1 && set.Contains(i * 128 + j)) continue;
                        cnt++;
                        ret[idx] = true;
                    }
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    sw.Write($"{cnt}\n");
                    for (char i = 'A'; i <= 'Z'; i++)
                    {

                        if (!ret[i]) continue;
                        sw.Write(i);
                        sw.Write(' ');
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                set = new(576);

                f = new int[128];
                b = new int[128];
                int n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int c1 = sr.Read();
                    int c2 = sr.Read();

                    f[c1]++;
                    b[c2]++;

                    set.Add(c1 * 128 + c2);
                    sr.Read();
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
// #include <bits/stdc++.h>
using namespace std;

int main(void) {
  ios::sync_with_stdio(false);
  cin.tie(NULL);
  cout.tie(NULL);

  int n;
  cin >> n;

  int check[26][26] = {0, };
  for (int i = 0; i < n; ++i) {
    char first, second;
    cin >> first >> second;
    check[first - 'A'][second - 'A']++;
  }

  bool ans[26] = {0, };
  for (int i = 0; i < 26 * 26; ++i) {
    int lf = i / 26, ls = i % 26;
    if (check[lf][ls] > 0) {
      --check[lf][ls];
      for (int j = 0; j < 26 * 26; ++j) {
        int rf = j / 26, rs = j % 26;
        if (check[rf][rs] > 0) {
          ans[max(lf, rs)] = ans[max(rf, ls)] = true;
        }
      }
    }
  }

  int cnt = 0;
  for (int i = 0; i < 26; ++i)
    cnt += ans[i];

  cout << cnt << "\n";
  for (int i = 0; i < 26; ++i)
    if (ans[i])
      cout << (char)(i + 'A') << " ";
    
  return 0;
}
#endif
}
