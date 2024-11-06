using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 1
이름 : 배성훈
내용 : 2 - SAT - 1, 2 - SAT - 2
    문제번호 : 11277번, 11278번

    2 - SAT - 3문제를 풀기전에 먼저 푼 문제이다

    가능한 모든 경우의 수를 따져가며 풀었다
    처음에는 현재 문제를 정답이 되게 sol 집합을 수정하고,
    이후 이전 문제들을 확인하는 형식으로 DFS를 만들었다 그러니 30%에서 틀렸다
    이는 DFS 탈출하면서 이전값 초기화를 안해줘서 제대로 입력이 안되어 생긴 문제인거 같다

    그래서 하나하나 초기화하면서 하니 통과했다
    그런데, 해당 방법을 수정하지 않는 이상, 2 - SAT - 3에서 쓸 수 없는 방법인거 같다
    그리고 해당 방법으로는 SCC와 엮을 규칙이 보이지 않는다

    SCC 와 엮을 수 있게 해봐야겠다
*/

namespace BaekJoon._45
{
    internal class _45_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            // 매번 되는지 확인해야 하는가?
            int[][] quest = new int[info[1]][];

            for (int i = 0; i < info[1]; i++)
            {

                quest[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }

            sr.Close();

            bool result = false;

#if Wrong30
            bool[] solve = new bool[info[0] + 1];

            // 이제 DFS 탐색으로 하자?
            int[] dirX = { 1, 1, -1 };
            int[] dirY = { 1, -1, 1 };

            DFS(quest, solve, dirX, dirY, 0, ref result);
#else

            bool[] visit = new bool[info[0] + 1];
            bool[] solve = new bool[info[0] + 1];
            bool[] memo = new bool[info[0] + 1];
            DFS(quest, visit, solve, 0, ref result, memo);
#endif

            if (result) 
            { 
                
                Console.WriteLine(1); 
                for (int i = 1; i < memo.Length; i++)
                {

                    if (memo[i]) Console.Write(1);
                    else Console.Write(0);

                    Console.Write(' ');
                }
            }
            else Console.WriteLine(0);
        }
#if Wrong30
        static void DFS(int[][] _quest, bool[] _solve, int[] _dirX, int[] _dirY, int _curNum, ref bool _success)
        {

            // 정답이 나온 경우
            if (_success) return;

            // 모든 문제 통과한 경우!
            if (_curNum == _quest.Length)
            {

                _success = true;
                return;
            }

            int a = _quest[_curNum][0];
            int b = _quest[_curNum][1];

            if (a == b)
            {

                if (a > 0) _solve[a] = true;
                else _solve[-a] = false;

                bool failed = false;
                for (int i = 0; i < _curNum; i++)
                {

                    // 정답 확인!
                    int chk1 = _quest[i][0];
                    int chk2 = _quest[i][1];

                    if (((chk1 > 0 && !_solve[chk1]) || (chk1 < 0 && _solve[-chk1]))
                        && ((chk2 > 0 && !_solve[chk2]) || chk2 < 0 && _solve[-chk2]))
                    {

                        failed = true;
                        break;
                    }
                }

                if (failed) return;

                DFS(_quest, _solve, _dirX, _dirY, _curNum + 1, ref _success);
                return;
            }

            if (a == -b)
            {

                int idx = a > 0 ? a : b;
                for (int i = 0; i < 2; i++)
                {

                    _solve[idx] = !_solve[idx];

                    bool failed = false;
                    for (int j = 0; j < _curNum; j++)
                    {

                        // 정답 확인!
                        int chk1 = _quest[j][0];
                        int chk2 = _quest[j][1];

                        if (((chk1 > 0 && !_solve[chk1]) || (chk1 < 0 && _solve[-chk1]))
                            && ((chk2 > 0 && !_solve[chk2]) || chk2 < 0 && _solve[-chk2]))
                        {

                            failed = true;
                            break;
                        }
                    }

                    if (failed) continue;

                    DFS(_quest, _solve, _dirX, _dirY, _curNum + 1, ref _success);
                }

                return;
            }

            for (int i = 0; i < 3; i++)
            {

                int idx1 = a * _dirX[i];
                int idx2 = b * _dirY[i];

                if (idx1 > 0) _solve[idx1] = true;
                else _solve[-idx1] = false;

                if (idx2 > 0) _solve[idx2] = true;
                else _solve[-idx2] = false;

                bool failed = false;
                for (int j = 0; j < _curNum; j++)
                {

                    // 정답 확인!
                    int chk1 = _quest[j][0];
                    int chk2 = _quest[j][1];

                    if (((chk1 > 0 && !_solve[chk1]) || (chk1 < 0 && _solve[-chk1]))
                        && ((chk2 > 0 && !_solve[chk2]) || chk2 < 0 && _solve[-chk2]))
                    {

                        failed = true;
                        break;
                    }
                }

                if (failed) continue;

                DFS(_quest, _solve, _dirX, _dirY, _curNum + 1, ref _success);
            }
        }
#else
        static void DFS(int[][] _quest, bool[] _visit, bool[] _solve, int _cur, ref bool _success, bool[] _memo)
        {

            // 성공한 경우 바로 탈출
            if (_success) return;
            // 마지막 문제까지 모두 푼 경우 탈출
            else if (_quest.Length == _cur)
            {

                _success = true;
                for (int i = 1; i < _memo.Length; i++)
                {

                    _memo[i] = _solve[i];
                }
                return;
            }

            // 현재 문제의 앞
            int q1 = _quest[_cur][0];
            int q2 = _quest[_cur][1];

            // 양수? 음수 판별
            bool chk1 = q1 > 0;
            bool chk2 = q2 > 0;

            int idx1 = chk1 ? q1 : -q1;
            int idx2 = chk2 ? q2 : -q2;

            if (_visit[idx1] && _visit[idx2])
            {

                // 둘 다 먼저 방문한 경우
                // 수정은 없다! 그냥 쭉 맞는지 판별만 한다
                if ((chk1 == _solve[idx1]) || (chk2 == _solve[idx2])) DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                return;
            }

            if (_visit[idx1])
            {

                // _visit[idx2]는 방문 X!이다
                // 여기는 idx1 != idx2가 자동으로보장!
                // 재진입 방지용!
                _visit[idx2] = true;

                if ((chk1 == _solve[idx1]))
                {

                    // 먼저, True가 보장된 경우!
                    _solve[idx2] = true;
                    DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                    _solve[idx2] = false;
                    DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                    _visit[idx2] = false;
                    return;
                }

                // 이제 앞에는 거짓이므로! 참으로 맞춰줘야한다!
                _solve[idx2] = chk2;
                DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                _solve[idx2] = false;
                _visit[idx2] = false;
                return;
            }

            if (_visit[idx2])
            {

                // visit[idx1]은 방문 X
                _visit[idx1] = true;

                if ((chk2 == _solve[idx2]))
                {

                    // 현재 문제에서는 참이 보장되었다
                    _solve[idx1] = true;
                    DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                    _solve[idx1] = false;
                    DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                    _visit[idx1] = false;
                    return;
                }

                _solve[idx1] = chk1;
                DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
                _solve[idx1] = false;
                _visit[idx1] = false;
                return;
            }

            // 이제는 둘 다 처음 방문! 케이스 3개로 나눈다!
            if (idx1 != idx2)
            {

                // 둘이 서로 다른 경우
                _visit[idx1] = true;
                _visit[idx2] = true;

                // 먼저 T, T
                _solve[idx1] = chk1;
                _solve[idx2] = chk2;

                DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);

                // T, F
                _solve[idx2] = !chk2;
                DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);

