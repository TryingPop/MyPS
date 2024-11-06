using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 1
이름 : 배성훈
내용 : 교집합 만들기
    문제번호 : 25393번

    애드혹, 자료구조 문제다
    그리디로 접근했다
    각 왼쪽끝에 대해 가장 긴 오른쪽 끝(최대)을 기입했다
    그리고 각 오른쪽 끝에 가장 진 왼쪽 끝(최소)을 기입했다
    그리고 해당 구간에 포함되면 교집합으로 만들 수 있다

    그런데, 확장해나가는 형식이라
    (2, 4) 가 있을 때,
    (1, 4), (2, 5)가 기입되면
    2는 최대 5, 4는 최소 1을 기록한다

    그런데, 2, 4 직선이 존재해 1개여야 하는데,
    최대 최소로만 비교해 2개를 내놓아 많이 틀렸다;

    이후에 해시 셋을 이용해 수정하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0857
    {

        static void Main857(string[] args)
        {

            string NO = "-1\n";
            string ONE = "1\n";
            string TWO = "2\n";

            StreamReader sr;
            StreamWriter sw;

            int[] l, r;
            HashSet<(int l, int r)> set;

            Solve();
            void Solve()
            {

                Init();

                SetArr();

                GetRet();

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    if (set.Contains((f, t))) sw.Write(ONE);
                    else if (t <= l[f] && r[t] <= f) sw.Write(TWO);
                    else sw.Write(NO);
                }
            }

            void SetArr()
            {

                int len = ReadInt();
                set = new(len);

                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    l[f] = Math.Max(l[f], t);
                    r[t] = Math.Min(r[t], f);
                    set.Add((f, t));
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                l = new int[1_000_001];
                r = new int[1_000_001];

                Array.Fill(l, -1);
                Array.Fill(r, 1_000_001);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
