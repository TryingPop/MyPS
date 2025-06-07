using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 25
이름 : 배성훈
내용 : 여우는 어떻게 울지?
    문제번호 : 9536번

    문자열, 해시 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1636
    {

        static void Main1636(string[] args)
        {

            string END = "what does the fox say?";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int q = int.Parse(sr.ReadLine());
            HashSet<string> not = new(100);

            while (q-- > 0)
            {

                string[] input = sr.ReadLine().Split();
                not.Clear();

                string str;
                while ((str = sr.ReadLine()) != END)
                {

                    not.Add(str.Split()[2]);
                }

                for (int i = 0; i < input.Length; i++)
                {

                    if (not.Contains(input[i])) continue;
                    sw.Write(input[i]);
                    sw.Write(' ');
                }

                sw.Write('\n');
            }
        }
    }
}
