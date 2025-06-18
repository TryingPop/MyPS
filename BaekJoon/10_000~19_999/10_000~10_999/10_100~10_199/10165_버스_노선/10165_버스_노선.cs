using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 8
이름 : 배성훈
내용 : 버스 노선
    문제번호 : 10165번

    정렬, 그리디, 스위핑 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1528
    {

        static void Main1528(string[] args)
        {

            int n, m;
            (int s, int e, int idx)[] arr1, arr2;
            int len1, len2;

            Input();

            GetRet();

            void GetRet()
            {

                Comparer<(int s, int e, int idx)> comp = Comparer<(int s, int e, int idx)>.Create((x, y) =>
                {

                    int ret = x.s.CompareTo(y.s);
                    if (ret == 0) ret = y.e.CompareTo(x.e);
                    return ret;
                });

                Array.Sort(arr1, 0, len1, comp);
                Array.Sort(arr2, 0, len2, comp);

                bool[] isContains = new bool[m + 1];
                int end = 0;
                // 먼저 n - 1, 0을 지나지 않는 경우
                for (int i = 0; i < len1; i++)
                {

                    int cur = arr1[i].e;

                    // 그리디로 끝값만 작은지 판별하면 된다.
                    if (cur <= end) isContains[arr1[i].idx] = true;
                    else end = cur;
                }

                end = -1;
                // n - 1, 0을 지나는 경우
                for (int i = 0; i < len2; i++)
                {

                    int cur = arr2[i].e;

                    // 끝값이 작으면 포함된다.
                    if (cur <= end) isContains[arr2[i].idx] = true;
                    else end = cur;
                }

                if (len2 > 0)
                {

                    for (int i = 0; i < len1; i++)
                    {

                        // n - 1, 0에 포함되는 간선에 포함될 수 있는지 확인한다.
                        // 이미 포함된 간선이면 넘긴다.
                        if (isContains[arr1[i].idx]
                        // arr1의 시작지점이 arr2의 가장 작은 시작지점보다 크거나 같은 경우
                        // arr2의 경우 n - 1, 0을 지나므로 arr1의 끝지점이 어떻게 되든 해당 arr2[0]에 포함된다.
                        // 마찬가지로 끝지점이 작으면 포함된다.
                            || (arr1[i].s < arr2[0].s && end < arr1[i].e)) continue;
                        isContains[arr1[i].idx] = true;
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= m; i++)
                {

                    if (isContains[i]) continue;
                    sw.Write(i);
                    sw.Write(' ');
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr1 = new (int s, int e, int idx)[m];
                arr2 = new (int s, int e, int idx)[m];
                len1 = 0;
                len2 = 0;
                for (int i = 0; i < m; i++)
                {

                    int s = ReadInt();
                    int e = ReadInt();

                    // n - 1에서 0을 지나지 않는다.
                    if (s < e) arr1[len1++] = (s, e, i + 1);
                    // n - 1에서 0을 지나는 경우
                    else arr2[len2++] = (s, e, i + 1);
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
}
