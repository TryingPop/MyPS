using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 23
이름 : 배성훈
내용 : 차집합
    문제번호 : 1822번

    정렬, 자료 구조 문제다.
    초기에 원소를 모두 넣어두기에 HashSet을 이용해 저장했다.
    만약 확인하고 빼는게 쿼리로 있다면 SortedSet을 이용했을 것이다.
    그리고 마지막에 정렬순으로 출력해야 하는데,
    이는 Linq의 OrderBy메소드를 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1358
    {

        static void Main1358(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();
            HashSet<int> a = new(n);

            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                a.Add(cur);
            }

            for (int i = 0; i < m; i++)
            {

                int pop = ReadInt();
                if (a.Contains(pop)) a.Remove(pop);
            }

            sw.WriteLine(a.Count);
            foreach(int item in a.OrderBy(x => x))
            {

                sw.Write(item);
                sw.Write(' ');
            }

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
