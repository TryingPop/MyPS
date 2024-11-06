using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 20
이름 : 배성훈
내용 : Rummikub
    문제번호 : 11453번

    구현, 문자열, 애드 혹 문제다
    루미큐브 규칙에 따라 같은 색상에 연속된 숫자 3개 이상이 있거나
    서로 다른 색상에 같은 숫자 3개가 있는지 확인하면 된다
*/
namespace BaekJoon.etc
{
    internal class etc_0982
    {

        static void Main982(string[] args)
        {

            string YES = "YES\n";
            string NO = "NO\n";

            StreamReader sr;
            StreamWriter sw;

            int m;
            bool[] red, green, blue, yellow;

            Solve();
            void Solve()
            {

                Init();

                int test = int.Parse(sr.ReadLine().Trim());
                while (test-- > 0)
                {

                    Input();

                    sw.Write(GetRet() ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                m = int.Parse(sr.ReadLine().Trim());
                for (int i = 1; i <= 100; i++)
                {

                    red[i] = false;
                    green[i] = false;
                    blue[i] = false;
                    yellow[i] = false;
                }

                string[] temp = sr.ReadLine().Split();
                for (int i = 0; i < m; i++)
                {

                    ReadColor(temp[i]);
                }
            }

            bool GetRet()
            {

                for (int i = 1; i <= 100; i++)
                {

                    int cnt = 0;
                    if (red[i]) cnt++;
                    if (green[i]) cnt++;
                    if (blue[i]) cnt++;
                    if (yellow[i]) cnt++;

                    if (cnt > 2) return true;
                }

                int r = 0;
                int g = 0;
                int b = 0;
                int y = 0;

                for (int i = 1; i <= 3; i++)
                {

                    if (red[i]) r++;
                    if (green[i]) g++;
                    if (blue[i]) b++;
                    if (yellow[i]) y++;
                }

                if (r == 3 || g == 3 || b == 3 || y == 3) return true;
                for (int i = 4; i <= 100; i++)
                {

                    if (red[i]) r++;
                    if (green[i]) g++;
                    if (blue[i]) b++;
                    if (yellow[i]) y++;

                    if (red[i - 3]) r--;
                    if (green[i - 3]) g--;
                    if (blue[i - 3]) b--;
                    if (yellow[i - 3]) y--;

                    if (r == 3 || g == 3 || b == 3 || y == 3) return true;
                }

                return false;
            }

            void ReadColor(string _str)
            {

                int len = _str.Length;
                int color = _str[--len];

                int val = 0;
                for (int i = 0; i < len; i++)
                {

                    val = val * 10 + _str[i] - '0';
                }

                if (color == 'r') red[val] = true;
                else if (color == 'g') green[val] = true;
                else if (color == 'y') yellow[val] = true;
                else if (color == 'b') blue[val] = true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                red = new bool[101];
                green = new bool[101];
                blue = new bool[101];
                yellow = new bool[101];
            }
        }
    }

#if other
// #include <iostream>
// #include <string>
// #include <unordered_map>
using namespace std;

inline int calc(char c) {
  return (1 << 0) * (c == 'b') +
         (1 << 1) * (c == 'g') +
         (1 << 2) * (c == 'r') +
         (1 << 3) * (c == 'y');
}

bool solve(void) {
  int m; cin >> m;

  unordered_map<int, int> mp;
  while (m--) {
    string s; cin >> s;
    mp[stoi(s.substr(0, s.length()-1))] |= calc(s.back());
  }

  for (int i=1; i<=100; i++) {
    if (__builtin_popcount(mp[i]) >= 3) return true;
  }
  for (int i=3; i<=100; i++) {
    if (mp[i-2] & mp[i-1] & mp[i]) return true;
  }
  return false;
}

int main(void) {
  ios::sync_with_stdio(false);
  cin.tie(nullptr);

  int t; cin >> t;
  while (t--) cout << (solve() ? "YES" : "NO") << "\n";
  return 0;
}
#endif
}
