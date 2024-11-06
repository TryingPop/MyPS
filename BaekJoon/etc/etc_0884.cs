using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 17 
이름 : 배성훈
내용 : 호반우가 길을 건너간 이유
    문제번호 : 20118번

    애드 혹, 해 구성하기 문제다
    같은 값을 XOR 연산하면 0이다
    그래서 같은 타일을 2번 밟게 가는게 중요하다

    8방향 이동가능하므로, 최단 거리는 row와 col 중 큰 값안에 이동할 수 있다
    거기에 2번씩 지나게 하면 된다

    짝수인경우 a -> b -> a -> b 
    이렇게 이동하면 된다 이상없이 도착한다
    
    반면 홀수인 경우는 짝수와 똑같이 하면 
    도착지점에서 시작하므로 문제가 생긴다

    행과 열의 크기가 2이상이므로 0, 0 -> 0, 1을 왕복한 뒤 0, 1에서 
    1, 1로 이동하면 홀수 번에서 시작하게 세팅해준다
    그러면 도착지까지 거리가 짝수가 되므로
    짝수의 방법을 쓸 수 있다

    많아야 row와 col 중 큰 값의 2배에 + 2 이므로 조건을 만족한다
*/

namespace BaekJoon.etc
{
    internal class etc_0884
    {

        static void Main884(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int row, col;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int max = Math.Max(row, col);

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int r = 0, c = 0;

                if (max % 2 == 1)
                {

                    sw.Write($"{(max + 1) * 2}\n");
                    sw.Write($"0 0\n0 1\n0 0\n0 1\n");
                    r = 1;
                    c = 1;
                }
                else
                {

                    sw.Write($"{max * 2}\n");
                }

                while(NotEnd(r, c))
                {

                    Next(r, c, out int nR, out int nC);
                    sw.Write($"{r} {c}\n{nR} {nC}\n{r} {c}\n{nR} {nC}\n");

                    Next(nR, nC, out r, out c);
                }

                sw.Close();
            }

            bool NotEnd(int _r, int _c)
            {

                return _r != row - 1 || _c != col - 1;
            }

            void Next(int _r, int _c, out int _nR, out int _nC)
            {

                if (_r == row - 1) _nR = _r;
                else _nR = _r + 1;

                if (_c == col - 1) _nC = _c;
                else _nC = _c + 1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();
                sr.Close();
            }

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
