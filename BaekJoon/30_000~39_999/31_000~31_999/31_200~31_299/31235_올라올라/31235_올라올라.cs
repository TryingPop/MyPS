using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 25
이름 : 배성훈
내용 : 올라올라
    문제번호 : 31235번

    그리디 문제다.
    처음에는 매개변수 탐색으로 접근했다.
    해집합이 k라 하면 k + 1에서는 자명하게 성립하기 때문이다.
    그래서 우선순위 큐로 k개의 최댓값을 관리하며 접근하니 N log N log N으로 시간초과 났다.
    
    
    이제 어떻게 접근할까 고민했다.
    우선순위 큐로 이전 범위의 최댓값에서 이전 길이 이상으로 현재 최댓값을 찾아간다.
    찾는데 작은 경우 길이를 1씩 늘려간다.
    이렇게 구간의 끝 부분이 n인 경우 종료하고 마지막 길이가 정답이 된다.
    그러니 N log N의 풀이방법이 나왔다.
    해당 코드 작성 전에 풀이가 나와서 해당 방법은 없다.
    
    더 고민하니 우선순위 큐가 아닌 큰 값의 차이로 해결할 수 있음을 확인했다.
    가장 큰 값을 모아놓고 해결하는 것이다.
    다만 조금 더 보니 값을 저장할 필요가 없었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1578
    {

        static void Main1578(string[] args)
        {

#if TIME_OUT || MORE_MEMORY
            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

#if TIME_OUT
                PriorityQueue<int, int> pq = new(n), pop = new(n);

                Console.Write(BinarySearch());

                int BinarySearch()
                {

                    int l = 1;
                    int r = n;

                    while(l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (Chk(mid)) r = mid - 1;
                        else l = mid + 1;
                    }

                    return r + 1;
                }

                bool Chk(int _len)
                {

                    pq.Clear();
                    pop.Clear();

                    for (int i = 0; i < _len; i++)
                    {

                        pq.Enqueue(arr[i], -arr[i]);
                    }

                    for (int i = _len; i < n; i++)
                    {

                        int cur = pq.Peek();
                        pop.Enqueue(arr[i - _len], -arr[i - _len]);

                        while (pop.Count > 0 && pop.Peek() == pq.Peek())
                        {

                            pq.Dequeue();
                            pop.Dequeue();
                        }

                        pq.Enqueue(arr[i], -arr[i]);

                        int next = pq.Peek();

                        if (next < cur) return false;
                    }

                    return true;
                }

#else
                // MORE MEMORY

                int ret = 1;
                int prev = 0;
                int cnt = 1;
                for (int i = 1; i < n; i++)
                {

                    if (arr[prev] > arr[i]) cnt++;
                    else
                    {

                        ret = Math.Max(ret, cnt);
                        cnt = 1;
                        prev = i;
                    }
                }

                ret = Math.Max(cnt, ret);
                Console.Write(ret);
#endif

            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput());

                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
#else

            using StreamReader sr = new(Console.OpenStandardInput());

            int n = ReadInt();
            int prev = ReadInt();
            int idx = 0;
            int ret = 0;
            for (int i = 1; i < n; i++)
            {

                int cur = ReadInt();

                if (cur < prev) continue;

                ret = Math.Max(ret, i - idx);
                idx = i;
                prev = cur;
            }

            // 끝 구간 확인
            // 나머지를 묶어서 끝내는걸 마지막에 가장 큰 수가 있다고 가정한다.
            ret = Math.Max(ret, n - idx);
            Console.Write(ret);

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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
#endif
        }
    }
}
