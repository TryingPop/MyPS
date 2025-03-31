using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 31
이름 : 배성훈
내용 : 엘프의 검
    문제번호 : 4436번

    브루트포스 문제다.
    수에 0 ~ 9까지 새롭게 등장한 갯수를 세는 함수를 만들어 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1501
    {

        static void Main1501(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string input;
            bool[] use = new bool[10];
            while (!string.IsNullOrEmpty(input = sr.ReadLine()))
            {

                long n = int.Parse(input);

                Array.Fill(use, false);

                int cnt = Cnt(n);
                int mul = 1;
                while (cnt != 10)
                {

                    mul++;
                    cnt += Cnt(mul * n);
                }

                sw.WriteLine(mul);
            }

            int Cnt(long _val)
            {

                if (_val == 0)
                {

                    if (use[0]) return 0;
                    use[0] = true;
                    return 1;
                }

                int ret = 0;
                while (_val > 0)
                {

                    long cur = _val % 10;
                    _val /= 10;

                    if (use[cur]) continue;
                    ret++;
                    use[cur] = true;
                }

                return ret;
            }
        }
    }
}
