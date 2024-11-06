using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : 직각삼각형
    문제번호 : 4153번

    수학, 기하학, 피타고라스 정리 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1050
    {

        static void Main1050(string[] args)
        {

            string YES = "right\n";
            string NO = "wrong\n";

            StreamReader sr;
            StreamWriter sw;
            int a, b, c;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    sw.Write(GetRet() ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            bool GetRet()
            {

                int aa = a * a;
                int bb = b * b;
                int cc = c * c;
                return aa == bb + cc || bb == cc + aa || cc == aa + bb;
            }

            bool Input()
            {

                a = ReadInt();
                b = ReadInt();
                c = ReadInt();

                return a != 0 || b != 0 || c != 0;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == -1 || c == ' ' || c == '\n') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
