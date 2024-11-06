using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 1
이름 : 배성훈
내용 : 소풍
    문제번호 : 1242번

    수학 문제다
    아이디어는 다음과 같다
    시뮬레이션 돌린다
    k번 보다 작은 앞 i번 사람이 빠져나가면 k번째가 k - 1번이되므로
    k의 번호를 1 빼고 i번부터 시작한다
    이렇게 k번이 빠져나갈때까지 시뮬레이션 돌리면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0930
    {

        static void Main930(string[] args)
        {

            int n, m, k;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {


                int r = n;
                int ret = 0;
                int b = 0;
                while(r > 0)
                {

                    b = (b + m) % r;
                    ret++;
                    r--;
                    if (b == k) break;
                    else if (b < k) k--;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]) - 1;
                k = int.Parse(temp[2]) - 1;
            }
        }
    }

#if other
// #include <stdio.h>

int main()
{
	int n, k, m;
	scanf("%d %d %d", &n, &k, &m);
	int ans = 0;
	while (m!=0)
	{
		m = m - k;
		while (m < 0)
			m = m + n - ans;
		ans++;
	}
	printf("%d", ans);
}
#endif
}
