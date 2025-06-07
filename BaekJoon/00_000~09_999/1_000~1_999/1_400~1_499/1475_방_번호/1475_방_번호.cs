using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 방번호
    문제번호 : 1475번

    구현 문제다
    아이디어는 다음과 같다
    6, 9 이외 사용된 숫자의 최대 개수를 센다

    그리고 이중에 가장 많은것을 찾는다
    6, 9의 경우 서로 바꿔쓸 수 있으므로 따로 센다
    그리고 합의 절반을 찾는다(올림)

    그리고 앞에서 찾은 사용된 최대 개수와 6, 9 중앙값 중 큰게 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0393
    {

        static void Main393(string[] args)
        {

            string str = Console.ReadLine();
            int[] cnt = new int[10];

            for (int i = 0; i < str.Length; i++)
            {

                int cur = str[i] - '0';

                cnt[cur]++;
            }

            int sn = 0;
            int ret = 0;
            for (int i = 0; i < 10; i++)
            {

                if (i == 6 || i == 9) sn += cnt[i];
                else if (ret < cnt[i]) ret = cnt[i];
            }

            sn += 1;
            sn /= 2;

            ret = sn < ret ? ret : sn;

            Console.WriteLine(ret);
        }
    }
}
