using System;
using System.IO;

/*
날짜 : 2025. 3. 24
이름 : 배성훈
내용 : 합
    문제번호 : 1132번

    그리디 문제다.
    브루트포스와 백트래킹으로도 통과한다.
    처음에는 브루트포스로 3초 안으로 해결했다.

    그런데 다른 사람 맞춘 시간을 보니, 1초도 안되었다.
    그래서 고민하니, 그리디로 접근할 수 있음을 깨달았다.

    해당 자리의 문자를 1로 표현했을 때,
    값들 중 큰 것에 가장 큰 값을 배치하면 된다.
    다만, 첫 문자로 온 문자는 0이 될 수 없으므로 0을 먼저 찾고 나머지를 큰 것부터 배치해갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1461
    {
        
        static void Main1461(string[] args)
        {

            // 1132 합
            int n;
            string[] input;
            bool[] notZero;
            int[] val;

            Input();

            GetRet();

            void GetRet()
            {

#if first
                long ret = 0;

                for (int i =  'A'; i <= 'J'; i++)
                {

                    Array.Fill(val, -1);
                    if (notZero[i]) continue;
                    val[i] = 0;
                    DFS();
                }

                Console.Write(ret);

                void DFS(int _dep = 1)
                {

                    if (_dep == 10)
                    {

                        long cur = 0;
                        for (int i = 0; i < n; i++)
                        {

                            cur += GetVal(i);
                        }

                        ret = Math.Max(ret, cur);
                        return;
                    }

                    for (int i = 'A'; i <= 'J'; i++)
                    {

                        if (val[i] != -1) continue;
                        val[i] = _dep;

                        DFS(_dep + 1);

                        val[i] = -1;
                    }
                }

                long GetVal(int _idx)
                {

                    long ret = 0;
                    for (int i = 0; i < input[_idx].Length; i++)
                    {

                        ret = ret * 10 + val[input[_idx][i]];
                    }

                    return ret;
                }
#else

                (long s, int c)[] sum = new (long s, int c)[10];

                for (int i = 'A'; i <= 'J'; i++)
                {

                    Sum(i);
                    sum[i - 'A'].c = i;
                }

                Array.Sort(sum, (x, y) => x.s.CompareTo(y.s));

                int zero = -1;
                for (int i = 0; i < 10; i++)
                {

                    if (notZero[sum[i].c]) continue;
                    zero = i;
                    break;
                }

                int val = 1;
                long ret = 0;
                for (int i = 0; i < 10; i++)
                {

                    if (zero == i) continue;
                    ret += sum[i].s * val;
                    val++;
                }

                Console.Write(ret);

                void Sum(int _c)
                {

                    for (int i = 0; i < n; i++)
                    {

                        long ret = 0;
                        for (int j = 0; j < input[i].Length; j++)
                        {

                            ret *= 10;
                            if (input[i][j] != _c) continue;
                            ret++;
                        }

                        sum[_c - 'A'].s += ret;
                    }
                }
#endif
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                input = new string[n];
                for (int i = 0; i < n; i++)
                {

                    input[i] = sr.ReadLine();
                }

                notZero = new bool[255];
                val = new int[255];
                Array.Fill(val, -1);
                for (int i = 0; i < n; i++)
                {

                    notZero[input[i][0]] = true;
                }
            }
        }
    }

#if other
// #define _CRT_SECURE_NO_WARNINGS
// #include <stdio.h>
// #include <string.h>

int main()
{
	int n, len, p[10] = { 0, }, po;
	long long a[10] = { 0, }, temp, min = 10000000000000, sum = 0;
	char c[13];
	scanf("%d", &n);
	for (int i = 0; i < n; i++)
	{
		scanf("%s", c);
		len = strlen(c);
		p[c[0] - 'A'] = 1;
		temp = 1;
		for (int j = 0; j < len; j++)
		{
			a[c[len - 1 - j] - 'A'] += temp;
			temp *= 10;
		}
	}
	for (int i = 0; i < 10; i++)
	{
		if (p[i] == 0)
		{
			if (min>a[i])
			{
				min = a[i];
				po = i;
			}
		}
	}
	a[po] = 0;
	for (int i = 0; i < 10; i++)
	{
		for (int j = i + 1; j < 10; j++)
		{
			if (a[i]>a[j])
			{
				temp = a[i];
				a[i] = a[j];
				a[j] = temp;
			}
		}
	}
	for (int i = 1; i < 10; i++)
		sum += i*a[i];
	printf("%lld\n", sum);
	return 0;
}
#endif
}
