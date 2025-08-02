using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 31
이름 : 배성훈
내용 : 특별한 화재 경보
    문제번호 : 27897

    세그먼트 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1800
    {

        static void Main1800(string[] args)
        {

            int[] seg = new int[1 << 20];
            int b = 1 << 19;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            long l = ReadLong();

            long ret = 0;

            for (int i = 0; i < n; i++)
            {

                int val = ReadInt();
                ret += GetVal(val);
                Update(val);
            }

            long INF = (n * (n - 1L)) >> 1;
            if (INF - ret < l) ret = INF;
            else ret += l;

            Console.Write(ret);

            void Update(int _val)
            {

                // seg에 val값 개수 추가
                int idx = b | _val;

                while (idx > 0)
                {

                    seg[idx]++;
                    idx >>= 1;
                }
            }

            int GetVal(int _val)
            {

                // _val 이상인 개수 찾기
                int l = b | _val;
                int r = b | n;

                int ret = 0;
                while (l < r)
                {

                    if ((l & 1) == 1) ret += seg[l++];
                    if ((r & 1) == 0) ret += seg[r--];

                    l >>= 1;
                    r >>= 1;
                }

                if (l == r) ret += seg[l];
                return ret;
            }

            long ReadLong()
            {

                long ret = 0;

                while (TryReadLong()) ;
                return ret;

                bool TryReadLong()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
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
                    if (c == ' ' || c == '\n') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
