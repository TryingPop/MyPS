using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 8
이름 : 배성훈
내용 : 세 번째
    문제번호 : 5619번

    그리디, 브루트포스 문제다.
    a < b 에 대해 c와 이어붙이는 경우를 보자.
    ac < bc이고 ca < cb임을 알 수 있다.
    그래서 3번 째로 작은 것을 찾기위해 4개의 원소를 모아서 비교하면 된다.
    그래서 작은거의 일부만 발췌해서 찾아줘도 된다.
    
    가장 작은 3개만 쓴다면 3번째로 작은 수를 찾을 수 없다.
        9, 10, 20, 30
    인 경우 모든 경우를 조합해보면 3번째로 작은 수는 39이다.
    
    그런데 4개를 쓰면 정확히 찾을 수 있다.
    이는 경우의 수를 나눠서 확인하면 최소임을 알 수 있다.
    자릿수가 다른거 4개, 3개, 2개(1 - 3, 2 - 2), 1개 이렇게 나눌 수 있다.
    모두 3번째로 작은 것을 찾을 수 있음을 확인할 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1967
    {

        static void Main1967(string[] args)
        {

            int n, len;
            int[] arr, cnt;

            Input();

            GetRet();

            void GetRet()
            {

                FindMin();

                int[] ret = new int[100];
                int idx = 0;
                for (int i = 0; i < len; i++)
                {

                    for (int j = i + 1; j < len; j++)
                    {

                        string f = arr[i].ToString();
                        string t = arr[j].ToString();

                        ret[idx++] = int.Parse(f + t);
                        ret[idx++] = int.Parse(t + f);
                    }
                }

                Array.Sort(ret, 0, idx);

                Console.Write(ret[2]);

                void FindMin()
                {

                    len = 0;
                    for (int i = 0; i < cnt.Length; i++)
                    {

                        while (cnt[i] > 0)
                        {

                            cnt[i]--;
                            arr[len++] = i;
                            if (len == 4) return;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                cnt = new int[10_001];
                arr = new int[4];
                for (int i = 0; i < n; i++)
                {

                    cnt[ReadInt()]++;
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
