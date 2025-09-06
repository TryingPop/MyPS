using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 3
이름 : 배성훈
내용 : 퐁당퐁당 2
    문제번호 : 17938번

    수학, 구현 문제다.
    계산을 잘못해서 계속해서 틀렸다.

    먼저 손을 드는 것은 다음처럼 끊어서보자.

    1, 2, 3, ..., 2n - 1    -> 1씩 증가하는 구간
    2n, 2n-1, 2n-2, ..., 2  -> 1씩 감소하는 구간

    그러면 증가하는 구간 -> 감소하는 구간 -> 증가하는 구간 -> ... 순으로주기성이 보인다.
    이제 증가하는 구간 -> 감소하는 구간인 4n - 2번 손드는 경우를 1주기로 보면
    총 드는 손의 개수는 (1 + 2 + ... + 2n - 1) + (2n + 2n - 1 + ... + 3 + 2)
    = (2n - 1) x (2n + 1)임을 알 수 있다.
    

    그리고 시작 위치는 뒤로 1칸 이동함을 알 수 있다.
    예를 들어 초기에 1번의 왼손을 들었다면 1주기를 돌면 바로 직전인 n번의 오른손을 든다.
    반면 2번의 오른손으로 시작했다면 1주기를 돌면 2번의 왼손이 된다.

    t를 몇 주기 i를 돌았는지 확인한다.
    그리고나서 시작 위치는 1번 왼손에서 -i번째에 있는 손에서 시작한다.

    그리고 시뮬레이션 돌려 찾았다.
    시뮬레이션은 n이 1000이므로 충분히 유효한 방법이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1860
    {

        static void Main1860(string[] args)
        {

            // 17938 - 퐁당퐁당 2
            // 해결해야 한다.
            int n;
            int p, t;

            Input();

            GetRet();

            void GetRet()
            {

                int MOD = 2 * n;
                p--;
                t--;
                int init = (t / (4 * n - 2)) % MOD;

                t %= 4 * n - 2;
                t++;

                int s = (2 * n - init) % MOD;
                int e = (2 * n - init) % MOD;


                for (int i = 2; i <= Math.Min(t, 2 * n); i++)
                {

                    int nS = (e + 1) % MOD;
                    int nE = (e + i) % MOD;

                    s = nS;
                    e = nE;
                }

                for (int i = 2 * n + 1; i <= t; i++)
                {

                    int hand = 4 * n - i;

                    int nS = (e + 1) % MOD;
                    int nE = (e + hand) % MOD;

                    s = nS;
                    e = nE;
                }

                bool ret = false;
                if (s <= e)
                {

                    s >>= 1;
                    e >>= 1;
                    if (s <= p && p <= e) ret = true;
                }
                else
                {

                    s >>= 1;
                    e >>= 1;
                    if (s <= p || p <= e) ret = true;
                }

                Console.Write(ret ? "Dehet YeonJwaJe ^~^" : "Hing...NoJam");
            }

            void Input()
            {

                n = int.Parse(Console.ReadLine());
                string[] temp = Console.ReadLine().Split();

                p = int.Parse(temp[0]);
                t = int.Parse(temp[1]);
            }
        }
    }
}
