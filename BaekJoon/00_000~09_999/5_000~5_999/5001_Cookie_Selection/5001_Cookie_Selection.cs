using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 1
이름 : 배성훈
내용 : Cookie Selection
    문제번호 : 5001번

    우선순위 큐 문제다.
    현재 배열의 중앙값을 찾아야 한다.
    우선순위 큐 2개를 이용해 찾았다.

    문제에서 짝수인 경우 큰 쪽의 중앙값을 출력해야 한다.
    그래서 min은 오름 차순 정렬 했을 때 작은 n / 2개의 원소를 보관한다.(소숫점 내림)
    max는 남은 부분을 나타낸다.

    이러면 max의 가장 작은 원소가 중아값이 된다.
    max의 원소는 min과 1개 차이 나야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1604
    {

        static void Main1604(string[] args)
        {

            string TOOL = "#";
            int CAPACITY = 300_000;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            PriorityQueue<int, int> min = new(CAPACITY), max = new(CAPACITY);

            string input;

            while (!string.IsNullOrEmpty((input = sr.ReadLine())))
            {

                if (input == TOOL)
                {

                    int ret = max.Dequeue();

                    if(max.Count < min.Count)
                    {

                        int val = min.Dequeue();
                        max.Enqueue(val, val);
                    }

                    sw.Write($"{ret}\n");
                }
                else
                {

                    int num = int.Parse(input);

                    min.Enqueue(num, -num);
                    if (max.Count < min.Count)
                    {

                        int val = min.Dequeue();
                        max.Enqueue(val, val);
                    }

                    if (min.Count > 0 && min.Peek() > max.Peek())
                    {

                        int maxVal = max.Dequeue();
                        int minVal = min.Dequeue();

                        min.Enqueue(maxVal, -maxVal);
                        max.Enqueue(minVal, minVal);
                    }
                }
            }
        }
    }
}
