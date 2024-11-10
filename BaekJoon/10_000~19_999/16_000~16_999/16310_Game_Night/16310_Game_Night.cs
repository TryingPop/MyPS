using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 10
이름 : 배성훈
내용 : Game Night
    문제번호 : 16310번

    누적합, 슬라이딩 윈도우 문제다
    슬라이딩 윈도우를 이용해 모든 경우를 탐색해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1101
    {

        static void Main1101(string[] args)
        {

            string arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] cnt = new int[3];
                int[] next = new int[3];
                for (int i = 0; i < arr.Length; i++)
                {

                    switch (arr[i])
                    {

                        case 'A':
                            cnt[0]++;
                            break;

                        case 'B':
                            cnt[1]++;
                            break;

                        case 'C':
                            cnt[2]++;
                            break;
                    }
                }

                int cur = 0;
                next[0] = cnt[0];
                next[1] = cnt[0] + cnt[1];
                next[2] = arr.Length;
                for (int i = 0; i < next[0]; i++)
                {

                    if (arr[i] != 'A') cur++;
                }

                for (int i = next[0]; i < next[1]; i++)
                {

                    if (arr[i] != 'B') cur++;
                }

                for (int i = next[1]; i < next[2]; i++)
                {

                    if (arr[i] != 'C') cur++;
                }

                int ret = cur;
                for (int idx1 = 0; idx1 < arr.Length; idx1++)
                {

                    int idx2 = (idx1 + next[0]) % arr.Length;
                    int idx3 = (idx1 + next[1]) % arr.Length;

                    if (arr[idx1] == 'C') cur--;
                    else if (arr[idx1] == 'A') cur++;

                    if (arr[idx2] == 'A') cur--;
                    else if (arr[idx2] == 'B') cur++;

                    if (arr[idx3] == 'B') cur--;
                    else if (arr[idx3] == 'C') cur++;

                    ret = Math.Min(ret, cur);
                }

                next[1] = cnt[0] + cnt[2];
                cur = 0;
                for (int i = 0; i < next[0]; i++)
                {

                    if (arr[i] != 'A') cur++;
                }

                for (int i = next[0]; i < next[1]; i++)
                {

                    if (arr[i] != 'C') cur++;
                }

                for (int i = next[1]; i < next[2]; i++)
                {

                    if (arr[i] != 'B') cur++;
                }

                ret = Math.Min(ret, cur);
                for (int idx1 = 0; idx1 < arr.Length; idx1++)
                {

                    int idx2 = (idx1 + next[0]) % arr.Length;
                    int idx3 = (idx1 + next[1]) % arr.Length;

                    if (arr[idx1] == 'B') cur--;
                    else if (arr[idx1] == 'A') cur++;

                    if (arr[idx2] == 'A') cur--;
                    else if (arr[idx2] == 'C') cur++;

                    if (arr[idx3] == 'C') cur--;
                    else if (arr[idx3] == 'B') cur++;

                    ret = Math.Min(ret, cur);
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sr.ReadLine();
                arr = sr.ReadLine();

                sr.Close();
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
typedef long long ll;
typedef pair<int,int> pii;

int n;
char A[100005];
int cnt[3];
int ans;
void pro(int a,int b) {
    int tmp = 0;
    for(int i=0;i<cnt[a];i++) tmp += (A[i] != (a + 'A'));
    for(int i=cnt[a];i<cnt[a]+cnt[b];i++) tmp += (A[i] != (b + 'A'));
    for(int i=cnt[a]+cnt[b];i<n;i++) tmp += (A[i] != (3 - a - b + 'A'));
    ans = min(ans , tmp);
    for(int i=0;i+cnt[a]+cnt[b]<n;i++) {
        tmp -= (A[i] != (a + 'A'));
        tmp -= (A[cnt[a] + i] != (b + 'A'));
        tmp -= (A[cnt[a] + cnt[b] + i] != (3 - a - b + 'A'));

        tmp += (A[i] != (3 - a - b + 'A'));
        tmp += (A[cnt[a] + i] != (a + 'A'));
        tmp += (A[cnt[a] + cnt[b] + i] != (b + 'A'));

        ans = min(ans , tmp);
    }
}
int main() {
    ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
    cin>>n;
    cin>>A;
    for(int i=0;i<n;i++) {
        cnt[A[i]-'A']++;
    }
    ans = n-1;
    for(int i=0;i<3;i++) {
        for(int j=0;j<3;j++) {
            if(i != j) {
                pro(i,j);
            }
        }
    }
    cout<<ans;
    return 0;
}
#endif
}
