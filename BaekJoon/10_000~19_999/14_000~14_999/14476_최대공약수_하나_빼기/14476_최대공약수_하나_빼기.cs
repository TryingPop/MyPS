using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 15
이름 : 배성훈
내용 : 최대공약수 하나 빼기
    문제번호 : 14476번

    수학, 정수론, 누적합, 유클리드 호제법 문제다.
    아이디어는 다음과 같다.

    num1, num2, num3, ... numR의 gcd는
    num1과 num2의 gcd 인 gcd1을 구한다.

    그리고 gcd1과 num3의 gcd인 gcd2를 구한다.
    ... 이렇게 gcd(R-2)와 numR의 gcd인 gcd(R-1)을 구한다.
    그러면 gcd(R-1)이 num1, num2, ... numR의 gcd가 된다.
    여기서 gcd는 최대 공약수를 의미한다.

    이렇게 순차적으로 구하기에 
    왼쪽 누적과 오른쪽 gcd 누적을 구한 뒤
    왼쪽과 오른쪽 gcd 계산을 해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1115
    {

        static void Main1115(string[] args)
        {

            int n;
            int[] arr;
            int[] left, right;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                int ret1;
                int ret2 = arr[0];
                if (arr[0] % right[1] == 0) ret1 = -1;
                else ret1 = right[1];
                
                for (int i = 1; i < n - 1; i++)
                {

                    int gcd = GetGCD(left[i - 1], right[i + 1]);
                    if (arr[i] % gcd == 0 || gcd < ret1) continue;
                    ret1 = gcd;
                    ret2 = arr[i];
                }

                if (arr[n - 1] % left[n - 2] != 0 && ret1 < left[n - 2])
                {

                    ret1 = left[n - 2];
                    ret2 = arr[n - 1];
                }

                if (ret1 != -1) Console.Write($"{ret1} {ret2}");
                else Console.Write(-1);
            }

            void SetArr()
            {

                left = new int[n];
                left[0] = arr[0];
                for (int i = 1; i < n; i++)
                {

                    left[i] = GetGCD(left[i - 1], arr[i]);
                }

                right = new int[n];
                right[n - 1] = arr[n - 1];
                for (int i = n - 2; i >= 0; i--)
                {

                    right[i] = GetGCD(right[i + 1], arr[i]);
                }
            }

            int GetGCD(int _a, int _b)
            {

                while (_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

int n, mx, val, v[1'000'001], pSum[1'000'001];

int main() {
    struct stat st; fstat(0, &st);
	char* p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
	auto ReadInt = [&]() {
		int ret = 0;
		for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};

	n = ReadInt();
	for (int i = 1; i <= n; i++) pSum[i] = __gcd(pSum[i - 1], v[i] = ReadInt());
    for (int i = n, t = 0; i >= 1; t = __gcd(v[i--], t)) {
        const int g = __gcd(pSum[i - 1], t);
        if (g % v[i] && mx < g) mx = g, val = v[i];
    }
	if (mx <= pSum[n]) cout << -1 << '\n';
	else cout << mx << ' ' << val << '\n';
}
#endif
}
