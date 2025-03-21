using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 22
이름 : 배성훈
내용 : 단어 뒤집기
    문제번호 : 9093번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1214
    {

        static void Main1214(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = int.Parse(sr.ReadLine());
            while(t-- > 0)
            {

                GetRet();
            }
            sr.Close();
            sw.Close();

            void GetRet()
            {

                string[] rev = sr.ReadLine().Split();
                
                for (int i = 0; i < rev.Length; i++)
                {

                    Reverse(rev[i]);
                    sw.Write(' ');
                }

                sw.Write('\n');
                sw.Flush();
            }

            void Reverse(string _str)
            {

                for (int i = _str.Length - 1; i>= 0; i--)
                {

                    sw.Write(_str[i]);
                }
            }
        }
    }
}
