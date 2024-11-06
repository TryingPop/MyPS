using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 팬그램
    문제번호 : 10384번
    
    구현, 문자열 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0466
    {

        static void Main466(string[] args)
        {

            string ZERO = "Not a pangram\n";
            string ONE = "Pangram!\n";
            string TWO = "Double pangram!!\n";
            string THREE = "Triple pangram!!!\n";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int n = int.Parse(sr.ReadLine());
            int[] cnt = new int[26];

            for (int i = 1; i <= n; i++)
            {

                string str = sr.ReadLine();

                for (int j = 0; j < str.Length; j++)
                {

                    if (str[j] <= 'Z' && 'A' <= str[j]) cnt[str[j] - 'A']++;
                    else if (str[j] <= 'z' && 'a' <= str[j]) cnt[str[j] - 'a']++;
                }

                int chk = 3;
                for (int j = 0; j < 26; j++)
                {

                    if (cnt[j] < chk) chk = cnt[j];
                    cnt[j] = 0;
                }
                sw.Write($"Case {i}: ");
                if (chk == 0) sw.Write(ZERO);
                else if (chk == 1) sw.Write(ONE);
                else if (chk == 2) sw.Write(TWO);
                else sw.Write(THREE);
            }

            sr.Close();
            sw.Close();
        }
    }
}
