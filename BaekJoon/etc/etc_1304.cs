using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 31
이름 : 배성훈
내용 : 부분 수열의 중앙값
    문제번호 : 3013번

    누적합 문제다.
    여기서 말하는 부분수열은 연속된 부분수열이다.
    즉 항이 이어진 것이어야 한다.

    여기서 중앙값이 b임을 확인하는 것은
    b보다 큰 것을 1로, b보다 작은 것을 -1로 바꾸면
    누적합으로 바뀐다.

    이제 b 이후의 값에 대해 합 갯수를 누적해간다.
    그리고 앞으로 찾는데, b를 포함한 경우 앞의 갯수를 누적해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1304
    {

        static void Main1304(string[] args)
        {

            int n, b;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] cnt = new int[1 + (n << 1)];
                int bIdx = -1;
                for (int i = 0; i < n; i++)
                {


                    if (arr[i] > b) arr[i] = 1;
                    else if (arr[i] < b) arr[i] = -1;
                    else
                    {

                        bIdx = i;
                        arr[i] = 0;
                    }
                }

                int cur = 1;
                for (int i = bIdx; i < n; i++)
                {

                    if (arr[i] > 0) cur++;
                    else cur--;

                    cnt[n + cur]++;
                }

                long ret = 0;
                cur = -1;
                for (int i = bIdx; i >= 0; i--)
                {

                    if (arr[i] > 0) cur--;
                    else cur++;

                    ret += cnt[n + cur];
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                b = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
                        if (c == ' ' || c == '\n') return true;

                        ret = c - '0';
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <iostream>
using namespace std;

typedef long long ll;

int N, K, num;
ll ans;
int cnt[200002];

int main() {
    cin.tie(NULL);  ios_base::sync_with_stdio(false);
    cin >> N >> K;
    bool check = false;
    cnt[N] = 1;
    int cur = 0;
    for (int i = 0; i < N; i++) {
        cin >> num;
        if (num == K) check = true;
        cur += (num > K ? 1 : num < K ? -1 : 0);
        if (check) ans += cnt[cur + N];
        else cnt[cur + N]++;
    }
    cout << ans << '\n';
}
#endif
}
