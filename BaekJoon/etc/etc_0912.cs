using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 26 
이름 : 배성훈
내용 : 친구비
    문제번호 : 16562번

    분리 집합 문제다
    친구의 친구는 친구이므로 해당 사람을 친구로 사겼을 때
    친구가 되는 사람들을 하나의 그룹으로 생각할 수 있다
    그래서 유니온 파인드로 그룹을 나누고,
    그룹안에서 가장 싼 사람이 친구비 최소가 된다

    이렇게 모두와 친구하는데 비용을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0912
    {

        static void Main912(string[] args)
        {

            StreamReader sr;
            int[] group;
            int[] coins;
            int[] groupMin;
            int n, m, coin;
            int[] stack;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int INF = 10_001;
                groupMin = new int[n + 1];
                Array.Fill(groupMin, INF);

                for (int i = 1; i <= n; i++)
                {

                    int curGroup = Find(i);
                    groupMin[curGroup] = Math.Min(groupMin[curGroup], coins[i]);
                }

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (groupMin[i] == INF) continue;
                    ret += groupMin[i];
                }

                if (ret <= coin) Console.Write(ret);
                else Console.Write("Oh no");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                coin = ReadInt();

                coins = new int[n + 1];
                group = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    coins[i] = ReadInt();
                    group[i] = i;
                }

                stack = new int[n];
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();


                    f = Find(f);
                    b = Find(b);

                    if (f == b) continue;
                    Union(f, b);
                }

                sr.Close();
            }

            void Union(int _f, int _b)
            {

                if (_f < _b)
                {

                    int temp = _f;
                    _f = _b;
                    _b = temp;
                }

                group[_f] = _b;
            }

            int Find(int _chk)
            {

                int len = 0;
                while(_chk != group[_chk])
                {

                    stack[len++] = _chk;
                    _chk = group[_chk];
                }

                while(len-- > 0)
                {

                    group[stack[len]] = _chk;
                }

                return _chk;
            }

            int ReadInt()
            {

                int c, ret = 0;
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
var reader = new Reader();

int n = reader.NextInt();
int m = reader.NextInt();
int k = reader.NextInt();

var chingubi = new int[n + 1];
for (int i = 1; i <= n; i++)
    chingubi[i] = reader.NextInt();

var chingu = new DisjointSet(n + 1);
while (m-- > 0)
    chingu.Union(reader.NextInt(), reader.NextInt());

var toPay = new int[n + 1];
Array.Fill(toPay, 696969);
for (int i = 1; i <= n; i++)
{
    var par = chingu.Find(i);
    toPay[par] = Math.Min(toPay[par], chingubi[i]);
}

var total = toPay.Where(x => x != 696969).Sum();
Console.Write(total > k ? "Oh no" : total);

class DisjointSet
{
    private int size;

    private int[] parent;

    private int[] setSize;

    public DisjointSet(int capacity)
    {
        this.size = capacity;
        this.parent = new int[capacity];
        this.setSize = new int[capacity];

        for (int i = 0; i < size; i++)
        {
            this.parent[i] = i;
            this.setSize[i] = 1;
        }
    }

    public int Find(int node)
    {
        if (node == this.parent[node])
            return node;

        var result = this.Find(parent[node]);
        parent[node] = result;
        setSize[node] = setSize[result];

        return result;
    }

    public void Union(int n, int m)
    {
        int nRep = this.Find(n);
        int mRep = this.Find(m);

        if (nRep != mRep)
        {
            int size = this.setSize[nRep] + this.setSize[mRep];
            setSize[nRep] = size;
            setSize[mRep] = size;
        }

        parent[nRep] = mRep;
    }

    public int GetSetSize(int n)
    {
        int rep = this.Find(n);

        return setSize[rep];
    }

    public int[] GetSet(int n)
    {
        int rep = this.Find(n);

        var queue = new Queue<int>();
        var visited = new bool[this.size];

        queue.Enqueue(rep);
        visited[rep] = true;

        var set = new List<int>();
        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            set.Add(cur);

            for (int i = 0; i < size; i++)
            {
                if (this.parent[i] != cur || visited[i] == true)
                    continue;

                queue.Enqueue(i);
                visited[i] = true;
            }
        }

        return set.ToArray();
    }
}

class Reader
{
    StreamReader reader;

    public Reader()
    {
        BufferedStream stream = new(Console.OpenStandardInput());
        reader = new(stream);
    }

    public int NextInt()
    {
        bool negative = false;
        bool reading = false;

        int value = 0;
        while (true)
        {
            int c = reader.Read();

            if (reading == false && c == '-')
            {
                negative = true;
                reading = true;
                continue;
            }

            if ('0' <= c && c <= '9')
            {
                value = value * 10 + (c - '0');
                reading = true;
                continue;
            }

            if (reading == true)
                break;
        }

        return negative ? -value : value;
    }
}
#endif
}
