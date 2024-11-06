using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 13
이름 : 배성훈
내용 : 나무 자르기
    문제번호 : 2805번

    여기서는 자를 수 있는 나무의 길이가 0 ~ 1_000_000_000 으로 주어져서 정렬할 필요가 없다
    정렬하면 시간만 늦어진다!
*/

namespace BaekJoon._28
{
    internal class _28_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            long[] info = sr.ReadLine().Split(' ').Select(long.Parse).ToArray();

            // 오름차순 정렬
            // int[] inputs = sr.ReadLine().Split(' ').Select(int.Parse).OrderByDescending(x => x).ToArray();
            int[] inputs = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            sr.Close();

            int start = 0;
            // int end = inputs[0];
            int end = 1_000_000_000;


            while (start < end)
            {

                int mid = (start + end + 1) / 2;
                long sum = 0;

                for (int i = 0; i < info[0]; i++)
                {

                    int cut = inputs[i] - mid;
                    if (cut < 0) cut = 0;
                    sum += cut;
                }

                if (sum >= info[1]) start = mid;
                else end = mid - 1;
            }

            Console.WriteLine(start);
        }
    }
}
