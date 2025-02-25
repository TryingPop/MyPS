using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 22
이름 : 배성훈
내용 : 배
    문제번호 : 
*/

namespace BaekJoon.etc
{
    internal class etc_1289
    {

        static void Main1289(string[] args)
        {

            int n, m;
            int[] robot, box;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {


            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                robot = new int[n];
                Array.Sort(robot, (x, y) => y.CompareTo(x));

                for (int i = 0; i < n; i++)
                {

                    robot[i] = ReadInt();
                }

                m = ReadInt();
                box = new int[m];
                for (int i = 0; i < m; i++)
                {

                    box[i] = ReadInt();
                }

                Array.Sort(box, (x, y) => y.CompareTo(x));

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
