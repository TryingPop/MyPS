using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 숫자 게임
    문제번호 : 2303번

    구현, 브루트포스 문제다
    조건대로 구현하고 모든 경우를 탐색해 풀었다
    방법은 DFS가 아닌 삼중 포문을 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0377
    {

        static void Main377(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[] calc = new int[5];

            int max = 0;
            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 0; j < 5; j++)
                {

                    calc[j] = ReadInt();
                }

                int cur = 0;
                for (int a = 0; a < 3; a++)
                {

                    for (int b = a + 1; b < 4; b++)
                    {

                        for (int c = b + 1; c < 5; c++)
                        {

                            int num = (calc[a] + calc[b] + calc[c]) % 10;
                            cur = cur < num ? num : cur;
                        }
                    }
                }

                if (max <= cur)
                {

                    max = cur;
                    ret = i;
                }
            }

            sr.Close();

            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
