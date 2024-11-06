using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 19
이름 : 배성훈
내용 : 친구와 배달하기
    문제번호 : 27370번

    그리디 문제다
    최대 값의 범위가 10만 * 100만이므로 long 자료형으로 설정했다
    그리고, 중앙 거리 부분 처리가 중요한데 이는 10만 범위이므로 그냥 일일히 더해가면서 확인했다
    이후 연산으로 해당 부분을 해결하니 160ms에서 156ms로 4ms 가 줄었고
    정렬도 의미없어 보여 없애니 104ms로 줄었다

    아이디어는 다음과 같다
    예제를 해보면 꼭 같은 수로 이동할 필요가 없다
    그래서 a와 b거리를 재면서 작은쪽으로 이동시킨다
    같은 경우는 차이의 최소를 구하는데 영향을 주기에 따로 모아둔다
    이외는 a와 b에 누적해간다

    두사람의 합은 a, b의 이동거리 + 중앙 거리 개수를 연산한 뒤 2배를 곱해주면된다
    최소 차이의 경우 중앙 거리 개수가 있으면

    중앙 개수를 1개 차감시키고 중앙 거리만큼 차이에서 빼준다
    만약 차이가 음수인 경우 중앙거리를 더해준다
    그리고 마지막에 절대값을 씌우면 된다
    10만 단위라 하나씩 빼줘도 위에서 언급했듯이 속도에 큰 차이없다(4ms)
    
    이를 연산으로 표현하면 음수가 나오기 전까지 중앙 거리를 최대한 빼준다
    그리고 음수 앞까지 진행했음에도 남아있다면 -, +연산이 반복되므로 2로 나누고
    나머지만큼 더하거나 빼주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0573
    {

        static void Main573(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 6);
            StreamWriter sw = new(Console.OpenStandardOutput());

            int[] arr = new int[100_000];

            Solve();
            sr.Close();
            sw.Close();

            void Solve()
            {

                int test = ReadInt();
                int len;
                int s, e;
                while(test-- > 0)
                {

                    len = ReadInt();
                    s = ReadInt();
                    e = ReadInt();

                    for (int i = 0; i < len; i++)
                    {

                        arr[i] = ReadInt();
                    }

                    // Array.Sort(arr, 0, len);

                    long equal = 0;
                    long equalDis = 0;

                    long a = 0;
                    long b = 0;
                    for (int i = 0; i < len; i++)
                    {

                        int diffA = Math.Abs(arr[i] - s);
                        int diffB = Math.Abs(e - arr[i]);

                        if (diffA < diffB) a += 2 * diffA;
                        else if (diffA > diffB) b += 2 * diffB;
                        else
                        {

                            equal++;
                            equalDis = 2 * diffA;
                        }
                    }

                    long ret1 = a + b + (equal * equalDis);
                    long ret2 = Math.Abs(a - b);

                    // equal인 경우 10만 범위므로, 일일히 확인하면서 더해줬다
                    // 만약 해당부분을 계산으로 한다면,
                    // diff가 양수인만큼 비교하면서 빼준다

                    /*
                    for (int i = 0; i < equal; i++)
                    {

                        ret2 = Math.Min(Math.Abs(ret2 - equalDis), Math.Abs(ret2 + equalDis));
                    }
                    */
                    if (equal > 0 && equalDis > 0)
                    {

                        long cnt = ret2 / equalDis;

                        cnt = Math.Min(cnt, equal);
                        equal -= cnt;
                        ret2 -= cnt * equalDis;
                        ret2 = ret2 < 0 ? -ret2 : ret2;

                        if (equal > 0)
                        {

                            equal %= 2;
                            if (equal == 1)
                            {

                                ret2 -= equalDis;
                                ret2 = ret2 < 0 ? -ret2 : ret2;
                            }
                        }
                    }

                    sw.Write($"{ret1} {ret2}\n");
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while(( c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
import sys
input = sys.stdin.readline
g = lambda: map(int, input().split())

for _ in range(int(input())):
    N, *l = g()
    PA, PB = sorted(l)
    a, b = 0, 0
    reserve, cnt = 0, 0
    for num in g():
        if num - PA == PB - num:
            reserve = num - PA
            cnt += 1
        elif num - PA > PB - num:
            b += PB - num
        else:
            a += num - PA
    for i in range(cnt):
        if a < b:
            a += reserve
        else:
            b += reserve
    a <<= 1
    b <<= 1
    print(a + b, abs(a - b))
#elif other2
// #include <algorithm>
// #include <iostream>

using namespace std;

typedef long long int ll;

int T, N, A, B;

void solve() {
  ll aSum = 0, bSum = 0, center = 0;
  cin >> N >> A >> B;
  if (A > B)
    swap(A, B);
  for (int i = 0; i < N; i++) {
    ll x;
    cin >> x;
    if (x - A < B - x) {
      aSum += (x - A) * 2;
    } else if (x - A > B - x) {
      bSum += (B - x) * 2;
    } else {
      center++;
    }
  }
  ll centerPos = (A + B) / 2;

  while (center--) {
    if (aSum < bSum) {
      aSum += (centerPos - A) * 2;
    } else {
      bSum += (B - centerPos) * 2;
    }
  }
  cout << aSum + bSum << " " << abs(aSum - bSum) << "\n";
}

int main() {
  cin.tie(0);
  cout.tie(0);
  ios_base::sync_with_stdio(false);
  cin >> T;
  while (T--) {
    solve();
  }
  return 0;
}

#endif
}
