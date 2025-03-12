using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1401
    {

        static void Main(string[] args)
        {

            // 16124
            long MOD = 998_244_353L;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string input;

            int n;
            long[] pow;
            (long val, long lazy)[][] tree;


            void Input()
            {

                input = sr.ReadLine();
                n = input.Length;

                pow[0] = 1;
                for (int i = 0; i < n; i++)
                {

                    pow[i + 1] = (pow[i] * 10) % MOD;
                }

                int log = n == 1 ? 1 : (int)(Math.Log2(n - 1) + 1e-9) + 2;
                tree = new (long val, long lazy)[10][];
                for (int i = 0; i < n; i++)
                {

                    tree[i] = new (long val, long lazy)[1 << log];
                }
            }

            // 이거부터..
            void Update(int _s, int _e, int _chk, int _val, int _idx)
            {


            }


            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
