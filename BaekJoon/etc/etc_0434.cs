using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 조화평균
    문제번호 : 2090번

    수학, 구현, 정수론, 유클리드 호제법 문제다
    arr[i]의 크기가 100 이하인데
    9개가 들어오면 분모는 10^18승까지간다
    그래서 long으로 자료형을 설정했다

    이후에는 조화평균을 GCD로 조건대로 구현하니
    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0434
    {

        static void Main434(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }
            sr.Close();

            long curD = arr[0];
            long curU = 1;
            for (int i = 1; i < n; i++)
            {

                long nextD = arr[i];
                long nextU = 1;

                long gcdD = GetGCD(curD, nextD);

                curU = (nextD / gcdD) * curU + (curD / gcdD);
                curD = curD * nextD / gcdD;
            }

            long gcd = GetGCD(curU, curD);
            Console.WriteLine($"{curD / gcd}/{curU / gcd}");

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

            long GetGCD(long _a, long _b)
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
        }
    }
}
