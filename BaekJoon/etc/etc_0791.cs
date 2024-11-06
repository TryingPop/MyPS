using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 4
이름 : 배성훈
내용 : 도영이가 만든 맛있는 음식
    문제번호 : 2961번

    브루트포스, 비트마스킹, 백트래킹 문제다
    브루트포스로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0791
    {

        static void Main791(string[] args)
        {

            StreamReader sr;
            int ret = 2_000_000_000;
            (int m, int a)[] arr;
            int len;
            Solve();

            void Solve()
            {

                Input();

                DFS(0, 1, 0, true);

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                len = ReadInt();
                arr = new (int m, int a)[len];

                for (int i = 0; i < len; i++)
                {

                    int m = ReadInt();
                    int a = ReadInt();
                    arr[i] = (m, a);
                }

                sr.Close();
            }

            void DFS(int _depth, int _mul, int _add, bool _isEmpty)
            {

                if (_depth == len)
                {

                    if (_isEmpty) return;

                    int chk = _mul - _add;
                    if (chk < 0) chk = -chk;

                    ret = chk < ret ? chk : ret;
                    return;
                }


                DFS(_depth + 1, _mul * arr[_depth].m, _add + arr[_depth].a, false);
                DFS(_depth + 1, _mul, _add, _isEmpty);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Linq;

internal class Program
{
    static void Main(string[] args)
    {
        int num = Int32.Parse(Console.ReadLine());
        int[,] numList = new int[num, 2];
        bool[] isSelected = new bool[num];
        int minSum = int.MaxValue;

        for (int i = 0; i < num; i++)
        {
            int[] str = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            numList[i, 0] = str[0];
            numList[i, 1] = str[1];
        }

        void makeSub(int cnt)
        {
            if (cnt == num)
            {
                int sum = getSum();
                if (sum != int.MaxValue)
                {
                    minSum = Math.Min(minSum, sum);
                }
                return;
            }

            isSelected[cnt] = true;
            makeSub(cnt + 1);

            isSelected[cnt] = false;
            makeSub(cnt + 1);
        }

        int getSum()
        {
            int sourProduct = 1;
            int bitterSum = 0;
            bool selected = false;

            for (int j = 0; j < num; j++)
            {
                if (isSelected[j])
                {
                    sourProduct *= numList[j, 0];
                    bitterSum += numList[j, 1];
                    selected = true;
                }
            }

            if (!selected)
            {
                return int.MaxValue; // 재료를 하나도 사용하지 않은 경우 무시
            }

            return Math.Abs(sourProduct - bitterSum);
        }

        makeSub(0);
        Console.WriteLine(minSum);
    }
}
#endif
}
