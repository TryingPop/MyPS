using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 31
이름 : 배성훈
내용 : 커피숍2
    문제번호 : 1275번

    세그먼트 트리 문제다.
    기존에는 재귀함수로 구현했는데, 여기서는 while문으로 구현했다.
    확실히 속도가 빠르다.
*/

namespace BaekJoon.etc
{
    internal class etc_1502
    {

        static void Main1502(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m, bias;
            long[] seg;

            Input();

            GetRet();

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                SetSeg();

                for (int i = 1; i <= n; i++)
                {

                    seg[i | bias] = ReadInt();
                }

                for (int i = bias - 1; i >= 1; i--)
                {
                    int child = i << 1;
                    seg[i] = seg[child] + seg[child | 1];
                }
            }

            void GetRet()
            {

                for (int i = 0; i < m; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();
                    int a = ReadInt();
                    int b = ReadInt();

                    long ret = GetVal(x, y);
                    sw.Write(ret);
                    sw.Write('\n');

                    Update(a, b);
                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)Math.Log2(n - 1) + 1;
                bias = 1 << log;
                seg = new long[(bias << 1) + 1];
            }

            void Update(int _chk, int _val)
            {

                int idx = bias | _chk;
                seg[idx] = _val;

                while (idx > 1)
                {

                    int parent = idx >> 1;
                    seg[parent] = seg[idx] + seg[idx ^ 1];
                    idx = parent;
                }
            }

            long GetVal(int _f, int _t)
            {

                if (_t < _f)
                {

                    int temp = _f;
                    _f = _t;
                    _t = temp;
                }

                int l = bias | _f;
                int r = bias | _t;

                long ret = 0;
                while (l < r)
                {

                    if ((l & 1L) == 1L) ret += seg[l++];
                    if ((r & 1L) == 0L) ret += seg[r--];
                    l >>= 1;
                    r >>= 1;
                }

                if(l == r) ret += seg[l];
                return ret;
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
