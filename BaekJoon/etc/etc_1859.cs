using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 2
이름 : 배성훈
내용 : k개 사과 트리 노드만으로 배를 최대로 수확하기
    문제번호 : 25977번

    비트마스킹, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1859
    {

        static void Main1859(string[] args)
        {

            int n, k;
            int apple, pear;
            int[] parent;

            Input();

            GetRet();

            void GetRet()
            {

                bool[] visit = new bool[1 << n];

                int root = -1;

                for (int i = 0; i < n; i++)
                {

                    if (parent[i] != -1) continue;
                    root = i;
                    break;
                }

                int ret = 0;
                DFS(GetAdd(root, apple), GetAdd(root, pear), 1 << root);

                Console.Write(ret);

                void DFS(int _apple, int _pear, int _state)
                {

                    if (visit[_state] || _apple > k) return;
                    visit[_state] = true;

                    ret = Math.Max(ret, _pear);

                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) != 0
                            || (_state & (1 << parent[i])) == 0) continue;

                        int add1 = GetAdd(i, apple);
                        int add2 = GetAdd(i, pear);

                        DFS(_apple + add1, _pear + add2, _state | (1 << i));
                    }
                }

                int GetAdd(int _idx, int _fruit)
                    => ((1 << _idx) & _fruit) == 0 ? 0 : 1;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                parent = new int[n];
                Array.Fill(parent, -1);
                for (int i = 1; i < n; i++)
                {

                    int p = ReadInt();
                    int c = ReadInt();
                    parent[c] = p;
                }

                apple = 0;
                pear = 0;

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    if (cur == 1) apple |= 1 << i;
                    else if (cur == 2) pear |= 1 << i;
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
