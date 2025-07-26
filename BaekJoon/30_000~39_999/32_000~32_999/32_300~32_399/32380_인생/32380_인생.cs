using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 11
이름 : 배성훈
내용 : 인생
    문제번호 : 32380번

    그리디, 누적합 문제다
    그리디와 슬라이딩 윈도우 형식으로 풀었다

    아이디어는 다음과 같다
    만약 현재 A를 택하면 A + U * K + C,
    여기서 K는 뒤에 남은 선택 횟수이고, C는 최소점수라 생각하자

    만약 현재 B를 택하면 B - D * K + C,
    K는 앞과 같다 C 역시 교환, 결합 법칙을 잘 분배하면 앞과 같음을 알 수 있다

    그래서 A가 당장은 좋지만
    계속 진행하면 어느순간 B로 바꾸는게 좋은 시점이 항상 존재한다 

    연산으로 진행하면 (A[i] - B[i]) / (U + D) <= k인 
    음이 아닌 정수 k의 최소값이 된다

    해당 시점을 priorityqueue에 저장하고
    해당 시점이 오면 바꿔주는연산을 했다

    다만 영향을 주는 개수를 잘못 파악해 N번 이 아닌 
    N x (N - 1) / 2인줄 알아 한번 틀렸다
    이렇게 제출하니 이상없이 통과한다

    다른 사람의 풀이를 보니 누적값을 저장하고
    우선순위 큐가 아닌 배열에 모아서 진행했다
*/

namespace BaekJoon.etc
{
    internal class etc_1047
    {

        static void Main1047(string[] args)
        {

            int[] A, B;
            int U, D;
            int u, d, n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                PriorityQueue<(int turn, int idx), int> pq = new(n);

                u = 0;
                d = 0;

                long sum = 0;
                for (int i = 0; i < n; i++)
                {

                    ChangeUD(i);

                    if (A[i] < B[i])
                    {

                        int k = 1 + (B[i] - A[i] - 1) / (U + D);
                        pq.Enqueue((i + k, i), i + k);
                        sum = sum + A[i] + 1L * u * U - 1L * d * D;
                        u++;
                    }
                    else
                    {

                        sum = sum + B[i] + 1L * u * U - 1L * d * D;
                        d++;
                    }

                    sw.Write($"{sum}\n");
                }

                sw.Close();

                void ChangeUD(int _turn)
                {

                    while (pq.Count > 0 && pq.Peek().turn <= _turn)
                    {

                        int idx = pq.Dequeue().idx;
                        long cnt = _turn - idx - 1;

                        sum = sum - cnt * (D + U) - A[idx] + B[idx];
                        u--;
                        d++;
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                U = ReadInt();
                D = ReadInt();

                A = new int[n];
                B = new int[n];

                for (int i = 0; i < n; i++)
                {

                    A[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    B[i] = ReadInt();
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
typedef long long ll;

int main()
{
	cin.tie(0)->sync_with_stdio(0);
	int n, u, d; cin >> n >> u >> d;
	vector<int> a(n), b(n), t(n);
	for (auto &x : a) cin >> x;
	for (auto &x : b) cin >> x;

	vector<ll> c(n);
	ll ans = 0, uu = 0, dd = 0;
	for (int i = 0; i < n; i++) {
		if (t[i]) {
			ans += c[i];
			uu -= t[i];
			dd += t[i];
		}
		if (a[i] < b[i]) {
			ans += a[i] + uu * u - dd * d;
			uu++;
			int k = (b[i] - a[i]) / (u + d) + 1;
			if (i + k < n) {
				c[i + k] += b[i] - a[i] - (u + d) * (k - 1);
				t[i + k]++;
			}
		}
		else {
			ans += b[i] + uu * u - dd * d;
			dd++;
		}
		cout << ans << '\n';
	}
}
#endif
}
