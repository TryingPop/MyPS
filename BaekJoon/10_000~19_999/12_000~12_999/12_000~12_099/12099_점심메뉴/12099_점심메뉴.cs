using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 23
이름 : 배성훈
내용 : 점심메뉴
    문제번호 : 12099번

    정렬, 이분 탐색 문제다.
    우선 매운맛과 단맛은 고유하다.
    즉 arr[i].a = arr[j].a 이거나 arr[i].b = arr[j].b인 경우 i = j이다!
    그리고 v ≤ u + 10_000이다.

    이는 u, v 구간에 속하는 원소는 b의 값 상관없이 많아야 1만개이다.
    그래서 arr을 매운맛의 오름차순으로 정렬하고 이분탐색으로 u, v에 속하는 인덱스 범위를 찾는다.
    이 범위는 1만 이하이다!

    그리고 해당 1만개의 원소에 대해 브루트포스로 x, y범위에 들어가는지 확인한다.
    이는 매운맛이 고유한 사실과 브루트포스, 이분탐색으로 풀었다.

    만약 고유하지 않다면 세그먼트 트리와 오프라인 쿼리를 이용할 것이다.
    사각형의 갯수를 세그먼트 트리에 기록한다.
    그리고 점이 어느 트리에 속하는지 확인할 것이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1726
    {

        static void Main1726(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt(), m = ReadInt();

            (int a, int b)[] food = new (int a, int b)[n];
            for (int i = 0; i < n; i++)
            {

                food[i] = (ReadInt(), ReadInt());
            }

            Array.Sort(food, (x, y) => x.a.CompareTo(y.a));

            for (int i = 0; i < m; i++)
            {

                Query();
            }

            void Query()
            {

                int u = ReadInt();
                int v = ReadInt();

                int x = ReadInt();
                int y = ReadInt();

                int left = GetLeft(u);
                int right = GetRight(v);

                int ret = 0;
                for (int i = left; i <= right; i++)
                {

                    if (food[i].b < x || y < food[i].b) continue;
                    ret++;
                }

                sw.Write($"{ret}\n");

                int GetLeft(int _u)
                {

                    int l = 0;
                    int r = n - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (food[mid].a < _u) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }

                int GetRight(int _v)
                {

                    int l = 0;
                    int r = n - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (food[mid].a <= _v) l = mid + 1;
                        else r = mid - 1;
                    }

                    return r;
                }
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
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n') 
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
