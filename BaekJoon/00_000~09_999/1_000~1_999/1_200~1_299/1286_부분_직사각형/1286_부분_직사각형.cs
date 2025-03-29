using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 29
이름 : 배성훈
내용 : 부분 직사각형
    문제번호 : 1286번

    조합론, 수학 문제다.
    r, c를 포함하는 사각형의 갯수는
    r보다 윗선 중 1개를 택한다. 즉, r + 1개 있다.
    r보다 아랫선 중 1개를 택한다. row * 2 - r개 있다.
    이제 c보다 왼선 중 1개를 택한다. c + 1개 있다.
    c보다 오른선 중 1개를 택한다 col * 2 - c개 있다.

    이렇게 끝점으로 하는 사각형이 r,c 를 포함하는 사각형이 되고
    전체 사각형을 해당 방법으로 찾을 수 있다.
    그렇게 좌표를 포함하는 사각형을 찾고 누적하면 된다.
    나올 수 있는 사각형의 갯수는 200억으로 int.MaxValue보다 크다.
*/

namespace BaekJoon.etc
{
    internal class etc_1488
    {

        static void Main1488(string[] args)
        {

            int row, col;
            string[] board;
            int[][] cnt;
            long[] ret;

            Input();

            SetCnt();

            GetRet();

            void GetRet()
            {

                ret = new long[26];
                for (int r = 0; r < cnt.Length; r++)
                {

                    for (int c = 0; c < cnt[r].Length; c++)
                    {

                        int idx = board[r][c] - 'A';
                        ret[idx] += cnt[r][c];
                    }
                }

                for (int i = 0; i < 26; i++)
                {

                    Console.WriteLine(ret[i]);
                }
            }

            void SetCnt()
            {

                // r, c를 포함하는 사각형의 갯수
                cnt = new int[row * 2][];

                for (int r = 0; r < cnt.Length; r++)
                {

                    cnt[r] = new int[col * 2];
                    // r보다 위에서 1개 택
                    int up = r + 1;
                    // r보다 아래에서 1개 택
                    int down = row * 2 - r;
                    for (int c = 0; c < cnt[r].Length; c++)
                    {

                        // c보다 왼쪽에서 1개 택
                        int left = c + 1;
                        // c보다 오른쪽에서 1개 택
                        int right = cnt[r].Length - c;

                        cnt[r][c] = up * down * left * right;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] size = sr.ReadLine().Split();

                row = int.Parse(size[0]);
                col = int.Parse(size[1]);

                board = new string[row * 2];
                for (int r = 0; r < row; r++)
                {

                    string temp = sr.ReadLine();
                    temp += temp;
                    board[r] = temp;
                    board[r + row] = temp;
                }
            }
        }
    }
}
