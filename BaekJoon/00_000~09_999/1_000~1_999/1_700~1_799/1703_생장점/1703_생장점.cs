using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 24
이름 : 배성훈
내용 : 생장점
    문제번호 : 1703번

    수학, 구현, 사칙연산 문제다.
    가지가 뻗어나가는걸 계산해 세어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1354
    {

        static void Main1354(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;

            while (Input()) 
            {

                GetRet();
            }

            void GetRet()
            {

                long ret = ReadInt() - ReadInt();
                for (int i = 1; i < n; i++)
                {

                    int mul = ReadInt();
                    int pop = ReadInt();

                    ret = ret * mul - pop;
                }

                sw.WriteLine(ret);
            }

            bool Input()
            {

                n = ReadInt();

                return n != 0;
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
                    if (c == '\n' || c == ' ') return true;
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