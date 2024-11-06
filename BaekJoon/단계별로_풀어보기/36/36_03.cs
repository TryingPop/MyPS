using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 14
이름 : 배성훈
내용 : 트리의 지름
    문제번호 : 1967번

    36_02의 다른 사람 풀이를 가져왔다
    점 A에 대해 A를 지나는 가장 긴 경로 C와 
    경로 C와 disjoint 이고 A를 지나는 경로 중 가장 긴 경로 D의 합을 result에 계산한다
    그러면 점 A를 지나는 가장긴 경로가 나온다
    이러한 연산을 각 점에 하고, 이 중에 가장 큰 값이 지름이 됨을 알 수 있다
    이는 DFS 코드에 있다!
*/

namespace BaekJoon._36
{
    internal class _36_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            List<(int dst, int dis)>[] root = new List<(int dst, int dis)>[len + 1];

            for (int i = 0; i < len; i++)
            {

                root[i + 1] = new();
            }

            for (int i = 0; i < len - 1; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                root[temp[0]].Add((temp[1], temp[2]));
                root[temp[1]].Add((temp[0], temp[2]));
            }

            sr.Close();

            bool[] visit = new bool[len + 1];
            int[] dp = new int[len + 1];

            visit[1] = true;
            int result = 0;
            DFS(root, visit, 1, ref result);
            Console.WriteLine(result);
        }

        static int DFS(List<(int dst, int dis)>[] _root, bool[] _visit, int _start, ref int _result)
        {

            // 해당 점을 지나면서 가장 긴 경로의 거리를 담는다
            int max = 0;
            // max경로와 disjoint한 경로 중에 가장 긴 경로의 거리를 담는다
            int sec = 0;
            var curRoot = _root[_start];
            for (int i = 0; i < curRoot.Count; i++)
            {

                int dst = curRoot[i].dst;
                if (_visit[dst]) continue;

                _visit[dst] = true;

                int dis = curRoot[i].dis;
                // DFS 탐색이므로 해당 점을 지나는 경로 중 가장 긴 값이 나온다
                // 그래서 max와 sec의 경로는 disjoint가 보장된다!
                int chk = DFS(_root, _visit, dst, ref _result) + dis;

                if (max < chk) 
                {

                    sec = max;
                    max = chk; 
                }
                else if (sec < chk) sec = chk;
            }

            // 해당 지점을 지나는 서로 소인 경로의 합 중 최장 길이 저장
            // result를 참조해서 넣기에 마지막에 들어가는 값은 지름이 된다!
            if (_result < max + sec) _result = max + sec;

            return max;
        }
    }
}
