using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 25
이름 : 배성훈
내용 : 배틀쉽
    문제번호 : 7771번

    해 구성하기 문제다.
    해를 간단하게 만드는게 문제다.
    먼저 100이 있는 위치에 무조건 배가 있어야 복잡도가 가장 큰걸 확인할 수 있다.
    이제 배가 있는 줄을 최소한 몇 개로 줄일 수 있는지 확인하니
    3줄로 줄일 수 있었다.

    4개, 2개, 2개
    3개, 3개, 2개
    1개, 1개, 1개, 1개이다.

    이 중 형태가 2개인데 서로 겹치지 않는 형태가 있는 것은 3, 3, 2이다.
    (4, 2, 2로 잡아도 된다.)

        ###.###.##
        ##.###.###
    해당 두 경우를 보면, .이 겹치는 부분이 없다.
    
    그리고 4, 2, 2부분은 ####.##.##
    1, 1, 1, 1은 #.#.#.#...
    이렇게 채운다.

    이제 100이 있는 곳은 3 3 2로 채운다.
    그리고 100의 좌표가 포함되게 세워야한다.

    나머지 구간에 4 2 2 구간과 1 1 1 1를 세우면 되는데
    경우를 나눠서 생각했다.

    100 이 있는게 위에서 5번째 줄 이하인 경우
    100 이 있는 줄에 3 3 2로 채운다.
    그리고 7, 9번째 줄에 4 2 2구간과 1 1 1 1구간을 넣으면 된다.
    나머지는 배가 없다.

    100이 6번째 줄 이상인 경우 2, 4 번째 줄에 4 2 2 구간과 1 1 1 1 구간을 채운다.
    그리고 100 이있는 줄에 3 3 2로 채운다.
    나머지는 배가 없다.
*/

namespace BaekJoon.etc
{
    internal class etc_1577
    {

        static void Main1577(string[] args)
        {

            int size = 10;
            int[][] rounds;

            Input();

            GetRet();

            void GetRet()
            {

                string EMPTY = "..........\n";
                GetMax(out int eR, out int eC);

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (eR >= 5)
                {

                    WriteOther();

                    for (int i = 5; i < size; i++)
                    {

                        if (eR == i) WriteEnd();
                        else sw.Write(EMPTY);
                    }
                }
                else
                {

                    for (int i = 0; i < 5; i++)
                    {

                        if (eR == i) WriteEnd();
                        else sw.Write(EMPTY);
                    }

                    WriteOther();
                }

                void WriteEnd()
                {

                    if (eC != 2 && eC != 6) sw.Write("##.###.###\n");
                    else sw.Write("###.###.##\n");
                }

                void WriteOther()
                {

                    sw.Write(EMPTY);
                    sw.Write("#.#.#.#...\n");
                    sw.Write(EMPTY);
                    sw.Write("####.##.##\n");
                    sw.Write(EMPTY);
                }

                void GetMax(out int _r, out int _c)
                {

                    _c = 0;
                    for (_r = 0; _r < size; _r++)
                    {

                        for (_c = 0; _c < size; _c++)
                        {

                            if (rounds[_r][_c] == 100) return;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                rounds = new int[size][];
                for (int i = 0; i < size; i++)
                {

                    rounds[i] = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                }
            }
        }
    }
}
