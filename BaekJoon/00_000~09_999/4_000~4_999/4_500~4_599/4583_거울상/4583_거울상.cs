using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 18
이름 : 배성훈
내용 : 거울상
    문제번호 : 4583번

    구현 문제다.
    조건대로 구현했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1632
    {

        static void Main1632(string[] args)
        {

            // 거울 문자 가능한지 확인용 문자열
            bool[] chk = new bool[255];
            // 뒤집어진 문자열
            char[] rev = new char[255];

            SetChk();

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string N = "INVALID\n";
            string input;
            while ((input = sr.ReadLine())[0] != '#')
            {

                bool flag = true;
                for (int i = 0; i < input.Length; i++)
                {

                    if (chk[input[i]]) continue;
                    flag = false;
                    break;
                }

                if (flag)
                {

                    for (int i = input.Length - 1; i >= 0; i--)
                    {

                        sw.Write(rev[input[i]]);
                    }

                    sw.Write('\n');
                }
                else sw.Write(N);
            }

            void SetChk()
            {

                // 뒤집기 가능 문자와 뒤집어진 문자 세팅
                char[] s = { 'i', 'o', 'v', 'w', 'x' }; // 뒤집어도 같은 문자
                char[] r = { 'p', 'q', 'b', 'd' };      // 뒤집어진 문자이 존재하는 문자

                for (int i = 0; i < s.Length; i++)
                {

                    chk[s[i]] = true;
                }

                for (char i = 'a'; i < 'z'; i++)
                {

                    rev[i] = i;
                }

                for (int i = 0; i < r.Length; i++)
                {

                    chk[r[i]] = true;
                    rev[r[i]] = r[i^ 1];
                }
            }
        }
    }
}
