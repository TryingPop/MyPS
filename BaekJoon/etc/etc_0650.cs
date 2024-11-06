using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 29
이름 : 배성훈
내용 : 달이 차오른다, 가자.
    문제번호 : 1194번

    BFS, 비트마스킹 문제다
    50 * 50 사이즈에 서로 다른 키는 6개이다
    그래서 키 여부별로 맵을 만들면 64 * 2500 = 16만 크기이므로
    키 여부별로 맵을 만들었다

    방문 배열이 없으면 다른 BFS 문제에서 시간초과, 메모리 초과로 터지는 경험으로
    방문 배열역시 BFS로 만들었다
    해당 부분은 롱자료형의 비트마스킹으로도 될거 같으나 
    수도 커지고 익숙한 불 배열을 이용해 이전에 방문했는지 확인했다

    BFS 특성상 가장 먼저 도착하면 최단 경로가 보장되므로 바로 탈출했다
    이렇게 제출하니 76ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0650
    {

        static void Main650(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] map;
            int[][][] dp;
            bool[][][] visit;

            int ret = -1;
            Queue<(int r, int c, int key)> q;

            Solve();

            void Solve()
            {

                Input();
                BFS();

                Console.WriteLine(ret);
            }

            void BFS()
            {

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    int curTurn = dp[node.key][node.r][node.c];
                    int curKey = node.key;
                    bool find = false;
                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];
                        if (ChkInvalidPos(nextR, nextC) || visit[curKey][nextR][nextC]) continue;
                        visit[curKey][nextR][nextC] = true;

                        int nextTile = map[nextR][nextC];
                        if (nextTile == -100) continue;
                        if (nextTile == 100)
                        {

                            dp[curKey][nextR][nextC] = curTurn + 1;
                            find = true;
                            ret = curTurn + 1;
                            break;
                        }

                        int nextKey = curKey;
                        if (nextTile > 0)
                        {

                            nextTile--;
                            nextKey |= 1 << nextTile;
                        }

                        if (nextTile < 0)
                        {

                            nextTile = -nextTile - 1;
                            if ((curKey & (1 << nextTile)) == 0) continue;
                        }

                        dp[nextKey][nextR][nextC] = curTurn + 1;
                        q.Enqueue((nextR, nextC, nextKey));
                    }

                    if (find) q.Clear();
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                map = new int[row][];
                dp = new int[64][][];
                visit = new bool[64][][];
                q = new(row * col * 64);

                for (int i = 0; i < 64; i++)
                {

                    dp[i] = new int[row][];
                    visit[i] = new bool[row][];
                }

                for (int r = 0; r < row; r++)
                {

                    for (int i = 0; i < 64; i++)
                    {

                        dp[i][r] = new int[col];
                        visit[i][r] = new bool[col];
                    }

                    map[r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '0')
                        {

                            cur = 0;
                            q.Enqueue((r, c, 0));
                        }
                        else if (cur == '#') cur = -100;
                        else if (cur == '1') cur = 100;
                        else if (cur == '.') cur = 0;
                        else if ('a' <= cur && cur <= 'z') cur = cur - 'a' + 1;
                        else if ('A' <= cur && cur <= 'Z') cur = -(cur - 'A' + 1);

                        map[r][c] = cur;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static int[] dy = new int[] { 1, 0, -1, 0 };
        static int[] dx = new int[] { 0, 1, 0, -1 };
        static int n;
        static int m;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();

            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            n = arr[0]; m = arr[1];
            char[,] map = new char[n, m];
            int sr = 0; int sc = 0;
            for (int i = 0; i < n; i++)
            {
                string s = input.ReadLine();
                for (int j = 0; j < m; j++)
                {
                    if (s[j] == '0')
                    {
                        sr = i;
                        sc = j;
                    }
                    map[i, j] = s[j];
                }                
            }
            sb.Append($"{bfs(map, sr, sc)}\n");


            output.Write(sb);

            input.Close();
            output.Close();
        }
        static int bfs(char[,] map, int sr,int sc)
        {
            bool[,,] visit = new bool[(1 << 6) + 1, n, m];
            Queue<(int ,int, int, int)> q = new();
            q.Enqueue((0, sr, sc, 0));
            visit[0,sr, sc] = true;
            int res = -1;
            while (q.Count > 0)
            {
                (int key, int row, int col, int count) = q.Dequeue();

                if (map[row, col] == '1')
                {
                    res = count;
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    int nr = row + dy[i];
                    int nc = col + dx[i];
                    if (nr < 0 || nr == n || nc < 0 || nc == m || visit[key, nr, nc] || map[nr, nc] == '#') continue;
                    visit[key, nr, nc] = true;
                    if ('A' <= map[nr, nc] && map[nr, nc] <= 'F')
                    {
                        if ((key & 1 << map[nr, nc] - 'A') == 1 << map[nr, nc] - 'A')
                            q.Enqueue((key, nr, nc, count + 1));
                    }
                    else if ('a' <= map[nr, nc] && map[nr, nc] <= 'f')
                    {
                        int temp = key;
                        if ((key & 1 << map[nr, nc] - 'a') != 1 << map[nr, nc] - 'a')
                            temp |= 1 << map[nr, nc] - 'a';
                        q.Enqueue((temp, nr, nc, count + 1));
                        visit[temp, nr, nc] = true;
                    }
                    else
                        q.Enqueue((key, nr, nc, count + 1));
                }
            }
            return res;
        }
    }
}
#elif other2
string[] inputs = Console.ReadLine()!.Split();
int n = int.Parse(inputs[0]), m = int.Parse(inputs[1]);

