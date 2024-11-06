using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 9
이름 : 배성훈
내용 : New Password
    문제번호 : 26416번

    문자열, 구현 문제다
    조건대로 패스워드 추가만 해주면된다
*/

namespace BaekJoon.etc
{
    internal class etc_0756
    {

        static void Main756(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            StringBuilder ret;
            string cur;
            int n;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int test = int.Parse(sr.ReadLine());
                ret = new(10_000);

                for (int t = 1; t <= test; t++)
                {

                    n = int.Parse(sr.ReadLine());
                    cur = sr.ReadLine();
                    bool ctDigit = false;
                    bool ctMinAlpha = false;
                    bool ctMaxAlpha = false;
                    bool ctOthers = false;

                    for (int i = 0; i < n; i++)
                    {

                        if ('0' <= cur[i] && cur[i] <= '9') ctDigit = true;
                        else if ('a' <= cur[i] && cur[i] <= 'z') ctMinAlpha = true;
                        else if ('A' <= cur[i] && cur[i] <= 'Z') ctMaxAlpha = true;
                        else ctOthers = true;

                        ret.Append(cur[i]);
                    }

                    if (!ctDigit) ret.Append('0');
                    if (!ctMinAlpha) ret.Append('v');
                    if (!ctMaxAlpha) ret.Append('V');
                    if (!ctOthers) ret.Append('&');

                    while (ret.Length < 7)
                    {

                        ret.Append('v');
                    }

                    ret.Append('\n');
                    sw.Write($"Case #{t}: ");
                    sw.Write(ret);
                    ret.Clear();
                }

                sr.Close();
                sw.Close();
            }

        }
    }

#if other
// #include <iostream>
// #include <cctype>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(nullptr);

	int T;
	cin >> T;

	for (int i = 1; i <= T; ++i) {
		int N;
		string old_password;
		cin >> N >> old_password;

		bool uppercase = false;
		bool lowercase = false;
		bool digit = false;
		bool special = false;

		for (const auto c : old_password) {
			if (!uppercase) {
				if (isupper(c))
					uppercase = true;
			}

			if (!lowercase) {
				if (islower(c))
					lowercase = true;
			}

			if (!digit) {
				if (isdigit(c))
					digit = true;
			}

			if (!special) {
				if (c == '#' || c == '@' || c == '*' || c == '&')
					special = true;
			}
		}

		string additional;

		if (!uppercase)
			additional += 'A';

		if (!lowercase)
			additional += 'a';

		if (!digit)
			additional += '1';

		if (!special)
			additional += '#';

		const int size = old_password.size() + additional.size();

		if (size < 7)
			additional += string(7 - size, 'a');

		cout << "Case #" << i << ": " << old_password << additional << '\n';
	}
}
#endif
}
