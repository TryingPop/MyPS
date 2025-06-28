using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 주차장
    문제번호 : 5464번

    상황을 구현하는 시물레이션 문제다
    주차 상황을 구현했다
    다만 나가고 다음 차를 넣는 과정에서 넣었다고 표현을 안해줘서 2번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0108
    {

        static void Main108(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int[] parkCost = new int[n];
            for (int i = 0; i < n; i++)
            {

                parkCost[i] = ReadInt(sr);
            }

            int[] weight = new int[m + 1];

            for (int i = 1; i <= m; i++)
            {

                weight[i] = ReadInt(sr);
            }

            // 전체 차량 수 * 차량의 최대 무게 * 무게당 요금 <= 20억
            int total = 0;
            int[] park = new int[n];
            Queue<int> q = new Queue<int>(m);
            for (int i = 0; i < 2 * m; i++)
            {

                int cur = ReadInt(sr);
                if (cur > 0)
                {

                    // 차가 들어오는 경우
                    bool isEmpty = false;
                    for (int j = 0; j < n; j++)
                    {

                        // 주차 공간이 있는지 판별
                        if (park[j] == 0)
                        {

                            // 주차공간이 있어 주차
                            park[j] = cur;
                            isEmpty = true;
                            total += parkCost[j] * weight[cur];
                            break;
                        }
                    }

                    // 주차공간이 없다면 대기
                    if (!isEmpty) q.Enqueue(cur);
                }
                else
                {

                    // 차를 뺀다
                    cur = -cur;
                    for (int j = 0; j < n; j++)
                    {

                        // 빼는 공간 확인
                        if (park[j] == cur)
                        {

                            // 뺀다
                            park[j] = 0;

                            // 대기차가 들어와야하는지 확인
                            // 가득 찬 경우 q가 차게된다!
                            if (q.Count > 0) 
                            { 
                                
                                // 다음차가 여기에 주차한다
                                cur = q.Dequeue();
                                total += parkCost[j] * weight[cur];
                                park[j] = cur;
                            }

                            break;
                        }
                    }
                }
            }

            // 문제 조건에서 대기하다가 주차못하고 가는 차가 없다고 했다
            // 그래서 q를 따로 조사 안한다
            sr.Close();

            // 결과 출력
            Console.WriteLine(total);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }
                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
