using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 18
이름 : 배성훈
내용 : 요세푸스 문제0
    문제번호 : 11866번
*/

namespace BaekJoon._20
{
    internal class _20_03
    {

        static void Main3(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            
            StringBuilder sb = new StringBuilder();

            Queue<int> que = new Queue<int>(inputs[0]);
            
            // 숫자 채워넣기
            for (int i = 1; i <= inputs[0]; i++)
            {

                que.Enqueue(i);
            }

            // 답안 조건에 맞춰서 넣은 구문
            sb.Append("<");

            // 1장 남을 때까지 계속
            while (que.Count > 1)
            {

                // 매 inputs[i]번째를 빼야한다
                for (int i = 1; i < inputs[1]; i++)
                {

                    // 원형이므로 inputs[i] - 1회만 넣고 뺏다를 반복
                    que.Enqueue(que.Dequeue());
                }

                // inputs[i]번째 제외되고 전체 크기가 1 줄어든다
                sb.Append($"{que.Dequeue().ToString()}, ");
            }

            // 마지막은 답안 조건에 맞춰서 입력
            sb.Append($"{que.Dequeue()}>");

            // 출력
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
