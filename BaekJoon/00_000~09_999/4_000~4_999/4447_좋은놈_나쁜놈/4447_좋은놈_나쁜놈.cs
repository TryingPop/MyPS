using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 1
이름 : 배성훈
내용 : 좋은놈 나쁜놈
    문제번호 : 4447번

    문자열 문제다.
    문자열안에 g, b의 갯수를 세어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1369
    {

        static void Main1369(string[] args)
        {

            string MID = " is ";
            string RET1 = "GOOD\n";
            string RET2 = "A BADDY\n";
            string RET3 = "NEUTRAL\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            string input;
            for (int i = 0; i < n; i++)
            {

                input = sr.ReadLine();

                int ret = GetType();
                sw.Write(input);
                sw.Write(MID);
                if (ret == 1) sw.Write(RET1);
                else if (ret == 2) sw.Write(RET2);
                else sw.Write(RET3);

                sw.Flush();
            }

            int GetType()
            {

                int g = 0;
                int b = 0;

                for (int i = 0; i < input.Length; i++)
                {

                    if (input[i] == 'g' || input[i] == 'G') g++;
                    if (input[i] == 'b' || input[i] == 'B') b++;
                }

                if (g > b) return 1;
                else if (g < b) return 2;
                else return 3;
            }
        }
    }
}
