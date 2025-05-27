using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 1
이름 : 배성훈
내용 : 스터디 그룹
    문제번호 : 14572번

    그리디, 정렬, 두 포인터 문제다.
    n을 교집합, u을 합집합이라 하면 |Set|을 집합 Set의 원소의 갯수라 하면
    |A n B n C| <= |A n B|. |A u B| <= |A u B u C| 이고,
    
    그리고 a, b 모두 음이 아닌 정수일 때,
    a * b <= a * (b + 1)이다.

    그리디의 exchange argument로 인해
    효율은 학생을 최대한 포함할 수 있으면 포함하는게 좋다.

    그래서 점수로 정렬하고 학생을 최대한 포함하며 최대값을 계산해 갔다.
    그런데 정렬인가 입력 문제인가 뭔지는 몰라도 시간초과가 2번 났다;
*/

namespace BaekJoon.etc
{
    internal class etc_1233
    {

        static void Main1233(string[] args)
        {

            int n, k, d;
            (int iq, int known)[] student;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(student, (x, y) => x.iq.CompareTo(y.iq));

                int[] cnt = new int[k + 1];
                int human = 0;

                int left = 0;
                int ret = 0;

                for (int right = 0; right < n; right++)
                {

                    int cur = student[right].iq;
                    int min = cur - d;
                    human++;

                    Add(student[right].known, 1);
                    while (left < right && student[left].iq < min)
                    {

                        Add(student[left].known, -1);
                        human--;
                        left++;
                    }

                    int union = 0, inter = 0;

                    for (int i = 1; i <= k; i++)
                    {

                        if (cnt[i] > 0) union++;
                        if (cnt[i] == human) inter++;
                    }

                    int e = (union - inter) * human;

                    ret = Math.Max(ret, e);
                }

                Console.Write(ret);

                void Add(int _known, int _add)
                {

                    for (int i = 1; i <= k; i++)
                    {

                        if ((_known & (1 << i)) == 0) continue;
                        cnt[i] += _add;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();
                d = ReadInt();

                student = new (int iq, int known)[n];
                for (int i = 0; i < n; i++)
                {

                    int m = ReadInt();
                    student[i].iq = ReadInt();
                    for (int j = 0; j < m; j++)
                    {

                        student[i].known |= 1 << ReadInt();
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';
                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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

#if other
// #include <bits/stdc++.h>
const char nl = '\n';
using namespace std;
using ll = long long;
using ld = long double;

char get() {
  static char buf[100000], *S = buf, *T = buf;
  if (S == T) {
    T = (S = buf) + fread(buf, 1, 100000, stdin);
    if (S == T) return EOF;
  }
  return *S++;
}
void read(int& x) {
  static char c; x = 0;
  for (c = get(); c < '0'; c = get());
  for (; c >= '0'; c = get()) x = x * 10 + c - '0';
}

int main() {
  cin.tie(0)->sync_with_stdio(0);
  int n, K, D; read(n); read(K); read(D);
  vector<pair<int, int>> a(n);
  for (auto& [c, d] : a) {
    int k; read(k); read(d);
    for (int j = 0; j < k; j++) {
      int b; read(b); b--;
      c ^= 1 << b;
    }
  }
  sort(begin(a), end(a), [](const auto& a, const auto& b) { return a.second < b.second; });
  vector<int> cnt(K);
  int ans = 0;
  for (auto l = begin(a), r = begin(a); l != end(a); ++l) {
    while (r != end(a) && r->second - l->second <= D) {
      for (int i = 0; i < K; i++) {
        cnt[i] += (r->first >> i) & 1;
      }
      ++r;
    }
    int any = 0, all = 0;
    for (int i = 0; i < K; i++) {
      any += !!cnt[i];
      all += cnt[i] == (r - l);
    }
    ans = max(ans, int((any - all) * (r - l)));
    for (int i = 0; i < K; i++) {
      cnt[i] -= (l->first >> i) & 1;
    }
  }
  cout << ans << nl;
}

#endif
}
