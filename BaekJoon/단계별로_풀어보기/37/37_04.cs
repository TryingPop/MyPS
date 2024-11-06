using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 17
이름 : 배성훈
내용 : 사이클 게임
    문제번호 : 20040번

    유니온 파인드 알고리즘으로 사이클 판정하기 문제이다
    처음에는 어떻게 접근해야할지 몰랐다

    그래서 몇개의 실험을 해봤고, 해도 못찾아서 검색으로 다른 사람 아이디어를 보고 예제를 들어 시행해보니 풀이를 이해했다
    
    여기서 사이클은 기존의 사이클과 조금 다르다
    1 -> 2 -> 3 -> 1처럼 정해진 경로 따라 이동하면 처음 위치로 올 수 있는걸 의미하는게 아니다!

    조금 더 넓은 의미의 사이클이된다
    1 -> 2 -> 3, 1 -> 3처럼 선분을 따라 이동할 때, 처음 위치로 오는 경우도 포함된다

    해당 사이클을 찾기 위해 단방향 간선들을 양방향으로 보고 노드가 연결 안된 경우 연결을 한다
    그리고 다음 간선들의 노드들을 가져와 연결되어져 있는지 확인하고 이어져 있지 않다면 다시 연결한다
    해당 작업을 반복한다
    만약 간선이 주어졌는데, 해당 간선의 두 노드가 이미 연결되었다면 37_04의 사이클이 발생했다와 동형이 된다

    /// 36_07과 비교
    36_07의 문제는 양방향 간선이고 여기는 단방향 간선이므로 문제 자체가 다르다!
    그러나 36_07은 해당 방법으로 풀린다

    왜냐하면, 36_07의 모든 경우의 수는 중복이 없기에 여기의 일부 경우의 수로 모두 치환이 가능하기 때문이다
    예를들어 1 - 2 양방향 간선은 왼쪽을 시작 지점, 오른쪽을 끝지점으로 하는 단방향 간선으로 1대 1로 치환이 가능하고
    그러면 36_07의 사이클은 여기의 사이클로 똑같이 해석되기 때문이다 그래서 37_04의 방법으로 36_07을 풀 수 있다!

    그러나 36_07에서는 입력값으로 주어질 수 없지만, 여기서는 주어질 수 있는 경우의 수가 존재한다
    여기서는 1 -> 2, 2 -> 1은 서로 다른 단방향 간선이므로 입력이 가능하고 이 경우 37_04에서는 사이클이 존재한다고 볼 수 있다
    그러나 36_07의 경우 유일하다는 조건에 의해 해당 입력값을 입력할 수 없다

    즉, 36_07은 37_04의 일부분이라 보고 풀어도 된다! 그러나 37_04의 방법을 여기에 무턱대로 데려와서는 안된다.
    실제로 36_07의 방법으로 37_04를 해결하려면 DFS에서 발견 안되었더라도 기존 코드에 선분이 중복해서 들어오는 경우 사이클이 존재한다고 예외 처리를 해줘야한다
    ///
*/

namespace BaekJoon._37
{
    internal class _37_04
    {

        static void Main4(string[] args)
        {

            // const int MAX_VERTEX = 500_000;
            // const int MAX_LINES = 1_000_000;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] board = new int[info[0]];

            Stack<int> s = new Stack<int>();

            for (int i = 0; i < info[0]; i++)
            {

                board[i] = i;
            }

            int result = 0;

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                int f = Find(board, temp[0], s);
                int b = Find(board, temp[1], s);

                if (f == b)
                {

                    // 연결된게 발견되었다면
                    // 즉, 사이클이 발생했다면 여기 온다
                    result = i + 1;
                    break;
                }

                // 사이클이 없다면 단방향 간선을 양방향 간선으로 보고,
                // 두 노드를 포함하는 연결집합을 이어 붙인다
                if (f > b) board[f] = b;
                else board[b] = f;
            }

            sr.Close();
            Console.WriteLine(result);
        }

        static int Find(int[] _board, int _chk, Stack<int> _calc)
        {

            // 앞과 같다
            int chk = _chk;

            while(chk != _board[chk])
            {

                _calc.Push(chk);
                chk = _board[chk];
            }

            while(_calc.Count > 0)
            {

                _board[_calc.Pop()] = chk;
            }

            return chk;
        }
    }
}
