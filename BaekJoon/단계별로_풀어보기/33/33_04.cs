using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
날짜 : 2023. 1. 2
이름 : 배성훈
내용 : 미확인 도착지
    문제번호 : 9370번

    **********************************
    거리가 양수이므로 다익스트라! 이용
    **********************************

    초기화 부분에서 많이 틀렸다
    j = 1; j <= info[0]로 했어야 하는데, j = 0; j < info[0]으로 해서 쓸모없는 0항을 초기화하고, 
    꼭 해야하는 info[0]항을 초기화 안해서 많이 틀렸었다

    무턱대고 특정한 최단 경로 문제로 풀면 안된다!
        극단적인 예를 들어 점이 4개 1, 2, 3, 4, 5이고 
        시작은 1, 끝은 5,
        그리고 2와 4 사이의 간선을 지나가는지 확인해야한다

        그리고 주어진 간선이 
           시작,  목적지,  거리
            1,      2,      1
            2,      3,      2
            2,      4,      10
            3,      4,      3
            4,      5,      1

        이라 가정하면
        1 -> 5로 가는 최단 거리 7은 1 -> 2 -> 3 -> 4 -> 5 를 갔을 경우 이다
        이 경우 33_02로 풀면 존재한다고 나온다 그러나 문제 조건에 의하면 2와 4 끝점으로하는 간선을 지나가지는 않는다
    
    그런데 특정한 최단 경로 문제 풀이도 이 문제를 푸는 방법 중 하나이다!

    이는 조건 중 2와 4를 지나는 간선이 목적지 후보들 중 적어도 1개로 향하는 최단 경로라는 조건에 의해 
    특정한 최단 경로 문제로 풀어도 이상이 없다! 그러나 다익스트라를 3번이나 하기 때문에, 시간이 더 걸린다

    그러나 짝수홀수 아이디어를 보고 해당 방법으로 풀었다
    아이디어는 간단하다 지나야하는 도로만 홀수 이외는 짝수로 만든다
    그리고, 시작지점에서 하는 다익스트라를 한번 돌린다

    짝수 + 짝수 = 짝수, 홀수 + 짝수 = 홀수와 
    양방향과 최단 경로라는 조건에 의해서 해당 도로를 가는 경우는 많아야 1번이 보장된다

    그러면 저장된 배열에 값이 홀수면 해당 도로를 지났고, 짝수면 지나지 않았음을 의미하므로 홀수 짝수만 확인하면 된다!

    그리고 66% ? 50%에서 시간 초과로 못쓴 방법이 하나 있기는 한데,
    그것은 33_02의 입력값을 가중치, v, w를 거쳤는지 확인하는 불 변수 2개 총 3개로 하고, 
    PriorityQueue의 비교 방법을 직접 만들어 주어 푸는 방법으로 했었다
    
*/

namespace BaekJoon._33
{
    internal class _33_04
    {

        static void Main4(string[] args)
        {

            // 2배로 해서 최대값 2억 
            // 모든 점 2,000, 가중치 1,000, 2배
            // 400만 이상이며 된다!
            const int MAX = 10_000_000;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 최대 점의 개수가 2000개
            List<(int dst, int dis)>[] roots = new List<(int dst, int dis)>[2_001];
            // 목적지에 쓸거
            int[] pos = new int[2_001];

            // 초기값 설정
            Array.Fill(pos, MAX);

            // 가능한 목적지
            List<int> dstable = new List<int>(); 

            int len = int.Parse(sr.ReadLine());
            for (int i = 0; i < len; i++)
            {

                // 교차로(= 지점), 도로, 목적지 후보의 개수
                int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                if (i != 0)
                {

                    // 처음이 아닌 경우 루트 초기화
                    for (int j = 0; j < info[0]; j++)
                    {

                        roots[j + 1]?.Clear();
                        // MAX값으로 초기화
                        pos[i] = MAX;
                    }

                    pos[info[0]] = MAX;
                    dstable.Clear();
                }

                // 시작지점, 최단 거리에서 지나간 무조건 지나가야하는 두 지점
                int[] chk = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // 루트 넣기
                for (int j = 0; j < info[1]; j++)
                {

                    int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    // 기본 짝수 설정
                    temp[2] *= 2;

                    // 만약 특정 도로면 홀수로 나오게 한다
                    if (temp[0] == chk[1] && temp[1] == chk[2])
                    {

                        temp[2]--;
                    }
                    else if (temp[0] == chk[2] && temp[1] == chk[1])
                    {

                        temp[2]--;
                    }

                    roots[temp[0]] = roots[temp[0]] ?? new();
                    roots[temp[1]] = roots[temp[1]] ?? new();

                    roots[temp[0]].Add((temp[1], temp[2]));
                    roots[temp[1]].Add((temp[0], temp[2]));
                }

                
                for (int j = 0; j < info[2]; j++)
                {

                    dstable.Add(int.Parse(sr.ReadLine()));
                }

                // 문제 조건으로 오름차순 설정
                dstable.Sort();

                Dijkstra(roots, info, chk[0], pos);

                for (int j = 0; j < info[2]; j++)
                {

                    // 특정 도로를 지날 경우 홀수!
                    if (pos[dstable[j]] % 2 == 1)
                    {

                        sw.Write(dstable[j]);
                        sw.Write(' ');
                    }
                }
                sw.WriteLine();
            }

            sr.Close();
            sw.Close();
        }

