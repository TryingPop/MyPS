using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 23
이름 : 배성훈
내용 : 謎紛芥索紀 (Large)
    문제번호 : 14731번

    수학, 미적분학, 분할 정복을 이용한 거듭제곱
    b = 0인 경우와 오버플로우로 문제로 4번 틀렸다
    그리고 입력 자체에 문제가 있다; 즉, 57% 쯤에서 탭으로 간격 띄운게 있다!
*/

namespace BaekJoon.etc
{
    internal class etc_0721
    {

        static void Main721(string[] args)
        {

            long MOD = 1_000_000_007L;
            StreamReader sr;

            long ret = 0;
            int n;

            Solve();

            void Solve()
            {

                Input();
                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (b == 0) continue;

                    long val = GetPow(b - 1);
                    ret = (ret + ((val * f) % MOD * b) % MOD) % MOD;
                }

                sr.Close();
            }

            long GetPow(int _exp)
            {

                long ret = 1;
                long a = 2;
                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * a) % MOD;

                    a = (a * a) % MOD;
                    _exp /= 2;
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n' && c != '\t')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
