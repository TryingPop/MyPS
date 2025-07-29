using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 29
이름 : 배성훈
내용 : 그래픽스 퀴즈
    문제번호 : 2876번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1792
    {

        static void Main1792(string[] args)
        {

            int MAX = 5;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int[] cnt = new int[MAX + 1];
            bool[] chk = new bool[MAX + 1];

            int n = ReadInt();
            int ret1 = 0, ret2 = 0;

            for (int i = 0; i < n; i++)
            {

                chk[ReadInt()] = true;
                chk[ReadInt()] = true;

                for (int j = 1; j <= MAX; j++)
                {

                    if (chk[j])
                    {

                        chk[j] = false;
                        cnt[j]++;

                        if (ret1 < cnt[j])
                        {

                            ret1 = cnt[j];
                            ret2 = j;
                        }
                        else if (ret1 == cnt[j] && j < ret2)
                            ret2 = j;
                    }
                    else
                        cnt[j] = 0;
                }
            }

            Console.Write($"{ret1} {ret2}");

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }

        }
    }

#if other
namespace no2876try1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine());
            int[,] data = new int[length, 2];
            int[,] dp = new int[length, 5];
            int max = -1;
            int answerGrade = -1;

            for (int index = 0; index < length; ++index)
            {
                string line = Console.ReadLine();
                data[index, 0] = line[0] - '0';
                data[index, 1] = line[2] - '0';
            }
            for (int grade = 1; grade <= 5; ++grade)
            {
                for (int index = 0; index < length; ++index)
                {
                    if (data[index, 0] == grade || data[index, 1] == grade)
                    {
                        dp[index, grade - 1] = (index == 0) ? 1 : dp[index - 1, grade - 1] + 1;

                        if (max < dp[index, grade - 1])
                        {
                            max = dp[index, grade - 1];
                            answerGrade = grade;
                        }
                    }
                }
            }
            Console.WriteLine($"{max} {answerGrade}");
        }
    }
}
#endif
}
