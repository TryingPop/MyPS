using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : 웰컴 키트
    문제번호 : 30802번

    수학, 구현, 사칙연산 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1051
    {

        static void Main1051(string[] args)
        {

            int[] size;
            int t, p;
            int n;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet() 
            {

                int ret1 = 0;
                for (int i = 0; i < 6; i++)
                {

                    if (size[i] == 0) continue;
                    size[i]--;
                    ret1 += 1 + size[i] / t;
                }

                int ret2 = n / p;
                int ret3 = n % p;

                Console.Write($"{ret1}\n{ret2} {ret3}");
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = new int[6];
                n = ReadInt();

                for (int i = 0; i < 6; i++)
                {

                    size[i] = ReadInt();
                }

                t = ReadInt();
                p = ReadInt();

                sr.Close();

                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
