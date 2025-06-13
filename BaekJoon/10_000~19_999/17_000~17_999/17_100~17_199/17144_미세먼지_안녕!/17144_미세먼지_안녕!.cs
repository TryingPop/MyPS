using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 미세먼지 안녕!
    문제번호 : 17144번

    구현 시뮬레이션 문제다
    조건대로 시물레이션 되게 코드를 작성했다

    바람 회전이나, 미세먼지 퍼지는 부분을
    메서드로 하면 더 깔끔해질거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0179
    {

        static void Main179(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt(sr);
            int col = ReadInt(sr);
            int t = ReadInt(sr);
            Queue<(int r, int c, int val)> q = new(row * col);

            int[,] board = new int[row, col];
            int up = -1;
            int down = -1;

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int chk = ReadInt(sr);
                    board[r, c] = chk;
                    if (chk > 0) q.Enqueue((r, c, chk));
                    if (chk == -1)
                    {

                        if (up == -1)
                        {

                            up = r;
                        }
                        else
                        {

                            down = r;
                        }
                    }
                }
            }

            sr.Close();

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            int ret = 0;
            while(t-- > 0)
            {

                // 먼저 미세먼지 확장?
                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    int spreadVal = node.val / 5;
                    if (spreadVal == 0) continue;

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirY[i];
                        int nextC = node.c + dirX[i];

                        if (ChkInvalidPos(nextR, nextC, row, col) || board[nextR, nextC] == -1) continue;

                        board[nextR, nextC] += spreadVal;
                        board[node.r, node.c] -= spreadVal;
                    }
                }

                // 공기 청정기에 의한 미세먼지 이동
                // 먼저 윗부분!

                // 아래 이동
                for (int i = up - 2; i >= 0; i--)
                {

                    board[i + 1, 0] = board[i, 0];
                }

                // 왼쪽 이동
                for (int i = 1; i < col; i++)
                {

                    board[0, i - 1] = board[0, i];
                }

                // 위로 이동
                for (int i = 0; i < up; i++)
                {

                    board[i, col - 1] = board[i + 1, col - 1];
                }

                // 오른쪽 이동
                for (int i = col - 1; i >= 2; i--)
                {

                    board[up, i] = board[up, i - 1];
                }

                board[up, 1] = 0;
                
                // 공기 청정기 아랫부분
                // 위로 이동
                for (int i = down + 2; i < row; i++)
                {

                    board[i - 1, 0] = board[i, 0];
                }

                // 왼쪽 이동
                for (int i = 1; i < col; i++)
                {

                    board[row - 1, i - 1] = board[row - 1, i];
                }

                // 아래로 이동
                for (int i = row - 1; i >= down + 1; i--)
                {

                    board[i, col - 1] = board[i - 1, col - 1];
                }

                // 오른쪽 이동
                for (int i = col - 1; i >= 2; i--)
                {

                    board[down, i] = board[down, i - 1];
                }

                board[down, 1] = 0;

                // 먼지 확산 조사
                ret = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int chk = board[r, c];
                        if (chk > 0) 
                        { 
                            
                            q.Enqueue((r, c, chk));
                            ret += chk;
                        }
                    }
                }
            }

            Console.WriteLine(ret);
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _c < 0 || _r >= _row || _c >= _col) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            bool plus = true;
            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }

#if other
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int r = ScanInt(), c = ScanInt(), t = ScanInt();
var map = new int[r, c];
var cleaner = -1;
for (int i = 0; i < r; i++)
    for (int j = 0; j < c; j++)
    {
        var item = ScanInt();
        map[i, j] = item;
        if (item == -1 && cleaner == -1)
            cleaner = i;
    }
var newMap = new int[r, c];
var dirs = new ValueTuple<int, int>[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
while (t-- > 0)
{
    for (int i = 0; i < r; i++)
        for (int j = 0; j < c; j++)
            DiffuseDust(i, j);
    ReplaceMap();

    MoveDustRow(cleaner - 2, 0, 0);
    MoveDustColMinus(1, c - 1, 0);
    MoveDustRowMinus(1, cleaner + 1, c - 1);
    MoveDustCol(c - 2, 1, cleaner);
    map[cleaner, 1] = 0;

    MoveDustRowMinus(cleaner + 3, r - 1, 0);
    MoveDustColMinus(1, c - 1, r - 1);
    MoveDustRow(r - 2, cleaner + 1, c - 1);
    MoveDustCol(c - 2, 1, cleaner + 1);
    map[cleaner + 1, 1] = 0;

}

var dustSum = 0;
for (int i = 0; i < r; i++)
    for (int j = 0; j < c; j++)
        if (map[i, j] > 0)
            dustSum += map[i, j];
Console.Write(dustSum);

void ReplaceMap()
{
    (map, newMap) = (newMap, map);
    for (int i = 0; i < r; i++)
        for (int j = 0; j < c; j++)
            newMap[i, j] = 0;
    map[cleaner, 0] = -1;
    map[cleaner + 1, 0] = -1;
}

void MoveDustRow(int from, int to, int col)
{
    for (int i = from; i >= to; i--)
        map[i + 1, col] = map[i, col];
}

void MoveDustRowMinus(int from, int to, int col)
{
    for (int i = from; i <= to; i++)
        map[i - 1, col] = map[i, col];
}

void MoveDustCol(int from, int to, int row)
{
    for (int i = from; i >= to; i--)
        map[row, i + 1] = map[row, i];
}

void MoveDustColMinus(int from, int to, int row)
{
    for (int i = from; i <= to; i++)
        map[row, i - 1] = map[row, i];
}

void DiffuseDust(int i, int j)
{
    var ori = map[i, j];
    if (ori <= 0)
        return;

    var diffusion = 0;
    foreach ((var wr, var wc) in dirs)
    {
        int nr = i + wr, nc = j + wc;
        if (0 <= nr && nr < r &&
            0 <= nc && nc < c &&
            map[nr, nc] != -1)
        {
            newMap[nr, nc] += ori / 5;
            diffusion++;
        }
    }
    newMap[i, j] += ori - ori / 5 * diffusion;
}

int ScanInt()
{
    int c = sr.Read(), ret, sign;
    if (c != '-')
    {
        ret = c - '0';
        sign = 1;
    }
    else
    {
        ret = 0;
        sign = -1;
    }

    while (!((c = sr.Read()) is '\n' or ' ' or -1))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + (c - '0') * sign;
    }

    return ret;
}
#endif
}
