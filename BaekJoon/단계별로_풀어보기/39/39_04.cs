using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 20
이름 : 배성훈
내용 : 우수 마을
    문제번호 : 1949번

    괜히 조건 추가해서 틀린 문제다;
    그냥 간단하게, 포함될 때 최대값과 포함 안될때의 최대값을 넣어주면 된다

    그러면 except와 contain이 같아 중간에 끊길 수가 있는데
    예를 들면 트리가
                    1
            2               3
                            4
                            5
                            6
                            7

    1은 2, 3을 자식으로
    3은 4를 자식으로
    4는 5를 자식으로
    5는 6을 자식으로
    6은 7을 자식으로

    그리고  
            노드       1   2   3   4   5   6   7
            가중치     1  10   1  10   1   1  10

    로 주어졌다고 하자

    그러면 2, 4, 7을 택하면 모든 최대 값 30이 나온다
    
    그런데 찾아갈 때,
        1에서 2를 탐색 그러면 2에 10 / 0이 담긴다
        그리고 2 탐색이 끝났으므로 종료
        3에서는 자식으로 4, 5, 6, 7으로 쭉 가면 7에서 멈춘다
        그리고 7에서 10 / 0이 담기고 7탐색 종료되고 6으로 간다
        그리고 6에서 
            6이 우수 마을인 경우 7은 우수마을이 아닌 경우이므로 0이 담긴다
            6이 우수 마을이 아닌 경우 7의 큰 경우가 담기는데 이때는 7 이 우수사원인 경우이다
            그래서 6에 1 / 10이 담긴다
            그리고 6의 탐색이 끝나고 5로 간다

        5에서 5가 우수 마을인 경우 6은 우수 마을이될 수 없으므로 1 + 10 = 11이 담긴다
            그리고 5에서 우수마을이 아닌 경우 큰값을 담으면, 10이 담긴다
            그러면 이 경우는 5가 우수 마을이랑 인접하지 않은 경우이다!
            그런데 만약 4에서 끝나는 경우면 마지막에 큰 값을 찾기에 4가 우수 마을인 경우가 크므로 딱히 신경 안써도 된다!
            그래서 5에는 11 / 10이 담기고 탐색이 끝나서 4로 간다

        이제 4로 가면 4가 우수 마을인 경우 5는 우수마을이 아니다 그래서 10 + 10 = 20이 담긴다
            반면 4가 우수마을이 아닌경우 5에서 큰 값 11이 담긴다
            4에서 탐색이 끝났으므로 3으로 간다

        3에서 3이 우수마을인 경우 1 + 11 = 12가 담기고
            3이 우수 마을이 아닌 경우 20이 담긴다
            3의 탐색이 끝나고 1로 간다
        
        1의 모든 자식 노드들을 탐색했으므로 1을 탐색한다
            1이 우수 마을인 경우 2, 3이 우수마을이 아닌 경우와 합쳐진다
            그래서 1 + 12 = 13
            1이 우수마을이 아닌 경우 2, 3의 값들 중 큰 값들을 합하여 담는다
            그래서 10 + 20 = 30

        어느 경우던 우수마을과 적어도 하나 인접해있고, 우수마을끼리 인접한 경우는 없다
        
        실제로 B - C - ... 노드들이 있고 DFS에의해 C - B 순으로 진행된다고 하자
            그리고 C는 우수마을과 이어져 있고 C에 담긴 수 중에 우수 마을이 아닌 경우가 크다고 하자
            B가 우수 마을이 아닌 경우 큰 값은 C에서 우수마을이 아닌 경우가 담긴다
            B가 우수 마을인 경우 당연히 C가 우수 마을이 아닌 경우가 담기고, B의 가중치 b가 추가된 값이 담긴다
            그런데 B의 우수마을이 아닌 경우는 B가 우수마을이 이어지지 않는다는 사실에 유의하자
            B에서 큰 값을 비교하기에 우수마을인 경우만 담긴다!

            그리고 B의 부모인 경우는 A 노드가 존재해 어떻게 A에 값이 담기는지 살펴보자
                만약 A의 가중치 a가 B의 가중치 b라하면
                    A가 우수 마을인 경우 a + B에서 우수마을이 아닌 경우의 값이 된다
                    A가 우수 마을이 아닌 경우 b + B에서 우수마을이 아닌 경우의 값이다!
                    어느 경우던 A에서는 A가 우수마을이 되던 B가 우수마을이 되던 우수마을인 경우가 담긴다!

        마지막에 비교연산을 하므로 문제 조건에 맞게 나온다!
        처음에는 우수 마을과 인접한 상태로 만들면서 이동했는데, 
        이 경우 코드를 잘못 작성해서 그런지 연산량만 많아지고 반례인 상황들이 계속해서 나왔다;
*/

namespace BaekJoon._39
{
    internal class _39_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int num = int.Parse(sr.ReadLine());

            // 인구
            int[] pop = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 1부터 시작
            List<int>[] lines = new List<int>[num + 1];

            for (int i = 1; i <= num; i++)
            {

                lines[i] = new();
            }

            // 트리의 간선 입력 받는다
            for (int i = 1; i < num; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add(temp[1]);
                lines[temp[1]].Add(temp[0]);
            }

            sr.Close();

            int start = 1;

            if (lines[start].Count == 1) start = lines[start][0];

            // dp
            (int contain, int except, bool visit)[] dp = new (int contain, int except, bool visit)[num + 1];

            // 탐색
            DFS(lines, start, dp, pop);

            // 출력
            if (dp[start].contain > dp[start].except) Console.WriteLine(dp[start].contain);
            else Console.WriteLine(dp[start].except);
        }

        static void DFS(List<int>[] _lines, int _start, (int contain, int except, bool visit)[] _dp, int[] _pop)
        {

            _dp[_start].contain += _pop[_start - 1];
            _dp[_start].visit = true;

            for (int i = 0; i < _lines[_start].Count; i++)
            {

                int next = _lines[_start][i];

                if (_dp[next].visit) continue;
                DFS(_lines, next, _dp, _pop);

                _dp[_start].contain += _dp[next].except;
                _dp[_start].except += _dp[next].contain < _dp[next].except ? _dp[next].except : _dp[next].contain;
            }
        }
    }
}
