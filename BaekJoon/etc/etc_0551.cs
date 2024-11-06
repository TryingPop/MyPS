using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 회의실 배정
    문제번호 : 1931번

    그리디, 정렬 문제다
    앞에서 풀어봤던 문제다 13_02 <- 이게 etc_0548의 예제인거 같다
    etc_0548의 경우는 차이가 최소이고, 이후 시작 시간 순번으로 정렬하면 된다

    먼저 끝쪽을 기준으로 정렬한다
    그래야 현재 시간에 참석해서 가장 빠르게 끝나는 일을 할 수 있다
    여기서는 시작 = 끝인 경우도 있어 이후에 시작시간으로 정렬해줘야한다
    그래야 4 = 4에 끝나는 일들도 함께 진행할 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0551
    {

        static void Main551(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            (int s, int e)[] arr = new (int s, int e)[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = (ReadInt(), ReadInt());
            }

            sr.Close();

            Array.Sort(arr, (x, y) => 
            {

                int ret = x.e.CompareTo(y.e);
                if (ret != 0) return ret;

                return x.s.CompareTo(y.s);
            });

            int beforeE = -1;
            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                if (arr[i].s < beforeE) continue;
                ret++;
                beforeE = arr[i].e;
            }

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
}
