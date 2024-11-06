using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 19
이름 : 배성훈
내용 : Leftmost Segment
    문제번호 : 14751번

    볼록 껍질을 이용한 최적화 문제다
    (컨벡스 헐 트릭)

    직접 컨벡스헐 트릭을 구현하는 문제다
    문제에서는 y로 했는데 x, y를 한번 바꿔서 했기에
    복잡해지고 이걸로 x를 비교해야할 곳에서 y를 비교했기에 엄청나게 틀렸다

    다른 사람 팁을 보니 부동소수점 오차를 고려해야한다고 했고
    여기서 처음으로 EPS ? 로 부동소수점 조절해서 어느게 잘못되었는지 찾기 힘들었다

    이후 하나씩 디버깅하고 로직을 따져가니 잘못된 구간을 찾았고
    딱 끝과 일치하는 부분인 이분탐색 idx 문제에 한 번 더 틀렸다;
    오차를 허용하는 코드라 따로 반례를 처리했다

    이후에는 540ms에 통과했다
    해당 문제에서 ReadInt() 함수로 데이터를 받아오고 싶었으나
    예제부터 중간에 공백이 있어 포기했다
*/

namespace BaekJoon._58
{
    internal class _58_03
    {

        static void Main3(string[] args)
        {

            decimal ERROR = (decimal)1e-9;
            StreamReader sr;

            int n;
            int upper, lower;
            long dX;

            int[] dp;
            decimal[] val;

            int dpLen;
            (decimal a, decimal b, int idx)[] arr;

            Solve();
            void Solve()
            {

                Input();

                SetLeft();

                GetRet();
            }

            void GetRet()
            {

                int len = int.Parse(sr.ReadLine().Trim());

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < len; i++)
                {

                    decimal find = decimal.Parse(sr.ReadLine().Trim());
                    int l = 0;
                    int r = dpLen;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (val[mid] - find <= ERROR) r = mid - 1;
                        else l = mid + 1;
                    }

                    if (r == dpLen) r--;
                    sw.Write($"{arr[dp[r + 1]].idx}\n");
                }

                sw.Close();
                sr.Close();
            }

            void SetLeft()
            {

                dp = new int[n];
                val = new decimal[n];

                int top = 0;
                int[] stack = dp;

                for (int i = 1; i < n; i++)
                {

                    decimal temp;
                    while (top > 0)
                    {

                        int idx = dp[top - 1];
                        if (IsMeet(idx, i, out temp) && val[top - 1] + ERROR < temp) top--;
                        else break;
                    }

                    if (top == 0) val[0] = lower;

                    if (IsMeet(dp[top], i, out temp) && F(i, lower) + ERROR < F(dp[top], lower))
                    {

                        val[top++] = temp;
                        dp[top] = i;
                        val[top] = lower;
                    }
                }

                dpLen = top;
            }

            bool IsMeet(int _idx1, int _idx2, out decimal _x)
            {

                _x = arr[_idx1].a - arr[_idx2].a;
                if (Math.Abs(_x) < ERROR) return false;

                _x = (arr[_idx2].b - arr[_idx1].b) / _x;

                if (ERROR < lower - _x || ERROR < _x - upper) return false;
                return true;
            }

            decimal F(int _idx, decimal _x)
            {

                return arr[_idx].a * _x + arr[_idx].b;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                upper = int.Parse(temp[0]);
                lower = int.Parse(temp[1]);

                dX = upper - lower;

                n = int.Parse(sr.ReadLine().Trim());

                arr = new (decimal a, decimal b, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    temp = sr.ReadLine().Split();
                    decimal u = decimal.Parse(temp[0]);
                    decimal l = decimal.Parse(temp[1]);

                    decimal m = (u - l) / dX;
                    arr[i] = (m, l - m * lower, i + 1);
                }

                // 중간에부터 끼워넣는게 아닌 정렬해서 넣어 시간이 꽤 걸린다
                Array.Sort(arr, (x, y) =>
                {

                    decimal chk1 = x.a * upper + x.b;
                    decimal chk2 = y.a * upper + y.b;
                    if (Math.Abs(chk1 - chk2) < ERROR)
                    {

                        chk1 = x.a * lower + x.b;
                        chk2 = y.a * lower + y.b;

                        if (Math.Abs(chk1 - chk2) < ERROR) return 0;
                        return chk1.CompareTo(chk2);
                    }
                    return chk1.CompareTo(chk2);
                });
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;
using ll = long long;

struct Rational {
    int num, den;
    Rational() {};
    Rational(int n, int d)
    : num(n), den(d) {};
    bool operator<(const Rational& rhs) {
        return (ll)num * rhs.den < (ll)den * rhs.num;
    }
    bool operator==(const Rational& rhs) {
        return (ll)num * rhs.den == (ll)den * rhs.num;
    }
};
struct Line {
    int id, m, xh, xl;
    Line(int id, int m, int xh, int xl)
    : id(id), m(m), xh(xh), xl(xl) {};
    bool operator<(const Line& rhs) {
        return m > rhs.m;
    }
};
vector<Line> lines, hull;
vector<Rational> Ys;

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0); cout.tie(0);
    Rational Y;
    string s;
    int maxY, minY, N, xh, xl, M;

