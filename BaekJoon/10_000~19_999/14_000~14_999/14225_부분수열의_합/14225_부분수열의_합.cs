using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 13
이름 : 배성훈
내용 : 부분수열의 합
    문제번호 : 14225번

    브루트포스 문제다.
    범위를 잘못설정해 한 번 틀렸다.
    모든 부분합을 구해서 1부터 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1272
    {

        static void Main1272(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            bool[] sum = new bool[(1 << 20) + 1];

            DFS();

            int ret = sum.Length;
            for (int i = 1; i < sum.Length; i++)
            {

                if (sum[i]) continue;
                ret = i;
                break;
            }

            Console.Write(ret);

            void DFS(int _idx = 0, int _val = 0)
            {

                if (_idx == n) return;
                DFS(_idx + 1, _val);
                _val += arr[_idx];
                
                if (_val <= sum.Length) sum[_val] = true;

                DFS(_idx + 1, _val);
            }
        }
    }
}
