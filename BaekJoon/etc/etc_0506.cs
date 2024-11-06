using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 염색체
    문제번호 : 9342번

    문자열, 정규 표현식 문제다
    정규식 없이 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0506
    {

        static void Main506(string[] args)
        {

            string YES = "Infected!\n";
            string NO = "Good\n";

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
            int test = int.Parse(sr.ReadLine());

            while(test-- > 0)
            {

                string str = sr.ReadLine();
                bool ret = ChkString(str);
                sw.Write(ret ? YES : NO);
            }

            sr.Close();
            sw.Close();

            bool ChkString(string _str)
            {

                int f = _str[0] - 'A';
                // 맨 앞에 A, B, C, D, E, F인지 확인
                if (f > 5) return false;

                // 이후 앞에 A가 나오는지 확인
                int s = f == 0 ? 0 : 1;
                int n = -1;
                for (int i = s; i < _str.Length; i++)
                {

                    if (_str[i] == 'A') continue;

                    n = i;
                    break;
                }

                if (n == s || n == -1) return false;

                // F 판별
                s = n;
                n = -1;
                for (int i = s; i < _str.Length; i++)
                {

                    if (_str[i] == 'F') continue;

                    n = i;
                    break;
                }

                // C 판별
                if (s == n || n == -1) return false;

                s = n;
                n = _str.Length;

                for (int i = s; i < _str.Length; i++)
                {

                    if (_str[i] == 'C') continue;

                    n = i;
                    break;
                }

                if (n == s || n < _str.Length - 1) return false;

                // 마지막 글자 판별
                if (n == _str.Length - 1)
                {

                    f = _str[n] - 'A';
                    if (f > 5) return false;
                }

                return true;
            }
        }
    }

#if other
using System.Text.RegularExpressions;

int T = int.Parse(Console.ReadLine());
while (T-- > 0)
    Console.WriteLine(new Regex("^[A-F]?A+F+C+[A-F]?$").IsMatch(Console.ReadLine()) ? "Infected!" : "Good");
#elif other2
#endif
}
