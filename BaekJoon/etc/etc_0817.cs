using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 시각
    문제번호 : 18312번

    구현, 문자열, 브루트포스 문제다
    상황을 구현해 해결했다
    조금 더 효율적으로 한다면, 시간, 분, 초를 따로 두는게 아닌
    하나의 변수에 합치고 나눌 수를 배열에 저장해 풀거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0817
    {

        static void Main817(string[] args)
        {

            int n, k;

            int h, m, s;
            int ret;
            Solve();
            void Solve()
            {

                Init();

                while (h <= n)
                {

                    if (FindK(h, m, s)) ret++;
                    TimeUp();
                }

                Console.Write(ret);
            }

            void TimeUp()
            {

                s++;
                if (s == 60)
                {

                    s = 0;
                    m++;
                }

                if (m == 60)
                {

                    m = 0;
                    h++;
                }
            }

            bool FindK(int _h, int _m, int _s)
            {

                int h = _h;
                int m = _m;
                int s = _s;

                for (int i = 0; i < 2; i++)
                {

                    int chkH = h % 10;
                    int chkM = m % 10;
                    int chkS = s % 10;

                    h /= 10;
                    m /= 10;
                    s /= 10;

                    if (chkH == k || chkM == k || chkS == k) return true;
                }

                return false;
            }

            void Init()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);

                h = 0;
                m = 0;
                s = 0;
                ret = 0;
            }
        }
    }

#if other
// #include <stdio.h>
int N, K, r = 0;
int main() {
	scanf("%d%d", &N, &K);
	for(int i = 0; i <= N; i++) {
		r += ((i % 10 == K || i / 10 == K) ? 3600 : (K < 6 ? 1575 : 684));
	}
	printf("%d", r);
}
#endif
}
