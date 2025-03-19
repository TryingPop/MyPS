using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 19
이름 : 배성훈
내용 : 순회강연
    문제번호 : 2109번

    그리디, 우선순위 큐 문제다.
    아이디어는 다음과 같다.
    끝날짜를 e라 하면 e ~ d일째에 최대 강연료가 되게 거꾸로 진행한다.
    이렇게 진행해가면 그리디로 최대값임이 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1420
    {

        static void Main1420(string[] args)
        {

            int n;
            (int p, int d)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => y.d.CompareTo(x.d));

                int curDay = arr[0].d;
                PriorityQueue<int, int> pq = new(n);

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    pq.Enqueue(arr[i].p, -arr[i].p);

                    if (arr[i].d == arr[i + 1].d) continue;

                    while (pq.Count > 0 && curDay > arr[i + 1].d)
                    {

                        curDay--;
                        ret += pq.Dequeue();
                    }

                    curDay = arr[i + 1].d;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new (int p, int d)[n + 1];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
                }

                int ReadInt()
                {

                    int ret = 0;

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
// #include <cstdio>

constexpr int RSZ = 1<<16;
char rbuf[RSZ], *rbp;
constexpr char *rbp_end = rbuf + RSZ;
inline char ReadChar() {
    if(rbp == rbp_end) fread(rbp = rbuf, sizeof(char), RSZ, stdin);
    return *rbp++;
}
inline int ReadInt() {
    int ret = 0;
    for (char c = ReadChar(); c&0x10; ret = ret*10 + (c&0x0F), c = ReadChar());
    return ret;
}

// #include <queue>
// #include <algorithm>
using namespace std;
typedef short s16;
// #define FOR(val, s_v, e_v) for(s16 val=(s_v); val<(e_v); ++val)
struct node { s16 cost, day; };
bool cmp(node &s1, node &s2) {
    return s1.day < s2.day;
}
priority_queue<s16, vector<s16>, greater<s16>> pq;

int main() {
    fread(rbp = rbuf, sizeof(char), RSZ, stdin);
    s16 n = ReadInt();
    
    node arr[10001];
    FOR(i, 0, n) {
        arr[i].cost = ReadInt();
        arr[i].day = ReadInt();
    }
    sort(arr, arr+n, cmp);
    FOR(i, 0, n) {
        pq.push(arr[i].cost);
        while(arr[i].day < pq.size()) {
            pq.pop();
        }
    }
    int sum = 0;
    while(!pq.empty()) {
        sum += pq.top(); pq.pop();
    }
    printf("%d\n", sum);
    return 0;
}

#endif
}
