using System;
using System.IO;
using System.Collections.Generic;

/*
날짜 : 2025. 5. 30
이름 : 배성훈
내용 : 서로 다른 수와 쿼리 1
    문제번호 : 14897번

    현재 시간초과난다.
*/
namespace BaekJoon.etc
{
    internal class etc_1656
    {

        static void Main1656(string[] args)
        {

            // 시간초과
            int n;
            int[] arr, ret;
            int q;
            (int s, int e, int idx)[] queries;
            Dictionary<int, int> dic;
            Input();

            GetRet();

            Output();

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
            }

            void GetRet()
            {

                int sqrt = (int)(Math.Sqrt(n + 1e-9));
                Array.Sort(queries, (x, y) =>
                {

                    int ret = (x.s / sqrt).CompareTo(y.s / sqrt);
                    if (ret == 0) ret = x.e.CompareTo(y.e);
                    return ret;
                });

                ret = new int[q];
                

                int s = queries[0].s;
                int e = queries[0].e;
                int cnt = 0;

                for (int i = s; i <= e; i++)
                {

                    if (dic[arr[i]] == 0) cnt++;
                    dic[arr[i]]++;
                }

                ret[queries[0].idx] = cnt;
                for (int i = 1; i < q; i++)
                {

                    MoveStart(queries[i].s);
                    MoveEnd(queries[i].e);

                    ret[queries[i].idx] = cnt;
                }

                void MoveEnd(int _dst)
                {

                    while (e < _dst)
                    {

                        if (dic[arr[++e]]++ == 0) cnt++;
                    }

                    while (e > _dst)
                    {

                        if (--dic[arr[e--]] == 0) cnt--;
                    }
                }

                void MoveStart(int _dst)
                {

                    while (s < _dst)
                    {

                        if (--dic[arr[s++]] == 0) cnt--;

                    }

                    while (s > _dst)
                    {

                        if (dic[arr[--s]]++ == 0) cnt++;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                dic = new(n);

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    dic[arr[i]] = 0;
                }

                q = ReadInt();
                queries = new (int s, int e, int idx)[q];
                for (int i = 0; i < q; i++)
                {

                    queries[i] = (ReadInt() - 1, ReadInt() - 1, i);
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
