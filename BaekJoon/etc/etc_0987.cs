using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 22
이름 : 배성훈
내용 : 조약돌 꺼내기
    문제번호 : 13251번

    수학, 조합론, 확률론 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0987
    {

        static void Main987(string[] args)
        {

            StreamReader sr;

            decimal[] cnt;
            int n, k;
            decimal sum;
            decimal ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (cnt[i] < k) continue;

                    decimal mo = sum;
                    decimal ja = cnt[i];

                    decimal exp = 1;
                    for (int j = 0; j < k; j++)
                    {

                        exp *= ja / mo;
                        ja -= 1;
                        mo -= 1;
                    }

                    ret += exp;
                }

                Console.Write($"{ret:0.0000000000}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sum = 0;

                n = ReadInt();

                cnt = new decimal[n];
                for (int i = 0; i < n; i++)
                {

                    cnt[i] = ReadInt();
                    sum += cnt[i];
                }

                k = ReadInt();

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
