using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 점수 계산
    문제번호 : 25710번

    브루트포스, 비둘기집 원리를 써서 푸는 문제다
    힌트 보고 풀 수 있었다

    주된 아이디어는 다음과 같다
    있는 원소 중 2개(조합)를 뽑아 연산하기에 많아야 50만번 연산이 필요하다
    그래서 개수를 세고, 있은 원소만 풀면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0329
    {

        static void Main329(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] nums = new int[1_000];

            int n = ReadInt(sr);
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt(sr);
                nums[cur]++;
            }

            sr.Close();

            int ret = 0;
            for (int i = 1; i < 1_000; i++)
            {

                if (nums[i] == 0) continue;
                for (int j = i; j < 1_000; j++)
                {

                    if ((i == j && nums[j] < 2) || (i != j && nums[j] == 0)) continue;
                    int mul = i * j;
                    int calc = 0;
                    while (mul > 0)
                    {

                        calc += mul % 10;
                        mul /= 10;
                    }

                    if (ret < calc) ret = calc;
                }
            }


            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
