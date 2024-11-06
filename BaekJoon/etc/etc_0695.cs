using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 15
이름 : 배성훈
내용 : 세 용액
    문제번호 : 2473번

    두 포인터, 정렬, 이분탐색 문제다
    두 포인터를 이용해 풀었다

    처음에는 이분 탐색으로 접근하려고 했다
    l = 0, r = n - 1로하고
    sum = arr[l] + arr[r]로 놓는다

    arr[mid] < sum 이면 l을 옮겨서 mid 값을 커지게, 이외는 r을 옮겨서 mid 값을 작아지게 하는 이분탐색1과
    arr[mid] <= sum 이면 l을 옮기는 이분 탐색 2를 두번 해서 값을 비교하려고 했으나
    l, r 이동을 n^2 경우 말고는 안떠올라 해당 방법은 포기했다

*/

namespace BaekJoon.etc
{
    internal class etc_0695
    {

        static void Main695(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr;
            int[] ret;
            long diff;

            Solve();

            void Solve()
            {

                Input();

                Find();

                Console.Write($"{arr[ret[0]]} {arr[ret[1]]} {arr[ret[2]]}");
            }

            void Find()
            {

                Array.Sort(arr);
                ret = new int[3];
                diff = 4_000_000_000;

                for (int i = 0; i < n; i++)
                {

                    ChkL(i);
                    // ChkR(i);
                }
            }

            void ChkL(int _s) 
            {

                int l = _s + 1;
                int r = n - 1;

                long sum;
                while (l < r)
                {

                    sum = arr[l] + arr[r];
                    sum += arr[_s];

                    long calc = Math.Abs(sum);
                    if (calc < diff)
                    {

                        diff = calc;

                        ret[0] = _s;
                        ret[1] = l;
                        ret[2] = r;
                    }

                    if (sum < 0) l++;
                    else r--;
                }
            }

            /*
            void ChkR(int _s)
            {

                int l = _s + 1;
                int r = n - 1;

                long sum;
                while (l < r)
                {

                    sum = arr[l] + arr[r];
                    sum += arr[_s];

                    long calc = Math.Abs(sum);
                    if (calc < diff)
                    {

                        diff = calc;

                        ret[0] = _s;
                        ret[1] = l;
                        ret[2] = r;
                    }

                    if (sum <= 0) l++;
                    else r--;
                }
            }
            */

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == -1) return 0;

                bool plus = c != '-';
                int ret;

                if (plus) ret = c - '0';
                else ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
namespace _2473
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            long N = long.Parse(Console.ReadLine()!);
            long[] numbers = new long[N];
            string[] input = Console.ReadLine()!.Split(' ');

            for (long i = 0; i < N; i++)
            {
                numbers[i] = long.Parse(input[i]);
            }

            Array.Sort(numbers);

            long min = long.MaxValue;
            long temp;
            long negative = 0;
            long positive = 0;
            long first = 0;

            for (long i = 0; i < N; i++)
            {
                long low = i + 1;
                long high = N - 1;

                while (low < high)
                {
                    temp = Math.Abs(numbers[i] + numbers[low] + numbers[high]);

                    if (temp < min)
                    {
                        min = temp;
                        negative = numbers[low];
                        positive = numbers[high];
                        first = numbers[i];
                    }

                    if (numbers[i] + numbers[low] + numbers[high] < 0)
                    {
                        low++;
                    }
                    else
                    {
                        high--;
                    }
                }
            }
            Console.WriteLine($"{first} {negative} {positive}");
        }
    }
}
#elif other2
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var n = ScanInt();
var liquids = new int[n];
for (int i = 0; i < n; i++)
    liquids[i] = ScanInt();
Array.Sort(liquids);

var diff = long.MaxValue;
int ret0 = 0, ret1 = 0, ret2 = 0;
for (int i = 0; i < liquids.Length - 2; i++)
{
    int l = i + 1, r = n - 1;
    while (l < r)
    {
        var sum = (long)liquids[i] + liquids[l] + liquids[r];
        if (diff > Math.Abs(sum))
        {
            diff = Math.Abs(sum);
            (ret0, ret1, ret2) = (liquids[i], liquids[l], liquids[r]);
        }
        if (sum < 0)
            l++;
        else if (sum > 0)
            r--;
        else
        {
            Console.Write($"{ret0} {ret1} {ret2}");
            return;
        }
    }
}
Console.Write($"{ret0} {ret1} {ret2}");

int ScanInt()
{
    int c = sr.Read(), n = 0;
    if (c != '-')
    {
        n = c - '0';
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
    }
    else
    {
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n - (c - '0');
        }
    }
    return n;
}
#elif other3
// #include <stdio.h>
// #include <algorithm>
// #define lli long long int
using namespace std;

int main()
{
	lli Arr[5003], a, b, c, mn = 3e10, sum=0, ssum = 0;
	int i, n;
	int l, r;
	bool chk = false;
	scanf("%d", &n);

	for( i = 0; i < n; i++ )
    {
        scanf("%lld", &Arr[i]);
    }

	sort(Arr, Arr + n);
	lli temp;

	for( i = 0; i < n-2; i++ )
	{
		temp = Arr[i];
		l = i + 1;
		r = n - 1;
		while (l < r)
		{
		    sum = temp + Arr[l]+Arr[r];
		    ssum = sum;
		    if( ssum < 0 ) ssum *= -1;
			if( ssum < mn )
			{
				mn = ssum;
				a = temp;
				b = Arr[l];
				c = Arr[r];
				if(a+b+c == 0)
				{
					printf("%lld %lld %lld\n", a, b, c);
					return 0;
				}
			}

			if (sum < 0) l++;
			else r--;
		}
	}
	printf("%lld %lld %lld\n", a, b, c);
}

#endif
}
