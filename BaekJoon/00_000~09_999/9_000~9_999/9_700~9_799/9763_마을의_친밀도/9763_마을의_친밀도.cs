using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 7
이름 : 배성훈
내용 : 마을의 친밀도
    문제번호 : 9763번

    브루트포스 문제다
    폰으로 풀었는데, 30분 초과해서 2번 작성했다;2
*/

namespace BaekJoon.etc
{
    internal class etc_1097
    {

        static void Main1097(string[] args)
        {

            int n;
            (int x, int y, int z)[] pos;
            int[] dp1, dp2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                pos = new (int x, int y, int z)[n];

                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                sr.Close();
                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

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

            void GetRet()
            {

                dp1 = new int[n];
                dp2 = new int[n];

                Array.Fill(dp1, 10_000);
                Array.Fill(dp2, 10_000);
                for (int i = 0; i < n; i++)
                {

                    for (int j = i + 1; j < n; j++)
                    {

                        int dis = GetDis(i, j);
                        SetDp(i, dis);
                        SetDp(j, dis);
                    }
                }

                int ret = 50_000;
                for (int i = 0; i < n; i++)
                {

                    ret = Math.Min(ret, dp1[i] + dp2[i]);
                }

                Console.Write(ret);
                int GetDis(int _idx1, int _idx2)
                {

                    return Math.Abs(pos[_idx1].x - pos[_idx2].x)
                        + Math.Abs(pos[_idx1].y - pos[_idx2].y)
                        + Math.Abs(pos[_idx1].z - pos[_idx2].z);
                }

                void SetDp(int _idx, int _dis)
                {

                    if (dp2[_idx] <= _dis) return;
                    if (dp1[_idx] <= _dis) dp2[_idx] = _dis;
                    else
                    {

                        dp2[_idx] = dp1[_idx];
                        dp1[_idx] = _dis;
                    }
                }
            }
        }
    }

#if other
// #include<stdio.h>
// #include<algorithm>
// #include<stdlib.h>
using namespace std;
// #define M 1000000
typedef pair<int, int> pii;
pair<pii, int> a[10005];
int b[10005];
int c[10005];
int n;
int main()
{
//	n = 10000;
	scanf("%d",&n);
	int i, j, k, l;
	int ti, tj ,tk;
	for(i=0;i<n;i++)
	{
		scanf("%d %d %d",&j,&k,&l);
	//	j = k = i%100; l = i/100;
		a[i].first.first = j;
		a[i].first.second = k;
		a[i].second = l;
		b[i] = c[i] = M;
	}
	sort(a,a+n);
	for(i=0;i<n;i++)
	{
		for(j=i+1;j<n;j++)
		{
			ti = abs(a[i].first.first - a[j].first.first);
			tj = abs(a[i].first.second - a[j].first.second);
			tk = abs(a[i].second - a[j].second);
			if(ti >= c[i]) break;
			k = ti+tj+tk;
			if(c[i] > k)
			{
				c[i]=k;
				if(b[i] >c[i])
				{
					k=b[i];
					b[i]=c[i];
					c[i]=k;
				}
			}
		}
		for(j=i-1;j>=0;j--)
		{
			ti = abs(a[i].first.first - a[j].first.first);
			tj = abs(a[i].first.second - a[j].first.second);
			tk = abs(a[i].second - a[j].second);
			if(ti >= c[i]) break;
			k = ti+tj+tk;
			if(c[i] > k)
			{
				c[i]=k;
				if(b[i]>c[i])
				{
					k=b[i];
					b[i]=c[i];
					c[i]=k;
				}
			}
		}
	}
	int res=M;
	for(i=0;i<n;i++)
		res = min(res,b[i]+c[i]);
	printf("%d\n",res);
}
#endif
}
