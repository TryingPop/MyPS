using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 16
이름 : 배성훈
내용 : 스카이라인
    문제번호 : 1933번

    스위핑, 우선순위 큐 문제다.
    아이디어는 다음과 같다.
    x를 정렬해 높이 변화를 한다.
    그리고 x가 변할 때 높이가 변했는지 확인하고
    높이가 변했다면 출력하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1193
    {

        static void Main1193(string[] args)
        {

            PriorityQueue<int, int> maxHeight, popHeight;
            PriorityQueue<(int x, int height, bool use), int> x;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                var comp = Comparer<int>.Create((x, y) => y.CompareTo(x));
                maxHeight = new(n + 1, comp);
                popHeight = new(n, comp);

                int curX = 0;   // 현재 x
                // 이전 높이, 현재 높이
                long bHeight = 0, cHeight = 0;
                maxHeight.Enqueue(0, 0);

                while (x.Count > 0)
                {

                    var node = x.Dequeue();

                    // x좌표 변했는지 확인
                    if (curX != node.x && curX > 0)
                    {

                        // 높이가 다른 경우만 출력
                        if (bHeight != cHeight) sw.Write($"{curX} {cHeight} ");
                        // 이전 높이 갱신
                        bHeight = cHeight;
                    }

                    // 높이 빼는건지 넣는건지 확인
                    if (node.use) maxHeight.Enqueue(node.height, node.height);
                    else popHeight.Enqueue(node.height, node.height);

                    // x와 현재 높이 최신화
                    curX = node.x;
                    cHeight = GetMaxHeight();
                }

                sw.Write($"{curX} {cHeight}");

                sw.Close();

                int GetMaxHeight()
                {

                    while (maxHeight.Count > 0 
                        && popHeight.Count > 0
                        && maxHeight.Peek() == popHeight.Peek())
                    {

                        maxHeight.Dequeue();
                        popHeight.Dequeue();
                    }

                    return maxHeight.Peek();
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                x = new(n << 1);

                for (int i = 0; i < n; i++)
                {

                    int l = ReadInt();
                    int h = ReadInt();
                    int r = ReadInt();

                    x.Enqueue((l, h, true), l);
                    x.Enqueue((r, h, false), r);
                }

                sr.Close();
                int ReadInt()
                {
                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
