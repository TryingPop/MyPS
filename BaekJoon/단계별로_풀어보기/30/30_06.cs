using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 19
이름 : 배성훈
내용 : 앱
    문제번호 : 7579번

    입력 
    5 60            // 실행 중인 파일 수, 확보해야하는 메모리
    30 10 20 35 40  // 활성화 때 소모하는 메모리 
    3 0 3 5 4       // 종료할 때 필요한 메모리

    // 필요메모리를 확보함에 있어 종료할 때 메모리를 최소화하는 

    최저는 이라한다
    30 + 10 + 20 >= 60
    3 + 0 + 3 = 6

    찾는 방법 1
    일단 메모리 부터 맞춰서? 

    음? 동적 계획법인 만큼 메모리를 저장 해야할거 같다
    일단 for문 돌면서 ? cost 값이 최소일 때 맥스 값 찾을까?
    
    최저 코스트 >>> 에 따른 최대 용량 구하는 방법으로 풀었다
    최대 100개 있고, 개당 최대 크기가 100 이므로 전체는 10000 이하로 예상된다
    방법은 동전의 아이디어를 따왔다

    dp에는 해제 메모리에 따른 활성화에 사용되는 메모리를 기록
    즉, 인덱스는 해제 메모리가 되고, 값은 활성화 메모리이다
*/

namespace BaekJoon._30
{
    internal class _30_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            // 최대 크기가 100 이하이므로 100 을 곱해준다

            int[] memActive = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] memInActive = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            // 100 대신 memInActive.Max로 잡아도 된다
            int MAX = 100 * info[0];

            // dp 연산
            // 매번 int[]를 생성 하는 방법 보다 돌려쓰는 식으로 했다.
            int[][] dp = new int[2][];
            
            dp[0] = new int[MAX + 1];
            dp[1] = new int[MAX + 1];

            bool curZero = false;
            int len = MAX + 1;

            ///
            /// dp를 이용해 비활성화 메모리에서 가장 큰 메모리 해제 찾기
            ///
            for (int i = 0; i < info[0]; i++)
            {

                // 이전과 현재 구분
                curZero = !curZero;
                int curNum = curZero ? 0 : 1;
                int beforeNum = curZero ? 1 : 0;

                // 해당 반례 방지용으로 앞의 내용 덮어 씌운다!
                // 입력이
                // 5 1
                // 1 1 1 1 1
                // 5 4 3 1 2
                // 인 경우 해당 포문 없으면 틀린다!
                for (int j = 0; j < memInActive[i]; j++)
                {

                    dp[curNum][j] = dp[beforeNum][j];
                }

                // 크기 비교하며 덮어 쓴다
                for (int j = memInActive[i]; j < len; j++)
                {

                    // 이전꺼에 현재 크기를 덮어 씌우는 경우
                    int chk = dp[beforeNum][j - memInActive[i]] + memActive[i];
                    // 앞의 크기
                    dp[curNum][j] = dp[beforeNum][j];

                    // 비교해서 큰쪽으로 채워 넣는다!
                    if (dp[curNum][j] < chk) dp[curNum][j] = chk;
                }
            }

            // 마지막에 덮어 씌운 dp를 찾는다
            int idx = curZero ? 0 : 1;
            for (int i = 0; i < len; i++)
            {

                if (info[1] <= dp[idx][i])
                {

                    // 해당 인덱스 찾고 반환
                    Console.WriteLine(i);
                    break;
                }
            }

        }
    }
}
