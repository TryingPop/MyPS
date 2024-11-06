using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 지금 만나러 갑니다
    문제번호 : 18235번

    BFS 문제다
    처음에는 그냥 일반적인 BFS 로하면 4^15 이상 확인할 수 있으니 시간초과나 메모리 초과나 뜰 수 있다
    그래도 되면 좋고 안되면 다른 방법 생각하지 하는 마음으로 제출했다
    그리고 당연하게 메모리 초과로 틀렸다(큐에 많이 담겨서 뜬거 같다)

    그래서 다르게 접근했다
    두 오리를 동시에 점프시키는게 아닌
    턴마다 한 마리씩 점프시켰다 그리고 두 오리가 만나는 점이 있는지 확인했다
    이 경우 시간이 4^M이 아닌 N * M 이된다 
    여기서 N은 이동할 수 있는 범위, M은 턴수가 된다

    해당 방법으로 구현하니 108ms로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0444
    {

        static void Main444(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int[] jump = new int[21];

            for (int i = 0; i <= 20; i++)
            {

                jump[i] = 1 << i;
            }

            Queue<int> q1 = new(input[0]);
            bool[] nMove1 = new bool[input[0] + 1];

            Queue<int> q2 = new(input[0]);
            bool[] nMove2 = new bool[input[0] + 1];

            int[] dir = { -1, 1 };
            int ret = -1;

            q1.Enqueue(input[1]);
            q2.Enqueue(input[2]);
            
            for (int i = 0; i <= 20; i++)
            {

                while(q1.Count > 0)
                {

                    var pos = q1.Dequeue();

                    for (int j = 0; j < 2; j++)
                    {

                        int next = pos + dir[j] * jump[i];
                        if (ChkInValidPos(next)) continue;

                        nMove1[next] = true;
                    }
                }

                while (q2.Count > 0)
                {

                    var pos = q2.Dequeue();

                    for (int j = 0; j < 2; j++)
                    {

                        int next = pos + dir[j] * jump[i];
                        if (ChkInValidPos(next)) continue;

                        nMove2[next] = true;
                    }
                }

                for (int j = 1; j <= input[0]; j++)
                {

                    if (nMove1[j] && nMove2[j])
                    {

                        ret = i + 1;
                        break;
                    }
                    else if (nMove1[j])
                    {

                        nMove1[j] = false;
                        q1.Enqueue(j);
                    }
                    else if (nMove2[j])
                    {

                        nMove2[j] = false;
                        q2.Enqueue(j);
                    }
                }

                if (ret > 0) break;
            }

            Console.WriteLine(ret);

            bool ChkInValidPos(int _x)
            {

                if (_x <= 0 || _x > input[0]) return true;
                return false;
            }
        }
    }
}
