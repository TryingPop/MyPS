using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 24
이름 : 배성훈
내용 : 트로피 진열
    문제번호 : 1668번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0833
    {

        static void Main833(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }
            
            void GetRet()
            {

                int ret = 0;
                int cur = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] <= cur) continue;

                    cur = arr[i];
                    ret++;
                }

                Console.Write($"{ret}\n");
                ret = 0;
                cur = 0;

                for (int i = n - 1; i >= 0; i--)
                {

                    if (arr[i] <= cur) continue;

                    cur = arr[i];
                    ret++;
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++) 
                { 
                    
                    arr[i] = ReadInt(); 
                }

                sr.Close();
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
