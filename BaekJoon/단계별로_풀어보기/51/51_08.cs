using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 28
이름 : 배성훈
내용 : 격자 0 만들기
    문제번호 : 11495번

    최대 유량 문제다
    간선 연결 부분에서 여러 번 틀렸다
    행을 최우선으로 차선으로 열으로 오름차순 정렬한 뒤 칸에 id(인덱스)를 1씩 부여했다
    홀수와 짝수로 나눠서 풀면 되지 않을까 하고 홀수는 시작지점과 잇고, 짝수는 끝지점과 이은 뒤 해당 값만큼 유량을 설정했다
    예제에서는 2 * 2, 2 * 4에서는 희한하게 통과되었다?
    이후 고민해보니, 해당 간선 잘못 이은게 아닌가? 하는 생각이 들었고 인접한 걸로만 이어 제출하니 통과했다

    아이디어는 다음과 같다
    각 정점에 대해 노드를 값만큼 부여한다
    예를들어 다음과 같은 2 * 2 격자가 있다고 하자
        2 2
        3 5
    그러면 좌표 (1, 1)의 값은 2이므로 해당 좌표에 해당하는 노드 A는 2개 있다

    (1, 2)는 B 2개, (2, 1)은 C 3개, (2, 2)는 D 5개
    그리고 각 노드에대해 인접한거 끼리 간선을 잇고
    A - B, A - C, ... D - B, D - C (A - B는 A의 원소 2개에 대해 각각 B의 모든 원소에 간선을 잇는 것을 의미한다)
    이분 매칭시켜주면 2 * 1, 1 * 2로 제거할 수 있는 최대 격자의 수와 같다
    그리고 남은 수에 대해서는 일일히 1개씩 없애야한다
    이러면 A, D는 그룹 1이고 B, C는 그룹 2로 해서 매칭 시키면 이분 매칭 문제로 된다

    그리고 이를 네트워크 플로우로 관점을 전환해서 보면 (1, 1)인 A의 개수는 A에 흐르는 유량으로 볼 수 있고
    B, C, D 역시 마찬가지로 해당 노드에 흐르는 유량으로 볼 수 있다
    이분 매칭의 그룹 1은 source와 연결시키고, 그룹 2는 sink와 연결시키면 최대 유량이 최대 이분매칭과 같게 된다
    원래라면 A -> A 로 분할해서 해야하나 그룹 1 -> 그룹 2로만 흐르기에 A를 분할하는 대신에 그냥source에서 A로 가는 유량으로 바꿔도 된다


    그리고 unsafe로 시도하려고 봤는데
    unsafe 키워드를 사용하기 위해서는 오른쪽의 솔루션 탐색기에서
    프로젝트명 오른쪽 클릭 -> 속성(R) 선택 
    그리고 왼쪽에서 빌드 클릭한 뒤 일반에서 안전하지 않은 코드 체크하면 기본 세팅은 끝난다
    그런데 역간선 정보를 저장할 때 주소를 넘겨줘야하는데 이를 넘겨줄주 몰라서 구현못했다;

    Edge로 하니 256 -> 164ms 로 줄었다
    메모리는 59060kb -> 6704kb로 확 줄었다
*/

namespace BaekJoon._51
{
    internal class _51_08
    {

        struct Edge
        {

            private int to;
            private int inv;

            private int capacity;
            private int flow;

            public int To => to;
            public int Inv => inv;

            public bool CanFlow => capacity > flow;

            public int Remain => capacity - flow;

            public void Clear()
            {

                to = -1;
                inv = -1;

                flow = 0;
                capacity = 0;
            }

            public void Flow(int _n)
            {

                flow += _n;
            }

            public void Set(int _to, int _rev, int _capacity)
            {

                to = _to;
                inv = _rev;

                capacity = _capacity;
                flow = 0;
            }
        }

        static void Main8(string[] args)
        {

            int MAX_SIZE = 50;
            int INF = 100_000_000;

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int[][] board;
            int[][] idx;

            int source, sink;
            int[] dirR, dirC;

            Queue<int> q;
            int ret;

            int[] d, lvl;


#if !first

            List<int>[] line;
            Edge[] edges;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    ConnLine();

                    MaxFlow();

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[MAX_SIZE][];
                idx = new int[MAX_SIZE][];

                for (int r = 0; r < MAX_SIZE; r++)
                {

                    board[r] = new int[MAX_SIZE];
                    idx[r] = new int[MAX_SIZE];
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                int len = MAX_SIZE * MAX_SIZE;
                line = new List<int>[len + 2];
                edges = new Edge[MAX_SIZE * MAX_SIZE * 6];

                for (int i = 1; i <= len; i++)
                {

                    line[i] = new(5);
                }

                source = 0;
                sink = len + 1;
                line[0] = new(len);
                line[len + 1] = new(len);

                d = new int[MAX_SIZE * MAX_SIZE + 2];
                lvl = new int[MAX_SIZE * MAX_SIZE + 2];
                q = new(2 + MAX_SIZE * MAX_SIZE / 2);
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                ret = 0;
                int idxs = 0;

                line[source].Clear();
                line[sink].Clear();
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        board[r][c] = cur;
                        idx[r][c] = ++idxs;
                        line[idxs].Clear();
                        ret += cur;
                    }
                }
            }

