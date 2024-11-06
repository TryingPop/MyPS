using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 7 
이름 : 배성훈
내용 : 숫자 놀이
    문제번호 : 2777번

    해당 자리에 올 수 있는 수들의 개수를 찾았다
    십진법이므로 9, 8, ... 2, 1까지 가능하다
    그런데 1은 곱셈에서 항등원이므로 없는게 가장 짧다
        1 * x = x * 1 = x인 1을 연산 * 의 항등원

    처음에는 2, 3, 5, 7만 셀까 하다가
    2, 3의 경우를 분할해줘야하기에 복잡한 코드가 나올거 같아
    그냥 9, 8, ... 2까지 세는 방법을 이용했다
    해당 경우도 짧은 경우가 보장된다

    그리고 만약 11이상의 소수를 약수로 포함하고 있다면,
    십진법으로는 표현이 불가능하다 그래서 -1

    또한 1인 경우 해당 방법으로 카운팅이 안된다!
    그래서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0162
    {

        static void Main162(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = ReadInt(sr);

            int[] cnt = new int[10];

            while (test-- > 0)
            {

                int n = ReadInt(sr);
                if (n == 1)
                {

                    sw.WriteLine(1);
                    continue;
                }

                for (int i = 9; i >= 2; i--)
                {

                    // 약수 개수 세기
                    while (true)
                    {

                        if (n % i != 0) break;
                        cnt[i]++;
                        n /= i;
                    }
                }

                int ret = -1;
                if (n == 1)
                {

                    ret = 0;
                    for (int i = 0; i < 10; i++)
                    {

                        ret += cnt[i];
                    }
                }

                sw.WriteLine(ret);

                for (int i = 2; i < 10; i++)
                {

                    cnt[i] = 0;
                }
            }
            sw.Close();
            sr.Close();
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
