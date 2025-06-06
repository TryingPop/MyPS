using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 과제 제출하기
    문제번호 : 30686번

    그리디, 브루트포스 알고리즘 문제다
    공부한거 확인하는 배열의 인덱스를 잘못 설정해 한 번 틀렸다

    아이디어는 다음과 같다
    먼저, 문제 푸는 순서를 브루트 포스로 모두 정한다
    그리고 각 문제 푸는 순서에 따라 그리디로 암기해야할 최소 횟수를 찾는다
    최소 횟수는 문제 푸는 날 알고 있는 지식인지 확인하고 외운거면 넘어가고
    까먹었으면 공부 횟수를 추가한다
    이렇게 공부횟수를 추가한게 최소값이 보장된다

    해당 코드로 아이디어를 풀어 제출하니 304ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0385
    {

        static void Main385(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            // 까먹는데 걸리는 시간
            int[] d = new int[n];
            for (int i = 0; i < n; i++)
            {

                d[i] = ReadInt();
            }

            // 문제 푸는데 필요한 선행지식
            int[][] sol = new int[m][];

            for (int i = 0; i < m; i++)
            {

                int k = ReadInt();
                sol[i] = new int[k];

                for (int j = 0; j < k; j++)
                {

                    sol[i][j] = ReadInt() - 1;
                }
            }
            
            sr.Close();

            // 문제 푸는 순서
            int[] order = new int[m];
            // 기억하고 있는거
            int[] remem = new int[n];
            // 문제 순서 정할 때 이미 정했는지 확인용
            bool[] visit = new bool[m];

            int ret = DFS(0);
            Console.WriteLine(ret);

            int DFS(int _depth)
            {

                int ret = 10_000;
                if (_depth == m)
                {
                    ret = 0;
                    for (int i = 0; i < m; i++)
                    {

                        // 현재 풀 문제
                        int curSolve = order[i];
                        for (int j = 0; j < sol[curSolve].Length; j++)
                        {

                            // 문제 푸는데 선행지식 있는지 판별
                            int curStudy = sol[curSolve][j];
                            if (remem[curStudy] > 0) continue;

                            // 까먹은 경우 공부
                            ret++;
                            remem[curStudy] = d[curStudy];
                        }

                        for (int j = 0; j < remem.Length; j++)
                        {

                            // 까먹기
                            if (remem[j] == 0) continue;
                            remem[j]--;
                        }
                    }

                    // 초기화
                    for (int i = 0; i < remem.Length; i++)
                    {

                        remem[i] = 0;
                    }

                    return ret;
                }

                for (int i = 0; i < m; i++)
                {

                    // 문제 푸는 순서 정하기
                    if (visit[i]) continue;
                    visit[i] = true;
                    order[_depth] = i;
                    int chk = DFS(_depth + 1);
                    ret = chk < ret ? chk : ret;
                    visit[i] = false;
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
