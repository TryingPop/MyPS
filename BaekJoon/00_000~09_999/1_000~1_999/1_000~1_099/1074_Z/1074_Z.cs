using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : Z
    문제번호 : 1074번

    주된 아이디어는 다음과 같다
    다음과 같이 4구간으로 나누고, 어디에 속한지 알아야한다
        1 | 2
        -----
        3 | 4

    1번 구간은 더하는 숫자가 없다
    2번 구간은 구간의 크기를 1번 더한다
    3번 구간은 구간의 크기를 2번 더한다
    4번 구간은 구간의 크기를 3번 더한다

    그리고 해당 구간을 또 4등분해서 들어가면 된다
    예를들어 3번 구간을 탐색했다면
    
                |
              1 | 2
                |
             -------
             1|2|
             ---| 4
             3|4|

    해당처럼 다시 4구간으로 쪼개주면 된다
    이렇게 끝까지 진행해서 구간의 크기가 1일 때 종료하고 여태 더한 값을 반환하면 된다

    전체 구간의 크기는 4^15 < 11억이고
    반환 결과를 int로 했다

    DFS로 구현하면 될거 같았고, 바로 구현하지 못하고 시간이 걸렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0069
    {

        static void Main69(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] twoPow = new int[info[0] + 1];
            int[] fourPow = new int[info[0] + 1];
            twoPow[0] = 1;
            fourPow[0] = 1;
            for (int i = 1; i <= info[0]; i++)
            {

                twoPow[i] = twoPow[i - 1] * 2;
                fourPow[i] = fourPow[i - 1] * 4;
            }

            int ret = DFS(info[1], info[2], twoPow, fourPow, info[0] - 1);

            Console.WriteLine(ret);
        }

        static int DFS(int _r, int _c, int[] _twoPow, int[] _fourPow, int _depth)
        {

            if (_depth < 0) return 0;

            int chk1 = _r / _twoPow[_depth];
            int chk2 = _c / _twoPow[_depth];

            int mul = chk1 * 2 + chk2;
            int ret = mul * _fourPow[_depth];
            chk1 *= _twoPow[_depth];
            chk2 *= _twoPow[_depth];
            ret += DFS(_r - chk1, _c - chk2, _twoPow, _fourPow, _depth - 1);

            return ret;
        }
    }

#if other
    class Program
    {
        static int r_cnt = 0;
        static int c_cnt = 0;
        static int FindAnswer(int n, int r, int c)
        {
            int division = (int)Math.Pow(2, n - 1);
            if (n == 1)
                return r * 2 + c * 1;
            else
                return ((r / division) * 2 + (c / division) * 1) * division * division + FindAnswer(n - 1, r % division, c % division);
            //2*4+ 1 1 1

        }

        static void Main(string[] args)
        {

            string[] input = Console.ReadLine().Split();
            int N = Int32.Parse(input[0]);
            int r = Int32.Parse(input[1]);
            int c = Int32.Parse(input[2]);

            Console.WriteLine(FindAnswer(N, r, c));


        }
    }
#elif other2
var i = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

int F(int n, int r, int c)
{
    if (n == 0)
        return 0;

    int i = 0;
    if (r >= n)
    {
        r -= n;
        i += n * n * 2;
    }
    if (c >= n)
    {
        c -= n;
        i += n * n;
    }

    return i + F(n / 2, r, c);
}

Console.WriteLine(F((int)Math.Pow(2, i[0] - 1), i[1], i[2]));
#endif
}
