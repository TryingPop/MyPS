using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 29
이름 : 배성훈
내용 : 9-퍼즐
    문제번호 : 14394번

    애드 혹 문제다.
    해당 퍼즐을 보면 바깥 테두리부터 맞추고 안쪽으로 맞추는 식으로 하면
    같은 색상의 개수의 모든 모형을 만들 수 있다.

    그래서 서로 다른 색상의 개수를 세면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1224
    {

        static void Main1224(string[] args)
        {

            string input = Console.ReadLine();
            int[] cnt = new int[26];
            for (int i = 0; i < input.Length; i++)
            {

                int cur = input[i];
                if (cur == '*') continue;
                cnt[cur - 'A']++;
            }

            input = Console.ReadLine();
            for (int i = 0; i < input.Length; i++)
            {

                int cur = input[i];
                if (cur == '*' || cnt[cur - 'A'] == 0) continue;
                cnt[cur - 'A']--;
            }

            int ret = 0;
            for (int i = 0; i < cnt.Length; i++)
            {

                ret += cnt[i];
            }

            Console.Write(ret);
        }
    }
}
