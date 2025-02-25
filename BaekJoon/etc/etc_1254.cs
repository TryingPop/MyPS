using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 6
이름 : 배성훈
내용 : 괄호 추가하기 3
	문제번호 : 16639번

	dp 문제다.
	괄호를 마음대로 넣을 수 있는 것은 연산순서를 마음대로 조절할 수 있다는 것과 동치이다.
	minDp[i][j] = val를 i ~ j 범위 안에 모든 연산을 실행할 때 최솟값 val을 담게 하면 된다.
	maxDp[i][j] = val를 i ~ j 범위 안에 모든 연산을 실행할 때 최댓값 val을 담는다
	
	최댓값과 최솟값을 저장하는 이유는
	곱셈 연산에서 음수 x 음수 = 양수이므로 최솟값도 함께 고려해야한다.
	그리고 과정 중간에 뺄셈 연산에서 작은거 - 큰거를 하면 최소값이 나올 수 있어 둘 다 필요하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1254
    {

        static void Main1254(string[] args)
        {

            int n;
            string input;
            long[][] minDp, maxDp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                DFS(0, n - 1);

                Console.Write(maxDp[0][n - 1]);

                void DFS(int _i, int _j)
                {

                    if (minDp[_i][_j] != -1 && maxDp[_i][_j] != -1)
                        return;

                    long min = minDp[_i][_j];
                    long max = maxDp[_i][_j];

                    min = long.MaxValue;
                    max = long.MinValue;

                    long min1, min2, max1, max2, chk = 0;
                    for (int i = _i + 1; i < _j; i += 2)
                    {

                        char op = input[i];

                        DFS(_i, i - 1);
                        DFS(i + 1, _j);

                        min1 = minDp[_i][i - 1];
                        max1 = maxDp[_i][i - 1];
                        min2 = minDp[i + 1][_j];
                        max2 = maxDp[i + 1][_j];

                        // out 매개변수는 지역함수 사용 불가능
                        chk = Calc(min1, min2, op);
                        SetMinMax();

                        chk = Calc(min1, max2, op);
                        SetMinMax();

                        chk = Calc(max1, min2, op);
                        SetMinMax();

                        chk = Calc(max1, max2, op);
                        SetMinMax();
                    }

                    minDp[_i][_j] = min;
                    maxDp[_i][_j] = max;

                    void SetMinMax()
                    {

                        min = Math.Min(min, chk);
                        max = Math.Max(max, chk);
                    }
                }
            }

            long Calc(long _f, long _t, char _op)
            {

                switch (_op)
                {

                    case '+':
                        return _f + _t;

                    case '-':
                        return _f - _t;

                    case '*':
                        return _f * _t;
                }

                return -1;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                input = sr.ReadLine();

                minDp = new long[input.Length][];
                maxDp = new long[input.Length][];

                for (int i = 0; i < input.Length; i++)
                {

                    minDp[i] = new long[input.Length];
                    maxDp[i] = new long[input.Length];

                    Array.Fill(minDp[i], -1);
                    Array.Fill(maxDp[i], -1);
                }

                for (int i = 0; i < input.Length; i += 2)
                {

                    minDp[i][i] = input[i] - '0';
                    maxDp[i][i] = input[i] - '0';
                }
            }
        }
    }

#if other
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CodeTestCS
{
	class Program
	{
		static readonly StreamReader reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
		static readonly StreamWriter writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
		static readonly StringBuilder std = new StringBuilder();

		static int n;
		static string syntex;
		static readonly List<long> numList = new List<long>();
		static readonly List<char> opList = new List<char>();
		static readonly Dictionary<(int, int), long> dpMax = new Dictionary<(int, int), long>();
		static readonly Dictionary<(int, int), long> dpMin = new Dictionary<(int, int), long>();

		static void InputCase()
		{
			int[] input = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
			n = input[0];
			syntex = reader.ReadLine();

			for (int i = 0; i < n; i++)
			{
				// 짝수번째는 숫자
				if (i % 2 == 0)
				{
					if (long.TryParse(syntex[i].ToString(), out var num))
					{
						numList.Add(num);
					}
				}
				// 홀수번째는 연산자
				else
				{
					opList.Add(syntex[i]);
				}
			}
		}

		static long Calculate(long a, long b, char op)
		{
			switch (op)
			{
				case '+':
					return a + b;
				case '-':
					return a - b;
				case '*':
					return a * b;
			}
			return a;
		}

		static void SetMaxMin(int start, int end, long max, long min)
		{
			// 최대값 갱신
			if (dpMax.TryGetValue((start, end), out var value))
			{
				if (value < max)
				{
					dpMax[(start, end)] = max;
				}
			}
			else
			{
				dpMax.Add((start, end), max);
			}

			// 최소값 갱신
			if (dpMin.TryGetValue((start, end), out value))
			{
				if (value > min)
				{
					dpMin[(start, end)] = min;
				}
			}
			else
			{
				dpMin.Add((start, end), min);
			}
		}

		static void BottomUp()
		{
			for (int add = 0; add < numList.Count; add++)
			{
				for (int start = 0; start < numList.Count - add; start++)
				{
					// [start, start]는 최대값과 최소값이 그냥 num
					if (add == 0)
					{
						SetMaxMin(start, start, numList[start], numList[start]);
					}
					else
					{
						for (int mid = start; mid < start + add; mid++)
						{
							// [start, start + add] = [start, mid] op [min + 1, start + add]
							var temp = new List<long>();
							temp.Add(Calculate(dpMax[(start, mid)], dpMax[(mid + 1, start + add)], opList[mid]));
							temp.Add(Calculate(dpMax[(start, mid)], dpMin[(mid + 1, start + add)], opList[mid]));
							temp.Add(Calculate(dpMin[(start, mid)], dpMax[(mid + 1, start + add)], opList[mid]));
							temp.Add(Calculate(dpMin[(start, mid)], dpMin[(mid + 1, start + add)], opList[mid]));
							temp.Sort();
							SetMaxMin(start, start + add, temp[3], temp[0]);
						}
					}
				}
			}

			// 최대값 반환
			if (dpMax.TryGetValue((0, numList.Count-1), out var answer))
			{
				std.Append(answer);
				writer.Write(std);
				writer.Close();
			}
		}

		static void Main(string[] args)
		{
			//int[] input = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);

			InputCase();
			BottomUp();

			//std.Append(" ");
			//writer.Write(std);
			//writer.Close();
		}
	}
}
#endif
}
