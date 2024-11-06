using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 18
이름 : 배성훈
내용 : Приготовление сэндвича
    문제번호 : 29570번

    구현 문제다
    파파고, 구글 번역기를 돌려보면 모듈로 입력으로 1000을 초과하지 않는다는데 
    절댓값 1000이었다;

    이를 캐치 못해 3번이나 인덱스 에러 났다
*/

namespace BaekJoon.etc
{
    internal class etc_0975
    {

        static void Main975(string[] args)
        {

            int[][] map;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int w = 0; w <= 2_000; w++)
                {

                    for (int h = 0; h <= 2_000; h++)
                    {

                        if (map[w][h] == 0) continue;
                        ret++;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                map = new int[2_001][];
                for (int i = 0; i < 2_001; i++)
                {

                    map[i] = new int[2_001];
                }
                
                int l, d, r, u;

                for (int i = 0; i < 4; i++)
                {

                    GetRect(out l, out d, out r, out u);
                    DrawRect(l, d, r, u);
                }
            }

            void DrawRect(int _l, int _d, int _r, int _u)
            {

                for (int w = _l; w < _r; w++)
                {

                    for (int h = _d; h < _u; h++)
                    {

                        map[w][h]++;
                    }
                }
            }

            void GetRect(out int _l, out int _d, out int _r, out int _u)
            {

                string[] temp = Console.ReadLine().Split();

                _l = int.Parse(temp[0]) + 1000;
                _d = int.Parse(temp[1]) + 1000;
                _r = int.Parse(temp[2]) + 1000;
                _u = int.Parse(temp[3]) + 1000;
            }
        }
    }
}
