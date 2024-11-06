using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 7
이름 : 배성훈
내용 : 이진 삼진 탐색 놀이 3
    문제번호 : 19601번

    2가지 이유로 2번 틀렸다
        1. 3개인 경우 삼진 탐색의 경우 세는 방법을 잘못 인지
            3인 경우 1번 인덱스를 2번 탐색하고
            0, 2번은 2가 된다
        2. 그냥 연산자 잘못 설정한 경우로 >=가 아닌  >를 사용해 틀렸다
        
    
*/

namespace BaekJoon.etc
{
    internal class etc_0160
    {


        static void Main160(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 512);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int n = int.Parse(sr.ReadLine());
            ulong[] dp2 = new ulong[64];
            dp2[0] = 1;
            for (int i = 1; i < dp2.Length; i++)
            {

                dp2[i] = dp2[i - 1] * 2;
            }

            uint[] dp3 = new uint[12];
            dp3[1] = 1;
            dp3[2] = 2;
            dp3[3] = 3;
            dp3[4] = 3;
            dp3[5] = 3;
            dp3[6] = 4;
            dp3[7] = 4;
            dp3[8] = 4;
            dp3[9] = 5;
            dp3[10] = 5;
            dp3[11] = 5;

            for (int i = 0; i < n; i++)
            {

                ulong find = GetULong(sr);

                for (int j = dp2.Length - 1; j >= 0; j--)
                {

                    if (dp2[j] > find) continue;

                    sw.Write(j);
                    break;
                }

                sw.Write(' ');

                int ret = -1;
                while (true)
                {

                    if (find >= 12)
                    {

                        find -= 2;
                        ret += 2;
                    }
                    else 
                    {

                        ret += (int)dp3[find];
                        break;
                    }

                    uint add = 0;
                    if (find % 3 != 0) add++;

                    find /= 3;
                    find += add;
                }

                sw.Write(ret);
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }

        static ulong GetULong(StreamReader _sr)
        {

            ulong ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = (ret * 10) + (ulong)(c - '0');
            }

            return ret;
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;

using ll = long long;

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	vector<ll> B(63);
	vector<ll> T(80);
	B[0] = T[0] = 1;
	for (int i = 1; i < B.size(); ++i) {
		B[i] = 2 * B[i - 1];
	}
	for (int i = 1; i < T.size(); ++i) {
		T[i] = i % 2 ? (2 * T[i - 1]) : (3 * T[i - 2]);
	}
	int Q{};
	cin >> Q;
	while (Q--) {
		ll N{};
		cin >> N;
		cout << upper_bound(B.begin(), B.end(), N) - B.begin() - 1 << " ";
		cout << upper_bound(T.begin(), T.end(), N) - T.begin() - 1 << "\n";
	}
	return 0;
}

#endif
}
