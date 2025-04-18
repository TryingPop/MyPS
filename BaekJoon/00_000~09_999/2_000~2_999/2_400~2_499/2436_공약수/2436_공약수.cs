using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 공약수
    문제번호 : 2436번

    먼저 같은 최대 공약수와 최소 공배수를 갖기에,
    최소 공배수에 최대 공약수를 나눴다

    그리고 해당 값에 대해 소인수 분해를 했다
    소인수들을 리스트에 저장했는데 서로소가 되게 저장했다.
    해당 수가 2, 3, 5로 나눠 떨어진다면 2^n, 3^m, 5^k형태로 나눠서 저장했다

    그리고 DFS 탐색으로 서로소들끼리 곱하며 합을 비교했고 최소값이 담기게 했다

    풀때는 DFS 탐색에서 2^n 복잡도라 이게 될까? 하는 쫄리는 마음으로 풀었다
    다 풀고 주석 처리하는데, 확인해보니 가능한 경우다!

    우선 값의 범위가 2 ~ 1억이므로
    최대 공약수와 최소 공배수에 대한 약수 중 서로 다른 소수의 개수는 많아야 8개이다!
    (2 * 3 * 5 * 7 * 11 * 13 * 17 * 19 * 23 = 223,092,870, 9개면 최소 공배수 값이 1억을 넘는다!)

    거기에 최대 공약수가 2 이상이므로 최소 공배수에 최대 공약수를 나눈 값을 고려하면 7개로 줄어든다
    그래서 이상없음을 알게되었다

    다른 사람의 풀이를 보니, 그런 걱정이 필요없는 풀이다
    다른 사람 풀이는 약수들을 찾는게 아닌, 유클리드 호제법으로 GCD를 판정하며 서로 소가 되게 풀었는데
    해당 방법이 수학적 지식만 있다면 쉽게 풀 수 있는 방법 같다

    처음에는 div == 1인 경우도 담길 수 있는데,
    이를 방지하는 코드를 넣었다 그리고 그에 맞게 2줄 정도 추가했다;
    long 의 div연산을 2번해서 그런가? 조금 더 느려졌다 -> 이를 수정하니 다시 원래 속도 나왔다;
    
    68ms로 이상없이 통과했다
    브루트포스 문제임을 알면 쉽게 접근가능한데, 모를 때는 어렵게 느껴진다

    해당 문제 시간 단축하는 법이 있었다;

    앞에서 사용한 냅색문제 아이디어를 비슷하게 하면 된다;
    이를 써서 해결하는 문제가 있다
    etc_0100번!
*/

namespace BaekJoon.etc
{
    internal class etc_0043
    {

        static void Main43(string[] args)
        {

            long[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            // 일단 두 수의 최대 공약수와 최소 공배수를 알고 있으므로
            // 둘이 나눈다
            // 여기의 소인수들로 구성하면 된다
            long div = nums[1] / nums[0];
            long temp = div;

            // 서로소인 소인수들 집합
            // 소수의 힘을 빌린다
            // div % 30 == 0 (30 = 2 * 3 * 5)인 경우
            // 2^?, 3^?, 5^? 를 elements에 담는다
            List<long> elements = new List<long>();

            for (long i = 2; i < div; i++)
            {

                // 에라토스테네스의 체 이론
                if (i * i > div) break;

                if (div % i == 0)
                {

                    long cur = 1;
                    while (true)
                    {

                        cur *= i;
                        div /= i;
                        if (div % i != 0) break;
                    }

                    elements.Add(cur);
                }
            }

            // 1일 수도 있으니 선별해서 담는다
            // 안넣어도 해당 수 범위에서는 DFS가 200~ 500번 정도? 늘어나는 정도다;
            if (div != 1) elements.Add(div);
            // 결과 보관용, 최소 합, 두 수를 담는다
            long[] ret = new long[3];

            ret[0] = 1;
            ret[1] = temp;
            // 선택 유무
            bool[] select = new bool[elements.Count];
            // 값의 범위가 1억을 못 넘는다!
            ret[0] = 200_000_000;

            // DFS 가 끝나면 select에는 최소합이 담긴다
            // 0번이 아닌 1번은 겹치는 탐색을 안하기 위해서다
            // a * b = b * a 인 경우 방지!
            DFS(elements, ret, 1, select);

            // 출력
            // 우리가 찾은건 gcd로 나눠진 값이다
            ret[1] *= nums[0];
            ret[2] *= nums[0];

            // 작은거부터 출력
            if (ret[1] < ret[2]) Console.WriteLine($"{ret[1]} {ret[2]}");
            else Console.WriteLine($"{ret[2]} {ret[1]}");
        }
        
        static void DFS(List<long> _ele, long[] _ret, int _depth, bool[] _select)
        {

            if (_depth >= _select.Length)
            {

                long _a = 1;
                long _b = 1;

                for (int i = 0; i < _select.Length; i++)
                {

                    if (_select[i]) _a *= _ele[i];
                    else _b *= _ele[i];
                }

                if (_ret[0] > _a + _b)
                {

                    _ret[0] = _a + _b;
                    _ret[1] = _a;
                    _ret[2] = _b;
                }
                return;
            }

            DFS(_ele, _ret, _depth + 1, _select);
            _select[_depth] = true;
            DFS(_ele, _ret, _depth + 1, _select);
            _select[_depth] = false;
        }
    }

#if other
using static System.Console;

class Program
{
    static long GCD(long a, long b){
        return b>0? GCD(b, a%b) : a;
    }
    static void Main() {
        string[] s=ReadLine().Split(' ');
        long g=long.Parse(s[0]), l=long.Parse(s[1]);
        long m=l/g, v1=0, v2=0;
        
        for(long i=1; i*i<=m; i++){
            if(m%i==0){
                if(GCD(i, m/i)==1){
                    v1=g*i;
                    v2=g*(m/i);
                }
            }
        }
        Write("{0} {1}", v1, v2);
    }
}
#endif
}
