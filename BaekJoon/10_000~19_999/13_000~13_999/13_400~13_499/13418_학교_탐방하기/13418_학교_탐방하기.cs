using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 28
이름 : 배성훈
내용 : 학교 탐방하기
    문제번호 : 13418번

    최소 스패닝 트리 문제다.
    오르막길은 시작 끝지점 경사로 판별하는데 
    주어지는 간선은 무방향이라 한참을 고민했다;

    질문게시판 보니 같은 고민을 한 사람이 있었고,
    험한길, 쉬운길로 해석하는게 좋다 했다.

    그리고 간선의 갯수가 m개인데 예제를 보면 m + 1개가 온다.
    ... 입구 0 - 1은 세지 않은거 같다.

    문제 접근 자체는 쉽다.
    MST로 접근하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1735
    {

        static void Main1735(string[] args)
        {

            int n, m;
            (int s, int e, int type)[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(edge, (x, y) => x.type.CompareTo(y.type));

                int[] group = new int[n + 1];
                int[] stk = new int[n];

                Init();

                int cnt = 0;
                for (int i = 0; i <= m; i++)
                {

                    if (Union(edge[i].s, edge[i].e) && edge[i].type == 0) cnt++;
                }

                int ret = cnt * cnt;

                Init();

                cnt = 0;
                for (int i = m; i >= 0; i--)
                {

                    if (Union(edge[i].s, edge[i].e) && edge[i].type == 0) cnt++;
                }

                ret -= cnt * cnt;
                Console.Write(ret);

                void Init()
                {

                    for (int i = 0; i <= n; i++)
                    {

                        group[i] = i;
                    }
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while (_chk != group[_chk])
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }

                bool Union(int _f, int _t)
                {

                    int f = Find(_f);
                    int t = Find(_t);

                    if (f == t) return false;

                    int min = f < t ? f : t;
                    int max = f < t ? t : f;

                    group[max] = min;
                    return true;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                edge = new (int s, int e, int type)[m + 1];

                for (int i = 0; i <= m; i++)
                {

                    edge[i] = (ReadInt(), ReadInt(), ReadInt());
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
