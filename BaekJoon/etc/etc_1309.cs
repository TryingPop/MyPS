using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 2
이름 : 배성훈
내용 : 팔
    문제번호 : 1105번

    수학, 그리디 문제다.
    l, r 사이에 있는 수들 중 8의 갯수가 최소인 경우를 찾아야 한다.
    우선 8과 0사이에 9가 있다.

    먼저 l과 r의 자리가 다른 경우
    1_000..0으로 8이 0개가 될 수 있다.

    반면 자릿수가 같은 경우 이제 큰 수부터 확인한다.
    만약 값이 다른 경우 뒤의 자리를 모두 0으로 만들면 뒤에는 8이 0개가 될 수 있다.
    예를들어 100 과 200인 경우
    1을 2로 올리고, 200으로 만드는 것이다.

    만약 다른 경우 뒤의 숫자가 8이라면 바꾸고 1을 빼준다.
    즉, 100과 800인 경우 800으로 만들고, -1을 해서 799로 한다.
    1을 빼줘도 범위 안임은 1과 8이 다르다는 전제 조건에서 자명하다.
    이제 같은 경우만 보면 된다.

    같은 경우는 해당 수를 꼭 사용해야하므로 다음 자릿수로 간다.
    만약 같은 자리의 숫자가 8인 경우 8이 꼭 나와야한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1309
    {

        static void Main1309(string[] args)
        {

            string l, r;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                if (l.Length != r.Length)
                {

                    Console.Write(0);
                    return;
                }

                int ret = 0;
                for (int i = 0; i < l.Length; i++)
                {

                    if (l[i] != r[i]) break;
                    else if (l[i] == '8') ret++;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                l = temp[0];
                r = temp[1];
            }
        }
    }
}
