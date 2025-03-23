using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 내일 할거야
    문제번호 : 7983번

    문제를 잘못읽어 1번 틀리고,
    etc_0216을 그대로 써서 1번 틀렸다

    앞에서는 100만의 값이 최대였던 반면 
    여기서는 10억 이상의 값이 올 수 있다
    그래서 ret = 1_000_001에서 에러를 일으켰고

    다음으로 잘못해석했나 싶어 
    중간에 최대로 쉴 수 있는 시간을 구했었다;
    그런데 중간에 최대로 쉬는 것은 현재 코드로 못찾는 방법이다;
    그래서 다시 읽어보니, 문제를 etc_0216과 같음을 다시 상기했고
    초기값을 다르게 설정해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0217
    {

        static void Main217(string[] args)
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

            int ret = work[0].finishTime;

            for (int i = 0; i < n; i++)
            {

                if (ret > work[i].finishTime) ret = work[i].finishTime;
                ret -= work[i].workTime;
            }

            Console.WriteLine(ret);
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

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
