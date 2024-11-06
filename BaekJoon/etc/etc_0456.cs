using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 럭키 세븐
    문제번호 : 28706번

    dp 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0456
    {

        static void Main456(string[] args)
        {

            int PLUS = '+' - '0';
            string YES = "LUCKY";
            string NO = "UNLUCKY";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            bool[] cV = new bool[7];
            bool[] nV = new bool[7];

            while(test-- > 0)
            {

                int len = ReadInt();

                cV[1] = true;
                for (int i = 0; i < len; i++)
                {

                    int op1 = ReadInt();
                    int n1 = ReadInt();

                    int op2 = ReadInt();
                    int n2 = ReadInt();

                    for (int j = 0; j < 7; j++)
                    {

                        if (!cV[j]) continue;
                        nV[Next(j, op1, n1)] = true;
                        nV[Next(j, op2, n2)] = true;

                        cV[j] = false;
                    }

                    bool[] temp = cV;
                    cV = nV;
                    nV = temp;
                }

                sw.WriteLine(cV[0] ? YES : NO);

                for (int i = 0; i < 7; i++)
                {

                    cV[i] = false;
                }
            }

            sr.Close();
            sw.Close();

            int Next(int _n, int _op, int _calc)
            {

                int ret = _op == PLUS ? _n + _calc : _n * _calc;
                ret %= 7;
                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.Text;

public class Program
{
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < t; i++)
        {
            int n = int.Parse(Console.ReadLine());
            bool[,] dp = new bool[n + 1, 7];
            dp[0, 1] = true;
            for (int j = 1; j <= n; j++)
            {
                string s = Console.ReadLine();
                int v1 = s[2] - '0', v2 = s[6] - '0';
                for (int k = 0; k < 7; k++)
                {
                    dp[j, (s[0] == '+' ? k + v1 : k * v1) % 7] |= dp[j - 1, k];
                    dp[j, (s[4] == '+' ? k + v2 : k * v2) % 7] |= dp[j - 1, k];
                }
            }
            sb.Append(dp[n, 0] ? "LUCKY" : "UNLUCKY");
            if (i < t - 1)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
}
#endif
}
