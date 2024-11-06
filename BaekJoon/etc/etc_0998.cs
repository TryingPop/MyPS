using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 25
이름 : 배성훈
내용 : 게임
    문제번호 : 17431번

    수학, 정수론, 해 구성하기, 유클리드 호제법 문제다
    아이디어는 다음과 같다
    etc_0988 문제에서 정렬 조건만 빠진 문제다

    etc_0988을 그냥 하면 시간초과 날거 같아 탐색범위를 더 좁힐 수 없을까 고민했다
    브루트포스로 모든 길이를 확인했고... 탐색에 3시간 넘게 걸렸다; 
    (콘솔 띄우기, 메모장 저장 2번, 그냥 비교 탐색했다;)

    콘솔로 했는데, 왜인지 내용이 짤릴거 같아서 출력 중간에 멈췄다(다 찾고 출력부분;)
    메모장걸 그대로 옮기니 30만 정수라... visual studio에서 렉이 걸리길레 
    그냥 비교하는 형식으로 했다

    그러니 idx / 4 ~ idx / 2사이만 탐색해도 됨을 알았고
    더 좁은 범위가 있을거 같은데 시간이 너무 많이 걸려 안했다!
    이렇게 제출하니 460ms에 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0998
    {

        static void Main998(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] order;

            Solve();

            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                order = new int[40];
                for (int i = 0; i < n; i++)
                {

                    int find = ReadInt();
                    int len = 0;

                    int a = GetNum(find);
                    int b = find - a;

                    while (a != 1 || b != 1)
                    {

                        if (a > b)
                        {

                            a -= b;
                            order[len++] = 1;
                        }
                        else
                        {

                            b -= a;
                            order[len++] = 2;
                        }
                    }

                    for (int j = len - 1; j >= 0; j--)
                    {

                        if (order[j] == 1) sw.Write('B');
                        else sw.Write('R');
                    }

                    sw.Write('R');
                    sw.Write('\n');
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
            }

            int GetGCD(int _a, int _b, out int _q)
            {

                _q = 1;
                while (_b > 0)
                {

                    _q += _a / _b;
                    if (_b == 1) _q--;
                    int temp = _a % _b;

                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            int GetNum(int _idx)
            {

                int ret = -1;
                int max = _idx;
                int e = _idx >> 1;
                int s = Math.Max(e >> 1, 1);

                for (int i = s; i <= e; i++)
                {

                    int gcd = GetGCD(_idx - i, i, out int q);
                    if (gcd != 1) continue;

                    if (q < max) 
                    { 
                        
                        max = q;
                        ret = _idx - i;
                    }
                }

                return ret;
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
using namespace std;


void solve() {

	int n;
	cin >> n;

	int ans = 1;
	int ans_tot = n - 1;

	for (int i = 2; i <= n / 2; i++) {

		int a = n, b = i, tot = 0;
		// 마지막 상태

		bool ok = 1;
		while (b != 1) {

			if (a % b == 0) {
				ok = 0;
				break;
			}
			tot += a / b;
			a %= b;
			swap(a, b);
		}

		if (!ok) continue;
		tot += a - 1;

		if (tot < ans_tot) {
			ans_tot = tot;
			ans = i;
		}
	}


	int a = n, b = ans;
	vector<int> tmp;
	while (b != 1) {
		tmp.push_back(a / b);
		a %= b;
		swap(a, b);
	}
	tmp.push_back(a - 1);

	bool p = 0;
	for (int i = tmp.size() - 1; i >= 0; i--) {
		if (!p) cout << string(tmp[i], 'R');
		else cout << string(tmp[i], 'B');
		p ^= 1;
	}
	cout << '\n';


}
int main() {

	ios::sync_with_stdio(false);
	cin.tie(0);

	int t;
	cin >> t;
	while (t--) {
		solve();
	}

}
#endif
}
