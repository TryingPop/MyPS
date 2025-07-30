using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 17
이름 : 배성훈
내용 : 박스포장
    문제번호 : 1997번

    구현, 시뮬레이션 문제다.
    판을 순차적으로 넣는다.
    다만 가득 찬다면 다음 박스에 넣어주면 된다.

    박스의 각 열 c에 대해 쌓인 곳의 가장 높은 위치를 d[c]라 하자.
    그리고 판의 각 열 c에 대해 존재하는 구간을 모두 덮을 수 있는 가장 작은 막대기의 길이를 u[c]라 하자.

    그러면 u[c] + d[c] 의 최댓값이 해당 판이 있는 위치가 된다.
    이렇게 찾는 경우 박스의 높이는 w에 찾을 수 있다.
    해당 높이가 음수인 경우 새로운 박스를 가져와야 한다.
    그리고 초기화하는 경우 w x h를 수정해야 한다.

    그리고 박스를 채우는 것은 위에서부터 채운다면 w x 10에 채울 수 있다.

    이렇게 하는 경우 최악의 경우 매번 초기화 해준다해도 n x ((w x 10) + w + w x h)에 해결 가능하다.
    시간복잡도는 O(10nw + nwh) = O(nwh)이다.
    n, w, h는 100이하인 자연수이므로 유효한 방법이다!
*/

namespace BaekJoon.etc
{
    internal class etc_1774
    {

        static void Main1774(string[] args)
        {

            // 1997 - 박스포장
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt(), w = ReadInt(), b = ReadInt(), h;

            bool[][] board, diagram;
            int[] u, d;

            int[] ret;
            int len;


            SetArr();

            for (int i = 0; i < n; i++)
            {

                SetDiagram();

                FillBoard();
            }

            Output();

            void Output()
            {

                ret[len++] = GetPeek();

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{ret[i]} ");
                }
            }

            int GetPeek()
            {

                int ret = 0;

                for (int j = 0; j < w; j++)
                {

                    ret = Math.Max(ret, b - d[j]);
                }

                return ret;
            }

            void FillBoard()
            {

                int top = FindTop();
                if (top < 0)
                {

                    ret[len++] = GetPeek();
                    ClearBoard();
                    top = FindTop();
                }

                for (int r = top, i = 0; i < h; i++, r++)
                {

                    for (int c = 0; c < w; c++)
                    {


                        if (diagram[i][c])
                        { 
                            
                            board[r][c] = diagram[i][c];
                            d[c] = Math.Min(d[c], r);
                        }
                    }
                }
            }

            int FindTop()
            {

                int ret = b;
                for (int j = 0; j < w; j++)
                {

                    ret = Math.Min(d[j] - u[j] - 1, ret);
                }

                return ret;
            }

            void ClearBoard()
            {

                Array.Fill(d, b);
                for (int i = 0; i < b; i++)
                {

                    for (int j = 0; j < w; j++)
                    {

                        board[i][j] = false;
                    }
                }
            }

            void SetDiagram()
            {

                h = ReadInt();
                Array.Fill(u, -1);
                
                for (int i = 0; i < h; i++)
                {

                    for (int j = 0; j < w; j++)
                    {

                        int cur = sr.Read();
                        if (cur == '.') diagram[i][j] = false;
                        else
                        {
                         
                            diagram[i][j] = true;
                            u[j] = Math.Max(i, u[j]);
                        }
                    }

                    while (sr.Read() != '\n') ;
                }
            }

            void SetArr()
            {

                u = new int[w];
                d = new int[w];


                Array.Fill(d, b);

                board = new bool[b][];
                for (int i = 0; i < b; i++)
                {

                    board[i] = new bool[w];
                }

                diagram = new bool[Math.Min(b, 10)][];
                for (int i = 0; i < diagram.Length; i++)
                {

                    diagram[i] = new bool[w];
                }

                ret = new int[n];
                len = 0;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
