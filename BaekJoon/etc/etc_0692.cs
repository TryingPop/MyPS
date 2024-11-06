using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 14
이름 : 배성훈
내용 : 상어 초등학교
    문제번호 : 21608번

    구현 문제다
    자리가 비어있을 경우 배치해야하는데, 있는 자리를 덮어씌워서 1% 에서 두 번 틀렸다
        3
        5 6 1 8 4
        6 7 4 8 2
        7 3 1 6 9
        4 1 6 9 7
        8 5 4 6 3
        2 6 4 3 7
        1 2 5 8 4
        9 4 7 5 6
        3 4 1 6 5

    해당 예제를 돌려보니
        6   7   4       9   7   4
        8   5   2   ->  8   5   2
        0   1   0       0   1   0
    6번 학생이 있는데 9번학생이 자리를 빼앗아 문제가 있음을 알았다
    이를 수정하니 이상없이 68ms에 통과했다

    아이디어는 다음과 같다
    학생을 배치할 때, 모든 좌표에 점수를 매겼다
    인접한 빈 공간은 칸당 1점, 인접한 친한 친구가 있으면 칸당 10점
    그리고 탐색을 행 0 번의 모든 열을, 행 1번의 모든 열을 ... 방법으로 탐색해서
    점수가 가장 높은 빈 자리가 나오면 바로 배치했다
*/

namespace BaekJoon.etc
{
    internal class etc_0692
    {

        static void Main692(string[] args)
        {

            StreamReader sr;
            int[][] arr;
            int[] orderToIdx;

            int len;
            int n;

            int[][] board;
            int[][] calc;

            int[] dirR;
            int[] dirC;

            int[] s;

            Solve();

            void Solve()
            {

                Input();

                SetPos();

                int ret = GetRet();

                Console.WriteLine(ret);
            }

            void SetPos()
            {

                orderToIdx = new int[len + 1];

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                for (int i = 0; i < len; i++)
                {

                    int max = 0;
                    orderToIdx[arr[i][0]] = i;

                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < n; c++)
                        {

                            if (board[r][c] != 0) continue;

                            int score = 0;
                            for (int dir = 0; dir < 4; dir++)
                            {

                                int nextR = r + dirR[dir];
                                int nextC = c + dirC[dir];

                                if (ChkInvalidPos(nextR, nextC)) continue;
                                if (board[nextR][nextC] == 0) score++;
                                else if (board[nextR][nextC] == arr[i][1]
                                         || board[nextR][nextC] == arr[i][2]
                                         || board[nextR][nextC] == arr[i][3]
                                         || board[nextR][nextC] == arr[i][4]) score += 10;
                            }

                            calc[r][c] = score;
                            max = max < score ? score : max;
                        }
                    }

                    bool record = false;
                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < n; c++)
                        {

                            if (calc[r][c] == max && !record && board[r][c] == 0)
                            {

                                board[r][c] = arr[i][0];
                                record = true;
                            }

                            calc[r][c] = 0;
                        }
                    }
                }
            }

            int GetRet()
            {

                s = new int[5] { 0, 1, 10, 100, 1000 };

                int ret = 0;
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        int cur = orderToIdx[board[r][c]];
                        int match = 0;
                        for (int dir = 0; dir < 4; dir++)
                        {

                            int nextR = r + dirR[dir];
                            int nextC = c + dirC[dir];

                            if (ChkInvalidPos(nextR, nextC)) continue;

                            for (int i = 1; i < 5; i++)
                            {

                                if (arr[cur][i] != board[nextR][nextC]) continue;
                                match++;
                                break;
                            }
                        }

                        ret += s[match];
                    }
                }

                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                n = ReadInt();

                len = n * n;
                arr = new int[len][];
                for (int i = 0; i < len; i++)
                {

                    arr[i] = new int[5];
                    for (int j = 0; j < 5; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }

                board = new int[n][];
                calc = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    calc[i] = new int[n];
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
        }
    }

#if other
namespace ConsoleApp1
{
    internal class Program
    {
        static int[,] map;
        static int n;
        static int[,] like;
        static int[] dy = new int[] { 1, 0, -1, 0 };
        static int[] dx = new int[] { 0, 1, 0, -1 };
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            n = int.Parse(input.ReadLine());
            map = new int[n, n];
            like = new int[n * n + 1, 4];
            for(int i = 0; i < n * n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < 4; j++)                
                    like[temp[0], j] = temp[j + 1];                
                set(temp[0]);
            }
            int answer = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int count = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        int nr = i + dy[k];
                        int nc = j + dx[k];
                        if (nr < 0 || nc < 0 || nr == n || nc == n) continue;                        
                        if (check(map[i, j], map[nr,nc]))
                            count++;
                    }
                    if (count == 1)
                        answer++;
                    else if (count > 1)
                        answer += (int)Math.Pow(10,count-1);
                }
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
        static bool check(int me,int num)
        {
            for (int i = 0; i < 4; i++)            
                if (like[me, i] == num)
                    return true;
            
            return false;
        }
        static void set(int me)
        {
            int maxl = -1;
            int maxe = -1;
            int row = 0;
            int col = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (map[i, j] != 0) continue;
                    int like = 0;
                    int emp = 0;
                    for(int k = 0; k < 4; k++)
                    {
                        int nr = i + dy[k];
                        int nc = j + dx[k];
                        if (nr < 0 || nc < 0 || nr == n || nc == n) continue;
                        if (map[nr, nc] == 0)
                            emp++;
                        else if (check(me, map[nr,nc]))
                            like++;
                    }
                    if (like > maxl)
                    {
                        maxl = like;
                        maxe = emp;
                        row = i;
                        col = j;
                    }
                    else if(like == maxl && maxe < emp)
                    {
                        maxe = emp;
                        row = i;
                        col = j;
                    }
                }
            }
            map[row, col] = me;
        }
    }
}
#endif
}
