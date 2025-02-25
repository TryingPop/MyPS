using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 1 
이름 : 배성훈
내용 : 지폐가 넘쳐흘러
    문제번호 : 17422번

    dp, 트리 문제다.
    아이디어는 다음과 같다.
    먼저 트리를 옮겨 금고의 최대값을 찾는 것은
    그리디로 리프에서 리프로 가는게 최대값임이 보장된다.

    실제로 그래프를 어떻게 변형해도 
    연결성된 갯수는 같으므로 리프는 리프거나 루트임이 보장된다.
    
    이에 리프에서 리프로 가는 최대값을 찾아 주면 된다.
    LCA 아이디어처럼 해당 공통 조상에 대해 비교하면서 찾는다.
    그리고 해당 자식 중 최대가 되는 루트는 자주 사용되므로 dp에 저장한다.
    이렇게 최대값을 찾아간다.
*/

namespace BaekJoon.etc
{
    internal class etc_1307
    {

        static void Main1307(string[] args)
        {

            StreamReader sr;

            int n;

            int[] val;
            // arr1 : 해당 i번째 노드를 부모로하면서
            //      리프로 가는 경로 중 가장 큰 합을 저장
            // arr2 : 해당 i번째 노드를 지나면서
            //      최대 값을 저장
            long[] arr1, arr2;

            // pop은 빠지는 루트 합
            PriorityQueue<long, long> ret, pop;

            Solve();
            void Solve()
            {

                Input();

                SetMax();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret.Peek()}\n");

                int m = ReadInt();
                while (m-- > 0)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    val[f] = t;
                    while (f != 0)
                    {

                        int l = f << 1;
                        int r = l + 1;

                        pop.Enqueue(arr2[f], -arr2[f]);
                        arr1[f] = val[f] + Math.Max(arr1[l], arr1[r]);
                        arr2[f] = val[f] + arr1[l] + arr1[r];

                        ret.Enqueue(arr2[f], -arr2[f]);
                        f >>= 1;
                    }

                    while (pop.Count > 0 && pop.Peek() == ret.Peek())
                    {

                        pop.Dequeue();
                        ret.Dequeue();
                    }

                    sw.Write($"{ret.Peek()}\n");
                }

                sr.Close();
                sw.Close();
            }

            void SetMax()
            {

                arr1 = new long[(n << 1) + 2];
                arr2 = new long[(n << 1) + 2];

                ret = new(n * 20);
                pop = new(n * 20);
                for (int i = n; i > 0; i--)
                {

                    int l = i << 1;
                    int r = l + 1;

                    arr1[i] = Math.Max(arr1[l], arr1[r]) + val[i];
                    arr2[i] = val[i] + arr1[l] + arr1[r];

                    ret.Enqueue(arr2[i], -arr2[i]);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                val = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    val[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using uint = unsigned int;
using pii = pair<uint, uint>;

const int N = 262144;

pii dp[N];
uint w[N];
int n;
char buf[N * 10 + 17000100];

inline uint ri()
{
	static int i = 0;
	uint x = buf[i++] - '0';
	while (buf[i] >= '0')
		x = x * 10 + buf[i++] - '0';
	return ++i, x;
}

char buf2[11000100];
int ii;

inline void wi(uint x)
{
	char s[11];
	int i = 0;
	do {
		s[i++] = x % 10 + '0';
		x /= 10;
	} while (x);
	for (--i; i >= 0; i--)
		buf2[ii++] = s[i];
	buf2[ii++] = ' ';
}

inline void upd(int i)
{
	if (i > n / 2)
		dp[i] = { w[i], w[i] };
	else
		dp[i] = { w[i] + max(dp[i * 2].first, dp[i * 2 + 1].first), max({dp[i * 2].second, dp[i * 2 + 1].second, dp[i * 2].first + dp[i * 2 + 1].first + w[i]}) };
}

int main()
{
	fread(buf, 1, sizeof buf, stdin);
	int i;
	n = ri();
	for (i = 1; i <= n; i++)
		w[i] = ri();
	for (i = n; i >= 1; i--)
		upd(i);
	int q = ri();
	wi(dp[1].second);
	while (q--)
	{
		uint x, y, mx = 0;
		x = ri();
		y = ri();
		w[x] = y;
		for (i = x; i >= 1; i >>= 1)
			upd(i);
		wi(dp[1].second);
	}
	fwrite(buf2, 1, ii, stdout);
}
#endif
}
