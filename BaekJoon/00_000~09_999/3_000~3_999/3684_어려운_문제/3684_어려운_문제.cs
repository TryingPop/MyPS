using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 11
이름 : 배성훈
내용 : 어려운 문제
    문제번호 : 3684번

    브루트포스 문제다.
    xn = a * x(n-1) + b의 형태로 확장된 수열이다.
    가능한 모든 a, b에 대해 1씩 늘려가며 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1177
    {

        static void Main1177(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int MOD = 10_001;
            int n;
            int A, B;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                FindAB();
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{F(arr[i])}\n");
                }

                sw.Close();
            }

            void FindAB()
            {

                A = 0;
                B = 0;
                for (A = 0; A <= 10_000; A++)
                {

                    for (B = 0; B <= 10_000; B++)
                    {

                        bool flag = true;
                        for (int i = 1; i < n; i++)
                        {

                            int chk = F(F(arr[i - 1]));
                            if (chk == arr[i]) continue;

                            flag = false;
                            break;
                        }

                        if (flag) return;
                    }
                }
            }

            int F(int _x)
            {

                return (_x * A + B) % MOD;
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

                sr.Close();
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
                    if (c == '\n' || c == ' ') return true;
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
// #include<stdio.h>
int num[105];
int main() {
	int T;
	scanf("%d", &T);
	for(int i = 1; i <= T; i++) scanf("%d", &num[i]);
	if(T == 1) {
		printf("%d\n", num[1]);
		return 0;
	}

	for(int a = 0; a <= 10000; a++) {
		int chk = (num[2] - ((a * a % 10001) * num[1])) % 10001;
		if (chk < 0) chk += 10001;

		int i;
		for(i = 3; i <= T; i++) {
			int chk2 = (num[i] - ((a * a % 10001) * num[i-1])) % 10001;
			if (chk2 < 0) chk2 += 10001;

			if (chk != chk2) break;
		}

		if(i == T+1) {
			int b;
			for(b = 0; b <= 10000; b++) {
				if((a+1)*b % 10001 == chk) break;
			}
			if(b == 10001) continue;

			for(i = 1; i <= T; i++) {
				printf("%d\n", (num[i] * a + b) % 10001);
			}
			return 0;
		}
	}
	return 0;
}

#endif
}
