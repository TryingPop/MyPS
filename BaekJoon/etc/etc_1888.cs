using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 16
이름 : 배성훈
내용 : Build The Grid
    문제번호 : 24654번

    해 구성하기
    각 열, 행에 있는 검정색의 개수가 순열을 이뤄야 한다.
    그래서 적당히 행과 행끼리 열과 열끼리 바꾸면

        BBBBB....BW
        BBBBB....WW
        ...
        BWWWW....WW
        WWWWW....WW

    형태로 나타낼 수 있다.

    위 그림을 보면 1행 2열의 B는 W와 인접하지 않으므로 불가능하다.
    이제 2열과 n열을 바꾸고 2행과 n열을 바꾼다.
    그러면 

        BBBBB....BW         BWBBB....BB         BWBBB....BB
        BBBBB....WW         BWBBB....WB         WWWWW....WW
        ...             ->  ...             ->  ...
        BWWWW....WW         BWWWW....WW         BWWWW....WW
        WWWWW....WW         WWWWW....WW         BWBBB....WB

    그러면 1행 2열의 문제는 해결되었다.
    그리고 1행 4열에도 B가 겹치는 문제가 있다.
    똑같이 4열과 N - 2열을 바꾸고 4행과 N - 2행을 바꾼다.

    이렇게 5이하는 모두 해결된다.

    그래서 귀납적으로 2 x k와 N + 2 - 2 x k 행과 열을 바꾸는 식으로 진행하면 되지 않을까 추론했다.
    검증은 해당 방법으로 진행했을 때 W가 한붓긋기가 가능한지와
    B 4방향에 W가 있는지로 판별하니 499와 500인 경우 이상없이 통과했다.
    그래서 제출하니 이상없이 통과했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1888
    {

        static void Main1888(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            bool[][] board = new bool[n][];
            for (int r = 0; r < n; r++)
            {

                board[r] = new bool[n];
                for (int c = 0; c < n - 1 - r; c++)
                {

                    board[r][c] = true;
                }
            }

            for (int i = 1, j = n - 1; i < j; i += 2, j -= 2) 
            {

                ChangeRow(i, j);
                ChangeCol(i, j);
            }

            for (int r = 0; r < n; r++)
            {

                for (int c = 0; c < n; c++)
                {

                    sw.Write(board[r][c] ? 'B' : 'W');
                }

                sw.Write('\n');
            }

            void ChangeRow(int r1, int r2)
            {

                for (int c = 0; c < n; c++)
                {

                    bool temp = board[r1][c];
                    board[r1][c] = board[r2][c];
                    board[r2][c] = temp;
                }
            }

            void ChangeCol(int c1, int c2)
            {

                for (int r = 0; r < n; r++)
                {

                    bool temp = board[r][c1];
                    board[r][c1] = board[r][c2];
                    board[r][c2] = temp;
                }
            }
        }
    }
}