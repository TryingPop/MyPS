using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 6
이름 : 배성훈
내용 : 수
    문제번호 : 1056번

    dp, 이분 탐색 문제다.
    아이디어는 다음과 같다.
    dp[i]를 1에서 i로 이동하는 최소 경우라 하자.
    그러면 i < j 에대해 dp[j] - dp[i] ≤ j - i임을 알 수 있다.
    1씩 증가하는 경로로 가면 i로 가는 경우 i - 1이고 하나의 경로이다.
    dp의 정의가 최단 경로이므로 dp[i] < i가 성립한다.

    i 에서 j로 1번으로 이동하면 하나의 경로가 되고 dp[j] ≤ dp[i] + j - i이다.
    그래서 dp[j] - dp[i] ≤ j - i이다.

    그래서 각 x에 대해 a를 a^x ≤ n인 최댓값이라 하자.
    그러면 b < a 인 경우 b^x로 n으로 이동하는 것은 a^x로 이동하는 것보다 거리가 큼을 알 수있다.
    마찬가지로 a + 1 < b에 대해 b^x로 n으로 이동하는 것은 a + 1보다 큼을 알 수 있다.
    그래서 a와, a + 1로 이동한 경우만 비교하면 된다.

    모든 x에 대해 a를 찾고 a,  a+ 1로 이동한 최단 경로를 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1615
    {

        static void Main1615(string[] args)
        {

            long INF = 4_000_000_000_000_000_000;

            int MAX = 60;
            long n = Convert.ToInt64(Console.ReadLine());

            Dictionary<long, long> dp = new(300_000);
            dp[1] = 0;
            dp[0] = 1;
            Console.Write(DFS(n));

            long DFS(long _dep)
            {

                if (dp.ContainsKey(_dep)) return dp[_dep];
                dp[_dep] = _dep - 1;
                long ret = dp[_dep];

                for (int i = 2; i <= MAX; i++)
                {

                    // a^i < _dpe인 가장 큰 a를 이분 탐색으로 찾는다.
                    // 이는 _dep와 가까운 a^i를 찾기 위해서이다.
                    long a = BinarySearch(_dep, i);

                    for (int j = 0; j < 2; j++, a++)
                    {

                        long cur = DFS(a);
                        long add = Math.Abs(MyPow(a, i) - _dep);
                        ret = Math.Min(ret, cur + 1 + add);
                    }
                }

                return dp[_dep] = ret;

                long MyPow(long _a, int _exp)
                {

                    bool flag = false;
                    long ret = 1;
                    while (_exp > 0)
                    {

                        if ((_exp & 1) == 1) ret = MyMul(ret, _a);
                        _exp >>= 1;
                        _a = MyMul(_a, _a);
                        if (flag) break;
                    }

                    if (flag) return INF;
                    else return ret;

                    // 곱셈 정의
                    long MyMul(long _f, long _t)
                    {

                        long ret = 0;
                        while (_t > 0)
                        {

                            if ((_t & 1L) == 1L) ret += _f;
                            _t >>= 1;
                            _f += _f;
                            if (_f > INF)
                                return INF;
                        }

                        return ret;
                    }
                }

                long BinarySearch(long _chk, int _exp)
                {

                    long l = 1;
                    long r = 1_000_000_000;

                    while (l <= r)
                    {

                        long mid = (l + r) >> 1;
                        long pow = MyPow(mid, _exp);

                        if (pow <= _chk) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l - 1;
                }
            }
        }
    }
}
