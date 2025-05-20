using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 19
이름 : 배성훈
내용 : 양 구출 작전
    문제번호 : 16437번

    트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1634
    {

        static void Main1634(string[] args)
        {

            int n;
            List<int>[] edge;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Console.Write(DFS());
                long DFS(int _cur = 1, int _prev = 0)
                {

                    long ret = arr[_cur];
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];

                        ret += DFS(next, _cur);
                    }

                    ret = ret < 0 ? 0 : ret;
                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n + 1];
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int to = 2; to <= n; to++)
                {

                    int type = ReadType();
                    int num = ReadInt();
                    int from = ReadInt();

                    if (type == 1) arr[to] = num;
                    else arr[to] = -num;
                    edge[from].Add(to);
                }

                int ReadType()
                {

                    int ret = 0;

                    while (TryReadType()) ;
                    return ret;

                    bool TryReadType()
                    {

                        int c = sr.Read();

                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c == 'W' ? 2 : (c == 'S' ? 1 : 0);
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
