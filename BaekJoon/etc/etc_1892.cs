using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : CTP 왕국은 한솔 왕국을 이길 수 있을까?
    문제번호 : 15789번

    그리디, 분리집합 문제다.
    유니온 파인드 알고리즘으로 초기 동맹국을 관리한다.
    그리디 알고리즘으로 가장 큰 동맹을 가진 사람부터 동맹을 맺을 수 있는 만큼 맺는 것이 그리디로 최대임이 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1892
    {
        static void Main1892(string[] args)
        {

            int[] group, cnt, stk;
            int n, m, c, h, k;

            Input();

            GetRet();

            void Union(int f, int t)
            {

                int gF = Find(f);
                int gT = Find(t);
                if (gF == gT) return;

                int min = gF < gT ? gF : gT;
                int max = gF < gT ? gT : gF;

                group[max] = min;
                cnt[min] += cnt[max];
                cnt[max] = 0;
            }

            int Find(int chk)
            {

                int len = 0;
                while (chk != group[chk])
                {

                    stk[len++] = chk;
                    chk = group[chk];
                }

                while (len-- > 0)
                {

                    group[stk[len]] = chk;
                }

                return chk;
            }

            void GetRet()
            {

                bool[] chk = new bool[n + 1];
                PriorityQueue<int, int> pq = new();
                for (int i = 1; i <= n; i++)
                {

                    int idx = Find(i);
                    if (chk[idx]) continue;
                    chk[idx] = true;

                    pq.Enqueue(idx, -cnt[idx]);
                }

                int popC = Find(c);
                int popH = Find(h);
                int ret = cnt[popC];

                while (k > 0 && pq.Count > 0)
                {

                    int node = pq.Dequeue();
                    if (node == popC || node == popH) continue;
                    k--;
                    ret += cnt[node];
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);


                n = ReadInt();
                m = ReadInt();

                group = new int[n + 1];
                cnt = new int[n + 1];
                stk = new int[n];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                    cnt[i] = 1;
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    Union(f, t);
                }

                c = ReadInt();
                h = ReadInt();
                k = ReadInt();

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