            void ConnLine()
            {

                int lidx = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int from = idx[r][c];
                        if ((r + c) % 2 == 0)
                        {

                            // source와 연결
                            edges[lidx].Set(from, lidx + 1, board[r][c]);
                            edges[lidx + 1].Set(source, lidx, 0);

                            line[source].Add(lidx);
                            line[from].Add(lidx + 1);

                            lidx += 2;

                            for (int i = 0; i < 4; i++)
                            {

                                int nextR = r + dirR[i];
                                int nextC = c + dirC[i];

                                if (ChkInvalidPos(nextR, nextC)) continue;
                                int to = idx[nextR][nextC];

                                edges[lidx].Set(to, lidx + 1, INF);
                                edges[lidx + 1].Set(from, lidx, 0);

                                line[from].Add(lidx);
                                line[to].Add(lidx + 1);

                                lidx += 2;
                            }
                        }
                        else
                        {

                            edges[lidx].Set(sink, lidx + 1, board[r][c]);
                            edges[lidx + 1].Set(from, lidx, 0);

                            line[from].Add(lidx);
                            line[sink].Add(lidx + 1);

                            lidx += 2;
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            bool BFS()
            {

                Array.Fill(lvl, -1);
                lvl[source] = 0;

                q.Enqueue(source);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        Edge edge = edges[line[node][i]];

                        int next = edge.To;
                        if (lvl[next] == -1 && edge.CanFlow)
                        {

                            lvl[next] = lvl[node] + 1;
                            q.Enqueue(next);
                        }
                    }
                }

                return lvl[sink] != -1;
            }

            int DFS(int _n, int _flow)
            {

                if (_n == sink) return _flow;

                for (; d[_n] < line[_n].Count; d[_n]++)
                {

                    int eidx = line[_n][d[_n]];
                    int next = edges[eidx].To;

                    if (lvl[next] == lvl[_n] + 1 && edges[eidx].CanFlow)
                    {

                        int ret = DFS(next, Math.Min(edges[eidx].Remain, _flow));

                        if (ret > 0)
                        {

                            edges[eidx].Flow(ret);
                            int inv = edges[eidx].Inv;
                            edges[inv].Flow(-ret);

                            return ret;
                        }
                    }
                }

                return 0;
            }

            void MaxFlow()
            {

                while (BFS())
                {

                    Array.Fill(d, 0);

                    while (true)
                    {

                        int flow = DFS(source, INF);
                        if (flow == 0) break;

                        ret -= flow;
                    }
                }
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

#else


            List<int>[] line;

            int[,] c, f;
            int[] d, lvl;

            Solve();
            void Solve()
            {

                Init();
                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    ConnLine();

                    MaxFlow();

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                ret = 0;
                int idxs = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        board[r][c] = cur;
                        idx[r][c] = ++idxs;
                        ret += cur;
                    }
                }
            }

            void ConnLine()
            {

                source = 0;
                sink = row * col + 1;

                for (int i = 0; i <= sink; i++)
                {

                    line[i].Clear();
                }

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        int from = idx[i][j];
                        if ((i + j) % 2 == 0)
                        {

                            c[from, sink] = board[i][j];
                            c[sink, from] = 0;

                            f[from, sink] = 0;
                            f[sink, from] = 0;

                            c[source, from] = 0;
                            f[source, from] = 0;
                            f[from, source] = 0;

                            line[from].Add(sink);
                            line[sink].Add(from);
                        }
                        else
                        {

                            c[source, from] = board[i][j];
                            c[from, source] = 0;

                            c[from, sink] = 0;
                            f[source, from] = 0;
                            f[from, source] = 0;

                            line[from].Add(source);
                            line[source].Add(from);

                            for (int k = 0; k < 4; k++)
                            {

                                int nextR = i + dirR[k];
                                int nextC = j + dirC[k];

                                if (ChkInvalidPos(nextR, nextC)) continue;
                                int to = idx[nextR][nextC];
                                line[from].Add(to);
                                line[to].Add(from);

                                c[from, to] = INF;
                                c[to, from] = 0;

                                f[from, to] = 0;
                                f[to, from] = 0;
                            }
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void MaxFlow()
            {

                while (BFS())
                {

                    Array.Fill(d, 0);

                    while (true)
                    {

                        int flow = DFS(source, INF);
                        if (flow == 0) break;
                        ret -= flow;
                    }
                }
            }

            bool BFS()
            {

                Array.Fill(lvl, -1);
                lvl[source] = 0;

                q.Enqueue(source);

                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];

                        if (lvl[next] == -1 && c[node, next] - f[node, next] > 0)
                        {

                            lvl[next] = lvl[node] + 1;
                            q.Enqueue(next);
                        }
                    }
                }

                return lvl[sink] != -1;
            }

            int DFS(int _n, int _flow)
            {

                if (_n == sink) return _flow;

                for (; d[_n] < line[_n].Count; d[_n]++)
                {

                    int next = line[_n][d[_n]];

                    if (lvl[next] == lvl[_n] + 1 && c[_n, next] - f[_n, next] > 0)
                    {

                        int ret = DFS(next, Math.Min(c[_n, next] - f[_n, next], _flow));

                        if (ret > 0)
                        {

                            f[_n, next] += ret;
                            f[next, _n] -= ret;

                            return ret;
                        }
                    }
                }

                return 0;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput());

                board = new int[MAX_SIZE][];
                idx = new int[MAX_SIZE][];

                for (int r = 0; r < MAX_SIZE; r++)
                {

                    board[r] = new int[MAX_SIZE];
                    idx[r] = new int[MAX_SIZE];
                }

                line = new List<int>[MAX_SIZE * MAX_SIZE + 2];

                for (int i = 0; i < line.Length; i++)
                {

                    line[i] = new(1 + MAX_SIZE * MAX_SIZE / 2);
                }

                c = new int[line.Length, line.Length];
                f = new int[line.Length, line.Length];

                d = new int[MAX_SIZE * MAX_SIZE + 2];
                lvl = new int[MAX_SIZE * MAX_SIZE + 2];
                q = new(2 + MAX_SIZE * MAX_SIZE / 2);

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
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
#endif
    }
}
