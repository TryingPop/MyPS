using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1415
    {

        static void Main1415(string[] args)
        {

            // 24505
            int MOD = 1_000_000_007;
            int n;
            int[] arr;
            int[][] seg;


            Input();

            SetSeg();

            void GetRet()
            {

                void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
                {

                    if (_s == _e)
                    {

                        return;
                    }

                    int mid = (_s + _e) >> 1;
                    if (_chk <= mid) Update(_s, mid, _chk, _val, _idx * 2 + 1);
                    else Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);

                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)Math.Log2(n - 1) + 2;

                seg = new int[12][];
                for (int i = 0; i < 12; i++)
                {

                    seg[i] = new int[1 << log];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
                        if (c == ' ' || c == '\n') return true;
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
}
