using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 18
이름 : 배성훈
내용 : 회전하는 큐
    문제번호 : 1021번
*/

namespace BaekJoon._20
{
    internal class _20_06
    {

        static void Main6(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] finds = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            Queue<int> que = new Queue<int>(info[0]);

            for (int i = 1; i <= info[0]; i++)
            {

                que.Enqueue(i);
            }

            int result = 0;
            for (int i = 0; i < info[1]; i++)
            {

                int count = 0;
                while (true)
                {

                    int temp = que.Dequeue();

                    if (temp == finds[i])
                    {

                        break;
                    }
                    que.Enqueue(temp);
                    count++;
                }

                count = count <= que.Count - count + 1 ? count : que.Count - count + 1;

                result += count;
            }

            Console.WriteLine(result);
        }
    }
}
