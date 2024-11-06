using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 2
이름 : 배성훈
내용 : 타임머신
    문제번호 : 11657번

    벨만 - 포드 알고리즘 문제!

    벨만 - 포드 알고리즘 구현은 어렵지 않은데 오버(언더)플로우가 주된 문제 인거 같다
    ... 

    V : 점의 개수, E : 간선의 개수, W : 가중치(거리)
    우선 벨만 - 포드 알고리즘의 특징은
        기본 다익스트라 알고리즘보다 연산을 많이한다! O(ElogE) = O(ElogV) < O(EV)
        다만 다익스트라는 W가 양수가 보장 될때에만 이용 가능한 알고리즘인 반면에 벨만 - 포드는 W가 음수여도 상관없다
    위 두개라고 본다

    구현은 다음과 같다
        1. 우선 기본 테이블을 만든다
            시작 지점 값 거리 0, 이외는 MAX 값을 세팅한다
        2. 모든 점들의 경로를 계산한다
            현재 지나가지 않은 점의 경우는 계산하지 않는다
            지나간 곳인 경우면 거리를 계산해서 최단 경로이면 갱신한다
        3. 2번 과정을 V - 1번 실시할 때 까지 반복한다 
            먼저 음의 사이클이 존재하지 않는 경우를 보면 모든 점에 대해 많아야 한 번만 지나는 경로만이 최단 거리 후보가 될 수 있다
                그래서 V - 1번 돌리면 모든 후보들이 나온다
            음의 사이클이 존재하는 경우면 음의 사이클 점들을 반복해서 지나갈수록 가중치가 낮아지기에 음의 사이클이 갈 수 있는 점들은 계속해서 값이 낮아진다
                그래서 음의 사이클에 있는 점들이 지나갈 수 있는 점들은 계속해서 값이 낮아질 수 있다
                다만 음의 사이클이 지나갈 수 없는 점에 한해서는 최단 경로가 찾아진다

        1 ~ 3 번까지 진행했다면 음의 사이클과 엮이지 않은 최단 경로는 전부 구했다
        4. 2번을 한 번 더 반복한다
            여기서 값이 갱신되면 값이 줄어든 경우인데, 이는 음의 사이클이 존재함을 의미한다
            여기서 값이 갱신되지 않는다면 음의 사이클은 없다고 봐도 된다
        
            만약 음의 사이클이 있는데, 해당 점들의 모든 값을 찾고 싶다면, V - 1번실행한 표 a와 여기서 V번 더 반복 실행해서 나오는 표 b
            를 비교해서 값이 변한 것들은 음의 사이클에 영향 받는 것이라 판단을 내릴 수 있다
            값이 안변했다면 해당 경로가 최단 경로라 보장 가능!

    1번의 사이클당 가질 수 있는 값은 V * W_max = 500 * 10,000 = 5,000,000보다 작다
    그래서 v = 500, w = 10000 초기에 MAX값을 잡기로 int로 5_000_000으로 잡았었는데 틀렸다

    찾아보니 최소값은 극단적으로 음의 경로를 모두 가는 E * W_min = 6,000 * -10,000 = -60,000,000이고,
    이는 2번 1회당 내려갈 수 있는 최소값이다 이게 500번 반복되면 500 * -60,000,000 = -300억을 내려갈 수 있고
    int.MinValue(약 -21억) 보다 아득히 작은 값이다. 즉, 앞에서 말했듯이 int를 무턱대고 쓰면 언더 플로우가 발생한다
    
    -6000만은 음의 사이클의 존재가 확실한 경우고 없는 경우라 해도 V * W_min = -4,950,000이고,
    -4,950,000 * 499 = -24,700,500,000 < int.MinValue 로 마찬가지다!

    그리고 앞에서 경험하기로 long = int + int; 인 경우 
    백준 컴파일러에서는 int + int를 int연산을 하고 long 변환을 해서 잘못된 정답을 도출했던 경험이 있다
    그래서 거리를 저장할 변수를 long으로 했다

    그래서 MAX변수를 long 5_000_000으로 잡아도 이상없다
*/

namespace BaekJoon._33
{
    internal class _33_05
    {

        const long MAX = 5_000_000;

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            List<(int dst, long dis)>[] root = new List<(int dst, long dis)>[info[0] + 1];

            for(int i = 1; i < info[0] + 1; i++)
            {

                root[i] = new();
            }

            // 조건에서 출발지점 도착지점이라고 표현했고 이는 단방향이다
            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                root[temp[0]].Add((temp[1], temp[2]));
            }

            sr.Close();

            long[] dis = new long[info[0] + 1];
            // 여기서 생긴 문제로 쓸모없는 코드를 많이 수정했다
            // MAX + 1 으로 넣어놓고 벨만 포드에서 MAX로 찾고 있으니 이상하게 갔다;
            Array.Fill(dis, MAX);

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 음의 사이클이 잇는지 없는지 판별
            if (BellmanFord(root, info, dis))
            {

                for (int i = 2; i <= info[0]; i++)
                {

                    // 경로가 없으면 -1
                    if (dis[i] == MAX) sw.WriteLine(-1);
                    // 있으면 해당 값
                    else sw.WriteLine(dis[i]);
                }
            }
            else sw.WriteLine(-1);

            sw.Close();
        }

        static bool BellmanFord(List<(int dst, long dis)>[] _root, int[] _info, long[] _dis)
        {

            // 시작값 1로 세팅
            _dis[1] = 0;

            // cycle = _info[0]일 때는, 음의 사이클이 존재하는지 판별용이다!
            for (int cycle = 1; cycle <= _info[0]; cycle++)
            {

                // 정점의 개수 - 1 = _info[0] - 1번이면 충분하다
                for (int i = 1; i <= _info[0]; i++)
                {

                    // 아직 간선으로 도착하지 못한 곳이면 넘긴다!
                    if (_dis[i] == MAX) continue;

                    long cur = _dis[i];
                    // 도착된 곳에 한해 이동 시작
                    for (int j = 0; j < _root[i].Count; j++)
                    {

                        int next = _root[i][j].dst;
                        long dis = _root[i][j].dis + cur;

                        // 최단 경로 갱신?
                        if (_dis[next] > dis)
                        {

                            // 갱신
                            _dis[next] = dis;

                            // 음의 사이클이 존재하는지 확인용!
                            if (cycle == _info[0]) return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
