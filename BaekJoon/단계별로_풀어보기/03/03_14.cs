using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.08.10
 * 내용 : 백준 3단계 
 * 
 * 영수증
 */

namespace BaekJoon._03
{
    internal class _03_14
    {
        static void Main14(string[] args)
        {
            int total = int.Parse(Console.ReadLine());

            int len = int.Parse(Console.ReadLine());
            int result = 0;

            for (int i = 0; i < len; i++)
            {
                string[] strArray = Console.ReadLine().Split(" ");
                result += (int.Parse(strArray[0])) * int.Parse(strArray[1]);
            }

            if (result == total)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
