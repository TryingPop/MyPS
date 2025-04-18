using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 12
이름 : 배성훈
내용 : 수들의 합 4
    문제번호 : 2015번

    누적합 문제다.
    아이디어는 다음과 같다.
    0 ~ i번째까지의 누적합을 sum[i]에 저장한다.
    그러면 a ~ b까지의 누적합은 sum[b] - sum[a - 1]이 된다.
    그래서 i를 끝으로 하는 k가 나오는 연속 부분수열의 누적합은
    sum[i] = k - s가 된다.
    각 sum[i] 값의 개수를 해시에 저장하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1107
    {

        static void Main1107(string[] args)
        {

            int[] sum;
            int n;
            int k;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long ret = 0;
                Dictionary<int, int> dic = new(n);
                for (int i = 1; i <= n; i++)
                {

                    if (sum[i] == k) ret++;

                    if (dic.ContainsKey(sum[i] - k)) ret += dic[sum[i] - k];

                    if (dic.ContainsKey(sum[i])) dic[sum[i]]++;
                    else dic[sum[i]] = 1;
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                sum = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    sum[i] = ReadInt() + sum[i - 1];
                }

                sr.Close();
                int ReadInt()
                {

                    int c = sr.Read();
                    bool positive = c != '-';
                    int ret = positive ? c - '0' : 0;

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return positive ? ret : -ret;
                }
            }
        }
    }
#if other
// #include <iostream>
// #include <algorithm>
// #include <vector>
using namespace std;

int n, k, arr[200'005];
long long cont[200'005];
vector <int> s;
int main()
{
    ios::sync_with_stdio(0), cin.tie(0), cout.tie(0);
    cin >> n >> k;
    s.push_back(0);
    for (int i=1; i<=n; i++) {
        cin >> arr[i];
        arr[i] += arr[i-1];
        s.push_back(arr[i]);
    }
    sort(s.begin(), s.end());
    s.erase(unique(s.begin(), s.end()), s.end());
    
    long long cnt = 0;
    cont[lower_bound(s.begin(), s.end(), 0)-s.begin()]++;
    for (int i=1; i<=n; i++) {
        int a = arr[i]-k;
        int b = lower_bound(s.begin(), s.end(), a)-s.begin();
        if (a == s[b])
            cnt += cont[b];
        cont[lower_bound(s.begin(), s.end(), arr[i])-s.begin()]++;
    }
    cout << cnt;
    return 0;
}
#endif
}
