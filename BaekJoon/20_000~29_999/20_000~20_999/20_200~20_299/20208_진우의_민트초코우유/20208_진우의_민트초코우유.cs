using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 1
이름 : 배성훈
내용 : 진우의 민트초코우유
    문제번호 : 20208번

    브루트포스, 백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1664
    {

        static void Main1664(string[] args)
        {

            int n, m, h, len;
            (int r, int c) home;
            (int r, int c)[] hell;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = DFS(home.r, home.c, m);
                Console.Write(ret);

                int DFS(int _curR, int _curC, int _h, int _dep = 0, int _state = 0)
                {

                    int ret = 0;
                    if (ChkHome()) ret = _dep;

                    for (int i = 0; i < len; i++)
                    {

                        if ((_state & (1 << i)) != 0) continue;
                        int chkDis = GetDis(hell[i].r, hell[i].c);

                        if (chkDis > _h) continue;
                        int chk = DFS(hell[i].r, hell[i].c, _h - chkDis + h, _dep + 1, _state | (1 << i));
                        ret = Math.Max(ret, chk);
                    }

                    return ret;

                    bool ChkHome()
                    {

                        int ret = GetDis(home.r, home.c);
                        return ret <= _h;
                    }

                    int GetDis(int _r, int _c)
                        => Math.Abs(_r - _curR) + Math.Abs(_c - _curC);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                h = ReadInt();

                hell = new (int r, int c)[10];
                home = (-1, -1);
                len = 0;

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 0) continue;
                        else if (cur == 1) home = (r, c);
                        else hell[len++] = (r, c);
                    }
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
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;

// #nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        (int y, int x) home = (0, 0);
        var mints = new List<(int y, int x)>();

        var nmh = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, m, h) = (nmh[0], nmh[1], nmh[2]);

        for (var y = 0; y < n; y++)
        {
            var vals = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            for (var x = 0; x < n; x++)
                if (vals[x] == 1)
                    home = (y, x);
                else if (vals[x] == 2)
                    mints.Add((y, x));
        }

        if (mints.Count == 0)
        {
            sw.WriteLine(0);
            return;
        }

        // mask, last, hp
        var dp = new bool[1 << mints.Count, mints.Count, 200];
        dp[0, 0, m] = true;

        var max = 0;

        for (var mask = 0; mask < (1 << mints.Count); mask++)
            for (var last = 0; last < mints.Count; last++)
                for (var health = 1; health < 150; health++)
                    for (var target = 0; target < mints.Count; target++)
                        if (dp[mask, last, health])
                        {
                            var newmask = mask | (1 << target);

                            if (mask == newmask)
                                continue;

                            var (cy, cx) = home;
                            if (mask != 0)
                                (cy, cx) = mints[last];

                            var cost = Math.Abs(mints[target].y - cy) + Math.Abs(mints[target].x - cx);
                            if (cost > health)
                                continue;

                            var newhealth = health - cost + h;
                            dp[newmask, target, newhealth] = true;

                            if (newhealth >= Math.Abs(mints[target].y - home.y) + Math.Abs(mints[target].x - home.x))
                                max = Math.Max(max, BitOperations.PopCount((uint)newmask));
                        }

        sw.WriteLine(max);
    }
}
#elif other2
// #include <cstdio>
// #include <algorithm>
using namespace std;

bool v[11];
int n, m, h, k, ans;
int yy[11], xx[11];
int hy, hx;

int g(int y, int x, int y2, int x2)
{
	return abs(y - y2) + abs(x - x2);
}

void f(int y, int x, int c, int p)
{
	if (g(y, x, hy, hx) <= p)
		ans = max(ans, c);
	pair<int, int> ls[10];
	int cnt = 0;
	for (int i = 0; i < k; i++)
	{
		int z = g(y, x, yy[i], xx[i]);
		if (!v[i] && z <= p)
			ls[cnt++] = { z, i };
	}
	sort(ls, ls + cnt);
    int lm = min(cnt, 2 + (c <= 1));
	for (int i = 0; i < lm; i++)
	{
		auto d = ls[i];
		v[d.second] = true;
		f(yy[d.second], xx[d.second], c + 1, p - d.first + h);
		v[d.second] = false;
	}
}

int main()
{
	int i, j;
	scanf("%d%d%d", &n, &m, &h);
	for (i = 0; i < n; i++)
		for (j = 0; j < n; j++)
		{
			int x;
			scanf("%d", &x);
			if (x == 1)
				hy = i, hx = j;
			else if (x == 2)
			{
				yy[k] = i;
				xx[k] = j;
				k++;
			}
		}
	f(hy, hx, 0, m);
	printf("%d", ans);
}
#endif
}
