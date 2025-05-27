using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 크리스마스 선물
    문제번호 : 14235번

    우선 순위 큐에 원하는 정렬방법을 적용한 문제이다

    1%부터 입력에 공백이 있다 그래서 Trim메서드로 앞 뒤 공백을 제거해줘야 통과할 수 있다
    이외는 따로 신경쓸거 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0118
    {

        static void Main118(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int len = int.Parse(sr.ReadLine());
            
            // 내림 차순
            PriorityQueue<int, int> q = new PriorityQueue<int, int>(len, Comparer<int>.Create((x, y) => y.CompareTo(x)));

            for (int i = 0; i < len; i++)
            {

                string str = sr.ReadLine().Trim();
                int[] input = str.Split(' ').Select(int.Parse).ToArray();

                if (input[0] == 0)
                {

                    if (q.Count > 0) sw.WriteLine(q.Dequeue());
                    else sw.WriteLine(-1);
                    continue;
                }

                for (int j = 0; j < input[0]; j++)
                {

                    int add = input[j + 1];
                    q.Enqueue(add, add);
                }
            }

            sr.Close();
            sw.Close();
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
