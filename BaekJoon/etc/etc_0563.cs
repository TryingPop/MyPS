using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 치킨 배달
    문제번호 : 15686번

    백트래킹, 구현, 브루트포스 문제다
    처음 읽을 때 치킨 집 m개가 될때까지 폐업에 초점을 맞춰 어려움을 느꼈다
    간단 브루트포스로 접근했을 때, 경우의 수가 엄청났게 많다고 느꼈고
    혹시 놓친 조건이 있는가 확인했다

    그러니, 집의 수가 2 * N 이하와 치킨 집 수가 m개 이상 13개 이하임이 있어
    폐업이 아닌 살릴 집으로 초점을 맞추게 되었다

    그러니 살리는 경우의 수가 최악인 경우 13C6 = 13C7 = 1716이고
    이에 해당방법이 적절해보였다
    그래서 치킨집에서 -> 집까지의 택시 거리들을 찾고

    해당 치킨 집을 살렸을 때, 치킨 거리를 계산하는 식으로 해보니
    자잘한 연산은 생략하고 정답 찾는것만 대략 100 * 13 * 1_716 < 2_000_000 이 나왔다
    그래서 이렇게 구하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0563
    {

        static void Main563(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n;
            int m;

            int[,] board;
            int[] calc1;
            int[] calc2;

            Solve();
            sr.Close();

            void Solve()
            {

                Input();
                int ret = DFS();
                Console.WriteLine(ret);
            }

            int DFS(int _depth = 0, int _s = 0)
            {

                int ret;
                if (_depth == m)
                {

                    ret = 0;
                    for (int i = 0; i < calc1.Length; i++)
                    {

                        for (int j = 0; j < calc2.Length; j++)
                        {

                            calc2[j] = Math.Min(calc2[j], board[calc1[i], j]);
                        }
                    }

                    for (int i = 0; i < calc2.Length; i++)
                    {

                        ret += calc2[i];
                        calc2[i] = 10_000;
                    }

                    return ret;
                }
                ret = 200_000;

                for (int i = _s; i < board.GetLength(0); i++)
                {

                    calc1[_depth] = i;
                    int calc = DFS(_depth + 1, i + 1);
                    ret = calc < ret ? calc : ret;
                }

                return ret;
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                (int r, int c)[] h = new (int r, int c)[2 * n];
                (int r, int c)[] c = new (int r, int c)[13];
                int hLen = 0;
                int cLen = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cur = ReadInt();
                        if (cur == 0) continue;

                        if (cur == 1) h[hLen++] = (i, j);
                        else c[cLen++] = (i, j);
                    }
                }

                board = new int[cLen, hLen];
                calc1 = new int[m];
                calc2 = new int[hLen];
                Array.Fill(calc2, 10_000);

                for (int i = 0; i < cLen; i++)
                {

                    for (int j = 0; j < hLen; j++)
                    {

                        board[i, j] = Dis(c[i], h[j]);
                    }
                }

            }

            int Dis((int r, int c) _g, (int r, int c) _t)
            {

                return Math.Abs(_g.r - _t.r) + Math.Abs(_g.c - _t.c);                
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
public static class PS
{
    private static int m;

    private static (int r, int c)[] house;
    private static (int r, int c)[] chicken;
    private static int houseCnt;
    private static int chickenCnt;

    private static int[] selected;
    private static int min;

    static PS()
    {
        string[] nm = Console.ReadLine().Split();

        int n = int.Parse(nm[0]);
        m = int.Parse(nm[1]);

        house = new (int, int)[2 * n];
        chicken = new (int, int)[13];

        int[] temp;
        houseCnt = 0;
        chickenCnt = 0;

        for (int i = 0; i < n; i++)
        {
            temp = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            for (int j = 0; j < n; j++)
            {
                switch (temp[j])
                {
                    case 1: house[houseCnt++] = (i, j); break;
                    case 2: chicken[chickenCnt++] = (i, j); break;
                    default: break;
                }
            }
        }

        selected = new int[m];
        min = int.MaxValue;
    }

    public static void Main()
    {
        DFS(0, 0);
        Console.Write(min);
    }

    private static void DFS(int depth, int start)
    {
        if (depth == m)
        {
            int temp;
            int dist;
            int sum = 0;

            for (int i = 0; i < houseCnt; i++)
            {
                dist = int.MaxValue;

                for (int j = 0; j < m; j++)
                {
                    temp = Dist(house[i], chicken[selected[j]]);

                    if (temp < dist)
                        dist = temp;
                }

                sum += dist;
            }

            if (sum < min)
                min = sum;

            return;
        }

        for (int i = start; i < chickenCnt; i++)
        {
            selected[depth] = i;
            DFS(depth + 1, i + 1);
        }
    }

    private static int Dist((int r, int c) h, (int r, int c) c) => Math.Abs(h.r - c.r) + Math.Abs(h.c - c.c);
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaekJoon.Gold
{
    internal class _15686
    {
        static int[] nm;
        static List<(int, int)> housePos = new();
        static List<(int, int)> chickenPos = new();
        static List<(int, int)> surviveChicken = new();
        static bool[] visited = new bool[13];
        static int ans = 50*50*13;

        static void Main(string[] args)
        {
            nm = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            for(int i = 0; i< nm[0]; i++)
            {
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                for(int j = 0; j < nm[0]; j++)
                {
                    if (arr[j] == 1)
                        housePos.Add((i, j));
                    else if (arr[j] == 2)
                        chickenPos.Add((i, j));
                }
            }

            ChickenBattle(0, 0);
            Console.WriteLine(ans);
        }

        static void ChickenBattle(int idx, int count)
        {
            if (count == nm[1])
                ChickenDist();

            for(int i = idx; i<chickenPos.Count; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    surviveChicken.Add((chickenPos[i].Item1, chickenPos[i].Item2));
                    ChickenBattle(i, count + 1);
                    surviveChicken.RemoveAt(surviveChicken.Count-1);
                    visited[i] = false;
                }
            }

        }

        static void ChickenDist()
        {
            int sum = 0;
            for(int i = 0; i<housePos.Count; i++)
            {
                int dist = 50* 50 * 13;
                for(int j = 0; j<nm[1]; j++)
                    dist = Math.Min(dist, Math.Abs(housePos[i].Item1 - surviveChicken[j].Item1) + Math.Abs(housePos[i].Item2 - surviveChicken[j].Item2));
                sum += dist;
            }

            ans = Math.Min(ans, sum);
        }
    }
}

#elif other3
int[] line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
int N = line[0];
int S = line[1];
int T = 0;
int[,] map = new int[N, N];
List<(int, int)> customer = new List<(int, int)>();
List<(int, int)> chicken = new List<(int, int)>();
for(int i = 0; i < N; i++)
{
    line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    for(int j = 0;j <N;j++)
    {
        map[i, j] = line[j];
        if (line[j] == 1)
            customer.Add((i, j));
        if (line[j] == 2)
        {
            map[i, j] = 0;
            chicken.Add((i, j));
            T++;
        }
    }
}
List<int[]> idxlist = new List<int[]>();

//System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();

// 경우의 수 생성
int[] arr = new int[S];
Recursion(arr, 0);

//Console.WriteLine(sw.ElapsedMilliseconds + " ms"); sw.Reset(); sw.Start();

int min = int.MaxValue;
//foreach (var item in idxlist)
//{
//    int[,] clone = (int[,])map.Clone();
//    // 정해진 갯수만 채운다.
//    foreach(var inner in item)
//        clone[chicken[inner].Item1, chicken[inner].Item2] = 2;

//    int sum = 0;
//    for (int idx = 0; idx < customer.Count; idx++)
//        sum += Dist(clone, idx);

//    if (min > sum)
//        min = sum;
//}
//Console.WriteLine(min);

//Console.WriteLine(sw.ElapsedMilliseconds + " ms"); sw.Reset(); sw.Start();

min = int.MaxValue;
foreach (var item in idxlist)
{
    int sum = 0;
    for(int i = 0; i < customer.Count; i++)
    {
        sum += NewDist(item, i);
    }
    if (min > sum)
        min = sum;

}
Console.WriteLine(min);

//Console.WriteLine(sw.ElapsedMilliseconds + " ms"); sw.Reset(); sw.Start();
return;

int NewDist(int[] idxlist, int custidx)
{
    int n = customer[custidx].Item1;
    int m = customer[custidx].Item2;

    int a;
    int b;
    int min = int.MaxValue;
    foreach(var item in idxlist)
    {
        a = n - chicken[item].Item1;
        b = m - chicken[item].Item2;
        if (a < 0)
            a *= -1;
        if (b < 0)
            b *= -1;
        if (min > a + b)
            min = a + b;
    }
    return min;
}

int Dist(int[,] map, int custidx)
{
    int n = customer[custidx].Item1;
    int m = customer[custidx].Item2;
    int[,] clone = (int[,])map.Clone();
    clone[n, m] = 10;
    int dist = 10;
    while (true)
    {
        dist++;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (clone[i,j] == dist-1)
                {
                    if (i != 0)
                    {
                        if (clone[i - 1, j] == 2)
                            return dist-10;
                        else if (clone[i - 1, j] < 10)
                            clone[i - 1, j] = dist;
                    }
                    if (i != N-1)
                    {
                        if (clone[i + 1, j] == 2)
                            return dist-10;
                        else if (clone[i + 1, j] < 10)
                            clone[i + 1, j] = dist;
                    }
                    if (j != 0)
                    {
                        if (clone[i, j-1] == 2)
                            return dist-10;
                        else if (clone[i, j-1] < 10)
                            clone[i, j-1] = dist;
                    }
                    if (j != N-1)
                    {
                        if (clone[i, j+1] == 2)
                            return dist-10;
                        else if (clone[i, j+1] < 10)
                            clone[i, j+1] = dist;
                    }
                }
            }
        }
    }
}

void Recursion(int[] ints, int count)
{
    if (count >= S)
    {
        idxlist.Add(ints);
        return;
    }
    for (int i = count == 0 ? 0 : ints[count - 1] + 1; i < T; i++)
    {
        int[] newints = (int[])ints.Clone();
        newints[count] = i;
        Recursion(newints, count + 1);
    }
}
#endif
}
