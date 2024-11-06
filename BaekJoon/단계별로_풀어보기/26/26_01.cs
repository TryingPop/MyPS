using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 10. 21
이름 : 배성훈
내용 : 누적합
    문제번호 : 11659번

    값을 저장해 놓고 사용하는 방식!
*/
namespace BaekJoon._26
{
    internal class _26_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] nums = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 저장된 총합
            long[] sums = new long[info[0] + 1];
            long total = 0;

            // 1번부터 n번까지의 총합이다
            // 1번부터 0번까지니 넣는 값은 0
            for (int i = 1; i < sums.Length; i++)
            {

                total += nums[i - 1];
                sums[i] = total;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < info[1]; i++)
            {

                int[] se = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                long sum = sums[se[1]] - sums[se[0] - 1];

                sb.AppendLine(sum.ToString());
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }
        }
    }
}
