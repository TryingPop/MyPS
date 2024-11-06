using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 겉넓이 구하기
    문제번호 : 16931번

    구현, 기하학, 3차원 기하학 문제다

    아이디어는 다음과 같다
    옆면을 봤을 때, 차이만큼 면이 추가된다
    옆면에서 증가하는 경우 바라보는 방향이고 감소하는 경우는 바라보는 방향의 뒷면이 된다
    그리고 윗면과 밑면은 따로 처리했다
*/

namespace BaekJoon.etc
{
    internal class etc_0499
    {

        static void Main499(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt();
            int col = ReadInt();

            int[,] board = new int[row, col];
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = ReadInt();
                }
            }

            sr.Close();

            // 윗면 + 밑면
            int ret = row * col * 2;

            for (int r = 0; r < row; r++)
            {

                // row를 일렬로 세워서 바라본다
                int before = 0;
                for (int c = 0; c < col; c++)
                {

                    int cur = board[r, c];
                    int diff = cur - before;

                    // 차이는 추가된 면의 넓이다 양수면 앞면, 음수면 뒷면이 된다
                    diff = diff < 0 ? -diff : diff;
                    ret += diff;
                    before = cur;
                }

                // 마지막 뒷면
                ret += before;
            }

            for (int c = 0; c < col; c++)
            {

                int before = 0;
                for (int r = 0; r < row; r++)
                {

                    int cur = board[r, c];
                    int diff = cur - before;
                    diff = diff < 0 ? -diff : diff;
                    ret += diff;
                    before = cur;
                }

                ret += before;
            }

            Console.WriteLine(ret);

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
using System;

namespace AlgorithmStudy
{
    class boj16931
    {
        public static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            int n = int.Parse(input[0]), m = int.Parse(input[1]);
            int[,] arr = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine().Split();
                for (int j = 0; j < m; j++)
                {
                    arr[i, j] = int.Parse(input[j]);
                }
            }

            int total = 2 * n * m;
            for (int i = 0; i < n; i++)
            {
                total += arr[i, 0];
                total += arr[i, m - 1];
            }
            for (int i = 0; i < m; i++)
            {
                total += arr[0, i];
                total += arr[n - 1, i];
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i < m - 1)
                        if (arr[j, i] < arr[j, i + 1])
                            total += arr[j, i + 1] - arr[j, i];
                    if (i > 0)
                        if (arr[j, i] < arr[j, i - 1])
                            total += arr[j, i - 1] - arr[j, i];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i < n - 1)
                        if (arr[i, j] < arr[i + 1, j])
                            total += arr[i + 1, j] - arr[i, j];
                    if (i > 0)
                        if (arr[i, j] < arr[i - 1, j])
                            total += arr[i - 1, j] - arr[i, j];
                }
            }
            Console.WriteLine(total);
        }
    }
}

#endif
}
