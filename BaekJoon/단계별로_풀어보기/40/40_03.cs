using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 22
이름 : 배성훈
내용 : 방사형 그래프
    문제번호 : 25308번

    앞의 CCW 아이디어를 이요하면 쉬운 문제다

    먼저 아이디어는 다음과 같다
    0, 2, 1가 볼록한지 확인
    그리고 1, 3, 2가 볼록한지 확인
    ..., 5, 7, 6이 볼록한지 확인
    6, 0, 7이 볼록한지 확인
    마지막으로 7, 1, 0이 볼록한지 확인해야한다
    (이걸 확인 안해서 CCW가 문제인줄 알고, 문제 없는 CCW에서 헤맸다)
    
    왜 0, 1, 2가 볼록한지 확인하는게 아닌 0, 2, 1이 볼록한지인가 하면

    0과 2를 이은 선분에 1이 위에 있는지 밑에 있는지 대상이기 때문이다!
    (물론 0, 1, 2로 해서 부호를 바꿔도 된다!)
    
    그래서 먼저 동적 계획법 방법으로 경우의 수를 모두 체크했다
    다음으로, 이제 경우의 수 찾는건 DFS 탐색을 이용했다
    물론 중간에 오목한게 하나라도 들어오면 안되므로 탈출하는 백트래킹?도 섞었다
*/

namespace BaekJoon._40
{
    internal class _40_03
    {

        const int MAX_LEN = 8;
        
        static void Main3(string[] args)
        {


            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);


            // 먼저 볼록한 것들 찾기;
            // 인덱스가 볼록하면 참!
            bool[][][] convex = new bool[MAX_LEN][][];
            for (int i = 0; i < MAX_LEN; i++)
            {

                convex[i] = new bool[MAX_LEN][];

                for (int j = 0; j < MAX_LEN; j++)
                {

                    convex[i][j] = new bool[MAX_LEN];
                }
            }



            // 볼록하다면 회전해도 볼록하다! 그래서 값계산!
            // 중복이 6개씩 있으나 그냥 무시하고 연산한다!
            // 가능한지 판별!
            double calc = Math.Sqrt(2.0);
            for (int fst = 0; fst < MAX_LEN; fst++)
            {
                
                for (int snd = 0; snd < MAX_LEN; snd++)
                {

                    for (int trd = 0; trd < MAX_LEN; trd++)
                    {

                        // 각 i번째 점의 값을 num[i]라 하면
                        // 루트 2를 r2라 하자
                        // 1번의 좌표는 ( 0, num[1] )
                        // 2번의 좌표는 ( 1/r2 * num[2], 1/r2 * num[2] )
                        // 3번의 좌표는 ( num[2], 0 )
                        // 여기서 외적을 통해 너비 w값을 구하면
                        // w = (1 / r2) * (num[2] * (num[1] + num[3]) - (num[1] * num[3])
                        // w > 0 ? w < 0 ? 판별에서는
                        // r2 * w > 0 ? r2 * w < 0 ?으로 해도 이상없으므로 r2를 곱해줬다

                        // 그리고 2번, 3번, 4번 좌표의 볼록 여부 판정은 해당 좌표가 아닌 0, 1, 2번 좌표로 한칸씩 돌려서 했다
                        // 회전이동으로는 외적 연산에(det) 영향을 안주기 때문이다
                        // 그래서 3중 포문을 이용했다!
                        double chk = nums[snd] * (nums[fst] + nums[trd]);
                        chk -= calc * nums[fst] * nums[trd];

                        // =없어도 된다 해당 범위 안에서는 같은 경우가 나올 수 없다
                        if (chk >= 0) convex[fst][snd][trd] = true;
                    }
                }
            }

            // DFS 탐색
            int[] board = new int[MAX_LEN];
            bool[] visit = new bool[MAX_LEN];
            int result = 0;

            DFS(nums, convex, board, visit, 0, ref result);
            Console.WriteLine(result);
        }

        static void DFS(int[] _nums, bool[][][] _convex, int[] _board, bool[] _visit, int _idx, ref int _result)
        {

            if (_idx == _board.Length)
            {

                // 마지막에 6,7,0이 볼록하고 7,0,1이 볼록한지 확인!
                // 두 경우 모두 볼록해야 원하는 경우다!
                if (_convex[_board[6]][_board[7]][_board[0]] && _convex[_board[7]][_board[0]][_board[1]]) _result++;

                return;
            }

            for (int i = 0; i < MAX_LEN; i++)
            {

                
                if (_visit[i]
                    // 조건 만족 안하면 더 깊이 탐색안한다!
                    || (_idx > 1 && !_convex[_board[_idx - 2]][_board[_idx - 1]][i])) continue;
                
                _visit[i] = true;

                _board[_idx] = i;
                DFS(_nums, _convex, _board, _visit, _idx + 1, ref _result);
                _visit[i] = false;
            }
        }
    }
}
