using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 7
이름 : 배성훈
내용 : 조용히 완전히 영원히
    문제번호 : 30512번

    느리게 갱신되는 세그먼트 트리 문제다.
    최솟값 찾는 것은 단순히 세그먼트 트리를 이용해서 풀 수 있다.
    다만 잊혀진 수는 각 수에 몇 번째 쿼리에 갱신되었는지를 함께 기록해야 한다.
    그래서 느리게 갱신되는 세그먼트 트리로 자료 구조를 설정했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1873
    {

        static void Main1873(string[] args)
        {

            // 30512번 - 조용히 완전히 영원히
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int len = 1 << 18;
            int[] seg = new int[len + 1];
            int[] lazy = new int[len + 1];
            int[] query = new int[len + 1];
            int[] change = new int[len + 1];

            for (int chk = 1; chk <= n; chk++)
            {

                int val = ReadInt();
                Update(1, n, chk, val);
            }

            int q = ReadInt();
            int[] ret = new int[q + 1];

            for (int i = 1; i <= q; i++)
            {

                int chkS = ReadInt();
                int chkE = ReadInt();
                int val = ReadInt();
                LazyUpdate(1, n, chkS, chkE, val, i);
            }

            for (int i = 1; i <= n; i++)
            {

                sw.Write(GetVal(1, n, i));
                if (i < n) sw.Write(' ');
                else sw.Write('\n');
            }

            for (int i = 1; i <= q; i++)
            {

                ret[i] += ret[i - 1];
                sw.Write(ret[i]);
                sw.Write(' ');
            }

            int GetVal(int s, int e, int chk, int idx = 0)
            {

                ChkLazy(s, e, idx);

                if (s == e)
                {

                    ret[change[idx]]++;
                    return seg[idx]; 
                }

                int mid = (s + e) >> 1;
                if (mid < chk) return GetVal(mid + 1, e, chk, idx * 2 + 2);
                else return GetVal(s, mid, chk, idx * 2 + 1);
            }

            void LazyUpdate(int s, int e, int chkS, int chkE, int val, int q, int idx = 0)
            {

                ChkLazy(s, e, idx);
                if (chkS <= s && e <= chkE)
                {

                    if (s == e) SetSeg(idx, val, q);
                    else
                    {

                        SetLazy(idx * 2 + 1, val, q);
                        SetLazy(idx * 2 + 2, val, q);
                    }
                    return;
                }
                else if (e < chkS || chkE < s) return;

                int mid = (s + e) >> 1;
                LazyUpdate(s, mid, chkS, chkE, val, q, idx * 2 + 1);
                LazyUpdate(mid + 1, e, chkS, chkE, val, q, idx * 2 + 2);
            }

            void ChkLazy(int s, int e, int idx)
            {

                int q = query[idx];
                query[idx] = 0;
                if (q == 0) return;

                int val = lazy[idx];

                if (s == e) SetSeg(idx, val, q);
                else
                {

                    SetLazy(idx * 2 + 1, val, q);
                    SetLazy(idx * 2 + 2, val, q);
                }
            }

            void SetLazy(int idx, int val, int q)
            {

                if (query[idx] > 0 && lazy[idx] <= val) return;

                query[idx] = q;
                lazy[idx] = val;
            }

            void SetSeg(int idx, int val, int q)
            {

                if (seg[idx] <= val) return;
                seg[idx] = val;
                change[idx] = q;
            }

            void Update(int s, int e, int chk, int val, int idx = 0)
            {

                if (s == e)
                {

                    seg[idx] = val;
                    return;
                }

                int mid = (s + e) >> 1;
                if (mid < chk) Update(mid + 1, e, chk, val, idx * 2 + 2);
                else Update(s, mid, chk, val, idx * 2 + 1);
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
