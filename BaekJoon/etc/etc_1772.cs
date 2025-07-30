using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 17
이름 : 배성훈
내용 : 오늘 날짜
    문제번호 : 10699번

    구현 문제다.
    DateTime을 이용해 오늘 날짜 정보를 받아왔다.
    그리고 문제에 맞게 문자열 보간을 이용해 출력했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1772
    {

        static void Main1772(string[] args)
        {

            DateTime cur = DateTime.Now;
            // yyyy : 4자리 년도
            // MM : 2자리 월 mm은 분이다!
            // dd : 2자리 일
            Console.Write($"{cur:yyyy-MM-dd}");
        }
    }
}
