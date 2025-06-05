using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 5
이름 : 배성훈
내용 : 1과 5
    문제번호 : 33615번

    수학 문제다.
    우선 각 자릿수 합이 3의 배수면 3으로 나눠떨어짐을 안다.
    그래서 각자릿수 합을 chk3에 저장하고 3으로 나눠떨어지는지 확인한다.
    나눠떨어지는 경우 제거는 하지 않고 3으로 나눈다.

    이제 3의 배수가 아닌 경우를 확인하는데 3가지 경우로 나눌 수 있다.
    1만으로 이루어진 경우와 5로만 이뤄진 경우 그리고 1과 5가 함께 있는 경우다.
    5로만 이뤄져있다면 5의 배수이므로 제거하지 않고 5로 나눈다.

    1로만 이루어진 경우 길이를 짝수로 만든다.
    그러면 1111인 경우 11로 나눠떨어짐을 알 수 있다.
    이렇게 11로 나눈다. 주의할 경우는 111에서 1개를 빼서 11이 되는 경우인데
    이는 111이 3의 배수이므로 앞에서 처리가되어 반례가 없다.

    이제 1과 5로 이루어진 경우다.
    이 경우 3으로 나눈 나머지는 0, 1 또는 2이다.
    0은 앞에서 처리했고 1 또는 2만 확인한다.
    1인 경우는 1을 제거하고, 2인 경우는 5를 제거하면 3의 배수가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1679
    {

        static void Main1679(string[] args)
        {

            int MAX = 500_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] arr = new int[MAX];
            int n;
            int chk1, chk3, chk5;

            int q = int.Parse(sr.ReadLine());

            while (q-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                if (chk3 == 0)
                {

                    // 3 배수
                    sw.Write("0 3\n");
                    return;
                }

                if (chk1 == n)
                {

                    // 1로만 이뤄진 경우
                    int ret = n % 2 == 0 ? 0 : 1;
                    sw.Write($"{ret} 11\n");
                }
                else if (chk5 == n)
                    // 5로만 이뤄진 경우
                    sw.Write("0 5\n");
                else
                {

                    // 1, 5가 섞인 경우
                    for (int i = 0; i < n; i++)
                    {

                        if (chk3 != arr[i] % 3) continue;
                        sw.Write($"{i + 1} 3\n");
                        return;
                    }
                }
            }

            void Input()
            {

                n = 0;
                chk3 = 0;
                chk1 = 0;
                chk5 = 0;
                int c;
                while ((c = sr.Read()) != '\n')
                {

                    if (c == '\r') continue;
                    int cur = c - '0';
                    arr[n++] = cur;

                    chk3 += cur;

                    if (cur == 1) chk1++;
                    else chk5++;
                }

                chk3 %= 3;
            }
        }
    }
}
