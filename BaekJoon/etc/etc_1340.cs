using System;
using System.IO;

/*
날짜 : 2025. 2. 16
이름 : 배성훈
내용 : 인기 투표
    문제번호 : 11637번

    구현 문제다.
    조건대로 구현해 출력하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1340
    {

        static void Main1340(string[] args)
        {

            string NO = "no winner\n";
            string YES1 = "majority winner ";
            string YES2 = "minority winner ";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, sum;
            int[] arr;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();
                while(t-- > 0)
                {

                    Input();

                    GetRet();
                }
            }

            void GetRet()
            {

                int max = 0;
                int cnt = 0;
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (max < arr[i])
                    {

                        max = arr[i];
                        cnt = 1;
                        ret = i + 1;
                    }
                    else if (arr[i] == max) cnt++;
                }

                if (cnt != 1)
                    sw.Write(NO);
                else if (sum / 2 < max)
                {

                    sw.Write(YES1);
                    sw.Write(ret);
                    sw.Write('\n');
                }
                else
                {

                    sw.Write(YES2);
                    sw.Write(ret);
                    sw.Write('\n');
                }
            }

            void Init()
            {

                arr = new int[10];
            }

            void Input()
            {

                n = ReadInt();
                sum = 0;
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    sum += arr[i];
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
