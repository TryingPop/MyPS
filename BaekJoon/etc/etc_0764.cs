using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 21
이름 : 배성훈
내용 : 마지막 팩토리얼 수
    문제번호 : 2553번

    수학, 정수론 문제다
    에라토스 테네스의 체이론을 이용해 n 이하의 소수를 찾고
    뒤의 0을 제거한 뒤 팩토리얼 값을 2와 5를 제외한 소수로 소인수분해하면서 찾아갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0764
    {

        static void Main764(string[] args)
        {

            StreamReader sr;
            int n;
            bool[] notPrime;
            int[][] mul;
            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput());
                string str = sr.ReadLine();
                n = int.Parse(str);

                int ret = GetRet();
                Console.Write(ret);
            }

            int GetRet()
            {

                if (n < 5)
                {

                    int[] _ = new int[5] { 0, 1, 2, 6, 4 };
                    return _[n];
                }

                notPrime = new bool[n + 1];

                int ret = 1;


                ChkPrime();
                SetMul();
                ret *= DeleteTwoAndFive();

                for (int i = 2; i <= n; i++)
                {

                    if (notPrime[i]) continue;
                    int cnt = Cnt(i);
                    int val = i % 10;

                    cnt = (cnt - 1) % mul[val].Length;

                    ret = (ret * mul[val][cnt]) % 10;
                }

                return ret;
            }

            void SetMul()
            {

                mul = new int[10][];
                mul[1] = new int[1] { 1 };
                mul[2] = new int[4] { 2, 4, 8, 6 };
                mul[3] = new int[4] { 3, 9, 7, 1 };
                mul[7] = new int[4] { 7, 9, 3, 1 };
                mul[9] = new int[2] { 9, 1 };
            }

            void ChkPrime()
            {

                for (int i = 2; i <= n; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = (i << 1); j <= n; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            int Cnt(int _div)
            {

                int ret = 0;
                int div = _div;
                while(div <= n)
                {

                    ret += n / div;
                    div = div * _div;
                }

                return ret;
            }

            int DeleteTwoAndFive()
            {

                int ret = Cnt(2);
                ret -= Cnt(5);

                ret--;
                ret %= 4;

                notPrime[2] = true;
                notPrime[5] = true;
                return mul[2][ret];
            }
        }
    }

#if other
long n = long.Parse(Console.ReadLine());
long [] num = new long [n];
num[0] =  1;
for (long i = 1; i < n; i++)
{
    num[i] = num[i-1]*(i+1);
    num[i] = num[i] % 10000000;
    while (num[i] % 10 == 0)
        num[i] /= 10;
}
Console.WriteLine(num[n-1] % 10);
#elif other2
x=1;
main(t,n)
{
    for(scanf("%d",&n);x=x%5?t=t*x%10,n--:x/5*3;);
    printf("%d",t);
}

#endif
}
