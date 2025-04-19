using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 19
이름 : 배성훈
내용 : 강의실 배정
    문제번호 : 11000번

    그리디, 정렬, 우선순위 큐 문제다.
    정렬과 누적합 아이디어를 이용해 해결했다.
    해당 시간에 가장 많은 선이 포함된 갯수를 찾으면 된다.

    시작 시간 < 끝 시간이 보장되고 시작 시간 ≤ 끝 시간이면 이어서 강의가 가능하다.
    시간을 기준으로 배열을 저장한다.
    그리고 시작 시간과 끝 시간을 구분할 수 있게 초기에는 튜플 구조체에 시간과 end를 구분할 bool 변수를 추가했다.

    하지만 범위가 0 ~ 10억이므로 2배를 해도 int 범위이기에
    시간을 2배로하고 시작 시간만 + 1을 보정해줘도 된다.
    푸는 시간을 본 결과 튜플 구조체가 50% 이상 시간이 더 걸린다.

    이렇게 시간을 정렬한 뒤 이제 시간을 순차적으로 읽는다.
    누적합처럼 시작이면 +1, 끝이면 -1로 현재 갯수에 누적한다.
    그리고 시간이 갱신되면 현재 갯수를 최댓값과 비교해주면 된다.

    우선 순위 큐를 이용한 다른 사람의 풀이를 보니 시작 시간을 기준으로 정렬한다.
    즉, 우선순위 큐에 넣는다.

    그리고 끝시간을 다른 우선순위 큐에 넣어서 시간이 갱신될때마다 이미 지나간 끝시간을 빼내고
    우선순위 큐의 크기를 확인해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1556
    {

        static void Main1556(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();

            int[] arr = new int[n << 1];
            for (int i = 0; i < n; i++)
            {

                int s = ReadInt();
                int e = ReadInt();

                arr[i << 1] = s << 1 | 1;
                arr[i << 1 | 1] = e << 1;
            }

            Array.Sort(arr);

            int ret = 0;
            int cur = 0;
            int curTime = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {

                if (curTime != arr[i])
                {

                    ret = Math.Max(ret, cur);
                    curTime = arr[i];
                }

                if ((arr[i] & 1) == 1) cur++;
                else cur--;
            }

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
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
// #include <queue>
// #define bsz 1 << 16
using namespace std;


char buf[bsz];
int l = 0, r = 0;

inline char read() {
    if(l == r) { r = (int) fread(buf, 1, bsz, stdin); l = 0; }
    return buf[l++];
}

inline int rI() {
    int ret = 0, tmp = read();
    while(tmp >= 48) { ret = ret * 10 + tmp - 48; tmp = read(); }
    return ret;
}
struct sch{ int s, t; };

int main() {
    int n; scanf("%d\n", &n);
    int cnt = 1;
    
    vector<sch> input(n);
    priority_queue<int, vector<int>, greater<int>> pq;
    
    for(int i = 0; i < n; i++) input[i] = {rI(), rI()};
    sort(input.begin(), input.end(), [](sch &a, sch &b) { return a.s < b.s; });
    pq.push(0);
    
    for(auto now : input) {
        if(now.s < pq.top()) cnt++;
        else pq.pop();
        pq.push(now.t);
    }
    
    printf("%d\n", cnt);
    return 0;
}
#elif other2
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var n = ScanInt();
var list = new ClassTime[n];
for (int i = 0; i < n; i++)
{
    int s = ScanInt(), t = ScanInt();
    list[i] = new(s, t);
}
Array.Sort(list);

var heap = new PriorityQueue<int, int>();
foreach (var item in list)
{
    if (heap.TryPeek(out var firstClassEnd, out _) &&
        firstClassEnd <= item.Start)
        heap.Dequeue();
    heap.Enqueue(item.End, item.End);
}
Console.Write(heap.Count);

int ScanInt()
{
    int c, n = 0;
    while (!((c = sr.Read()) is ' ' or '\n'))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        n = 10 * n + c - '0';
    }
    return n;
}

record struct ClassTime(int Start, int End) : IComparable<ClassTime>
{
    public int CompareTo(ClassTime other)
    {
        return Start - other.Start;
    }
}
#endif
}
