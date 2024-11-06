using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 초콜릿과 ㄱ나이트 게임 (Sweet)
    문제번호 : 31459번

    그리디 문제다
    단방향으로만 이동한다
    해당 자리에서맵을 벗어나는 경우면 놓을 수 있고,
    이동 자리가 맵 안인데 나이트가 없어야 놓을 수 있다

    최대한 놓는 방법은 오른쪽 끝에서부터 
    나이트를 놓을 수 있을 때, 놓으면 최적해를 얻을 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0345
    {

        static void Main345(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            bool[,] board = new bool[100, 100];

            while (test-- > 0)
            {

                int col = ReadInt();
                int row = ReadInt();
                int x = ReadInt();
                int y = ReadInt();

                int ret = 0;
                for (int i = col - 1; i >= 0; i--)
                {

                    for (int j = row - 1; j >= 0; j--)
                    {

                        int nextX = i + x;
                        int nextY = j + y;
                        if (board[nextX, nextY]) continue;
                        board[i, j] = true;
                        ret++;
                    }
                }

                sw.WriteLine(ret);

                for (int j = 0; j < row; j++)
                {

                    for (int i = 0; i < col; i++)
                    {

                        board[i, j] = false;
                    }
                }

            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
