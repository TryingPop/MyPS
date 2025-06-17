using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 3
이름 : 배성훈
내용 : 성택이의 은밀한 비밀번호
    문제번호 : 25372번
*/

namespace BaekJoon.etc
{
    internal class etc_0152
    {

        static void Main152(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {

                string str = Console.ReadLine();
                if (str.Length <= 9 && str.Length >= 6) Console.WriteLine("yes");
                else Console.WriteLine("no");
            }
        }
    }
}
