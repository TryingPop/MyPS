using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 22
이름 : 배성훈
내용 : N과 M (1)
    문제번호 : 15649번

    순열(Permutation) 경우의 수 출력하는 문제
*/

namespace BaekJoon._25
{
    internal class _25_01
    {

        static void Main1(string[] args)
        {
#if first
            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();


            int[] board = new int[info[0] + 1];
            bool[] chk = new bool[info[0] + 1];

            board[0] = info[1];
            for (int i = 1; i <= info[0]; i++)
            {


                board[i] = i;
            }


            Back(board, chk);
#elif true
#endif
        }
#if first
        // 재귀를 이용한 백트래킹
        // 시간이 엄청 걸린다(최고속의 20배), 메모리(최고속의 3배)도 많이 잡아 먹는다
        // 다른사람이 푼 것을 보니 DFS로 풀어나갔따
        // 보드를 너무 크게 설정했다
        static void Back(int[] board, bool[] chk, int step = 0, string str = "")
        {
            
            // 재귀를 이용했으므로 함수 탈출
            // 길이만큼 진행했는지 확인
            if (step >= board[0])
            {

                // 길이만큼 진행했으니 결과물을 출력과 반환
                Console.WriteLine(str);
                return;
            }

            // 조건 확인
            for (int i = 1; i <= board.Length - 1; i++)
            {

                // 중복 불가능하므로 앞에께 사용안된 경우
                if (chk[i] == false)
                {

                    // 재귀에서 중복 진입 방지
                    chk[i] = true;
                    
                    // 현재 문자를 추가하고 현재 단계 추가해서 재귀
                    Back(board, chk, step + 1, $"{str}{i} ");
                    
                    // 재귀에서 해당 위치에 i문자를 포함한 모든 경우의 수를 다했으므로 중복진입 가능하게 변경 
                    chk[i] = false;
                }
            }
        }
#elif true
        // 클래스로 표현했고
        // 크기도 더 간결해졌다
        // 또한 입출력 버퍼를 통해 더 빠른거 같아 보인다
        public class PS
        {

            private static StreamWriter sw = null;

            private readonly int n;
            private readonly int m;

            private bool[] visited;
            private int[] ans;

            public PS(int n, int m)
            {

                if (sw == null) sw = new StreamWriter(Console.OpenStandardOutput());

                this.n = n;
                this.m = m;
                visited = new bool[n + 1];
                ans = new int[m];
            }

            public void Solve()
            {


                DFS(0);
                sw.Close();
            }

            public void DFS(int depth)
            {

                if (depth == m)
                {

                    for (int i = 0; i < m; i++)
                    {

                        sw.Write(string.Join(' ', this.ans));
                    }

                    sw.Write('\n');
                    return;
                }
                
                for (int i = 0; i <= n; i++)
                {

                    if (!visited[i])
                    {

                        visited[i] = true;

                        ans[depth] = i;
                        DFS(depth + 1);

                        visited[i] = false;
                    }
                }
            }
        }
#endif
    }
}