                // F, T
                _solve[idx1] = !chk1;
                _solve[idx2] = chk2;
                DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);

                _visit[idx1] = false;
                _visit[idx2] = false;
                return;
            }

            // 이제는 같은 경우!
            if (chk1 == chk2)
            {

                // TT만 허용!
                _visit[idx1] = true;
                _solve[idx1] = chk1;

                DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);

                _visit[idx1] = false;
                return;
            }

            // 뭘해도 상관없는 경우
            _visit[idx1] = true;
            _solve[idx1] = true;

            DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);

            _solve[idx1] = false;
            
            DFS(_quest, _visit, _solve, _cur + 1, ref _success, _memo);
            _visit[idx1] = false;
        }

#endif

#if other

StreamReader reader = new(Console.OpenStandardInput());
StreamWriter writer = new(Console.OpenStandardOutput());

var input = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
int half = input[0],dotcount = half*2, linecount = input[1];

LinkedList<int>[] lines = new LinkedList<int>[dotcount];

for (int i = 0; i < dotcount; i++)
{
    lines[i] = new();
}

int not(int n)
{
    if (n < half) return n + half;
    return n - half;
}

for (int i = 0; i < linecount; i++)
{
    input = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
    if (input[0] < 0) input[0] = -input[0] + half;
    if (input[1] < 0) input[1] = -input[1] + half;
    input[0]--;
    input[1]--;
    lines[not(input[0])].AddLast(input[1]);
    lines[not(input[1])].AddLast(input[0]);
}

PriorityQueue<string, int> output = new();
Stack<int> stack = new();
bool[] finished = new bool[dotcount];
int[] id = new int[dotcount];

int next = 0;
int dfs(int n)
{
    int parent = id[n] = ++next;
    stack.Push(n);

    foreach (var l in lines[n])
    {
        if (id[l] is 0) parent = Math.Min(parent, dfs(l));
        else if (!finished[l]) parent = Math.Min(parent, id[l]); 
    }

    if (parent == id[n])
    {
        SortedSet<int> result = new();
        int ret;
        do
        {
            ret = stack.Pop();
            result.Add(ret + 1);
            finished[ret] = true;
            id[ret] = parent;
        } while (ret != n);
        output.Enqueue(string.Join(' ',result)+" -1",result.First());
    }

    return parent;
}

for (int i = 0; i < dotcount; i++)
{
    if (finished[i] || id[i] is not 0) continue;
    dfs(i);
}

int answer = 1;
for (int i = 0; i < half; i++)
{
    if (id[i] == id[not(i)])
    {
        answer = 0;
        goto RESULT;
    }
}

RESULT:
writer.WriteLine(answer);
writer.Flush();
#endif
    }
}
