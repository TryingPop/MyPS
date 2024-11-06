// #define Wrong

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
/*
날짜 : 2024. 1. 20
이름 : 배성훈
내용 : 사회망 서비스(SNS)
    문제번호 : 2533번

    문제를 잘못 읽어서 ... 사서 고생했다
    기본 문제는 39_02를 풀었다면 아주 쉽게 풀린다

    처음에 인접한 노드 중 얼리 어답터(?)가 하나라도 있으면 전파가 되는 줄 알았다
    그래서 전처리 if구문의 Wrong에 있는 DFS 메서드가 나왔다

    해당 아이디어는 노드가 맨 밑에 있는 노드(leaf)인지 판별해야한다
        여기서 leaf는 자식 없고 부모만 있는 노드
    
    leaf 노드에서 얼리 어답터를 이어가기 때문이다
    예를 들어 보자

                                        1
                        2                               3
                4           5                       12      13
            10      11                  
        
        1은 2, 3를 자식으로
        2는 4, 5를 자식으로
        3은 12, 13을 자식으로
        4는 10, 11을 자식으로한다

        해당 경우 leaf로 불릴 수 있는 것은 10, 11, 5, 12, 13이다
        나머지 노드들은 길이 2개 이상 뚫려있다!

        leaf들은 10, 11, 5, 12, 13은
            얼리 어답터인 경우 1, 
            아닌 경우 0을 담는다

        이제 3, 4의 경우를 보면 
            4가 얼리 어답터인 경우면 1,
            아닌 경우면 자식들의 수 2를 담는다

        4의 경우는 끝났다 그러면, 2로 오면
            리프와 리프가 아닌 노드가 자식으로 있다!
            2가 얼리 어답터인 경우는 4, 5의 작은 값끼리 더하고 거기에 1을 더해준다
                1 + 0 + 1 = 2, 2가 담긴다 뒤에 1을 더해주는건 자기가 얼리 어답터이기 때문이다!
            2가 얼리 어답터가 아닌 경우!는 일단 4의 작은 값 1을 담는다
                그리고, 5는 리프이므로 얼리 어답터에 영향 받는게 없어서 강제로 얼리 어답터가 되어야한다
                그래서 1을 담는다
                얼리어답터가 아닌 경우 2가 담긴다

        이제 1의 경우를 보자
            자식들의 상태를 보면                     
                  (노드)      (얼리 어답터)        (얼리 어답터 X)        
                    2               2                       2
                    3               1                       2

            이렇게 되어져 있다
            1이 얼리 어답터인 경우면 작은 것들을 더해주고 마지막에 1을 더하면 된다
                2 + 1 + 1 = 4, 4가 담긴다
            1이 얼리 어답터가 아닌 경우면 일단 자식들 2, 3의  작은 값들을 담는다
                여기서 2에서 얼리 어답터를 채택했는지 여부를 확인한다
                    여기서는 같으므로 2가 얼리 어답터인 경우의 수 2를 계승한다
                        얼리 어답터 X의 수가 얼리 어답터의 경우의 수보다 작은 경우만 계승한다!

                그리고 3에서 얼리 어답터의 경우가 작으므로 1을 계승한다
                그래서 아닌 경우 3이 담긴다
                그런데, 2가 얼리 어답터이므로 1도 얼리 어답터를 받을 수 있다 그래서 3이된다

        만약 다음과 같은 상황이면
                            1
                    2               3
                    4               5
                    6               7

            1은 2, 3을 자식으로
            2는 4를 자식으로
            3은 5를 자식으로
            4는 6을 자식으로
            5는 7을 자식으로

            해당 경우 1을 연산해보자
            2, 3 노드에 담긴값은 다음과 같다
              (노드)        (얼리 어답터)            (얼리 어답터 X)
                2               2                           1
                3               2                           1

            2, 3 모두 얼리 어답터가 아닌 경우가 더 작다!
            1이 얼리 어답터인 경우 자기자신이 얼리 어답터이기에 뒤에 1 추가
                1 + 1 + 1 = 3
            1이 얼리 어답터가 아닌 경우
                1 + 1을 우선 담는다
                그리고 2, 3 중 아무거나! 하나를 얼리 어답터로 만들면 얼리 어답터와 이어져 있다!
                그래서 2 + 1 = 3이 된다!
                
                이는 앞에서 얼리 어답터와 얼리 어답터 X인 경우를 비교하며 담을 때,
                    같은 경우 얼리 어답터의 경우를 계승하게 해서 
                    최소값의 경우에 얼리 어답터에 이어져 있는데, 1을 더해버리는 불상사를 막아 최소값을 그대로 보장한다!

        Wrong의 코드를 실행해보면 (맨 위에 // #define Wrong 에서 '//'를 지우면 된다 )
        두 번째 예제의 경우 2로 출력된다!
        실제로 해보면 4, 3을 택하면 모든 노드들이 적어도 하나의 얼리 어답터와 이어져 있다!

        39_04와 달리 여기서는 가중치가 1이라서 시작 지점이 리프던 아니던 수정할 필요가 없다!
        다만, 가중치가 있을 경우 시작 지점이 리프면 바꿔줘야한다!
*/

