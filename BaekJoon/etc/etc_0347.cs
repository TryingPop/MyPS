using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 25
이름 : 배성훈
내용 : 블라인드
    문제번호 : 2799번

    구현 문제다
    문제 조건대로 구현했다
*/
namespace BaekJoon.etc
{
    internal class etc_0347
    {

        static void Main347(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] size = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] ret = new int[5];
            int[] calc = new int[size[1]];
            sr.ReadLine();
            for (int i = 0; i < size[0]; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    string[] temp = sr.ReadLine().Split('#');
                    for (int k = 1; k <= size[1]; k++)
                    {

                        if (temp[k][0] == '*') calc[k - 1]++;
                    }
                }

                for (int j = 0; j < size[1]; j++)
                {

                    ret[calc[j]]++;
                    calc[j] = 0;
                }

                sr.ReadLine();
            }

            for (int i = 0; i < 5; i++)
            {

                Console.Write($"{ret[i]} ");
            }
        }
    }
}
