using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 31
이름 : 배성훈
내용 : 증가 수열
    문제번호 : 30236번

    그리디 알고리즘, 애드 혹 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0853
    {

        static void Main853(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                n = ReadInt();
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    ret++;
                    int cur = ReadInt();

                    if (ret == cur) ret++;
                }

                sw.Write($"{ret}\n");
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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
