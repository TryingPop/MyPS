using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 바둑이 포커
    문제번호 : 17292번

    정렬 문제다
    Comp1, Comp2 함수를 만들어 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0449
    {

        static void Main449(string[] args)
        {

            string[] inputs = Console.ReadLine().Split(',');

            string[] combi = new string[15];

            int len = 0;
            for (int i = 0; i < 6; i++)
            {

                for (int j = i + 1; j < 6; j++)
                {

                    combi[len++] = inputs[i] + inputs[j];
                }
            }

            Array.Sort(combi, (x, y) => Comp1(x, y));

            for (int i = 0; i < 15; i++)
            {

                Console.WriteLine(combi[i]);
            }

            int Comp1(string _f, string _b)
            {

                int ret;
                int min1 = _f[0] < _f[2] ? _f[0] : _f[2];
                int max1 = _f[0] < _f[2] ? _f[2] : _f[0];

                int min2 = _b[0] < _b[2] ? _b[0] : _b[2];
                int max2 = _b[0] < _b[2] ? _b[2] : _b[0];

                bool conn1 = IsConti(min1, max1);
                bool conn2 = IsConti(min2, max2);

                if (conn1 != conn2) return conn1 ? -1 : 1;

                if (min1 == max1 && min2 != max2) return -1;
                if (min1 != max1 && min2 == max2) return 1;

                return Comp2(_f, _b);
            }

            bool IsConti(int _min, int _max)
            {

                if ((_max - _min == 1)
                    || (_max == 'a' && _min == '9')
                    || (_max == 'f' && _min == '1')) return true;

                return false;
            }

            int Comp2(string _f, string _b)
            {

                int ret;
                if ((_f[1] == _f[3]) != (_b[1] == _b[3]))
                {

                    ret = _f[1] == _f[3] ? -1 : 1;
                    return ret;
                }

                int l1 = _f[0] < _f[2] ? _f[2] : _f[0];
                int r1 = _b[0] < _b[2] ? _b[2] : _b[0];

                if (l1 != r1)
                {

                    ret = l1 > r1 ? -1 : 1;
                    return ret;
                }

                int l2 = _f[0] < _f[2] ? _f[0] : _f[2];
                int r2 = _b[0] < _b[2] ? _b[0] : _b[2];

                if (l2 != r2)
                {

                    ret = l2 > r2 ? -1 : 1;
                    return ret;
                }

                if (l1 == _f[0] && _f[1] == 'b') return -1;
                else if (l1 == _f[2] && _f[3] == 'b') return -1;

                return 1;
            }
        }
    }
}
