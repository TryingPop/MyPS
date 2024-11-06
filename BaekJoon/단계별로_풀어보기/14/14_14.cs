// #define first        // Array.Sort 과정을 대체하는 방법
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 12
이름 : 배성훈
내용 : 전깃줄
    문제번호 : 2565번
*/

namespace BaekJoon._14
{
    internal class _14_14
    {

        static void Main14(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int length = int.Parse(sr.ReadLine());
#if first
            int[][] lines = new int[length][];


            for (int i = 0; i < length; i++)
            {

                lines[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }

            // 이 과정을 크기가 500인 배열이용으로 대체 가능
            Array.Sort(lines, (x, y) =>
            {
                int res1 = x[0].CompareTo(y[0]);
                return res1;
            });
#else
            int[] lines = new int[501];

            for (int i = 0; i < length; i++)
            {

                int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                lines[inputs[0]] = inputs[1];
            }
#endif


            // lines[i][1]의 가장 긴 증가 부분 수열 만들기
            // result는 여기서 빠진 원소의 갯수!

            int[] lens = new int[length];
            int max = 0;
#if first
            for (int i = 0; i < length; i++)
#else
            for (int i = 0; i < lines.Length; i++)
#endif
            {

                int len = 0;
                for (int j = 0; j < i; j++)
                {
#if first
                    if (lines[i][1] > lines[j][1])
#else
                    if (lines[j] != 0 && lines[i] > lines[j])
#endif
                    {

                        
                        if (len < lens[j])
                        {

                            len = lens[j];
                        }
                    }
                }

                if (max < len + 1) max = len + 1;
                lens[i] = len + 1;
            }

            int result = length - max;
            Console.WriteLine(result);
        }
    }
}
