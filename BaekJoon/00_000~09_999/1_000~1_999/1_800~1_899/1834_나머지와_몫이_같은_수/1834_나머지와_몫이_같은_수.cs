using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 23
이름 : 배성훈
내용 : 나머지와 몫이 같은 수
    문제번호 : 1834번

    예제에서 보듯이 n = 2_000_000인 경우 3_999_999_999_999_000_000로
    ulong 범위를 초과한다. 그래서 BigInteger를 이용해 
*/

namespace BaekJoon.etc
{
    internal class etc_1357
    {

        static void Main1357(string[] args)
        {

            BigInteger ret = 0L;
            long n = int.Parse(Console.ReadLine());

            for (int i = 1; i < n; i++)
            {

                long add = i * n + i;
                ret += add;
            }

            Console.Write(ret);
        }
    }
}
