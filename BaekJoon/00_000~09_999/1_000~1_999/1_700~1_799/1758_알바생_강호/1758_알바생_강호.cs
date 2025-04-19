using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 17
이름 : 배성훈
내용 : 알바생 강호
    문제번호 : 1758번

    그리디 문제다.
    2원짜리 2명이 있다고 보자.
    그러면 3원을 받는다.

    만약 여기에 1원짜리 한명을 추가하는 경우 1원짜리 사람이 이득을 보게 받는다면
    1일차에 와야 하고 1원을 받는다.
    2일차는 2원짜리가 오고 1원을 받는다.
    3일차는 0원을 받는다.

    총 2원을 받아 앞보다 작다.
    대소 관계의 추이성(그리디)으로 작은 사람을 끼워넣는 것은 가격이 떨어짐을 알 수 있다.
    그래서 최대 사람부터 가득 채워넣어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1551
    {

        static void Main1551(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            Array.Sort(arr, (x, y) => y.CompareTo(x));

            long ret = 0;

            for (int i = 0; i < n; i++)
            {

                int add = arr[i] - i;
                if (add <= 0) break;
                ret += add;
            }

            Console.Write(ret);

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
