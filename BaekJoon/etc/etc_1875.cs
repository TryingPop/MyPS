using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 8
이름 : 배성훈
내용 : 지금 자면 꿈을 꾸지만
    문제번호 : 32029번

    브루트포스, 그리디, 정렬 문제다.
    두 포인터 알고리즘을 이용해 N^3에 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1875
    {

        static void Main1875(string[] args)
        {

            int n, a, b;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int ESCAPE = 123;
                int ret = 0;

                for (int prev = 0; prev <= n; prev++)
                {

                    int initIdx = 0;

                    // 초기에 일 가능한지 판별
                    bool flag = false;
                    for (int work = 1; work <= prev; work++)
                    {

                        initIdx = GetIdx(a * work, initIdx) + 1;
                        if (initIdx <= n) continue;

                        // prev개를 못채우면 종료한다.
                        flag = true;
                        break;
                    }

                    if (flag) break;
                    // 최대값갱신 시도
                    int max = prev;
                    ret = Math.Max(ret, prev);

                    for (int sleep = 0; sleep < a; sleep++)
                    {

                        int cur = prev;

                        int workTime = a - sleep;
                        int time = a * prev + b * sleep;
                        int chkIdx = GetIdx(time, initIdx);


                        while ((chkIdx = GetIdx(time + workTime, chkIdx) + 1) <= n)
                        {

                            cur++;
                            time += workTime;
                        }

                        max = Math.Max(cur, max);
                    }

                    ret = Math.Max(ret, max);
                }

                Console.Write(ret);

                // Val보다 크거나 같은 가장 작은 인덱스 찾기
                int GetIdx(int val, int minIdx = 0)
                {

                    for (int i = minIdx; i < n; i++)
                    {

                        if (arr[i] < val) continue;
                        return i;
                    }

                    return ESCAPE;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                a = ReadInt();
                b = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr);

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

                        while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
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
