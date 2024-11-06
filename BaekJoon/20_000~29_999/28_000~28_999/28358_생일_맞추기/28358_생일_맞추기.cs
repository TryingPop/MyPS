using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 생일 맞추기
    문제번호 : 28358번

    숫자만으로 답을 추론하는 계산식이 있을거 같으나, 복잡한 식일거 같고 당장 안떠올랐다

    최대 케이스 1234 
    윤년 1년 날짜 366 
    1234 * 366 < 50만 이므로 
    가능한 날짜를 카운팅하는 코드로 했다

    방법은 간단하다
    month별로 day를 센다
    윤년이므로 코드가 쉽게 짜여졌다
*/

namespace BaekJoon.etc
{
    internal class etc_0022
    {

        static void Main22(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = ReadInt(sr);

            bool[] nums = new bool[10];
            // 각 월에 따른 마지막 날짜!
            // 1월은 31일, 의미로 사용!
            int[] maxDays = new int[13] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            while(test-- > 0)
            {

                for(int i = 0; i < 10; i++)
                {

                    nums[i] = ReadInt(sr) == 0;
                }

                int result = 0;
                for (int month = 1; month <= 12; month++)
                {

                    if (month < 10)
                    {

                        // 1, 2, ... ,9월까지는 해당 값 사용이 가능해야한다!
                        // 안되면 해당 월은 안센다
                        if (!nums[month]) continue;
                    }
                    else
                    {

                        // 10, 11, 12인 경우다
                        // 여기는 1이 무조건 포함되어야한다!
                        if (!nums[1]) continue;
                        // 이제 0, 1, 2 검사를 한다
                        int chk = month - 10;
                        if (!nums[chk]) continue;
                    }

                    // 1 ~ 9일 검사
                    for (int day = 1; day < 10; day++)
                    {

                        if (nums[day]) result++;
                    }

                    // 10 ~ 19일 검사
                    if (nums[1])
                    {

                        // 먼저 1이 사용 가능한지 조사하고
                        // 일의 자리 날짜를 센다!
                        for (int day = 0; day < 10; day++)
                        {

                            if (nums[day]) result++;
                        }
                    }

                    // 20 ~ 29일 검사
                    // 윤년이므로 모두 29일이 보장된다!
                    if (nums[2])
                    {

                        for (int day = 0; day < 10; day++)
                        {

                            if (nums[day]) result++;
                        }
                    }

                    // 30 ~ 31일 검사
                    if (nums[3])
                    {

                        // 3월인 경우 30일까지만 센다,
                        // 2월인 경우 29일까지 세는데, for문 실행 순서 상 바로 탈출한다!
                        for (int day = 30; day <= maxDays[month]; day++)
                        {

                            if (nums[day - 30]) result++;
                        }
                    }
                }

                sw.WriteLine(result);
            }

            sw.Close();
            sr.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }

}
