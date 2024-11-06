using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 14
이름 : 배성훈
내용 : 가장 긴 증가하는 부분 수열 2
    문제번호 : 12015번

    LIS 따라 작성 했다
    가장 긴 증가하는 부분수열을 찾는게 아닌 
    수열의 각 원소에 대해 해당 값을 끝값으로 하는 가장 긴 부분 수열을 찾는 과정을 덮어쓰며 기록하는 방식이다
    값을 덮어쓰기에 LIS에 기록된 값들이 가장 긴 부분 수열의 길이와 일치하기에 해당 방법을 이용해서 푼다

    만약 특정 원소 a에서 끝 맺을 때 a원소에 있는 위치까지가 a원소를 포함하는 가장 긴 부분수열 중 하나라 볼 수 있으나
    기록이 끝날 때 남은 메모가 가장 긴 증가하는 부분수열이라 볼 수는 없다

    s = 1, 2, 1, 2, 5인 길이 5짜리 수열을 예를 들어 확인해 보자
    최대값이 5이다

    그러면 5(최대값) + 1으로 구성되어 있고 수열과 크기가 같은 temp 배열을 만든다
    리스트로 만들경우 따로 값 입력이 필요없다!

    s의 각 원소 a에 대해 
    temp의 원소들과 비교해서 a보다 작은 원소들의 수 i를 찾는다
    만약 temp에 원소가 없는 경우 0을 반환!

    그리고 temp[idx] = a로 해서 덮어쓴다
    그러면 temp는 증가 수열이된다
    
    이 로직을 적용하면
    
    s의 첫 번째 원소 1은
    temp = 6, 6, 6, 6, 6
    1보다 작은 temp의 원소는 없으므로 i = 0
    temp[i] = temp[0] = 1이 된다

    s의 두 번째 원소 2는
    temp = 1, 6, 6, 6, 6
    temp[0] = 1만 2보다 작으므로 i = 1이고
    temp[i] = temp[1] = 2가 된다

    s의 세 번째 원소 1은
    temp = 1, 2, 6, 6, 6
    1보다 작은 temp의 원소는 없으므로 i = 0
    temp[i] = temp[0] = 1

    s의 네 번째 원소 2는
    temp = 1, 2, 6, 6, 6
    temp[0] = 1만 2보다 작으므로 i = 1 이고
    temp[i] = temp[1] = 2

    s의 마지막 원소 5은
    temp = 1, 2, 6, 6, 6
    temp[0] = 1, temp[1] = 2만 5보다 작으므로 i = 2이고
    temp[i] = temp[2] = 5

    그러면 temp = 1, 2, 5, 6, 6, 6
    여기서 MAX + 1 이 아닌 원소의 개수는 3이고
    이 3이 증가하는 부분수열의 최대 길이가 된다
    
    그러나 앞서 말했듯이 MAX를 제외한 temp의 원소 1, 2, 5가 가장 긴 증가하는 부분수열이라고는 볼 수 없다
    (10 20 9 로 해보면 된다)
*/

namespace BaekJoon._28
{
    internal class _28_06
    {

        static void Main6(string[] args)
        {

            const int MAX = 1_000_001;

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            
            int[] series = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            // 여기서 연산 ..? 14_12가 같은 문제이다 third 방법이 정답인거 같은데
            // 왜 정답이 나오는지는 원리를 먼저 추론해보자
            int idx = 0;

            int[] lis = new int[len];

            // 최대값 입력
            for (int i = 0; i < len; i++)
            {

                lis[i] = MAX;
            }


            for (int i = 0; i < len; i++)
            {

                // 여기서 ... 이진 탐색 시작!
                int start = 0;
                int end = idx;
                int answer = 0;

                while (start <= end)
                {

                    int mid = (start + end) / 2;


                    if (lis[mid] < series[i])
                    {

                        start = mid + 1;
                    }

                    else 
                    { 

                        end = mid - 1;
                        answer = mid;
                    }
                }

                lis[answer] = series[i];
                // 최대 인덱스 기록
                idx = answer == idx ? idx + 1 : idx;
            }

            int result = idx;

            Console.WriteLine(result);
        }
    }
}
