using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 
이름 : 배성훈
내용 : 배
    문제번호 : 1092번
*/

namespace BaekJoon.etc
{
    internal class etc_1343
    {

        static void Main1343(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n, m;
            int[] arr1, arr2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr1);
                Array.Sort(arr2);

                if (arr1[n - 1] < arr2[m - 1])
                {

                    Console.Write(-1);
                    return;
                }

                int ret = m;
                int cur = 0;

                for (int i = 0; i < m; i++) 
                {

                    
                }
            }

            void Input()
            {

                n = ReadInt();
                arr1 = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr1[i] = ReadInt();
                }

                m = ReadInt();
                arr2 = new int[m];
                for (int i = 0;i < m; i++)
                {

                    arr2[i] = ReadInt();
                }
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
                    if (c == ' ' || c == '\n') return true;
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
