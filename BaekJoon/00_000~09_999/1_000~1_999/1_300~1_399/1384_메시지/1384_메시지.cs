using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 메시지
    문제번호 : 1384번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1494
    {

        static void Main1494(string[] args)
        {

            string P = "P";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            string[][] board = new string[21][];
            int g = 1;
            while (Input())
            {

                sw.Write($"Group {g++}\n");

                GetRet();

                sw.Write('\n');
            }

            void GetRet()
            {

                bool flag = false;

                for (int i = 1; i <= n; i++)
                {

                    int other = i;
                    for (int j = 1; j < n; j++)
                    {

                        other = other == 1 ? n : other - 1;
                        if (board[i][j] == P) continue;
                        flag = true;
                        sw.Write($"{board[other][0]} was nasty about {board[i][0]}\n");
                    }
                }

                if (flag) return;

                sw.Write("Nobody was nasty\n");
            }

            bool Input()
            {

                n = int.Parse(sr.ReadLine());
                for (int i = 1; i <= n; i++)
                {

                    board[i] = sr.ReadLine().Split();
                }

                return n != 0;
            }
        }
    }
}
