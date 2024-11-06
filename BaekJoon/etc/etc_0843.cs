using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 27
이름 : 배성훈
내용 : 전생했더니 슬라임 연구자였던 건에 대하여 (Easy)
    문제번호 : 14715번

    수학, 정수론 문제다
    나머지 개수에 log2를 씌워 올림한게 정답이다
*/

namespace BaekJoon.etc
{
    internal class etc_0843
    {

        static void Main843(string[] args)
        {

            Solve();
            void Solve()
            {

                int n = int.Parse(Console.ReadLine());
                int div = CntDiv(n);

                div--;
                int ret = 0;
                while(div != 0)
                {

                    div >>= 1;
                    ret++;
                }

                Console.Write(ret);
            }

            int CntDiv(int _n)
            {

                int ret = 0;
                for (int i = 2; i * i <= _n; i++)
                {

                    while (_n % i == 0)
                    {

                        _n /= i;
                        ret++;
                    }
                }

                if (_n != 1) ret++;
                return ret;
            }
        }
    }
}
