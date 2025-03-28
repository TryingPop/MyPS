using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 차트
    문제번호 : 1239번

    브루트포스 문제다.
    가능한 경우를 모두 조사해도 8!밖에 안된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1479
    {

        static void Main1479(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            bool[] visit = new bool[n];
            bool[] chk = new bool[101];

            chk[0] = true;
            int ret = 0;
            DFS();

            Console.Write(ret);
            void DFS(int _dep = 0, int _sum = 0, int _line = 0)
            {

                if (_dep == n)
                {

                    ret = Math.Max(_line, ret);
                    return;
                }

                for (int i = 0; i < n; i++)
                {

                    if (visit[i]) continue;
                    visit[i] = true;

                    int nextSum = arr[i] + _sum;
                    int add = nextSum - 50 < 0 || !chk[nextSum - 50] ? 0 : 1;
                    if (nextSum < 50) chk[nextSum] = true;
                    DFS(_dep + 1, nextSum, _line + add);
                    if (nextSum < 50) chk[nextSum] = false;
                    visit[i] = false;
                }
            }
        }
    }
}
