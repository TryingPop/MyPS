using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 1 
이름 : 배성훈
내용 : 크냐?
    문제번호: 4101번

    단순 구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1368
    {

        static void Main1368(string[] args)
        {

            string Y = "Yes\n";
            string N = "No\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;

            while (Input())
            {

                sw.Write(n > m ? Y : N);
            }

            bool Input()
            {

                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                return n != 0 || m != 0;
            }
        }
    }
}
