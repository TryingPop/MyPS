using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4 . 26
이름 : 배성훈
내용 : 삭삽 정렬
    문제번호 : 13884번

    그리디, 정렬 문제다.
    가장 작은 원소는 삭제해서는 안된다.
    가장 작은 원소 뒤에 그다음으로 가장 작은 원소가 있는지 확인한다.
    이렇게 만들어지는 수열의 길이가 살리는 것의 최댓값이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1581
    {

        static void Main1581(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();
            int[] arr = new int[1_000];
            int[] sorted = new int[1_000];

            while (t-- > 0)
            {

                int k = ReadInt();
                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int val = ReadInt();

                    arr[i] = val;
                    sorted[i] = val;
                }

                Array.Sort(sorted, 0, len);

                int idx = 0;
                for (int i = 0; i < len; i++)
                {

                    if (sorted[idx] == arr[i]) idx++;
                }

                sw.Write($"{k} {len - idx}\n");
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
