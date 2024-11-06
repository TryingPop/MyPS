using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 3
이름 : 배성훈
내용 : 멈뭄미믜 저주 탈출
    문제번호 : 31536번

    그리디, 브루트포스, 기하학, 스위핑 문제다
    그리디하게 풀었다

    아이디어는 다음과 같다
    영역이 disjoint 하므로 영역의 배치 형태만 따졌다
    믐 형태나 ㅁ|ㅁ 형태 두개로 모든 경우를 나눌 수 있다

    믐 형태인 경우 위에 사각형은 밑변들, 밑에 사각형은 윗변에 가까운 점들만 확인했다
    ㅁ|ㅁ은 왼쪽 사각형은 오른쪽 변에 가까운 점들, 오른쪽 사각형은 왼쪽 변에 가까운 점들만 확인했다
    이렇게 모든 경우를 구하니, 232ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0676
    {

        static void Main676(string[] args)
        {

            StreamReader sr;
            (int r, int c, int l) m;
            (int r, int c, int l) k;
            int[] arr1;
            int[] arr2;

            bool[] con1;
            bool[] con2;

            int type;
            Solve();

            void Solve()
            {

                Input();

                long ret = 1_000_000_000_000_000;
                int idx1 = -1;
                int idx2 = -1;

                for (int i = 0; i <= m.l; i++)
                {

                    if (!con1[i]) continue;

                    for (int j = 0; j <= k.l; j++)
                    {

                        if (!con2[j]) continue;
                        long chk = GetDis(i, j);
                        if (chk < ret)
                        {

                            ret = chk;
                            idx1 = i;
                            idx2 = j;
                        }
                    }
                }

                Console.Write($"{ret}\n");
                if (type < 3) Console.Write($"{arr1[idx1] + m.r} {idx1 + m.c}\n{arr2[idx2] + k.r} {idx2 + k.c}");
                else Console.Write($"{idx1 + m.r} {arr1[idx1] + m.c}\n{idx2 + k.r} {arr2[idx2] + k.c}");
            }

            long GetDis(int _idx1, int _idx2)
            {

                long calc1, calc2;
                if (type == 1 || type == 2)
                {

                    calc1 = (_idx1 + m.c) - (_idx2 + k.c);
                    calc1 *= calc1;

                    calc2 = (arr1[_idx1] + m.r) - (arr2[_idx2] + k.r);
                    calc2 *= calc2;
                }
                else
                {

                    calc1 = (_idx1 + m.r) - (_idx2 + k.r);
                    calc1 *= calc1;

                    calc2 = (arr1[_idx1] + m.c) - (arr2[_idx2] + k.c);
                    calc2 *= calc2;
                }

                return calc1 + calc2;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                m = (ReadInt(), ReadInt(), ReadInt());
                k = (ReadInt(), ReadInt(), ReadInt());

                arr1 = new int[m.l + 1];
                arr2 = new int[k.l + 1];

                con1 = new bool[m.l + 1];
                con2 = new bool[k.l + 1];
                type = GetType();

                SetArr();

                sr.Close();
            }

            void SetArr()
            {

                int len1 = ReadInt();
                int len2 = ReadInt();
                
                for (int i = 0; i < len1; i++)
                {

                    int r = ReadInt();
                    int c = ReadInt();

                    r = r - m.r;
                    c = c - m.c;
                    switch (type)
                    {

                        case 1:

                            arr1[c] = arr1[c] < r ? r : arr1[c];
                            con1[c] = true;
                            break;

                        case 2:

                            arr1[c] = r < arr1[c] ? r : arr1[c];
                            con1[c] = true;
                            break;

                        case 3:

                            arr1[r] = arr1[r] < c ? c : arr1[c];
                            con1[r] = true;
                            break;

                        default:

                            arr1[r] = c < arr1[r] ? c : arr1[r];
                            con1[r] = true;
                            break;
                    }
                }

                for (int i = 0; i < len2; i++)
                {

                    int r = ReadInt();
                    int c = ReadInt();

                    r = r - k.r;
                    c = c - k.c;
                    switch (type)
                    {

                        case 1:

                            arr2[c] = r < arr2[c] ? r : arr2[c];
                            con2[c] = true;
                            break;

                        case 2:

                            arr2[c] = arr2[c] < r ? r : arr2[c];
                            con2[c] = true;
                            break;

                        case 3:

                            arr2[r] = c < arr2[r] ? c : arr2[r];
                            con2[r] = true;
                            break;

                        default:

                            arr2[r] = arr2[r] < c ? c : arr2[r];
                            con2[r] = true;
                            break;
                    }
                }
            }

            int GetType()
            {

                if (m.r + m.l < k.r)
                {

                    Array.Fill(arr1, -1_000_000);
                    Array.Fill(arr2, 1_000_000);

                    return 1;
                }
                
                if (k.r + k.l < m.r)
                {

                    Array.Fill(arr1, 1_000_000);
                    Array.Fill(arr2, -1_000_000);

                    return 2;
                }

                if (m.c + m.l < k.c)
                {

                    Array.Fill(arr1, -1_000_000);
                    Array.Fill(arr2, 1_000_000);

                    return 3;
                }


                Array.Fill(arr1, 1_000_000);
                Array.Fill(arr2, -1_000_000);

                return 4;
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

struct Box
{
    int x, y, len;
}box[2];

// #define lli long long int
// #define plli pair<lli,lli>

const lli MAX = 21e8 * 21e7;
lli ans = MAX, pos[1005][2], cnt[2];
plli point[2];

int min_max(lli a, lli b, int s)
{
    if(a == MAX) return b;
    if(s) return max(a,b);
    return min(a,b);
}

long long int dis(plli p1, plli p2)
{
    if(p1.second == MAX || p2.second == MAX) return MAX;
    return (long long int)(p1.first-p2.first) * (p1.first-p2.first) + (long long int)(p1.second-p2.second) * (p1.second - p2.second);
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL); cout.tie(NULL);

    int x, y, pl[2], t = 0;
    for(int i=0; i<2; i++) cin >> box[i].x >> box[i].y >> box[i].len;
    cin >> cnt[0] >> cnt[1];
    for(int i=0;i<2; i++){
        for(int j=0; j<=1001; j++) pos[j][i] = MAX;
    }
    if(box[0].x + box[0].len < box[1].x || box[1].x + box[1].len < box[0].x){ ///ㅁㅣㅁ 형태
        int s = box[0].x + box[0].len < box[1].x; /// 0번째가 왼쪽
        t = 1;
        for(int i=0; i<2; i++){
            for(int j=1; j<=cnt[i]; j++){
                cin >> x >> y;
                pos[y-box[i].y][i] = min_max(pos[y-box[i].y][i], x, s);
            }
            s = !s;
            pl[i] = box[i].y;
        }
    }
    else{ /// 믐 형태
        int s = box[0].y + box[0].len < box[1].y; /// 0번째가 아랫쪽
        for(int i=0; i<2; i++){
            for(int j=1; j<=cnt[i]; j++){
                cin >> x >> y;
                pos[x-box[i].x][i] = min_max(pos[x-box[i].x][i], y, s);
            }
            s = !s;
            pl[i] = box[i].x;
        }
    }
    for(int i=0; i<=1000; i++){
        for(int j=0; j<=1000; j++){
            long long int d = dis(plli(i+pl[0], pos[i][0]), plli(j+pl[1], pos[j][1]));
            if(ans > d){
                ans = d;
                point[0].first = i+pl[0], point[0].second = pos[i][0];
                point[1].first = j+pl[1], point[1].second = pos[j][1];
            }
        }
    }
    cout <<ans << "\n";
    for(int i=0; i<2; i++){
        if(t) swap(point[i].first, point[i].second);
        cout <<point[i].first << " " << point[i].second <<"\n";
    }
}

#elif other2
import sys
input = sys.stdin.readline
INF = int(1e9)

Mx, My, A = map(int,input().split()) ; Kx, Ky, B = map(int,input().split())

M, K = map(int,input().split()) ; D1 = [[INF] * (A+1) for _ in range(4)] ; D2 = [[INF] * (B+1) for _ in range(4)]

for i in [1, 3] :
    for j in range(A+1) : D1[i][j] = -INF
    for j in range(B+1) : D2[i][j] = -INF

for _ in range(M) :
    x, y = map(int,input().split())

    if D1[0][x-Mx] > y : D1[0][x-Mx] = y
    if D1[1][x-Mx] < y : D1[1][x-Mx] = y
    if D1[2][y-My] > x : D1[2][y-My] = x
    if D1[3][y-My] < x : D1[3][y-My] = x

for _ in range(K) :
    x, y = map(int,input().split())
    if D2[0][x-Kx] > y : D2[0][x-Kx] = y
    if D2[1][x-Kx] < y : D2[1][x-Kx] = y
    if D2[2][y-Ky] > x : D2[2][y-Ky] = x
    if D2[3][y-Ky] < x : D2[3][y-Ky] = x

C, D = [], [] ; Ans = INF**2

for j in range(A+1) :
    if D1[0][j] != INF : C.append((j+Mx, D1[0][j]))
    if D1[1][j] != -INF : C.append((j+Mx, D1[1][j]))
    if D1[2][j] != INF : C.append((D1[2][j], j+My))
    if D1[3][j] != -INF : C.append((D1[3][j], j+My))

for j in range(B+1) :
    if D2[0][j] != INF : D.append((j+Kx, D2[0][j]))
    if D2[1][j] != -INF : D.append((j+Kx, D2[1][j]))
    if D2[2][j] != INF : D.append((D2[2][j], j+Ky))
    if D2[3][j] != -INF : D.append((D2[3][j], j+Ky))

p, q, r, s = 0, 0, 0, 0
for a, b in C :
    for c, d in D :
        e = (a-c)**2+(b-d)**2
        if Ans > e : Ans = e ; p, q, r, s = a, b, c, d

print(Ans) ; print(p, q) ; print(r, s)
#elif other3
// #include <iostream>
// #include <vector>
// #include <algorithm>

using namespace std;

bool compareSecond(const pair<int, int>& a, const pair<int, int>& b) {
	if (a.second != b.second) {
		return a.second < b.second;
	}
	return a.first < b.first;
}


int main()
{
	ios::sync_with_stdio(0);
	cin.tie();

	int a;
	cin >> a >> a >> a >> a >> a >> a;

	int m, k;
	cin >> m >> k;

	vector<pair<int, int>> v1(m);
	for (int i = 0; i < m; i++)
		cin >> v1[i].first >> v1[i].second;

	vector<pair<int, int>> v2(k);
	for (int i = 0; i < k; i++)
		cin >> v2[i].first >> v2[i].second;

	vector<pair<int, int>> v1Candi, v2Candi;

	{
		sort(v1.begin(), v1.end());

		{
			for (auto it = v1.begin(); it != v1.end(); it++)
			{
				if (it == v1.begin() || (it - 1)->first != it->first ||
					it + 1 == v1.end() || (it + 1)->first != it->first)
				{
					v1Candi.push_back({ it->first, it->second });
				}
			}
		}

		sort(v1.begin(), v1.end(), compareSecond);

		{
			for (auto it = v1.begin(); it != v1.end(); it++)
			{
				if (it == v1.begin() || (it - 1)->second != it->second ||
					it + 1 == v1.end() || (it + 1)->second != it->second)
				{
					v1Candi.push_back({ it->first, it->second });
				}
			}
		}
	}

	{
		sort(v2.begin(), v2.end());

		{
			for (auto it = v2.begin(); it != v2.end(); it++)
			{
				if (it == v2.begin() || (it - 1)->first != it->first ||
					it + 1 == v2.end() || (it + 1)->first != it->first)
				{
					v2Candi.push_back({ it->first, it->second });
				}
			}
		}

		sort(v2.begin(), v2.end(), compareSecond);

		{
			for (auto it = v2.begin(); it != v2.end(); it++)
			{
				if (it == v2.begin() || (it - 1)->second != it->second ||
					it + 1 == v2.end() || (it + 1)->second != it->second)
				{
					v2Candi.push_back({ it->first, it->second });
				}
			}
		}
	}

	long long ansValue = -1;
	pair<int, int> ans1, ans2;

	sort(v1Candi.begin(), v1Candi.end());
	sort(v2Candi.begin(), v2Candi.end());

	v1Candi.erase(unique(v1Candi.begin(), v1Candi.end()), v1Candi.end());
	v2Candi.erase(unique(v2Candi.begin(), v2Candi.end()), v2Candi.end());

	for (int i = 0; i < v1Candi.size(); i++)
	{
		for (int j = 0; j < v2Candi.size(); j++)
		{
			long long temp = (long long)(v1Candi[i].first - v2Candi[j].first) * (v1Candi[i].first - v2Candi[j].first) +
				(long long)(v1Candi[i].second - v2Candi[j].second) * (v1Candi[i].second - v2Candi[j].second);
			if (ansValue < 0 || temp < ansValue)
			{
				ansValue = temp;
				ans1 = v1Candi[i];
				ans2 = v2Candi[j];
			}
		}
	}

	std::cout << ansValue << endl;
	std::cout << ans1.first << " " << ans1.second << endl;
	std::cout << ans2.first << " " << ans2.second << endl;
}

#endif
}
