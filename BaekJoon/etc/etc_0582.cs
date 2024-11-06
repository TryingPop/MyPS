using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 소가 길을 건너간 이유 3
    문제번호 : 14469번

    그리디, 정렬 문제다
    아이디어는 다음과 같다
    소가 온시간에 따라 정렬한다
    그리고 검문을 시작한다

    검문이 끝나고 나서 오면 소가 온시간 + 검문 시간으로 갱신하고
    검문 중에 오면 해당 소보고 대기하라하고 검문 시간을 추가한다

    이렇게 나오는 결과가 검문이 끝나는 최소 시간이다
*/

namespace BaekJoon.etc
{
    internal class etc_0582
    {

        static void Main582(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);

            int n;
            (int s, int t)[] cow;

            Solve();
            sr.Close();

            void Solve()
            {

                Input();
                Array.Sort(cow, (x, y) => x.s.CompareTo(y.s));

                int ret = -1;
                for (int i = 0; i < n; i++)
                {

                    if (ret <= cow[i].s) ret = cow[i].s + cow[i].t;
                    else ret += cow[i].t;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                n = ReadInt();
                cow = new (int s, int t)[n];

                for (int i = 0; i < n; i++)
                {

                    cow[i] = (ReadInt(), ReadInt());
                }
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
        }
    }
#if other
int t = int.Parse(Console.ReadLine()), R;
int[] st = new int[t], end = new int[t];
for (int i = 0; i < t; i++)
{
    string[] str = Console.ReadLine().Split(' ');
    st[i] = int.Parse(str[0]); end[i] = int.Parse(str[1]);
} 
Array.Sort(st, end);
R = st[0] + end[0];
for (int i = 1; i < t; i++)
    if (R >= st[i])
        R += end[i];
    else
        R = st[i] + end[i];
Console.Write(R);
#endif
}
