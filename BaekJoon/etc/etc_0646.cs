using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 링크와 스타트
    문제번호 : 15661번

    백트래킹, 비트마스킹, 브루트포스 문제다
    DFS에 무의식적으로 for문을 사용해 팀을 나눴는데
    구현을 잘못해서 이미 연산한 결과를 다시 탐색하는 시간초과코드가 되었다
    이로 6번 틀리고, 처음에는 for문이 아닌, 하나씩 ox로 했다
    이후에 for문을 먼저 하고 이후에 값 연산을 하니 이상없이 통과했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0646
    {

        static void Main646(string[] args)
        {

#if first
            StreamReader sr;
            int n;
            int[][] arr;
            int ret = 100_000;

            Solve();
            void Solve()
            {

                Input();

                DFS(1, 0);

                Console.WriteLine(ret);
            }

            void DFS(int _depth, int _team)
            {

                if (_depth == n)
                {

                    int ret1 = 0;
                    int ret2 = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if ((_team & (1 << i)) == 0)
                        {

                            for (int j = i + 1; j < n; j++)
                            {

                                if ((_team & (1 << j)) == 0) ret2 += arr[i][j] + arr[j][i];
                            }
                        }
                        else
                        {

                            for (int j = i + 1; j < n; j++)
                            {

                                if ((_team & (1 << j)) != 0) ret1 += arr[i][j] + arr[j][i];
                            }
                        }
                    }

                    int diff = ret1 - ret2;
                    diff = diff < 0 ? -diff : diff;
                    ret = diff < ret ? diff : ret;
                    return;
                }

                DFS(_depth + 1, _team | 1 << _depth);
                DFS(_depth + 1, _team);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                arr = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }

                sr.Close();
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
#else

            StreamReader sr;
            int n;
            int[][] arr;
            int ret = 100_000;

            Solve();
            void Solve()
            {

                Input();

                DFS(1, 0);

                Console.WriteLine(ret);
            }

            void DFS(int _s, int _team)
            {

                for (int i = _s; i < n; i++)
                {

                    DFS(i + 1, _team | 1 << i);
                }

                int ret1 = 0;
                int ret2 = 0;

                for (int i = 0; i < n; i++)
                {

                    if ((_team & (1 << i)) == 0)
                    {

                        for (int j = i + 1; j < n; j++)
                        {

                            if ((_team & (1 << j)) == 0) ret2 += arr[i][j] + arr[j][i];
                        }
                    }
                    else
                    {

                        for (int j = i + 1; j < n; j++)
                        {

                            if ((_team & (1 << j)) != 0) ret1 += arr[i][j] + arr[j][i];
                        }
                    }
                }

                int diff = ret1 - ret2;
                diff = diff < 0 ? -diff : diff;
                ret = diff < ret ? diff : ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                arr = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }

                sr.Close();
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

#if other
int n = int.Parse(Console.ReadLine());
var status = new int[n][];
int min = int.MaxValue;
var team = new bool[n];

for (int i = 0; i < n; i++)
    status[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();

Solve(0, 0);

Console.Write(min);

void Solve(int person, int depth)
{
    Calc();
    if (depth > n / 2 || min == 0) return; 

    for (int i = person; i < n; i++)
    {
        team[i] = true;
        Solve(i + 1, depth + 1);
        team[i] = false;
    }
}

void Calc() 
{
    int s = 0, l = 0;

    for (int i = 0; i < n - 1; i++)
        for (int j = i + 1; j < n; j++)
            if (team[i] && team[j])        s += status[i][j] + status[j][i];
            else if (!team[i] && !team[j]) l += status[i][j] + status[j][i];

    min = Math.Min(Math.Abs(s - l), min);
}
#endif
}
