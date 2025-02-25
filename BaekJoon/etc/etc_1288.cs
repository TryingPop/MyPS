using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 22
이름 : 배성훈
내용 : 화학실험
    문제번호 : 20311번

    그리디, 우선순위 큐, 정렬 문제다.
    그리디로 가장 많은거부터 중복 안되게 쓰는 것이 좋다.
    그래서 우선순위 큐로 최대 값을 가지며 진행했다
*/

namespace BaekJoon.etc
{
    internal class etc_1288
    {

        static void Main1288(string[] args)
        {

            int n, k;
            PriorityQueue<(int cnt, int idx), int> pq;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int half = (n + 1) >> 1;

                if (half < pq.Peek().cnt)
                {

                    sw.Write(-1);
                    return;
                }

                var node = pq.Dequeue();
                sw.Write($"{node.idx} ");
                node.cnt--;
                var prev = node;

                while (pq.Count > 0)
                {

                    node = pq.Dequeue();
                    sw.Write($"{node.idx} ");
                    node.cnt--;

                    if (prev.cnt > 0)
                    {

                        pq.Enqueue(prev, prev.cnt);
                    }

                    prev = node;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                pq = new(k, Comparer<int>.Create((x, y) => y.CompareTo(x)));

                for (int i = 1; i <= k; i++)
                {

                    int cnt = ReadInt();
                    pq.Enqueue((cnt, i), cnt);
                }

                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define fi first
// #define se second
// #define all(v) (v).begin(), (v).end()

using namespace std;
typedef long long ll;
typedef pair<ll,ll> pll;
typedef pair<int,int> pii;
typedef long double ld;

pii A[300005];
int B[300005];
int main()
{
    ios::sync_with_stdio(0); cin.tie(0);
    int n,k; cin>>n>>k;
    for(int i=1;i<=k;i++) cin>>A[i].fi, A[i].se=i;
    sort(A+1,A+1+k);
    if(A[k].fi>(n+1)/2) return cout<<"-1", 0;
    int j=k;
    for(auto x:{1,2}) for(int i=x;i<=n;i+=2)
    {
        B[i]=A[j].se;
        A[j].fi--; if(!A[j].fi) j--;
    }
    for(int i=1;i<=n;i++) cout<<B[i]<<' ';
    return 0;
}

#endif
}
