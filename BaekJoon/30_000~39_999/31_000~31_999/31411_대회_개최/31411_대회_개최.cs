using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 24
이름 : 배성훈
내용 : 대회 개최
    문제번호 : 31411번

    그리디, 정렬, 두 포인터 문제다
    우선 문제를 오름차순으로 배치하면 최댓값과 최솟값의 차의 최대값이 된다
    방법이 안떠올라 해설을 봤다
    아이디어는 다음과 같다
    문제를 1 ~n까지 하나씩 선택해야 한다
    오름차순으로 문제를 정렬한다
    그리고 연속된 범위로 문제를 선택한다
    여기서 n개의 알고리즘 문제가 선택되었는지 확인한다
    선택되면 양 끝값을 빼서 최소값을 찾는다

    구현을 잘못해 4번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1077
    {

        static void Main1077(string[] args)
        {

            StreamReader sr;
            int n, k;
            (int val, int d)[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.val.CompareTo(y.val));
                int l = 0;
                int r = -1;
                int[] cnt = new int[n];
                int match = 0;

                int len = n * k;
                int ret = 200_000;
                while (r < len)
                {

                    if (match < n)
                    {

                        if (r == len - 1) break;
                        r++;
                        int chk = cnt[arr[r].d]++;
                        if (chk == 0) match++;
                    }
                    else
                    {

                        ret = Math.Min(ret, arr[r].val - arr[l].val);
                        int chk = cnt[arr[l].d]--;
                        if (chk == 1) match--;
                        l++;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                arr = new (int val, int d)[n * k];
                int idx = 0;
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < k; c++)
                    {

                        arr[idx++] = (ReadInt(), r);
                    }
                }

                sr.Close();

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
// #include <vector>
using namespace std;

int main() {
    cin.tie(0)->sync_with_stdio(0);
    int N, K; cin >> N >> K;
    vector<int> v, vis(N);
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < K; j++) {
            int x; cin >> x;
            v.push_back(x * N + i);
        }
    }
    sort(v.begin(), v.end());
    
    int cnt = 0, ans = 1e9 + 7;
    int l = 0, r = 0;
    while (r < N * K) {
        while (r < N * K && cnt < N) {
            int xx = v[r] / N, ii = v[r] % N;
            if (!vis[ii]++) cnt++;
            r++;
        }
        if (cnt >= N) ans = min(ans, v[r - 1] / N - v[l] / N);
        cnt -= !--vis[v[l++] % N];
    }

    cout << ans;
}

#endif
}
