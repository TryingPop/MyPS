using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 22
이름 : 배성훈
내용 : 데이터 스트림의 섬
    문제번호 : 10432번

    브루트포스 문제다.
    시작과 끝의 값보다 큰 서로 다른 구간의 개수를 찾아주면 된다.
    그래서 값들을 정렬해서 작은 값부터 값이 같은 곳을 시작지점으로 해서, 
    해당 값보다 작아지는 구간의개수를 세어줬다.
    배열의 길이가 12로, 12 x 12로 모두 찾아진다.
*/

namespace BaekJoon.etc
{
    internal class etc_1912
    {

        static void Main1912(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int q = ReadInt();
            int t;
            int[] arr = new int[12];
            int[] val = new int[12];
            while(q-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(val);
                int prev = 0;

                int ret = 0;
                for (int i = 0; i < 12; i++)
                {

                    if (val[i] == prev) continue;
                    prev = val[i];

                    bool flag = false;
                    for (int j = 0; j < 12; j++)
                    {

                        if (arr[j] == prev) flag = true;
                        else if (arr[j] > prev) continue;
                        else
                        {

                            if (flag) ret++;
                            flag = false;
                        }
                    }
                }

                sw.Write($"{t} {ret}\n");
            }

            void Input()
            {

                t = ReadInt();
                for (int i = 0; i < 12; i++)
                {

                    arr[i] = ReadInt();
                    val[i] = arr[i];
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
                    if (c < '0' || '9' < c) return true;

                    ret = c - '0';
                    while ('0' <= (c = sr.Read()) && c <= '9')
                    {

                        ret = ret * 10 + c - '0';
                    }
                    return false;
                }
            }
        }
    }
}
