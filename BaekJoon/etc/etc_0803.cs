using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 10
이름 : 배성훈
내용 : Rust Study
    문제번호 : 30033번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0803
    {

        static void Main803(string[] args)
        {

            StreamReader sr;
            int[] arr;
            int n;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (ReadInt() < arr[i]) continue;
                    ret++;
                }

                sr.Close();
                Console.Write(ret);
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
