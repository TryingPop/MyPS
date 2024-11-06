using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 25
이름 : 배성훈
내용 : 연산자 끼워넣기
    문제번호 : 14888번

    백트래킹 단원인데 완전 탐색으로 풀었다;
    아마도 의도는 opNum이 같으면 탈출하라 같은데... 추후에 다시 해봐야겠다
    ... ?
*/

namespace BaekJoon._25
{
    internal class _25_07
    {

        static void Main7(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] opNum = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);   // +, -, *, /

            int min = int.MaxValue, max = int.MinValue;

            // Dfs7(1, nums[0], nums, opNum[0], opNum[1], opNum[2], opNum[3], ref min, ref max);

            Console.WriteLine(max);
            Console.WriteLine(min);
        }

        private static void Dfs(int depth, int total, int[] nums, int plus, int minus, int multi, int div, ref int min, ref int max)
        {

            if (depth == nums.Length)
            {

                min = min > total ? total : min;
                max = max >= total ? max : total;
                return;
            }

            if (plus > 0)
            {

                Dfs(depth + 1, total + nums[depth], nums, plus - 1, minus, multi, div, ref min, ref max);
            }

            if (minus > 0)
            {

                Dfs(depth + 1, total - nums[depth], nums, plus, minus - 1, multi, div, ref min, ref max);
            }

            if (multi > 0)
            {

                Dfs(depth + 1, total * nums[depth], nums, plus, minus, multi - 1, div, ref min, ref max);
            }

            if (div > 0)
            {

                Dfs(depth + 1, total / nums[depth], nums, plus, minus, multi, div - 1, ref min, ref max);
            }
        }


        /*
        static void Main77(string[] args)
        {

            int[] arr1 = { 1, 2, 3 }, arr2 = new int[4];
            Array.Copy(arr1, arr2, 2);      // 1 2 0 0


            int[][] arr3 = new int[3][], arr4 = { arr1, arr2 };

            Array.Copy(arr4, arr3, 2);

            arr4[0] = new int[2];

            foreach(var item in arr3)
            {

                foreach(var i in item)
                {

                    Console.WriteLine(i);
                }
            }
        }

        // 모든 경우의 수 따져야하는데 .. ? 
        // 탈출을 어떻게?
        // 1. 곱셈 유무 맨 뒤는 곱셈이 와야할거 같다!
        // 그리고 DFS가 아닌 BFS 방법의 탐색이 필요한거 같은데?..? 그래서 가지치기
        // 그래야 진행하면서 같은 개수를써도 가지치기가 될거 같다?

    */
    }
}
