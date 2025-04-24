using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 25
이름 : 배성훈
내용 : The Bovine Fire Drill
    문제번호 : 5984번

    구현, 시뮬레이션 문제다.
    조건대로 구현하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1576
    {

        static void Main1576(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            // arr[i] = val : i + 1 번 의자에 앉아 있는 사람이 val
            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = i + 1;
            }

            // 이동했는지 여부 파악
            bool[] move = new bool[n + 1];

            // cur : 현재 사람, pos : 현재 좌표, prev : 이전 사람, dis
            int cur = 0, pos = 0, prev = 0;

            // 많아야 N명 시행한다.
            while (true)
            {

                if (IsEnd()) break;

                prev = cur;
                cur = arr[pos];
                arr[pos] = prev;
                move[cur] = true;

                Move();

                // 탈출 조건 확인
                bool IsEnd()
                {

                    // 빈 자리 or 이미 이동한 사람이 있는 곳인지 확인한다.
                    int chk = arr[pos];
                    return move[chk] || chk == 0;
                }

                void Move()
                {

                    // 의자 번호 만큼 이동한다.
                    int len = pos + 1;
                    for (int i = 0; i < len; i++)
                    {

                        pos = pos == n - 1 ? 0 : pos + 1;
                    }
                }
            } 

            Console.Write(cur);
        }
    }
}
