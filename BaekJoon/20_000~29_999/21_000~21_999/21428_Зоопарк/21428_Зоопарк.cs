using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 12
이름 : 배성훈
내용 : Зоопарк
    문제번호 : 21428번

    수학, 조합론 문제다.
    순서도로 경우를 나열하고
    추가되는 경우를 확인했다.

    다만 3 이하인 경우를 고려안해 1번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1626
    {

        static void Main1626(string[] args)
        {

            int n;
            int[] arr;
            int sum;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 0;
                if (n >= 3)
                {

                    long sum = arr[n - 2] + arr[n - 1];
                    long prev = arr[n - 2] * arr[n - 1];
                    for (int i = n - 3; i >= 0; i--)
                    {

                        ret += arr[i] * prev;
                        prev += arr[i] * sum;
                        sum += arr[i];
                    }
                }


                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                sum = 0;

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    sum += arr[i];
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
