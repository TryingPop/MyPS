using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 19
이름 : 배성훈
내용 : 불
    문제번호 : 5427번

    4179번(etc_0063) 문제의 아이디어를 재활용해서 쓸 수 있게 바꿨다
    약간 변형된 부분도 있다

    앞에서는 턴이 지나면 성공했는지 확인했는데,
    여기서는 입력 때와 휴먼이 움직일때 바로 확인한다

    입력에서 판별하기 위해 맨 윗줄, 중앙줄, 맨 아랫줄 3구간으로 나눴다
    그리고 맨 윗줄과 아랫줄에 휴먼이 입력되면 바로 골이라 하고 끝냈다
    중앙 줄의 경우는 왼쪽과 오른쪽 끝에 휴먼이 입력된 경우면 골이라 했다
    여기서, 1줄짜리 확인을 안해서 인덱스 에러 떴다(1줄짜리 경우가 있다!)

    그리고 입력에서 골이 아닌 경우 불과 사람을 한턴씩 확장시킨다
    불의 경우 빈땅과 사람이 있는 곳에 불을 먼저 붙이고, 
    다음으로 사람을 이동할 수 있는 장소인지 확인하고 사람을 이동시킨다
    그리고 이동장소가 끝라인이면 바로 탈출한다
    불부터 이동시켰기에 쓸 수 있는 방법이다

    해당 아이디어를 코드로 나타내서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0056
    {

        static void Main56(string[] args)
        {

            // 상수 역할? 변수
            string IMPOSSIBLE = "IMPOSSIBLE";
            int HUMAN = '@';
            int FIRE = '*';
            int EMPTY = '.';

            int MAX = 1_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            // 맵
            int[,] board = new int[MAX, MAX];
            // 이동에 쓸 변수
            Queue<(int row, int col)> human = new();
            Queue<(int row, int col)> fire = new();
            // 계산용
            Queue<(int row, int col)> calc = new();

            // 이동 방향
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            while(test-- > 0)
            {

                int col = ReadInt(sr);
                int row = ReadInt(sr);
                bool goal = false;

                human.Clear();
                fire.Clear();
                calc.Clear();

                // 입력 받기
                {

                    // 맨 윗줄
                    string str = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        board[0, c] = str[c];
                        if (str[c] == HUMAN) goal = true;
                        else if (str[c] == FIRE) fire.Enqueue((0, c));
                    }
                }

                // 중앙 줄
                // 2줄 이하일 땐, 탐색 안된다 
                for (int r = 1; r < row - 1; r++)
                {

                    string str = sr.ReadLine();

                    for (int c = 0; c < col; c++)
                    {

                        board[r, c] = str[c];
                        if (str[c] == HUMAN) human.Enqueue((r, c));
                        else if (str[c] == FIRE) fire.Enqueue((r, c));
                    }

                    if (str[0] == HUMAN || str[col - 1] == HUMAN) goal = true;
                }

                {

                    // 맨 아랫줄
                    // 맨 윗줄에서 1번 받았으므로 해당 줄은 없다!
                    // 2줄 이상일 때만 탐색한다
                    if (row > 1)
                    {

                        string str = sr.ReadLine();
                        for (int c = 0; c < col; c++)
                        {

                            board[row - 1, c] = str[c];
                            if (str[c] == HUMAN) goal = true;
                            else if (str[c] == FIRE) fire.Enqueue((row - 1, c));
                        }
                    }
                }

                // 끝점 이동 가능한 경우
                int turn = 0;
                while (!goal)
                {

                    // 턴 업하고 이동 시작
                    turn++;

                    // 불부터 이동
                    while(fire.Count > 0)
                    {

                        var node = fire.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.row + dirY[i];
                            int nextC = node.col + dirX[i];

                            if (ChkInvalidPos(nextR, nextC, row, col)) continue;

                            int chk = board[nextR, nextC];
                            if (chk == EMPTY || chk == HUMAN) 
                            { 
                                
                                board[nextR, nextC] = FIRE;
                                calc.Enqueue((nextR, nextC));
                            }
                        }
                    }

                    var temp = fire;
                    fire = calc;
                    calc = temp;

                    // 인간 이동
                    while(human.Count > 0)
                    {

                        var node = human.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.row + dirY[i];
                            int nextC = node.col + dirX[i];

                            if (ChkInvalidPos(nextR, nextC, row, col)) continue;

                            if (board[nextR, nextC] == EMPTY)
                            {

                                board[nextR, nextC] = HUMAN;
                                // 목적지 도달 시 더 이상 탐색 X
                                if (nextR == 0 || nextR == row - 1 || nextC == 0 || nextC == col - 1) 
                                { 
                                    
                                    goal = true;
                                    break;
                                }
                                calc.Enqueue((nextR, nextC));
                            }
                        }
                    }

                    temp = human;
                    human = calc;
                    calc = temp;

                    // 사람이 이동 못하면 사방이 막혔다고 볼 수 있다
                    if (human.Count == 0) break;
                }

                // 골에 도달할 경우
                if (goal) sw.WriteLine(turn + 1);
                // 막혀있는 경우
                else sw.WriteLine(IMPOSSIBLE);
            }

            sr.Close();
            sw.Close();
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
}
