using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 23
이름 : 배성훈
내용 : 謎紛芥索紀 (Small)
    문제번호 : 14730번

    수학, 미적분학 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0720
    {

        static void Main720(string[] args)
        {

            StreamReader sr;
            int n;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    ret += f * b;
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == -1) return 0;

                bool plus = c != '-';
                
                int ret = plus ? c - '0' : 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
