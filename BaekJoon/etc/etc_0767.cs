using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 21
이름 : 배성훈
내용 : 사탕의 밀도
    문제번호 : 13011번

    수학, 임의 정밀도, 삼분탐색 문제다
    고등학교 1학년 때 배우는 절댓값 함수의 특징을 알면 
    쉽게 풀리는 문제다
    
    출력 형식을 잘못 설정해 2번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0767
    {

        static void Main767(string[] args)
        {

            StreamReader sr;

            decimal[] c, w;
            int n;

            Solve();

            void Solve()
            {

                Input();

                decimal ret = FindRet();
                Console.Write($"{ret:0.0########}");
            }

            decimal FindRet()
            {

                decimal ret = 50_000_000;

                for (int i = 0; i < n; i++)
                {

                    decimal chk = w[i] / c[i];
                    decimal sum = 0;

                    for (int j = 0; j < n; j++)
                    {

                        if (i == j) continue;
                        decimal cur = (chk * c[j]) - w[j];
                        cur = cur < 0 ? -cur : cur;
                        sum += cur;
                    }

                    if (sum < ret)
                    {

                        ret = sum;
                    }
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                c = new decimal[n];
                w = new decimal[n];

                for (int i = 0; i < n; i++)
                {

                    c[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    w[i] = ReadInt();
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
