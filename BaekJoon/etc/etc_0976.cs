using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 19
이름 : 배성훈
내용 : Timovi
    문제번호 : 15578번

    수학, 구현 문제다
    오버플로우로인한 출력초과로 여러 번 틀렸다
    아이디어는 단순하다

    A 방향을 1 -> n - 1까지 올라가는거라 생각하고
    B 방향을 n -> 2까지 내려가는거라 생각한다
    그러면 A와 B로 번갈아 이동한다

    A, B로 각각 이동할 때마다 전체 k * (n - 1)명을 각 팀에 배치한다
    A, B로 배치하면서 남은 인원이 k * (n - 1)이 되는 경우
    이제 일일히 배치하는 식으로 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0976
    {

        static void Main976(string[] args)
        {

            long n, k, m;
            long[] arr;
            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput());

                sw.Write(arr[0]);
                for (int i = 1; i < n; i++)
                {

                    sw.Write(' ');
                    sw.Write(arr[i]);
                }

                sw.Close();
            }

            void GetRet()
            {

                long chk = k * (n - 1);

                long b = (m / chk) >> 1;
                long f = (m / chk) - b;

                m %= chk;
                arr[0] = f * k;
                for (int i = 1; i < n - 1; i++)
                {

                    arr[i] = (f + b) * k;
                }

                arr[n - 1] = b * k;

                if (b < f)
                {

                    for (long i = n - 1; i >= 0; i--)
                    {

                        if (m == 0) break;
                        long add = m < k ? m : k;
                        m -= add;

                        arr[i] += add;
                    }
                }
                else
                {

                    for (long i = 0; i < n; i++)
                    {

                        if (m == 0) break;
                        long add = m < k ? m : k;
                        m -= add;

                        arr[i] += add;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();

                n = long.Parse(temp[0]);
                k = long.Parse(temp[1]);
                m = long.Parse(temp[2]);

                arr = new long[n];
            }
        }
    }
}
