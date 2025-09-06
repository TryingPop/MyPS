using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
퍼즐 해설 및 DP 로직 설명

[1] 퍼즐의 기본 원리
- 아이들은 모자의 총 개수(B: 검은색, W: 흰색)를 알고 있다.
- 계단 위(뒤)에서부터 차례로 질문을 받는데, 아이는 앞(계단 아래)만 볼 수 있다.
- "모른다"라는 대답이 나올 때마다, 남은 아이들에 대해
  "앞쪽에 존재할 수 있는 검은 모자의 최대 개수"가 1씩 줄어든다는 사실이 공용 지식이 된다.
- 따라서 t명이 연속해서 "모른다"를 말하면,
  앞의 j = k - t 명에 대해 검은 모자 수 b_j의 범위가
  [Lk, Rk - (k-j)] 로 갱신된다.
  (여기서 Lk = max(0, k-W), Rk = min(k, B))

[2] 아이가 자기 모자를 확정할 수 있는 조건
- 뒤에서 한 칸 위 아이가 보는 앞쪽 검은 모자 수를 x라 하자.
- x == L_j - 1 이면 → 본인은 반드시 검은색이어야 함.
- x == R_j 이면 → 본인은 반드시 흰색이어야 함.
- 둘 다 아니면 "모른다"를 말한다.

[3] "뒤에서 i번째 아이가 처음 정답을 말한다"는 조건
- 위쪽 i-1명은 모두 "모른다"였다는 뜻이다.
- 즉, 앞의 j명 검은 모자 합에 대한 상한이 i-1번 줄어든 상태가 공유된 상황이다.
- 뒤에서 i번째, 즉 앞에서 p = k - i + 1번째 아이가
  위 조건을 만족해 자기 모자를 확정한다.

[4] 동적 계획법(DP) 설계
- 상태: dp[j][b] = 앞에서 j명을 배치했을 때, 그 중 검은 모자가 b개인 경우의 수.
- 초기값: dp[0][0] = 1
- 전이:
  - 보통의 경우(아이가 아직 "맞히는" 위치가 아니면):
    - 흰 모자를 놓아 dp[j+1][b] 증가
    - 검은 모자를 놓아 dp[j+1][b+1] 증가
    - 단, j+1 ∈ [p, k-1]이면 "모른다" 제약 Lk ≤ b_{j+1} ≤ upper[j+1]를 만족해야만 유효.
  - "정답을 맞히는" 위치 (j+1 == p):
    - b == Lp - 1이면 → 본인은 검은색으로 강제
    - b == Rp 이면 → 본인은 흰색으로 강제
    - 둘 다 아니면 이 경로는 폐기
- upper[j] = Rk - (k-j) (상한이 줄어든 값)

[5] 종료와 정답
- j == k에 도달하면 dp[k][b]를 모두 합산.
- 단, b는 Lk ≤ b ≤ Rk 범위여야 하고, 검은 모자 총량 ≤ B, 흰 모자 총량 ≤ W 제약도 만족해야 한다.
- 최종 답을 32749로 나눈 나머지를 출력.

[6] 요약
- "모른다" → 앞쪽 검은 모자 수의 상한이 1 줄어듦.
- "정답" → 자기 색이 강제됨.
- 이 두 가지 규칙을 DP로 반영해 경우의 수를 모두 세고, 모듈러 32749를 취한다.
*/

/*
작은 입력 예시로 DP 테이블 전개 확인

[예제 1] B=2, W=2, k=3, i=2
- p = k-i+1 = 2
- Lk=1, Rk=2
- upper[j] = [ -,0,1,2 ]
- "모른다" 제약: j=2에서 b2 ∈ [1,1]

DP 전개:
j=0: dp[0][0]=1
 j=0→1: 흰 → dp[1][0]=1, 검 → dp[1][1]=1
 j=1→2 (결정 위치 p=2):
   b1=0=Lp-1 → 본인 검 → b2=1
   b1=1=Rp   → 본인 흰 → b2=1
   결과 dp[2][1]=2
 j=2→3:
   dp[2][1]=2 → 흰: dp[3][1]=2, 검: dp[3][2]=2
최종: dp[3][1]=2, dp[3][2]=2 → 합 = 4

[예제 2] B=1, W=1, k=2, i=1
- p=2
- Lk=1, Rk=1
- upper[j] = [ -,0,1 ]
- "모른다" 제약 없음

DP 전개:
j=0: dp[0][0]=1
 j=0→1: 흰 → dp[1][0]=1, 검 → dp[1][1]=1
 j=1→2 (결정 위치 p=2):
   b1=0=Lp-1 → 본인 검 → b2=1
   b1=1=Rp   → 본인 흰 → b2=1
   결과 dp[2][1]=2
최종: dp[2][1]=2 → 합 = 2

[예제 3] B=2, W=1, k=3, i=1
- p=3
- Lk=2, Rk=2
- upper[j] = [ -,0,1,2 ]
- "모른다" 제약 없음

DP 전개:
j=0: dp[0][0]=1
 j=0→1: 흰 → dp[1][0]=1, 검 → dp[1][1]=1
 j=1→2:
   dp[1][0]=1 → 검 → dp[2][1]=1
   dp[1][1]=1 → 흰 → dp[2][1]+=1 (총2), 검 → dp[2][2]=1
 j=2→3 (결정 위치 p=3):
   Lp=2, Rp=2
   b2=1=Lp-1 → 본인 검 → dp[3][2]+=2
   b2=2=Rp   → 본인 흰 → dp[3][2]+=1
최종: dp[3][2]=3 → 합 = 3

------------------------------------
핵심 정리:
- "모른다" → 앞쪽 검은 모자 수 상한 1씩 감소
- 결정 위치 p → b_{p-1}=Lk-1이면 본인 검, b_{p-1}=upper[p]이면 본인 흰
- DP는 각 전이마다 재고(B,W)와 제약을 확인
*/


