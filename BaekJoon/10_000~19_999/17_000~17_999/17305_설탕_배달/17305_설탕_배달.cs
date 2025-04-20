using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 사탕 배달
    문제번호 : 17305번

    그리디 알고리즘, 정렬, 누적합 문제다
    문제를 잘못해석해서 엄청나게 틀렸다
    문제에서 요구하는 조건은 다음과 같다
    '당분'이 최대가 되게 담을 때, 당분이 얼마인지 구하는 문제다

    처음에는 최대한 사탕을 담아가려고한다 << 이 문장에 의해
    사탕의 개수가 최대가 되면서, 당분이 제일 높은건 어떤건지 찾고만 있었다;
    ... 그래서 틀릴 수 밖에 없었다
    예를들어
        5 10
        3 1
        3 2
        3 3
        5 1_000_000_000
        5 1_000_000_000
    
    해당 조건에 맞춰버리면 6이 최대다;
    반면 여기 문제에서 요구하는 값은 20억이다...

    당분이 최대가 되는 경우는 그냥 당분을 내림차순 정렬하고,
    누적합 배열을 만들면 된다
    그러면 해당 당분을 n 들고갈 때, 최대 당분을 얻을 수 있다
    해당 아이디어로 제출하면 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0284
    {

        static void Main284(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            long[] sum3 = new long[n + 1];
            long[] sum5 = new long[n + 1];

            sum3[0] = 1_000_000_001;
            sum5[0] = 1_000_000_001;
            int cn3 = 0;
            int cn5 = 0;
            for (int i = 1; i <= n; i++)
            {

                int type = ReadInt(sr);
                int sweet = ReadInt(sr);
                if (type == 3)
                {

                    sum3[i] = sweet;
                    cn3++;
                }
                else 
                { 
                    
                    sum5[i] = sweet;
                    cn5++;
                }
            }

            sr.Close();

            Array.Sort(sum3, (x, y) => y.CompareTo(x));
            Array.Sort(sum5, (x, y) => y.CompareTo(x));

            sum3[0] = 0;
            sum5[0] = 0;

            for (int i = 1; i <= cn3; i++)
            {

                sum3[i] += sum3[i - 1];
            }

            for (int i = 1; i <= cn5; i++)
            {

                sum5[i] += sum5[i - 1];
            }

            int candy3 = m <= 3 * cn3 ? m / 3 : cn3;
            long ret = 0;

            while (candy3 >= 0)
            {

                int calc = m - candy3 * 3;
                int candy5 = calc <= 5 * cn5 ? calc / 5 : cn5;
                long chk = sum3[candy3] + sum5[candy5];

                ret = ret < chk ? chk : ret;
                candy3--;
            }
            Console.WriteLine(ret);
        }

        static bool ChkMax(int _cn3, int _cn5, int _cm3, int _cm5, int _max)
        {

            // 개수부터 확인
            if (_cn3 < 0 || _cn5 < 0 || _cn3 > _cm3 || _cn5 > _cm5) return false;

            int calc = _cn3 * 3 + _cn5 * 5;
            return calc <= _max;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if Wrong

using System;
using System.IO;
using System.Collections.Generic;

StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

int n = ReadInt(sr);
int m = ReadInt(sr);

List<long> c3 = new List<long>(n);
List<long> c5 = new List<long>(n);

for (int i = 0; i < n; i++)
{

    int type = ReadInt(sr);
    int weight = ReadInt(sr);
    if (type == 3) c3.Add(weight);
    else c5.Add(weight);
}

sr.Close();

c3.Sort((x, y) => y.CompareTo(x));
c5.Sort((x, y) => y.CompareTo(x));

long ret = MaxWeight(c3, c5, m);
Console.WriteLine(ret);

static long MaxWeight(List<long> _c3, List<long> _c5, int _m)
{

    int num3 = _c3.Count;
    int num5 = _c5.Count;
    FindMaxCandy(_m, num3, num5, out int maxCandy, out int r);

    if (num5 == 0) return GetTotal(_c3, maxCandy);
    else if (num3 == 0) return GetTotal(_c5, maxCandy);
    else if (maxCandy == num3 + num5) return GetTotal(_c5, num5) + GetTotal(_c3, num3);
    else if (r < 2 || maxCandy == 0)
    {

        if (maxCandy <= num3) return GetTotal(_c3, maxCandy);
        return GetTotal(_c3, num3) + GetTotal(_c5, maxCandy - num3);
    }

    long ret = 0;
    int candy3 = num3 <= maxCandy ? num3 : maxCandy;
    int candy5 = num3 <= maxCandy ? maxCandy - num3 : 0;
    if (candy3 == 1 || num5 == 1 || r < 4)
    {

        ret = GetTotal(_c3, candy3 - 1);
        ret += GetTotal(_c5, candy5);

        if (_c3[candy3 - 1] >= _c5[candy5]) ret += _c3[candy3 - 1];
        else ret += _c3[candy5];

        return ret;
    }

    ret = GetTotal(_c3, candy3 - 2);
    ret += GetTotal(_c5, candy5);

    if (_c3[candy3 - 2] >= _c5[candy5])
    {

        ret += _c3[candy3 - 2];
        if (_c3[candy3 - 1] >= _c5[candy5]) ret += _c3[candy3 - 1];
        else ret += _c5[candy5];
    }
    else
    {

        ret += _c5[candy5];
        if (candy5 < num5 && _c3[candy3 - 2] < _c5[candy5 + 1]) ret += _c5[candy5 + 1];
        else ret += _c3[candy3 - 2];
    }

    return ret;
}

static long GetTotal(List<long> _list, int _end)
{

    long ret = 0;
    for (int i = 0; i < _end; i++)
    {

        ret += _list[i];
    }

    return ret;
}

static void FindMaxCandy(int _m, int _c3, int _c5, out int _r, out int _maxCandy)
{

    int calc = _c3 * 3;
    if (_m <= calc) 
    {

        _r = _m % 3;
        _maxCandy = _m / 3;
        return;
    }

    _m -= calc;
    calc = _c5 * 5;
    if (_m <= calc) 
    {

        _r = _m % 5;
        _maxCandy = calc + _m / 5;
        return;
    }

    _r = 0;
    _maxCandy = _c3 + _c5;
    return;
}

static int ReadInt(StreamReader _sr)
{

    int c, ret = 0;
    while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
    {

        if (c == '\r') continue;
        ret = ret * 10 + c - '0';
    }

    return ret;
}
#endif
}
