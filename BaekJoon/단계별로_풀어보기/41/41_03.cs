using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 24
이름 : 배성훈
내용 : 외판원 순회
    문제번호 : 2098번

    8% 에서 계속 틀렸다
    그래서 다른 사람 코드를 참고했고 그걸로 먼저 통과했다

    그리고 나서 기존에 자꾸 틀리던 코드를 살펴보니 왜 틀렸는지 알겠다
    앞과 달리 dp를 이차원으로 설정하지 않을 시 그냥할 경우
    방문하는 곳이 n개일 때, 1, 2, 3, ..., n 장소라하면
    1 번에서 시작해서 2, 3, ..., n을 모두 한 번만 방문하는 최단 경로를 C라하자
    (C 이외에 1번에서 시작해서 2, 3, ..., n을 모두 방문하는 경로가 있을 수 있다!)

    그런데, C의 마지막 지점에서 1로 가는 경로가 없으면, 순회할 수 없는 루트이다
    dp가 일차원인 경우, 해당 루트로 가는 길이 최단 경로에 계속 담겨져 있고, 
    다른 경로에서 갈 수 있는경우가 발견되면, 해당 최단경로에서 가버리는 불상사가 일어난다!

    예를들어
        len = 3이라하고 비용 표는 다음과 같다고 하자
            0   100   1
            0   0   1
            1   1   0
        그러면 1에서 시작해서 2, 3을 지나는 최단 경로는
        1 -> 3 -> 2 순으로 가면 최단 비용 2를 얻는다!

        그런데, 2 -> 1로는 못가서 순회를 못하는 루트이다!
        실제로 순회하는 루트는 1 -> 2 -> 3으로 101 비용을 소모해서 가야한다!
        그리고 3 -> 1로 가면 총 102비용으로 순회가 가능하다!

        이때, dp가 1차원인 방법에서는
        1 << 0 | 1 << 1 | 1 << 2 = 7인 인덱스에서 3의 값을 가진 뒤 순회할 수 있는 길이 없기에 멈춰버리고
        1 -> 3 -> 2 순으로 간 루트에서 기존의 1 -> 2 -> 3의 값 3을 써서 1추가해 4를 반환해버리는 불상사가 일어난다!

        이런 불상사가 일어나는 코드가 Wrong8이다

        그래서 앞(41_02)과 달리 2차원 배열을 써야한다!
        앞(41_02)은 1차원 배열을 써도 된다!

    ... 이걸 생각 못해서 새벽까지 왜 틀린지 알아보고 있었다;

    그리고 다른 사람의 아이디어를 참고한 코드에서,
    주의할께 있다! 만약 초기에 Array.Fill에 1억을 채우고 시작해버리면, 시간 초과가 뜬다!
    이유는 INF로 초기값을 설정할 시, 방문 후 INF를 채워진 경우 다시 재방문하기에 생기는 문제라고 한다!

    그래서 처음에 초기값을 -1 같이 INF와 다른 값으로 주던지, 현재 문제에서는 비용에 자연수(> 0)가 담기기에! 0을 초기값으로 써도 상관없다!

    앞에서 발생하는 문제를 고친게 DFS함수이다
    해당 경우 경로가 없으면 INF를 반환한다!

    그리고 값이 담긴거에 대해서는, 재탐색을 하지않게 바로 반환해버린다!
*/

namespace BaekJoon._41
{
    internal class _41_03
    {

        static void Main3(string[] args)
        {

#if Wrong8
            const int INF = 100_000_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] board = new int[len][];

            for (int i = 0; i < len; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }

            sr.Close();

            // 앞과 비슷한 방법으로 한다!
            int[] dp = new int[1 << len];
            int end = (1 << len) - 1;

            Array.Fill(dp, INF);

            Stack<ValueTuple<int, int>> q = new Stack<ValueTuple<int, int>>(1 << (len - 1));
            // 1 << 0 == 1

            q.Push((1 << 0, 0));
            dp[1 << 0] = 0;

            while (q.Count > 0)
            {

                (int visit, int curPos) node = q.Pop();
                int curValue = dp[node.visit];

                for (int nextPos = 0; nextPos < len; nextPos++)
                {

                    // 이미 지나온 장소거나 다음으로 가는 경로가 없는 경우다!
                    if ((node.visit & (1 << nextPos)) != 0
                        || board[node.curPos][nextPos] == 0) continue;

                    int nextIdx = node.visit | (1 << nextPos);

                    int chk = curValue + board[node.curPos][nextPos];

                    if (nextIdx == end)
                    {

                        // 순회이므로 모든 장소를 돌았다면 처음 위치로 오는 값도 해서 계산한다!
                        // 처음 위치로 가는 경로가 없으면 탈출!
                        if (board[nextPos][0] == 0) chk = INF;
                        else chk += board[nextPos][0];
                    }

                    if (dp[nextIdx] > chk)
                    {

                        dp[nextIdx] = chk;
                        q.Push((nextIdx, nextPos));
                    }
                }
            }

            
#else
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] board = new int[len][];
            int[][] dp = new int[len][];

            for (int i = 0; i < len; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                dp[i] = new int[1 << len];
            }

            sr.Close();

            int end = (1 << len) - 1;


            Console.WriteLine(DFS(board, dp, 0, 1 << 0, len, end)); 
#endif

        }

#if !Wrong8
        static int DFS(int[][] _board, int[][] _dp, int _curPos, int _visit, int _len, int _end)
        {

            const int INF = 100_000_000;
            if (_visit == _end)
            {

                if (_board[_curPos][0] == 0) return INF;
                else return _board[_curPos][0];
            }

            if (_dp[_curPos][_visit] != 0) return _dp[_curPos][_visit];

            _dp[_curPos][_visit] = INF;
            for (int nextPos = 0; nextPos < _len; nextPos++)
            {

                if ((_visit & (1 << nextPos)) != 0) continue;
                if (_board[_curPos][nextPos] == 0) continue;

                int result = DFS(_board, _dp, nextPos, _visit | (1 << nextPos), _len, _end);
                if (result == INF)
                {

                    _dp[_curPos][_visit] = Math.Min(_dp[_curPos][_visit], INF);
                }
                else
                {

                    _dp[_curPos][_visit] = Math.Min(_dp[_curPos][_visit], result + _board[_curPos][nextPos]);
                }
            }

            return _dp[_curPos][_visit];
        }
#endif
    }
}
