using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 30
이름 : 배성훈
내용 : 진법
    문제번호 : 2085번

    dp, 많은 조건 분기 문제다.
    문자열의 길이 n이 35로 짧다
    그래서 n^3 방법이 유효하다.

    이에 길이별로 가능한 경우의 수를 찾는다.
    경우의 수를 찾는데 dp를 이용해 n^2 방법으로 찾아간다.
    dp[i] = val를 i번째 시작에서 유효한 경우 val의 개수를 담게 한다.
    그러면 dp[i] = ∑dp[j] (j > i) 임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1850
    {

        static void Main1850(string[] args)
        {
            
            // 2085번
            string num = Console.ReadLine();

            long[] dp = new long[num.Length];

            long ret = 0;

            for (int i = 1; i < num.Length; i++)
            {

                ret += Find(i);
            }

            Console.Write(ret);

            // 경우의 수찾기
            long Find(int _dS)
            {

                if (ChkInvalidNum(_dS, num.Length - _dS)) return 0;
                Array.Fill(dp, -1);

                return DFS(0);
                long DFS(int _dep = 0)
                {

                    if (_dep == _dS) return 1;
                    ref long ret = ref dp[_dep];
                    if (ret != -1) return ret;

                    ret = 0;
                    for (int cur = _dep, len = 1; cur < _dS; cur++, len++)
                    {

                        if (ChkInvalidNum(_dep, len, _dep != 0 | _dS == 1) || !IsMin(_dep, len, _dS)) break;
                        ret += DFS(_dep + len);
                    }

                    return ret;
                }
            }

            // 진법 값보다 작은지 확인
            bool IsMin(int _s, int _l, int _dS)
            {

                int len = num.Length - _dS;
                if (_l != len) return _l < len;

                for (int i = 0; i < len; i++)
                {

                    if (num[_s + i] != num[_dS + i]) return num[_s + i] < num[_dS + i];
                }

                return false;
            }

            // 0으로 시작하는지 확인
            bool ChkInvalidNum(int _s, int _len, bool _canZero = false)
            {

                if (_canZero) return _len > 1 & num[_s] == '0';
                else return num[_s] == '0';
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>
typedef long long ll;

char in[36]; 
int len, ans;

// base의 시작 index가 b라고 했을 때, 가능한 경우의 수를 센다.
int count(int b) {
  int DP[36] = {1};
  if (len > 2 * b) {  // base가 모든 경우의 수보다 클 수 밖에 없는 경우는 모두 세어준다.
    for (int i = 0; i < b; ++i) {
      if (in[i] == '0') { // 0으로 시작하면 뒤에 수가 있으면 안되므로 한 자릿 수만 세준다.
        DP[i+1] += DP[i];
        continue;
      }
      for (int j = i; j < b; ++j) DP[j+1] += DP[i];
    }
  } else {  // base가 작은 경우가 생기는 상황은 base를 구해줘서 체크한다.
    ll base = 0, val;
    for (int i = b; in[i]; ++i) base = base * 10 + in[i] - '0';
    for (int i = 0; i < b; ++i) {
      if (in[i] == '0') {
        DP[i+1] += DP[i];
        continue;
      }
      val = 0LL;
      for (int j = i; j < b && j < i + 17; ++j) {
        val = val * 10LL + in[j] - '0';
        if (val < base) DP[j+1] += DP[i];
        else break;
      }
    }
  }
  return DP[b];
}

int main(int argc, char *argv[]) {
  scanf("%s", in);
  len = strlen(in);
  if (in[0] == '0') return !printf("%d\n", in[1] == '0' ? 0 : 1);
  for (int i = 1; i < len; ++i) {
    if (in[i] == '0') continue;
    ans += count(i);
  }
  return !printf("%d\n", ans);
}
#endif
}
