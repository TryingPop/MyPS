using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 25
이름 : 배성훈
내용 : Affine Cipher
    문제번호 : 11575번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1216
    {

        static void Main1216(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int a, b;
            string str;
            Solve();
            void Solve()
            {

                Init();
                int t = int.Parse(sr.ReadLine());

                while (t-- > 0) 
                {

                    Input();
                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                string[] temp = sr.ReadLine().Split();
                a = int.Parse(temp[0]);
                b = int.Parse(temp[1]);

                str = sr.ReadLine();
            }

            void GetRet()
            {

                for (int i = 0; i < str.Length; i++)
                {

                    int cur = str[i] - 'A';
                    cur = (cur * a + b) % 26;
                    sw.Write((char)(cur + 'A'));
                }

                sw.Write('\n');
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            }
        }
    }
}
