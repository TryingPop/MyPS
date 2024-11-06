using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 공 포장하기
    문제번호 : 12981번

	그리디, 많은 조건분기 문제다
	범위가 100만이라 그리디가 아닌 dp와 dfs 탐색으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0543
    {

        static void Main543(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int[,,] dp = new int[arr[0] + 1, arr[1] + 1, arr[2] + 1];
            dp[0, 0, 0] = 1;

            int ret = DFS(arr[0], arr[1], arr[2]);
            Console.WriteLine(ret);

            int DFS(int _a, int _b, int _c)
            {

                _a = _a < 0 ? 0 : _a;
                _b = _b < 0 ? 0 : _b;
                _c = _c < 0 ? 0 : _c;

                if (dp[_a, _b, _c] > 0) return dp[_a, _b, _c] - 1;
                dp[_a, _b, _c] = 1;
                int ret = 10_000;

                int calc;
                if (_a > 0)
                {

                    calc = DFS(_a - 3, _b, _c) + 1;
                    ret = calc < ret ? calc : ret;
                }

                if (_b > 0)
                {

                    calc = DFS(_a, _b - 3, _c) + 1;
                    ret = calc < ret ? calc : ret;
                }

                if (_c > 0)
                {

                    calc = DFS(_a, _b, _c - 3) + 1;
                    ret = calc < ret ? calc : ret;
                }

                calc = DFS(_a - 1, _b - 1, _c - 1) + 1;
                ret = calc < ret ? calc : ret;

                dp[_a, _b, _c] = ret + 1;
                return ret;
            }
        }
    }
#if other
using StreamWriter wt = new(Console.OpenStandardOutput());
using StreamReader rd = new(Console.OpenStandardInput());
var input = rd.ReadLine().Split().Select(int.Parse).ToArray();
int count = 0, a = 0, b = 0;
count = input.Sum(i => i / 3);
input = input.Select(i => i % 3).ToArray();
a = input.Count(i => i != 0);

while (input.Sum() != 0)
{
    if (input[0] > 0) input[0]--;
    if (input[1] > 0) input[1]--;
    if (input[2] > 0) input[2]--;
    b++;
}

wt.Write(count + Math.Min(a, b));
#elif other2
using System;
using System.Linq;

public class Test
{
	public static void Main()
	{
		int[] balls = Read_ints();
		int boxCnt = 0, packed;

		for (int i = 0; i < balls.Length; i++)
		{
			packed = Packing_OneColor(balls[i]);
			balls[i] -= packed * 3;
			boxCnt += packed;
		}
		
		packed = Packing_Colorful(balls[0], balls[1], balls[2]);
		boxCnt += packed;
		int left = balls.Sum();
		left -= packed * 3;

		if (left != 0)
			boxCnt += left > 2 ? 2 : 1;

		Console.Write(boxCnt);
	}

	static int[] Read_ints()
	{
		string input = Console.ReadLine();
		string[] strArr = input.Split(' ');
		int len = strArr.Length;
		int[] nums = new int[len];
		for (int i = 0; i < len; i++)
			nums[i] = Convert.ToInt32(strArr[i]);
		return nums;
	}
	static int Packing_OneColor(int ball)
	{
		int cnt = 0;
		if (ball > 2)
		{
			cnt = ball / 3;
		}
		return cnt;
	}
	static int Packing_Colorful(int r, int g, int b)
	{
		int cnt = 0;
		for (int i = 0; i < 2; i++)
		{
			if ((r > 0) && (g > 0) && (b > 0))
			{
				cnt++;
				r--;
				g--;
				b--;
			}
		}
		return cnt;
	}
}
#endif
}
