using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 13
이름 : 배성훈
내용 : 홍삼 게임 (Easy, Hard)
    문제번호 : 14714번, 14705번 

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1700
    {

        static void Main1700(string[] args)
        {

            int n, a, b, da, db;

            Input();

            GetRet();

            void GetRet()
            {

                int NOT_VISIT = -2;
                int INF = 1_234_567;
                Queue<int> q = new(n), next = new(n);
                // visit?[idx1][idx2]
                // idx1 : 두 사람이 번갈아 한 턴이 홀짝인지 확인
                // idx2 : 사람 번호
                // idx1을 홀짝으로 나누는 이유는 2턴마다 해당 위치로 돌아올 수 있기 때문이다.
                int[][] visita = new int[2][], visitb = new int[2][];
                for (int i = 0; i < 2; i++)
                {

                    visita[i] = new int[n];
                    visitb[i] = new int[n];

                    Array.Fill(visita[i], NOT_VISIT);
                    Array.Fill(visitb[i], NOT_VISIT);
                }

                BFS(visita, a, da, 1);
                BFS(visitb, b, db, 2);

                int ret = GetMin();
                if (ret == INF) Console.Write("Evil Galazy");
                else Console.Write(ret);

                int GetMin()
                {

                    int ret = INF;
                    for (int i = 0; i < n; i++)
                    {

                        for (int idx = 0; idx < 2; idx++)
                        {

                            // A에서 만나는 최솟값 찾기
                            int ca = visita[idx][i];
                            int cb = visitb[idx ^ 1][i];

                            if (ca == NOT_VISIT || cb == NOT_VISIT) continue;

                            if (ca > cb) ret = Math.Min(ca, ret);
                            else ret = Math.Min(cb + 1, ret);
                        }
                    }

                    for (int i = 0; i < n; i++)
                    {

                        for (int idx = 0; idx < 2; idx++)
                        {

                            // B에서 만나는 최솟값 찾기
                            int cb = visitb[idx][i];
                            int ca = visita[idx][i];

                            if (ca == NOT_VISIT || cb == NOT_VISIT) continue;

                            if (cb > ca) ret = Math.Min(cb, ret);
                            else ret = Math.Min(ca + 1, ret);
                        }
                    }

                    return ret;
                }

                void BFS(int[][] _visit, int _start, int _dis, int _turn)
                {

                    // 시작 턴
                    int turn = _turn;
                    q.Enqueue(_start);
                    // 처음 위치 보정
                    _visit[0][_start] = _turn - 2;

                    int idx = 0;
                    while (q.Count > 0)
                    {

                        // 홀짝 확인
                        idx ^= 1;
                        while (q.Count > 0)
                        {

                            int node = q.Dequeue();

                            // 왼쪽과 오른쪽 이동
                            int left = GetLeft(node);
                            int right = GetRight(node);

                            TryNext(left);
                            TryNext(right);
                        }

                        var temp = q;
                        q = next;
                        next = temp;

                        turn += 2;
                    }

                    void TryNext(int _val)
                    {

                        // 이미 방문한 경우 확인
                        if (_visit[idx][_val] != NOT_VISIT) return;

                        // 방문 안했으면 다음 큐에 넣는다.
                        _visit[idx][_val] = turn;
                        next.Enqueue(_val);
                    }

                    int GetLeft(int _val)
                    {

                        // 왼쪽 경우로 이동
                        int ret = _val - _dis;
                        if (ret < 0) ret += n;
                        return ret;
                    }

                    int GetRight(int _val)
                    {

                        // 오른쪽 경우로 이동
                        int ret = _val + _dis;
                        if (n <= ret) ret -= n;
                        return ret;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                a = int.Parse(temp[1]) - 1;
                b = int.Parse(temp[2]) - 1;
                da = int.Parse(temp[3]);
                db = int.Parse(temp[4]);
            }
        }
    }
#if other
// #include <bits/stdc++.h>
using namespace std;

const int MAXN = 500010;

bool dp[MAXN][2][2];

int main() {
	ios::sync_with_stdio(0); cin.tie(0);
	int N, A, B, DA, DB;
	cin >> N >> A >> B >> DA >> DB;

	if(A == B) {
		cout << "0\n";
		return 0;
	}

	A %= N;
	B %= N;
	int la = A, ra = A;
	int lb = B, rb = B;
	dp[A][0][0] = dp[B][1][0] = true;
	for(int i = 1; i < N + 10; i++) {
		la = (la - DA + N) % N;
		ra = (ra + DA) % N;
		dp[la][0][i % 2] = true;
		dp[ra][0][i % 2] = true;
		if(dp[la][1][1 - i % 2] || dp[ra][1][1 - i % 2]) {
			cout << (i * 2 - 1) << "\n";
			return 0;
		}
		if(dp[la][1][i % 2] || dp[ra][1][i % 2]) {
			cout << (i * 2) << "\n";
			return 0;
		}

		lb = (lb - DB + N) % N;
		rb = (rb + DB) % N;
		dp[lb][1][i % 2] = true;
		dp[rb][1][i % 2] = true;
		if(dp[lb][0][i % 2] || dp[rb][0][i % 2]) {
			cout << (i * 2) << "\n";
			return 0;
		}
		if(dp[lb][0][1 - i % 2] || dp[rb][0][1 - i % 2]) {
			cout << (i * 2 + 1) << "\n";
			return 0;
		}
	}
	cout << "Evil Galazy\n";
	return 0;
}

#endif
}
