using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 9
이름 : 배성훈
내용 : 단어 덧셈
    문제번호 : 4964번

    브루트포스, 백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1620
    {

        static void Main1620(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            string[] str = new string[13];          // 입력된 문자열
            bool[] use = new bool[255];             // 사용된 알파벳 여부
            int[] alphabet = new int[10];           // 사용된 알파벳들
            int[] val = new int[255];               // 알파벳에 따른 값
            bool[] impoZero = new bool[255];        // 0 불가능 판별

            while ((n = ReadInt()) != 0)
            {

                // 사용된 알파벳의 갯수
                int len = 0;

                for (int i = 0; i < n; i++)
                {

                    // 문자열 읽기
                    str[i] = sr.ReadLine().Trim();
                    for (int j = 0; j < str[i].Length; j++)
                    {

                        // 이미 사용된 문자인지 확인
                        int cur = str[i][j];
                        if (use[cur]) continue;
                        use[cur] = true;
                        alphabet[len++] = cur;
                    }

                    // 2자리 이상인 경우 앞에 0이 못온다는 조건!
                    if (str[i].Length > 1) impoZero[str[i][0]] = true;
                }

                int ret = DFS();
                sw.Write(ret);
                sw.Write('\n');

                int DFS(int _dep = 0, int _state = 0)
                {

                    if (_dep == len)
                        // 0 ~ n - 2까지 합쳤을 때 n - 1이면 1, 아니면 0
                        return Calc() ? 1 : 0;

                    // 현재 알파벳
                    int cur = alphabet[_dep];
                    int s = impoZero[cur] ? 1 : 0;

                    int ret = 0;
                    for (int i = s; i < 10; i++)
                    {

                        // 알파벳에 부여
                        if ((_state & (1 << i)) != 0) continue;
                        val[cur] = i;
                        ret += DFS(_dep + 1, _state | 1 << i);
                    }

                    return ret;
                }

                bool Calc()
                {

                    // 0 ~ n - 2까지 합친다.
                    long ret = 0;

                    for (int i = 0; i < n - 1; i++)
                    {

                        ret += StringToInt(i);
                    }

                    ret -= StringToInt(n - 1);

                    return ret == 0;

                    long StringToInt(int _idx)
                    {

                        // 알파벳을 입력한 숫자로 변환
                        long ret = 0;
                        for (int i = 0; i < str[_idx].Length; i++)
                        {

                            ret = ret * 10 + val[str[_idx][i]];
                        }

                        return ret;
                    }
                }

                // 초기화
                for (int i = 0; i < len; i++)
                {

                    int cur = alphabet[i];
                    use[cur] = false;
                    impoZero[cur] = false;
                }
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
                    ret = c - '0';

                    while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <cstdio>
// #include <algorithm>

int solve(int N) {
	int Counts[11]{}, cnt = 0;
	bool Zero[11]{};
	{
		char _check[26]{};
		for (int _i = 1; _i <= N; ++_i) {
			char _inp[9];
			scanf("%s", _inp);

			int _slen = 0;
			while (_inp[++_slen]);

			for (int _pos = _slen, _mul = _i != N ?: -1; _pos; --_pos, _mul *= 10) {
				char& _c = _check[_inp[_pos - 1] - 'A'];
				Counts[_c = _c ?: ++cnt] += _mul;
			}

			Zero[_check[_inp[0] - 'A']] |= _slen != 1;
		}
	}

	int perm[10]{}, result = 0;
	for (int i = 10 - cnt, t = 1; i < 10; ++i, ++t) perm[i] = t;

	do {
		if (Zero[perm[0]]) continue;
		int sum = 0;
		for (int i = 1; i < 10; ++i) sum += Counts[perm[i]] * i;
		result += sum == 0;
	} while (std::next_permutation(perm, perm + 10));

	return result;
}

int main() {
	for (int N; scanf("%d", &N), N;) printf("%d\n", solve(N));
	return 0;
}
#endif
}
