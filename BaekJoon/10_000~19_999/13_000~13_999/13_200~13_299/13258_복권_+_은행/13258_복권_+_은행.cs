using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 복권 + 은행
    문제번호 : 13258번

    수학 dp문제인데

    수학으로만 풀었다
    my : 나의 초기 금액
    other : 다른 이의 초기 금액
    reward : 당첨금
    weeks : 주
    total = other + my;
    my + (my / (total)) * reward * weeks
    정답이 동형임을 알 수 있고

    해당 식을제출하니 이상없이 맞췄다
*/

namespace BaekJoon.etc
{
    internal class etc_0241
    {

        static void Main241(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int my = ReadInt(sr);
            int other = 0;
            for (int i = 1; i < n; i++)
            {

                other += ReadInt(sr);
            }

            int reward = ReadInt(sr);
            int weeks = ReadInt(sr);

            sr.Close();

            decimal per = my / (decimal)(other + my);
            per *= reward * weeks;

            decimal ret = per + my;

            Console.WriteLine($"{ret:0.0#########}");
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
// dp를 이용한 풀이
// #include <bits/stdc++.h>

// #define FAST() cin.tie(0)->sync_with_stdio(0)
// #define ALL(x) (x).begin(), (x).end()
// #define SIZE(x) (int)((x).size())

// #define deb(x) cout << #x << " : " << (x) << "\n"
// #define deb_pair(x, y) cout << "(" << #x << ", " << #y << ") : (" << (x) << ", " << (y) << ")\n"
// #define deb_triplet(x, y, z) cout << "(" << #x << ", " << #y << ", " << #z << ") : (" << (x) << ", " << (y) << ", " << (z) << ")\n"
// #define deb_tuple(s) \
    cout << "["; \
    for (int __i = 0; __i < SIZE(s); __i++) { \
        cout << s[__i]; \
        if (__i != SIZE(s) - 1) cout << ", "; \
    } \
    cout << "]\n";

using namespace std;

vector<int> a;
int b, m, s;
vector<vector<double>> c;

double dp(int i, int j) {
    if (i == m) {
        return a[0] + b * j;
    }
    if (c[i][j] != -1) {
        return c[i][j];
    }

    double sum = 0;

    int t = s + b * i;
    sum += (double)(a[0] + b * j) / t * dp(i + 1, j + 1);
    sum += (double)(t - (a[0] + b * j)) / t * dp(i + 1, j);

    return c[i][j] = sum;
}

int main() {
    FAST();

    int n;
    cin >> n;

    a.resize(n);
    for (auto& x : a) {
        cin >> x;
    }

    s = accumulate(ALL(a), 0);

    cin >> b >> m;

    c.resize(m, vector<double>(m, -1));

    cout << fixed;
    cout.precision(15);

    cout << dp(0, 0) << '\n';
}

#endif
}
