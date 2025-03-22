using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : 금민수의 개수
    문제번호 : 1527번

    브루트포스 문제다.
    처음에는 만들어 풀려고 했으나, 코드가 꽤나 복잡해졌다.
    다른 방법은 없을까 고민하던 중 전체 개수를 세어보니 10억 미만에서는 1022개만 존재 가능하다.
    그래서 모든 경우를 만들고 정렬한 뒤 이분탐색으로 개수를 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1147
    {

        static void Main1147(string[] args)
        {

            int INF = 1_000_000_000;
            List<int> specialNum = new(1 << 10);
            specialNum.Add(4);
            specialNum.Add(7);

            for (int i = 0; i < specialNum.Count; i++)
            {

                int cur = specialNum[i];
                if (cur * 10L > INF) break;

                specialNum.Add(cur * 10 + 4);
                specialNum.Add(cur * 10 + 7);
            }

            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int ret = BinarySearch(input[1]) - BinarySearch(input[0] - 1);
            Console.Write(ret);

            int BinarySearch(int _chk)
            {

                int l = 0;
                int r = specialNum.Count - 1;
                while(l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (specialNum[mid] <= _chk) l = mid + 1;
                    else r = mid - 1;
                }

                return l - 1;
            }
        }
    }
}
