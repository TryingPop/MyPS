using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 9 
이름 : 배성훈
내용 : 짚신벌레
    문제번호 : 2560번

    dp, 누적합 문제다
    큐로 시뮬레이션 돌려 풀었다
*/
namespace BaekJoon.etc
{
    internal class etc_1041
    {

        static void Main1041(string[] args)
        {

            int MOD = 1_000;
            int n;
            Queue<int> A, B, D;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int sumA = 1;
                int sumB = 0;
                int sumD = 0;

                A.Dequeue();
                A.Enqueue(1);
                for (int i = 0; i < n; i++)
                {

                    DayUpdate();
                }

                Console.Write((sumA + sumB + sumD) % MOD);

                void DayUpdate()
                {

                    int a = A.Dequeue();
                    int b = B.Dequeue();

                    sumB = (sumB - b + a + MOD) % MOD;
                    B.Enqueue(a);

                    sumD = (sumD - D.Dequeue() + b + MOD) % MOD;
                    D.Enqueue(b);

                    sumA = (sumA - a + sumB + MOD) % MOD;
                    A.Enqueue(sumB);
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                int a = int.Parse(temp[0]);
                int b = int.Parse(temp[1]);
                int d = int.Parse(temp[2]);
                n = int.Parse(temp[3]);

                A = new(a + 1);
                B = new(b - a + 1);
                D = new(d - b + 1);

                for (int i = 0; i < a; i++)
                {

                    A.Enqueue(0);
                }

                for (int i = 0; i < b - a; i++)
                {

                    B.Enqueue(0);
                }

                for (int i = 0; i < d - b; i++)
                {

                    D.Enqueue(0);
                }
            }
        }
    }

#if other
// #include <cstdio>
int main() {
	int ary[10001] = { 1 }, adult = 0;
	int a, b, d, n, front = 0, end;
	scanf("%d%d%d%d", &a, &b, &d, &n);
	if (a > n) {
		printf("1");
		return 0;
	}
	end = d;
	while (n--) {
		a = a != 0 ? a - 1 : end;
		b = b != 0 ? b - 1 : end;
		d = d != 0 ? d - 1 : end;
		front = front != 0 ? front - 1 : end;
		adult = (adult + ary[a] - ary[b] + 1000) % 1000;
		ary[front] = adult;
		ary[d] = 0;
	}
	a = 0;
	for (int i = 0; i <= end; i++)
		a += ary[i];
	printf("%d", a % 1000);
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onekara
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string[] ss = s.Split();

            int a = int.Parse(ss[0]);
            int b = int.Parse(ss[1]);
            int d = int.Parse(ss[2]);
            int N = int.Parse(ss[3]);

            int[] cnts = new int[N+1];
            cnts[0] = 1;

            int nextBirth = 0;
            for(int i=1; i<=N; i++)
            {
                if (i - a >= 0)
                    nextBirth += cnts[i - a];
                if (i - b >= 0)
                    nextBirth -= cnts[i - b];
                nextBirth = (nextBirth + 1000) % 1000;

                cnts[i] = nextBirth;
            }
            int total = 0;

            for(int i=N-d+1; i<=N; i++)
            {
                if (i < 0)
                    continue;
                total = (total + cnts[i]) % 1000;
            }

            Console.WriteLine(total);

        }
    }
}
#endif
}
