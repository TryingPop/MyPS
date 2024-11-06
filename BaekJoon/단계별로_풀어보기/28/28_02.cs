using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 12
이름 : 배성훈
내용 : 랜선 자르기
    문제번호 : 1654번

    제한 시간이 2초이므로 n log n 으로 풀어야한다
*/

namespace BaekJoon._28
{
    internal class _28_02
    {

        static void Main2(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] lines = new int[info[0]];

            for (int i = 0; i < info[0]; i++)
            {

                lines[i] = int.Parse(sr.ReadLine());
            }

            // 이진 탐색을 위해 정렬!
            lines = lines.OrderBy(x => x).ToArray();
            sr.Close();

            // 이제 정답찾기!
            long start = 1;
            // 맨 뒤의 인덱스 == 정렬로 인해 가장 큰 값 
            long end = lines[^1];

            // 이분 탐색
            while (start < end)
            {

                // 범위 때문에 long 타입으로 하지 않으면 여기서 오버플로우 발생!
                // +1 을 해줌으로써 루프에 빠지지 않는다!
                // 그래서 start에 해당 값을 포함해서 검색할 수 있다.
                long mid = (start + end + 1) / 2;

                long sum = 0;
                for (int i = 0; i < info[0]; i++)
                {

                    sum += lines[i] / mid;
                }

                // 조건을 만족하므로 해당 값을 포함해서 더 크게 잘라도 되는지 확인 시도
                if (sum >= info[1]) start = mid;
                // 조건을 만족하지 않으므로 해당값을 제외하고 더 작게 짤라야 하는경우
                else end = mid - 1;
            }

            // start == mid 가 된다
            Console.WriteLine(start);
        }
    }
}
