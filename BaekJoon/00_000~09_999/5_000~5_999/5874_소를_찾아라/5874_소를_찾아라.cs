using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 소를 찾아라
    문제번호 : 5874번

    스위핑 문제다
    누적해서 개수를 세면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0128
    {

        static void Main128(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 뒷다리 갯수 저장
            int back = 0;

            int before = -1;
            int idx = 0;
            int ret = 0;
            while (true)
            {

                idx++;
                int c = sr.Read();

                if (c == -1 || c == ' ' || c == '\n') break;

                if (idx > 0 && c == before)
                {

                    if (c == '(') back += 1;
                    else if (c == ')')
                    {

                        ret += back;
                    }
                }

                before = c;
            }

            sr.Close();

            Console.WriteLine(ret);
        }

        
    }
}
