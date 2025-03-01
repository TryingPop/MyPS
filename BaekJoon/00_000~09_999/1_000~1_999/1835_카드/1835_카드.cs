using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 28
이름 : 배성훈
내용 : 카드
    문제번호 : 1835번

    구현, 자료구조, 시뮬레이션, 덱 문제다.
    카드를 조건대로 하나씩 펼쳐놓을 때 1, 2, 3, ... N의 순서로 나와야 한다.
    해당 부분을 캐치 못해 해당 방법으로 나오는 카드가 몇 번인지 확인하는걸 출력해 1번 틀렸다.
    큐를 이용해 구현했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1366
    {

        static void Main1366(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            Queue<int> q = new(n);
            for (int i = 1; i <= n; i++)
            {

                q.Enqueue(i);
            }

            int[] ret = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                int len = i % q.Count;
                for (int j = 0; j < len; j++)
                {

                    q.Enqueue(q.Dequeue());
                }

                int idx = q.Dequeue();
                ret[idx] = i;
            }

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            for (int i = 1; i <= n; i++)
            {

                sw.Write($"{ret[i]} ");
            }
        }
    }
}
