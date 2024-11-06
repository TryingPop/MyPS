using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : 자동차 주차
    문제번호 : 30993번

    수학 조합론 문제다
    조합 단원에서 많이 푼 문제다

    같은차종이므로 조합이다
    A + B + C 에서 A 개를 택해서 배치한다
    (A + B + C) Combi A 이다

    그리고 나머지 B + C 에서 B개를 택해 배치하면 C는 나머지 자리에 배치하면 된다
    그래서 전체 경우는 ((A + B + C) Combi A )* ((B + C) Combi B) = (A + B + C)! / (A! B! C!)
    이되고 이를 구해 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0491
    {

        static void Main491(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            long[] fac = new long[input[0] + 1];
            fac[0] = 1;
            for (int i = 1; i <= input[0]; i++)
            {

                fac[i] = fac[i - 1] * i;
            }

            long ret = fac[input[0]] / (fac[input[1]] * fac[input[2]] * fac[input[3]]);
            Console.WriteLine(ret);
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noPAtry1
{
    internal class Program
    {
        static ulong Factorial(ulong value)
        {
            ulong result = 1;
            for (; value > 1; value--) result *= value;
            return result;
        }

        static void Main(string[] args)
        {
            string[] nums = Console.ReadLine().Split(' ');
            ulong[] cars = new ulong[nums.Length];
            for (int index = 0; index < nums.Length; index++)
            {
                cars[index] = ulong.Parse(nums[index]);
            }
            ulong result = Factorial(cars[0]) / (Factorial(cars[1]) * Factorial(cars[2]) * Factorial(cars[3]));
            Console.WriteLine(result);
        }
    }
}
#elif other2
// cs30993 - rbybound
// 2023-12-25 11:37:20
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cs30993
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static int N, A, B, C;
        static int count = 0;

        public static void Main(string[] args)
        {
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            (N, A, B, C) = (line[0], line[1], line[2], line[3]);

            Recursion();
            sb.AppendLine(count.ToString());

            sw.Write(sb);
            sw.Close();
            sr.Close();
        }

        static void Recursion()
        {
            if(A+B+C == 0)
            {
                count++;
                return;
            }

            if(A > 0)
            {
                A--;
                Recursion();
                A++;
            }
            if(B > 0)
            {
                B--;
                Recursion();
                B++;
            }
            if(C > 0)
            {
                C--;
                Recursion();
                C++;
            }
        }
    }
}
#endif
}
