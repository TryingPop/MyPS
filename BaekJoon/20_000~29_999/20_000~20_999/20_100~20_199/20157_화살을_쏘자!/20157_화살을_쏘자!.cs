using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 28
이름 : 배성훈
내용 : 화살을 쏘자!
    문제번호 : 20157번

    해시, 수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1736
    {

        static void Main1736(string[] args)
        {

            int n;
            Dictionary<(int x, int y), int> dic;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                foreach(int item in dic.Values)
                {

                    ret = Math.Max(ret, item);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                dic = new(n);
                for (int i = 0; i < n; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();

                    int gcd = GetGCD(Math.Abs(a), Math.Abs(b));
                    a /= gcd;
                    b /= gcd;

                    if (dic.ContainsKey((a, b))) dic[(a, b)]++;
                    else dic[(a, b)] = 1;
                }

                int GetGCD(int _a, int _b)
                {

                    while (_b > 0)
                    {

                        int temp = _a % _b;
                        _a = _b;
                        _b = temp;
                    }

                    return _a;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }
}
