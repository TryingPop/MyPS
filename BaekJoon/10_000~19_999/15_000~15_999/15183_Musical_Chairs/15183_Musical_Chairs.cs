using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : Musical Chairs
    문제번호 : 15183번

    구현, 시뮬레이션 번호다.
    N이 15로 작다.
    그리고 의자 찾는 게임은 많아야 N을 넘지 않는다. 회전은 많아야 1회당 30회이하이다.
    시뮬레이션 돌려도 N^2 x M <= 225 x 30 < 10_000 이므로 이상없다.
*/

namespace BaekJoon.etc
{
    internal class etc_1429
    {

        static void Main1429(string[] args)
        {

            string ELI = " has been eliminated.\n";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            string[] name = new string[n];
            int[] table = new int[n];

            for (int i = 0; i < n; i++)
            {

                name[i] = sr.ReadLine();
                table[i] = i;
            }

            int r = int.Parse(sr.ReadLine());
            int len = n;
            for (int i = 0; i < r; i++)
            {

                string[] temp = sr.ReadLine().Split();

                int pop = int.Parse(temp[0]) - 1;
                int rot = int.Parse(temp[1]) % len;

                for (int j = 0; j < rot; j++)
                {

                    Rot(len);
                }

                Remove(pop, len);
                len--;

                sw.Write($"{name[table[len]]}{ELI}");
            }

            Array.Sort(table, 0, len);

            if (len == 1)
                sw.Write($"{name[table[0]]} has won.");
            else
            {

                sw.Write("Players left are");

                for (int i = 0; i < len; i++)
                {

                    sw.Write($" {name[table[i]]}");
                }

                sw.Write('.');
            }

            void Remove(int _pop, int _len)
            {

                int pop = table[_pop];

                for (int i = _pop + 1; i < _len; i++)
                {

                    table[i - 1] = table[i];
                }

                table[_len - 1] = pop;
            }

            void Rot(int _len)
            {

                int temp = table[_len - 1];
                for (int i = _len - 1; i >= 1; i--)
                {

                    table[i] = table[i - 1];
                }

                table[0] = temp;
            }
        }
    }
}
