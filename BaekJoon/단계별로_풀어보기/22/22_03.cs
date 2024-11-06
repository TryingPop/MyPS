using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 약수들의 합
    문제번호 : 9506번
*/

namespace BaekJoon._22
{
    internal class _22_03
    {

        static void Main3(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            {

                int input, len;
                int sum;
                Queue<int> que = new Queue<int>();
                while (true)
                {

                    // 값 입력
                    input = int.Parse(Console.ReadLine());

                    // 탈출 확인
                    if (input == -1) break;

                    // 초기 변수 세팅
                    len = input / 2 + 1;
                    sum = 0;

                    que.Clear();

                    // 약수 찾기
                    for (int i = 1; i < len; i++)
                    {

                        if (input % i == 0)
                        {

                            que.Enqueue(i);
                            sum += i;
                        }
                    }

                    // 완전수이면
                    if (sum == input)
                    {

                        sb.Append($"{input} = ");

                        while (que.Count > 0)
                        {

                            sb.Append(que.Dequeue().ToString());

                            if (que.Count != 0)
                            {

                                sb.Append(" + ");
                            }
                            else
                            {

                                sb.Append("\n");
                            }
                        }
                    }
                    // 완전수가 아니면
                    else
                    {

                        sb.Append($"{input} is NOT perfect.").Append("\n");
                    }
                }

                // 출력
                using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                {

                    sw.WriteLine(sb);
                }
            }
        }
    }
}
