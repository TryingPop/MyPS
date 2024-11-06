using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 교수님의 기말고사
    문제번호 : 20126번

    정렬 문제다
    정렬된 데이터인줄 알고 접근했다가 한번 틀렸다
    정렬된게 아니지만 그렇다고 망가진 데이터가 들어오는 경우는 없었다!
    (시간이 겹치는 데이터!)

    그리디하게 접근해서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0502
    {

        static void Main502(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            int n = ReadInt();
            int m = ReadInt();
            int e = ReadInt();


            (int s, int u)[] time = new (int s, int u)[n];

            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                int use = ReadInt();

                time[i] = (cur, use);
            }
            sr.Close();

            Array.Sort(time, (x, y) => x.s.CompareTo(y.s));

            int before = 0;
            int ret = -1;

            for (int i = 0; i < n; i++)
            {

                int cur = time[i].s;
                if (cur - before >= m)
                {

                    ret = before;
                    break;
                }

                before = time[i].s + time[i].u;
            }

            if (e - before >= m) ret = before;
            Console.WriteLine(ret);
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
var reader = new Reader();
var (n, m, s) = (reader.NextInt(), reader.NextInt(), reader.NextInt());

var schedules = new (int start, int end)[n + 2];
schedules[0] = (0, 0);
for (int i = 1; i <= n; i++)
{
    var (x, y) = (reader.NextInt(), reader.NextInt());
    schedules[i] = (x, x + y);
}
schedules[n + 1] = (s, s);
schedules = schedules.OrderBy(x => x.start).ToArray();

int start = -1;
for (int i = 0; i < n + 1; i++)
{
    var (cs, ce) = schedules[i];
    var (ns, ne) = schedules[i + 1];
    
    // Console.WriteLine($"Start: {ce}  End: {ns}  Interval: {ns - ce}");
    if (ns - ce >= m)
    {
        start = ce;
        break;
    }
}

Console.Write(start);

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}
