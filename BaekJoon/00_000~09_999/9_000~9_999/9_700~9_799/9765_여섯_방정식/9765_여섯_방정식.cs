using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 여섯 방정식
    문제번호 : 9765번

    출력 형식을 잘못 맞춰 한 번 틀렸다
    처음에는 에라토스 테네스의 체이론으로 접근했지만,
    조금 더 고민해보니 c1 != c5, c3 != c6 조건에의해
    GCD로 접근하는게 더 빨라 보였다

    그래서 GCD로 접근해서 풀었다
    xi는 2000만 이하의 수이므로 에라토스 테네스의 체 이론을 써도 무방하다!
*/

namespace BaekJoon.etc
{
    internal class etc_0191
    {

        static void Main191(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            // 입력 범위 (20_000_000)^2 미만!
            long[] inputs = new long[6];
            for (int i = 0; i < 6; i++)
            {

                inputs[i] = ReadLong(sr);
            }

            // long[] inputs = sr.ReadLine().Split(' ').Select(long.Parse).ToArray();
            sr.Close();

            long[] ret = new long[8];


            ret[1] = GCD(inputs[0], inputs[4]);
            ret[0] = inputs[0] / ret[1];
            ret[2] = inputs[4] / ret[1];

            // ret[3] = inputs[3] * ret[0];

            ret[5] = GCD(inputs[2], inputs[5]);
            ret[6] = inputs[2] / ret[5];
            ret[4] = inputs[5] / ret[5];

            // ret[7] = inputs[1] * ret[6];

            for (int i = 0; i < 7; i++)
            {

                if (i == 3) continue;
                Console.Write(ret[i]);
                Console.Write(' ');
            }
        }

        static long GCD(long _a, long _b)
        {

            if (_a < _b)
            {

                long temp = _a;
                _a = _b;
                _b = temp;
            }

            while(_b > 0)
            {

                long temp = _a % _b;
                _a = _b;
                _b = temp;
            }

            return _a;
        }

        static long ReadLong(StreamReader _sr)
        {

            int c;
            long ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
