using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 팰린드롬인지 확인하기
    문제번호 : 10988번
*/

namespace BaekJoon._23
{
    internal class _23_07
    {

        static void Main7(string[] args)
        {

            string str = Console.ReadLine();
            int len = str.Length;
            
            // 대칭 판별
            for (int i = 0; i < len / 2; i++)
            {

                // 대칭 아닌 경우 탈출
                if (str[i] != str[len - 1 -i]) 
                {

                    Console.WriteLine("0");
                    return;
                }
            }

            // 대칭
            Console.WriteLine("1");
        }
    }
}
