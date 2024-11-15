using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 15
이름 : 배성훈
내용 : 제271회 웰노운컵
    문제번호 : 16225번

    그리디, 정렬, 우선순위 큐 문제다.
    아이디어는 다음과 같다.
    2개를 어떻게 뽑아도 상대방은 B 점수가 높은걸 선택하기에
    결국에는 B의 점수가 가장 낮은건 내가 고르고, B 점수가 가장 높은건 상대방이 고른다.
    B 점수가 낮은 순서로 배치했을 때, 2개당 1개씩 강제적으로 선택 해야 된다.
    이는 10개에서 B점수가 작은 앞에서 4개를 내가 모두 선택하게 할 수 있다. 
    하지만 B점수가 낮은 5 ~ 8번째 모두를 내가 선택 불가능하다.

    그래서 앞에서 2개씩 택하면서 가장 높게 선택하는게 
    정답이 되지 않을까 추론했고 제출하니 통과했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1116
    {

        static void Main1116(string[] args)
        {

            int n;
            (int s1, int s2)[] scores;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(scores, (x, y) => x.s2.CompareTo(y.s2));

                PriorityQueue<int, int> pq = new(n, Comparer<int>.Create((x, y) => y.CompareTo(x)));
                long ret = scores[0].s1;

                for (int i = 1; i < n / 2; i++)
                {

                    pq.Enqueue(scores[i * 2 - 1].s1, scores[i * 2 - 1].s1);
                    pq.Enqueue(scores[i * 2].s1, scores[i * 2].s1);

                    ret += pq.Dequeue();
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                scores = new (int s1, int s2)[n];
                for (int i = 0; i < n; i++)
                {

                    scores[i].s1 = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    scores[i].s2 = ReadInt();
                }

                sr.Close();

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
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    vector<pair<int, int>> scores(n);
    for (int i = 0; i < n; i++) {
        cin >> scores[i].second;
    }
    for (int i = 0; i < n; i++) {
        cin >> scores[i].first;
    }

    sort(scores.begin(), scores.end());

    long long ans = 0;
    priority_queue<int> pq;

    ans += scores[0].second;

    for (int i = 1; i < n - 1; i += 2) {
        pq.push(scores[i].second);
        pq.push(scores[i+1].second);
        ans += pq.top();
        pq.pop();
    }

    cout << ans << '\n';

    return 0;
}
#endif
}
