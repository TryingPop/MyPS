using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 9
이름 : 배성훈
내용 : 모두싸인 출근길
    문제번호 : 24229번

    그리디, 스위핑 문제다.
    먼저 합칠 수 있는 판자는 모두 합친다.
    정렬을 이용하면 그리디로 O(N)에 판자들을 합칠 수 있다.
    
    그리고 합쳐진 판자에 대해서 점프할 수 있는 최대 장소를 그리디로 찾아가면 된다.
    이 경우 O(N)에 찾을 수 있다.

    그래서 전체 시간 복잡도는 정렬로 N log N,
    합치기 N, 정답 찾기 N으로 O(N log N) = O(2N + N log N)이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1879
    {

        static void Main1879(string[] args)
        {

            int n;
            (int l, int r)[] arr;

            Input();

            ConnArr();

            GetRet();

            void GetRet()
            {

                int ret = arr[0].r;
                int maxPos = 2 * arr[0].r - arr[0].l;

                for (int i = 1; i < n; i++)
                {

                    if (maxPos < arr[i].l) break;
                    ret = arr[i].r;
                    maxPos = Math.Max(maxPos, 2 * arr[i].r - arr[i].l);
                }

                Console.Write(ret);
            }

            void ConnArr()
            {

                Array.Sort(arr, (x, y) => x.l.CompareTo(y.l));

                int curL = arr[0].l;
                int curR = arr[0].r;
                int idx = 0;
                for (int i = 1; i < n; i++)
                {

                    // 이어붙일 수 있으면 이어붙이기!
                    if (arr[i].l <= curR) curR = Math.Max(curR, arr[i].r);
                    else
                    {

                        // 이어붙일 수 없다.
                        arr[idx++] = (curL, curR);
                        curL = arr[i].l;
                        curR = arr[i].r;
                    }
                }

                arr[idx++] = (curL, curR);
                n = idx;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new (int l, int r)[n];
                for (int i = 0; i < n; i++)
                {

                    int l = ReadInt();
                    int r = ReadInt();
                    arr[i] = (l, r);
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
}
