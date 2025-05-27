using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 전생했더니 슬라임 연구자였던 건에 대하여 (Hard)
    문제번호 : 14698번

    그리디, 우선순위 큐를 이용한 문제이다
    작은 값들을 먼저 합쳐가면서 곱하는게 가장 싼 방법이다!
    

    입력 범위가 int까지인줄 알아서 여러 번 틀렸다
    이후 long 범위까지 올 수 있음을 알고 수정하니 이상없이 통과했다

    시간은 320ms 걸렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0178
    {

        static void Main178(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            PriorityQueue<long, long> q = new PriorityQueue<long, long>(60);

            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int len = ReadInt(sr);

                for (int i = 0; i < len; i++)
                {

                    long num = ReadLong(sr);
                    q.Enqueue(num, num);
                }

                long ret = 1;
                while(q.Count > 1)
                {

                    long f = q.Dequeue();
                    long b = q.Dequeue();

                    long calc = f * b;
                    q.Enqueue(calc, calc);

                    ret *= calc % 1_000_000_007;
                    ret %= 1_000_000_007;
                }

                sw.WriteLine(ret);
                q.Clear();
            }

            sw.Close();
            sr.Close();
        }

        static long ReadLong(StreamReader _sr)
        {

            int c;
            long ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
