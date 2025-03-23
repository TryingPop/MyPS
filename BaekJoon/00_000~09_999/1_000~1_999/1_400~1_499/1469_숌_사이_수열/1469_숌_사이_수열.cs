using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 23
이름 : 배성훈
내용 : 숌 사이 수열
    문제번호 : 1469번

    브루트포스, 백트래킹 문제다.
    따로 방법이 안떠올라 힌트를 봤다.
    그러니 브루트포스 알고리즘이라 적혀,
    모든 수열을 만들어보고 조건을 만족하는지 찾아봤다.
*/

namespace BaekJoon.etc
{
    internal class etc_1453
    {

        static void Main1453(string[] args)
        {

            int x = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            bool[] use = new bool[x];
            Array.Sort(arr);
            int[] calc = new int[x << 1];
            bool[] fill = new bool[x << 1];

            if (DFS())
            {

                for (int i = 0; i < calc.Length; i++)
                {

                    Console.Write($"{calc[i]} ");
                }
            }
            else Console.Write(-1);
            

            bool DFS(int _dep = 0)
            {

                if (_dep == calc.Length)
                    return true;
                else if (fill[_dep])
                    return DFS(_dep + 1);

                for (int i = 0; i < x; i++)
                {

                    if (use[i]) continue;

                    int val = arr[i];
                    int next = val + _dep + 1;
                    if (x * 2 <= next || fill[next]) continue;

                    use[i] = true;
                    fill[_dep] = true;
                    fill[next] = true;

                    calc[_dep] = val;
                    calc[next] = val;
                    if (DFS(_dep + 1)) return true;
                    fill[_dep] = false;
                    use[i] = false;
                    fill[next] = false;
                }

                return false;
            }
        }
    }
}
