using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 1
이름 : 배성훈
내용 : 같이 눈사람 만들래?
    문제번호 : 20366번

    정렬, 두 포인터 문제다
    범위 문제, 두 포인터 조건 문제로 2번 틀렸다
    다른 사람의 풀이를 보니 n의 크기가 600밖에 안되므로
    합들의 정렬을 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_1012
    {

        static void Main1012(string[] args)
        {

            int MAX = 2_000_000_000;
            StreamReader sr;
            int[] arr;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr);

                int ret = MAX;

                for (int i = 0; i < n; i++)
                {

                    for (int j = i + 1; j < n; j++)
                    {

                        ret = Math.Min(ret, GetMinDiff(i, j));
                    }
                }

                Console.Write(ret);
            }

            int GetMinDiff(int _l, int _r)
            {

                int val = arr[_l] + arr[_r];

                int ret = MAX;
                int l = _l + 1;
                int r = _r - 1;

                while (l < r)
                {

                    int chk = arr[l] + arr[r] - val;
                    
                    if (chk <= 0)
                    {

                        l++;
                        ret = Math.Min(ret, -chk);
                    }
                    else
                    {

                        r--;
                        ret = Math.Min(ret, chk);
                    }
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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

#if other
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

using tii = tuple<int, short, short>;

void radix_sort(vector<tii>& v) {
	const unsigned int SZ = 8;
    const unsigned int mask = (1 << SZ) - 1;
	static queue<tii> Q[1 << SZ];
    for (int k = 0; k < 4; k++) {
	    for (const auto& i : v) Q[(unsigned)get<0>(i) >> k * SZ & mask].push(i);
	    v.clear();
	    for (auto& Q : Q) while (Q.size()) v.push_back(Q.front()), Q.pop();
    }
}

int main() {
	fastio;
	int n, mn = 1e9; cin >> n;
	vector<int> v(n);
    vector<tii> w; w.reserve(n * (n + 1) >> 1);
	for (int i = 0; i < n; i++) {
		cin >> v[i];
		for (int j = 0; j < i; j++) w.push_back({ v[i] + v[j], i, j });
	}

    radix_sort(w);
	for (int i = 1; i < w.size(); i++) {
		const auto& [a, i1, j1] = w[i - 1];
		const auto& [b, i2, j2] = w[i];
		if (mn <= b - a || i1 == i2 || i1 == j2 || j1 == i2 || j1 == j2) continue;
		mn = min(mn, b - a);
	}
	cout << mn << '\n';
}
#endif
}
