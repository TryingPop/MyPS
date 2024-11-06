using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 13
이름 : 배성훈
내용 : K번째 수
    문제번호 : 1300번
    
    K번째로 큰 수를 찾는 것인데
    문제에서 정렬되어 있으므로
    특정 수를 입력 했을 때 이거보다 작은 원소의 개수를 찾는다
    그리고 중복인 수가 있을 수 있으니 작은 원소의 개수가 K보다 크거나 같은 경우 중에서
    가장 작은 수를 찾게하는게 핵심이다


    size를 long으로 하니 해결되었다.
    아마도 size = 100_000 이 최대값이고 size * size = 10_000_000_000 > int.MaxValue이므로
    여기서 오버플로우가 발생해 실패한거 같다
*/

namespace BaekJoon._28
{
    internal class _28_05
    {

        static void Main5(string[] args)
        {

            // 문제 입력
            long size = int.Parse(Console.ReadLine());
            long idx = long.Parse(Console.ReadLine());

            long start = 1;
            // 10^ 10 
            long end = size * size;
            if (end > 1_000_000_000) end = 1_000_000_000;

            /*
            while (start < end)
            {

                // 여기서는 가장 작은값을 찾는다!
                long mid = (start + end) / 2;
                long cnt = 0;

                // mid보다 작은 인덱스 갯수를 찾는다
                for (int i = 1; i <= size; i++)
                {

                    long chk = mid / i;
                    // mid 보다 작은 값들의 개수를 찾는다
                    if (chk > size) chk = size;
                    cnt += chk;
                }

                // 찾은 원소의 개수가 목표치 보다 낮은 경우
                if (cnt < idx) start = mid + 1;
                // 조건보다 크거나 같은 경우 최소 수를 찾기 위해 해당 값을 포함하며 끝자리를 줄인다
                else end = mid;
            }

            Console.WriteLine(end);
            */

            long result = 0;
            while (start <= end)
            {

                long mid = (start + end) / 2;

                long cnt = 0;

                for (int i = 1; i <= size; i++)
                {

                    long chk = mid / i;
                    if (chk > size) chk = size;
                    cnt += chk;
                }

                if (cnt < idx) start = mid + 1;
                else
                {

                    end = mid - 1;
                    result = mid;
                }
            }

            Console.WriteLine(result);
        }
    }
}
