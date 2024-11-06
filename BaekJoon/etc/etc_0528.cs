using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : 맥주 마시면서 걸어가기
    문제번호 : 9205번

    BFS 문제다
    택시 거리가 1000 이하인 경우 간선으로 연결되어져 있다고 봤다
    그리고 노드가 100개 이하이므로 플로이드 워셜로 이어져 있는지 확인했다
    이어진 경우 YES, 안이어진 경우 NO로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0528
    {

        static void Main528(string[] args)
        {

            string YES = "happy\n";
            string NO = "sad\n";

            int MAX = 200;
            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int test = ReadInt();

            int[,] fw = new int[100, 100];
            (int x, int y)[] pos = new (int x, int y)[100 + 2];
            while(test-- > 0)
            {

                int len = ReadInt() + 2;

                for (int i = 0; i < len; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                }

                SetArr(len);
                FW(len);
                if (fw[0, len - 1] == MAX) sw.Write(NO);
                else sw.Write(YES);
            }

            sr.Close();
            sw.Close();

            bool ChkConn(int _idx1, int _idx2)
            {

                int dis = Math.Abs(pos[_idx1].x - pos[_idx2].x) + Math.Abs(pos[_idx1].y - pos[_idx2].y);
                return dis <= 1_000;
            }
            void SetArr(int _len)
            {

                for (int i = 0; i < _len; i++)
                {

                    for (int j = 0; j < _len; j++)
                    {

                        if (i == j) fw[i, j] = 0;
                        else fw[i, j] = MAX;
                    }
                }

                for (int i = 0; i < _len - 1; i++)
                {

                    for (int j = i + 1; j < _len; j++)
                    {

                        if (!ChkConn(i, j)) continue;
                        fw[i, j] = 1;
                        fw[j, i] = 1;
                    }
                }
            }
            void FW(int _len)
            {

                for (int mid = 0; mid < _len; mid++)
                {

                    for (int s = 0; s < _len; s++)
                    {

                        if (fw[s, mid] == MAX) continue;
                        int sTm = fw[s, mid];

                        for (int e = 0; e < _len; e++)
                        {

                            int mTe = fw[mid, e];
                            if (mTe == MAX || fw[s, e] <= mTe + sTm) continue;
                            fw[s, e] = sTm + mTe;
                        }
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ?  ret : -ret;
            }
        }
    }

#if other
using Point = System.ValueTuple<int, int>;
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

var t = ScanInt();
var nodes = new Point[100 + 1];
var visited = new bool[100 + 1];
for (int i = 0; i < t; i++)
{
    var n = ScanInt();
    var house = ScanPoint();
    for (var j = 0; j <= n; j++)
        nodes[j] = ScanPoint();
    var able = DPS(house);
    sw.WriteLine(able ? "happy" : "sad");
    Array.Fill(visited, false, 0, n);

    bool DPS(Point p)
    {
        for (int i = n; i >= 0; i--)
        {
            var newNode = nodes[i];
            if (!AbleGo(newNode, p) || visited[i]) continue;
            if (i == n) return true;
            visited[i] = true;
            if (DPS(newNode)) return true;
        }
        return false;
    }
}

bool AbleGo(Point p1, Point p2) =>
    Math.Abs(p1.Item1 - p2.Item1) + Math.Abs(p1.Item2 - p2.Item2) <= 1000;

Point ScanPoint() => (ScanInt(), ScanInt());

int ScanInt()
{
    int c = sr.Read(), n = 0;
    if (c == '-')
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n - c + '0';
        }
    else
    {
        n = c - '0';
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
    }
    return n;
}
#elif other2
using System;
using System.Linq;
using System.Collections.Generic;

namespace 백준_9205
{
    class Node
    {
        public int x;
        public int y;
        public Node(int a, int b)
        {
            x = a;
            y = b;
        }
    }
    class Program
    {
        // 맥주 한병을 마시면 50미터를 갈수 있다.
        // 맥주 박스 크기에 맞게 맥주는 20개 까지만 들고 갈수 있다.
        // 편의점에서 빈병을 버리고 맥주를 구매할수 있다.
        // 편의점을 나선 직후에도 50미터를 가지 전에 맥주 한병을 마셔야 한다.
        // 도착점에 도착하면 "happy", 더 이동할 수 없으면 "sad"를 출력한다.
        static int T,N;
        static int[] home, festival;
        static bool[] visted;
        static List<Node> store;
       
        static string bfs()
        {
            int beer = 20;
            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(new Node(home[0], home[1]));

            while (Q.Count != 0)
            {
                Node now = Q.Dequeue();
                if (Math.Abs(now.x- festival[0]) + Math.Abs(now.y - festival[1]) <= 1000)
                {
                    return "happy";
                }
                for (int i = 0; i < N; i++)
                {
                    if (visted[i]) continue;

                    if (Math.Abs(now.x - store[i].x) + Math.Abs(now.y - store[i].y) <= 1000)
                    {
                        Q.Enqueue(store[i]);
                        visted[i] = true;
                    }
                }
            }
            return "sad";
        }
        static void Main(string[] args)
        {
            int[] input;

            T = int.Parse(Console.ReadLine());

            for (int i=0; i<T; i++)
            {
                store = new List<Node>();
                N = int.Parse(Console.ReadLine()); // 편의점 개수
                home = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                for(int a=0; a<N; a++)
                {
                    input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    store.Add(new Node(input[0], input[1]));
                }
                festival = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                
                visted = new bool[N];

                Console.WriteLine(bfs());
            }
        }
    }
}

#endif
}
