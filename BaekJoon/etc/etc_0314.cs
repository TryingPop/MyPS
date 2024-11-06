using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 수학 선생님의 고민(Hard)
    문제번호 : 28242번

    수학, 브루트포스 알고리즘 문제다
    로직 잘못으로 2번,
    약수의 개수 설정을 잘못해서 인덱스 에러 1번 나왔고,
    정렬을 안해줘서 2번 틀렸다

    주된 아이디어는 다음과 같다
    n의 약수를 모두 찾아 a, c의 후보를 모두 찾고
    그리고 n + 2의 약수를 모두 찾아 b, d의 후보를 모두 찾는다
    에라토스테네스 체 이론을 써서 찾았다

    그리고 정렬한 뒤 두 포인터 알고리즘으로 a - c, b - d 짝을 이룬다
    마지막으로 ad - bc 혹은, bc - ad가(b 또는 d가 음수여야한다) n + 1 이 되는지 확인한다
    그래서 찾으면 정답, 없으면 -1을 출력했다

    찾는 방법으로는 조합으로 찾아 중복을 없애고 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0314
    {

        static void Main314(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[] left = new int[500];
            int[] right = new int[500];

            FillDivisor(left, n, out int lLen);
            FillDivisor(right, n + 2, out int rLen);

            Array.Sort(left, 0, lLen);
            Array.Sort(right, 0, rLen);
            int[] ret = new int[4];
            int lIdx = 0;
            int rIdx = lLen - 1;
            while(lIdx <= rIdx)
            {

                bool find = false;
                for (int i = 0; i < rLen; i++)
                {

                    if (ChkMid(left[lIdx], -right[i], left[rIdx], right[rLen - 1 - i], n + 1))
                    {


                        ret[0] = left[lIdx];
                        ret[1] = -right[i];

                        ret[2] = left[rIdx];
                        ret[3] = right[rLen - 1 - i];

                        find = true;
                        break;
                    }

                    if (ChkMid(left[lIdx], right[i], left[rIdx], -right[rLen - 1 - i], n + 1))
                    {


                        ret[0] = left[lIdx];
                        ret[1] = right[i];

                        ret[2] = left[rIdx];
                        ret[3] = -right[rLen - 1 - i];

                        find = true;
                        break;
                    }
                }

                if (find) break;

                lIdx++;
                rIdx--;
            }

            if (ret[0] == 0) Console.WriteLine(-1);
            else Console.WriteLine($"{ret[0]} {ret[1]} {ret[2]} {ret[3]}");
        }

        static bool ChkMid(int _a, int _b, int _c, int _d, int _chk)
        {

            int calc1 = _a * _d;
            int calc2 = _c * _b;

            int calc = calc1 + calc2;

            if (calc == _chk) return true;
            return false;
        }

        static void FillDivisor(int[] _arr, int _n, out int _len)
        {

            _len = 0;
            for (int i = 1; i <= _n; i++)
            {

                if (i * i > _n) break;
                if (_n % i > 0) continue;

                _arr[_len++] = i;
                int other = _n / i;
                if (other != i) _arr[_len++] = other;
            }
        }
        
    }
}
