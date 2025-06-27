using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6.27
이름 : 배성훈
내용 : 합이 0
    문제번호 : 3151번

    브루트포스 문제다.
    처음에는 크기가 1만이므로 해시를 이용하면 O(1)에 해결되므로 
    아슬아슬하게 N^2의 방법이 유효하다 판단했다.

    제출하니 500ms에 통과했다.
    그런데 다른 사람과 시간이 4배 이상 차이가 났고,

    다른 사람 풀이를 본 결과 경우의 수를 나눠서 계산함이 좋음을 알았다.

    아이디어는 다음과 같다.
    0을 쓰는 경우와 안쓰는 경우로 나눈다.
    0을 딱 2개쓰는 건 마지막 합이 0이기에 불가능하다.

    0을 3개 쓰는 경우는 조합으로 zC3으로 찾는다.
    0을 1개 쓰는 경우는 양수를 1개 택하면 다른 음수를 1개 택하게 된다.
    그래서 해시로 갯수를 저장해 찾았다.

    이제 0을 안쓰는 경우는 다음 2가지로 나뉨을 알 수 있다.
    양수 3개나 음수 3개는 0이 될 수 없기 때문에
    양수 2개 + 음수 1개 또는 양수 1개 음수 2개뿐이다.

    그래서 양수 갯수를 세고 양수 2개를 택한다.
    이후 합한 값이 음수에 있는지 확인한다.
    비슷하게 음수도 세어 찾았다.

    그러니 130ms를 넘지 않고, 4배 이상 빨리 풀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1734
    {

        static void Main1734(string[] args)
        {

#if first
            int OFFSET = 10_000;
            int MAX = OFFSET * 2;
            int n;
            int[] arr, cnt;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    cnt[arr[i] + OFFSET]--;
                    for (int j = i + 1; j < n; j++)
                    {

                        cnt[arr[j] + OFFSET]--;
                        Cnt(arr[i] + arr[j]);
                    }

                    for (int j = i + 1; j < n; j++)
                    {

                        cnt[arr[j] + OFFSET]++;
                    }
                }

                Console.Write(ret);

                void Cnt(int _val)
                {

                    int idx = -_val + OFFSET;
                    if (ChkInvalidIdx()) return;
                    ret += cnt[idx];

                    bool ChkInvalidIdx()
                        => idx < 0 || idx > MAX;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                cnt = new int[MAX + 1];

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    cnt[cur + OFFSET]++;
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
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
#else

            int OFFSET = 20_000;
            int MAX = 2 * OFFSET;

            int n;
            int[] arr, cnt;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 1L * cnt[OFFSET] * (cnt[OFFSET] - 1) * (cnt[OFFSET] - 2) / 6;

                for (int i = OFFSET >> 1; i < OFFSET; i++)
                {

                    ret += 1L * cnt[OFFSET] * cnt[i] * cnt[MAX - i];
                }

                Array.Sort(arr);
                int pos = 0, neg = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] > 0) pos++;
                    else if (arr[i] < 0) neg++;
                }

                for (int i = 0; i < neg; i++)
                {

                    
                    for (int j = i + 1; j < neg; j++)
                    {

                        int cur = -arr[i] - arr[j];
                        ret += cnt[cur + OFFSET];
                    }
                }

                for (int i = n - pos; i < n; i++)
                {

                    for (int j = i + 1; j < n; j++)
                    {

                        int cur = -arr[i] - arr[j];
                        ret += cnt[cur + OFFSET];
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                cnt = new int[MAX + 1];

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    cnt[cur + OFFSET]++;
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
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
#endif
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
// #include <queue>
// #include <vector>

using namespace std;

typedef long long ll;

void input();
void solve();
int n;
int positive[10001];
int negative[10001];

int main(void) {
  input();
  solve();
  return 0;
}

void input() {
  cin >> n;
  int temp;
  for (int i = 0; i < n; i++) {
    cin >> temp;
    if (temp < 0) {
      negative[abs(temp)]++;
    } else {
      positive[temp]++;
    }
  }
  return;
}

ll pickTwo(int cur, int* container) {
  ll localTotal = 0;
  for(int i = 0, j = cur; i < j; i++,j--) {
    localTotal += container[i] * container[j];
  }

  int middleCouple = container[cur/2];
  if(cur % 2 == 0 && middleCouple >= 2) {
    localTotal += middleCouple * (middleCouple - 1) / 2;
  }

  return localTotal;
}

void solve() {
  ll total = 0;
  for(int i = 1; i < 10001; i++) {
    if (positive[i]) {
      total += pickTwo(i, negative) * positive[i];
    }
    if(negative[i]) {
      total += pickTwo(i, positive) * negative[i];
    }
  }
  if (positive[0] >= 3) {
    ll zeroVal = positive[0];
    total += zeroVal * (zeroVal - 1) * (zeroVal - 2) / 6;
  }

  cout << total;
  return;
}
#elif other2
// #include <iostream>
// #include <algorithm>
// #include <vector>
using namespace std;
using lld = long long;
// #define MX 10001
// #define MOD 1000000007
// #define fastio cin.tie(0)->sync_with_stdio(0)
// #define pi pair<int, int>
// #define pl pair<lld, lld>

lld res, N, v[MX], zer, pos[2 * MX], neg[2 * MX];

int main() {
	fastio;
    
    cin >> N;
    for(int i = 0; i < N; i++){
        cin >> v[i];
        if(!v[i]) zer++;
        else v[i] > 0? pos[v[i]]++ : neg[-v[i]]++;
    }
    if(zer > 2) res += zer * (zer - 1) * (zer - 2) / 6; //use 3 zero
    if(zer) for(int i = 1; i < MX; i++) res += zer * pos[i] * neg[i]; //use 1 zero
    
    sort(v, v + N);
    int zidx, nidx;
    zidx = nidx = 0;
    for(int i = 0; i < N; i++){
        if(v[i] < 0) zidx = i + 1;
        if(v[i] <= 0) nidx = i + 1;
    }
    for(int i = 0; i < zidx; i++) for(int j = i + 1; j < zidx; j++) res += pos[-v[i] - v[j]];
    for(int i = nidx; i < N; i++) for(int j = i + 1; j < N; j++) res += neg[v[i] + v[j]];
    cout << res;
    
	return 0;
}
#endif
}
