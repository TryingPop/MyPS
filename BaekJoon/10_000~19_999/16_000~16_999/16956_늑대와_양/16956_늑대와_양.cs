using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 17
이름 : 배성훈
내용 : 늑대와 양
    문제번호 : 16956번

    에드혹 문제?
    해를 만드는 문제이다

    최소 울타리로 만들 필요 없다
    그래서 빈땅에 모두 울타리를 설치했다;

    그리고 늑대 옆에 양이 있는지만 체크했다
    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0055
    {

        static void Main55(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int height = ReadInt(sr);
            int width = ReadInt(sr);

            char[,] board = new char[height, width];

            Queue<(int x, int y)> q = new Queue<(int x, int y)>();

            for (int i = 0; i < height; i++)
            {

                string str = sr.ReadLine();

                for (int j = 0; j < width; j++)
                {

                    char c = str[j];
                    // 늑대는 조사를 위해 위치 넣는다
                    if (c == 'W') q.Enqueue((j, i));
                    
                    // 빈땅이면 울타리 설치
                    else if (c == '.') c = 'D';
                    board[i, j] = c;
                }
            }
            sr.Close();

            bool chk = true;
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            // 모든 곳에 울타리 쳐졌으므로 늑대 옆에 양이 옆에 있는지만 확인하면 된다
            while(q.Count > 0)
            {

                var curPos = q.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int nextX = curPos.x + dirX[i];
                    int nextY = curPos.y + dirY[i];

                    if (ChkInValidPos(nextX, nextY, width, height)) continue;

                    if (board[nextY, nextX] != 'S') continue;

                    chk = false;
                    q.Clear();
                }
            }

            // 출력
            // 울타리 친 상태로 출력하면 된다
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(chk ? 1 : 0);
                if (chk)
                {

                    for (int i = 0; i < height; i++)
                    {

                        for (int j = 0; j < width; j++)
                        {

                            sw.Write(board[i, j]);
                        }
                        sw.Write('\n');
                    }
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }

        static bool ChkInValidPos(int _x, int _y, int _width, int _height)
        {

            if (_x < 0 || _x >= _width) return true;
            if (_y < 0 || _y >= _height) return true;

            return false;
        }
    }
}
