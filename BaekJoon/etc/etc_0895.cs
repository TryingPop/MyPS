using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : 축구
    문제번호 : 1344번

    수학, dp, 조합론, 확률론 문제다
    서로 독립 사건이므로 교집합 확률은 두 확률의 곱이다
    그래서 각각의 확률을 구해 더하고 교집합 부분을 빼면 된다

    소수인 경우 확률은 전처리로 해결했다
    전처리가 아닌 경우 dp를 써야한다
*/

namespace BaekJoon.etc
{
    internal class etc_0895
    {

        static void Main895(string[] args)
        {

            /*
            Console.WriteLine(Comb(18, 2));
            Console.WriteLine(Comb(18, 3));
            Console.WriteLine(Comb(18, 5));
            Console.WriteLine(Comb(18, 7));
            Console.WriteLine(Comb(18, 11));
            Console.WriteLine(Comb(18, 13));
            Console.WriteLine(Comb(18, 17));

            int Comb(int _n, int _r)
            {

                if (_n == _r || _r == 0 || _n == 1) return 1;

                return Comb(_n - 1, _r - 1) + Comb(_n - 1, _r);
            }
            */

            (int comb, int sp, int sq)[] type;
                
            Solve();
            void Solve()
            {

                int a = int.Parse(Console.ReadLine());
                int b = int.Parse(Console.ReadLine());

                type = new (int comb, int sp, int sq)[7] { (153, 2, 16), (816, 3, 15), (8568, 5, 13), (31824, 7, 11),
                (31824, 11, 7), (8568, 13, 5), (18, 17, 1) };

                double ret1 = 0.0;
                double ret2 = 0.0;

                for (int i = 0; i < 7; i++)
                {

                    ret1 += GetProb(i, a, 100 - a);
                    ret2 += GetProb(i, b, 100 - b);
                }

                Console.Write($"{ret1 + ret2 - ret1 * ret2:0.00000000}");
            }

            double GetPow(double _a, int _exp)
            {

                double ret = 1.0;
                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret *= _a;

                    _a *= _a;
                    _exp >>= 1;
                }

                return ret;
            }

            double GetProb(int _type, int _p, int _q)
            {

                return type[_type].comb * GetPow(_p / 100.0, type[_type].sp) * GetPow(_q / 100.0, type[_type].sq);
            }
        }
    }

#if other
using System;

namespace baekjoon_algorithm
{
    class Program
    {
		static double[,,] dp = new double[20, 20, 20];

        static void Main()
        {
			var a = double.Parse(Console.ReadLine()) / 100;
			var b = double.Parse(Console.ReadLine()) / 100;

			dp[0, 0, 0] = 1.0;

			for (int i = 1; i <= 18; i++)
			{
				for (int j = 0; j <= i; j++)
				{
					for (int k = 0; k <= i; k++)
					{
						if (j > 0) 
							dp[i, j, k] += dp[i - 1, j - 1, k] * a * (1 - b);
						if (k > 0) 
							dp[i, j, k] += dp[i - 1, j, k - 1] * (1 - a) * b;
						if (j > 0 && k > 0) 
							dp[i, j, k] += dp[i - 1, j - 1, k - 1] * a * b;
						
						dp[i, j, k] += dp[i - 1, j, k] * (1 - a) * (1 - b);
					}
				}
			}

			double result = 0;

			for (int i = 0; i <= 18; i++)
				for (int j = 0; j <= 18; j++)
					if (IsPrimenNumber(i) || IsPrimenNumber(j)) 
						result += dp[18, i, j];

			Console.WriteLine(result);
		}

        private static bool IsPrimenNumber(int number)
        {
            if (number <= 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
#endif
}
