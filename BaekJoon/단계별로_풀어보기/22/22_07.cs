using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 가로수
    문제번호 : 2485번
*/

namespace BaekJoon._22
{
    internal class _22_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[] dis = new int[len - 1];

            int before;
            int gcd;
            {

                int f1 = int.Parse(sr.ReadLine());
                int f2 = int.Parse(sr.ReadLine());
                before = int.Parse(sr.ReadLine());
                dis[0] = f2 - f1;
                dis[1] = before - f2;
                gcd = GCD(dis[0], dis[1]);
            }

            {

                int input = before;

                for (int i = 3; i < len; i++)
                {

                    before = input;
                    input = int.Parse(sr.ReadLine());
                    dis[i - 1] = input - before;
                    gcd = GCD(gcd, dis[i - 1]);
                }
                sr.Close();
            }

            int result = 0;
            for (int i = 0; i < dis.Length; i++)
            {

                dis[i] /= gcd;
                result += dis[i] - 1;
            }

            Console.WriteLine(result);
        }

        static int GCD(int x, int y)
        {

            int r = x % y;
            while (r != 0)
            {

                x = y;
                y = r;
                r = x % y;
            }

            r = y;
            return r;
        }

        /*
        // 문자 숫자로 변환하는 함수!
        int ScanInt()
        {
            int c, n = 0;
            while (!((c = Console.Read()) is '\n' or -1))
                n = 10 * n + c - '0';
            return n;
        }
        */
    }
}
