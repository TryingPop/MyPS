using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 17
이름 : 배성훈
내용 : 큐 2
    문제번호 : 18258번

    문제 수가 200만개라서 시간복잡도를 1로 만들어야한다
    그럴러면 큐의 마지막 원소를 찾는 Last를 이용하면 안된다
    마지막 원소는 반복기로 호출하기에 시간복잡도가 n으로 올라간다
    그래서 마지막 변수만 따로 담는 last변수를 할당했다

    빠르게 하고 싶다면 200만 개의 값을 받을 수 있는 배열을 설정하고
    큐의 Push와 Pop을 정의해야한다
*/

namespace BaekJoon._20
{
    internal class _20_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());

            Queue<string> que = new Queue<string>(len);
            StringBuilder sb = new StringBuilder();

            int last = 0;               // que의 마지막 원소
            for (int i = 0; i < len; i++)
            {

                string[] input = sr.ReadLine().Split(' ');

                if (input[0] == "push")
                {

                    que.Enqueue(input[1]);
                    last = int.Parse(input[1]);         // FIFO 이므로 마지막에 입력한 것이 가장 뒤에 있다
                }
                else if (input[0] == "pop")
                {

                    if (que.Count > 0)
                    {
                        
                        sb.AppendLine(que.Dequeue());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
                else if (input[0] == "size")
                {

                    sb.AppendLine(que.Count.ToString());
                }
                else if (input[0] == "empty")
                {

                    if (que.Count > 0)
                    {

                        sb.AppendLine("0");
                    }
                    else
                    {

                        sb.AppendLine("1");
                    }
                }
                else if (input[0] == "front")
                {

                    if (que.Count > 0)
                    {

                        sb.AppendLine(que.Peek());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
                else if (input[0] == "back")
                {

                    if (que.Count> 0)
                    {
                        // sb.AppendLine(que.Last().ToString());    // 해당 메소드를 찾아보니 리스트로 형변환해서 찾거나
                                                                    // 혹은 다음항목 일일히 검색해가며 찾는다
                                                                    // 실제로 해당 부분 바꾸기 전에는 시간초과 떴다
                        sb.AppendLine(last.ToString());
                    }
                    else
                    {
                        sb.AppendLine("-1");
                    }
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
