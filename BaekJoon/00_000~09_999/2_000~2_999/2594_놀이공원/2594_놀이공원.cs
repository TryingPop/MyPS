using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 놀이공원
    문제번호 : 2594번

    스위핑 알고리즘을 이용해서 풀었다
    주된 아이디어는 다음과 같다

    시작 시간이 작은 것을 우선으로 정렬하고 같을 시 종료 시간이 큰 것을 차선으로 했다
    그리고 오픈부터 시작시간까지 휴식시간을 먼저 찾고, 이후 중앙에 쉬는 시간을 찾았다

    중앙에 쉬는 시간은 종료 시간을 기록해서 갱신되는 순간
    시작 시간이 갱신 전 종료 시간보다 빠른 경우 휴식시간 계산하고
    아니면 종료 시간 증가만 시켰다

    그리고 마지막에 끝 시간과 놀이공원 문닫는 시간을 마지막으로 조사한다
    이렇게 해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0116
    {

        static void Main116(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            Ride[] rides = new Ride[len];

            for (int i = 0; i < len; i++)
            {

                int startTime = ReadInt(sr);
                int endTime = ReadInt(sr);
                
                rides[i].Set(TimeToInt(startTime), TimeToInt(endTime));
            }

            sr.Close();

            Array.Sort(rides);

            int ret = rides[0].startTime - TimeToInt(1000) - 10;
            ret = ret < 0 ? 0 : ret;
            int chkTime = rides[0].endTime;

            for (int i = 1; i < len; i++)
            {

                if (rides[i].endTime > chkTime)
                {

                    if (rides[i].startTime > chkTime) 
                    {

                        int chk = rides[i].startTime - 20 - chkTime;
                        if (chk > ret) ret = chk;
                    }

                    chkTime = rides[i].endTime;
                }
            }

            int quitTime = TimeToInt(2200);
            int calc = quitTime - chkTime - 10;
            if (calc > ret) ret = calc;

            Console.WriteLine(ret);
        }

        static int TimeToInt(int _time)
        {

            int hour = _time / 100;
            int minute = _time % 100;

            return hour * 60 + minute;
        }

        struct Ride : IComparable<Ride>
        {

            public int startTime;
            public int endTime;

            public int CompareTo(Ride other)
            {

                int ret = startTime.CompareTo(other.startTime);
                if (ret == 0) other.endTime.CompareTo(endTime);

                return ret;
            }

            public void Set(int _startTime, int _endTime) 
            { 
            
                startTime = _startTime;
                endTime = _endTime;
            }
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
    }
}
