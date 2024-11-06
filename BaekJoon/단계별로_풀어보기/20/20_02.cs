using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 18
이름 : 배성훈
내용 : 카드 2
    문제번호 : 2164번

    다른 사람들 풀이를 보니 해당 문제의 로직을 풀어서 제출했다
    입력 받은 숫자 : n
    그리고 반환할 숫자는 2^t > n 인 t의 최솟값에 대해
    2 * n - t
*/

namespace BaekJoon._20
{
    internal class _20_02
    {

        static void Main2(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            Queue<int> queue = new Queue<int>(len);

            // 숫자 채워 넣기
            for (int i = 1; i <= len; i++)
            {

                queue.Enqueue(i);
            }


            // 1장 남을때까지 계속한다
            while (queue.Count > 1)
            {

                // 처음은 꺼내고
                queue.Dequeue();

                // 다음은 맨 밑으로 이동 
                int num = queue.Dequeue();
                queue.Enqueue(num);

                // 그래서 실제로 1회당 1장씩 줄어든다
            }

            Console.WriteLine(queue.Dequeue());
        }
    }
}
