using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 11
이름 : 배성훈
내용 : AAAAHH! Overbooked!
    문제번호 : 7637번

    정렬, 파싱 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1624
    {

        static void Main1624(string[] args)
        {

            string Y = "conflict\n";
            string N = "no conflict\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            (int s, int e)[] time = new (int s, int e)[100];
            Comparer<(int s, int e)> cmp = Comparer<(int s, int e)>.Create((x, y) => x.s.CompareTo(y.s));

            while ((n = int.Parse(sr.ReadLine())) > 0)
            {

                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split('-');
                    time[i] = (TimeToInt(temp[0]), TimeToInt(temp[1]));
                }

                Array.Sort(time, 0, n, cmp);

                int e = -1;
                bool ret = false;
                // 겹치는 시간이 있다면 시작 시간이 끝시간보다 늦은 경우가 있는 경우다!
                for (int i = 0; i < n; i++)
                {

                    if (time[i].s < e)
                    {

                        ret = true;
                        break;
                    }
                    else e = time[i].e;
                }

                sw.Write(ret ? Y : N);
            }

            int TimeToInt(string _time)
            {

                int h = (_time[0] - '0') * 10 + _time[1] - '0';
                int m = (_time[3] - '0') * 10 + _time[4] - '0';

                return h * 60 + m;
            }
        }
    }
}
