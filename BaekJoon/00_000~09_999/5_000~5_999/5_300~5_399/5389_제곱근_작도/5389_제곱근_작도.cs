using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 2
이름 : 배성훈
내용 : 제곱근 작도
    문제번호 : 5389번

    수학, 브루트포스 알고리즘, 정수론 문제다.
    각 a에 대해, a^2 + input = b^2인 b가 존재하는지 확인해야 한다.
    단순히 적당한 수 a에 대해 확인하는 경우 b를 찾는 경우
    a의 범위가 매우 크다. 

    일례로 (a + 1)^2 - a^2 = 2 * a + 1 의 경우 홀수의 경우 항상 존재한다!
    그리고 입력은 10억까지 들어오므로, 5억까지의 제곱 수를 확인해야 한다.

    그래서 다른 방법을 찾아야 한다.
    다른 방법으로 a^2 - b^2 = (a + b)(a - b)로 인수분해됨을 이용할 수 있다.
    그래서 sqrt(n)까지 조사하면서 인수분해되는지 확인한다.
    그리고 a, b를 찾아 작은 값이 가장 작은지 확인해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1372
    {

        static void Main1372(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            string IMPO = "IMPOSSIBLE\n";
            for (int i = 0; i < n; i++)
            {

                int input = int.Parse(sr.ReadLine());

                int half = input >> 1;

                bool flag = false;
                int b = -1, a = -1;
                for (int sub = 1; sub * sub <= input; sub++)
                {

                    if (input % sub != 0) continue;
                    int add = input / sub;

                    int A = (add + sub) >> 1;
                    int B = (add - sub) >> 1;

                    if (A + B != add || A - B != sub) continue;
                    flag = true;
                    if (a == -1 || B < a)
                    {

                        b = A;
                        a = B;
                    }
                }

                if (flag)
                    sw.Write($"{a} {b}\n");
                else
                    sw.Write(IMPO);
            }
        }
    }
}
