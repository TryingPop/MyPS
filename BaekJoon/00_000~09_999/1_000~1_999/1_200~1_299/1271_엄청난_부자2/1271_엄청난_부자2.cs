using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 28
이름 : 배성훈
내용 : 엄청난 부자 2
    문제번호 : 1271번

    사칙연산, 큰 수 연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1486
    {

        static void Main1486(string[] args)
        {

			string[] input = Console.ReadLine().Split();
#if first
            BigInteger a = BigInteger.Parse(input[0]), b = BigInteger.Parse(input[1]);
            BigInteger q = a / b;
            BigInteger r = a - q * b;

            Console.Write($"{q}\n{r}");
#else
			// 숫자를 1자리씩 보유
			int[] a = new int[1_001], b = new int[1_001];
			int[] q = new int[1_001];
			int[][] mulB = new int[10][];

			for (int i = 1; i < 10; i++)
			{

				mulB[i] = new int[1_001];
			}

			StrToList(input[0], a);
			StrToList(input[1], b);

			if (Comp(a, b) < 0)
			{

				// 반례처리
				Console.Write(0);
				Console.Write(input[1]);
				return;
			}

			for (int i = 1; i < 10; i++)
			{

				Mul(b, i, mulB[i]);
			}

			int shift = input[0].Length - input[1].Length;
			for (int i = shift; i >= 0; i--)
			{

				int m = 9;
				for (int j = 1; j < 10; j++)
				{

					if (Comp(a, mulB[j], i) >= 0) continue;
					m = j - 1;
					break;
				}

				if (m > 0) Sub(a, mulB[m], i);
				q[i] = m;
			}

			using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);


			int len = GetNotZero(q);
			for (int i = len; i >= 0; i--)
			{

				sw.Write(q[i]);
			}

			sw.Write('\n');

			len = GetNotZero(a);
			for (int i = len; i >= 0; i--)
			{

				sw.Write(a[i]);
			}

			int GetNotZero(int[] _arr)
			{

				for (int i = _arr.Length - 1; i >= 0; i--)
				{

					if (_arr[i] > 0) return i;
				}

				return 0;
			}

			void ChkNeg(int[] _arr)
			{

				for (int i = 0; i < _arr.Length - 1; i++)
				{

					while (_arr[i] < 0)
					{

						_arr[i] += 10;
						_arr[i + 1]--;
					}
				}
			}

			void ChkTen(int[] _arr)
			{

				for (int i = 0; i < _arr.Length; i++)
				{

					if (_arr[i] < 10) continue;
					int add = _arr[i] / 10;
					_arr[i] %= 10;

					if (i + 1 == _arr.Length) throw new("1000자리 넘는 숫자가 들어왔습니다.");
					_arr[i + 1] += add;
				}
			}

			void Sub(int[] _a, int[] _b, int _shiftB = 0)
			{

				for (int i = _shiftB; i < _a.Length; i++)
				{

					_a[i] -= _b[i - _shiftB];
				}

				ChkNeg(_a);
			}

			void Mul(int[] _a, int _mul, int[] _ret)
			{

				for (int i = 0; i < _a.Length; i++)
				{

					_ret[i] = _a[i] * _mul;
				}

				ChkTen(_ret);
			}

			int Comp(int[] _a, int[] _b, int _shiftB = 0)
			{

				for (int i = _a.Length - 1; i >= _shiftB; i--)
				{

					if (_a[i] != _b[i - _shiftB])
						return _a[i].CompareTo(_b[i - _shiftB]);
				}

				for (int i = 0; i < _shiftB; i++)
				{

					if (_a[i] > 0) return 1;
				}

				return 0;
			}

			void StrToList(string _str, int[] _arr)
			{

				for (int i = _str.Length - 1, idx = 0; i >= 0; i--, idx++)
				{

					_arr[idx] = _str[i] - '0';
				}
			}
#endif
		}
    }

#if other
// #include <stdio.h>
// #include <string.h>
char dividend[1002], divisor[10][1002], quotient[1002] = { 0 };
int compare(char a[], char b[])
{
	int i;
	for (i = 0; a[i] == b[i] && a[i] != '\0' && b[i] != '\0'; i++);
	if (a[i] > b[i])
		return 1;
	else if (a[i] < b[i])
		return -1;
	else
		return 0;
}
int main()
{
	int a, b, c, d, len_dividend, len_divisor;
	dividend[0] = '0', divisor[1][0] = '0';
	scanf("%s %s", &dividend[1], &divisor[1][1]);
	len_dividend = strlen(dividend);
	len_divisor = strlen(divisor[1]);
	for (a = 0; a < len_divisor; a++)
	{
		divisor[0][a] = '0';
	}
	int times = len_dividend - len_divisor + 1;
	c = 0;
	for (int i = 2; i < 10; i++)
	{
		for (int j = len_divisor - 1; j >= 0; j--)
		{
			b = divisor[i - 1][j] + divisor[1][j] - 2 * '0' + c;
			divisor[i][j] = b % 10 + '0';
			c = b / 10;
		}
	}
	c = 0, b = 0;
	for (int i = 0; i < times; i++)
	{
		//comparison from 9 to 0
		for (int j = 9; j >= 0; j--)
		{
			if (compare(dividend + i, divisor[j]) >= 0)
			{
				quotient[i] = j + '0';
				c = 0;
				for (int k = len_divisor - 1; k > -1; k--)
				{
					b = dividend[i + k] - divisor[j][k] - c;
					if (b < 0)
					{
						b += 10;
						c = 1;
					}
					else
					{
						c = 0;
					}
					dividend[i + k] = b + '0';
				}
				break;
			}
		}
	}
	//print quotient skipping leading zeros
	d = 0;
	for (int i = 0; i < times; i++)
	{
		if (quotient[i] != '0')
		{
			d = 1;
			printf("%c", quotient[i]);
		}
		else if (d == 1)
		{
			printf("%c", quotient[i]);
		}
		else
		{
			if (i == times - 1)
			{
				putchar('0');
			}
		}
	}
	printf("\n");
	//print remainder
	d = 0;
	for (int i = 0; i < len_dividend; i++)
	{
		if (dividend[i] != '0')
		{
			d = 1;
			printf("%c", dividend[i]);
		}
		else if (d == 1)
		{
			printf("%c", dividend[i]);
		}
		else if (i == len_dividend - 1)
		{
			putchar('0');
		}
	}
	printf("\n");
	return 0;
}

#endif
}
