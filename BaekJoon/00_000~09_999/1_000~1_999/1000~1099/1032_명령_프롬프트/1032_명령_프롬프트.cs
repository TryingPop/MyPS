using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 17
이름 : 배성훈
내용 : 명령 프롬프트
    문제번호 : 1032번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1280
    {

        static void Main1280(string[] args)
        {

            int n;
            string[] str;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                char[] ret = new char[str[0].Length];
                for (int i = 0; i < str[0].Length; i++)
                {

                    ret[i] = str[0][i];
                }

                for (int i = 0; i < str[0].Length; i++)
                {

                    for (int j = 1; j < n; j++)
                    {

                        if (str[0][i] == str[j][i]) continue;
                        ret[i] = '?';
                        break;
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < ret.Length; i++)
                {

                    sw.Write(ret[i]);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                str = new string[n];
                for (int i = 0; i < n; i++)
                {

                    str[i] = sr.ReadLine();
                }
            }
        }
    }
}
