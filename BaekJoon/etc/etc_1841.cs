using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 26
이름 : 배성훈
내용 : 특이한 수열
    문제번호 : 13018번

    해 구성하기 문제다.
    1의 경우 gcd를 하면 항상 1이므로 개수가 0이다.
    그래서 n = k인 경우 언제나 불가능하다!

    이제 k < n인 경우를 보자.
    뒤에서부터 i = 0, 1, 2, ..., n - k + 1에 대해
    n - k + 1의 값을 채운다.

    그러면 우리가 찾는 k개가 모두 gcd(i, a[i]) = i > 1로 된다.
    즉, k개가 나왔다.

    이제 나머지 j = 2, 3, ..., n - k에 대해 값을 j - 1로 채우면,
    gcd(j, j - 1)은 항사 1이므로!(귀류법과 수학적 귀납법으로 보일 수 있다.)
    채운다. 이제 1에는 남은 값을 채우면 나머지 n - k에대해 모두 1이 되게 만들 수 있다.

    이렇게 k에 대해 만들 수 있음을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1841
    {

        static void Main1841(string[] args)
        {

            int n, k;

            Input();

            GetRet();

            void GetRet()
            {

                string IMPO = "Impossible";
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (k == n) sw.Write(IMPO);
                else
                {

                    sw.Write(n - k);
                    sw.Write(' ');

                    for (int i = 2; i <= n - k; i++)
                    {

                        sw.Write($"{i - 1} ");
                    }

                    for (int i = n - k + 1; i <= n; i++)
                    {

                        sw.Write($"{i} ");
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);
            }
        }
    }
}
