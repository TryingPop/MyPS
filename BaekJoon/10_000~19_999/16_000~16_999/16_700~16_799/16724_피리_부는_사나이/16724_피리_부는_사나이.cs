using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : 피리 부는 사나이
    문제번호 : 16724번

    그래프 탐색, DFS 문제다.
    아이디어는 다음과 같다.
    외부로 벗어나는 경로는 주어지지 않는다고 한다.

    그래서 이동을 계속해서 이동할 수 있다.
    비둘기집 원리로 사이클은 항상 존재한다.
    
    이제 사이클로 향하는 곳에 안전장치를 설치한다.
    그러면 해당 사이클로 가는 길들은 모두 1개로 해결된다.

    그리고 사이클의 정의로 A 사이클에서 A 와 다른 B 사이클로는 이동할 수 없다.
    이렇게 사이클 갯수만큼 설치하면 최소가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1158
    {

        static void Main1158(string[] args)
        {

            int row, col;
            int[][] board;
            int[][] group;

            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dirR = new int[5] { 0, -1, 0, 1, 0 };
                dirC = new int[5] { 0, 0, -1, 0, 1 };

                int ret = 0;
                int g = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (group[r][c] != 0) continue;
                        g++;
                        int chk = Move(r, c);

                        if (chk == -1 || chk == g) ret++;
                    }
                }

                Console.Write(ret);

                int Move(int _r, int _c)
                {

                    int r = _r, c = _c;

                    while (true)
                    {

                        if (group[r][c] != 0) break;
                        group[r][c] = g;
                        
                        int dir = board[r][c];
                        r = r + dirR[dir];
                        c = c + dirC[dir];

                        if (ChkInvalidPos(r, c)) return -1;
                    }

                    return group[r][c];
                }

                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                group = new int[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    group[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        switch (cur)
                        {

                            case 'U':
                                board[r][c] = 1;
                                break;

                            case 'L':
                                board[r][c] = 2;
                                break;

                            case 'D':
                                board[r][c] = 3;
                                break;

                            default:
                                board[r][c] = 4;
                                break;
                        }
                    }

                    if (sr.Read() == '\r') sr.Read();
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
    }

#if other
// #include <iostream>

using namespace std;

int N, M;
char arr[1000][1001];

inline int calc(int y, int x)
{
    if(arr[y][x] == 1) {
        return 0;
    } else if(arr[y][x] == 0) {
        return 1;
    } else {
        int ret;
        char next =  arr[y][x];
        arr[y][x] = 0;
        switch(next) {
        case 'R':
            ret = calc(y, x + 1);
            break;
        case 'L':
            ret = calc(y, x - 1);
            break;
        case 'U':
            ret = calc(y - 1, x);
            break;
        case 'D':
            ret = calc(y + 1, x);
            break;
        }
        arr[y][x] = 1;
        return ret;
    }
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(0);
    cin >> N >> M;
    for(int i = 0; i < N; i++)
        cin >> arr[i];
    int ans = 0;
    for(int i = 0; i < N; i++) {
        for(int j = 0; j < M; j++) {
            ans += calc(i, j);
        }
    }
    cout << ans << '\n';
}
#endif
}
