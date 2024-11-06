using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 31
이름 : 배성훈
내용 : 공주님의 정원
    문제번호 : 2457번

    그리디, 정렬 문제다
    아이디어는 다음과 같다

    먼저 시작 날짜를 기준으로 정렬한다
    그리고 현재 날짜를 기준으로 심을 수 있는 꽃 중
    가장 지는날짜가 가장 긴 꽃을 찾는다
    해당 꽃이 현재날짜보다 작은 경우 끝으로 갈 수 없으므로 종료한다

    그리고 매번 0번부터 진행하는 것은 비효율적이므로
    이전 진행을 기록해 이전 진행다음부터 탐색한다
    이렇게 제출하면 O(N)으로 찾아지고 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0929
    {

        static void Main929(string[] args)
        {

            StreamReader sr;

            int n;
            int[] day;
            (int s, int e)[] flowers;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                // 시작 시간 순으로 정렬
                Array.Sort(flowers, (x, y) => x.s.CompareTo(y.s));

                // 시작 날짜 3. 1
                int cur = GetDay(3, 1);
                int ret = 0;

                int idx = 0;
                // 종료 날짜 12. 1
                int end = GetDay(12, 1);

                while (cur < end && idx < n)
                {

                    // 다음 날짜 찾기
                    // 현재 날짜에서 가장 멀리갈 수 있는 날을 찾는다
                    int next = 0;
                    for (; idx < n && flowers[idx].s <= cur; idx++)
                    {

                        next = Math.Max(next, flowers[idx].e);
                    }

                    // 다음 날짜를 못찾으면 종료
                    if (next < cur) break;
                    cur = next;
                    ret++;
                }

                if (cur < end) ret = 0;
                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                day = new int[14] { 0, 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                for (int i = 2; i <= 13; i++)
                {

                    day[i] += day[i - 1];
                }

                flowers = new (int s, int e)[n];
                for (int i = 0; i < n; i++)
                {

                    int m = ReadInt();
                    int d = ReadInt();

                    int s = GetDay(m, d);

                    m = ReadInt();
                    d = ReadInt();

                    int e = GetDay(m, d);

                    flowers[i] = (s, e);
                }

                sr.Close();
            }

            int GetDay(int _m, int _d)
            {

                return day[_m] + _d;
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
        }
    }
#if other
// #include <cstdio>
// #include <algorithm>
using namespace std;

char buf[1<<20];
int idx = 1<<20;

inline char read()
{
	if (idx == 1<<20)
	{
		fread(buf, 1, 1<<20, stdin);
		idx = 0;
	}
	return buf[idx++];
}
inline int readInt()
{
	int sum = 0;
	char nw = read();
	
	while (nw == ' ' || nw == '\n') nw = read();
	while (nw >= '0' && nw <= '9')
	{
		sum = sum*10 + nw-48;
		nw = read();
	}
	
	return sum;
}
int main()
{
	int n = readInt();
	
	int a, b, c, d;
	int pos = 0;
	pair<int,int> p[n];
	for (int i = 0; i < n; ++i)
	{
		a = readInt();
		b = readInt();
		c = readInt();
		d = readInt();
		if (a*100+b < c*100+d)
		{
			p[pos].first = a*100+b;
			p[pos].second = c*100+d;
			pos++;
		}
	}
	
	sort(p, p+pos);
	
	int ans = 0;
	int max = 0;
	int flg = 0;
	int rng = 301;
	for (int i = 0; i < pos; ++i)
	{
		if (p[i].first > rng)
		{
			rng = max < rng ? rng : max;
			max = rng;
			flg = 0;
		}
		if (p[i].first <= rng && rng < 1201)
		{
			if (!flg) ans++, flg = 1;
			max = p[i].second > max ? p[i].second : max;
		}
	}
	
	printf("%d", max >= 1201 ? ans : 0);
	
	return 0;
}
#endif
}
