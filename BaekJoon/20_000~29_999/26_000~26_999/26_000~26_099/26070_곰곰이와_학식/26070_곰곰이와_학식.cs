using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 곰곰이와 학식
    문제번호 :  26070번

    그리디, 구현, 시뮬레이션 문제다
    오버플로우를 생각 못해 6% 컷당했다

    처음에는 로직 문제도 있었다 -> 나머지 부분 처리!
        3 0 0
        0 6 1
    
        -> 1

    너무 복잡하게 코드를 작성했는가 하면서 구현 방법을 바꾸다가
    로직에는 이상없는거 같아 몇 가지 예제를 들어보았다

    그래서 예제 중 최대값 넣어볼까 시도하다가
    오버플로우가 문제임을 알아차렸다

    이후 오버플로우 부분을 수정하니, 이상없이 통과했다
    이전 코드들 역시 마찬가지다
*/

namespace BaekJoon.etc
{
    internal class etc_0281
    {

        static void Main281(string[] args)
        {

            int[] want = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);
            int[] have = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);

            long ret = 0;
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    int min = want[j] < have[j] ? want[j] : have[j];
                    want[j] -= min;
                    have[j] -= min;
                    ret += min;
                }

                int q1 = have[0] / 3;
                int q2 = have[1] / 3;
                int q0 = have[2] / 3;

                for (int j = 0; j < 3; j++)
                {

                    have[j] = have[j] % 3;
                }

                have[0] += q0;
                have[1] += q1;
                have[2] += q2;
            }


            Console.WriteLine(ret);
        }
    }

#if first
long[] want = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
long[] have = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

long ret = 0;
for (int i = 0; i < 3; i++)
{

    long min = want[i] < have[i] ? want[i] : have[i];
    ret += min;
    want[i] -= min;
    have[i] -= min;
}

for (int i = 0; i < 3; i++)
{

    ret += ChangeHave(have, want);
}
Console.WriteLine(ret);

static long ChangeHave(long[] _have, long[] _want)
{

    long ret = 0;
    if (_have[0] >= _have[1] && _have[0] >= _have[2])
    {

        long calc = _have[0] / 3;
        _have[1] += calc;
        _have[0] -= calc * 3;
        ret = _have[1] < _want[1] ? _have[1] : _want[1];
        _have[1] -= ret;
        _want[1] -= ret;
    }
    else if (_have[1] >= _have[2] && _have[1] >= _have[0])
    {

        long calc = _have[1] / 3;
        _have[2] += calc;
        _have[1] -= calc * 3;
        ret = _have[2] < _want[2] ? _have[2] : _want[2];
        _have[2] -= ret;
        _want[2] -= ret;
    }
    else
    {

        long calc = _have[2] / 3;
        _have[0] += calc;
        _have[2] -= calc * 3;
        ret = _have[0] < _want[0] ? _have[0] : _want[0];
        _have[0] -= ret;
        _want[0] -= ret;
    }

    return ret;
}
#elif second
int[] want = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
int[] have = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

long ret = 0;
for (int i = 0; i < 3; i++)
{

    int min = want[i] < have[i] ? want[i] : have[i];
    ret += min;
    want[i] -= min;
    have[i] -= min;
}

long max = 0; 
for (int i = 0; i < 9; i++)
{

    long calc = GetVal(want, have, i);
    if (calc > max) max = calc;
}

ret += max;
Console.WriteLine(ret);

static long GetVal(int[] _want, int[] _have, int _type)
{

    int[] want = new int[] { _want[0], _want[1], _want[2] };
    int[] have = new int[] { _have[0], _have[1], _have[2] };

    int first = _type % 3;
    long ret = 0;
    for (int i = 0; i < 3; i++)
    {

        int idx1 = (first + i) % 3;
        int idx2 = (first + i + 2) % 3;

        int calc = have[idx1] + have[idx2] / 3;
        int min = want[idx1] < calc ? want[idx1] : calc;

        ret += min;
        want[idx1] -= min;
        if (min <= have[idx1])
        {

            have[idx1] -= min;
        }
        else
        {

            min -= have[idx1];
            have[idx1] = 0;
            have[idx2] -= min * 3;
        }
    }

    int second = _type / 3;
    for (int i = 0; i < 3; i++)
    {

        int idx1 = (second + i) % 3;
        int idx2 = (second + i + 1) % 3;
        int calc = have[idx1] + have[idx2] / 9;
        int min = want[idx1] < calc ? want[idx1] : calc;

        ret += min;
        want[idx1] -= min;

        if (min <= have[idx1])
        {

            have[idx1] -= min;
        }
        else
        {

            min -= have[idx1];
            have[idx1] = 0;
            have[idx2] -= min * 9;
        }
    }

    return ret;
}
#endif
}
