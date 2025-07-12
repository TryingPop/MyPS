using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 배열 복원하기
    문제번호 : 16967번

    구현 문제다
    A의 존재성이 보장되어져 있다
    그리고, x < row, y < col도 보장되어져 있다
    필요한 부분만 읽고 구하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0418
    {

        static void Main418(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int row = ReadInt();
            int col = ReadInt();
            int x = ReadInt();
            int y = ReadInt();

            int[,] a = new int[row, col];

            for (int r = 0; r < x; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    a[r, c] = ReadInt();
                }


                for (int c = col; c < col + y; c++)
                {

                    ReadInt();
                }
            }

            for (int r = x; r < row; r++)
            {

                for (int c = 0; c < y; c++)
                {

                    a[r, c] = ReadInt();
                }

                for (int c = y; c < col; c++)
                {

                    a[r, c] = ReadInt() - a[r - x, c - y];
                }

                for (int c = col; c < col + y; c++)
                {

                    ReadInt();
                }
            }

            sr.Close();

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    sw.Write(a[r, c]);
                    sw.Write(' ');
                }

                sw.Write('\n');
            }

            sw.Close();
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
}
