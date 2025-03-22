using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 23
이름 : 배성훈
내용 : 수열 정렬
    문제번호 : 1015번
    
    정렬 문제다.
    비내림차순 정렬한 뒤 인덱스를 찾을 수 있어야 한다.
    그래서 인덱스와 값을 함께 저장하는 자료형으로 풀고
    값은 정렬하고나서는 쓸모없으니 순서로 값을 바꿨다.
    그리고 다시 인덱스로 정렬해 순서를 출력했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1291
    {

        static void Main1291(string[] args)
        {

            (int val, int idx)[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr, (x, y) =>
                {

                    int ret = x.val.CompareTo(y.val);
                    if (ret == 0) ret = x.idx.CompareTo(y.idx);
                    return ret;
                });

                for (int i = 0; i < arr.Length; i++)
                {

                    arr[i].val = i;
                }

                Array.Sort(arr, (x, y) => x.idx.CompareTo(y.idx));
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < arr.Length; i++)
                {

                    sw.Write($"{arr[i].val} ");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                int n = ReadInt();
                arr = new (int val, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), i);
                }

                int ReadInt()
                {

                    int c, ret = 0;
                    while((c = sr.Read()) != -1 && c!= ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
