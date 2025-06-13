using System;
using System.IO;

/*
날짜 : 2025. 2. 1
이름 : 배성훈
내용 : 비트가 넘쳐흘러
    문제번호 : 17419번

    비트마스킹 문제다.
    문제 조건을 보면 k & (~k + 1)이 나오는데,
    이는 음의 보수법연산으로 하면, 
    가장 오른쪽에 1인 비트를 찾아낸다.
    
    그래서 해당 문제는 이진 수로 나타냈을 때 1의 개수만큼 연산하게 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1305
    {

        static void Main1305(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            int ret = 0;
            for (int i = 0; i < n; i++) 
            {

                if (sr.Read() == '0') continue;
                ret++;
            }

            Console.Write(ret);
        }
    }
}
