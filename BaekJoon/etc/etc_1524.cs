using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 6
이름 : 배성훈
내용 : 가희야 거기서 자는 거 아니야
    문제번호 : 21771번

    구현, 문자열 문제다.
    베개는 방안에 들어간다 했으므로
    맵에 안에 전체 사이즈가 포함되어 있다.
    만약 맵에 있는 베개의 사이즈가 베개의 크기보다 작다면
    이는 강아지가 올라가 있다 봐도 된다.
    그래서 왼쪽 위 위치를 찾고 직사각형 범위에 베개가 되는지 확인해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1524
    {

        static void Main1524(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int[] mapSize = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int[] sizes = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            string[] board = new string[mapSize[0]];

            for (int r = 0; r < mapSize[0]; r++)
            {

                board[r] = sr.ReadLine();
            }

            for (int r = 0; r < mapSize[0]; r++)
            {

                for (int c = 0; c < mapSize[1]; c++)
                {

                    if (board[r][c] != 'P') continue;

                    if (Chk(r, c)) Console.Write(1);
                    else Console.Write(0);

                    return;
                }
            }

            bool Chk(int _r, int _c)
            {

                int eR = _r + sizes[2];
                int eC = _c + sizes[3];

                if (eR > mapSize[0] || eC > mapSize[1]) return true;
                for (int r = _r; r < eR; r++)
                {

                    for (int c = _c; c < eC; c++)
                    {

                        if (board[r][c] != 'P') return true;
                    }
                }

                return false;
            }
        }
    }
}
