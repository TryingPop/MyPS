using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 17
이름 : 배성훈
내용 : 여행 가자
    문제번호 : 1976번

    유니온 파인드로 연결되어져 있는지 확인!

    조건에 A -> B이면 B -> A 가 보장되므로 유니온 파인드를 쓸 수있다
    해당 조건이 없는 경우 정점의 개수가 500 이하이므로 플로이드 워셜 알고리즘이 더 좋아보인다

    조건 A -> B이면 B -> A가 보장되어서 무턱대고
    root[j] == 1이면, board[j] = board[i]로 해서 틀렸다
    다음과 같이 입력이 주어지면
        5
        5
        0 1 0 0 0
        1 0 0 0 1
        0 0 0 1 0
        0 0 1 0 1
        0 1 0 1 0
    board에 0 2 2 2 2 가 담긴다;
    실제로는 0, 1, 2, 3, 4, 5 모두 같은 집합이다
    유니온이 제대로 안됐다

    그래서, 넣을 때, 현재 i의 부모 pi를 찾고, 그리고 j의 부모 pj를 찾아 board[pj] = board[pi]로 했다

    그리고 출력할 때 그냥 찾으면 될줄 알았는데, 유니온을 부모만 갱신해줘서 갱신안된 자녀가 존재할 수 있다
    그래서 board[chk[0] - 1] == board[chk[i] - 1]을 하면 안된다
    대신 chk[0] - 1의 부모 p0를 찾고, chk[i] - 1의 부모 pi를 찾아 같은지 비교해야한다!
*/

namespace BaekJoon._37
{
    internal class _37_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());
            int m = int.Parse(sr.ReadLine());

            int[] board = new int[n];

            for (int i = 0; i < n; i++)
            {

                board[i] = i;
            }

            Stack<int> s = new Stack<int>();
            for (int i = 0; i < n; i++)
            {

                int[] root = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 유니온 풀이!
                // A -> B이면 B -> A가 보장되어져 있다!
                //


                int calc = Find(board, i, s);
                for (int j = 0; j < n; j++)
                {

                    if (root[j] == 1) 
                    {

                        int parent = Find(board, j, s);
                        board[parent] = calc; 
                    }
                }
            }


            int[] chk = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int curGroup = board[chk[0] - 1];
            bool result = true;
            
            // 파인드
            for (int i = 1; i < m; i++)
            {

                if (board[chk[i] - 1] != curGroup)
                {

                    result = false;
                    break;
                }
            }

            if (result) Console.WriteLine("YES");
            else Console.WriteLine("NO");
        }

        static int Find(int[] _board, int _chk, Stack<int> _calc)
        {

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
