using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 10
이름 : 배성훈
내용 : 팰린드롬
    문제번호 : 8892번

    문자열, 브루트포스 문제다.
    모든 두 문자열을 합치고, 팰린드롬인지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1533
    {

        static void Main1533(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] add = new int[20_000];
            string[] strs = new string[100];

            int t = int.Parse(sr.ReadLine());

            while (t-- > 0)
            {

                int n = int.Parse(sr.ReadLine());
                for (int i = 0; i < n; i++)
                {

                    strs[i] = sr.ReadLine();
                }

                bool flag = false;

                for (int i = 0; i < n; i++)
                {

                    Concat(i, 0);
                    int s = strs[i].Length;
                    for (int j = 0; j < n; j++)
                    {

                        if (i == j) continue;
                        Concat(j, s);
                        int len = s + strs[j].Length;
                        if (IsPalindrome(len))
                        {

                            flag = true;
                            sw.Write($"{strs[i]}{strs[j]}\n");
                            break;
                        }
                    }

                    if (flag) break;
                }

                if (flag) continue;

                sw.Write("0\n");
            }

            void Concat(int _idx, int _s)
            {

                for (int i = 0; i < strs[_idx].Length; i++)
                {

                    add[_s + i] = strs[_idx][i];
                }
            }

            bool IsPalindrome(int _len)
            {

                for (int s = 0, e = _len - 1; s < e; s++, e--)
                {

                    if (add[s] == add[e]) continue;
                    return false;
                }

                return true;
            }
        }
    }
}
