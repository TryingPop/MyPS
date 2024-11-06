using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 뒤집기 3
    문제번호 : 1464번

    자료구조, 덱, 그리디 알고리즘 문제다
    먼저 그리디로 규칙을 찾고,
    두 포인터로 배열을 덱형태를 띄게해서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0258
    {

        static void Main258(string[] args)
        {

            string str = Console.ReadLine();
            int n = str.Length;
            char[] arr = new char[2 * n];
            char beforeMin = str[0];
            arr[n] = str[0];
            int s = n;
            int e = n;

            for (int i = 1; i < n; i++)
            {

                // 그리디
                if (arr[s] >= str[i])
                {

                    // 가장 작으면 맨 앞에 와야한다
                    arr[--s] = str[i];
                }
                else
                {

                    // 아니면 맨 뒤로 가야한다
                    arr[++e] = str[i];
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = s; i <= e; i++)
                {

                    sw.Write(arr[i]);
                }
            }

        }
    }
}
