using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 뱀
    문제번호 : 3190번

    구현 시뮬레이션 문제다
    시뮬레이션 문제는 구현 자체는 되는데 시간이 오래 걸린다
    꼬리 부분 처리를 잘못해서 한 번 틀렸다
    주어진 조건에 맞게 구현해서 이상없이 통과했다

    주된 아이디어는 다음과 같다
    보드 세팅 -> 이제 방향키 타이밍 입력부분인데
    여기서 방향키 입력 타이밍까지 시뮬레이션 한다
    그리고 해당 시간이 되면 시뮬레이션을 중지하고 다음 방향을 설정
    방향키 입력 간격간에 중간에 죽는 경우 바로 탈출한다

    만약 방향키 입력까지 살아있다면,
    죽을때까지 자동 진행을 한다

    그리고 죽는 시간을 출력
    해당 코드로 68ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0208
    {

        static void Main208(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[,] board = new int[n, n];

            int apples = ReadInt(sr);
            for (int i = 0; i < apples; i++)
            {

                int r = ReadInt(sr) - 1;
                int c = ReadInt(sr) - 1;

                board[r, c] = 1;
            }

            int move = ReadInt(sr);

            int curDir = 0;
            int curTime = 0;
            int curLen = 1;

            bool gameOver = false;

            Queue<(int r, int c)> body = new(n * n);

            int[] dirR = { 0, 1, 0, -1 };
            int[] dirC = { 1, 0, -1, 0 };

            Queue<(int r, int c)> head = new(1);
            head.Enqueue((0, 0));
            board[0, 0] = 2;
            for (int i = 0; i < move; i++)
            {

                int t = ReadInt(sr);
                int dir = sr.ReadLine()[0];
                
                while(curTime < t)
                {

                    // 방향키 입력 전까지 시뮬레이션
                    curTime++;
                    // 현재 머리 위치
                    var curHead = head.Dequeue();

                    // 현재 머리 위치를 몸통에 넣는다
                    body.Enqueue(curHead);

                    // 머리 이동 좌표
                    curHead.r += dirR[curDir];
                    curHead.c += dirC[curDir];

                    // 몸통이 있거나 벽에 부딪히면 사망
                    if (ChkInvalidPos(curHead.r, curHead.c, n) || board[curHead.r, curHead.c] == 2)
                    {

                        gameOver = true;
                        break;
                    }

                    // 사과 먹은경우 여기서는 안쓰지만 길이 증가
                    if (board[curHead.r, curHead.c] == 1) curLen++;
                    else
                    {

                        // 사과 못먹은 경우
                        // 꼬리 부분 제거
                        var tail = body.Dequeue();
                        // 몸통 없음으로 수정
                        board[tail.r, tail.c] = 0;
                    }

                    // 머리 부분 수정
                    board[curHead.r, curHead.c] = 2;
                    head.Enqueue(curHead);
                }

                // 해당 시간동안 구현했으니 이제 방향 전환
                if (dir == 'D') curDir = curDir == 3 ? 0 : curDir + 1;
                else curDir = curDir == 0 ? 3 : curDir - 1;

                // 죽으면 강제 종료
                if (gameOver) break;
            }
            sr.Close();

            // 방향 전환 끝난 후 살아있으면 죽을 때까지 자동 진행
            while (!gameOver)
            {

                curTime++;
                var curHead = head.Dequeue();

                body.Enqueue(curHead);

                curHead.r += dirR[curDir];
                curHead.c += dirC[curDir];

                if (ChkInvalidPos(curHead.r, curHead.c, n) || board[curHead.r, curHead.c] == 2)
                {

                    gameOver = true;
                    break;
                }

                if (board[curHead.r, curHead.c] == 1) curLen++;
                else if (body.Count > 0)
                {

                    var tail = body.Dequeue();
                    board[tail.r, tail.c] = 0;
                }

                board[curHead.r, curHead.c] = 2;
                head.Enqueue(curHead);
            }

            // 죽는 시간 출력
            Console.WriteLine(curTime);
        }

        static bool ChkInvalidPos(int _r, int _c, int _n)
        {

            if (_r < 0 || _r >= _n || _c < 0 || _c >= _n) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
