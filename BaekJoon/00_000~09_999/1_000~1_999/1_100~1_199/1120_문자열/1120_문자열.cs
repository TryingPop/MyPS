using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 13
이름 : 배성훈
내용 : 문자열
    문제번호 : 1120번

    문자열, 브루트포스 문제다.
    아이디어는 다음과 같다.
    앞 뒤로 추가하는데 추가된 것들은 매칭되는걸로 추가하는게 
    그리디로 최소임을 알 수 있다.

    그래서 해당 자리에서 매칭 안되는 갯수를 찾아주면 되는 문제로 바뀐다.
*/

namespace BaekJoon.etc
{
    internal class etc_1271
    {

        static void Main1271(string[] args)
        {

            string[] input = Console.ReadLine().Split();
            int ret = 51;
            int len = input[1].Length - input[0].Length;
            for (int i = 0; i <= len; i++)
            {

                int chk = 0;
                for (int j = 0; j < input[0].Length; j++)
                {

                    if (input[0][j] == input[1][i + j]) continue;
                    chk++;
                }

                ret = Math.Min(ret, chk);
            }

            Console.Write(ret);
        }
    }
}
