using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 21
이름 : 배성훈
내용 : HangMan
    문제번호 : 26514번

    구현 문제다.
    조건대로 구현했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1212
    {

        static void Main1212(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            bool[] use;
            string[] input;
            string hangman;
            int[] idx;

            Solve();
            void Solve()
            {

                Init();

                int t = int.Parse(sr.ReadLine());

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                Array.Fill(use, false);
                int cnt = 0;
                for (int i = 0; i < input[0].Length; i++)
                {

                    int curIdx = input[0][i] - 'A';
                    if (use[curIdx]) continue;
                    use[curIdx] = true;
                    cnt++;
                }

                int len = int.Parse(input[1]);
                int ret = 0;
                for (int i = 0; i < len; i++)
                {

                    int curIdx = input[i + 2][0] - 'A';
                    if (use[curIdx])
                    {

                        use[curIdx] = false;
                        cnt--;
                        if (cnt == 0) break;
                    }
                    else 
                    {

                        ret++;
                        if (ret == 9) break;
                    }
                }

                sw.Write($"{input[0]}\n");
                if (ret == 0) sw.Write("SAFE");
                else
                {

                    for (int i = 0; i <= idx[ret]; i++)
                    {

                        sw.Write(hangman[i]);
                    }
                }

                sw.Write("\n\n");
            }

            void Input()
            {

                input = sr.ReadLine().Split();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                use = new bool[26];
                hangman = "  O\n+=|=+\n  |\n / \\";
                idx = new int[10] { -1, 2, 4, 5, 6, 7, 8, 12, 15, 0 };
                idx[9] = hangman.Length - 1;
            }
        }
    }
}
