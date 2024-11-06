using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 30
이름 : 배성훈
내용 : 볼링장 아르바이트
    문제번호 : 27979번

    정렬, 애드 혹 문제다
    아이디어는 다음과 같다
    뒤에서부터 조사하는데
    가장 무거운 것이 나올 때까지 빼서 앞으로 옮겨야한다
    가장 무거운게 나오면 해당 공은 맨 뒤로 갔으므로 정위치에 놓여있어 앞으로 갈 필요가 없다
    다시 다음으로 가장 무거운 공이 나올 때까지 빼야한다
    이렇게 처음까지 조사하면 빼야할 공의 개수와 일치한다
    빼내야 할 공을 적절한 순서로 빼낸다면 정렬되게 할 수 있다

    이렇게 제출하니 92ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0782
    {

        static void Main782(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int[] sortArr;

            int n;

            Solve();

            void Solve()
            {

                Input();

                Console.Write(GetRet());
            }

            int GetRet()
            {

                int ret = n;
                Array.Sort(sortArr);

                int idx = n - 1;
                for (int i = n - 1; i >= 0; i--)
                {

                    if (sortArr[idx] != arr[i]) continue;
                    idx--;
                    ret--;
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                n = ReadInt();

                arr = new int[n];
                sortArr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    sortArr[i] = cur;
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
// #include <bits/stdc++.h>
// #define ll long long
using namespace std;
ll N, mx, ans;
vector<ll> v;
int main(){
    ios::sync_with_stdio(0);
    cin.tie(0);
    cin >> N;
    while (N--){
        ll a;
        cin >> a;
        if (!v.empty() && v[v.size()-1]>a){
            ans++;
            mx=max(mx,a);
        }
        else
            v.push_back(a);
    }
    cout << ans+(lower_bound(v.begin(), v.end(), mx)-v.begin());
    return 0;
}
#endif
}
