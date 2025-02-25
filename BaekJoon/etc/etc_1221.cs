using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 
이름 : 배성훈
내용 : Meeting
    문제번호 : 8410번
*/

namespace BaekJoon.etc
{
    internal class etc_1221
    {

        static void Main1221(string[] args)
        {

            var chk1 = Chk1(100);
            var chk2 = Chk2(100);

            for (int i = 0; i <= 10_000; i++)
            {

                if (chk1[i][0] == chk2[i][0]) continue;
                Console.Write($"{i} : {chk1[i][0]} - {chk2[i][0]}\n");
            }

            int[][] Chk2(int _n)
            {

                if (_n < 2) _n = 2;
                int[][] cnt = new int[_n + 1][];
                for (int i = 0; i <= _n; i++) 
                {

                    cnt[i] = new int[2];
                }

                cnt[0][0] = 0;
                cnt[0][1] = 0;
                cnt[1][0] = 0;
                cnt[1][1] = 0;
                cnt[2][0] = 1;
                cnt[2][1] = 0;

                for (int i = 3; i <= _n; i++)
                {

                    cnt[i][0] = (cnt[i - 1][0] + cnt[i - 2][0] + 3 + cnt[i - 2][1]) % 10;
                    cnt[i][1] = (cnt[i - 1][1] + cnt[i - 2][1] + 1) % 10;
                }

                return cnt;
            }

            int[][] Chk1(int _n)
            {

                if (_n < 2) _n = 2;
                int[][] cnt = new int[_n + 1][];
                for (int i = 0; i <= _n; i++)
                {

                    cnt[i] = new int[2];
                    for (int j = 0; j < 2; j++)
                    {

                        cnt[i][j] = -1;
                    }
                }

                cnt[0][0] = 0;
                cnt[0][1] = 0;
                cnt[1][0] = 0;
                cnt[1][1] = 0;
                cnt[2][0] = 1;
                cnt[2][1] = 0;

                // Console.Write((DFS(_n) + 1) % 10);
                DFS(_n);

                return cnt;

                int DFS(int _idx1, int _idx2 = 0)
                {

                    ref int ret = ref cnt[_idx1][_idx2];
                    if (ret != -1) return ret;

                    ret = 0;
                    if (_idx2 == 0)
                        ret = (DFS(_idx1 - 2, 1) + 1) % 10;

                    return ret = (ret
                        + DFS(_idx1 - 1, 0)
                        + DFS(_idx1 - 2, 0) + 1) % 10;
                }
            }

            

            Console.ReadKey();


        }
    }
}
