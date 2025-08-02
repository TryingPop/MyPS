using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 콰트로치즈피자
    문제번호 : 27964번

    구현, 문자열, 해시 문제다
    중복되는 치즈가 없어야하기에 hashset 자료구조를 이용했다
    그리고 cheese는 마지막만 확인하면 되기에
    6개 이상인 문자에 한해서 뒤에서 6개만 세었다
    그리고 서로 다른 4개의 치즈가 필요하기에 4개 이상이면 yes로 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0346
    {

        static void Main346(string[] args)
        {

            string YES = "yummy";
            string NO = "sad";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            HashSet<string> set = new(n);

            string[] temp = sr.ReadLine().Split(' ');
            sr.Close();

            char[] cheese = new char[6] { 'e', 's', 'e', 'e', 'h', 'C' };
            for (int i = 0; i < temp.Length; i++)
            {

                if (temp[i].Length < 6) continue;

                bool find = true;
                for (int j = 0; j < 6; j++)
                {

                    if (temp[i][^(j + 1)] == cheese[j]) continue;
                    find = false;
                }

                if (find) set.Add(temp[i]);
            }

            if (set.Count > 3) Console.WriteLine(YES);
            else Console.WriteLine(NO);
        }
    }
}
