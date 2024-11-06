using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 곰곰이와 시소
    문제번호 : 26072번

    무게 중심을 찾는 문제다
    오차는 소수점 6자리까지 허용한다기에 
    결과는 다른 자료보다 정확한 연산을 해주는 decimal 자료형을 이용했다
    
    그리고 좌표의 위치는 10만 내외이고 무게도 10만 내외이므로
    10만 * 10만 = 100억 > int.MaxValue 이므로 long 자료형으로 무게와 좌표를 곱한 값을 저장했다
    totalWeight역시 10만개의 값이 들어오므로 마찬가지다

    예제에서 소수점 7자리 이상 표현했기에 오차 6자리까지 허용하므로 7자리까지 출력했다
    출력은 문자열 보간법으로 해결했다
    0.00 << 은 0.00 소수점 2째자리까지 표현해준다
        1인 경우 1.00
    그러니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0106
    {

        static void Main106(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int num = ReadInt(sr);
            int len = ReadInt(sr);

            long[] calc = new long[num];

            for (int i = 0; i < num; i++)
            {

                // 예제 0도 입력되기에 무게 무시안되게
                // 1로 옮긴다
                calc[i] = ReadInt(sr) + 1;
            }

            // sigma pos * weight == dis * totalWeight
            // 인 dis가 무게중심이다!
            long totalWeight = 0;
            for (int i = 0; i < num; i++)
            {

                int chk = ReadInt(sr);
                calc[i] *= chk;
                totalWeight += chk;
            }

            sr.Close();

            decimal dis = 0;
            for (int i = 0; i < num; i++)
            {

                dis += Calc(calc[i], totalWeight);
            }

            // 소수점 7자리까지 표현
            // 1을 옮겼기에 1을 뺀다
            Console.WriteLine($"{dis - 1:0.0000000}");
        }

        static decimal Calc(decimal _target, decimal _div)
        {

            decimal ret = _target / _div;
            return ret;
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
