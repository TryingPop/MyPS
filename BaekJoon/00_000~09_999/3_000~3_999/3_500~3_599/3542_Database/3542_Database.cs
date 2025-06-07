using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 20
이름 : 배성훈
내용 : Database
    문제번호 : 3542번

    문자열, 해시 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1126
    {

        static void Main1126(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string[] temp = sr.ReadLine().Split();
            int n = int.Parse(temp[0]);
            int m = int.Parse(temp[1]);

            string[][] input = new string[n][];
            for (int i = 0; i < n; i++)
            {

                input[i] = sr.ReadLine().Split(',');
            }

            sr.Close();

            Dictionary<(string f, string b), int> dic = new(n);

            for (int c1 = 0; c1 < m; c1++)
            {

                for (int c2 = c1 + 1; c2 < m; c2++)
                {

                    for (int r = 0; r < n; r++)
                    {

                        (string f, string b) chk = (input[r][c1], input[r][c2]);
                        if (dic.ContainsKey(chk))
                        {

                            Console.Write($"NO\n{dic[chk] + 1} {r + 1}\n{c1 + 1} {c2 + 1}");
                            return;
                        }

                        dic[chk] = r;
                    }

                    dic.Clear();
                }
            }

            Console.Write("YES");
        }
    }
}
