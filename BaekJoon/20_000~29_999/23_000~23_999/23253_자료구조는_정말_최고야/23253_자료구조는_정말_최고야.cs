using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 9
이름 : 배성훈
내용 : 자료구조는 정말 최고야
    문제번호 : 23253번

    구현, 자료 구조, 애드 혹 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1037
    {

        static void Main1037(string[] args)
        {

            string YES = "Yes";
            string NO = "No";

            StreamReader sr;
            int n, m;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(Chk() ? YES : NO);
                sr.Close();
            }
            
            bool Chk()
            {

                for (int i = 0; i < m; i++)
                {

                    int len = ReadInt();
                    int b = n + 1;
                    for (int j = 0; j < len; j++)
                    {

                        int cur = ReadInt();
                        if (b > cur) 
                        {

                            b = cur;
                            continue;
                        }
                        return false;
                    }
                }

                return true;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                n = ReadInt();
                m = ReadInt();
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

int main()
{
	ios::sync_with_stdio(false); cin.tie(NULL);

	int N, M;
	cin >> N >> M;

	int k, prev, now;
	while (M--) {
		prev = 2147483647;
		cin >> k;
		while (k--) {
			cin >> now;
			if (now > prev) {
				cout << "No";
				return 0;
			}
			prev = now;
		}
	}

	cout << "Yes";
	return 0;
}
#endif
}
