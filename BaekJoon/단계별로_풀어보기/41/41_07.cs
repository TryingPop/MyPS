using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 25
이름 : 배성훈
내용 : 큰 수(BIG)
    문제번호 : 14928번

    박성원(41_04) 문제를 해결하기 위해 필요한 조건을 찾아보니 큰 수를 나누는 방법을 알아야한다
    그래서 구글 검색을 통해 문제를 확인했고 테스트 겸 먼저 풀어본다
*/

namespace BaekJoon._41
{
    internal class _40_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            const int DIV = 20_000_303;
            
            long num = 0;
            int c;
            // 수자 입력받기
            while((c = sr.Read()) != '\n' && c != '\0')
            {

                if (c == '\r') continue;

                num *= 10;
                num += c - '0';
                if (num >= DIV) num %= DIV;
            }

            sr.Close();

            Console.WriteLine(num);
        }
    }
}
