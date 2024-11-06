using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 29
이름 : 배성훈
내용 : 해밍 수열
    문제번호 : 7868번

    수학, 브루트포스, 정렬, 정수론 문제다
    정답은 10^18이하가 보장되므로 

    p1, p2, p3로 생성되는 10^18 이하의 모든 수열을 찾으면 된다
    10^18 < 2^60 이므로 생성되는 수들은 60^3 = 216_000개를 넘지 않는다

    그리고 10^18은 long.MaxValue에 가까우므로 중간 연산에 
    오버플로우가 발생할 가능성이 매우 높다

    이를 a * b < c <=> a <= c / b 를 이용해 오버플로우 안되게 확인했다
    그리고 서로다른 소수들의 곱이므로 소수 정의상 절대로 중복값이 나올 수 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0848
    {

        static void Main848(string[] args)
        {

            const long INF = 1_000_000_000_000_000_000;
            StreamReader sr;

            long p1, p2, p3, i;
            long[] arr;
            int len;

            Solve();
            void Solve()
            {

                Init();
                FillArr();

                Console.Write(arr[i]);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                p1 = ReadLong();
                p2 = ReadLong();
                p3 = ReadLong();
                i = ReadLong();

                sr.Close();
                arr = new long[220_000];
            }

            void FillArr()
            {

                len = 0;
                int len1 = GetMaxExp(p1);
                int len2 = GetMaxExp(p2);
                int len3 = GetMaxExp(p3);

                long pp1 = 1;

                for (int i = 0; i <= len1; i++)
                {

                    long pp2 = 1;
                    for (int j = 0; j <= len2; j++)
                    {

                        long chk = pp1 * pp2;
                        long pp3 = 1;
                        for (int k = 0; k <= len3; k++)
                        {

                            long cur = chk * pp3;
                            arr[len++] = cur;

                            if (IsPossible(pp3, p3) && IsPossible(chk, pp3 * p3)) 
                            {

                                pp3 *= p3;
                                continue; 
                            }

                            break;
                        }

                        if (IsPossible(pp2, p2) && IsPossible(pp1, pp2 * p2))
                        {

                            pp2 *= p2;
                            continue;
                        }

                        break;
                    }

                    if (IsPossible(pp1, p1))
                    {

                        pp1 *= p1;
                        continue;
                    }

                    break;
                }

                Array.Sort(arr, 0, len);
            }

            bool IsPossible(long _a, long _b, long _MAX = INF)
            {

                return _b <= _MAX / _a;
            }

            int GetMaxExp(long _p)
            {

                int ret = 0;
                long MAX = INF;
                while(IsPossible(_p, _p, MAX))
                {

                    ret++;
                    MAX /= _p;
                }

                return ret;
            }

            long ReadLong()
            {

                int c;
                long ret = 0;
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
// #include <stdio.h>
// #include <queue>
// #include <unordered_set>
// #define M 1000000000000000001
using namespace std;

priority_queue<long long, vector<long long>, greater<>> pq;
unordered_set<long long> vis;

int isoverflow(long long x, long long y, long long m) {
    if (m / y < x) return 1;
    else return 0;
}

int main(void) {
    long long p1, p2, p3, c, x;
    scanf("%lld %lld %lld %lld", &p1, &p2, &p3, &c);
    pq.push(1);
    x = 1;
    for (long long i = 0; i < c; i++) {
        x = pq.top();
        //printf("%lld\n", x);
        pq.pop();
        if (!isoverflow(x, p1, M)) {
            if (vis.find(x * p1) == vis.end()) {
                pq.push(x * p1);
                vis.insert(x * p1);
            }
        }
        if (!isoverflow(x, p2, M)) {
            if (vis.find(x * p2) == vis.end()) {
                pq.push(x * p2);
                vis.insert(x * p2);
            }
        }
        if (!isoverflow(x, p3, M)) {
            if (vis.find(x * p3) == vis.end()) {
                pq.push(x * p3);
                vis.insert(x * p3);
            }
        }
    }
    printf("%lld", pq.top());
    return 0;
}
#endif
}