namespace BaekJoon._39
{
    internal class _39_03
    {

        static void Main3(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int num = int.Parse(sr.ReadLine());

            List<int>[] lines = new List<int>[num + 1];

            for (int i = 1; i <= num; i++)
            {

                lines[i] = new();
            }
            // 트리라는 조건이 주어진다
            // 없으면 사이클 여부 판별해야한다!
            for (int i = 0; i < num - 1; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add(temp[1]);
                lines[temp[1]].Add(temp[0]);
            }

            sr.Close();

            bool[] visit = new bool[num + 1];

#if Wrong
            var dp = new (int contain, int except, bool leaf)[num + 1];
            DFS(lines, dp, visit, 1);

#else

            (int contain, int except)[] dp = new (int contain, int except)[num + 1];
            DFS(lines, dp, visit, 1);

            
#endif

            // 출력
            if (dp[1].contain < dp[1].except) Console.WriteLine(dp[1].contain);
            else Console.WriteLine(dp[1].except);
        }
#if Wrong
        // ... 주변에 1명이 contain인 경우에 사용가능한 메서드..
        // 문제는 모든 친구들이 contain되어야한다!
        static void DFS(List<int>[] _lines, (int contain, int except, bool leaf)[] _dp, bool[] _visit, int _start)
        {

            _visit[_start] = true;

            _dp[_start].contain++;

            if (_lines[_start].Count == 1) _dp[_start].leaf = true;

            bool chkSNS = false;

            for (int i = 0; i < _lines[_start].Count; i++)
            {

                int next = _lines[_start][i];
                if (_visit[next]) continue;

                DFS(_lines, _dp, _visit, next);

                bool chk = _dp[next].except < _dp[next].contain;
                int add = chk ? _dp[next].except : _dp[next].contain;
                _dp[_start].contain += add;
                _dp[_start].except += add;

                if (!chk) chkSNS = true;
                if (_dp[next].leaf) 
                { 
                    
                    chkSNS = true;
                    _dp[_start].except++;
                }
            }

            if (!_dp[_start].leaf && !chkSNS) _dp[_start].except++;
        }
#else

        static void DFS(List<int>[] _lines, (int contain, int except)[] _dp, bool[] _visit, int _start)
        {

            _visit[_start] = true;
            // 자기자신 추가!
            _dp[_start].contain++;

            for (int i = 0; i < _lines[_start].Count; i++)
            {

                int next = _lines[_start][i];
                if (_visit[next]) continue;

                DFS(_lines, _dp, _visit, next);

                _dp[_start].contain += _dp[next].contain < _dp[next].except ? _dp[next].contain : _dp[next].except;
                _dp[_start].except += _dp[next].contain;
            }
        }
#endif
    }
}
