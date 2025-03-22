using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : 아무래도이문제는A번난이도인것같다
    문제번호 : 1402번

    수학 문제다.
    변환은 추이성이 성립한다.
    A => B => 이면 A => C이다.

    A < B인 경우
    A x 1 => A + 1로 계속해서 변환해 가면 B로 바뀔 수 있다.
    이제 B < A 인 경우는 다음으로 만들 수 있다.
    A x 1 => A + 1 로 계속해서 A를 변환해 간다.
    |B| < |A|가 될때까지 진행한다.
    이후 -A x -1 => -A - 2 < B가 된다.
    그러면 B로 변환할 수 있다.

    모든 정수로 서로 변환 가능하다.
    그래서 테스트 케이스 수 만큼 yes를 출력하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1427
    {

        static void Main1427(string[] args)
        {

            string ans = "yes\n";
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {

                sw.Write(ans);
            }
        }
    }
}
