using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 29
이름 : 배성훈
내용 : Monument Tour
    문제번호 : 17557번

    누적합, 스위핑, 삼분탐색 문제다
    처음에 거리의 최소값은 y의 중앙값을 찾으면 되겠네 생각했다
    중복 고려 X와, 세지 않아도 되는 y들을 세어 계속해서 틀렸다

    그래서 힌트를 보았고 삼분탐색이 있기에
    삼분탐색을 검색했다
    그러니 U, V형태의 그래프에 대해서
    쓸 수 있고, 이는 절댓값 함수들의 합이니 
    그래프를 그리면 U, V 형태이므로 쓸 수 있다
    그래서 삼분탐색으로 처음 통과했다

    다른 사람 풀이를 보니, 
    선분의 사이에 있는 y는 무시해도 되는 조건이 없어 
    중앙값으로 안풀리는 것이었다

    이후 해당 조건을 추가하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0921
    {

        static void Main921(string[] args)
        {
#if first
            StreamReader sr;
            int X, Y, n;
            (int x, int y)[] pos;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            long GetGas(int _y)
            {

                long ret = X - 1;
                int x = -1;

                int min = _y, max = _y;
                for (int i = 0; i < n; i++)
                {

                    if (x != pos[i].x)
                    {

                        if (min < _y) ret += 2 * (_y - min);
                        if (_y < max) ret += 2 * (max - _y);

                        x = pos[i].x;
                        min = pos[i].y;
                        max = pos[i].y;
                    }
                    else max = pos[i].y;
                }

                if (min < _y) ret += 2 * (_y - min);
                if (_y < max) ret += 2 * (max - _y);

                return ret;
            }

            void GetRet()
            {


                Array.Sort(pos, (x, y) =>
                {

                    int ret = x.x.CompareTo(y.x);
                    if (ret == 0) return x.y.CompareTo(y.y);
                    return ret;
                });

                long ret = Search();
                Console.Write(ret);
            }

            long Search()
            {

                int l = 0;
                int r = n;
                while (r - l >= 3)
                {

                    int p = (l * 2 + r) / 3;
                    int q = (l + r * 2) / 3;

                    if (GetGas(p) <= GetGas(q)) r = q;
                    else l = p;
                }

                long ret = 1_000_000_000_000_000;
                for (int i = l; i <= r; i++)
                {

                    ret = Math.Min(ret, GetGas(i));
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                X = ReadInt();
                Y = ReadInt();

                n = ReadInt();
                pos = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
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
#else

            StreamReader sr;
            int X, Y, n;
            int[] sMin, sMax;


            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                List<int> ys = new(n);

                for (int i = 0; i < X; i++)
                {

                    if (sMax[i] == -1) continue;
                    ys.Add(sMin[i]);
                    ys.Add(sMax[i]);
                }

                ys.Sort();

                int y = ys[ys.Count / 2];

                long ret = X - 1;

                for (int i = 0; i < X; i++)
                {

                    if (sMax[i] == -1) continue;
                    int min = Math.Min(sMin[i], y);
                    int max = Math.Max(sMax[i], y);

                    ret += 2 * (max - min);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                X = ReadInt();
                Y = ReadInt();
                n = ReadInt();

                sMin = new int[X];
                sMax = new int[X];

                Array.Fill(sMin, 100_001);
                Array.Fill(sMax, -1);

                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    sMin[x] = Math.Min(sMin[x], y);
                    sMax[x] = Math.Max(sMax[x], y);
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
#endif
        }
    }

#if other
// #include <bits/stdc++.h>
// #define fi first
// #define se second
// #define eb emplace_back
// #define all(v) (v).begin(), (v).end()
// #define rmin(r, x) r = min(r, x)
// #define rmax(r, x) r = max(r, x)
// #define ends ' '
// #define endl '\n'
// #define fastio ios_base::sync_with_stdio(0), cin.tie(0)
using namespace std;
typedef long long ll;
 
const int maxx = 1e5 + 10;
 
int X, Y, N, A[maxx], B[maxx];
vector<int> V;
 
int main() {
	fastio;
	memset(A, 0x3f, sizeof(A));
	memset(B, -1, sizeof(B));
	cin >> X >> Y >> N;

    for (int i = 1; i <= N; i++) 
    {
	
        int x, y;	cin >> x >> y;
		A[x] = min(A[x], y);
		B[x] = max(B[x], y);
	}

    for (int i = 0; i <= maxx - 1; i++)
    {

        if(A[i] != -1) 
        {
		
            V.push_back(A[i]);
		    V.push_back(B[i]);
	    }
    }
	
	sort(all(V));
	int mid = V[V.size() / 2];
	ll ans = 0;

    for (int i = 0; i <= maxx - 1; i++)
    {
        
        if(A[i] != -1) 
        {
		
            A[i] = min(A[i], mid);
		    B[i] = max(B[i], mid);
		    ans += 2 * (B[i] - A[i]);
	    }
    }
	cout << X - 1 + ans;
	return 0;
}
#endif
}
