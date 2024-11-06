using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 카우게임
    문제번호 : 15720번

    수학, 구현, 정렬, 그리디, 사칙연산 문제다
    정렬해서 가장 큰 값들을 할인해서 최소값을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0460
    {

        static void Main460(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int b = ReadInt();
            int s = ReadInt();
            int d = ReadInt();

            int ret1 = 0;
            int ret2 = 0;

            int max = b < s ? s : b;
            int min = b < s ? b : s;

            max = d < max ? max : d;
            min = d < min ? d : min;

            int[] arr = new int[max];

            Get(b);
            Get(s);
            Get(d);

            Console.Write($"{ret1}\n{ret2}");

            void Get(int _len)
            {

                for (int i = 0; i < _len; i++)
                {

                    arr[i] = ReadInt();
                    ret1 += arr[i];
                    ret2 += arr[i];
                }

                Array.Sort(arr, 0, _len);

                for (int i = 1; i <= min; i++)
                {

                    ret2 -= arr[_len - i] / 10;
                }
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
