using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 18
이름 : 배성훈
내용 : 뒤집기
    문제번호 : 1439번

    그리디 문제다.
    그리디로 0 또는 1로된 구간의 갯수 중 최솟값이 변형의 최솟갑이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1417
    {

        static void Main1417(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string input = sr.ReadLine();

            int prev = -1;
            int one = 0, zero = 0;

            for (int i = 0; i < input.Length; i++)
            {

                if (prev == input[i]) continue;
                prev = input[i];
                if (input[i] == '1') one++;
                else zero++;
            }

            Console.Write(Math.Min(one, zero));
        }
    }
}
