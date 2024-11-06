using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 문자열
    문제번호 : 9086번
*/

namespace BaekJoon._23
{
    internal class _23_18
    {

        static void Main18(string[] args)
        {

            int len = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {

                int c;
                char s = '\0', e = '\0';

                // vs에는 마지막에 c == '\r'도 추가 해줘야한다
                // 13 = '\r';
                while (!((c = Console.Read()) == '\n' || c == -1))
                {

                    if (s == '\0')
                    {

                        s = (char)c;
                    }

                    e = (char)c;
                }

                sb.Append(s).Append(e).Append('\n');
            }
            Console.WriteLine(sb);
        }
    }
}
