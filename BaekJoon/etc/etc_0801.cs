using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6
이름 : 배성훈
내용 : 점호
    문제번호 : 31747번

    구현, 시뮬레이션 문제다
    슬라이딩 윈도우 기법을 써서 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0801
    {

        static void Main801(string[] args)
        {

            StreamReader sr;
            int n, k;
            int[] arr, front;

            Solve();
            void Solve()
            {

                Input();

                Console.Write(GetRet());

            }

            int GetRet()
            {

                for (int i = 0; i <k;i++)
                {

                    front[arr[i]]++;
                }

                int ret = 0;
                int idx = k;
                while (front[1] != 0 || front[2] != 0)
                {

                    int add = Chk();

                    for (int i = 0; i < add && idx < n; i++, idx++)
                    {

                        front[arr[idx]]++;
                    }

                    ret++;
                }

                return ret;
            }

            int Chk()
            {

                if (front[1] > 0 && front[2] > 0)
                {

                    front[1]--;
                    front[2]--;
                    return 2;
                }

                if (front[1] > 0) front[1]--;
                else front[2]--;

                return 1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                n = ReadInt();
                k = ReadInt();

                arr = new int[n];
                front = new int[3];

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
