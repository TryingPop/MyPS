using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 21
이름 : 배성훈
내용 : 음주 코딩
    문제번호 : 5676번

    세그먼트 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1832
    {

        static void Main1832(string[] args)
        {

            // 상수 정의
            int CHANGE = 'C' - '0';

            int NOT_VISIT = 2;
            int END = -101;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] seg = new int[1 << 18];
            int b = 1 << 17;
            int n, k;

            while (Input())
            {

                for (int i = 0; i < k; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();

                    if (op == CHANGE)
                        Update(f, Sig(t));
                    else
                    {

                        int ret = GetVal(f, t);
                        Print(ret);
                    }
                }

                sw.Write('\n');
            }

            void Print(int _val)
            {

                if (_val > 0) sw.Write('+');
                else if (_val < 0) sw.Write('-');
                else sw.Write('0');
            }

            bool Input()
            {

                n = ReadInt();
                k = ReadInt();

                Array.Fill(seg, NOT_VISIT);

                for (int i = 1; i <= n; i++)
                {

                    int val = Sig(ReadInt());
                    Update(i, val);
                }

                return n != END;
            }

            int Sig(int _val)
            {

                if (_val > 0) return 1;
                else if (_val < 0) return -1;
                else return 0;
            }

            void Update(int _idx, int _val)
            {

                int idx = b | _idx;
                seg[idx] = _val;

                while (idx > 1)
                {

                    int p = idx >> 1;
                    seg[p] = seg[idx] * seg[idx ^ 1];
                    idx = p;
                }
            }

            int GetVal(int _l, int _r)
            {

                int l = _l | b;
                int r = _r | b;

                int ret = 1;
                while (l < r)
                {

                    if ((l & 1) == 1) ret *= seg[l++];
                    if ((r & 1) == 0) ret *= seg[r--];

                    l >>= 1;
                    r >>= 1;
                }

                if (l == r) ret *= seg[l];
                return ret;
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
                    else if (c == -1)
                    {

                        ret = END;
                        return false;
                    }
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
