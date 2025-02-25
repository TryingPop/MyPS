using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 19
이름 : 배성훈
내용 : 치킨 쿠폰
    문제번호 : 1673번

    구현, 수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1285
    {

        static void Main1285(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n, k;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int cp = n;
                int ret = n;
                while (cp >= k)
                {

                    int use = cp / k;
                    cp %= k;
                    cp += use;
                    ret += use;
                }

                sw.WriteLine(ret);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = 0;
                k = 0;
            }

            bool Input()
            {

                string temp = sr.ReadLine();
                if (string.IsNullOrEmpty(temp)) return false;

                string[] input = temp.Split();
                n = int.Parse(input[0]);
                k = int.Parse(input[1]);

                return true;
            }
        }
    }

#if other
// #include <cstdio>

int main ()
{
	int n, k;
	while(scanf("%d %d", &n, &k) == 2)
	{
		int b=n;
		for(; n>=k;)
		{
			b+=n/k;
			n=n/k+n%k;
		}
		printf("%d\n", b);
	}
}
#endif
}
