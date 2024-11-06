using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 26
이름 : 배성훈
내용 : 기차표 검사
    문제번호 : 3167번

    그리디 알고리즘 문제다
    아이디어는 다음과 같다
    티켓을 확인안한 인원의 최소값은 티켓을 확인한 인원을 우선적으로 내리면 된다
    최대값은 티켓을 확인안한 인원부터 내린다
    그리고 시뮬레이션 돌려서 정답을 찾아냈다

    탑승한 역에서 바로 내리는 경우는 없기에
    내릴 사람 먼저 내리고, 이후에 탑승을 시켜야
    탑승한 역에서 하차가 이뤄지지 않는다
*/

namespace BaekJoon.etc
{
    internal class etc_0840
    {

        static void Main840(string[] args)
        {

            StreamReader sr;
            int n, k;
            int[] u, d;
            int ret1, ret2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret1 = 0;
                ret2 = 0;

                int curU = 0;
                int curD = 0;

                // 최소값 찾기
                for (int i = 0; i < n; i++)
                {

                    // 승객 내리기
                    // 최소값은 내릴 때 티켓 확인한 인원을 최우선으로 내린다
                    if (curU < d[i])
                    {

                        int pop = d[i] - curU;
                        curU = 0;
                        curD -= pop;

                        ret1 += pop;
                    }
                    else curU -= d[i];

                    // 승객탑승
                    curD += u[i];

                    // 조사
                    if (i % k == 0)
                    {

                        curU += curD;
                        curD = 0;
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    // 승객 내리기
                    // 최대값은 내릴 때 티켓 확인 안한 인원을 최우선으로 내린다
                    if (curD < d[i])
                    {

                        ret2 += curD;
                        curU -= d[i] - curD;
                        curD = 0;
                    }
                    else
                    {

                        ret2 += d[i];
                        curD -= d[i];
                    }

                    // 승객 탑승
                    curD += u[i];

                    // 조사
                    if (i % k == 0)
                    {

                        curU += curD;
                        curD = 0;
                    }
                }

                Console.Write($"{ret1} {ret2}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                u = new int[n];
                d = new int[n];
                for (int i = 0; i < n; i++)
                {

                    d[i] = ReadInt();
                    u[i] = ReadInt();
                }

                sr.Close();
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

#if other
// #include <bits/stdc++.h>
using namespace std;
int main() {
  int N, K, Yes = 0, No = 0, Min = 0, Max = 0, M[1001], P[1001];
  cin >> N >> K;
  for(int i = 1; i <= N; i++) scanf("%d %d", &M[i], &P[i]);
  for(int i = 1; i <= N; i++) {
    if(Yes < M[i]) { No -= M[i] - Yes; Min += M[i] - Yes; }
    Yes -= min(M[i], Yes); No += P[i];
    if(!((i - 1) % K)) { Yes += No; No = 0; }
  }
  for(int i = 1; i <= N; i++) {
    Max += min(M[i], No);
    if(No < M[i]) Yes -= M[i] - No;
    No = No - min(M[i], No) + P[i];
    if(!((i - 1) % K)) { Yes += No; No = 0; }
  }
  printf("%d %d", Min, Max);
  return 0;
}
#endif
}
