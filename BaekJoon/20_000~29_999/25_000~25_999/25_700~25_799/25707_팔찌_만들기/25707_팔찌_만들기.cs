using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 25
이름 : 배성훈
내용 : 팔찌 만들기
    문제번호 : 25707번

    그리디 문제다.
    인접한 것의 최소가 되게 한다.
    그러면 정렬된 상태로 한 경우 가장 큰 것과 가장 작은것의 차가 된다.
    삼각 부등식으로 최소임을 알 수 있다.(그리디)

    그리고 마지막에 이어주는 것 역시 다른 경우와 비교하면 그리디로 최소임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1730
    {

        static void Main1730(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int min = 1_000_000_000;
            int max = 1;

            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                min = Math.Min(min, cur);
                max = Math.Max(max, cur);
            }

            Console.Write((max - min) << 1);

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
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <iostream>
// #include <algorithm>
using namespace std;

int arr[500002], n, m, X, p;
char r[6000000];
inline int f() {
	int answer = 0, x;
	while (1){
		x = r[p++];
		if (x == '\n' || x == ' ') break;
		answer = answer * 10 + (x & 0x0F);
	}
	return answer;
}
bool f(int num) {
	for (int i = num + 1; i <= m + 1; i++)
	if (arr[i] - arr[i - num - 1] > X)
		return true;
	return false;
}
int main(void){
	fread(r, 1, 6000000, stdin);
	n = f();
	m = f();
	for (int i = 1; i <= m; i++) arr[i] = f();
	sort(arr + 1, arr + m + 1);
	arr[m + 1] = n + 1;

	int left, right = m;
	X = f();
	left = f();

	while (right - left > 1) {
		int mid = (left + right) / 2;
		f(mid) ? right = mid : left = mid + 1;
	}
	cout << m - (f(left) ? left : right);
	return 0;
}
#endif
}
