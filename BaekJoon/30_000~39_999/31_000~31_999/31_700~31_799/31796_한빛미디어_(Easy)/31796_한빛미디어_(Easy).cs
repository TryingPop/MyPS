using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 22
이름 : 배성훈
내용 : 한빛미디어 (Easy)
    문제번호 : 31796번

    그리디, 정렬 문제다.
    가장 싼거부터 채워넣으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1783
    {

        static void Main1783(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int p = 0;
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    if (arr[i] < p) continue;
                    ret++;
                    p = arr[i] << 1;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr);
                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
