using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 23
이름 : 배성훈
내용 : 민균이의 비밀번호
    문제번호 : 9933번

    해시, 문자열 문제다.
    StringBuilder를 이용해 문자열을 뒤집은 문자열을 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1356
    {

        static void Main1356(string[] args)
        {

            StringBuilder sb = new(15);
            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());
            HashSet<string> set = new(n);

            for (int i = 0; i < n; i++)
            {

                string input = sr.ReadLine();
                set.Add(input);

                string rev = Reverse(input);
                if (set.Contains(rev) && (input.Length & 1) == 1)
                {

                    Console.Write($"{input.Length} {input[input.Length >> 1]}");
                    break;
                }
            }

            string Reverse(string _str)
            {

                sb.Clear();
                for (int i = _str.Length - 1; i >= 0; i--)
                {

                    sb.Append(_str[i]);
                }

                return sb.ToString();
            }
        }
    }
}
