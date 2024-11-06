using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 28
이름 : 배성훈
내용 : 버블 소트
    문제번호 : 1377번

    정렬 문제다
    아이디어는 다음과 같다
    버블 정렬이므로 1회 탐색 당 현재 남은 부분 중 가장 큰 값을 뒤로 보낸다
    반면 작은 원소들은 1회 탐색에서 자기자리가 아닌 경우에 한해 1번 움직인다
    그래서 작은 원소들이 자기 자리 찾아 가는데 걸리는 최대 값을 이용해 정답을 찾았다
    정렬된 배열과 기존 배열을 이분 탐색으로 비교하면서 찾았다
    다른 사람의 풀이를 보니, 기존 인덱스와 값을 저장한 자료구조를 정렬해 풀었는데 해당 방법이 더 좋아보인다
*/

namespace BaekJoon.etc
{
    internal class etc_1085
    {

        static void Main1085(string[] args)
        {

            int n;
            int[] arr, sort;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(sort);
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    int chk = BinarySearch(arr[i], i);
                    if (chk > i) continue;
                    ret = Math.Max(ret, i - chk);
                }

                Console.Write(ret + 1);

                int BinarySearch(int _n, int _r)
                {

                    int l = 0;
                    int r = _r;
                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (sort[mid] <= _n) l = mid + 1;
                        else r = mid - 1;
                    }

                    return r;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                sort = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    sort[i] = arr[i];
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
// #include <cstdio>
// #include <utility>
// #include <algorithm>
using namespace std;

char buf[1 << 17];

inline char read()
{
	static int idx = 1 << 17;
	if (idx == 1 << 17)
	{
		fread(buf, 1, 1 << 17, stdin);
		idx = 0;
	}
	return buf[idx++];
}
inline int readInt()
{
	int r, t = read() & 15;
	while ((r = read()) & 16) t = t * 10 + (r & 15);
	return t;
}
int main()
{
	int n = readInt();

	pair<int, int> p[n];
	for (int i = 0; i < n; ++i)
	{
		p[i].first = readInt();
		p[i].second = i;
	}

	sort(p, p + n);

	int ans = 0;
	for (int i = 0; i < n; ++i)
		ans = max(ans, p[i].second - i);

	printf("%d", ans + 1);

	return 0;
}
#endif
}
