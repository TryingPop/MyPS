using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 인간 탑 쌓기
    문제번호 : 2123번

    차가 최소가 되게 쌓아야한다
    문제 조건을 보면
        상수 C - (나의 무게 + 힘)
    이 위험도이므로

    나의 무게 + 힘으로 정렬해야한다
    그리고 위에서부터 작은걸 쌓으면 된다
    만약 A의 힘 + A의 무게 == B의 힘 + B의 무게인 경우
    어느것을 먼저 놔도 상관없다
        W P
        3 5
        7 1
    놓고 해보면 된다
    그리고 해당 방법으로 4개짜리의 예제를 모든 경우의 수를 따져 진행해본 결과 최적해가 나와서
    제출하니 통과했다

    찾아보니 Exchange Argument ? 가 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0124
    {

        static void Main124(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            Human[] humans = new Human[len];

            for (int i = 0; i < len; i++)
            {

                int weight = ReadInt(sr);
                int pow = ReadInt(sr);
                humans[i].Set(pow, weight);
            }

            sr.Close();

            Array.Sort(humans);

            int addedWeight = 0;
            int max = -1_000_000_000;

            for (int i = 0; i < len; i++)
            {

                int cur = addedWeight - humans[i].pow;
                if (cur > max) max = cur;

                addedWeight += humans[i].weight;
            }

            Console.WriteLine(max);
        }

        struct Human : IComparable<Human>
        {

            public int pow;
            public int weight;

            public void Set(int _pow, int _weight)
            {

                pow = _pow;
                weight = _weight;
            }

            public int CompareTo(Human other)
            {

                int ret = (weight + pow).CompareTo(other.weight + other.pow);
                return ret;
            }
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
}
