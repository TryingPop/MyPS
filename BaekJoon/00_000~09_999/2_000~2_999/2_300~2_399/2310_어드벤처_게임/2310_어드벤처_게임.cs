using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 18
이름 : 배성훈
내용 : 어드벤처 게임
    문제번호 : 2310번

    탐색 문제다.
    우선순위 큐를 이용해 탐색했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1201
    {

        static void Main1201(string[] args)
        {

            int E = 21;
            int L = 28;
            int T = 36;

            int NOT_VISIT = -1;
            int MAX = 1_000;

            string YES = "Yes\n";
            string NO = "No\n";

            StreamReader sr;
            StreamWriter sw;

            PriorityQueue<int, int> pq;
            int n;
            List<int>[] edge;
            (int type, int cost)[] root;
            int[] dis;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                // 시작지점이 E라는 보장이 없어 0에서 시작한다.
                pq.Enqueue(0, 0);
                while (pq.Count > 0)
                {

                    int cur = pq.Dequeue();
                    int curCost = dis[cur];

                    for (int i = 0; i < edge[cur].Count; i++)
                    {

                        int next = edge[cur][i];
                        int nCost;
                        if (root[next].type == T)
                        {

                            if (curCost < root[next].cost) continue;
                            nCost = curCost - root[next].cost;
                        }
                        else if (root[next].type == L) nCost = Math.Max(curCost, root[next].cost);
                        else nCost = curCost;

                        if (nCost <= dis[next]) continue;
                        dis[next] = nCost;
                        pq.Enqueue(next, nCost);
                    }
                }

                if (dis[n] == NOT_VISIT) sw.Write(NO);
                else sw.Write(YES);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                edge = new List<int>[MAX + 1];
                edge[0] = new(1);
                edge[0].Add(1);
                for (int i = 1; i < MAX; i++)
                {

                    edge[i] = new(MAX);
                }

                root = new (int type, int cost)[MAX + 1];
                dis = new int[MAX + 1];
                pq = new(MAX * 4, Comparer<int>.Create((x, y) => y.CompareTo(x)));
            }

            bool Input()
            {

                n = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    root[i] = (ReadInt(), ReadInt());
                    edge[i].Clear();

                    int next;
                    while ((next = ReadInt()) != 0)
                    {

                        edge[i].Add(next);
                    }

                    dis[i] = NOT_VISIT;
                }

                return n != 0;
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApp3
{
    public enum RoomType
    {
        Empty,
        Lepricon,
        Troll,
    }
    class Room
    {
        public int index;
        public RoomType roomType;
        public List<int> path;
        public int value;

        public Room(int index, string[] roomInfo)
        {
            this.index = index;
            if (roomInfo[0] == "T")
                roomType = RoomType.Troll; 
            else if (roomInfo[0] == "L")
                roomType = RoomType.Lepricon;
            else
                roomType = RoomType.Empty;

            value = int.Parse(roomInfo[1]);
            path = new List<int>();
            for (int i = 2; i < roomInfo.Length - 1; i++)
                path.Add(int.Parse(roomInfo[i]) - 1);
        }
    }

    class Program
    {
        static StreamReader reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static Room[] rooms;
        static bool[] visited;
        static int gold = 0;
        static void DFS(Room next)
        {
            visited[next.index] = true;
            foreach(int nextRoom in next.path)
            {
                if (visited[nextRoom])
                    continue;

                Room target = rooms[nextRoom];
                switch(target.roomType)
                {
                    case RoomType.Lepricon:
                        if(gold < target.value)
                            gold = target.value;
                        break;
                    case RoomType.Troll:
                        if (gold < target.value)
                            continue;
                        gold -= target.value;
                        break;
                }

                DFS(target);
            }
        }

        static void Main()
        {
            while(true)
            {
                int roomSize = int.Parse(reader.ReadLine());
                if (roomSize == 0)
                    break;
                rooms = new Room[roomSize];
                visited = new bool[roomSize];

                for (int i = 0; i < roomSize; i++)
                {
                    Room room = new Room(i, reader.ReadLine().Split(' '));
                    rooms[i] = room;
                }

                DFS(rooms[0]);
                if (visited[roomSize - 1])
                    sb.AppendLine("Yes");
                else
                    sb.AppendLine("No");
            }
            
            writer.Write(sb);
            writer.Close();
        }
    }
}

#elif other2
// #include <iostream>
// #include <string.h>
// #include<algorithm>
// #include<sstream>
// #include<math.h>
// #include<cctype>
// #include<stdbool.h>
// #include<vector>
// #include<map>
// #include<queue>
// #include<stdbool.h>

using namespace std;

bool visited[1001];
int re;
int n;

typedef struct t {
	char c;
	int m;
	vector<int>ve;
}t;

vector<t> v;

void dfs(int here, int coin)
{
	if (re == 1)
		return;
	if (coin < 0)
		return;
	if (here == n)
	{
		re = 1;
		return;
	}
	for (int i = 0; i < v[here].ve.size(); i++)
	{
		int next = v[here].ve[i];
		if (!visited[next])
		{
			if (v[next].c == 'L')
			{
				if (coin < v[next].m)
					coin = v[next].m;
			}
			else if (v[next].c == 'T')
			{
				coin -= v[next].m;
			}
			if (coin < 0)
				return;
			visited[next] = 1;
			dfs(next, coin);
			visited[next] = 0;
		}
	}
}

int main(void)
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);

	while (1)
	{
		re = 0;
		cin >> n;
		if (n == 0)
			break;
		v.resize(n + 1);
		for (int i = 1; i <= n; i++)
		{
			char cn;
			cin >> cn;
			v[i].c = cn;
			int put_num;
			cin >> put_num;
			v[i].m = put_num;
			while (1) {
				cin >> put_num;
				if (put_num == 0)
					break;
				v[i].ve.push_back(put_num);
			}
		}

		if (v[1].c == 'E')
		{
			visited[1] = 1;
			dfs(1, 0);
		}
		else if (v[1].c == 'L')
		{
			visited[1] = 1;
			dfs(1, v[1].m);
		}
		else if (v[1].c == 'T')
		{
			re = 0;
		}

		if (re == 1)
		{
			cout << "Yes\n";
		}
		else
			cout << "No\n";

		v.clear();
		for (int i = 0; i <= n; i++)
		{
			visited[i] = 0;
		}
	}

	return 0;
}
#endif
}
