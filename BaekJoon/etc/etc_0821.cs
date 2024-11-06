using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 지수를 더하자
    문제번호 : 27725번

    수학, 정수론 문제다
    아이디어는 간단하다
    해당 인수들의 최대값을 더해주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0821
    {

        static void Main821(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr;
            long k;

            long ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 0;

                for (int i = 0; i < n; i++)
                {

                    long cur = arr[i];
                    while (cur <= k)
                    {

                        ret += k / cur;
                        cur *= arr[i];
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                k = ReadLong();
                sr.Close();
            }

            long ReadLong() 
            {

                int c;
                long ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
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
// #include <bits/stdc++.h>

using namespace std;

int p[200000];

using ll = long long;

int main() {
	cin.tie(0);
	ios_base::sync_with_stdio(0);

	int N;
	cin >> N;

	for (int i = 0; i < N; i++) {
		cin >> p[i];
	}

	ll K;
	cin >> K;

	ll res = 0;
	for (int i = 0; i < N; i++) {
		ll c = p[i];
		while (c <= K) {
			res += K / c;
			c = c * p[i];
		}
	}
	cout << res << '\n';
}

/*
싸울 날을 위하여 마병을 예비하거니와 이김은 여호와께 있느니라
*/
#endif
}
