using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 19
이름 : 배성훈
내용 : 시험 감독
    문제번호 : 13458번

    수학, 사칙연산 문제다
    소수점 내림연산을 올림 연산으로 변형하면 된다
    예를들어 6 / 7 == 1, 7 / 7 = 1, 1 / 7 = 1이다
    이는 분자에 1을 빼고 내림 연산을 한 뒤 전체에 1을 더해주면 올림과 같은 기능을 한다
    6 / 7 => 1 + (5 / 7) = 1
    7 / 7 => 1 + (6 / 7) = 1
    1 / 7 => 1 + (0 / 7) = 1
*/

namespace BaekJoon.etc
{
    internal class etc_0761
    {

        static void Main761(string[] args)
        {

            StreamReader sr;

            int n, b, c;
            int[] arr;

            Solve();

            void Solve()
            {

                Input();

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    ret++;
                    arr[i] -= b + 1;
                    if (arr[i] < 0) continue;
                    ret += 1 + arr[i] / c;
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                b = ReadInt();
                c = ReadInt();
            }

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
        }
    }
}
