using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 7
이름 : 배성훈
내용 : 램프
    문제번호 : 1034번

    애드혹, 브루트포스 문제다.
    아이디어는 다음과 같다.
    전구는 열단위로 키고 끌 수 있다.
    그리고 행으로 모두 1인 갯수를 찾아야 한다.

    초기 행의 켜짐 상태가 다르면
    둘이 동시에 모두 켜지는 경우는 존재할 수 없다!

    그래서 해당 행을 모두 켤 수 있는지 확인하고,
    같은 초기행을 찾아 갯수를 센다.
    이렇게 모든 행을 조사해 가장 많이 켜진 경우를 정답으로 제출하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1292
    {

        static void Main1292(string[] args)
        {

            int row, col, k;
            string[] board;
            Dictionary<string, int> same;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;

                for (int r = 0; r < row; r++)
                {

                    int curZero = 0;

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == '1') continue; ;
                        curZero++;
                    }

                    int sum = 0;
                    if (curZero <= k && (curZero & 1) == (k & 1))
                        sum = same[board[r]];

                    ret = Math.Max(ret, sum);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                same = new(row);
                board = new string[row];

                for (int r = 0; r < row; r++)
                {

                    string temp = sr.ReadLine();
                    board[r] = temp;
                    if (same.ContainsKey(temp)) same[temp]++;
                    else same[temp] = 1;
                }

                k = ReadInt();

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
    }
}