    cin >> maxY >> minY >> N;
    for (int n=1; n<=N; n++) {
        cin >> xh >> xl;
        lines.emplace_back(n, xh-xl, xh, xl);
    }
    sort(lines.begin(), lines.end());

    for (auto& line : lines) {
        while (hull.size()) {
            Line& last = hull.back();
            if (line.xl < last.xl && line.xh < last.xh) {
                hull.pop_back(), Ys.pop_back();
                continue;
            }
            else if (line.xl > last.xl && line.xh > last.xh)
                break;
            Y = { line.xl - last.xl, last.m - line.m };
            if (Ys.back() < Y) {
                hull.emplace_back(line);
                Ys.push_back(Y);
                break;
            }
            else
                hull.pop_back(), Ys.pop_back();
        }
        if (!hull.size()) {
            hull.emplace_back(line);
            Ys.emplace_back(0, 1);
        }
    }
    Ys.emplace_back(1, 1);

    maxY *= 1000, minY *= 1000;
    cin >> M;
    while (M--) {
        cin >> s;
        bool isNeg = (s[0] == '-');
        if (isNeg)
            s.erase(0, 1);
        auto period = s.find('.');
        while (s.length() < period +4)
            s.push_back('0');
        int intg = stoi(s.substr(0, period));
        int frac = stoi(s.substr(period +1));
        int y = intg*1000 + frac;
        if (isNeg)
            y = -y;
        Y = { y - minY, maxY - minY };
        auto i = distance(Ys.begin(), lower_bound(Ys.begin(), Ys.end(), Y));
        if (Y < Ys[i])
            i--;
        cout << hull[i].id << '\n';
    }

    return 0;
}

#elif other2
// #include <iostream>
// #include <vector>
// #include <algorithm>

using namespace std;
using ll = long long;
using pii = pair<int, int>;

struct P {
    int x, y, i;
    bool operator<(P& r) const {
        return x != r.x ? x < r.x : y < r.y;
    }
};

ll area(P a, P b, P c) {
    return (ll)(a.x-b.x)*(a.y-c.y)-(ll)(a.y-b.y)*(a.x-c.x);
}

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);

    int maxY, minY, n;
    cin >> maxY >> minY >> n;
    vector<P> p(n);
    for (int i=0, uX, lX; i<n; ++i) {
        cin >> uX >> lX;
        p[i] = {lX-uX, uX*(maxY-minY), i+1};
    }

    sort(begin(p), end(p));
    vector<P> ch;
    ch.push_back(p[0]);
    for (int i=1; i<n; ++i) {
        while (ch.size() >= 2 &&
            area(ch[ch.size()-2], ch.back(), p[i]) <= 0)
            ch.pop_back();
        ch.push_back(p[i]);
    }
    while (ch.size() >= 2 && ch[ch.size()-2].y <= ch.back().y)
        ch.pop_back();

    int m;
    cin >> m >> ws;
    for (char c; m--;) {
        ll t = 0;
        bool f = false;
        if (cin.peek() == '-') cin.get(c), f = true;
        while (true) {
            cin.get(c);
            if (c == '.') break;
            t = t*10+c-'0';
        }
        int cnt = 3;
        while (true) {
            cin.get(c);
            if (c == '\n') break;
            t = t*10+c-'0';
            --cnt;
        }
        while (cnt--) t *= 10;
        if (f) t = -t;
        t = maxY*1000-t;

        int l = 0, r = ch.size()-1;
        while (l < r) {
            int m = (l+r)/2;
            if ((ch[m+1].x-ch[m].x)*t + (ch[m+1].y-ch[m].y)*1000ll > 0)
                r = m;
            else l = m+1;
        }
        cout << ch[l].i << '\n';
    }

}
#elif other3
import io, os, sys
input=io.BytesIO(os.read(0, os.fstat(0).st_size)).readline

EPS = 1e-7
from bisect import bisect_left
def CHT(K, M): # K[i] * x + M[i]
  intersect = lambda i, j: (M[j] - M[i]) / (K[i] - K[j])

  hull_i = []
  hull_x = []
  order = sorted(range(len(K)), key=K.__getitem__, reverse=True)
  for i in order:
    while True:
      if not hull_i:
        hull_i.append(i)
        break
      elif (K[hull_i[-1]] - K[i]) < EPS :
        if M[hull_i[-1]] < M[i]:
          break
        hull_i.pop()
        if hull_x: hull_x.pop()
      else:
        x = intersect(i, hull_i[-1])
        if hull_x and x <= hull_x[-1]:
          hull_i.pop()
          hull_x.pop()
        else:
          hull_i.append(i)
          hull_x.append(x)
          break
  return hull_i, hull_x

def query(x, K, M, hull_i, hull_x):
  i = hull_i[bisect_left(hull_x, x + EPS)]
  return i

def sol() :
  maxY, minY = map(int, input().split())
  N = int(input())
  K, M = [], []
  for _ in range(N):
    b, a = map(int, input().split())
    K.append((b - a) / (maxY - minY))
    M.append((a * maxY - b * minY) / (maxY - minY))
  hull_i, hull_x = CHT(K, M)

  Q = int(input())
  ans = []
  for _ in range(Q):
    x = float(input())
    ans.append(query(x, K, M, hull_i, hull_x) + 1)
  
  sys.stdout.write('\n'.join(map(str, ans)))

sol()
#endif
}
