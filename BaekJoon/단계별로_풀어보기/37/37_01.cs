using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 17
이름 : 배성훈
내용 : 집합의 표현
    문제번호 : 1717번
    
    유니온 파인드 알고리즘
    유니온은 두 집합을 합치는 과정이다
    만약 정점 1, 2, 3, 4, 5가 있을 때,
    유니온을 하기 위해서는 하나의 차트가 필요하다
    먼저 정점에 따른 유니온 차트는 초기에 자기자신을 인덱스로하고 값은 자기자신인 차트가 된다
        idx     1   2   3   4   5
        value   1   2   3   4   5

    그리고 간선들의 정보로 값을 수정한다
    만약 1, 2를 잇는 간선이 존재하면

    1과 2는 연결된 상태이고 둘이 같은 값을 가져야한다
    관계에 의미를 부여할게 아니라면 1, 2 아무거나 잡아도 상관없다
    그래서 여기서는 2로 잡자
        idx     1   2   3   4   5
        value  '2'  2   3   4   5
    유니온이 끝난 이후 표는 위와 같다

    반면 1 - 2 - 3 - 4 간선으로 이어져 있을 경우
    시작 지점으로 값을 넣는다고 하면 유니온 연산이 끝난 표는 다음과 같다
        idx     1   2   3   4   5
        value   1  '1' '2' '3'  5

    파인드는 하나의 원소가 어떤 집합에 속해있는지 확인하는 연산이다
    board의 value와 idx가 같을 때 까지 탐색해야한다
    위 표에서 4를 파인드한다고 하면
        idx 4에서 value는 3이고 4 != 3 이므로 idx = 3인 곳으로 간다
        idx 3에서 value는 2이고 3 != 2 이므로 idx = 2인 곳으로 간다
        idx 2에서 value는 1이고 2 != 1 이므로 idx = 1인 곳으로 간다
        idx 1에서 value는 1이고 1 == 1 이므로 4는 1집합에 속해있다

    계층? 단계가 많아 질수록 파인드 하는 시간이 늦어지는 것을 확인할 수 있다
    idx와 value 사이에 의미를 부여했다면, 어쩔 수 없지만 여기서는 같은 집합임을 확인하는 것 뿐이기에 의미가 없다!
    그래서 idx 3, 4의 value를 1로 바꾸면 3, 4의 파인드 과정이 짧아지는 것을 바로 알 수 있다!
        idx     1   2   3   4   5
        value   1   1   2   3   5

        idx     1   2   3   4   5
        value   1   1  '1' '1'  5

    매번 모든 값을 찾아 갱신하는 것은 시간이 늦다고 생각했다
    그래서 처음 find했던 것들에 한해서만 값을 갱신해줬다
    이게 Find 함수에 Stack을 받아오는 이유이다!
*/

namespace BaekJoon._37
{
    internal class _37_01
    {

        static void Main1(string[] args)
        {

            const string YES = "YES\n";
            const string NO = "NO\n";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] board = new int[info[0] + 1];

            for (int i = 1; i < board.Length; i++)
            {

                board[i] = i;
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            Stack<int> calc = new Stack<int>();

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                // 파인드
                int chk1 = Find(board, calc, temp[1]);
                int chk2 = Find(board, calc, temp[2]);

                if (temp[0] == 0)
                {

                    // 유니온
                    // 값이 밑으로 향하기에
                    // 밑에 있는 값만 이어주면 된다
                    if (chk1 > chk2)
                    {

                        board[chk1] = chk2;
                    }
                    else board[chk2] = chk1;
                }
                else if (temp[0] == 1)
                {

                    // 확인
                    if (chk1 == chk2) sw.Write(YES);
                    else sw.Write(NO);
                }
            }

            sw.Close();
        }

        static int Find(int[] _board, Stack<int> _calc, int _chk)
        {

            int chk = _chk;

            while (chk != _board[chk])
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
