using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 13
이름 : 배성훈
내용 : 종이 접기
    문제번호 : 12979번

    수학, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1819
    {

        static void Main1819(string[] args)
        {

            int INF = 123_456_789;
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            // 약수 찾기
            List<int> divs = new();
            for (int i = 1; i * i <= arr[2]; i++)
            {

                if (arr[2] % i != 0) continue;
                divs.Add(i);
                divs.Add(arr[2] / i);
            }

            divs.Sort();

            // 이제 최소 경우 찾기 브루트포스
            int ret = INF;
            for (int i = 0; i < divs.Count; i++)
            {

                int find1 = divs[i];
                int find2 = arr[2] / divs[i];

                int chk = GetCnt(arr[0], find1) + GetCnt(arr[1], find2);
                ret = Math.Min(ret, chk);
            }

            if (ret == INF) ret = -1;
            Console.Write(ret);

            // 개수 찾기
            int GetCnt(int _cur, int _find)
            {

                if (_cur < _find) return INF;

                int ret = 0;
                while (_find < _cur)
                {

                    ret++;
                    _cur = (_cur + 1) >> 1;
                }

                return ret;
            }
        }
    }
}
