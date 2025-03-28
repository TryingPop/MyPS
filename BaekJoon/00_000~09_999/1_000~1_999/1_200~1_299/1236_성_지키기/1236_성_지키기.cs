using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 성 지키기
    문제번호 : 1236번

    구현 문제다.
    아직 안채워진 행과 열 중 큰 값이 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1478
    {

        static void Main1478(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int[] size = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

            string[] board = new string[size[0]];
            for (int i = 0; i < size[0]; i++)
            {

                board[i] = sr.ReadLine();
            }

            int chkR = 0;
            for (int r = 0; r < size[0]; r++)
            {

                bool flag = false;
                for (int c = 0; c < size[1]; c++)
                {

                    if (board[r][c] == 'X') 
                    {

                        flag = true;
                        break; 
                    }
                }

                if (flag) chkR++;
            }

            int chkC = 0;
            for (int c = 0; c < size[1]; c++)
            {

                bool flag = false;

                for (int r = 0; r < size[0]; r++)
                {

                    if (board[r][c] == 'X')
                    {

                        flag = true;
                        break;
                    }
                }

                if (flag) chkC++;
            }

            Console.Write(Math.Max(size[0] - chkR, size[1] - chkC));
        }
    }
}
