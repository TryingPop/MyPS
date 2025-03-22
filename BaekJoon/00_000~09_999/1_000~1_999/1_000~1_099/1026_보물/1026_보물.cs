using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 보물
    문제번호 : 1026

    간단한 정렬? 문제다
    A의 수는 재배열되고, B는 안된다는데
    이는 A를 찾을 때 필요한 연산이다

    여기서는 결과값만 최소가 되게 하면 되기에
    실수의 교환법칙으로 B를 재배치해도 이상없다

    만약 A를 찾으라고 하면 B역시 재배치 하고 
    B의 기존 인덱스를 찾을 수 있게만 세팅하면 딱시 상관없다

    주어지는 값이 100이하의 음이아닌 정수이기에
    작은건 작은거끼리 곱하고 큰건 큰거끼리 곱하는게 큰 값이나온다
    반대로 작은것과 큰것을 곱하면서 더하면 작은 값이 나오게된다

    그리디한 방법으로 제출하니 이상없이 풀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0159
    {

        static void Main159(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int[] numsA = new int[len];
            int[] numsB = new int[len];

            for (int i = 0; i < len; i++)
            {

                numsA[i] = ReadInt(sr);
            }

            for (int i = 0; i < len; i++)
            {

                numsB[i] = ReadInt(sr);
            }

            sr.Close();

            Array.Sort(numsA);
            Array.Sort(numsB, (x, y) => y.CompareTo(x));

            int ret = 0;
            for (int i = 0; i < len; i++)
            {

                ret += numsA[i] * numsB[i];
            }

            Console.WriteLine(ret);
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
