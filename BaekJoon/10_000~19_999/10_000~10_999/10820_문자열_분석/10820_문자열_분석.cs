using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024.10. 11
이름 : 배성훈
내용 : 문자열 분석
    문제번호 : 10820번

    구현, 문자열 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1046
    {

        static void Main1046(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] ret;

            Solve();
            void Solve()
            {

                Init();
                while (Input()) { GetRet(); }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                sw.Write($"{ret[0]} {ret[1]} {ret[2]} {ret[3]}\n");
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                ret = new int[4];
            }

            bool Input()
            {

                ret[0] = 0;
                ret[1] = 0;
                ret[2] = 0;
                ret[3] = 0;

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == '\n') return false;

                ChkChar(c);
                while ((c = sr.Read()) != '\n')
                {

                    ChkChar(c);
                }

                return true;
            }

            void ChkChar(int _c)
            {

                if (_c == '\r') return;
                if ('a' <= _c && _c <= 'z') ret[0]++;
                else if ('A' <= _c && _c <= 'Z') ret[1]++;
                else if ('0' <= _c && _c <= '9') ret[2]++;
                else if (_c == ' ') ret[3]++;
            }
        }
    }
}
