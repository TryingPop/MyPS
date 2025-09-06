using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 26
이름 : 배성훈
내용 : 플립과 시프트
    문제번호 : 7347번

    홀짝성, 애드 혹 문제다.
    2칸씩 서로 교환이 가능하다.
    만약 홀수인 경우 모든 칸은 이동할 수 있어
    항상 가능하고

    짝수인 경우 홀수 또는 짝수칸만 이동할 수 있어
    홀수에 검은 것의 개수와 짝수에 검은 것의 개수 차이가 1이하인 경우에만 가능하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1840
    {

        static void Main1840(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string Y = "YES\n";
            string N = "NO\n";

            int[] arr = new int[30];
            int q = ReadInt();

            while (q-- > 0)
            {

                int n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                if ((n & 1) == 1) sw.Write(Y);
                else
                {

                    int o = 0;
                    int e = 0;

                    for (int i = 0; i < n; i += 2)
                    {

                        e += arr[i];
                        o += arr[i + 1];
                    }

                    if (Math.Abs(e - o) <= 1) sw.Write(Y);
                    else sw.Write(N);
                }
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
