using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 12
이름 : 배성훈
내용 : 수 정렬하기 4
    문제번호 : 11931번

    정렬 문제다.
    서로 다른 수만 주어지고 절댓값이 100만 범위 이내이다.
    그리고 입력되는 숫자의 갯수는 100만개까지 온다.

    처음에는 다른 수 조건을 못봐서 배열로 정렬하는 방법을 이용했으나,
    이후에는 다른 수 조건을 이용하면 bool 배열로 숫자 유무만 판별하고 큰 수부터 읽어 풀었다.
    뒤의 경우 정렬 연산을 하지 않고 bool 배열로 만들기에 저장하는 배열의 크기도 작다.
*/

namespace BaekJoon.etc
{
    internal class etc_1971
    {

        static void Main1971(string[] args)
        {

            int OFFSET = 1_000_000;
            int n;
            bool[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = arr.Length - 1; i >= 0; i--)
                {

                    if (arr[i]) sw.Write($"{i - OFFSET}\n");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new bool[OFFSET * 2 + 1];
                for (int i = 0; i < n; i++)
                {

                    arr[ReadInt() + OFFSET] = true;
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
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }
}
