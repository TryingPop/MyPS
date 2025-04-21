using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 21
이름 : 배성훈
내용 : 꿀 따기
    문제번호 : 21758번
    
    그리디, 누적합 문제다.
    벌통을 중심으로 꿀벌의 위치를 확인하면 다음과 같이 3가지로 나뉜다.
      1. 꿀벌 2마리가 벌통 오른쪽에 있는 경우
      2. 꿀벌 2마리가 벌통 왼쪽에 있는 경우
      3. 꿀벌 사이에 벌통이 있는 경우

    1.의 경우를 보면 벌통은 젤 왼쪽에 두는게 얻을 수 있는 점수가 최대이다.
    그리고 꿀벌 1마리는 오른쪽 끝에 두는게 제일 좋다.
    그래서 중앙에 벌꿀을 놓아보며 최댓값을 찾으면 된다.

    마찬가지로 2.의 경우 벌통은 젤 오른쪽이고 꿀벌 1마리는 젤 왼쪽이다.
    그래서 중앙에 벌꿀을 놓아보며 최댓값을 찾으면 된다.

    3.의 경우는 꿀벌은 젤 왼쪽과 젤 오른쪽에 두는게 좋다.
    그래서 중앙에 벌통을 놓아보며 최댓값을 찾으면 된다.
    
    1, 2, 3에서 찾은 최댓값이 전체의 최댓값이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1562
    {

        static void Main1562(string[] args)
        {

            int n;
            long[] arr;

            Input();

            GetRet();

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                arr = new long[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = arr[i - 1] + ReadInt();
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

            void GetRet()
            {

                long ret = 0;

                for (int i = 2; i < n; i++)
                {

                    long cur;
                    // 오른쪽
                    cur = ((arr[n] - arr[i]) << 1) + arr[i - 1] - arr[1];
                    ret = Math.Max(cur, ret);

                    // 왼쪽
                    cur = ((arr[i - 1] - arr[0]) << 1) + arr[n - 1] - arr[i];
                    ret = Math.Max(cur, ret);

                    // 중앙
                    cur = arr[i] - arr[1] + arr[n - 1] - arr[i - 1];
                    ret = Math.Max(cur, ret);
                }

                Console.Write(ret);
            }
        }
    }
}
