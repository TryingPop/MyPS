using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 9
이름 : 배성훈
내용 : All Your Base (Small, Large)
    문제번호 : 12637번, 12638번

    정수론, 그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1756
    {

        static void Main1756(string[] args)
        {

            string HEAD = "Case #";
            string MID = ": ";
            string TAIL = "\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int q = int.Parse(sr.ReadLine());
            string str;
            int[] use = new int[255];
            int cnt;

            for (int t = 1; t <= q; t++)
            {

                sw.Write(HEAD);
                sw.Write(t);
                sw.Write(MID);
                Input();

                GetRet();

                sw.Write(TAIL);
            }

            void GetRet()
            {

                for (int i = 0; i < str.Length; i++)
                {

                    if (use[str[i]] == 0) use[str[i]] = ++cnt;
                }

                long ret = 0;
                if (cnt == 1) cnt++;
                for (int i = 0; i < str.Length; i++)
                {

                    ret = GetVal(use[str[i]]) + ret * cnt;
                }

                sw.Write(ret);
                int GetVal(int _v)
                {

                    if (_v == 1) return _v;
                    else if (_v == 2) return 0;
                    else return _v - 1;
                }
            }

            void Input()
            {

                str = sr.ReadLine();
                Array.Fill(use, 0);
                cnt = 0;
            }
        }
    }
}
