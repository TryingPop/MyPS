using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 주사위
    문제번호 : 1041번

    수학, 그리디 알고리즘 문제다
    n > 1이면 정육면체는 많아야 3개의 면이 보인다
    n == 1이면 5개의 면이 보인다

    그래서 n == 1이면 최소값 하나만 제거하고 나머지를 더한 값을 바로 출력한다

    이제 n > 1인 경우
    가장 최소값인 면이 보이는 개수,
    두 번째 최소값인 면이 보이는 개수,
    세 번째 최소값인 면이 보이는 개수
    를 세었는데, 여기서 두 번째 최소값과, 세 번째 최소값을 구해

    추가해야하는 면들만 찾았다
    그래서 최소값이 보이는 면의 개수는 정육면체의 모든면(밑면은 제외!)에 최소값인 면을 색칠한다

    그리고 두 번째 최소인 면들을 보여야하는 블록에 한해 모두 한 면만 두번째 최소면을 덧칠한다
    덧칠은 두 번째 최소와 첫 번째 최소 차이를 더해주면 된다

    마지막으로 세 번재 최소값인 면을 보여야하는 블록의 개수에 세 번째 최소인 면을 덧칠한다
    세 번째 최소인 면을 보이는 면은 4개 뿐이고 여기에는 첫 번째 최소 값이 덧칠되어져 있다

    이러한 아이디어를 코드로 작성해 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0289
    {

        static void Main289(string[] args)
        {

            long n = long.Parse(Console.ReadLine());
            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            long[] min = new long[3];
            min[0] = nums[0] < nums[5] ? nums[0] : nums[5];
            min[1] = nums[1] < nums[4] ? nums[1] : nums[4];
            min[2] = nums[2] < nums[3] ? nums[2] : nums[3];
            
            Array.Sort(min);
            // 최소 면에 덧칠 하는 형식이라
            // 2번째, 3번째는 차이만 저장해두면 된다
            min[2] -= min[0];
            min[1] -= min[0];
            if (n == 1)
            {

                // 1개짜리는 별도로 가장 큰 면만 제외!
                long ret = 0;
                long max = 0;
                for (int i = 0; i < 6; i++)
                {

                    if (max < nums[i]) max = nums[i];
                    ret += nums[i];
                }
                ret -= max;
                Console.WriteLine(ret);
            }
            else
            {

                long ret = 5 * n * n;
                // 보이는 전체 면에 최소값을 덧칠
                ret *= min[0];
                // 2번째 최소가 보여야하는 면
                // 2 번째 최소인 면이 보여야 하는 모든 블록에 1면만 두 번째 면으로 덧칠한다
                // 첫번재 최소와 2번째 최소 차이만큼 더해주면 두 번째 최소가 된다
                ret += min[1] * (8 * (n - 1));
                // 블록의 개수로 접근했기에 
                // 최대 면이 보이는 갯수 4에 3번째 면으로 바꿔준다
                ret += min[2] * 4;

                Console.WriteLine(ret);
            }
        }
    }
}
