using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 14
이름 : 배성훈
내용 : 셔틀버스
    문제번호 : 9083번

    그리디 문제다
    아이디어를 다시 생각해야겠다;
    버스 하나 잡고 최대한 일시키기로 바꿔서 해본다!
    -> 이러니 통과되었다

    처음에는 시뮬레이션으로 해결하려고 했다
    시뮬레이션 돌리면서 버스가 부족하면 버스를 추가하는 식으로 했다
    예를들어 스케줄을 하나씩 확인하는데, 쉬는 버스가 있는지 확인했다
    쉬는 버스가 없는 경우 버스를 추가하는 식 으로 말이다
    그런데 25%에서 6번 틀렸다;

    이후 일을 시간순으로 정렬하고, 버스 한대로 일을 최대한 해결하는 식으로 바꿔서 하니 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0691
    {

        struct Work : IComparable<Work>
        {

            public int time;
            public int type;
            public bool end;

            public void Set(int _time, int _type) 
            {

                time = _time;
                type = _type;
                end = false;
            }

            public int CompareTo(Work _other)
            {

                return time.CompareTo(_other.time);
            }
        }

        static void Main691(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int t;

            Work[] arr;
            int len;

            Work bus;

            Solve();


            void Solve()
            {

                Init();
                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    int ret = 0;
                    while (true)
                    {

                        int s = -1;
                        // 안끝난 일 확인
                        for (int i = 0; i < len; i++)
                        {

                            if (arr[i].end) continue;
                            s = i;
                            arr[i].end = true;
                            break;
                        }

                        if (s == -1) break;

                        // 안끝난게 있다
                        // 해당 일을 시작으로 버스를 운행 시작
                        ret++;
                        bus.Set(arr[s].time + t, arr[s].type == 0 ? 1 : 0);

                        // 다른 운행 일을 할 수 있는지 확인!하고 최대한 일을 진행
                        for (int i = s + 1; i < len; i++)
                        {

                            if (arr[i].end) continue;

                            if (arr[i].type == bus.type)
                            {

                                if (bus.time > arr[i].time) continue;
                                bus.Set(arr[i].time + t, arr[i].type == 0 ? 1 : 0);
                                arr[i].end = true;
                            }
                            else
                            {

                                if (bus.time + t > arr[i].time) continue;
                                bus.Set(arr[i].time + t, arr[i].type == 0 ? 1 : 0);
                                arr[i].end = true;
                            }
                        }

                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput());

                arr = new Work[40];
                bus = new();
            }

            void Input()
            {

                t = ReadInt();
                int len1 = ReadInt();

                for (int i = 0; i < len1; i++)
                {

                    arr[i].Set(ReadTime(), 0);
                }

                int len2 = ReadInt();
                len = len1 + len2;
                for (int i = len1; i < len; i++)
                {

                    arr[i].Set(ReadTime(), 1);
                }

                Array.Sort(arr, 0, len);
            }

            int ReadTime()
            {

                int h = sr.Read() - '0';
                h = h * 10 + sr.Read() - '0';
                sr.Read();

                int m = sr.Read() - '0';
                m = m * 10 + sr.Read() - '0';

                if (sr.Read() == '\r') sr.Read();
                return h * 60 + m;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;

int t, d, n, m, x, y;

int main()
{
	cin>>t;
	while(t--)
	{
		vector<pair<int, int> > v;
		vector<int> p1, p2;
		cin>>d;
		cin>>n;
		for(int i=1;i<=n;i++)
		{
			scanf("%d:%d", &x, &y);
			v.push_back({x*60+y, 1});
		}
		cin>>m;
		for(int i=1;i<=m;i++)
		{
			scanf("%d:%d", &x, &y);
			v.push_back({x*60+y, 2});
		}
		sort(v.begin(), v.end());
		int ans=0;
		for(auto i : v)
		{
			if(i.second==1)
			{
				if(!p1.empty())
				{
					int cnt=0;
					if(p1[cnt]<=i.first)
					{
						while(cnt+1<p1.size() && p1[cnt+1]<=i.first) cnt++;
						p1.erase(p1.begin()+cnt);
						p2.push_back(i.first+d);
						goto skip;
					}
				}
				if(!p2.empty())
				{
					int cnt=0;
					if(p2[cnt]+d<=i.first)
					{
						while(cnt+1<p2.size() && p2[cnt+1]+d<=i.first) cnt++;
						p2.erase(p2.begin()+cnt);
						p2.push_back(i.first+d);
						goto skip;
					}
				}
				ans++;
				p2.push_back(i.first+d);
			}
			else
			{
				if(!p2.empty())
				{
					int cnt=0;
					if(p2[cnt]<=i.first)
					{
						while(cnt+1<p2.size() && p2[cnt+1]<=i.first) cnt++;
						p2.erase(p2.begin()+cnt);
						p1.push_back(i.first+d);
						goto skip;
					}
				}
				if(!p1.empty())
				{
					int cnt=0;
					if(p1[cnt]+d<=i.first)
					{
						while(cnt+1<p1.size() && p1[cnt+1]+d<=i.first) cnt++;
						p1.erase(p1.begin()+cnt);
						p1.push_back(i.first+d);
						goto skip;
					}
				}
				ans++;
				p1.push_back(i.first+d);
			}
			skip:;
		}
		cout<<ans<<"\n";
	}
}
#elif other2
import sys


t = int(sys.stdin.readline())
for _ in range(t):
    travel = int(sys.stdin.readline())
    times = []
    a = int(sys.stdin.readline())
    for _ in range(a):
        hh, mm = (int(x) for x in sys.stdin.readline().split(':'))
        times.append((60 * hh + mm, False))
    b = int(sys.stdin.readline())
    for _ in range(b):
        hh, mm = (int(x) for x in sys.stdin.readline().split(':'))
        times.append((60 * hh + mm, True))
    times.sort()
    buses = 0
    while times:
        buses += 1
        new_times = []
        last_time, last_location = 0, None
        for time, location in times:
            if time - last_time >= travel and last_location != location:
                last_time, last_location = time, location
            elif time - last_time >= 2 * travel:
                last_time, last_location = time, location
            else:
                new_times.append((time, location))
        times = new_times
    sys.stdout.write(f'{buses}\n')

#endif
}
