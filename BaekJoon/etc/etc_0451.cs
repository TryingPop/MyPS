using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 개미 수열
    문제 번호 : 28292번

    구현, 에드혹 문제다
    .. 규칙성을 찾아 풀었다

    처음에는 문제를 잘못 이해해 한 번 틀렸다
    1121인 경우 1이 3개 2가 1개 다음으로 1321이 나오는줄 알았다
    그러나, 연속한거끼리만 같은지 카운트 한다 1121 다음에 122111이다

    이후 직접 엄청난 가비지를 생성하며 개미수열들을 찾는 식으로 먼저 제출해 시간초과를 받았다
    50까지 돌려본 결과 3을 못넘어간다고 추측했고
    숫자를 부여하는 규칙을 보니 최대 3개까지만 만들 수 있다
    정확히 1, 2, 3으로 4이상의 숫자를 만드는 것은 불가능하다

    만약 1, 2, 3의 숫자로 n번째에 처음으로 숫자 4이상의 숫자 k가 나왔다고 하자
    m이 존재해 mmm ... m 이 k개 있어야하는데, 
    m이 연달아 나온경우는 m2가 되므로 m이 m이 m * (10 ^k -1 + 10^k-2 + ... + 1)번 나와야한다
    그리고 이를 계속해서 나가면 n이 자연수이므로 언젠가는 1이 된다(페르마의 강하법)
    이때 시작은 1이 나올 수 없어 모순이다
    이는 4 이상의 숫자를 만들 수 있다는 사실에 기인한 것이므로 4 이상의 숫자는 만들 수가 없다

    그래서 6 이상은 3, 3 이상은 2, 이외는 1로 해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0451
    {

        static void Main451(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            char ret = '0';

            string str = "1";
            for (int i = 0; i < n; i++)
            {

                char before = '0';
                string newStr = "";
                int cnt = 0;
                ret = '0';
                for (int j = 0; j < str.Length; j++)
                {

                    if (ret < str[j])
                    {

                        ret = str[j];
                    }

                    if (before != str[j])
                    {

                        if (j > 0) newStr += $"{cnt}{str[j]}";
                        else newStr += $"{str[j]}";
                        before = str[j];
                        cnt = 1;
                    }
                    else cnt++;
                }

                newStr += $"{cnt}";
                str = newStr;
            }

            
            Console.WriteLine(ret);
        }
    }
}
