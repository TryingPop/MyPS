using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 3
이름 : 배성훈
내용 : 미네크래프트
    문제번호 : 15708번

    그리디, 우선순위 큐 문제다.
    문제 해석을 잘못해 한참을 고민했다.

    만약 모두 캐는 경우면 무료로 이동하는 줄 알았다;
    전부 캐는 것과 무료를 고민해야했고 문제를 다시 읽어보게 되었다.
    그러니 캐는거 상관없이 이동에 p만큼 들어감을 확인했다.

    아이디어는 다음과 같다.
    먼저 이동은 인덱스가 증가하는 오른쪽으로만 이동한다.
    인덱스가 감소하는 곳, 왼쪽으로 가는 것은 에너지 낭비이기 때문이다.

    먼저 현재 가능한 최대 에너지를 찾는다.
    이는 아무것도 캐지 않고 이동한 에너지를 뜻한다.

    최대에너지가 허용하는한 돌을 최대한 캐는 것이 좋다.
    그리고 이전에 효율이 좋아 캐지 않았던것은 다음에 캐지 않음을 확인할 수 있다.
    그래서 우선순위 큐로 여태까지 캐온 돌들을 관리한다.
    내림차순으로 효율이 안좋은 것들을 먼저 버린다.
    이렇게 진행하면서 최대 돌의 갯수를 매번 확인한다.

    여기서 발견된 최댓값들의 최댓값이 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1610
    {

        static void Main1610(string[] args)
        {

            int n, t, p;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                PriorityQueue<int, int> pq = new(n);
                int ret = 0;
                int sum = 0;

                // t -= p : 이동에 사용한 에너지
                // t : i번째에 있을 수 있는 최대 에너지
                for (int i = 0; i < n && 0 <= t; i++, t -= p)
                {

                    sum += arr[i];
                    pq.Enqueue(arr[i], -arr[i]);

                    while (sum > t && pq.Count > 0)
                    {

                        sum -= pq.Dequeue();
                    }
                    
                    ret = Math.Max(ret, pq.Count);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                t = ReadInt();
                p = ReadInt();

                arr = new int[n];
                for(int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
