using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 24
이름 : 배성훈
내용 : 집합
    문제번호 : 11723번

    비트 마스크를 이용하는 문제이다
    비트 마스크는 검색해보니 정수 int를 비트단위로 기록해서 저장하는 방법이다

    포폴에서 사용했었던 방법이다 다만, 나중에 코드 읽기가 힘들어져 메모리는 좀 더 먹지만 읽기 쉬운 코드로 수정 했다;

    장점은 수행 시간이 빠르고, 코드가 짧다, 메모리 사용량이 적다 정도로 보인다
*/

namespace BaekJoon._41
{
    internal class _41_01
    {

        static void Main1(string[] args)
        {

            const string a = "add";
            const string r = "remove";
            const string c = "check";
            const string t = "toggle";
            const string all = "all";
            const string e = "empty";

            // dp부분을 비트마스크로 표현한다!
            // 0이 32비트 모두 0인 값!
            int dp = 0;
            // bool[] dp = new bool[21];
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int len = int.Parse(sr.ReadLine());

            for (int i = 0; i < len; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');
                if (temp[0] == a)
                {

                    int n = int.Parse(temp[1]);

                    // dp[n] = true;
                    // 기록
                    dp |= 1 << n;
                }
                else if (temp[0] == r)
                {

                    int n = int.Parse(temp[1]);
                    // 제거
                    // dp[n] = false;
                    dp &= ~(1 << n);
                }
                else if (temp[0] == c)
                {

                    int n = int.Parse(temp[1]);

                    // 있으면 1, 없으면 0
                    // if (dp[n]) sw.WriteLine(1);
                    // else sw.WriteLine(0);

                    // 있으면 1 없으면 0 확인이다!
                    if ((1 << n & dp) == 0) sw.WriteLine(0);
                    else sw.WriteLine(1);
                }
                else if (temp[0] == t)
                {

                    int n = int.Parse(temp[1]);

                    // dp[n] = !dp[n];

                    // 덧셈으로 해도 된다
                    // if ((1 << n & dp) == 0) dp += 1 << n;
                    // else dp -= 1 << n;

                    dp ^= 1 << n;
                }
                else if (temp[0] == e)
                {

                    // 모두 0으로
                    dp = 0;
                }
                else
                {

                    // 모두 1로!
                    dp = ~0;
                } 

            }

            sr.Close();
            sw.Close();

        }
    }
}
