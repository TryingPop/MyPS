using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 도미노 무너트리기
    문제번호 : 25972번

    그리디 알고리즘과 정렬 문제다
    먼저 도미노 자료구조를 만들고 x순으로 정렬했다
    그리고 조건대로 도미노를 쓰러뜨리면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_0270
    {

        static void Main270(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            Domino[] d = new Domino[len];

            for (int i = 0; i < len; i++)
            {

                int x = ReadInt(sr);
                int l = ReadInt(sr);
                d[i].Set(x, l);
            }

            sr.Close();
            Array.Sort(d);

            int ret = 1;
            for (int i = 1; i < len; i++)
            {

                if (d[i - 1].next >= d[i].x) continue;
                ret++;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }

        struct Domino : IComparable<Domino>
        {

            public int x;
            public int l;

            public int next;
            public int CompareTo(Domino other)
            {

                return x.CompareTo(other.x);
            }

            public void Set(int _x, int _l)
            {

                x = _x;
                l = _l;

                next = x + l;
            }
        }
    }
}
