using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 20
이름 : 배성훈
내용 : 기초 나머지 계산
    문제번호 : 4328번

    수학, 사칙연산 문제다.
    m, p를 10진법으로 바꾼다.
    그리고 나머지를 찾은 뒤 b진법으로 바꿔 풀었다.
    b진법으로 나눠 출력할 때는 끝에서부터 채워지므로 
    스택을 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1351
    {

        static void Main1351(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            string[] nums = null;
            long b, m;
            Stack<long> ret = new(10);

            Solve();
            void Solve()
            {

                b = 0;
                m = 0;
                while (Input())
                {

                    GetRet();
                }
            }

            void GetRet()
            {

                long r = 0;
                for (int i = 0; i < nums[1].Length; i++)
                {

                    r = r * b + nums[1][i] - '0';
                    r %= m;
                }

                while (r > 0)
                {

                    ret.Push(r % b);
                    r /= b;
                }

                if (ret.Count == 0) sw.Write("0\n");
                else
                {

                    while (ret.Count > 0)
                    {

                        sw.Write(ret.Pop());
                    }

                    sw.Write('\n');
                }
            }

            bool Input()
            {

                string input = sr.ReadLine();

                if (input[0] == '0') return false;
                nums = input.Split();
                b = long.Parse(nums[0]);

                m = 0;
                for (int i = 0; i < nums[2].Length; i++)
                {

                    m = m * b + nums[2][i] - '0';
                }

                return true;
            }
        }
    }
}
