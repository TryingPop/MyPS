using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 16
이름 : 배성훈
내용 : Shuffled Anagrams
    문제번호 : 23015번

    그리디, 문자열 문제다.
    아이디어는 다음과 같다.
    가장 많이 나오는 것을 가장 적게 나온거부터 채운다.
    그리고 다음 많은 것은 가장 많은거부터 채워간다.
    이렇게 서로 다른 것으로 모두 채울 수 있는경우 모두 채울 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1890
    {

        static void Main1890(string[] args)
        {

            string N = "IMPOSSIBLE\n";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            string str;
            Queue<int>[] pos = new Queue<int>[26];
            int[] cnt = new int[26];
            int[] ret = new int[10_000];
            Stack<int> stk = new(10_000);
            PriorityQueue<int, int> max = new(26), min = new(26);
            for (int i = 0; i < 26; i++)
            {

                pos[i] = new(10_000);
            }

            for (int i = 1; i <= n; i++)
            {

                sw.Write($"Case #{i}: ");
                Input();

                GetRet();
            }

            void GetRet()
            {

                min.Clear();
                max.Clear();

                for (int i = 0; i < 26; i++)
                {

                    if (pos[i].Count == 0) continue;
                    int add = pos[i].Count * 100 + i;
                    min.Enqueue(i, add);
                    max.Enqueue(i, -add);
                }

                int n = str.Length;

                if (Possible())
                {

                    Fill();

                    Output();
                }
                else sw.Write(N);

                void Fill()
                {

                    while (max.Count > 0)
                    {

                        int maxIdx = max.Dequeue();
                        int maxCnt = cnt[maxIdx];

                        while (maxCnt > 0)
                        {

                            int idx = stk.Pop();
                            ret[idx] = maxIdx;
                            maxCnt--;
                        }
                    }
                }

                bool Possible()
                {

                    int maxIdx = max.Dequeue();
                    int maxCnt = pos[maxIdx].Count;
                    if (n - maxCnt < maxCnt) return false;

                    while (maxCnt > 0)
                    {

                        int minIdx = min.Dequeue();
                        while (maxCnt > 0 && pos[minIdx].Count > 0)
                        {

                            int minPos = pos[minIdx].Dequeue();
                            ret[minPos] = maxIdx;
                            maxCnt--;
                        }
                    }

                    min.Clear();

                    for (int i = 0; i < 26; i++)
                    {

                        if (pos[i].Count == 0) continue;
                        min.Enqueue(i, pos[i].Count * 100 + i);
                    }

                    while (min.Count > 0)
                    {

                        int idx = min.Dequeue();
                        while (pos[idx].Count > 0)
                        {

                            stk.Push(pos[idx].Dequeue());
                        }
                    }

                    return true;
                }

                void Output()
                {

                    for (int i = 0; i < n; i++)
                    {

                        sw.Write((char)(ret[i] + 'a'));
                    }

                    sw.Write('\n');
                }
            }

            void Input()
            {

                str = sr.ReadLine();
                for (int i = 0; i < 26; i++)
                {

                    cnt[i] = 0;
                    pos[i].Clear();
                }

                for (int i = 0; i < str.Length; i++)
                {

                    int cur = str[i] - 'a';
                    pos[cur].Enqueue(i);
                    cnt[cur]++;
                }
            }
        }
    }

#if other
// #define sad return 0
// #include <bits/stdc++.h>
// #define all(v) (v).begin(), (v).end()
using namespace std;
using ll = long long;

string solve(string s, string t) {
  int len = s.size();

  sort(all(t));
  for (int i = 0; i < len; ++i) if (t[i]==t[(i+len/2)%len]) return "IMPOSSIBLE";

  vector<char> v;
  for (char c : t) v.push_back(c);
  int p[26];
  for (int i = 0; i < 26; ++i) p[i] = lower_bound(all(v), 'a'+i) - v.begin();

  string ret = "";
  for (int i = 0; i < len; ++i) {
    ret.push_back(t[ (p[s[i]-'a'] + len/2 ) % len ]);
    p[s[i]-'a']++;
  }
  return ret;
}

int main() {
  ios::sync_with_stdio(0), cin.tie(0);

  int _TEST; cin >> _TEST; for (int TEST = 1; TEST <= _TEST; ++TEST) {
    string s; cin >> s;
    cout << "Case #" << TEST << ": " << solve(s, s) << "\n";
  }

  sad;
}


#endif
}
