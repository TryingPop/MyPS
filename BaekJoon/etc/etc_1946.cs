using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 21
이름 : 배성훈
내용 : 회문인 수
    문제번호 : 11068번

    브루트포스 문제다.
    조건대로 b진법으로 바꿔서 회문인지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1946
    {

        static void Main1946(string[] args)
        {

            string Y = "1\n";
            string N = "0\n"; 
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int t = int.Parse(sr.ReadLine());
            int[] calc = new int[21];
            int len;
            while (t-- > 0)
            {

                int num = int.Parse(sr.ReadLine());

                bool ret = false;
                for (int i = 2; i <= 64; i++)
                {

                    Fill(num, i);
                    if (ChkRet()) 
                    { 
                        
                        ret = true;
                        break;
                    }
                }

                sw.Write(ret ? Y : N);
            }

            bool ChkRet()
            {

                for (int i = 0, j = len - 1; i < j; i++, j--)
                {

                    if (calc[i] != calc[j]) return false;
                }

                return true;
            }

            void Fill(int num, int digit)
            {

                len = 0;
                while (num > 0)
                {

                    calc[len++] = num % digit;
                    num /= digit;
                }
            }
        }
    }
}
