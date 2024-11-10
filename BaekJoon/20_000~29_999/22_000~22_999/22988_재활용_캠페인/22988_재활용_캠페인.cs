using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 8
이름 : 배성훈
내용 : 재활용 캠페인
    문제번호 : 22988번

    그리디 알고리즘, 정렬, 두 포인터 문제다.
    모두 꽉찬 경우를 생각못해 한 번 틀렸다.

    아이디어는 다음과 같다.
    가장 작은 것과 가장 큰 것을 교환 했을 때 가득찬 것을 얻을 수 있다면 해당 교환을 진행한다.
    반면 안나오는 경우 가장 작은걸 2개로 교환해 채워넣는다.
    이렇게 바꾸면 그리디로 최대한 완성된걸로 바꿀 수 있다.

    우선순위 큐 4개를 이용해 구현했다.

    다른사람의 풀이를 보니 바꿀수 있는 만큼 바꾸고 
    나머지는 3개로만 교환가능하니 3개로 나누는 아이디어가 더 좋아보인다.
*/

namespace BaekJoon.etc
{
    internal class etc_1100
    {

        static void Main1100(string[] args)
        {

            int n;
            long x;

            PriorityQueue<long, long> minQ, maxQ, minP, maxP;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                long add = x >> 1;
                while (n > 0 && maxQ.Peek() == x)
                {

                    n--;
                    ret++;
                    long pop = maxQ.Dequeue();
                    minP.Enqueue(pop, pop);
                }

                while (n > 1)
                {

                    n--;
                    ChkPop(minQ, minP);
                    long min = minQ.Peek();
                    ChkPop(maxQ, maxP);
                    long max = maxQ.Peek();

                    long chk = min + max + add;
                    if (x <= chk)
                    {

                        minQ.Dequeue();
                        maxQ.Dequeue();

                        minP.Enqueue(max, max);
                        maxP.Enqueue(min, -min);
                        n--;
                        ret++;
                        continue;
                    }

                    min = minQ.Dequeue();
                    ChkPop(minQ, minP);
                    max = minQ.Dequeue();

                    maxP.Enqueue(min, -min);
                    maxP.Enqueue(max, -max);

                    chk = min + max + add;

                    minQ.Enqueue(chk, chk);
                    maxQ.Enqueue(chk, -chk);
                }

                ChkPop(minQ, minP);
                if (n > 0 && x <= minQ.Peek()) ret++;
                Console.Write(ret);

                void ChkPop(PriorityQueue<long, long> _pq, PriorityQueue<long, long> _pop)
                {

                    while (_pop.Count > 0 && _pq.Peek() == _pop.Peek())
                    {

                        _pq.Dequeue();
                        _pop.Dequeue();
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = (int)ReadLong();
                x = ReadLong() << 1;

                minQ = new((n * 3) >> 1);
                maxQ = new((n * 3) >> 1);
                minP = new((n * 3) >> 1);
                maxP = new((n * 3) >> 1);

                for (int i = 0; i < n; i++)
                {

                    long cur = ReadLong() << 1;
                    minQ.Enqueue(cur, cur);
                    maxQ.Enqueue(cur, -cur);
                }

                sr.Close();

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>

using namespace std;

long long arr[100001];

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(0);

    long long N, X;
    int ans = 0;

    cin >> N >> X;
    for (int i = 0; i < N; i++)
    {
        cin >> arr[i];
    }

    sort(arr, arr + N);

    int l = 0, r = N - 1;
    int left = 0;

    while (arr[r] >= X && r >= 0)
    {
        r--;
        ans++;
    }

    while (l <= r)
    {
        if (l < r &&arr[r] + arr[l] >= (X + 1) / 2)
        {
            ans++;
            l++;
            r--;
        }
        else {
            l++;
            left++;
        }
    }
    cout << ans + left / 3;


    return 0;
}
#endif
}