        static void Dijkstra(List<(int dst, int dis)>[] _root, int[] _info, int _start, int[] _pos)
        {

            PriorityQueue<(int dst, int dis), int> q = new PriorityQueue<(int dst, int dis), int>();

            int dis = 0;

            q.Enqueue((_start, dis), dis);
            _pos[_start] = dis;

            while (q.Count > 0)
            {

                var node = q.Dequeue();
                dis = node.dis;

                if (_pos[node.dst] < dis) continue;
                if (_root[node.dst] == null) continue;

                for (int i = 0; i < _root[node.dst].Count; i++)
                {

                    int dst = _root[node.dst][i].dst;
                    int dstDis = _root[node.dst][i].dis + dis;

                    if (_pos[dst] <= dstDis) continue;
                    _pos[dst] = dstDis;

                    q.Enqueue((dst, dstDis), dstDis);
                }
            }
        }

#if Wrong

    // 우선순위 큐에 순서 비교를 원하는대로 하는 방법
    public class MyComparer : IComparer<(int dis, bool v, bool w)>
    {

        public int Compare((int dis, bool v, bool w) x, (int dis, bool v, bool w) y)
        {

            int result;

            if(x.dis > y.dis)
            {

                result = 1;
            }
            else if (x.dis < y.dis)
            {

                result = -1;
            }
            else
            {

                if (x.v == y.v)
                {

                    if (x.w == y.w) result = 0;
                    else if (x.w) result = -1;
                    else result = 1;
                }
                else if (x.w == y.w)
                {

                    if (x.v) result = -1;
                    else result = 1;
                }
                else
                {

                    if (x.v && x.w) result = -1;
                    else if (y.v && y.w) result = 1;
                    else result = 0;
                }
            }

            return result;
        }
    }

    static void Dijkstra(List<(int dst, int dis)>[] _root, int[] _info, int[] _chk, (int dis, bool v, bool w)[] _pos)
    {

        MyComparer comp = new MyComparer();

        PriorityQueue<(int dst, int dis, bool v, bool w), (int dis, bool v, bool w)> q = 
            new PriorityQueue<(int dst, int dis, bool v, bool w), (int dis, bool v, bool w)>(comp);

        int dis = 0;
        bool v = _chk[0] == _chk[1];
        bool w = _chk[0] == _chk[2];

        q.Enqueue((_chk[0], dis, v, w), (dis, v, w));
        _pos[_chk[0]].dis = dis;
        _pos[_chk[0]].v = v;
        _pos[_chk[0]].w = w;

        while (q.Count > 0)
        {

            var node = q.Dequeue();
            dis = node.dis;
            v = node.v;
            w = node.w;

            if (comp.Compare(_pos[node.dst], (dis, v, w)) < 0) continue;
            if (_root[node.dst] == null) continue;

            for (int i = 0; i < _root[node.dst].Count; i++)
            {

                int dst = _root[node.dst][i].dst;
                int dstDis = _root[node.dst][i].dis + dis;
                bool dstV = v ? v : _root[node.dst][i].dst == _chk[1];
                bool dstW = w ? w : _root[node.dst][i].dst == _chk[2];

                if (comp.Compare(_pos[dst], (dstDis, dstV, dstW)) < 0) continue;

                _pos[dst].dis = dstDis;
                _pos[dst].v = dstV;
                _pos[dst].w = dstW;

                q.Enqueue((dst, dstDis, dstV, dstW), (dstDis, dstV, dstW));
            }
        }
    }
#endif
    }
}
