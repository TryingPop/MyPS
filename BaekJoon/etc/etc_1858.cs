using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 2
이름 : 배성훈
내용 : k개 트리 노드에서 사과와 배를 최대로 수확하기
    문제번호 : 25976번

    비트마스킹, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1858
    {

        static void Main1858(string[] args)
        {

            int n, k;
            int apple, pear;
            int[] parent;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dp1 = new int[1 << n];
                int[] dp2 = new int[1 << n];

                Array.Fill(dp1, -1);
                Array.Fill(dp2, -1);

                int max = -1;
                int ret1 = -1;
                int ret2 = -1;

                int root = -1;
                for (int i = 0; i < n; i++)
                {

                    if (parent[i] != -1) continue;
                    root = i;
                    break;
                }

                DFS(1, 1 << root, GetAdd(root, apple), GetAdd(root, pear));

                Console.Write($"{ret1} {ret2}");

                void DFS(int _dep, int _state, int _cnt1, int _cnt2)
                {

                    if (dp1[_state] != -1) return;
                    dp1[_state] = _cnt1;
                    dp2[_state] = _cnt2;

                    if (_dep > k)
                    {

                        if (ChkMax())
                        {

                            ret1 = _cnt1;
                            ret2 = _cnt2;
                            max = ret1 * ret2;
                        }
                        return;
                    }

                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) != 0
                            || (_state & (1 << parent[i])) == 0) continue;

                        int nState = _state | (1 << i);
                        int add1 = GetAdd(i, apple);
                        int add2 = GetAdd(i, pear);

                        DFS(_dep + 1, nState, _cnt1 + add1, _cnt2 + add2);
                    }

                    bool ChkMax()
                    {

                        if (max < _cnt1 * _cnt2) return true;
                        else if (max == _cnt1 * _cnt2)
                        {

                            if (ret1 < _cnt1) return true;
                            else if (ret1 == _cnt1 && ret2 < _cnt2) return true;
                        }

                        return false;
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
