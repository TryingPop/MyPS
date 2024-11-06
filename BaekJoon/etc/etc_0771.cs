using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 23
이름 : 배성훈
내용 : HG 음성기호
    문제번호 : 25594번

    구현, 문자열, 브루트포스 알고리즘 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0771
    {

        static void Main771(string[] args)
        {

            string YES = "It's HG!\n";
            string NO = "ERROR!";

            StreamReader sr;
            StreamWriter sw;

            StringBuilder sb;
            Dictionary<int, string> sTc;

            Solve();

            void Solve()
            {
                sTc = new(26);
                sTc['a'] = "aespa";
                sTc['b'] = "baekjoon";
                sTc['c'] = "cau";
                sTc['d'] = "debug";
                sTc['e'] = "edge";
                sTc['f'] = "firefox";
                sTc['g'] = "golang";
                sTc['h'] = "haegang";
                sTc['i'] = "iu";
                sTc['j'] = "java";
                sTc['k'] = "kotlin";
                sTc['l'] = "lol";
                sTc['m'] = "mips";
                sTc['n'] = "null";
                sTc['o'] = "os";
                sTc['p'] = "python";
                sTc['q'] = "query";
                sTc['r'] = "roka";
                sTc['s'] = "solvedac";
                sTc['t'] = "tod";
                sTc['u'] = "unix";
                sTc['v'] = "virus";
                sTc['w'] = "whale";
                sTc['x'] = "xcode";
                sTc['y'] = "yahoo";
                sTc['z'] = "zebra";

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);
                sb = new(100_000);

                bool possible = true;
                while (true)
                {

                    int c = sr.Read();
                    if (c == -1 || c == '\r' || c == '\n') break;
                    if (ChkInvalid(c))
                    {

                        possible = false;
                        break;
                    }

                    sb.Append((char)c);
                }

                if (possible)
                {

                    sw.Write(YES);
                    sw.Write(sb);
                }
                else sw.Write(NO);

                sw.Close();
                sr.Close();
            }

            bool ChkInvalid(int _start)
            {

                if (!sTc.ContainsKey(_start)) return true;
                string chk = sTc[_start];

                for (int i = 1; i < chk.Length; i++)
                {

                    int cur = sr.Read();
                    if (cur == chk[i]) continue;
                    return true;
                }

                return false;
            }
        }
    }
}
