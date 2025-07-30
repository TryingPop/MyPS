using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 30
이름 : 배성훈
내용 : 늑대 사냥꾼
    문제번호 : 2917번

    그래프 이론, 다익스트라 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1798
    {

        static void Main1798(string[] args)
        {

            int row, col, s, e;
            int[][] dis;
            int[] dirR, dirC;
            Queue<(int r, int c)> q;

            Input();

            BFS();

            GetRet();

            void GetRet()
            {

                int INF = 1_234_567_890;
                // 최소 거리를 Union - Find로 찾을 생각이다.
                int[] g = new int[row * col];
                int[] stk = new int[row * col];
                int[] ret = new int[row * col];

                for (int i = 0; i < g.Length; i++)
                {

                    g[i] = i;
                    ItoP(i, out int r, out int c);
                    ret[i] = dis[r][c];
                }

                PriorityQueue<(int f, int t, int d), int> pq = new(row * col * 4);
                // s에서 시작해 e로 가는 경로 찾기?...?
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = PtoI(r, c);

                        for (int dir = 0; dir < 4; dir++)
                        {

                            int nR = r + dirR[dir];
                            int nC = c + dirC[dir];

                            if (ChkInvalidPos(nR, nC)) continue;

                            int next = PtoI(nR, nC);
                            int treeDis = Math.Min(dis[r][c], dis[nR][nC]);

                            pq.Enqueue((cur, next, treeDis), -treeDis);
                        }
                    }
                }

                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();

                    int a = Find(node.f);
                    int b = Find(node.t);

                    if (a == b) continue;

                    Union(a, b, node.d);

                    if (Find(s) == Find(e)) break;
                }

                // 격자판 이동이므로 언제나 해가 존재
                Console.Write(ret[Find(s)]);

                void Union(int _f, int _t, int _dis)
                {

                    int min = _f < _t ? _f : _t;
                    int max = _f < _t ? _t : _f;

                    g[_t] = _f;
                    ret[_f] = Math.Min(ret[_f], _dis);
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while (_chk != g[_chk])
                    {

                        stk[len++] = _chk;
                        _chk = g[_chk];
                    }

                    while (len-- > 0)
                    {

                        g[stk[len]] = _chk;
                    }

                    return _chk;
                }
            }

            void BFS()
            {

                // 나무와의 최소 거리 찾기 BFS
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    int cur = dis[node.r][node.c];

                    for (int dir = 0; dir < 4; dir++)
                    {

                        int nR = node.r + dirR[dir];
                        int nC = node.c + dirC[dir];

                        if (ChkInvalidPos(nR, nC) || dis[nR][nC] != -1) continue;
                        dis[nR][nC] = cur + 1;
                        q.Enqueue((nR, nC));
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
                => _r < 0 || _c < 0 || _r >= row || _c >= col;

            void Input()
            {

                // 입력 함수 : 맵의 크기, 나무 상태와 초기 위치와 목적지 위치 변수에 할당
                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string temp = sr.ReadLine();
                {

                    string[] sp = temp.Split();
                    row = int.Parse(sp[0]);
                    col = int.Parse(sp[1]);
                }

                dis = new int[row][];
                s = -1;
                e = -1;

                q = new(row * col);

                for (int r = 0; r < row; r++)
                {

                    dis[r] = new int[col];
                    temp = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        if (temp[c] == '+') q.Enqueue((r, c));
                        else dis[r][c] = -1;

                        if (temp[c] == 'V') s = PtoI(r, c);
                        else if (temp[c] == 'J') e = PtoI(r, c);
                    }
                }
            }

            // 좌표를 인덱스
            int PtoI(int _r, int _c)
                => _r * col + _c;

            void ItoP(int _idx, out int _r, out int _c)
            {

                _r = _idx / col;
                _c = _idx % col;
            }
        }
    }
}
