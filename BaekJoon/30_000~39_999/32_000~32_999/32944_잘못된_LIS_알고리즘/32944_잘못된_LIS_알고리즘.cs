using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 5
이름 : 배성훈
내용 : 잘못된 LIS 알고리즘
    문제번호 : 32944번

    해 구성하기 문제다.
    먼저 1 ~ k - 1을 채운다. 이후에 n을 채운다.
    그러면 정현이의 방법으로 길이 k인 가장 긴 증가하는 부분수열이 만들어진다.

    다음으로 m - k + 1개를 n - 1를 끝으로 하는 수를 채워넣는다.
    그러면 중간에 n을 제외하면 LIS의 길이가 m을 바로 찾을 수 있다.

    이후에 나머지 숫자를 내림차순으로 배열하면 된다.
    안되는 경우는 n < m + 1인 경우 뿐이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1377
    {

        static void Main1377(string[] args)
        {

            int n, m, k;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                if (n < m + 1)
                {

                    sw.Write(-1);
                    return;
                }


                for (int i = 1; i < k; i++)
                {

                    sw.Write(i);
                    sw.Write(' ');
                }

                sw.Write(n);
                sw.Write(' ');

                for (int i = n - m + k - 1; i < n; i++)
                {

                    sw.Write(i);
                    sw.Write(' ');
                }

                for (int i = n - m + k - 2; i >= k; i--)
                {

                    sw.Write(i);
                    sw.Write(' ');
                }

                sw.Flush();
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                k = int.Parse(temp[2]);
            }
        }
    }
}
