using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 등급 계산하기
    문제번호 : 25374

    수학, 구현, 정렬, 사칙연산 문제다
    정렬한 뒤 하나씩 읽으면서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0637
    {

        static void Main637(string[] args)
        {

            StreamReader sr;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                ReadInt();
                int[] arr = new int[100];

                for (int i = 0; i < 100; i++)
                {

                    arr[i] = -ReadInt();
                }
                sr.Close();

                Array.Sort(arr);
                int[] ret = new int[9];
                int[] cnt = new int[9] { 4, 11, 23, 40, 60, 77, 89, 96, 100 };

                int before = -101;
                int g = -1;
                for (int i = 0; i < 100; i++)
                {

                    if (arr[i] != before)
                    {

                        // 범위가 100이라, 이전 점수가 다르면 매번 등급을 확인하며 간다
                        for (int j = 0; j < 9; j++)
                        {

                            if (i >= cnt[j]) continue;
                            g = j;
                            break;
                        }

                        before = arr[i];
                    }

                    ret[g]++;
                }

                for(int i = 0; i < 9; i++)
                {

                    Console.WriteLine(ret[i]);
                }
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

#if other
// cs25374 - rby
// 2023-03-07 오후 11:13:27
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs25374
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int[] score = new int[101];
            foreach (var item in line)
                score[item]++;

            int[] per = new int[] { 4, 11, 23, 40, 60, 77, 89, 96, 100 };
            int cur = 0;

            int[] people = new int[10];
            int count = 0;

            for (int i = 100; i >= 0; i--)
            {
                count += score[i];

                while (cur < 9 && count >= per[cur])
                    people[++cur] = count;

                if (cur == 9)
                    break;
            }

            for (int i = 1; i <= 9; i++)
                sw.WriteLine(people[i] - people[i - 1]);

            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
