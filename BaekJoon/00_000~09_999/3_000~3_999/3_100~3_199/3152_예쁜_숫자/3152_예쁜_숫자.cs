using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 18
이름 : 배성훈
내용 : 예쁜 숫자
    문제번호 : 3152번

    수학, 에드혹 문제다.
    만들어지는 수의 형태는 적당한 정수 a_i가 존재해 ∑p^a_i 형태이다.
    만들어진 두 개의 합은 다음과 같은 형태로 표현가능하다.

    ∑(b_j * p^(a_j))
    여기서 0 ≤ b_j ≤ 2이다.
    이외의 경우는 두 개로 만들 수 없다.
    그래서 먼저 모든 b_j가 2이하인지 확인한다.

    이후 이제 만드는 유일한 경우를 살펴보면 다음과 같이 3가지로 분류할 수 있다.
    b_j = 1인 j가 2개이고 b_j = 2인 경우가 없는 경우다.
    이는 p^(a_n) + p^(a_m)의 형태꼴이다.
    그래서 가능한 경우는 p^(a_n)과 p^(a_m)뿐으로 못나눈다.
    이는 서로 다른 두 노드이므로 유일하다.
    
    다음으로 b_j = 1인 노드가 1개이고 b_j = 2인 노드가 적어도 1개 존재하는 경우다.
    그러면 p^(a_n) + 2 ∑ p^(c_i) 형태이다. 여기서 c_i != a_n이다.
    그러면 p^(a_n) + ∑ p^(c_i)인 노드와 ∑ p^(c_i)인 노드로 밖에 못나눈다.
    그리고 이는 서로 다른 두 노드이고 유일하다.

    이제 이외 경우는 불가능함을 보이면 된다.
    먼저 b_j = 1인 노드가 존재하지 않는 경우를 보자.
    그러면 2 ∑ p^(a_n) 형태인데 이는 같은 노드를 선택해야만한다.
    그래서 서로 다른 노드로는 불가능하다.

    이제 b_j = 1인게 적어도 3개 이상 존재하는 경우다.
    그러면 p^(a_n1) + p^(a_n2) + p^(a_n3) + 2 ∑ p^(c_i)이다.
    p^(a_n1) + ∑ p^(c_i)을 선택하고 p^(a_n2) + p^(a_n3) + ∑ p^(c_i) 방법이 있다.
    반면 p^(a_n1) + p^(a_n2) + ∑ p^(c_i)와 p^(a_n3) + ∑ p^(c_i)를 선택하는 방법이 있다.
    이는 서로 다른 두 가지 노드로 표현하는 방법이 2개 이상 존재함을 뜻한다.
    이렇게 불가능하다.

    이제 b_j = 1인게 1개이고, b_j = 2인게 0개인 경우를 본다.
    그러면 p^(a_n1) 의 형태이고 이는 2개로 못 구분한다.

    마지막으로 b_j = 1인게 2개이고 b_j = 2인게 1개 이상인 경우를 본다.
    p^(a_n1) + p^(a_n2) + 2 * ∑ p^(c_i)이다.
    그러면 p^(a_n1) + ∑ p^(c_i)와 p^(a_n2) + ∑ p^(c_i)방법이 있다.
    또한 p^(a_n1) + p^(a_n2) + ∑ p^(c_i)와 ∑ p^(c_i)의 방법이 있다.
    이는 유일하지 않음을 보였다.

    이렇게 가지치기로 되는 경우를 찾고 코드로 작성했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1710
    {

        static void Main1710(string[] args)
        {

            // 3152
            long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            int[] cnt = new int[3];
            for (int i = 1; i < input.Length; i++)
            {

                if (Chk(input[i], input[0])) Console.Write("1 ");
                else Console.Write("0 ");
            }

            bool Chk(long _n, long _p)
            {

                Array.Fill(cnt, 0);
                while (_n > 0)
                {

                    long r = _n % _p;
                    if (r > 2) return false;
                    _n /= _p;
                    cnt[r]++;
                }

                if ((cnt[1] == 1 && cnt[2] > 0)
                    || (cnt[1] == 2 && cnt[2] == 0)) return true;
                return false;
            }
        }
    }
}