namespace BaekJoon.etc
{
    internal class etc_1853
    {

        static void Main1853(string[] args)
        {

            /*
            12433번 - 모자 쓴 아이들(Small)
            시간복잡도는 O(k⋅B) 상태에서 각 위치별 상수 전이 ⇒ 매우 빠릅니다(최대 k,B≤20).
            핵심 아이디어는 공개 가능한 정보(범위 [Lj,Rj])가 위쪽 경계만 1씩 줄어든다는 점과, 답을 맞히는 순간의 색이 강제된다는 점입니다.
            모드는 32749를 사용
            */

            const int MOD = 32749;

            var sr = new System.IO.StreamReader(Console.OpenStandardInput());
            var sw = new System.IO.StreamWriter(Console.OpenStandardOutput());
            int T = int.Parse(sr.ReadLine().Trim());
            for (int tc = 1; tc <= T; tc++)
            {
                var parts = sr.ReadLine().Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToArray();
                int B = parts[0], W = parts[1], k = parts[2], i = parts[3];

                // 타깃 위치 p: 앞(계단 아래)에서 p번째 == 뒤에서 i번째
                int p = k - i + 1;

                // 초기 범위 [Lk, Rk] for b_k (전체 k명 중 검은 모자 수)
                int Lk = Math.Max(0, k - W);
                int Rk = Math.Min(k, B);

                // 불가능한 경우(아이 수가 총 모자 수를 넘지 않도록 주어졌지만, 초기 범위가 공집합이면 0)
                if (Lk > Rk)
                {
                    sw.WriteLine($"Case #{tc}: 0");
                    continue;
                }

                // R_j = Rk - (k - j), L_j = Lk  (j ∈ [p, k-1]에서 '모른다' 제약 적용)
                int[] upper = new int[k + 1]; // upper[j] = R_j
                for (int j = 0; j <= k; j++)
                    upper[j] = Rk - (k - j);

                // dp[j][b] = 앞에서 j명 배치, 그 중 b명이 검은 모자인 경우의 수 (모든 제약 충족)
                int[,] dp = new int[k + 1, B + 1];
                dp[0, 0] = 1;

                for (int j = 0; j < k; j++)
                {
                    for (int b = 0; b <= Math.Min(j, B); b++)
                    {
                        int cur = dp[j, b];
                        if (cur == 0) continue;

                        int nextIndex = j + 1;

                        // 다음 위치가 p(뒤에서 i번째가 바로 이번에 답함)인 경우: 색이 강제됨
                        if (nextIndex == p)
                        {
                            // 앞의 p-1명 검은 모자 수 = b
                            // 규칙: b == Lp-1  -> 본인(위치 p)은 검은색
                            //       b == Rp    -> 본인(위치 p)은 흰색
                            int Lp = Lk;
                            int Rp = upper[p];

                            // 강제: BLACK
                            if (b == Lp - 1)
                            {
                                int nb = b + 1;
                                if (nb <= B && (nextIndex - nb) <= W)
                                {
                                    // 위치 p에서 확정으로 검은색 배치
                                    dp[nextIndex, nb] = (dp[nextIndex, nb] + cur) % MOD;
                                }
                            }
                            // 강제: WHITE
                            else if (b == Rp)
                            {
                                int nb = b; // 흰색
                                if (nb <= B && (nextIndex - nb) <= W)
                                {
                                    dp[nextIndex, nb] = (dp[nextIndex, nb] + cur) % MOD;
                                }
                            }
                            // 둘 다 아니면 이 경로는 불가(이 아이가 반드시 맞혀야 하므로)
                        }
                        else
                        {
                            // 자유 배치(단, 만약 nextIndex ∈ [p, k-1]이면 '모른다' 제약: Lk <= b_{next} <= upper[nextIndex])
                            bool needRange = (nextIndex >= p && nextIndex <= k - 1);
                            int low = Lk;
                            int high = upper[nextIndex];

                            // 흰색 배치
                            {
                                int nb = b;
                                if (nb <= B && (nextIndex - nb) <= W)
                                {
                                    if (!needRange || (low <= nb && nb <= high))
                                    {
                                        dp[nextIndex, nb] = (dp[nextIndex, nb] + cur) % MOD;
                                    }
                                }
                            }

                            // 검은색 배치
                            {
                                int nb = b + 1;
                                if (nb <= B && (nextIndex - nb) <= W)
                                {
                                    if (!needRange || (low <= nb && nb <= high))
                                    {
                                        dp[nextIndex, nb] = (dp[nextIndex, nb] + cur) % MOD;
                                    }
                                }
                            }
                        }
                    }
                }

                // 최종 합: j = k
                int ans = 0;
                for (int b = Lk; b <= Math.Min(Rk, B); b++)
                {
                    // 총 k명 중 b가 검은색, k-b가 흰색이므로 수량 제약 자동 충족
                    ans = (ans + dp[k, b]) % MOD;
                }

                sw.WriteLine($"Case #{tc}: {ans}");
            }
            sw.Flush();
        }
    }
}
