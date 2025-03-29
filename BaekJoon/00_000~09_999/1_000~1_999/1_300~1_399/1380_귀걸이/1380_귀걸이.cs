using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 29
이름 : 배성훈
내용 : 귀걸이
    문제번호 : 1380번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1493
    {

        static void Main1493(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            string[] name = new string[101];
            bool[] use = new bool[101];
            int num = 1;

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                int len = 2 * n - 1;
                for (int i = 0; i < len; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    int idx = int.Parse(temp[0]);
                    if (use[idx]) use[idx] = false;
                    else use[idx] = true;
                }

                for (int i = 1; i <= n; i++)
                {

                    if (use[i])
                    {

                        use[i] = false;
                        sw.Write($"{num++} {name[i]}\n");
                    }
                }
            }

            bool Input()
            {

                n = int.Parse(sr.ReadLine());

                for (int i = 1; i <= n; i++)
                {

                    name[i] = sr.ReadLine();
                }
                return n != 0;
            }
        }
    }
}
