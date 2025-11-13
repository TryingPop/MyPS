using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 9
이름 : 배성훈
내용 : 트리 만들기
    문제번호 : 14244번

    그래프 이론 문제다.

    조건에서 해가 언제나 존재함을 알 수 있다.
    그런데 친절하게 문제 설명에서 해가 존재함을 알려준다.

    리프를 가장 밑에 자식의 개수로 만들어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1968
    {

        static void Main1968(string[] args)
        {

            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < n - m; i++)
                {

                    sw.Write($"{i} {i + 1}\n");
                }

                for (int i = n - m + 1; i < n; i++)
                {

                    sw.Write($"{0} {i}\n");
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
            }
        }
    }
}
