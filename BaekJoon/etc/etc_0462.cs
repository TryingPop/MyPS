using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : 로봇 시뮬레이션
    문제번호 : 2174번

    구현, 시뮬레이션 문제다
    로봇 명령을 일일히 시뮬레이션 돌리며 결론을 냈다
*/

namespace BaekJoon.etc
{
    internal class etc_0462
    {

        static void Main462(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt();
            int col = ReadInt();

            int[,] board = new int[row + 1, col + 1];
            int robotLen = ReadInt();
            int orderLen = ReadInt();

            (int r, int c, int dir)[] robot = new (int r, int c, int dir)[robotLen + 1];

            int[] dirR = { 1, 0, -1, 0 };
            int[] dirC = { 0, 1, 0, -1 };

            for (int i = 1; i <= robotLen; i++)
            {

                // 로봇의 초기 좌표 설정
                // 잘못 입력되는 경우는 없다
                int r = ReadInt();
                int c = ReadInt();
                int dir = ReadInt();

                if (dir == 'E' - '0') dir = 0;
                else if (dir == 'N' - '0') dir = 1;
                else if (dir == 'W' - '0') dir = 2;
                else dir = 3;

                robot[i] = (r, c, dir);
                board[r, c] = i;
            }

            string ret = "OK";
            for (int i = 1; i <= orderLen; i++)
            {

                // 명령 실행부분
                // 로봇 번호와 명령만 확인한다
                int ro = ReadInt();
                int order = ReadInt();

                if (order == 'L' - '0')
                {

                    // 왼쪽 회전
                    int dir = robot[ro].dir;
                    dir += ReadInt();
                    dir %= 4;
                    robot[ro].dir = dir;
                }
                else if (order == 'R' - '0') 
                {

                    // 오른쪽 회전
                    int dir = robot[ro].dir;
                    dir -= ReadInt();
                    dir %= 4;
                    robot[ro].dir = (4 + dir) % 4;

                }
                else
                {

                    // 이동
                    int move = ReadInt();
                    int curR = robot[ro].r;
                    int curC = robot[ro].c;
                    int curDir = robot[ro].dir;
                    board[curR, curC] = 0;

                    // 실패 여부 확인
                    bool fail = false;
                    for (int j = 0; j < move; j++)
                    {

                        // 한칸씩 전진하며 확인한다
                        curR = curR + dirR[curDir];
                        curC = curC + dirC[curDir];

                        if (ChkInvalidPos(curR, curC))
                        {

                            // 벽과 충돌
                            ret = $"Robot {ro} crashes into the wall";
                            fail = true;
                            break;
                        }
                        else if (board[curR, curC] > 0)
                        {

                            // 로봇과 충돌
                            ret = $"Robot {ro} crashes into robot {board[curR, curC]}";
                            fail = true;
                            break;
                        }
                    }

                    if (fail) break;
                    // 이상없이 이동
                    board[curR, curC] = ro;
                    robot[ro].r = curR;
                    robot[ro].c = curC;
                }
            }
            sr.Close();
            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r <= 0 || _c <= 0 || _r > row || _c > col) return true;
                return false;
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
var reader = new Reader();
var (w, h) = (reader.NextInt(), reader.NextInt());
var (n, m) = (reader.NextInt(), reader.NextInt());

var map = new int[h + 2, w + 2];
for (int i = 0; i < h + 2; i++) map[i, 0] = map[i, w + 1] = -1;
for (int i = 0; i < w + 2; i++) map[0, i] = map[h + 1, i] = -1;

var directions = new char[4] { 'N', 'E', 'S', 'W' };
var robots = new (int x, int y, char dir)[n + 1];
for (int i = 1; i <= n; i++)
{
    robots[i] = (reader.NextInt(), h - reader.NextInt() + 1, reader.NextChar());
    map[robots[i].y, robots[i].x] = i;
}

while (m-- > 0)
{
    var (num, cmd, repeats) = (reader.NextInt(), reader.NextChar(), reader.NextInt());

    var (dx, dy, dd) = robots[num];
    var (lastX, lastY) = (dx, dy);
    switch (cmd)
    {
        case 'L': dd = RotateAntiClockwise(dd, repeats); break;
        case 'R': dd = RotateClockwise(dd, repeats); break;
        case 'F': 
        try
        {
            (dx, dy) = MoveForward(dx, dy, dd, repeats);
        }
        catch (Exception e)
        {
            var msg = e.Message.Split();

            if (msg[0] == "CollideWall")
                Console.WriteLine($"Robot {num} crashes into the wall");
            else if (msg[0] == "CollideRobot")
                Console.WriteLine($"Robot {num} crashes into robot {msg[1]}");

            return;
        }

        break;
    }

    robots[num] = (dx, dy, dd);
    map[lastY, lastX] = 0;
    map[dy, dx] = num;
}

Console.WriteLine("OK");  

int DirectionToInt(char dir) => dir switch {
    'N' => 0, 'E' => 1, 'S' => 2, 'W' => 3
};

char RotateClockwise(char dir, int repeats) => directions[(DirectionToInt(dir) + repeats) % 4];
char RotateAntiClockwise(char dir, int repeats) => directions[((DirectionToInt(dir) - repeats) % 4 + 4) % 4];

(int x, int y) MoveForward(int x, int y, int dir, int repeats)
{
    var (dx, dy) = (0, 0);
    switch (dir)
    {
        case 'N': dy = -1; break;
        case 'E': dx = +1; break;
        case 'S': dy = +1; break;
        case 'W': dx = -1; break;
    }

    for (int i = 0; i < repeats; i++)
    {
        (x, y) = (x + dx, y + dy);

        if (map[y, x] == -1)
            throw new Exception("CollideWall");
        else if (map[y, x] != 0)
            throw new Exception("CollideRobot " + map[y, x]);
    }

    return (x, y);
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
public char NextChar(){char v='\0';while(true){int c=R.Read();if(c!=' '&&c!='\n'&&c!='\r'){v=(char)c;break;}}return v;}
}
#endif
}
