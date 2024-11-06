using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 10
이름 : 배성훈
내용 : 녹색 옷 입은 애가 젤다지?
    문제번호 : 4485번

    다익스트라 문제다
    처음에 dp로 접근하면 되지 않을까 접근했고
    상하좌우 이동이라 예제 2번째에서 막혔다

    그래서 양의 값이고 최소비용 경로를 찾는 문제이므로
    다익스트라로 접근했다

    방문 처리를 안하니 1번 메모리 초과로 틀렸다
    이후 방문처리하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0959
    {

        static void Main959(string[] args)
        {

            int INF = 34_567_890;

            StreamReader sr;
            StreamWriter sw;

            int[][] map;
            int[][] dis;
            bool[][] visit;

            PriorityQueue<(int r, int c), int> pq;
            int[] dirR, dirC;

            int size;
            Solve();
            void Solve()
            {

                Init();

                int test = 0;
                while ((size = ReadInt()) > 0)
                {

                    test++;
                    Input();

                    Dijkstra();

                    sw.Write($"Problem {test}: {dis[size - 1][size - 1]}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= size || _c >= size;
            }

            void Dijkstra()
            {


                dis[0][0] = map[0][0];
                pq.Enqueue((0, 0), dis[0][0]);

                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    if (visit[node.r][node.c]) continue;
                    visit[node.r][node.c] = true;

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || visit[nR][nC]) continue;
                        else if (dis[nR][nC] < dis[node.r][node.c] + map[nR][nC]) continue;

                        dis[nR][nC] = dis[node.r][node.c] + map[nR][nC];
                        pq.Enqueue((nR, nC), dis[nR][nC]);
                    }
                }
            }

            void Input()
            {

                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        map[r][c] = ReadInt();
                        dis[r][c] = INF;
                        visit[r][c] = false;
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pq = new(125 * 125 * 4);

                map = new int[125][];
                dis = new int[125][];
                visit = new bool[125][];

                for (int i = 0; i < 125; i++)
                {

                    map[i] = new int[125];
                    dis[i] = new int[125];
                    visit[i] = new bool[125];
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
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
int count = 1;
PriorityQueue<(int row, int col), int> pq = new PriorityQueue<(int, int), int>();
while (true)
{
    int size = int.Parse(Console.ReadLine()!);
    if (size == 0) break;
    int[,] grid = new int[size, size], dp = new int[size, size];
    //2차원 배열 입력
    for (int i = 0; i < size; i++)
    {
        int[] arr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
        for (int j = 0; j < size; j++) grid[i, j] = arr[j];
    }
    for (int i = 0; i < size; i++) for (int j = 0; j < size; j++) dp[i, j] = int.MaxValue;
    dp[0, 0] = grid[0, 0];
    pq.Clear();
    pq.Enqueue((0, 0), 0);
    while (pq.Count > 0)
    {
        var now = pq.Dequeue();
        //위로
        if (now.row > 0 && dp[now.row - 1, now.col] > dp[now.row, now.col] + grid[now.row - 1, now.col])
        {
            dp[now.row - 1, now.col] = dp[now.row, now.col] + grid[now.row - 1, now.col];
            pq.Enqueue((now.row - 1, now.col), dp[now.row - 1, now.col]);
        }
        //아래로
        if (now.row < size - 1 && dp[now.row + 1, now.col] > dp[now.row, now.col] + grid[now.row + 1, now.col])
        {
            dp[now.row + 1, now.col] = dp[now.row, now.col] + grid[now.row + 1, now.col];
            pq.Enqueue((now.row + 1, now.col), dp[now.row + 1, now.col]);
        }
        //왼쪽
        if (now.col > 0 && dp[now.row, now.col - 1] > dp[now.row, now.col] + grid[now.row, now.col - 1]) 
        {
            dp[now.row, now.col - 1] = dp[now.row, now.col] + grid[now.row, now.col - 1];
            pq.Enqueue((now.row, now.col - 1), dp[now.row, now.col - 1]);
        }
        //오른쪽
        if (now.col < size -1 && dp[now.row, now.col + 1] > dp[now.row, now.col] + grid[now.row, now.col + 1])
        {
            dp[now.row, now.col + 1] = dp[now.row, now.col] + grid[now.row, now.col + 1];
            pq.Enqueue((now.row, now.col + 1), dp[now.row, now.col + 1]);
        }
    }
    Console.WriteLine($"Problem {count++}: {dp[size - 1, size - 1]}");
}
#elif other2
// #include <cstdio>
// #define SIZE (125)

struct Data
{
    int y;
    int x;
    int luppy;
    bool operator < (const Data &r) const
    {
        return luppy <r.luppy;
    }
};

template <typename T>
struct Compare
{
    bool operator () (const T& lhs, const T &rhs) const
    {
        return lhs < rhs;
    }
};

template <typename T, typename Cmp>
class PriorityQueue
{
public:
    PriorityQueue()
    {
        hn =0;
    }
    void clear()
    {
        hn =0;
    }
    void push(const T&data)
    {
        int c = ++hn;
        for (; c > 1 && cmp(data , heap[c >> 1]); c >>= 1)
            heap[c] = heap[c >> 1];  // 부모의 값을 child값(nd) 위치로 내리기
        heap[c] = data;                // child값(nd)이 들어갈 위치를 찾음.
    }
    void pop()
    {
        Data nd = heap[hn];
        int c = 2;
        heap[hn--] = heap[1];
        for (; c <= hn; c <<= 1) {
            c += c < hn && cmp(heap[c + 1] , heap[c]); // 대표 child 선택하기
            if (cmp(heap[c] , nd))                     // 부모의 값(nd)가 child의 값(heap[c])보다 작다면
                heap[c >> 1] = heap[c];           // child의 값을 부모의 값으로 한다.
            else break;                           // 그렇지 않은경우 heap이 완성된경우이므로 종료
        }
        heap[c >> 1] = nd;                        // nd가 들어갈 위치를 찾음.
    }
    T top() const
    {
        return heap[1];
    }
    
    bool empty() const
    {
        return hn==0;
    }
private:
    T heap[325];
    int hn;
    Cmp cmp;
};

int dirx[]={0,0,-1,1};
int diry[] = {-1,1,0,0};

inline int get4thVal(const int& N)
{
    return N &(15);
}
int bfs(int N, short map[][SIZE])
{
    PriorityQueue<Data, Compare<Data>> pq;
    
    pq.push({0,0,get4thVal(map[0][0])});
    
    map[0][0] = (get4thVal(map[0][0]) << 4) | get4thVal(map[0][0]);
    
    while(!pq.empty())
    {
        auto dat = pq.top();
        pq.pop();
        
        if(dat.y == N-1 && dat.x == N-1)
        {
            return dat.luppy;
        }
        
        for(int i=0; i<4; i++)
        {
            int ny = dat.y + diry[i];
            int nx = dat.x + dirx[i];
            
            if(0<=ny &&ny <N && 0<=nx && nx <N )
            {
                if((map[ny][nx] >> 4) > get4thVal(map[ny][nx]) + dat.luppy) {
                    map[ny][nx] = (((get4thVal(map[ny][nx]) + dat.luppy)<<4)&map[ny][nx]) | get4thVal(map[ny][nx]);
                    pq.push({ny,nx, map[ny][nx] >> 4});
                }
            }
        }
    }
    return -1;
}

int main()
{
    int p = 0;
    short map[SIZE][SIZE];
    for(;;) {
        int N;
        scanf("%d", &N);
        if(N==0) break;
        
        for(int i=0; i<N; i++) {
            for(int j=0; j<N; j++) {
                int n;
                scanf(" %d", &n);
                map[i][j] = 0x7fff;
                map[i][j] = ((map[i][j] >> 4)<< 4)|n;
            }
        }
        
        printf("Problem %d: %d\n",++p, bfs(N, map));
    }
    
    return 0;
}


#endif
}