Queue<(int, int, int)> queue = new();
string[] map = new string[n];
bool[,,] visited = new bool[n, m, 1 << 6];
for (int i = 0; i < n; i++)
{
    map[i] = Console.ReadLine()!;
    for (int j = 0; j < m; j++)
    {
        if (map[i][j] == '0')
        {
            visited[i, j, 0] = true;
            queue.Enqueue((i, j, 0));
        }
    }
}

int cnt = 0;
(int, int)[] dir = { (0, 1), (0, -1), (1, 0), (-1, 0) };
while (queue.Count > 0)
{
    int qCnt = queue.Count;
    while (qCnt-- > 0)
    {
        var (r, c, key) = queue.Dequeue(); 
        if (map[r][c] == '1')
        {
            Console.WriteLine(cnt);
            return;
        }

        foreach (var d in dir)
        {
            int tr = r + d.Item1;
            int tc = c + d.Item2;
            int ascii;
            if (0 <= tr && tr < n && 0 <= tc && tc < m && (ascii = map[tr][tc]) != '#' && !visited[tr, tc, key])
            {
                if (ascii >= 'a')
                {
                    int newKey = key | (1 << (ascii - 'a'));
                    visited[tr, tc, newKey] = true;
                    queue.Enqueue((tr, tc, newKey));
                }
                else if (ascii >= 'A')
                {
                    if ((key & (1 << (ascii - 'A'))) > 0)
                    {
                        visited[tr, tc, key] = true;
                        queue.Enqueue((tr, tc, key));
                    }
                }
                else
                {
                    visited[tr, tc, key] = true;
                    queue.Enqueue((tr, tc, key));
                }
            }
        }
    }
    cnt++;
}

Console.WriteLine(-1);
#elif other3
var reader = new Reader();
var dir = new (int x, int y)[4] { (1, 0), (-1, 0), (0, 1), (0, -1) };

const int KEY_A = 1 << 0;
const int KEY_B = 1 << 1;
const int KEY_C = 1 << 2;
const int KEY_D = 1 << 3;
const int KEY_E = 1 << 4;
const int KEY_F = 1 << 5;
var keyChars = new Dictionary<char, int> {
    { 'a', KEY_A }, { 'b', KEY_B }, { 'c', KEY_C }, { 'd', KEY_D }, { 'e', KEY_E }, { 'f', KEY_F }
};
var doorKeys = new Dictionary<char, char> {
    { 'A', 'a' }, { 'B', 'b' }, { 'C', 'c' }, { 'D', 'd' }, { 'E', 'e' }, { 'F', 'f' }
};

var (n, m) = (reader.NextInt(), reader.NextInt());
var (sx, sy) = (0, 0);
var map = new char[n, m];
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        map[i, j] = reader.NextChar();
        
        if (map[i, j] == '0')
            (sx, sy) = (i, j);
    }
}

var queue = new Queue<(int x, int y, int keys)>();
var visited = new int[1 << 6][,];
EnqueueBFS(sx, sy, 0, 1);

while (queue.Count > 0)
{
    var cur = queue.Dequeue();
    var tile = map[cur.x, cur.y];

    // Console.WriteLine((cur.x, cur.y, Convert.ToString(cur.keys, 2).PadLeft(6, '0')) + " " + visited[cur.keys][cur.x, cur.y]);

    if (tile == '1')
    {
        Console.Write(visited[cur.keys][cur.x, cur.y] - 1);
        return;
    }

    foreach (var d in dir)
    {
        var (dx, dy) = (cur.x + d.x, cur.y + d.y);
        if (dx < 0 || dx >= n || dy < 0 || dy >= m)
            continue;

        if (map[dx, dy] == '#')
            continue;

        var dTile = map[dx, dy];
        var dKeys = cur.keys;
        if (keyChars.ContainsKey(dTile))
            dKeys |= keyChars[dTile];

        if (doorKeys.ContainsKey(dTile) && (dKeys & keyChars[doorKeys[dTile]]) == 0)
            continue;

        var steps = visited[cur.keys][cur.x, cur.y] + 1;
        if (cur.keys == dKeys && visited[cur.keys][dx, dy] != 0 && visited[cur.keys][dx, dy] <= steps)
            continue;

        EnqueueBFS(dx, dy, dKeys, steps);
    }
}

Console.Write(-1);

void EnqueueBFS(int x, int y, int keys, int steps)
{
    queue.Enqueue((x, y, keys));
    (visited[keys] ??= new int[n, m])[x, y] = steps;
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
public char NextChar(){char v='\0';while(true){int c=R.Read();if(c!=' '&&c!='\n'&&c!='\r'){v=(char)c;break;}}return v;}
}
#endif
}
