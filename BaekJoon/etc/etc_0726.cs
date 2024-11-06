using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 25
이름 : 배성훈
내용 : 2048 (Easy)
    문제번호 : 12100번

    구현, 백트래킹, 시뮬레이션 문제다
    시행해도 최대값이 감소하는 경우는 없으므로 최대한 시행할 수 있는만큼 시행한다
    그래서 최대값 비교는 시행할 수 있는 만큼 다 하고 찾았다

    명령어만 기록하고, 최대 명령횟수까지 되면 일을 시행했다
    시간은 104ms가 나온다

    다른 사람의 경우를 보니 이차원 배열로 횟수를 저장해서 시행한다
    Hard버전은 다른 사람 아이디어를 인용해서 제출해봐야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0726
    {

        static void Main726(string[] args)
        {

            StreamReader sr;
            int size;
            int[][] board;
            int[] op;
            int[][] calc;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                ret = 0;

                DFS(0);

                Console.WriteLine(ret);
            }

            void Move(int _op)
            {

                switch (_op)
                {

                    case 1:
                        UP();
                        return;

                    case 2:
                        DOWN();
                        return;

                    case 3:
                        LEFT();
                        return;

                    case 4:
                        RIGHT();
                        return;

                    default:
                        return;
                }
            }

            void UP()
            {

                MoveUp();

                AddUp();

                MoveUp();
            }

            void DOWN()
            {

                MoveDown();

                AddDown();

                MoveDown();
            }

            void LEFT()
            {

                MoveLeft();

                AddLeft();

                MoveLeft();
            }

            void RIGHT()
            {

                MoveRight();

                AddRight();

                MoveRight();
            }

            void SetBoard()
            {

                for (int i = 0; i < size; i++)
                {

                    for (int j = 0; j < size; j++)
                    {

                        calc[i][j] = board[i][j];
                    }
                }
            }

            void MoveUp()
            {

                for (int c = 0; c < size; c++)
                {

                    int idx = 0;
                    for (int r = 0; r < size; r++)
                    {

                        if (calc[r][c] == 0) continue;
                        if (idx == r) idx++;
                        else
                        {

                            calc[idx++][c] = calc[r][c];
                            calc[r][c] = 0;
                        }
                    }
                }
            }

            void MoveDown()
            {

                for (int c = 0; c < size; c++)
                {

                    int idx = size - 1;
                    for (int r = size - 1; r >= 0; r--)
                    {

                        if (calc[r][c] == 0) continue;
                        if (idx == r) idx--;
                        else
                        {

                            calc[idx--][c] = calc[r][c];
                            calc[r][c] = 0;
                        }
                    }
                }
            }

            void MoveLeft()
            {

                for (int r = 0; r < size; r++)
                {

                    int idx = 0;
                    for (int c = 0; c < size; c++)
                    {

                        if (calc[r][c] == 0) continue;
                        if (c == idx) idx++;
                        else
                        {

                            calc[r][idx++] = calc[r][c];
                            calc[r][c] = 0;
                        }
                    }
                }
            }

            void MoveRight()
            {

                for (int r = 0; r < size; r++)
                {

                    int idx = size - 1;
                    for (int c = size - 1; c >= 0; c--)
                    {

                        if (calc[r][c] == 0) continue;
                        if (idx == c) idx--;
                        else
                        {

                            calc[r][idx--] = calc[r][c];
                            calc[r][c] = 0;
                        }
                    }
                }
            }

            void AddUp()
            {

                for (int c = 0; c < size; c++)
                {

                    for (int r = 0; r < size - 1; r++)
                    {

                        if (calc[r][c] == 0) continue;

                        if (calc[r][c] == calc[r + 1][c])
                        {

                            calc[r + 1][c] = 0;
                            calc[r][c] *= 2;
                        }
                    }
                }
            }

            void AddDown()
            {

                for (int c = 0; c < size; c++)
                {

                    for (int r = size - 1; r > 0; r--)
                    {

                        if (calc[r][c] == 0) continue;

                        if (calc[r][c] == calc[r - 1][c])
                        {

                            calc[r - 1][c] = 0;
                            calc[r][c] *= 2;
                        }
                    }
                }
            }

            void AddLeft()
            {

                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size - 1; c++)
                    {

                        if (calc[r][c] == 0) continue;

                        if (calc[r][c] == calc[r][c + 1])
                        {

                            calc[r][c + 1] = 0;
                            calc[r][c] *= 2;
                        }
                    }
                }
            }

            void AddRight()
            {

                for (int r = 0; r < size; r++)
                {

                    for (int c = size - 1; c > 0; c--)
                    {

                        if (calc[r][c] == 0) continue;

                        if (calc[r][c] == calc[r][c - 1])
                        {

                            calc[r][c - 1] = 0;
                            calc[r][c] *= 2;
                        }
                    }
                }
            }

            void GetMax()
            {

                for (int r = 0; r< size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        ret = calc[r][c] <= ret ? ret : calc[r][c];
                    }
                }
            }

            void DFS(int _depth)
            {

                if (_depth == 5)
                {

                    SetBoard();
                    for (int i = 0; i < op.Length; i++)
                    {

                        Move(op[i]);
                    }

                    GetMax();
                    return;
                }

                for (int i = 1; i <= 4; i++)
                {

                    op[_depth] = i;
                    DFS(_depth + 1);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();

                board = new int[size][];
                calc = new int[size][];
                for (int r = 0; r < size; r++)
                {

                    calc[r] = new int[size];
                    board[r] = new int[size];
                    for (int c = 0; c < size; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                op = new int[5];
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
var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int N = int.Parse(sr.ReadLine());
int[,] map = new int[N, N];
int[,] map1 = new int[N, N];
int[,] map2 = new int[N, N];
int[,] map3 = new int[N, N];
int[,] map4 = new int[N, N];
int[,] map5 = new int[N, N];
for (int i = 0; i < N; ++i)
{
    string[] Input = sr.ReadLine().Split();
    for (int j = 0; j < N; ++j)
    {
        map[i, j] = int.Parse(Input[j]);
    }
}
int max = 0;
void BackTracking(int n)
{
    if (n == 5)
    {
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N; ++j)
            {
                max = Math.Max(max, map5[i, j]);
            }
        }
        return;
    }
    for (int i = 0; i < 4; ++i)
    {
        moveMap(n + 1, i);
        BackTracking(n + 1);
    }
}
void moveMap(int n, int k)
{
    int[,] before = ChooseMap(n - 1);
    int[,] NextMap = ChooseMap(n);
    List<int> list = new List<int>();
    if (k == 0) // Up
    {
        for (int j = 0; j < N; ++j)
        {
            int temp = 0;
            for (int i = 0; i < N; ++i)
            {
                if (before[i, j] == 0) continue;
                if (before[i, j] != temp)
                {
                    list.Add(before[i, j]);
                    temp = before[i, j];
                }
                else
                {
                    temp = 0;
                    list[list.Count - 1] += list[list.Count - 1];
                }
            }
            for (int i = 0; i < list.Count; ++i)
            {
                NextMap[i, j] = list[i];
            }
            for (int i = list.Count; i < N; ++i)
            {
                NextMap[i, j] = 0;
            }
            list.Clear();
        }
    }
    else if (k == 1) // Down
    {
        for (int j = N - 1; j >= 0; --j)
        {
            int temp = 0;
            for (int i = N - 1; i >= 0; --i)
            {
                if (before[i, j] == 0) continue;
                if (before[i, j] != temp)
                {
                    list.Add(before[i, j]);
                    temp = before[i, j];
                }
                else
                {
                    temp = 0;
                    list[list.Count - 1] += list[list.Count - 1];
                }
            }
            for (int i = 0; i < list.Count; ++i)
            {
                NextMap[N - 1 - i, j] = list[i];
            }
            for (int i = 0; i < N - list.Count; ++i)
            {
                NextMap[i, j] = 0;
            }
            list.Clear();
        }
    }
    else if (k == 2) // Left
    {
        for (int i = 0; i < N; ++i)
        {
            int temp = 0;
            for (int j = 0; j < N; ++j)
            {
                if (before[i, j] == 0) continue;
                if (before[i, j] != temp)
                {
                    list.Add(before[i, j]);
                    temp = before[i, j];
                }
                else
                {
                    temp = 0;
                    list[list.Count - 1] += list[list.Count - 1];
                }
            }
            for (int j = 0; j < list.Count; ++j)
            {
                NextMap[i, j] = list[j];
            }
            for (int j = list.Count; j < N; ++j)
            {
                NextMap[i, j] = 0;
            }
            list.Clear();
        }
    }
    else
    {
        for (int i = N - 1; i >= 0; --i)
        {
            int temp = 0;
            for (int j = N - 1; j >= 0; --j)
            {
                if (before[i, j] == 0) continue;
                if (before[i, j] != temp)
                {
                    list.Add(before[i, j]);
                    temp = before[i, j];
                }
                else
                {
                    temp = 0;
                    list[list.Count - 1] += list[list.Count - 1];
                }
            }
            for (int j = 0; j < list.Count; ++j)
            {
                NextMap[i, N - 1 - j] = list[j];
            }
            for (int j = 0; j < N - list.Count; ++j)
            {
                NextMap[i, j] = 0;
            }
            list.Clear();
        }
    }
}
int[,] ChooseMap(int k)
{
    if (k == 0) return map;
    else if (k == 1) return map1;
    else if (k == 2) return map2;
    else if (k == 3) return map3;
    else if (k == 4) return map4;
    else return map5;
}
BackTracking(0);
Console.WriteLine(max);
sr.Close();
#endif
}
