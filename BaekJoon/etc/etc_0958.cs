using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 9
이름 : 배성훈
내용 : 좀비 떼가 기관총 진지에도 오다니
    문제번호 : 19644번

    그리디, 누적합, 큐 문제다
    그리디로 먼저 기관총으로 해결가능하면 기관총으로 해결한다
    기관총으로 해결 안되는 경우 폭탄으로 시도한다
    폭탄이 없는 경우 실패가된다

    그래서 큐에 기관총 쏜 경우를 기억하고,
    데미지를 계산해 기관총으로 죽일 수 있는지 판별했다
    이렇게 시뮬레이션 돌려 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0958
    {

        static void Main958(string[] args)
        {

            string YES = "YES";
            string NO = "NO";

            StreamReader sr;

            int l, ml, mk, c;
            Queue<int> q;

            Solve();
            void Solve()
            {

                Input();

                int dmg = 0;
                bool ret = true;
                for (int i = 0; i < l; i++)
                {

                    int hp = ReadInt();

                    if (q.Count == ml)
                        dmg -= q.Dequeue();

                    if (hp <= dmg + mk)
                    {

                        q.Enqueue(mk);
                        dmg += mk;
                        continue;
                    }

                    if (c == 0)
                    {

                        ret = false;
                        break;
                    }

                    c--;

                    q.Enqueue(0);
                }

                Console.Write(ret ? YES : NO);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                l = ReadInt();
                ml = ReadInt();
                mk = ReadInt();
                c = ReadInt();
                q = new(ml);
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
// #pragma GCC optimize("O3")
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")
// #pragma GCC target("avx,avx2")
// #include<stdio.h>
// #include<algorithm>
// #include<vector>
// #include<unordered_map>
// #include<math.h>
// #include<map>
// #include<time.h>
// #include<queue>
// #include<assert.h>
using namespace std;
// #define M 1000000007
// #define E (1e-9)
using lld = long long int;
using pll = pair<long long int, long long int>;
using pii = pair<int, int>;
using pil = pair<int, long long int>;
using pli = pair<long long int, int>;
using piii = pair<pii, int>;
using lf = double;
using pff = pair<lf, lf>;
class FastIn {
public:
	const static int BUFFER_MAX = 1048576;
	static char buf[BUFFER_MAX + 1];
	static int pos;
	FastIn() {
		clear();
	}
	static void clear() {
		pos = BUFFER_MAX;
	}
	static void read_buffer() {
		fread(buf, sizeof(char), BUFFER_MAX, stdin);
	}
	static char getchar() {
		if (pos == BUFFER_MAX) {
			read_buffer();
			pos = 0;
		}
		return buf[pos++];
	}
	static int nextint() {
		int res = 0;
		char c;
		do {
			c = getchar();
		} while (c < '0' || c>'9');
		res = c - '0';
		while (1) {
			c = getchar();
			if (c < '0' || c>'9')break;
			res = (res * 10) + c - '0';
		}
		return res;
	}
};
char FastIn::buf[FastIn::BUFFER_MAX + 1];
int FastIn::pos;
int n, m, p, q;
int a[3000009];
int mark[3000009];
int main() {
	int i, j, k, l;
	int t = 1, tv = 0;
	while (t--) {
		n = FastIn::nextint();
		p = FastIn::nextint();
		q = FastIn::nextint();
		m = FastIn::nextint();
		for (i = 0; i < n; i++) {
			a[i] = FastIn::nextint();
		}
		bool valid = true;
		int deal = 0;
		for (i = 0; i < n - m; i++) {
			if (mark[i])deal -= q;
			if (a[i] - deal - q > 0) {
				if (m == 0) {
					valid = false;
					break;
				}
				m--;
			}
			else {
				if (i + p < n) {
					mark[i + p] = 1;
				}
				deal += q;
			}
		}
		printf("%s\n", valid ? "YES" : "NO");
	}
}
#endif
}
