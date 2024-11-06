using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 구슬 (BEAD)
    문제번호 : 14930번

    정렬, 애드혹 문제다
    완전 탄성 충돌이기에
    부딪히면, 충돌한 공의 속도를 따라가게 된다

    그래서 공의 속도만 변할 뿐 배치 순서는 변하지 않는다
    공들의 위치는 서로 뚫고 지나가는 것과 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0638
    {

        static void Main638(string[] args)
        {

            StreamReader sr;
            long[] s;
            long[] v;
            int n;
            int t;

            int idx;

            Solve();

            void Solve()
            {

                Input();

                GetRedInitPos();

                TPos();

                Console.WriteLine(s[idx]);
            }

            void TPos()
            {

                for (int i = 0; i < n; i++)
                {

                    s[i] = s[i] + v[i] * t;
                }

                Array.Sort(s);
            }

            void GetRedInitPos()
            {

                idx = 0;
                for (int i = 1; i < n; i++)
                {

                    if (s[i] < s[0]) idx++;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                n = ReadInt();
                t = ReadInt();

                s = new long[n];
                v = new long[n];
                for (int i = 0; i < n; i++)
                {

                    s[i] = ReadLong();
                    v[i] = ReadLong();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

            long ReadLong()
            {

                int c;
                long ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if ( c== '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using StreamWriter wt = new(Console.OpenStandardOutput());
using StreamReader rd = new(Console.OpenStandardInput());
var input = rd.ReadLine().Split().Select(long.Parse).ToArray();
long n = input[0], t = input[1];
var l = new List<long>();
var ll = new List<(long, long)>(); 

for (int i = 0; i < n; i++)
{
    input = rd.ReadLine().Split().Select(long.Parse).ToArray();
    l.Add(input[0] + input[1] * t);
    ll.Add((input[0], input[1]));
}

var r = ll[0];
ll = ll.OrderBy(x => x.Item1).ToList();
int index = ll.IndexOf(r);
l.Sort();
wt.WriteLine(l[index]);
#endif
}
