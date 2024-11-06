using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 18
이름 : 배성훈
내용 : 예산
    문제번호 : 2512번

    아이디어는 다음과 같다 
    먼저 도시에 필요한 금액을 담은 nums 배열을 정렬한다 O(NlogN)

    그리고 sum 배열을 따로 할당한다 (dp)
    sum배열의 값은 nums배열의 0 ~ i번 인덱스까지의 도시들이 필요한 예산합이다 O(N)

    그리고 nums에 있는 값들로 예산액을 할당했을 때,
    현재 금액을 초과하지 않는 도시 수의 최대값을 찾는다 O(NlogN)
    
    해당 경우 right가 미만이 되게 세팅했다
    그래서 right는 인덱스이고, right + 1이 도시 수가 된다

        이분 탐색에 비교할 때, 검색에서 = 유무로 left를 항상 초과하게 만들지, right를 항상 미만이 되게 만들지 설정할 수 있다
            만약 left가 초과되게 세팅했다면, right는 이하이고
                left가 초과되게 세팅하는 방법은 목표에 ==인 경우 left = mid + 1연산을 해주면 된다
            right가 미만이 되게 세팅했다면 left는 이상이 된다
                right가 미만되게 세팅하는 방법은 목표에 ==인 경우 right = mid - 1연산을 하면 된다

        여기서는 오른쪽 끝을 이하로 일관성있게 세팅했다!

    이제 도시수를 찾았으면 정확한 금액을 이분 탐색으로 찾는다 O(NlogN)
    여기서 도시 수가 0이라면 right == -1이다..; 이걸 캐치 못해서 index 에러가 자꾸났다.

    이제 도시수를 찾았으니 정확한 금액을 이분탐색으로 찾는다
    범위는 다음과 같다 앞에서 도시 수 a를 찾았으므로 금액의 범위는 정렬된 nums의 인덱스 a의 값에서 인덱스 a + 1의 값이된다
    만약 a + 1 == 최대 도시 수라면, nums의 금액이 찾는 금액이다!

    그리고 금액을 찾는 이분탐색을 진행했다
    right (== max)를 이하가 되게 세팅해서 right 값을 반환했다

    그리고 해당 아이디어를 코드로 풀어서 제출하니 이상없이 통과했다
    이분탐색 저번에 이해했다고 생각했는데, 바로 답이 안나오는걸 보면 더 연습해야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0058
    {

        static void Main58(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int[] nums = new int[len];
            int[] sum = new int[len + 1];
            for (int i = 0; i < len; i++)
            {

                nums[i] = ReadInt(sr);
            }

            int goal = ReadInt(sr);

            sr.Close();

            Array.Sort(nums);

            for (int i = 0; i < len; i++)
            {

                sum[i + 1] = sum[i] + nums[i];
            }
            // 먼저 국가 수 부터 찾자!
            int ret;
            {

                // 국가 수 찾기
                int right = len - 1;
                int left = 0;

                while (left <= right)
                {

                    int mid = (left + right) / 2;

                    int money = sum[mid];
                    money += (nums[mid] * (len - mid));
                    if (money <= goal) left = mid + 1;
                    else right = mid - 1;
                }

                int cityNum = right + 1;
                int min = right < 0 ? 0 : nums[right];
                // 딱 커지는 순간!
                int max = right + 1 < len ? nums[right + 1] : min;

                // 금액 찾기
                while(min <= max)
                {

                    int mid = (min + max) / 2;

                    int money = sum[cityNum] + (len - cityNum) * mid;
                    if (money <= goal) min = mid + 1;
                    else max = mid - 1;
                }

                ret = max;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
