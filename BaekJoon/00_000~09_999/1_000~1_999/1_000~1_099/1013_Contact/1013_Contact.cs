using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*
날짜 : 2025. 3. 23
이름 : 배성훈
내용 : Contact
    문제번호 : 1013번

    정규 표현식 문제다.
    몇몇 기호를 알 필요가 있다.
    정규 표현식에서는 앞에 @를 이용한 포맷한 string을 사용한다.

    일반적인 문자열에서는 역슬래쉬 \를 앞에 써서 역슬래쉬를 하나 출력하는데,
    @로 포맷하면 해당 역슬래쉬 기호가 필요 없다.

    정규 표현식은 해당 패턴과 만족하는 매칭된 부분 문자열을 반환한다.
    이로 문자열의 시작부분을 나타내는 ^ 특수문자와 문자열의 끝 부분을 나타내는 $를 패턴의 앞 뒤에 안붙이는 경우
    일부 부분문자만 반환하기에 오답을 반환한다.

    예를들어 문자열 (100+1+|01)+ 를 패턴으로 하는 경우
    10011001의 경우 전체는 해당 패턴을 만족한다.
    그러나 매칭으로 찾은 부분 문자열은 10011로 나온다.
    그래서 앞서 이야기한 ^와 $를 이용해 패턴을 써야 한다.
    문자열 ^(100+1+|01)+$ 로 바꾸면 이상없이 작동한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1454
    {

        static void Main1454(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n";

            Regex regex = new Regex(@"^(100+1+|01)+$");
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            for (int i = 0; i < n; i++)
            {

                string input = sr.ReadLine();

                if (regex.IsMatch(input)) sw.Write(Y);
                else sw.Write(N);
            }
        }
    }
}
