using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 8
이름 : 배성훈
내용 : 전봇대
    문제번호 : 8986번

    삼분탐색 문제다
    식을 풀면 절댓값 일차식들의 합이되고 (Unimodal 이라 한다)
    만약 빼는 값이 일정하면 중앙값을 찾아 풀 수 있으나
    여기서는 자리별로 값이 달라 중앙값을 바로 못쓴다

    절댓값 식들의 합은 U, V 형태의 그래프가 되므로 
    삼분탐색을 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0954
    {

        static void Main954(string[] args)
        {

            StreamReader sr;
            long[] pos;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long s = 1;
                long e = 1_000_000_000;

                while (e - s >= 3)
                {

                    long chk1 = (s * 2 + e) / 3;
                    long chk2 = (s + e * 2) / 3;

                    if (GetDis(chk1) <= GetDis(chk2)) e = chk2;
                    else s = chk1;
                }

                long ret = (long)1e15;

                for (long i = s; i <= e; i++)
                {

                    ret = Math.Min(ret, GetDis(i));
                }

                Console.Write(ret);
            }

            long GetDis(long _n)
            {

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    ret += Math.Abs(_n * i - pos[i]);
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                pos = new long[n];

                for (int i = 0; i < n; i++)
                {

                    pos[i] = ReadInt();
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
// #include <iostream>
// #include <vector>
// #include <climits>

using namespace std;

using ll = long long;

void fastIo() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
}

int main() {
    int n;
    ll ans = LLONG_MAX;
    vector<int> v;

    fastIo();

    cin >> n;

    v.resize(n);

    for (int &cur: v)
        cin >> cur;

    {
        ll left = 0, right = INT_MAX / max(1, (n - 1));
        while (left <= right) {
            ll mid1 = left + (right - left) / 3;
            ll mid2 = left + (right - left) / 3 * 2;

            ll cur1 = 0, cur2 = 0;
            for (int i = 0; i < n; i++) {
                cur1 += abs(v[i] - i * mid1);
                cur2 += abs(v[i] - i * mid2);
            }

            if (cur1 < cur2) {
                ans = min(ans, cur1);
                right = mid2 - 1;
            }
            else {
                ans = min(ans, cur2);
                left = mid1 + 1;
            }
        }
    }

    cout << ans << '\n';
    return 0;
}
#endif
}
