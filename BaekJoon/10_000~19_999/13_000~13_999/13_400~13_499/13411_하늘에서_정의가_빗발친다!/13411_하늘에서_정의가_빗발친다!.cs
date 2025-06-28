using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 18
이름 : 배성훈
내용 : 하늘에서 정의가 빗발친다!
    문제번호 : 13411번

    수학, 기하학, 피타고라스 정리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1553
    {

        static void Main1553(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();

            (int idx, double time)[] arr = new (int idx, double time)[n];

            for (int i = 0; i < n; i++)
            {

                int x = ReadInt();
                int y = ReadInt();

                int v = ReadInt();

                // a^2 의 대소 관계나 |a| 의 대소관계나 같다.
                int up = x * x + y * y;
                double time = 1.0 * up / (v * v);

                arr[i] = (i + 1, time);
            }

            Array.Sort(arr, (x, y) => 
            {

                int ret = x.time.CompareTo(y.time);
                if (ret == 0) ret = x.idx.CompareTo(y.idx);
                return ret;
            });

            for (int i = 0; i < n; i++)
            {

                sw.Write($"{arr[i].idx}\n");
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ' || c == -1) return true;
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;

                    return false;
                }
            }
        }
    }
}
