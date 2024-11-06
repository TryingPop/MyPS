using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 드래곤 앤 던전
    문제번호 : 16434번

    구현, 이분탐색 문제다
    예상치 못한 곳에서 오버플로우로 많이 틀렸다

    처음에 그리디로 접근하려고 했었다
    그런데 이분탐색으로도 가능해 보여서 이분탐색 연습하는 마음으로 그리디는 보류하고
    이분탐색으로 풀려고 시도했다

    123,456개를 입력받고, 한 번에 최대 10^12승 가까이 체력을 닳을 수 있다
    처음에는 잘못 봐서 한번에 10^13까지 닳을 수 잇는 줄 알았다
    그래서 10^19까지 계산해야하나? 싶어 ulong으로 자료형을 선정했고
    여기서 오버플로우 문제가 일어나서 이분 탐색이 틀리는 줄 알았다
    (실제론 10^12이고, 123_456개니, 약 1.2 * 10^18 정도다, 2 * 10^18을 끝값으로 하면 된다)

    그래서 이분탐색으로는 오버플로우? 문제 해결이 힘들어 보이고,
    먼저 문제부터 맞추고! 다음에 이분탐색으로 접근하자고 생각해서
    그리디로 접근했다 그래도 틀리니 이건 앞의 문제다 싶어 확인했고
    용사 공격력 쪽에서 생길 수 있는 문제임을 알았다 
    공격력 자료형을 long으로 바꾸니 해결됐다

    먼저 그리디 아이디어는 다음과 같다
    던전을 입력 받을 때, 해당 던전을 클리어하기 위한 필요 hp를 기록한다
    그리고 역으로 던전을 탐색하면서 필요한 hp를 누적해간다
    치료부분은 음수로 계산하고, hp는 0 밑으로 내려갈 수 없다
    이러한 연산에서 갖는 최대값이 던전을 클리어하기 위한 최소값과 동형이 된다
    해당 방법은 O(N)의 시간이 필요하다
    

    다음으로 이분탐색 문제다
    용사 hp를 이분 탐색으로 설정한다
    다음으로 용사가 던전 도는 경우를 시뮬레이션 한다
    만약 중간에 hp <= 0이되면 바로 탈출하고 용사 hp를 늘린다
    반면 용사 던전을 다 도는 동안 0 이하로 한 번도 떨어지지 않으면 용사 hp를 줄여본다
    이렇게 용사 hp를 찾는다
    시뮬레이션에서 나눗셈 연산이 많아 던전을 입력 받을 때 필요 hp를 연산해 필요 hp만 기록했다
    해당 방법은 O(N log M )시간이 예상된다 M은 2 * 10^18 혹은 15 * 10^17

    아래는 코드이다 두 방법 전부 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0414
    {

        static void Main414(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int atk = ReadInt();

            long[] arr = new long[n];
            for (int i = 0; i < n; i++)
            {

                int type = ReadInt();

                if (type == 1)
                {

                    // 필요 hp 계산해서 저장
                    long eAtk = ReadInt();
                    long eHp = ReadInt();

                    long turn = eHp / atk;
                    if (eHp % atk == 0) turn--;

                    arr[i] = eAtk * turn;
                }
                else
                {

                    // 회복하는 곳인 경우
                    atk += ReadInt();
                    arr[i] = -ReadInt();
                }
            }

            sr.Close();
#if Greedy
            long max = 0;
            long cur = 0;
            ulong ret = 0;

            for(int i = n - 1; i >= 0; i--)
            {

                cur += arr[i];
                if (cur < 0) cur = 0;
                else if (max < cur)
                {

                    max = cur;
                    ret = (ulong)max;
                }
            }

            Console.WriteLine(ret + 1);
#else

            long l = 1;
            long r = 2_000_000_000_000_000_000;
            long ret = 0;

            while (l <= r)
            {

                long mid = (l + r) / 2;
                long hp = mid;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] < 0)
                    {

                        hp += -arr[i];
                        if (hp > mid) hp = mid;
                    }
                    else hp -= arr[i];

                    if (hp <= 0) break;
                }

                if (hp <= 0) l = mid + 1;
                else r = mid - 1;
            }

            ret = r + 1;
            Console.WriteLine(ret);
#endif
            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
namespace BOJ
{
    static class Input<T>
    {
        static public T[] GetArray() => Array.ConvertAll(Console.ReadLine().Split(), ConvertTo);
        static T ConvertTo(string s) => (T)Convert.ChangeType(s, typeof(T));
    }
    
    class No_16434
    {
        static void Main()
        {
            var inputs = Input<int>.GetArray();
            // 방의 개수 N
            int N = inputs[0];
            // 초기 공격력 H
            long H = inputs[1];
            long curHp = 0, maxHp = 0;

            for (int i = 0; i < N; i++)
            {
                inputs = Input<int>.GetArray();
                int t = inputs[0];
                int a = inputs[1];
                int h = inputs[2];

                if (t == 1)
                {
                    curHp += a * ((h / H) - (h % H != 0 ? 0 : 1));
                    maxHp = Math.Max(maxHp, curHp);
                }
                else
                {
                    H += a;
                    curHp = Math.Max(curHp - h, 0);
                }
            }

            maxHp++;

            Console.WriteLine(maxHp);
        }
    }
}
#endif
}
