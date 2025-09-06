using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 5
이름 : 배성훈
내용 : Допрос подозреваемых
    문제번호 : 28545번

    그리디, 정렬 문제다.
    지루함 정도의 임계치를 넘기지 않게 진행하는데,
    모든 용의자를 조사했을 때 넘긴 임계치의 최소 개수를 찾아야 한다.
    이는 가장 작은 지루함 정도를 가진 사람으로 진행하면 최소가 보장된다.
    다만 모든 사람이 양수인 경우 임의 순서로 진행해도 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1866
    {

        static void Main1866(string[] args)
        {

            // 28545번 - 통과 완료
            // 값이 작은거부터 더하는게 최소임을 보장!
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();

            long sum = 0;
            int[] arr = new int[n];
            int[] order = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
                order[i] = i;
                sum += arr[i];
            }

            int m = ReadInt();
            int ret = 0;
            for (int i = 0; i < m; i++)
            {

                if (ReadInt() <= sum) ret++;
            }

            Array.Sort(order, (x, y) => arr[x].CompareTo(arr[y]));
            sw.Write(ret);
            sw.Write('\n');

            for (int i = 0; i < n; i++)
            {

                sw.Write($"{order[i] + 1} ");
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
                    if (c == '\n' || c == ' ') return true;
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
