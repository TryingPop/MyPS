using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 22
이름 : 배성훈
내용 : 재귀의 귀재
    문제번호 : 25501번
*/

namespace BaekJoon._24
{
    internal class _24_02
    {

        static void Main2(string[] args)
        {

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int len = int.Parse(sr.ReadLine());


            for (int i = 0; i < len; i++)
            {

                int r = isPallindrome(sr.ReadLine(), 0, 0);
                sw.WriteLine($"{r/10_000} {r%10_000}");
            }

            sr.Close();
            sw.Close();
        }

        // 문자열 최대 길이가 천단위!
        // 그래서 넉넉하게 1만 잡았다
        // 만 이하 자리는 시행횟수이고, 앞자리가 만인 경우 성공, 만 미만인 경우 실패이다
        // 1000 자리까지라 만단위가 아닌 천 단위로 해도 된다
        static int isPallindrome(string str, int chk, int ptr)
        {

            if (chk == -1) return 0;
           
            chk = str[ptr] == str[str.Length - 1 - ptr++] ? 1 : - 1;
            if (ptr > (str.Length / 2)) return 10001;

            return 1 + isPallindrome(str, chk, ptr);
        }
    }
}
