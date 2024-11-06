using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 24
이름 : 배성훈
내용 : 스도쿠
    문제번호 : 2580번

    해당 경우의 수 검색이 끝난 뒤에 값을 초기화 안해줬다
    그래서 1%에서 많이 틀리고 초기화를 하니 바로 맞췄다.    

    디버깅과 문제를 분석하니
    여기서는 가로 검색에는 초기화 안해도 이상이 없으나 
    초기화 안할 시 세로나 3 * 3 검색에서 문제가 생길 수 있음을 확인하고 수정했다
    
    문제 해결 25일에 다른 사람 풀이 살펴보고
    괜찮은거 있으면 코드 추가할 예정이다
    없으면 다음문제로 넘어간다!
*/

namespace BaekJoon._25
{
    internal class _25_06
    {

        static void Main6(string[] args)
        {

            // 스도쿠 판 생성
            int[,] board = new int[9, 9];

            StreamReader sr = new(Console.OpenStandardInput());
#if first
            // 현재 시간초과 난다
            // Stopwatch로 확인 결과 6000ms 걸린다..
            // 6초 ? 
            // 빈 좌표 
            Queue<int[]> emptyPos = new Queue<int[]>(30);



            // 판에 번호 입력
            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {

                    int n = 0, c;
                    while (true)
                    {

                        c = sr.Read();

                        // vs에서 엔터 키는 '\r\n'으로 출력된다!
                        if (c == '\r')
                        {
                            continue;
                        }

                        if (c == ' ' || c == '\n')
                        {

                            // 0 좌표만 따로 추출
                            if (n == 0)
                            {

                                emptyPos.Enqueue(new int[2] { i, j });
                            }

                            board[i, j] = n;
                            break;
                        }

                        n = c - '0';
                    }
                }
            }
            sr.Close();


            // 시간 측정
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // 연산
            while (emptyPos.Count > 0)
            {

                // 0인 좌표만 검색
                int[] pos = emptyPos.Dequeue();
                if (ChkBoard(board, pos))
                {

                    emptyPos.Enqueue(pos);
                }
            }

            timer.Stop();
            Console.WriteLine();
            Console.WriteLine($"{timer.ElapsedTicks}ms");   // 6000ms 이상
            Console.WriteLine();

            
#elif seconds

            // 판과 제로 좌표 입력
            (int x, int y)[] pos = new (int x, int y)[35];
            int _size = 0;

            for (int i = 0; i < 9; i++)
            {

                int j = 0;
                foreach(var item in Console.ReadLine().Split(' ').Select(int.Parse))
                {

                    board[i, j] = item;

                    if (item == 0)
                    {

                        pos[_size++] = (i, j);
                    }

                    j++;
                }
            }

            bool isStop = false;
            Back(board, pos, _size, 0, ref isStop);
#endif

            // 출력
            Console.WriteLine("정답 출력!");
            StringBuilder sb = new();

            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {

                    sb.Append($"{board[i, j]} ");
                }
                sb.Append('\n');
            }

            StreamWriter sw = new(Console.OpenStandardOutput());
            sw.Write(sb);
            sw.Flush();

            /*
            sb.Clear();

            Console.WriteLine("0,0 확인!");
            for (int i = 0; i < _size; i++)
            {

                sb.AppendLine($"{pos[i].x}, {pos[i].y}");
            }

            sw.Write(sb);
            sw.Flush();
            */
            sw.Close();

        }


#if first
        static bool ChkBoard(int[,] board, int[] pos) 
        {

            // 세로 검사
            // 존재하는건 true
            bool[] chk = new bool[10];
            for (int i = 0; i < 9; i++)
            {

                chk[board[pos[0], i]] = true;
            }

            int chkNum = 0;

            // 없는건 false
            for (int i = 1; i <= 9; i++)
            {

                if (!chk[i])
                {

                    if (chkNum == 0)
                    {

                        chkNum = i;
                        continue;
                    }
                    else if (chkNum != 0)
                    {

                        chkNum = -1;
                        break;
                    }
                }
            }

            // 두 개이상 있는 경우 가로 검사 시작
            if (chkNum != -1)
            {

                if (chkNum != 0)
                    board[pos[0], pos[1]] = chkNum;
                return false;
            }

            // 가로 검사 - 세로검사 로직과 같다
            chk = new bool[10];
            for (int i = 0; i < 9; i++)
            {
                
                chk[board[i, pos[1]]] = true;
            }

            chkNum = 0;

            for (int i = 1; i <= 9; i++)
            {

                if (!chk[i])
                {

                    if (chkNum == 0)
                    {

                        chkNum = i;
                        continue;
                    }
                    else if (chkNum != 0)
                    {

                        chkNum = -1;
                        break;
                    }
                }
            }

            if (chkNum != -1)
            {
                if (chkNum != 0)
                    board[pos[0], pos[1]] = chkNum;
                return false;
            }

            // 3*3 검사   - 세로 검사와 로직은 같다
            int h = 3 * (pos[0] / 3);
            int v = 3 * (pos[1] / 3);
            chk = new bool[10];
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    chk[board[i + h, j + v]] = true;
                }
            }

            chkNum = 0;
            for (int i = 1; i <= 9; i++)
            {

                if (!chk[i])
                {

                    if (chkNum == 0)
                    {

                        chkNum = i;
                        continue;
                    }
                    else if (chkNum != 0)
                    {

                        chkNum = -1;
                        break;
                    }
                }
            }

            if (chkNum != -1)
            {

                if (chkNum != 0)
                    board[pos[0], pos[1]] = chkNum;
                return false;
            }


            return true;
        }


#elif seconds
        static void Back(int[,] board, (int x, int y)[] zeroPos, int _size, int step, ref bool stop)
        {
            
            // 스탑 여부 검사
            if (stop)
            {

                return;
            }


            // 모두 찾은 경우
            if (step == _size)
            {

                // 이제 그만 표시하고 탈출
                stop = true;
                return;
            }

            for (int i = 1; i <= 9; i++)
            {


                board[zeroPos[step].x, zeroPos[step].y] = i;

                if (!ChkHV(board, zeroPos[step].x, zeroPos[step].y))
                {
                    Back(board, zeroPos, _size, step + 1, ref stop);
                }

                if (stop)
                {

                    return;
                }

                // 이 구문이 없어서 자꾸 틀렸다고 한다!
                // 재귀에 계속 들어가면 없는 경우의 수 값으로 바뀌기 때문이다
                board[zeroPos[step].x, zeroPos[step].y] = 0;
            }
        }

        // 가로 세로 검사
        static bool ChkHV(int[,] board, int x, int y)
        {

            // 가로 세로 검사
            for (int i = 0; i < 9; i++)
            {

                // 세로 검사
                if (i != y && board[x, i] == board[x, y])
                {

                    return true;
                }

                // 가로 검사
                if (i != x && board[i, y] == board[x, y])
                {

                    return true;
                }
            }

            // 3 * 3 검사
            int h = 3 * (x / 3);
            int v = 3 * (y / 3);

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    // x, y가 아니고 값이 중복?
                    if (!(h + i == x && v + j == y) 
                        && board[x, y] == board[h + i, v + j])
                    {

                        return true;
                    }
                }
            }

            return false;
        }


#endif
    }
}
