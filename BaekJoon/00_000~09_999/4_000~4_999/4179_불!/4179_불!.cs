using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 18
이름 : 배성훈
내용 : 불!
    문제번호 : 4179번

    매턴 상황을 기록해서 통과했는지 못했는지 확인하며 풀었다
    불부터 이동하고 사람이동을 하는 방법도 이상없어 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0063
    {

        static void Main63(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[][] board = new int[row][];
            Queue<(int row, int col)> human = new();
            Queue<(int row, int col)> fire = new();
            for (int i = 0; i < row; i++)
            {

                board[i] = new int[col];
                string str = sr.ReadLine();
                for (int j = 0; j < col; j++)
                {

                    if (str[j] == '#') board[i][j] = -1;
                    else if (str[j] == '.') board[i][j] = 0;
                    else if (str[j] == 'J') 
                    { 
                        
                        board[i][j] = 1;
                        human.Enqueue((i, j));
                    }
                    else
                    {

                        board[i][j] = 2;
                        fire.Enqueue((i, j));
                    }
                }
            }

            sr.Close();


            Queue<(int row, int col)> calc = new();
            int turn = 0;
            // 골대 바로 옆인지 확인
            bool goal = ChkGoal(board, row, col);
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };
            // 매턴 마다 이동 가능한 상황과 불 상황 기록
            while (!goal)
            {

                // 불 이동
                while(fire.Count > 0)
                {

                    var node = fire.Dequeue();
                    
                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.row + dirY[i];
                        int nextC = node.col + dirX[i];

                        if (ChkInvalidPos(nextR, nextC, row, col)) continue;
                        if (board[nextR][nextC] != 1 && board[nextR][nextC] != 0) continue;

                        board[nextR][nextC] = 2;
                        calc.Enqueue((nextR, nextC));
                    }
                }

                // 돌려쓰기!
                var temp = fire;
                fire = calc;
                calc = temp;

                // 불 다음에 사람이 이동한다
                while (human.Count > 0)
                {

                    var node = human.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.row + dirY[i];
                        int nextC = node.col + dirX[i];

                        if (ChkInvalidPos(nextR, nextC, row, col)) continue;
                        if (board[nextR][nextC] != 0) continue;

                        board[nextR][nextC] = 1;
                        calc.Enqueue((nextR, nextC));
                    }
                }

                // 돌려쓰기!
                temp = human;
                human = calc;
                calc = temp;
                
                turn++;
                goal = ChkGoal(board, row, col);
                // 불길에 가로 막혀 못나가는 경우 강제 탈출
                if (human.Count == 0) 
                {

                    turn = -1;
                    break;
                }
            }

            // 불길에 가로막힌 경우
            if (turn < 0) Console.WriteLine("IMPOSSIBLE");
            // 탈출 완료한 경우
            else Console.WriteLine(turn + 1);
        }

        static bool ChkGoal(int[][] _board, int _maxRow, int _maxCol)
        {

            int chk1 = 0, chk2 = _maxCol - 1;
            
            for (int i = 0; i < _maxRow; i++)
            {

                if (_board[i][chk1] == 1 || _board[i][chk2] == 1) return true;
            }

            chk2 = _maxRow - 1;
            for (int i = 0; i < _maxCol; i++)
            {

                if (_board[chk1][i] == 1 || _board[chk2][i] == 1) return true;
            }

            return false;
        }

        static bool ChkInvalidPos(int _r, int _c, int _maxRow, int _maxCol)
        {

            if (_r < 0 || _r >= _maxRow) return true;
            if (_c < 0 || _c >= _maxCol) return true;

            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
namespace Baekjoon;

public class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        int maxR = ScanInt(sr), maxC = ScanInt(sr);
        var blocked = new bool[maxR, maxC];
        Queue<(int, int)>
            jihunBoundary = new(),
            fireBoundary = new();
        for (int r = 0; r < maxR; r++)
        {
            for (int c = 0; c < maxC; c++)
            {
                var ch = sr.Read();
                if (ch == '#')
                    blocked[r, c] = true;
                else if (ch == 'F')
                {
                    blocked[r, c] = true;
                    fireBoundary.Enqueue((r, c));
                }
                else if (ch == 'J')
                {
                    blocked[r, c] = true;
                    jihunBoundary.Enqueue((r, c));
                }
            }
            if (sr.Read() == '\r')
                sr.Read();
        }

        Queue<(int, int)>
            newJihunBoundary = new(),
            newFireBoundary = new();
        var dirs = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var timeLapse = 0;
        do
        {
            timeLapse++;
            while (fireBoundary.Count > 0)
            {
                (var jr, var jc) = fireBoundary.Dequeue();
                foreach ((var dr, var dc) in dirs)
                {
                    (var nr, var nc) = (jr + dr, jc + dc);
                    if (0 <= nr && nr < maxR &&
                        0 <= nc && nc < maxC && !blocked[nr, nc])
                    {
                        newFireBoundary.Enqueue((nr, nc));
                        blocked[nr, nc] = true;
                    }
                }
            }
            do
            {
                (var jr, var jc) = jihunBoundary.Dequeue();
                foreach ((var dr, var dc) in dirs)
                {
                    (var nr, var nc) = (jr + dr, jc + dc);
                    if (!(0 <= nr && nr < maxR &&
                        0 <= nc && nc < maxC))
                    {
                        Console.Write(timeLapse);
                        return;
                    }

                    if (!blocked[nr, nc])
                    {
                        newJihunBoundary.Enqueue((nr, nc));
                        blocked[nr, nc] = true;
                    }
                }
            } while (jihunBoundary.Count > 0);
            (jihunBoundary, newJihunBoundary) = (newJihunBoundary, jihunBoundary);
            (fireBoundary, newFireBoundary) = (newFireBoundary, fireBoundary);
        } while (jihunBoundary.Count > 0);
        Console.Write("IMPOSSIBLE");
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
