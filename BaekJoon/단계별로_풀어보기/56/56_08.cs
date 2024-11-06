using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 26
이름 : 배성훈
내용 : 접두사 배열
    문제번호 : 13322번

    문자열, 구현, 애드 혹 문제다
    앞의 값들이 모두 같기에 길이순으로 나열된다
*/

namespace BaekJoon._56
{
    internal class _56_08
    {

        static void Main8(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 2);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 2);

                string str = sr.ReadLine();
                for (int i = 0; i < str.Length; i++)
                {

                    sw.Write($"{i}\n");
                }

                sr.Close();
                sw.Close();
            }
        }
    }
}
