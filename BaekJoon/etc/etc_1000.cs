using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 4
이름 : 배성훈
내용 : 여우 국수
    문제번호 : 23047번
*/

namespace BaekJoon.etc
{
    internal class etc_1000
    {

        static void Main1000(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;

            // Solve();
            void Solve()
            {

                Input();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = sr.Read();
                }

                if (sr.Read() == '\r') sr.Read();
            }

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
