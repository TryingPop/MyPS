using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 7
이름 : 배성훈
내용 : 달려라 홍준
    문제번호 : 1306번

    자료 구조, 슬라이딩 윈도우, 세그먼트 트리 문제다.
    구간 내의 최대 값을 구해야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1382
    {

        static void Main1382(string[] args)
        {

            int n, m;
            int[] arr;
            
            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                (int cnt, int max)[] seg = new (int cnt, int max)[(1 << 21) + 1];
                int init = 1 << 20;

                int e = 2 * m - 1;
                int s = 0;
                for (int i = s; i < e; i++)
                {

                    Update(arr[i], 1);
                }

                sw.Write($"{seg[1].max} ");

                for (; e < n; e++, s++)
                {

                    Update(arr[e], 1);
                    Update(arr[s], -1);

                    sw.Write($"{seg[1].max} ");
                }

                void Update(int _val, int _add)
                {

                    int idx = init | _val;
                    seg[idx].cnt += _add;
                    if (seg[idx].cnt > 0) seg[idx].max = _val;
                    else if (seg[idx].cnt == 0) seg[idx].max = 0;

                    while (idx > 1)
                    {

                        int other = idx ^ 1;
                        int parent = idx >> 1;

                        seg[parent].max = Math.Max(seg[idx].max, seg[other].max);
                        idx = parent;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

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
}
