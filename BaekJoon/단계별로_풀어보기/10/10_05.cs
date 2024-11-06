using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.22
 * 내용 : 백준 10단계 5번 문제
 * 
 * 영화감독 숌
 * 1만 범위 넘어가면 성립하지 않는 코드다!
 */

namespace BaekJoon._10
{
    internal class _10_05
    {
        static void Main5(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            int head = 0;
            int tail = 0;
            bool chk = false;
            int ni = 0;


            for (int i = 0; i < num - 1; i++)
            {
                FindNum(ref head, ref tail, ref chk, ref ni);
            }

            string result = "666";
            string chk2 = tail.ToString();
            if (head != 0)
            {
                result = head.ToString() + result;
            }

            if (ni != 0)
            {
                while (chk2.Length < ni)
                {
                    chk2 = "0" + chk2;
                }

                result = result + chk2;

            }

            Console.WriteLine(result);

        }

        static void FindNum(ref int head, ref int tail, ref bool chk, ref int ni)
        {
            if (chk)
            {
                tail++;
                if (tail == ((int)Math.Pow(10, ni)))
                {
                    tail = 0;
                    chk = false;

                    while (ni > 0)
                    {
                        ni--;
                        head *= 10;
                        if (ni == 0)
                        {
                            head += 7;
                        }
                        else
                        {
                            head += 6;
                        }
                    }
                }
            }
            else
            {
                head += 1;

                if (head % 10 == 6)
                {
                    head /= 10;
                    int i = 1;
                    ni = i;
                    while ((head % 10) == 6)
                    {
                        head /= 10;
                        i++;
                        ni = i;
                    }
                    chk = true;
                    tail = 0;
                }
            }
            return;
        }
    }
}
