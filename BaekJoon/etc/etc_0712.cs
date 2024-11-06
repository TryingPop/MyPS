using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 20
이름 : 배성훈
내용 : Power towers
    문제번호 : 13970번

    수학, 오일러 피 함수 문제다
    중국인의 나머지 정리로 해결하려고 했으나, 
    이전 노트에 시도한 경우의 수로 왜 + phi 를 하는지 해결이되어 해당 방법으로 했다
    + phi를 하는 이유는 다음과 같다
    2^k과 경우와 100으로 나눌 때 나머지를 돌린 기록이 있었다
    주기를 찾아 본다, 
    2 -> 4 -> 8 -> 16 -> 32 -> 64 -> 28 -> 56 -> 12 -> 24 -> 48 -> 96 -> 92 -> 84 -> 68 -> 36 -> 72 -> 44 -> 88 -> 76 -> 52 -> 4
    그러면 2가 아닌 4에서 같은 수가 처음 나오고, 이후 반복이 된다
    4의 앞에는 2와 52가 엄연히 다른 숫자다
    그냥 제곱수를 하면 52인데, 2가 되는 경우가 있어 이를 방지함을 보인다

    다음 문제에서 정말로; 중국인의 나머지 정리를 써서 풀어야겠다!

    1^???? = 1이므로 지수가 1인 경우 다음을 저장하지 않고 끊어버렸다
    그리고 길이를 밑과 제곱에 있는 숫자의 개수라 하자
        예를들어 2 는 길이가 1이고, 2^2는 길이가 2이다 3^3^4 는 길이각 3이다
    여기서 길이가 5 이상에 대해 최소값은 2^(2^(2^(2^2)))이다
        1의 경우는 끊었다!
    그래서 길이가 5 이상이면 2^65536이고 이는 long 범위를 아득히 초과한다
    그래서 길이가 5이상인 경우에 대해서는 항상 phi보다 크다고 했고,
    5 미만인 경우에 대해서 처음에 직접 계산해 phi와 비교했다
    그리고 끝의 5개 경우에 비교했다(길이가 5이하면 5이하인 경우까지만 세었다)

    여기서 0인 경우 반례 처리를 제대로 안해서 2번 틀렸다
    이후 이를 수정하니 120ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0712
    {

        static void Main712(string[] args)
        {

            int MAX = 1_000_001;

            StreamReader sr;
            StreamWriter sw;

            int len;
            int[] arr;
            int[] chk;

            int mod;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();
                mod = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    long ret;
                    if (len > 0) ret = DFS(0, mod);
                    else ret = 1;
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[1_000_000];
                chk = new int[5];
            }

            void Input()
            {

                int n = ReadInt();
                len = n;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;

                    if (cur == 1)
                    {

                        len = i;
                        break;
                    }
                }

                for (int i = len + 1; i < n; i++)
                {

                    ReadInt();
                }

                SetChk();
            }

            void SetChk()
            {

                int r = Math.Min(len, 4);
                if (len == 0) return;

                chk[1] = arr[len - 1];

                for (int i = 2; i <= r; i++)
                {

                    chk[i] = CalcPow(arr[len - i], chk[i - 1]);
                }
            }

            int CalcPow(long _a, long _b)
            {

                if (_a >= MAX || _b >= 32) return MAX;
                long ret = 1;

                while (_b > 0)
                {

                    if ((_b & 1L) == 1L)
                    {

                        ret = ret * _a;
                        if (ret >= MAX) break;
                    }

                    if (_b > 1) _a = _a * _a;
                    _b /= 2;

                    if (_a >= MAX) break;
                }

                if (ret >= MAX || _a >= MAX) return MAX;
                return Convert.ToInt32(ret);

            }

            int GetPhi(int _n)
            {

                if (_n == 1) return 0;

                int ret = 1;

                for (int i = 2; i <= _n; i++)
                {

                    if (_n < i * i) break;
                    if (_n % i != 0) continue;

                    _n /= i;
                    ret *= i - 1;

                    while (_n % i == 0)
                    {

                        ret *= i;
                        _n /= i;
                    }
                }

                if (_n > 1) ret *= _n - 1;
                return ret;
            }

            long GetPow(long _a, long _b, long _mod)
            {

                long ret = 1;
                while (_b > 0)
                {

                    if ((_b & 1L) == 1L) ret = (ret * _a) % _mod;

                    _a = (_a * _a) % _mod;
                    _b /= 2;
                }

                return ret;
            }

            bool Chk(int _idx, long _mod)
            {

                int r = len - _idx;
                if (arr[_idx] == 1 || r == 0) return 1L >= _mod;

                if (r > 4) return true;
                return chk[r] >= _mod;
            }

            long DFS(int _idx, int _mod)
            {

                if (_mod == 1) return 0;
                if (_idx == len - 1) return arr[len - 1] % _mod;

                int phi = GetPhi(_mod);

                if (GetGCD(arr[_idx], _mod) == 1) return GetPow(arr[_idx], DFS(_idx + 1, phi), _mod);
                else return GetPow(arr[_idx], DFS(_idx + 1, phi) + (Chk(_idx + 1, phi) ? 1 : 0) * phi, _mod);
            }

            long GetGCD(long _a, long _b)
            {

                while (_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

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
