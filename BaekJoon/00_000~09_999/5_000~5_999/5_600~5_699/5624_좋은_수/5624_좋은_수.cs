using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 28
이름 : 배성훈
내용 : 좋은 수
    문제번호 : 5624번

    중간에서 만나기 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1738
    {

        static void Main1738(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int OFFSET = 200_000;
                bool[] twoSum = new bool[400_000 + 1];
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        int idx = arr[i] - arr[j];
                        if (twoSum[idx + OFFSET]) 
                        {

                            ret++;
                            break;
                        }
                    }

                    for (int j = 0; j <= i; j++)
                    {

                        int sum = arr[i] + arr[j];
                        twoSum[sum + OFFSET] = true;
                    }
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
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }
}
