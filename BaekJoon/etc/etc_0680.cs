using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 6
이름 : 배성훈
내용 : 육각수
    문제번호 : 1229번

    dp 문제다
    육각수 점화식을 찾고, 이후에 이중 포문으로 풀었다
    1 ~ 100만 돌려보니, 6을 초과하는 것은 없었다
    그리고 100만 이하의 육각수는 710개 미만이었고
    이에 700 * 100만? 연산 해볼만하다 싶어 2중 포문으로 해결했다
    그러니, 약 1000ms쯤에 해결되었다
*/

namespace BaekJoon.etc
{
    internal class etc_0680
    {

        static void Main680(string[] args)
        {

            int[] six = new int[710];
            int n = int.Parse(Console.ReadLine());
            int[] ret = new int[n + 1];

            Array.Fill(ret, 6);
            int len = 1;
            for (int i = 1; i < six.Length; i++)
            {

                six[i] = six[i - 1] + 4 * (i - 1) + 1;

                if (six[i] > n) break;
                len++;
                ret[six[i]] = 1;
            }

            for (int i = 2; i <= n; i++)
            {

                for (int j = 1; j < len; j++)
                {

                    if (i >= six[j]) ret[i] = Math.Min(ret[i], ret[i - six[j]] + 1);
                    else break;
                }
            }

            Console.WriteLine(ret[n]);
        }
    }

#if other
/* acmicpc.net 1229 - 육각수
 * solved.ac - Gold IV
 * Source code by RebeLin
 * Time Limit: 2s (C#: 5s)
 * Memory Limit: 128MB (C#: 272MB)
 */

namespace BJ1229 {
  internal class Program {
    static StreamReader streamReader = new StreamReader (Console.OpenStandardInput());
    static StreamWriter streamWriter = new StreamWriter (Console.OpenStandardOutput());

    static int tableSum (in List<int> table, in int[] reference){
      int ret = 0;
      foreach (int refer in reference)
        ret += table[refer];

      return ret;
    }

    static bool hexagonalSum (in List<int> hexagonalNumbers, int i, int N){
      int hexagonalLimit = hexagonalNumbers.Count();
      int[] reference = new int[i];
      for (int j = 0; j < i; j++)
        reference[j] = 0;

      while (true){
        if (tableSum(hexagonalNumbers, reference) == N)
          return true;
        int j;
        bool stopper = true;
        for (j = 0; j < i; j++)
          if (++reference[j] < hexagonalLimit){
            stopper = false;
            break;
          }

        if (stopper)
          break;

        for (int k = 0; k < j; k++)
          reference[k] = reference[j];
      }

      return false;
    }

    public static void Main (string[] args){
      int N = int.Parse(streamReader.ReadLine());
      List<int> hexagonalNumbers = new List<int> ();
      
      hexagonalNumbers.Add(1);
      for (int i = 2; hexagonalNumbers.Last() + i*4 - 3 <= N; i++)
        hexagonalNumbers.Add(hexagonalNumbers.Last() + i*4 - 3);

      for (int i = 1; true; i++)
        if (hexagonalSum(hexagonalNumbers, i, N)){
          streamWriter.Write(i);
          streamWriter.Flush();
          return;
        }
    }
  }
}

#elif other2
namespace boj_1229
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());

            List<int> dp = new List<int> { 0, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 6, 2 };

            if (N < 13)
                Console.WriteLine(dp[N]);

            else
            {
                List<int> nums = new List<int> { 0 };
                for (int i = 0; i < 707; i++)
                    nums.Add(nums[i] - 4 + 5 + 4 * i);

                for (int i = 13; i < N + 1; i++)
                {
                    dp.Add(6);
                    foreach (int num in nums)
                    {
                        if (num > i) break;

                        dp[i] = Math.Min(dp[i - num] + 1, dp[i]);
                    }
                }

                Console.WriteLine(dp[N]);
            }
        }
    }
}
#elif other3
// #include <string>
// #include <vector>
// #include <iostream>

using namespace std;

int need_hexa_cnt(vector<int>& hexanum, int n, int upper_bound) {
	for (int i = 0; i != hexanum.size(); i++) {
		if (n == hexanum[i]) return 1;
	}
	if (upper_bound == 2) return 2;
	for (int i = 0; i != hexanum.size(); i++) {
		for (int j = 0; j != hexanum.size(); j++) {
			if (n == hexanum[i] + hexanum[j]) return 2;
		}
	}
	if (upper_bound == 3) return 3;
	for (int i = 0; i != hexanum.size(); i++) {
		for (int j = i; j != hexanum.size(); j++) {
			for (int k = j; k != hexanum.size(); k++) {
				if (n == hexanum[i] + hexanum[j] + hexanum[k]) return 3;
			}
		}
	}
	if (upper_bound == 4) return 4;
	for (int i = 0; i != hexanum.size(); i++) {
		for (int j = i; j != hexanum.size(); j++) {
			for (int k = j; k != hexanum.size(); k++) {
				for (int l = k; l != hexanum.size(); l++) {
					if (n == hexanum[i] + hexanum[j] + hexanum[k] + hexanum[l]) return 4;
				}
			}
		}
	}
	return upper_bound;
}

int main() {
	vector<int> hexanum;
	int n;
	cin >> n;
	for (int i = 0;; i++) {
		hexanum.push_back(2 * i * i + 3 * i + 1);	
		if (hexanum.back() > n) break;
	}
	if (n == 11 || n == 26) {
		cout << 6;
	}
	else if (n <= 130) {
		cout << need_hexa_cnt(hexanum, n, 5);
	}
	else if (n <= 146858) {
		cout << need_hexa_cnt(hexanum, n, 4);
	}
	else {
		cout << need_hexa_cnt(hexanum, n, 3);
	}
	return 0;
}
#endif
}
