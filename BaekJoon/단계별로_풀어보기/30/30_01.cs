using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 16
이름 : 배성훈
내용 : 파일 합치기
    문제번호 : 11066번


    문제를 잘못 읽어서 처음에는 순차적으로 하나씩 더해가는 것인줄 알았다
    다음으로는 '소설' 즉, 합쳤을 때 1 2 3 4 5 순서를 유지해야하는 성질을 무시해서
    이상한 결과가 나왔다 그런데 해당 코드와 관련된 문제가 있었는데 시간 초과로 틀렸다

    이제 본론으로 돌아오자
    이차원 배열을 이용한 DP 문제?
    풀이가 안떠올라 다른 사람 풀이 아이디어를 참고해 작성했다

    핵심은 i인덱스에서 j (> i)인덱스까지 최소 시간을 저장해 푸는 문제이다
    i 에서 j 이므로 이차원 배열이 필요하고
    저장하는 과정은 삼중 포문을 이용했다 O(n^3) 복잡도이다.

    찾아보니 빠르게 푼 사람은 O(n^2)으로 풀 수 있다고 했다
    https://js1jj2sk3.tistory.com/3
    사이트를 추후에 참고해서 3번째 방법으로 풀어봐야겠다!
*/

namespace BaekJoon._30
{
    internal class _30_01
    {

        static void Main1(string[] args)
        {

            // 파일의 최대 크기 1개당 1만을 넘지 않고, 최대 500개니 500만 이상으로 잡으면 된다
            const int MAX = 1_000_000_001;

            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 전체 문제 수
            int len = int.Parse(sr.ReadLine());

            // 여러 문제 이기에 덮어쓰면서 사용
            // 뒤에 값이 남더라도 해당 길이만큼만 이용하고 최대 500개 이므로
            // 문제에 이상이 있는게 아닌 이상 오류가 일어날 수 없다
            int[] sums = new int[500];
            // 이차원 배열이 아닌 이유는 이차원 배열이 성능이 안좋다고 나와있다
            // 그리고 msdn에서도 이차원 배열보다 다음 배열을 권장하고 있다
            int[][] dp = new int[500][];

            for (int i = 0; i < len; i++)
            {

                // 입력 수
                int num = int.Parse(sr.ReadLine());

#if wrong_1
                // 문제를 잘못 읽어서 나온 풀이.
                // 순차적으로 더하면 되는줄 알았다
                int[] arr = sr.ReadLine().Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();

                int sum = arr[0] + arr[1];
                int result = sum;
                for (int j = 2; j < num; j++)
                {

                    sum += arr[j];
                    result += sum;
                }

                sb.AppendLine(result.ToString());

#elif wrong_2
                // 마찬가지로 문제를 잘못읽어서 나온 풀이 2
                // 파일 합쳐야하는 순서가 있었다!
                // 파일 합치기 3 문제번호 13975번
                // 자료구조, 우선순위 큐, 그리디 알고리즘과 연관
                int[] arr = sr.ReadLine().Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();
                int result = 0;

                for (int j = 1; j < num; j++)
                {

                    arr[j] = arr[j - 1] + arr[j];
                    result += arr[j];
                    

                    for (int k = j + 1; k < num; k++)
                    {

                        if (arr[k - 1] <= arr[k]) break;

                        int temp = arr[k];
                        arr[k] = arr[k - 1];
                        arr[k-1] = temp;
                    }
                }

                sb.AppendLine(result.ToString());
#endif
                
                // 문제 저장하는 배열
                int[] arr = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // sum과 dp 새로 생성
                for (int j = 0; j < num; j++)
                {

                    if (j > 0) sums[j] = sums[j - 1] + arr[j];
                    else sums[j] = arr[j];

                    dp[j] = new int[num];
                }

                // 합칠 소설의 길이
                for (int length = 1; length <= num; length++)
                {

                    // 시작 인덱스
                    for (int startIdx = 0; startIdx < num - length; startIdx++)
                    {

                        int endIdx = startIdx + length;

                        // 최대값을 넣는다
                        dp[startIdx][endIdx] = MAX;

                        // 사이 값들의 합과 총합 (두 데이터 합)
                        // 중 가장 작은 값이 그 구간의 최소값이 된다
                        for (int mid = 0; mid < length; mid++)
                        {

                            // 기존 데이터
                            int _1 = dp[startIdx][endIdx];
                            // 해당 구간의 데이터
                            int _2 = dp[startIdx][startIdx + mid] + dp[startIdx + mid + 1][endIdx] + 
                                sums[endIdx] - sums[startIdx] + arr[startIdx];

                            // 비교해서 갱신
                            dp[startIdx][endIdx] = Math.Min(_1, _2);
                        }
                    }
                }

                // 0번부터 끝번까지의 최소값을 sb에 넣는다
                sb.AppendLine(dp[0][num - 1].ToString());
            }
            sr.Close();

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }
    }
}
