using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 공격
    문제번호 : 1430번

    기존의 x, y 좌표 거리로 적을 직접적으로 공격 가능한지 확인한다
    그리고 공격 거리를 확인하면 이제 최소 몇 단계 거쳐야 공격가능한지 확인한다
    해당 방법을 BFS로 하는 방법이 있으나, n이 50이하니 
    각 좌표간 연결되어져 있는지 확인하고 플로이드 워셜로 최단경로로 이어줘서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0197
    {

        static void Main197(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            double atkRange = ReadInt(sr);
            int hp = ReadInt(sr);

            int x = ReadInt(sr);
            int y = ReadInt(sr);

            (int x, int y)[] towers = new (int x, int y)[n];

            for (int i = 0; i < n; i++)
            {

                towers[i] = (ReadInt(sr), ReadInt(sr));
            }

            sr.Close();

            // n <= 50이므로
            // 플로이드 워셜로 몇 단계 떨어졌는지 확인
            int[,] fw = new int[n + 1, n + 1];

            for (int i = 0; i < n; i++)
            {

                for (int j = i + 1; j < n; j++)
                {

                    // 최대 거리는 50을 넘지 못한다
                    // 공격 거리 안에 있는 경우 1 아닌 경우 MAX이다
                    fw[i, j] = GetDis(towers[i], towers[j], atkRange) ? 1 : 100;
                    // 교환법칙 성립!
                    fw[j, i] = fw[i, j];
                }
            }

            for (int i = 0; i < n; i++)
            {

                // n은 적과의 거리다!
                fw[n, i] = GetDis(towers[i], (x, y), atkRange) ? 1 : 100;
                fw[i, n] = fw[n, i];
            }

            // 최단 거리 찾는다
            // 최단 전달은 몇단계거쳐야 가능한지 플로이드 워셜로 확인!
            for (int mid = 0; mid <= n; mid++)
            {

                for (int start = 0; start <= n; start++)
                {

                    // start -> mid로 존재 X면 다음으로
                    if (fw[start, mid] == 100) continue;
                    for (int end = 0; end <= n; end++)
                    {

                        // mid -> end가 존재 안하는 경우
                        if (fw[mid, end] == 100) continue;

                        int calc = fw[start, mid] + fw[mid, end];
                        // 최단 거리여야만 갱신한다
                        if (calc < fw[start, end]) fw[start, end] = calc;
                    }
                }
            }

            double ret = 0;
            for (int i = 0; i < n; i++)
            {

                // 100인 경우 넘긴다 안넘겨도 10^-10 보다 작기에 연산하던 말던 오차 범위 안이다
                if (fw[i, n] == 100) continue;
                // hp깎기
                // 적과의 거리는 fw[i, n] - 1이다
                // 100은 다리 건너서도 적 공격 불가능!
                ret += hp * Math.Pow(0.5, fw[i, n] - 1);
            }

            Console.WriteLine($"{ret:0.0#}");
        }

        static bool GetDis((int x, int y) _pos1, (int x, int y) _pos2, double _dis)
        {

            double ret = Math.Sqrt((_pos1.x - _pos2.x) * (_pos1.x - _pos2.x) + (_pos1.y - _pos2.y) * (_pos1.y - _pos2.y));
            return ret <= _dis;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
import heapq
import sys
import math
input =sys.stdin.readline
n, r, d, x, y = map(int, input().split())
tower = []
total = 0
graph = { i : [] for i in range(n+1)}
vi_map = [False] * (n+1)
tower.append([x, y])
for i in range(n):
    tower.append(list(map(int, input().split())))
for i in range(len(tower)):
    for j in range(i+1, len(tower)):
        x1, y1 = tower[i]
        x2, y2 = tower[j]
        dis = math.sqrt(abs(x1-x2)**2 + abs(y1-y2)**2)
        if dis <= r:
            graph[i].append([dis,j])
            graph[j].append([dis,i])

def bfs():
    global total
    queue = [[0, 0, 0]]
    vi_map[0] = True
    while queue:
        t, dis, id = heapq.heappop(queue)
        for _dis, _nex in graph[id]:
            if not vi_map[_nex]:
                vi_map[_nex] = True
                total += d * (2 ** -t)
                heapq.heappush(queue, [t+1, _dis, _nex])
bfs()
if isinstance(total, int):
    print(f'{total:.1f}')
else:
    print(total)
#endif
}
