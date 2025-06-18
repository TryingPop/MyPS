using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 10
이름 : 배성훈
내용 : 미로 보수
    문제번호 : 30689번

    DFS, 함수형 그래프 문제다.
    못빠져 나가는 경우는 사이클에 있거나 사이클로 가는 경우다.
    그래서 사이클 노드 중 가장 값싼것 1개를 제거하는게 좋다.

    사이클 발견은 DFS로 찾았다.
    현재 탐색 번호를 idx라 하자.
    그러면 다음 방문에서 idx가 있다면 이는 사이클로 갔다는 말이다.
    그래서 다시 DFS로 탐색하는데, -1로 변형한다.
    -1인 경우까지 다시 가면 이는 사이클 발견이다.
    이렇게 사이클을 발견했다.

    해당 방법 말고도 SCC를 찾는 코사라주의 알고리즘 사용부분에서
    스택을 이용해 사이클을 발견하는 방법이 있다.
    스택에 이동한 좌표를 모두 저장한다.
    그리고 다음 방문한 곳에서 탐색 번호로 방문한 곳이 있다면 현재부터 다음 길이까지가 모두 사이클이라는 증거다.
*/

namespace BaekJoon.etc
{
    internal class etc_1691
    {

        static void Main1691(string[] args)
        {

            int row, col;
            int[][] val;
            string[] move;

            Input();

            GetRet();

            void GetRet()
            {

                int ERROR = 123;

                int ret = 0;
                int[][] visit = new int[row][];
                for (int i = 0; i < row; i++)
                {

                    visit[i] = new int[col];
                }

                int idx = 1;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (visit[r][c] != 0) continue;
                        DFS_MOVE(r, c);
                        idx++;
                    }
                }

                Console.Write(ret);

                void DFS_MOVE(int _r, int _c)
                {

                    if (ChkInvalidPos(_r, _c)) return;

                    if (visit[_r][_c] == 0)
                    {

                        visit[_r][_c] = idx;
                        char dir = move[_r][_c];
                        DFS_MOVE(_r + NextDirR(dir), _c + NextDirC(dir));
                    }
                    else if (visit[_r][_c] == idx)
                    {

                        int min = DFS_CYCLE(_r, _c);
                        ret += min;
                    }
                    else return;
                }

                int DFS_CYCLE(int _r, int _c)
                {

                    if (visit[_r][_c] == -1)
                        return val[_r][_c];
                    visit[_r][_c] = -1;
                    char dir = move[_r][_c];

                    return Math.Min(DFS_CYCLE(_r + NextDirR(dir), _c + NextDirC(dir)), val[_r][_c]);
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;

                int NextDirR(char _dir)
                {

                    switch (_dir)
                    {

                        case 'L' or 'R':
                            return 0;

                        case 'U':
                            return -1;

                        case 'D':
                            return 1;

                        default:
                            return ERROR;
                    }
                }

                int NextDirC(char _dir)
                {

                    switch (_dir)
                    {

                        case 'U' or 'D':
                            return 0;

                        case 'L':
                            return -1;

                        case 'R':
                            return 1;

                        default:
                            return ERROR;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                move = new string[row]; 
                for (int r = 0; r < row; r++)
                {

                    move[r] = sr.ReadLine();
                }

                val = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    val[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        val[r][c] = ReadInt();
                    }
                }



                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
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
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int visited[2001][2001];

int main()
{
    ios_base::sync_with_stdio(false); 
    cin.tie(NULL);
    
    int n, m;
    cin>>n>>m;

    vector<string> dir(n);
    vector<vector<int>> cost(n, vector<int>(m));

    for(int i = 0; i < n; ++i)
    {
        cin>>dir[i];
    }

    for(int i = 0; i < n; ++i)
    {
        for(int j = 0; j < m; ++j)
        {
            cin>>cost[i][j];
        }
    }

    memset(visited, 0, sizeof(visited));

    int ans = 0;
    int tries = 0;
    for(int i = 0; i < n; ++i)
    {
        for(int j = 0; j < m; ++j)
        {
            if(visited[i][j] == 0)
            {
                ++tries;

                int x = j, y = i;

                while((y >= 0 && y < n) && (x >= 0 && x < m))
                {
                    if(visited[y][x] == 0)
                    {
                        visited[y][x] = tries;
                        
                        if(dir[y][x] == 'L') --x;
                        else if(dir[y][x] == 'R') ++x;
                        else if(dir[y][x] == 'U') --y;
                        else if(dir[y][x] == 'D') ++y;
                    }
                    else if(visited[y][x] == tries) // 순환 - 사이클 내 최저비용 찾기
                    {
                        int minCost = cost[y][x];

                        int _x = x, _y = y;

                        do
                        {
                            minCost = min(minCost, cost[_y][_x]);

                            if(dir[_y][_x] == 'L') --_x;
                            else if(dir[_y][_x] == 'R') ++_x;
                            else if(dir[_y][_x] == 'U') --_y;
                            else if(dir[_y][_x] == 'D') ++_y;
                        } while(_x != x || _y != y);
                         
                        ans += minCost;
                        break;
                    }
                    else // 이미 mapping된 경로와 만남-추가비용 x
                    {
                        break;
                    }
                }
            }
        }
    }
    cout<<ans;
}

#endif
}
