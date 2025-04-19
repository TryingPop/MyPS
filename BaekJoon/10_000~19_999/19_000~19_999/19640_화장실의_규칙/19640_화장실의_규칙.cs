using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 화장실의 규칙
    문제번호 : 19640번

    구현, 자료구조, 시뮬레이션, 우선순위 큐 문제다
    그리디하게 풀었다

    주된 아이디어는 다음과 같다
    일렬로 세우고 줄을 선다
    그리고 자신의 줄에서 앞에 있는 사람 중 가장 우선순위가 낮은 사람을 찾는다(자신 포함)

    그리고 자신의 줄과 다른 줄에서 가장 우선순위가 낮은 사람보다 우선수위가 높은 사람들을 앞에서 부터 찾는다
    낮은 경우 탈출하고 높은 경우 계속해서 줄이 끝날 때까지 찾는다 
    해당 사람들을 모두 찾은 값에 자신의 줄에서 앞에 있는 사람의 수를 더하면 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0264
    {

        static void Main264(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int all = ReadInt(sr);
            int group = ReadInt(sr);
            int myGroup;
            int myLen;
            {

                int myLine = ReadInt(sr);
                myGroup = myLine % group;
                myLen = myLine / group;
            }

            List<Human>[] humans = new List<Human>[group];
            int len = all / group;
            len = len < 1 ? 1 : len;
            for (int i = 0; i < group; i++)
            {

                humans[i] = new(len);
            }

            int cur = 0;
            for (int i = 0; i < all; i++)
            {

                Human add = new Human(ReadInt(sr), ReadInt(sr), cur);
                humans[cur++].Add(add);

                if (cur == group) cur = 0;
            }

            sr.Close();

            Human min = humans[myGroup][0];
            for (int i = 1; i <= myLen; i++)
            {

                // 자신의 줄에서 앞에 있는 사람 중 자신을 포함해서 가장 늦게 들어가는 애를 찾는다
                if (humans[myGroup][i].CompareTo(min) < 0) min = humans[myGroup][i];
            }

            // 자신의 줄에 앞에 있는 인원
            int ret = myLen;
            for (int i = 0; i < group; i++)
            {

                if (i == myGroup) continue;

                for (int j = 0; j < humans[i].Count; j++)
                {

                    // 다른 줄에서 앞에서부터 자신보다 우선수위 높은 사람이 먼저 간다
                    if (humans[i][j].CompareTo(min) > 0) ret++;
                    // 끊기면 뒤에 사람은 안세어야한다
                    else break;
                }
            }

            Console.WriteLine(ret);
        }

        struct Human : IComparable<Human>
        {

            public int d;
            public int h;
            public int g;

            public int CompareTo(Human other)
            {

                // 급한 순서를 표현
                int ret = d.CompareTo(other.d);
                if (ret != 0) return ret;

                ret = h.CompareTo(other.h);
                if (ret != 0) return ret;

                ret = other.g.CompareTo(g);
                return ret;
            }

            public Human(int _d, int _h, int _g)
            {

                d = _d;
                h = _h;
                g = _g;
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
// #include <iostream>
// #include <queue>
// #include <algorithm>

using namespace std;

// #define NMAX  100001
int N, M, K;
int D[NMAX];
int H[NMAX];
int MaxD = 100000001, MaxH = 100000001, line;

int main(void) {
	ios::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);

	cin >> N >> M >> K;

	for (int i = 0; i < N; i++)
		cin >> D[i] >> H[i];

	int idx;
	line = K % M;
	for (int i = 0; i < N; i++) {
		idx = line + i * M;

		if (idx > K)
			break;

		if (D[idx] > MaxD)
			continue;
		if (D[idx] == MaxD && H[idx] > MaxH)
			continue;

		MaxD = D[idx];
		MaxH = H[idx];

	}

	int i, cnt = K / M;
	for (int l = 0; l < M; l++) {
		if (l == line)
			continue;

		i = 0;
		while (1) {
			idx = l + M * i;
			if (idx >= N)
				break;

			if (D[idx] < MaxD)
				break;
			if (D[idx] == MaxD && H[idx] < MaxH)
				break;
			if (D[idx] == MaxD && H[idx] == MaxH && l > line)
				break;

			cnt++; i++;

		}
	}
	cout << cnt << '\n';

	return 0;
}
#endif
}
