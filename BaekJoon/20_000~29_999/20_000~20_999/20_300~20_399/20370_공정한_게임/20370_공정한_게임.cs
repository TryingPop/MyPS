using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 15
이름 : 배성훈
내용 : 공정한 게임
    문제번호 : 20370번

    그리디, 우선순위 큐 문제다.
    아이디어는 다음과 같다.
    우선 B는 가장 점수가 높은것들만 뽑으면 된다.
    A의 경우 상대가 높은거를 빼앗을지 혹은 자기의 높은걸 택할지 선택하면 된다.
    먼저 A가 B의 높은걸 빼앗는게 0개인 경우는 이상없다.
    반면 A가 B의 높은걸 빼앗는 경우에는 그리디로 자신의 점수도 높이고, 상대 점수도 낮추는
    A_i + B_i가 높은걸 택하는게 좋다.

    그래서 1개씩 빼앗아 가는 개수를 추가하면서 0 ~ k개 빼앗을 때 상대가 가질 수 있는 최대 점수를 찾는다.
    이후에 0 ~ 2k개 높은걸 택했으니, 나머지 중 1개씩 높은걸 택하면서 정답을 찾아가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1111
    {

        static void Main1111(string[] args)
        {

            int n, k;
            (int t1, int t2)[] scores;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {


                PriorityQueue<int, int> pq = new(k + 1, Comparer<int>.Create((x, y) => y.CompareTo(x)));

                // 상대방껄 k개 빼앗을 때 상대의 최대점수를 max[k]에 담는다.
                // k = 0일 때는 상대가 최고점수를 모두 가져간다.
                long[] max = new long[k + 1];
                long cur = 0;

                Array.Sort(scores, (x, y) => y.t2.CompareTo(x.t2));
                for (int i = 0; i < k; i++)
                {

                    cur -= scores[i].t2;
                    int add = scores[i].t1 + scores[i].t2;
                    pq.Enqueue(add, add);
                }

                max[0] = cur;
                for (int i = 0; i < k; i++)
                {

                    int add = scores[i + k].t1 + scores[i + k].t2;
                    pq.Enqueue(add, add);
                    cur -= scores[i + k].t2;
                    cur += pq.Dequeue();
                    max[i + 1] = cur;
                }

                // 이제 A가 가장 큰 점수를 1개씩 택하면서
                // 상대방껄 k - i개 남은 최대점수와 비교한다.
                cur = 0;
                long ret = max[k];

                pq.Clear();
                for (int i = 2 * k; i < n; i++) 
                { 
                    
                    pq.Enqueue(scores[i].t1, scores[i].t1);
                }

                for (int i = 1; i <= k; i++)
                {

                    // A가 i개 자기꺼 큰걸 뽑는다.
                    pq.Enqueue(scores[2 * k - i].t1, scores[2 * k - i].t1);
                    cur += pq.Dequeue();
                    ret = Math.Max(ret, max[k - i] + cur);
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                scores = new (int t1, int t2)[n];

                for (int i = 0; i < n; i++)
                {

                    scores[i].t1 = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    scores[i].t2 = ReadInt();
                }

                sr.Close();

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
using namespace std;

// #define ll long long

int N, K;
pair<int, int> BA[80000];
ll cand[40001];

int main() {
    cin.tie(0)->sync_with_stdio(0);
    cin >> N >> K;
    for (int n = 0; n < N; n++) cin >> BA[n].second;
    for (int n = 0; n < N; n++) { cin >> BA[n].first; BA[n].first *= -1; }
    sort(BA, BA+N);
    
    ll S = 0;
    priority_queue<int> q;
    for (int k = 0; k < K; k++) { S += BA[k].first; q.push(-BA[k].first + BA[k].second); }
    cand[0] = S;
    for (int k = 0; k < K; k++) {
        S += BA[K+k].first;
        q.push(-BA[K+k].first + BA[K+k].second);
        S += q.top();
        q.pop();
        cand[k+1] = S;
    }
    
    S = 0;
    q = {};
    for (int k = 2*K; k < N; k++) q.push(BA[k].second);
    for (int k = 1; k <= K; k++) {
        q.push(BA[2*K-k].second);
        S += q.top();
        q.pop();
        cand[K-k] += S;
    }
    
    ll ans = -1e18;
    for (int k = 0; k <= K; k++) ans = max(ans, cand[k]);
    cout << ans;
}

#endif
}
