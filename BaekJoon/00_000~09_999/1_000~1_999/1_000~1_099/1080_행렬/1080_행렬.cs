using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 10
이름 : 배성훈
내용 : 행렬
    문제번호 : 1080번

    그리디 문제다.
    왼쪽 위에서부터 맞춰가면 그리디로 최소가되게 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1326
    {

        static void Main1326(string[] args)
        {

            int SIZE_R = 3;
            int SIZE_C = 3;

            int row, col;
            int[][] arr1, arr2;

            Input();

            GetRet();

            void GetRet()
            {

                int eR = row - SIZE_R;
                int eC = col - SIZE_C;

                int ret = 0;
                for (int r = 0; r <= eR; r++)
                {

                    for (int c = 0; c <= eC; c++)
                    {

                        if (arr1[r][c] == arr2[r][c]) continue;
                        Switch(r, c);
                        ret++;
                    }
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (arr1[r][c] == arr2[r][c]) continue;
                        ret = -1;
                        break;
                    }
                }

                Console.Write(ret);

                void Switch(int _r, int _c)
                {

                    for (int i = 0; i < SIZE_R; i++)
                    {

                        for (int j = 0; j < SIZE_C; j++)
                        {

                            arr1[_r + i][_c + j] ^= 1;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] input = sr.ReadLine().Split();
                row = int.Parse(input[0]);
                col = int.Parse(input[1]);

                arr1 = new int[row][];
                arr2 = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    arr1[r] = new int[col];
                    arr2[r] = new int[col];
                }

                for (int r = 0; r < row; r++)
                {

                    string temp = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        arr1[r][c] = temp[c] - '0';
                    }
                }

                for (int r = 0; r < row; r++)
                {

                    string temp = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        arr2[r][c] = temp[c] - '0';
                    }
                }
            }
        }
    }
}
