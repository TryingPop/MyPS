using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 4
이름 : 배성훈
내용 : 버블 정렬
    문제번호 : 1838번

    정렬 문제다.
    버블 정렬의 실행할 때 각 원소는 많아야 1턴 앞으로 이동할 수 있다.
    그래서 정렬되는 순간은 앞과의 거리가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1674
    {

        static void Main1674(string[] args)
        {

            int n;
            (int val, int idx)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) =>
                {

                    int ret = x.val.CompareTo(y.val);
                    if (ret == 0) ret = x.idx.CompareTo(y.idx);
                    return ret;
                });

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int chk = arr[i].idx - i;
                    ret = Math.Max(chk, ret);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new (int val, int idx)[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), i);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>
// #include <algorithm>
using namespace std;

const int MAX = 50'0005;
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
	int sum = 0;
	bool flg = 0;
	char now = read();

	while (now == 10 || now == 32) now = read();
	if (now == '-') flg = 1, now = read();
	while (now >= 48 && now <= 57)
	{
		sum = sum * 10 + now - 48;
		now = read();
	}

	return flg ? -sum : sum;
}
int main()
{
	int n = readInt();

	pair<int, int> arr[MAX];
	for (int i = 0; i < n; ++i) arr[i] = { readInt(), i };

	sort(arr, arr + n);

	int ans = 0;
	for (int i = 0; i < n; ++i)
		ans = max(ans, arr[i].second - i);

	printf("%d", ans);

	return 0;
}
#endif
}
