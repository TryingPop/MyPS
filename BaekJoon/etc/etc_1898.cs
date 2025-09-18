using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : 나룻배
    문제번호 : 2065번

    시뮬레이션, 큐 문제다.
    문제조건대로 시뮬레이션 돌려도 N x 3으로 충분하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1898
    {

        static void Main1898(string[] args)
        {

            int LEFT = 65_908;
            int m, t, n;

            Queue<int> left, right, cur;
            int[] time;
            
            Input();

            GetRet();

            void GetRet()
            {

                bool posIsLeft = true;
                int r = n;
                int curTime = 0;
                int[] ret = new int[n];

                while (true)
                {

                    // 내리기 시도
                    while (cur.Count > 0)
                    {

                        int idx = cur.Dequeue();
                        ret[idx] = curTime;
                        r--;
                    }

                    if (r == 0) break;

                    // 현재 자리에 태울 사람 있는지 확인
                    Queue<int> curPos = posIsLeft ? left : right;
                    int pick = 0;
                    while (curPos.Count > 0 && time[curPos.Peek()] <= curTime && pick < m)
                    {

                        cur.Enqueue(curPos.Dequeue());
                        pick++;
                    }

                    if (pick > 0)
                    {

                        curTime += t;
                        posIsLeft = !posIsLeft;
                        continue;
                    }

                    if (posIsLeft)
                    {

                        // 왼쪽에 있는경우
                        if (left.Count > 0 && right.Count > 0)
                        {

                            // 둘중에서 작은 쪽으로 이동하자!
                            if (time[left.Peek()] <= time[right.Peek()])
                                curTime = time[left.Peek()];
                            else
                            {

                                curTime = Math.Max(time[right.Peek()] + t, curTime + t);
                                posIsLeft = false;
                            }
                        }
                        else if (left.Count > 0)
                            // right가 빈경우
                            curTime = time[left.Peek()];
                        else
                        {

                            // left가 빈 경우
                            curTime = Math.Max(time[right.Peek()] + t, curTime + t);
                            posIsLeft = false;
                        }
                    }
                    else
                    {

                        if (left.Count > 0 && right.Count > 0)
                        {

                            if (time[right.Peek()] <= time[left.Peek()])
                                curTime = time[right.Peek()];
                            else
                            {

                                curTime = Math.Max(time[left.Peek()] + t, curTime + t);
                                posIsLeft = true;
                            }
                        }
                        else if (right.Count > 0)
                            curTime = time[right.Peek()];
                        else
                        {

                            curTime = Math.Max(time[left.Peek()] + t, curTime + t);
                            posIsLeft = true;
                        }
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                m = ReadInt();
                t = ReadInt();
                n = ReadInt();

                left = new(n);
                right = new(n);
                cur = new(m);
                time = new int[n];

                for (int i = 0; i < n; i++)
                {

                    int arrivalTime = ReadInt();
                    bool flag = ReadInt() == LEFT;

                    if (flag) left.Enqueue(i);
                    else right.Enqueue(i);
                    time[i] = arrivalTime;
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

        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NQueen
{
    class Class4
    {
        static void Main(string[] args)
        {
            int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Queue<int>[] v = new int[3].Select(x => new Queue<int> { }).ToArray();
            List<(int, int, int)> sch = new List<(int, int, int)> { };
            long[] arrival = new long[vs[2] + 1];
            for (int i = 0; i < vs[2]; i++)
            {
                string[] str = Console.ReadLine().Split();
                sch.Add((int.Parse(str[0]), i, str[1][0] == 'l' ? 0 : 1));
            }
            sch.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            sch.Add((100001, vs[2], 0));
            //Console.WriteLine(string.Join("\n", sch));
            long T = 0;
            long BT = 0;
            int Left = 0;
            bool added = false;
            bool departed = false;
            for (int i = 0; i < sch.Count; i++)
            {
            IL_01:;
                var (a, b, c) = sch[i];
                T = a;
                if (T > BT)
                {
                    if (departed)
                    {
                        while (v[2].Count > 0)
                            arrival[v[2].Dequeue()] = BT;
                        Left ^= 1;
                    }
                    while (v[Left].Count > 0 && v[2].Count < vs[0])
                        v[2].Enqueue(v[Left].Dequeue());
                    departed = false;
                    if (v.Any(x => x.Count > 0))
                    {
                        departed = true;
                        BT += vs[1];
                        goto IL_01;
                    }
                    BT = T;
                }
                v[c].Enqueue(b);
            }
            //Console.WriteLine(BT);
            Console.WriteLine(string.Join("\n", arrival.SkipLast(1)));
        }
    }
}
#elif other2
// #include <iostream>
// #include <fstream>
// #include <string>
// #include <vector>
// #include <math.h>
// #include <queue>
// #include <sstream>
// #include <algorithm>
// #include <map>
// #include <deque>
// #include <stack>
// #include <set>
// #include <climits>
// #include <functional>
using namespace std;
struct pcmp {
	bool operator()(pair<int, int> a, pair<int, int> b) {
		return a.first > b.first;
	}
};
struct cmp {
	bool operator()(long long a, long long b) {
		return a > b;
	}
};
int main() {
	cin.tie(0);
	cout.tie(0);
	ios_base::sync_with_stdio(false);

	int m, n, t;
	cin >> m >> t >> n;
	queue<pair<int,int>> pq[2];

	int cnt = 0;
	for (int i = 0; i < n; i++) {
		int time;
		string s;
		cin >> time >> s;
		if (s == "left") {
			pq[0].push({time,cnt++});
		}
		else {
			pq[1].push({time,cnt++});
		}
	}
	bool lr = false;
	int answer = 0;
	queue<int> q;

	vector<int> ans(n + 1, 0);
	while (!pq[0].empty() || !pq[1].empty()) {
		/*cout << "위치" << lr << "시간" << answer<<endl;
		for (int i = 0; i < cnt; i++) {
			cout << ans[i] << " ";
		}
		cout << endl;*/

		int flag=1;
		int ccnt = m;
		while (ccnt--&&!pq[lr].empty() && pq[lr].front().first <= answer) {
			flag = 0;
			ans[pq[lr].front().second] = answer + t;
			pq[lr].pop();
		}
		if (flag) {//아무도 안탐
			if (!pq[lr].empty() && !pq[!lr].empty()) {
				if (pq[lr].front().first <= pq[!lr].front().first) {
					answer = max(pq[lr].front().first,answer);
				}
				else {
					answer = max(pq[!lr].front().first,answer) + t;
					lr = !lr;
				}
			}
			else if (!pq[lr].empty()) {
				answer = max(pq[lr].front().first,answer);
			}
			else if (!pq[!lr].empty()) {
				answer = max(pq[!lr].front().first,answer) + t;
				lr = !lr;
			}
		}
		else {
			lr = !lr;
			answer += t;
		}
	}

	for (int i = 0; i < cnt; i++) {
		cout << ans[i] << "\n";
	}
	return 0;
}
#endif
}
