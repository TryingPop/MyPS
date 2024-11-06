using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 11
이름 : 배성훈
내용 : 숨박꼭질 4
    문제번호 : 13913번

    많이 풀어본 숨박꼭질 문제다
    기존에는 BFS 탐색을 따로 함수로 만들어 사용했으나 여기서는 그냥 Main안에 넣었다

    탐색부분 인덱스 문제로 몇번 틀렸다
    읽기 초기에는, for문 밑에 있는 주석의 if - else 구문이다
    
    if (dp[result[i + 1] - 1] == i + 1) result[i] = result[i + 1] - 1
    구문에서 dp에 인덱스가 0이학 가능해 인덱스 에러가 났다
    1 0 을 입력 하면 바로 확인할 수 있다!

    result[i + 1] - 1 이 0 미만이 안되게 주의하면 된다
    pos의 MAX는 10만이고, dp는 이 MAX의 두 배를 저장하기에
    dp에서 인덱스 초과부분은 걱정할 필요가 없다!

    result의 인덱스 역시 걱정할 필요가 없는게 이미 result.Length - 1항은 따로 빼서 했다
    그리고 탐색 부분에서 dp에 1을 시작값으로 했기에 result의 길이는 1이 보장된다!
*/

namespace BaekJoon._35
{
    internal class _35_05
    {

        static void Main5(string[] args)
        {

            // 입력
            int[] pos = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();


            // BFS 탐색
            const int MAX = 100_000;
            int[] dp = new int[2 * MAX + 1];

            Queue<int> q = new Queue<int>();

            dp[pos[0]] = 1;
            q.Enqueue(pos[0]);

            while (q.Count > 0)
            {

                if (dp[pos[1]] != 0) 
                { 
                    
                    q.Clear();
                    break;
                }

                int node = q.Dequeue();

                int cur = dp[node];

                for (int i = 0; i < 3; i++)
                {

                    int next = NextNum(node, i);

                    if (ChkInValid(next, MAX)) continue;
                    if (dp[next] != 0) continue;
                    dp[next] = cur + 1;
                    q.Enqueue(next);
                }
            }

            // dp 역으로 읽기
            int[] result = new int[dp[pos[1]]];
            Array.Fill(result, -1);
            result[dp[pos[1]] - 1] = pos[1];
            for (int i = dp[pos[1]] - 2; i >= 0; i--)
            {

                for (int j = 0; j < 2; j++)
                {

                    int next = NextNum(result[i + 1], j);
                    if (ChkInValid(next, MAX)) continue;
                    if (dp[next] != i + 1) continue;

                    result[i] = next;
                    break;
                }

                if (result[i] == -1) result[i] = result[i + 1] / 2;

                // if (dp[result[i + 1] - 1] == i + 1) result[i] = result[i + 1] - 1;
                // else if (dp[result[i + 1] + 1 == i + 1) result[i] = result[i + 1] + 1;
                // else result[i] = result[i + 1] / 2;
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(dp[pos[1]] - 1);

                for (int i = 0; i < result.Length; i++)
                {

                    sw.Write(result[i]);
                    sw.Write(' ');
                }
            }
        }

        static bool ChkInValid(int _chk, int _MAX)
        {

            if (_chk < 0 || _chk > 2 * _MAX) return true;

            return false;
        }

        static int NextNum(int _num, int _idx)
        {

            switch (_idx)
            {

                case 0:
                    return _num - 1;

                case 1:
                    return _num + 1;

                default:
                    return 2 * _num;
            }
        }
    }
}
