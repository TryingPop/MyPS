using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 16
이름 : 배성훈
내용 : 색종이와 가위
    문제번호 : 20444번

    수학, 이분 탐색 문제다.
    어떻게 접근해야할지 몰라서 힌트를 봤다.

    이분 탐색을 확인했고 풀이 방법이 떠올랐다.
    먼저 산술 - 기하평균으로 가로 자르는 횟수가 절반에 가까울 수록
    잘랐을 때 색종이의 갯수가 늘어난다.

    그래서 자르는 횟수가 절반 이하에 대해
    가로 자르는 횟수와 나오는 색종이의 갯수는 증가 함수형태가 된다.
    여기서 이분 탐색을 적용해 해당 값이 나오는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1549
    {

        static void Main1549(string[] args)
        {

            long n, k;

            Input();

            GetRet();

            void GetRet()
            {

                Console.Write(BinarySearch() ? "YES" : "NO");

                bool BinarySearch()
                {

                    // 산술 기하 평균에 따라 절반 값이 곱하는게 최대임이 보장된다.
                    // 그리고 절반 이하일 때 가로 자르는 횟수가 증가하면 나뉘는 사각형의 갯수도 증가
                    long l = 0;
                    long r = n >> 1;

                    while (l <= r)
                    {

                        long mid = (l + r) >> 1;

                        long chk = (mid + 1) * (n - mid + 1);
                        if (chk < k) l = mid + 1;
                        else if (chk > k) r = mid - 1;
                        // 여기서는 마지막에 같은지 체크할 필요 없어서 그냥 따로 구분
                        else return true;
                    }

                    return false;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = long.Parse(temp[0]);
                k = long.Parse(temp[1]);
            }
        }
    }
}
