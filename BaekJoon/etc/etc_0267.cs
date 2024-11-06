using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 민트 초코
    문제번호 : 20302번

    수학, 정수론, 소수판정, 에라토스테네스의 체 이론 문제다

    처음에 0이 들어오는 경우 처리를 제대로 못했다
    그래서 93%에서 계속해서 틀렸다
    
    문제 아이디어 자체는 쉽게 보였다
    결과의 소인수들로 봐야한다

    0이 곱해지는 경우가 없을 때,
    여기서 음인 소수가 하나라도 존재하면 정수가 아닌 유리수가 되고
    소수가 모두 음이 아닌 정수인 경우 정수가된다

    0이 곱해지는 경우면 항상 정수다(0으로 나누는 경우가 없기 때문이다)
    이를 코드로 나타내면 이상없이 풀린다
    처음 0이 제대로 처리되는 줄알아 버퍼가 끊겼나 하면서 이상한 곳을 건드리고 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_0267
    {

        static void Main267(string[] args)
        {

            string YES = "mint chocolate";
            string NO = "toothpaste";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int[] div = new int[100_001];

            {

                int first = ReadInt(sr);
                if (first == 0)
                {

                    Console.WriteLine(YES);
                    return;
                }
                GetDiv(div, first, true);
            }

            for (int i = 1; i < n; i++)
            {

                bool isPlus = GetOper(sr);
                int num = ReadInt(sr);

                if (num == 0)
                {

                    Console.WriteLine(YES);
                    return;
                }

                GetDiv(div, num, isPlus);
            }

            sr.Close();

            bool ret = true;
            for (int i = 1; i < div.Length; i++)
            {

                if (div[i] < 0)
                {

                    ret = false;
                    break;
                }
            }

            Console.WriteLine(ret ? YES : NO);
        }

        static void GetDiv(int[] _div, int _n, bool _isPlus)
        {

            int add = _isPlus ? 1 : -1;
            for (int i = 2; i < _n; i++)
            {

                if (i * i > _n) break;
                if (_n % i > 0) continue;

                int calc = 0;

                while (_n % i == 0)
                {

                    _n /= i;
                    calc += add;
                }

                _div[i] += calc;
            }

            if (_n > 1)
            {

                _div[_n] += add;
            }
        }

        static bool GetOper(StreamReader _sr)
        {

            int c;
            bool ret = false;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = c == '*';
            }

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
