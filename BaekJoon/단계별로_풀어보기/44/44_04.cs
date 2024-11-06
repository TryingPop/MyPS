using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 29
이름 : 배성훈
내용 : LCA 2
    문제번호 : 11438번

    44_03이 LCA와 관련된 문제인줄 알았으나 관련없는 문제라 해당 문제 먼저 풀었다
    주된 아이디어는 다음과 같다 탐색 시간을 N에서 logN으로 줄이는 것이다

    44_02와 바뀐 점은 저장할 때, 2제곱 부모들을 함께 저장하는 것이다 
    즉 해당 노드의 0번째 부모, 1(2^0)번째 부모, 2(2^1)번째 부모, 4(2^2)번째 부모, 8(2^3)번째 부모, ..., 2^n번째 부모를 저장한다
    부모가 2^n번째 부모가 없을 시 넣지 않는다
    이에 가능한 2^n 부모의 최대값부터 찾아해당 값만큼 배열을 할당해야한다
    그리고 0번째 부모는 자기자신이다!
    
    2^n 부모들을 모두 저장했으면, 깊이 맞추기와 부모 찾을 때 이용해야한다
    먼저 깊이 맞추기는 기존에 1칸씩 이동하는 것을 깊이 차 diff 보다 작은 2^n 부모들로 바로 이동할 수 있다0

    예를들어 깊이차가 6이라면 깊이가 큰 노드의 2^2(4)부모까지 바로 뛰어 깊이차를 2칸으로 만든 뒤  2^1(2)부모로 뛰어 이동하면 깊이차가 6으로 맞춰진다
    6번 연산이 2번으로 줄었다
    만약 깊이차가 1000이면, 2^9(512) 부모로 뛰어 깊이차를 488로 줄이고, 다시 2^8(256)부모로 뛰어 깊이차를 232로 줄이고,
        2^7(128)부모로 뛰어 깊이차를 104로 줄이고, 2^6(64)부모로 뛰어 깊이차를 40으로 줄이고, 2^5(32)부모로 뛰어 깊이차를 8로 줄이고,
        2^3(8)부모로 뛰면 깊이차가 0이된다

    즉, 한칸씩 이동하면 1000번 해야할 연산이 6번으로 엄청나게 줄어든다
    그리고, 2^n부모는 깊이차 줄이는데 사용할 뿐만 아니라 LCA 맞추는데도 쓸 수 있다
    이는 이진탐색을 응용한 것이라 보면 된다

    예를들어 두 노드 A, B는 같은 깊이를 갖고, 깊이가 512(2^9) 이상에, 서로의 500번째 부모가 LCA라 하자
        그러면 최소 공통 조상 LCA가 500번째 부모이므로
        n이 1 ~ 8일 때, 2^n 은 500보다 작고 이에 A, B의 1 ~ 8의 2^n 부모들은 모두 다를것이다.
        그러나 500번째 부모가 같으므로 501번째, ..., 512번째 부모는 같을 수 밖에 없다 즉, 2^9 부모는 같다

            이로부터 1 ~ n - 1 까지의 m에대해 두 노드 C, D의 2^m부모는 다르고 2^n 부모에서 같다면,
            C, D의 LCA는 2^n - 1 ~ 2^n부모 사이에 있다고 볼 수 있다

        그리고 A, B 둘 다 각가의 2^8 부모인 노드로 바꾼다
        그러면 A, B 의 LCA는 244(500 - 2^8)번째 부모일 것이다
        해당 과정을 반복해서 244 -> 116 -> 52 -> 20 -> 4 -> 2 -> 1까지 줄인다
        여기서 A != B 이고 2^0(1)번째 부모는 같을 때, 무한루프에 걸릴 수 있어 최소 1번은 이동하게 해야한다!
        그러면 마지막에 찾은 노드가 LCA가 됨을 알 수 있다

    이를 코드로 표현한 것이 아래의 코드이다
    해당 방법으로 44_02를 푸니 152ms로 10배이상 줄여졌다!
*/

namespace BaekJoon._44
{
    internal class _44_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 최대 노드 수
            int len = int.Parse(sr.ReadLine());

            // 최대 2^n 부모 값 찾기
            int log = (int)Math.Log2(len) + 2;
            // 부모를 담을 배열,
            // log = 0일때는 자기자신
            // log = 1일때는 2^0(1)번째 부모
            // log = n일때는 2^n - 1번째 부모
            int[,] parents = new int[len + 1, log];
            // 깊이(루트에서 떨어진 정도)를 담을 배열
            int[] depth = new int[len + 1];
            // 간선을 담을 배열
            List<int>[] lines = new List<int>[len + 1];
            for (int i = 1; i <= len; i++)
            {

                lines[i] = new();
            }

            // 간선 입력
            for (int i = 1; i < len; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');
                int f = int.Parse(temp[0]);
                int b = int.Parse(temp[1]);

                lines[f].Add(b);
                lines[b].Add(f);
            }

            // 부모 탐색
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            parents[1, 0] = 1;
            while(q.Count > 0)
            {

                int node = q.Dequeue();
                int curDepth = depth[node];

                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i];
                    if (parents[node, 1] == next) continue;

                    depth[next] = curDepth + 1;
                    parents[next, 0] = next;
                    parents[next, 1] = node;
                    // 2^n부모를 담는다
                    for (int sec = 2; sec < log; sec++)
                    {

                        // 2^n번째 부모의 2^n번째 부모가 자신의 2^(n + 1)번째 부모가 된다
                        int chk = parents[parents[next, sec - 1], sec - 1];
                        // 없는 경우 탈출
                        if (chk == 0) break;
                        parents[next, sec] = chk;
                    }
                    q.Enqueue(next);
                }
            }

            // 이제 찾을거 입력받기!
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                int[] find = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 먼저 깊이 맞추기
                while (depth[find[0]] != depth[find[1]])
                {

                    // 깊이차
                    int diff = depth[find[0]] - depth[find[1]];

                    if (diff > 0)
                    {

                        // find[0]의 깊이가 큰 경우
                        int calc = 1;
                        int up = 0;
                        
                        while (calc <= diff)
                        {

                            calc *= 2;
                            up++;
                        }
                        // log범위를 잘못 잡으면 벗어난다!
                        // 처음에 log = (int)Math.Log2(len) + 1;로 했으면
                        // 마지막 2^n의 n번째 부모가 안잡힌다!
                        // if (up >= log) up = log - 1;
                        find[0] = parents[find[0], up];
                    }
                    else
                    {

                        // find[1]의 깊이가 큰 경우
                        // 방법은 위와 같다
                        diff = -diff;
                        int calc = 1;
                        int up = 0;

                        while(calc <= diff)
                        {

                            calc *= 2;
                            up++;
                        }
                        // if (up >= log) up = log - 1;
                        find[1] = parents[find[1], up];
                    }
                }

                // 깊이를 맞췄으므로 이젠 최소 공통 부모를 찾는다
                // 현재 달라야 while문에 들어간다
                while (find[0] != find[1])
                {

                    // 최소 1번은 이동!
                    int chk = 1;
                    // i == 0 은 while문 처음에 한다!
                    for (int i = 1; i < log; i++)
                    {

                        // 바로 앞번 은 다르고, 현재 같을 경우 탈출
                        if (parents[find[0], i] == parents[find[1], i]) break;
                        chk = i;
                    }

                    // 이동
                    find[0] = parents[find[0], chk];
                    find[1] = parents[find[1], chk];
                }

                // 둘이 같으므로 아무거나 출력했다
                sw.Write(find[0]);
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }
    }
}
