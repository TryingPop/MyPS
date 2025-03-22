using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2 11
이름 : 배성훈
내용 : 팰린드롬수
    문제번호 : 1259번

    팰린드롬 분할을 풀기전에 팰린 드롬을 알기 위해 푸는 연습문제
*/

namespace BaekJoon.etc
{
    internal class etc_0014
    {

        static void Main14(string[] args)
        {

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string YES = "yes";
            string NO = "no";

            while (true)
            {

                string str = sr.ReadLine();
                if (str == "0") break;

                // 앞 뒤가 같으면 팰린드롬!
                int end = str.Length - 1;
                int mid = end / 2;
                bool yes = true;
                for (int i = 0; i <= mid; i++)
                {

                    if (str[i] != str[end - i])
                    {

                        yes = false;
                        break;
                    }
                }

                sw.WriteLine(yes ? YES : NO);
            }

            sw.Close();
            sr.Close();
        }
    }
}
