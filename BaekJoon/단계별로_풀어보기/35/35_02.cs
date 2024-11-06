using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 8
이름 : 배성훈
내용 : 가장 긴 증가하는 부분 수열 4, 5
    문제번호 : 14002번, 14003번

    둘 다 같은 코드를 썼다
    주된 아이디어는 dp를 이용했다
    메모리는 3N + a을 사용한다 (N : 입력용 nums 배열, N : 인덱스 저장용 dp 배열, N : 부분수열 표시용 sub 배열, a : 기타 연산용 변수)

    기존 길이 찾기 로직은 nums의 수 nums[j]들을 비교하면서 다음 조건에 맞춰 sub에 입력한다
        1. sub 배열에 아무것도 없는경우 0번 인덱스에 해당 값을 입력한다
        2. sub 배열에서 nums 보다 큰 수 중 가장 작은 인덱스를 찾는다
            즉, nums[j]가 0, 1, 2, ..., i항까지에 대해 nums[j] > sub[0], nums[j] > sub[1], .... nums[j] > sub[i]이고
                nums[j] <= sub[i + 1]인  i + 1 이 존재하면, sub[i + 1] = nums[j]로 바꾼다 좀 더 작은 수로 갱신한다!
        3. 2번을 만족하는 인덱스가 없다면, sub의 입력된 마지막 원소에 nums[j]를 추가한다

    그러면 nums의 모든 원소를 다 순서대로 탐색하면 sub 배열에는 수들이 입력되어져 있다
    여기서 sub에 0이 아닌 입력된 수들의 개수와 가장 긴 증가하는 부분수열의 길이와 같다
    또한 0이 아닌 수 들 중에 가장 뒤에 입력된 값은 가장 긴 증가하는 부분수열의 끝 값 중 하나가 기록된다(해당 로직에 의하면 가장 작은 값이 기록된다!)
    
    여기서 가장 긴 증가하는 부분수열의 끝 값 중 가장 작은 값이 기록되는 사실로 
    sub에 입력하는 연산 중 값이 갱신될 때 만약 sub[i]가 갱신되면
    바로 앞의 항을 sub[i - 1](없으면 -1)을 dp에 기록하는게 좋아 보였다

    입력 범위를 보면 두 문제다 항의 개수 즉 인덱스는 1 ~ 100만이고, 값 입력 범위는 -10억 ~ 10억이므로
    값보다는 인덱스를 기준으로 담는게 좋아보인다(14002번은 인덱스는 1 ~ 1000, 값도 1 ~ 1000)

    이에 sub에는 인덱스를 넣었다
    nums에서 크기를 비교하며 sub에 인덱스를 넣었다 그래서 sub에서 nums를 기준으로 비교하는 경우에는 정렬성이 보장된다

    그리고 dp에는 sub에서 앞항을 기록한다
    sub[i] = j일 때, dp[j] = i - 1 >= 0 ? sub[i - 1] : -1;

    그러면 가장 긴 증가하는 부분수열을 찾고나면 sub와 dp에는 수가 담겨져 있다

    만약 모든 sub에 수가 0이라면 0항만 있는게 가장 긴 부분수열이된다 
    (이러한 반례를 처리하기 싫다면, sub 배열을 생성할 때 -1로 초기화하던 길이가 갱신될 때마다 기록해야한다 나는 따로 변수를 줘서 반례처리를 했다)

    sub에 입력된 0이 아닌 수 들 중 가장 뒤에 있는 j항을 기준으로
    sub[j - 1] = dp[j]로 바꾼다 sub[1 - 1] = dp[1] 까지 해주면 sub에는 가장 긴 증가하는 부분수열의 인덱스가 담겨져 있다!
    이를 다시 nums 씌우면 가장 긴 증가하는 부분수열이 된다!

    이렇게 제출하니 14002는 68ms, 14003은 604ms(-> 572ms)에 풀어냈다
*/

namespace BaekJoon._35
{
    internal class _35_02
    {

        static void Main2(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[] nums = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            sr.Close();

            // 이전 숫자 저장용!
            int[] sub = new int[len];
            // 이전 인덱스 저장용
            int[] dp = new int[len];

            // 초기에 0을 채운다
            sub[0] = 0;
            // 앞의 원소가 없을 알리기 위해 -1을 넣었다
            dp[0] = -1;
            // 현재 sub에 들어가 있는 끝 인덱스
            int endIdx = 0;

            // 가장 긴 부분 수열 찾기
            for (int i = 1; i < len; i++)
            {

                // sub에는 nums를 기준으로 증가하는 (정렬된) 수들을 담는다!
                // 그래서 이진 탐색 사용가능!
                int start = 0;
                int end = endIdx;

                // 연산용 현재 nums의 값
                int curValue = nums[i];

                // 이진 탐색
                // sub에서 
                while(start <= end)
                {

                    int mid = (start + end) / 2;
                    int midValue = nums[sub[mid]];

                    // 이후에는 start가 curValue < sub[j]인 j의 최소값이 담기게 세팅!
                    // 만약 모든 j에 대해 curValue > sub[j]면 start == j + 1 가 나온다
                    if (midValue < curValue) start = mid + 1;

                    // end는 curValue > sub[j]인 j의 최대값이 담기게 했다
                    // 만약 모든 j에 대해 curValue < sub[j]이면 end == -1이다 그러나 여기서는 안쓰여서 end는 신경 안써도 된다
                    else if (midValue > curValue) end = mid - 1;

                    // 중복을 허용하지 않기에 같은 값이면 해당 위치를 반환
                    else
                    {

                        // (midValue == curValue) 경우이다 (순서 공리)
                        start = mid;
                        break;
                    }
                }

                // 이진 탐색이 끝나면 여기로 온다
                // start는 위에서 말했듯이
                // start에는 curValue < sub[j]인 j의 최소값이 담기게 세팅!했다
                // 만약 모든 j에 대해 curValue > sub[j]면 start == j + 1 가 나온다
                // 만약 curValue = sub[j]이면 start = j이다
                if (start == 0) dp[i] = -1;
                else
                {

                    // start >= 1임이 보장되었으므로 이전 항이 존재!
                    dp[i] = sub[start - 1];
                    // 길이가 1보다 크므로 길이 확인
                    if (endIdx < start) endIdx = start;
                }

                // 값 갱신, 기록된 곳이면 작은 값으로 갱신된다!
                sub[start] = i;
            }

            // 이제 가장 긴 증가하는 부분수열 찾기
            for (int j = endIdx - 1; j >= 0; j--)
            {

                sub[j] = dp[sub[j + 1]];
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(endIdx + 1);

                for (int i = 0; i <= endIdx; i++)
                {

                    sw.Write(nums[sub[i]]);
                    sw.Write(' ');
                }
            }
        }
    }
}
