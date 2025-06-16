using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 11
이름 : 배성훈
내용 : 레이저 통신
    문제번호 : 6087번

    BFS 문제다.
    해당 지점에 도착하는데 방향을 최소로 꺾는 횟수를 찾아주면 된다.

    BFS로 접근했다. 단순히 벽인 경우나 맵을 지나가는 경우면 끊으면 된다.
    해당 문제는 먼저 방문한게 최소가 보장된다.
    그리고 방문한걸 만나면 끊는 형식으로 진행했는데, 아래와 같은 반례가 있다.

    입력
        4 5
        C..*
        ...*
        ...*
        *.**
        ...C

    정답
        2

    이에 방문한 곳은 건너뛰기로 해결했다.
    아래 코드는 1 x 1인 경우를 주의해야 한다.
    문제에서 C가 2개 이상 입력된다고 했고 항상 이어진다 했으므로
    따로 반례처리할 필요가 없다.
*/

namespace BaekJoon.etc
{
    internal class etc_1396
    {

        static void Main1396(string[] args)
        {

            int row, col;
            string[] board;
            int[][] visit;

            Input();

            GetRet();

            void GetRet()
            {

                bool flag = true;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] != 'C') continue;
                        if (flag)
                        {

                            flag = false;
                            BFS(r, c);
                        }
                        else 
                        { 
                            
                            Console.Write($"{visit[r][c] - 1}");
                            return;
                        }
                    }
                }

                void BFS(int _r, int _c)
                {

                    int[] dirR = { -1, 0, 1, 0 };
                    int[] dirC = { 0, -1, 0, 1 };
                    Queue<(int r, int c)> q = new(row * col);

                    q.Enqueue((_r, _c));

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();

                        int cur = visit[node.r][node.c];
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];
                            
                            while (ChkValidPos(nR, nC) && board[nR][nC] != '*')
                            {

                                if (visit[nR][nC] == 0)
                                {

                                    visit[nR][nC] = cur + 1;
                                    q.Enqueue((nR, nC));
                                }
                                nR += dirR[i];
                                nC += dirC[i];
                            }
                        }
                    }

                    bool ChkValidPos(int _r, int _c)
                        => 0 <= _r && 0 <= _c && _r < row && _c < col;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                col = ReadInt();
                row = ReadInt();

                board = new string[row];
                visit = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    visit[r] = new int[col];
                    board[r] = sr.ReadLine();
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
                        if (c == ' ' || c == '\n') return true;

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
}
