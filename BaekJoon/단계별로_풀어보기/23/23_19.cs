using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 그대로 출력하기
    문제번호 : 11718번
*/

namespace BaekJoon._23
{
    internal class _23_19
    {

        static void Main19(string[] args)
        {

            StringBuilder sb = new StringBuilder();

#if false
            // 하나씩 읽는 방법 이경우 메모리는 적게드나 속도는 약간 느리다
            int c;

            // null을 넣는 경우 -1을 반환
            while (!((c = Console.Read()) == -1))
            {

                if (c != '\r')
                {

                    sb.Append((char)c);
                }
            }

#else
            string str = "";
            while (str != null)
            {

                str = Console.ReadLine();
                sb.AppendLine(str);
            }
#endif
            Console.WriteLine(sb);
        }
    }
}
