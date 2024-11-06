using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 코딩은 체육과목 입니다
    문제번호 : 25314번
*/

namespace BaekJoon._23
{
    internal class _23_10
    {

        static void Main10(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            int num = int.Parse(Console.ReadLine()) / 4;

            for (int i = 0; i < num; i++)
            {

                sb.Append("long ");
            }

            sb.Append("int\n");

            Console.WriteLine(sb);
        }
    }
}
