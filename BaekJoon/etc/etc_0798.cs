using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6
이름 : 배성훈
내용 : 포닉스의 문단속
    문제번호 : 31784번

    그리디, 문자열 문제다
    무턱대고 하다가 26번 바꾸는 경우와 
    마지막에 k가 대문자 범위를 벗어나 1번 더 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0798
    {

        static void Main798(string[] args)
        {

            StreamReader sr;

            int n, k;
            int[] ret;

            Solve();

            void Solve()
            {

                Input();

                SetRet();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write((char)ret[i]);
                }

                sw.Close();
            }

            void SetRet()
            {

                for (int i = 0; i < n; i++)
                {

                    if (ChkChange(i)) continue;
                    else if (k == 0) return;
                }

                k %= 26;
                ret[n - 1] += k;
                if (ret[n - 1] > 'Z') ret[n - 1] -= 26;
            }

            bool ChkChange(int _idx)
            {

                if (ret[_idx] == 'A') return true;

                int change = 26 - (ret[_idx] - 'A');
                if (k < change) return true;
                k -= change;
                ret[_idx] = 'A';
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                ret = new int[n];
                for (int i = 0; i < n; i++)
                {

                    ret[i] = sr.Read();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;

public class Program
{
    static void Main()
    {
        string[] nk = Console.ReadLine().Split(' ');
        int n = int.Parse(nk[0]), k = int.Parse(nk[1]);
        char[] s = Console.ReadLine().ToCharArray();
        for (int i = 0; i < n; i++)
        {
            if (k == 0)
                break;
            if (91 - s[i] <= Math.Min(k, 25))
            {
                k -= 91 - s[i];
                s[i] = 'A';
            }
        }
        if (k > 0)
        {
            int c = s[^1] + k % 26;
            s[^1] = c <= 'Z' ? (char)c : (char)('A' + c - 'Z' - 1);
        }
        Console.Write(string.Concat(s));
    }
}
#endif
}
