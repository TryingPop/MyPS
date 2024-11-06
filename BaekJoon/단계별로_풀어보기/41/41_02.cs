using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 24
이름 : 배성훈
내용 : 할 일 정하기 1
    문제번호 : 1311번

    그냥 무턱대고 dfs 하면 20^20 경우의 수가 나온다
    당연히 시간초과 났다

    무엇을 저장해야할지 도저히 안떠올랐다.
    그래서 다른 사람의 풀이 아이디어를 봤다

    일과  사람의 시작을 0번부터 n까지 있다고 하자
    그러면 dp에 저장하는 데이터는 dp[A][B]는 다음과 같다
        A == 0 일때, 아무 일도 안했다
        A > 0일 때, 0번부터 A - 1번까지 일했음을 의미한다

        B는 (1 << b & B) != 0이면, b번째 사람이 일을 했음을 의미한다
        그리고, (1 << b & B) == 0이면 b는 일을 하지 않았음을 의미한다
        예를들어 B == 3인 경우 B = (1 << 0) + (1 << 1) 이므로 0번과 1번이 일한 경우이다!

        그리고 dp[A][B]에는 최소 비용을 저장한다

    여기까지 보고 해당 조건대로 코드를 짜봤다
    먼저 입력되는 일의 수가 len이라 하자
    그러면 앞의 A는 0번에서 A - 1번까지일 햇음을 의미하므로 len + 1
    B는 일을한 사람들 정보를 담아야하기에 2^len개 가 필요하다
    
    dp의 크기는 (len + 1) * (2 ^ len) 개... 최대 20이므로 2000만 크기를 자랑한다
    그리고, A의 정의로부터 순차적으로 해야하므로 for문이 필요하다
    다음으로 이전 정보에서 탐색해야하므로 이전 정보가 있는지 확인과 초기에 0, 0에 0의 값을 넣어줘야한다
    이후에 해당 값을 보면서 일과 사람을 하나씩 증가된 곳에 값을 최소값이 되게 갱신한다
    for문을 돌면 모든 사람이 일이 끝나고, 모든 사람이 일했을 경우에 담겨 있는 최소값이 결과가 된다

    이를 표현하면 other 내용와 같다

    그리고 필요한 부분만 저장하게 줄여가는 BFS 탐색 코드를 짜봤는데,
    메모리 사용량은 other의 80%양을 줄였으나(100 -> 20) 속도는 10%도 향상 안되었다

    다른 사람껄 보니 dfs 탐색인데, i를 인자로 넘기고 탐색 메서드 안에서 1 << i를 비교했다
*/
namespace BaekJoon._41
{
    internal class _41_02
    {

        static void Main2(string[] args)
        {
#if TimeOut
            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());
            int[][] board = new int[len][];
            for (int i = 0; i < len; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }
            sr.Close();

            // DFS 연산 !
            int visit = 0;
            int min = 200_000;

            DFS(board, 0, visit, 0, ref min);

            Console.WriteLine(min);
#elif other

            const int INF = 200_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int dpLen = 1 << len;
            int[][] board = new int[len][];
            
            // 다른 사람 아이디어를 빌려왔다
            // dp[a][b]는
            // 앞의 a는 0번에서 a - 1번까지 일을 했음을 의미한다
            // 뒤의 b는 1 << c & b != 0이면, c의 사람이 일한 것을 의미한다
            // 그래서 뒤의 b는 일한 사람들의 정보를 담고 있다

            // 예를들어
            // dp[2][3]인 경우
            // 앞의 2는 0, 1번 일을했고,
            // 뒤의 3 ((1 << 0) + (1 << 1))은 0, 1번 사람이 일한 최소값이 담겨있다
            // 즉, board[0][0] + board[1][1] 과 board[0][1] + board[1][0]의 최소값이 담겨있다
            int[][] dp = new int[len + 1][];
            dp[0] = new int[dpLen];
            Array.Fill(dp[0], INF);
            dp[0][0] = 0;

            for (int i = 0; i < len; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                // 20개면 약 100만개...
                dp[i + 1] = new int[dpLen];
                Array.Fill(dp[i + 1], INF);
            }

            sr.Close();
            // 탐색
            for (int i = 1; i <= len; i++)
            {

                for (int j = 0; j < dpLen; j++)
                {

                    // 앞 번에 일한 기록이 없다
                    // 그래서 넘긴다
                    if (dp[i - 1][j] == INF) continue;
                    
                    // 이제 일한 기록을 토대로 새로운 정보를 저장!
                    // 해당 일을 k번 사람이 일을 하는 값 추가한다
                    for (int k = 0; k < len; k++)
                    {

                        // 이미 k번 사람이 일했으면 넘긴다
                        if ((1 << k & j) != 0) continue;

                        // 일을 했을 때 값을 계산
                        int min = dp[i - 1][j] + board[i - 1][k];

                        // 그리고 해당일을 했을 때가 최선인지 비교한다
                        // INF 값이 초기값으로 있어 처음 경우는 자연스레 담긴다
                        // 이후 비교하면서 최소값 찾아간다
                        if (min < dp[i][j | 1 << k]) dp[i][j | 1 << k] = min;
                    }
                }
            }

            Console.WriteLine(dp[len][dpLen - 1]);
#else

            // 앞의 논리대로 메모리 사용량을 줄였다..
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] board = new int[len][];

            for (int i = 0; i < len; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }

            sr.Close();

            // dp의 크기를 줄여보자!
            int[] dp = new int[1 << len];

            // 다음 시작 인덱스를 보관한다
            Queue<int>[] q = new Queue<int>[2];
            // 큐가 확장되는 연산을 안하게, 담을 수 있는 최대 공간을 미리 할당
            q[0] = new Queue<int>(1 << (len - 1));
            q[1] = new Queue<int>(1 << (len - 1));
            int useIdx = 0;
            int saveIdx = 1;

            q[0].Enqueue(0);

            // BFS 탐색
            for (int work = 0; work < len; work++)
            {

                while (q[useIdx].Count > 0)
                {

                    int node = q[useIdx].Dequeue();

                    for (int man = 0; man < len; man++)
                    {

                        // 해당 사람이 이미 일했으면 넘긴다!
                        if ((node & 1 << man) != 0) continue;

                        int chk = dp[node] + board[man][work];

                        int curIdx = node | (1 << man);
                        int curTime = dp[curIdx];

                        // 처음 값이 들어가는 경우!
                        if (curTime == 0)
                        {

                            dp[curIdx] = chk;
                            // 다음 인덱스를 넣는다
                            q[saveIdx].Enqueue(curIdx);
                        }
                        else if (curTime > chk)
                        {

                            // 최소 비용이 발견된 경우 해당 값으로 대체
                            dp[curIdx] = chk;
                        }
                    }
                }

                useIdx = useIdx == 1 ? 0 : 1;
                saveIdx = saveIdx == 1 ? 0 : 1;
            }

            Console.WriteLine(dp[(1 << len) - 1]);
#endif
        }

#if TimeOut
        static void DFS(int[][] _board, int _curIdx, int _visit, int _value, ref int _min)
        {

            if (_curIdx == _board.Length)
            {

                if (_value < _min) _min = _value;
                return;
            }

            for (int i = 0; i < _board.Length; i++)
            {

                if ((_visit & 1 << i) != 0) continue;
                _visit |= 1 << i;

                DFS(_board, _curIdx + 1, _visit, _value + _board[_curIdx][i], ref _min);

                _visit &= ~1 << i;
            }
        }
#endif
    }
}
