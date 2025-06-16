using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 시간 관리하기
    문제번호 : 6068번, 1263번

    그리디, 정렬 문제다
    일의 시작시간을 찾아야한다

    먼저 시간을 내림차순으로 정렬한 뒤,
    그러면 맨 앞에는 마지막에 해야하는 일 A가 있다

    마지막 일 A를 끝내야하는 시간에서
    일에 필요한 시간을 빼면 마지막 일을 시작해야하는 가장 늦은 시간이 된다

    그리고 해당 시간을 i라고 기록한 뒤 마지막 일 A를 제거한다
    이제 남은 일 중에서 다시 마지막 일 B를 찾는다

    B의 끝내야하는 시간이 i보다 늦은 경우(i가 작은 경우) i에서 B의 일에 필요한 시간만큼
    빼면 일을 시작해야하는 가장 늦은 시간이 된다

    반면 B의 끝내야하는 시간이 i보다 빠른 경우(i가 큰 경우) B의 끝내야하는 시간에서
    B의 일에 필요한 시간을 뺀 시간이 일을 시작해야하는 가장 늦은 시간이 된다

    이러한 과정을 반복하며 모든 일을 끝낼 경우,
    내림차순으로 정렬되었기에 가장 늦게 일을 시작해야하는 시간이 보장된다

    아래는 해당 과정을 코드로 작성한거 뿐이다
*/

namespace BaekJoon.etc
{
    internal class etc_0216
    {

        static void Main216(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            Work[] work = new Work[n];

            for (int i = 0; i < n; i++)
            {

                work[i].Set(ReadInt(sr), ReadInt(sr));
            }

            sr.Close();

            Array.Sort(work);

            int ret = 1_000_001;

            for (int i = 0; i < n; i++)
            {

                if (ret > work[i].finishTime)
                {

                    ret = work[i].finishTime;
                }

                ret -= work[i].workTime;
            }

            if (ret < 0) Console.WriteLine(-1);
            else Console.WriteLine(ret);
        }

        struct Work : IComparable<Work>
        {

            public int workTime;
            public int finishTime;

            public void Set(int _workTime, int _finishTime)
            {

                workTime = _workTime;
                finishTime = _finishTime;
            }
            public int CompareTo(Work other)
            {

                return other.finishTime.CompareTo(finishTime);
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c!= ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
