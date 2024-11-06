using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 9단계 5번 문제
 * 
 * 하노이 탑 이동 순서
 */

namespace BaekJoon._09
{
    internal class _09_05
    {
        static void Main5(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            int len = FindNum(num);
            sb.AppendLine(FindNum(num).ToString());
            
            GoRing(num, 1, 3, sb);
            
            Console.WriteLine(sb);

            sb = new StringBuilder();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(sb);
        }

        // 타입이 불분명한건 그냥 void 하고 return; 하면 된다
        static int FindNum(int n)
        {
            if (n == 1)
            {
                return 1;
            }

            return 2 * FindNum(n - 1) + 1;
        }

        static void GoRing(int a, int b, int c, StringBuilder sb)
        {
            if (a <= 0)
            {
                return;
            }
            GoRing(a - 1, b, 6 - b - c, sb);
            
            sb.AppendLine($"{b} {c}");

            // 2 > 3 으로 옮기는거
            GoRing(a - 1, 6 - b - c, c, sb);

        }
    }
}
