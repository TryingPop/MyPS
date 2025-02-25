using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 21
이름 : 배성훈
내용 : 책 정리
    문제번호 : 1434번

    수학, 구현 문제다.
    그리디로 최대한 넣으면서 진행하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1352
    {

        static void Main1352(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int[] nm = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int[] box = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int[] book = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

#if first
            int ret = box.Sum() - book.Sum();
#else
            int ret = 0;
            int boxIdx = 0;
            int r = box[boxIdx++];
            for (int i = 0; i < book.Length; i++)
            {

                while (r < book[i])
                {

                    ret += r;
                    r = box[boxIdx++];
                }

                r -= book[i];
            }

            ret += r;
            while (boxIdx < box.Length)
            {

                ret += box[boxIdx++];
            }
#endif
            Console.Write(ret);
        }
    }
}
