using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 12
이름 : 배성훈
내용 : 수 찾기
    문제번호 : 1920번

    이진 탐색 문제라 이진 탐색으로 풀었다
    속도는 해시셋 쓰면 빠르다!
    그런데 질문글 찾아보니 해시셋은 오버헤드를 일으킨다고 한다
*/

namespace BaekJoon._28
{
    internal class _28_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            sr.ReadLine();

            int[] inputs = sr.ReadLine().Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();

            int len = int.Parse(sr.ReadLine());

            int[] targets = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            // 이진 탐색
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                sb.AppendLine(BinarySearch(inputs, targets[i]) ? "1" : "0");
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            sw.Write(sb);

            sw.Close();
        }

        static bool BinarySearch(int[] inputs, int target)
        {

            int start = 0;
            int end = inputs.Length - 1;
            while(start < end)
            {

                int mid = (start + end) / 2;

                if (target <= inputs[mid])
                {

                    end = mid;
                }
                else
                {

                    start = mid + 1;
                }
            }

            return inputs[end] == target;
        }
    }
}
