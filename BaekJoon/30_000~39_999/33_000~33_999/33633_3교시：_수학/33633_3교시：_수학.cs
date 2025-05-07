using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 7
이름 : 배성훈
내용 : 3교시: 수학
    문제번호 : 33633번

    수학, BFS, 해시 문제다.
    우박수를 찾는 문제다.
    큐를 이용해 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1617
    {

        static void Main1617(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            long[] cur = new long[700_000], next = new long[700_000];
            int cLen = 0, nLen = 0;
            cur[cLen++] = 1;

            for (int i = 2; i <= n; i++)
            {

                // 길이 i - 1인 원소들을 갖고 길이 i인 원소 찾기
                for (int j = 0; j < cLen; j++)
                {

                    long node = cur[j];
                    // 길이 i인 수에서 2배를 한 수는 길이 i + 1인 수이다.
                    long chk = node << 1;
                    next[nLen++] = chk;

                    // 홀수에서 올 수 있는지 확인
                    chk = (node - 1) / 3;
                    if (chk % 2 == 0 || chk == 1 || chk * 3 + 1 != node) continue;
                    next[nLen++] = chk;
                }

                // 다음꺼 바꾼다.
                var temp = cur;
                cur = next;
                next = temp;
                cLen = nLen;
                nLen = 0;
            }

            // 출력 조건에 맞춰 정렬
            Array.Sort(cur, 0, cLen);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            sw.Write($"{cLen}\n");

            for (int i = 0; i < cLen; i++)
            {

                sw.Write($"{cur[i]}\n");
            }
        }
    }
}
