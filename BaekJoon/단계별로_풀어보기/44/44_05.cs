using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 29
이름 : 배성훈
내용 : 도로 네트워크
    문제번호 : 3176번

    거리 측정을 잘못해서 3%에서 계속 틀렸다
    로직이 맞다는 가정하에 다른 사람 팁을 보면서 조금씩 바꾸다가(최대 최소 범위가 잘못되었는가?, 아니면 같은 노드가 있는가?) 4번 틀렸고, 
    이후 로직이 잘못되었음을 인정하고 하나하나 살펴갔다

    그래서 찾아보니, 2^n 최소, 최대 거리 자료를 넣을 때, 잘못 넣었음이 확인되었다
    예를 들어 4(2^2)번째 부모를 비교한다면
        주석되어져 있는 부분은 0 -> 1번째 이동한 최대 최소와, 2 -> 4번째 이동한 최대 최소를 비교해놓고
        0 -> 4번째 이동한 최대 최소라고 주장하는 꼴이었다
        1 -> 2의 최대 최소도 비교해야 하는데도 말이다!
    그래서 수정하니 이상없이 통과되었다

    주된 로직은 다음과 같다
    기존에는 부모 노드만 저장했으나, 여기서는
    해당 부모로 이동하는 최대 거리, 최소 거리를 같이 보관한다!
    저장 방법은 다음과 같다
        0 ~ 2^n의 최소 거리는 0 ~ 2^(n - 1)의 최소와 2^(n - 1) ~ 2^n 의 최소를 비교하여 넣은 것이 된다
        최대도 마찬가지이다
        그래서 비교하면서 넣었다

    그리고 찾아갈 때 depth를 맞춰 가는데, 맞춰가면서 이동한 것이므로! 최대 최소를 갱신하며 이동했다
    그리고 LCA를 찾으로 같이 이동할 때도 최대 최소를 갱신하며 이동했다!

    앞과 달리 여기서는, 자기자신으로 가는 것은 저장하지 않았다 비슷한 유형을 풀어보니, 메모리 낭비라고 느껴졌기 때문이다!
*/

namespace BaekJoon._44
{
    internal class _44_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 노드 수
            int len = int.Parse(sr.ReadLine());

            // 간선 입력 받을 준비
            List<(int dst, int weight)>[] lines = new List<(int dst, int weight)>[len + 1];
            for (int i = 1; i <= len; i++)
            {

                lines[i] = new();
            }

            // 간선 입력받는다
            for (int i = 1; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add((temp[1], temp[2]));
                lines[temp[1]].Add((temp[0], temp[2]));
            }

            // 최대 2^n 부모 찾기
            int log = (int)Math.Log2(len) + 1;
            // 부모 해당 부모 노드 번호 이동 시 최소 가중치, 이동 시 최대 가중치
            (int parent, int min, int max)[,] dp = new (int parent, int min, int max)[len + 1, log];
            int[] depth = new int[len + 1];

            // BFS 탐색
            Queue<int> q = new Queue<int>();
            // 루트를 1로 설정
            q.Enqueue(1);

            while(q.Count > 0)
            {

                int node = q.Dequeue();
                int curDepth = depth[node];

                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i].dst;
                    // 부모로 가는 길이면 넘긴다
                    if (dp[node, 0].parent == next) continue;

                    int weight = lines[node][i].weight;
                    
                    dp[next, 0] = (node, weight, weight);

                    for (int s = 1; s < log; s++)
                    {

                        // 2^(n - 1) 에서 2^n 이동!
                        // 2^(n - 1)부모의 2^(n - 1)부모가 있는지 검색
                        var chk = dp[dp[next, s - 1].parent, s - 1];
                        if (chk.parent == 0) break;

                        // 여기서 문제 생겼었다!
                        // 시작지점에서 2^(n - 1) 이동
                        var calc = dp[next, s - 1];
                        dp[next, s] = (chk.parent, Math.Min(chk.min, calc.min), Math.Max(chk.max, calc.max));
                        // 해당 코드로 계속 틀렸다! 
                        // 0 -> 2^0으로 이동한 최소거리랑 2^(n - 1) -> 2^n으로 이동하는 최소거리 둘만 비교해놓고
                        // 0 -> 2^n으로 이동한 최소거리랑 같다고 해서 틀렸다!
                        // 비교하려면 0 -> 2^(n - 1)의 최소거리랑 2^(n - 1) -> 2^n으로 이동하는 거리로 해야한다!
                        // dp[next, s] = (chk.parent, Math.Min(chk.min, weight), Math.Max(chk.max, weight));
                    }

                    depth[next] = curDepth + 1;

                    q.Enqueue(next);
                }
            }


            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                int min = 1_000_001;
                int max = 0;

                // 혹시 몰라서 같은 노드일 수 도 있으니, 0으로 바꾼다!
                if (temp[0] == temp[1]) min = 0;

                // 깊이 맞추기!
                while (depth[temp[0]] != depth[temp[1]])
                {

                    int diff = depth[temp[0]] - depth[temp[1]];

                    if (diff > 0)
                    {

                        int calc = 1;
                        int up = -1;
                        while (diff >= calc)
                        {

                            calc *= 2;
                            up++;
                        }

                        var chk = dp[temp[0], up];
                        temp[0] = chk.parent;
                        min = Math.Min(min, chk.min);
                        max = Math.Max(max, chk.max);
                    }
                    else
                    {

                        diff = -diff;

                        int calc = 1;
                        int up = -1;
                        while (diff >= calc)
                        {

                            calc *= 2;
                            up++;
                        }

                        var chk = dp[temp[1], up];
                        temp[1] = chk.parent;
                        min = Math.Min(min, chk.min);
                        max = Math.Max(max, chk.max);
                    }
                }

                // 깊이가 같아졌으므로 LCA까지 함께 올라가기!
                // 이거 찾아보니 for문 역순으로 돌리는게 매우 좋아보인다!
                while (temp[0] != temp[1])
                {

                    int up = 0;

                    for (int i = 0; i < log; i++)
                    {

                        if (dp[temp[0], i].parent == dp[temp[1], i].parent) break;
                        up = i;
                    }

                    var chk1 = dp[temp[0], up];
                    var chk2 = dp[temp[1], up];

                    temp[0] = chk1.parent;
                    temp[1] = chk2.parent;

                    min = Math.Min(Math.Min(chk1.min, chk2.min), min);
                    max = Math.Max(Math.Max(chk1.max, chk2.max), max);
                }

                sw.Write(min);
                sw.Write(' ');
                sw.Write(max);
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }
    }
}
