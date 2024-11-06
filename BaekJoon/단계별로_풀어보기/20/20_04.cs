using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 18
이름 : 배성훈
내용 : 프린터 큐
    문제번호 : 1966번
*/
namespace BaekJoon._20
{
    internal class _20_04
    {

        static void Main4(string[] args)
        {

            // 문제 개수
            int len = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                // 0 : 입력 받을 수열의 길이, 1 : 찾을 인덱스
                int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                // 수열 입력
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int count = 0;
#if queue
                Queue<int> que = new Queue<int>(info[0]);
                Queue<bool> chk = new Queue<bool>(info[0]);
                for (int j = 0; j < info[0]; j++)
                {

                    que.Enqueue(inputs[j]);
                    if (j == info[1])
                    {

                        chk.Enqueue(true);
                    }
                    else
                    {

                        chk.Enqueue(false);
                    }
                }

                while (true)
                {

                    if (que.Peek() == que.Max())
                    {

                        que.Dequeue();
                        count++;
                        
                        if (chk.Dequeue())
                        {

                            break;
                        }
                    }
                    else
                    {

                        que.Enqueue(que.Dequeue());
                        chk.Enqueue(chk.Dequeue());
                    }
                }
#else

                int last = -1;

                /*
                // 해당 논리로는
                // last 위치가 정확하지 않아서 틀렸다!
                for (int j = 9; j > inputs[info[1]]; j--)       // 이 for문이 문제이다
                {

                    // 중요도 최대치 9부터 시작해서 찾을 문서의 중요도 + 1 까지 검색
                    for (int k = 0; k < inputs.Length; k++)
                    {

                        // 중요도가 큰 문서 나오면
                        if (inputs[k] == j)
                        {

                            // 인쇄
                            count++;
                            // 마지막으로 제거한 인덱스 등록
                            last = k;
                        }
                    }
                }
                */
                int max = inputs.Max();

                int idx = 0;
                // 찾을 문서보다 중요도 큰 값 검색
                // 값을 0으로 대체했기 때문에 빠진 것도 검사한다
                // 그래서 속도가 느리다
                while (max > inputs[info[1]])
                {

                    if (inputs[idx] == max)
                    {

                        inputs[idx] = 0;
                        count++;
                        last = idx;
                        max = inputs.Max();
                    }

                    idx++;
                    if (idx == inputs.Length)
                    {

                        idx = 0;
                    }
                }


                // 마지막으로 제거한 인덱스의 위치 확인
                if (last < info[1])
                {

                    // 마지막으로 제거한 번호가 찾을 문서의 인덱스보다 작은 경우
                    // 마지막으로 제거한 번호와 찾을 문서의 인덱스 사이에 값이 같은거만 인쇄하면 된다
                    for (int j = last + 1; j <= info[1]; j++)
                    {

                        // inputs[info[1]]의 중요도가 가장 크다
                        // 그래서 같은 것들 제거
                        if (inputs[info[1]] == inputs[j])
                        {

                            count++;
                        }
                    }
                }
                else
                {

                    // 반면 큰 경우 마지막에 제거한 인덱스 뒤에서부터 시작해서 끝까지 가야한다
                    for (int j = last + 1; j < inputs.Length; j++)
                    {

                        if (inputs[info[1]] == inputs[j])
                        {

                            count++;
                        }
                    }

                    // 끝까지 갔으므로 0번부터 시작
                    for (int j = 0; j <= info[1]; j++)
                    {

                        if (inputs[info[1]] == inputs[j])
                        {

                            count++;
                        }
                    }
                }
#endif

                sb.AppendLine(count.ToString());
            }

            Console.WriteLine(sb);
        }
    }
}
