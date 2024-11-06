using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 회의실 배정
    문제번호 : 30827번

    그리디, 정렬 문제다
    break를 안넣어, 2번 틀렸다;

    아이디어는 다음과 같다
    우선 회의 끝나는 시간에 따라 정렬한다
    그리고 회의실 배정이 가능한 경우 회의실 배정하는게 잘 알려진 풀이방법이다

    여기서는 여러 회의실이 존재하므로 여기서 한 번 더 그리디를 적용해야한다
    그것은 가장 늦게 끝나는 회의실에 다음 회의를 먼저 배정하면 된다
    앞과 같은 로직이다
        (여기서 배정하고 break을 안해서, 자꾸 n번씩 배정해 틀렸다;)

    매번 가장 늦게 끝나는 회의를 찾아야하기에 회의실 시간을 계속해서 정렬했다
    이렇게 제출하니 144ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0584
    {

        static void Main584(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);

            (int s, int e)[] arr;
            int[] use;

            int lenArr;
            int lenUse;

            Solve();
            sr.Close();

            void Solve()
            {

                Input();

                Array.Sort(arr, (x, y) => x.e.CompareTo(y.e));

                int ret = 0;

                for (int i = 0; i < lenArr; i++)
                {

                    for (int j = lenUse - 1; j >= 0; j--)
                    {

                        if (use[j] < arr[i].s)
                        {

                            ret++;
                            use[j] = arr[i].e;

                            Array.Sort(use);
                            break;
                        }
                    }
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                lenArr = ReadInt();
                lenUse = ReadInt();

                arr = new (int s, int e)[lenArr];
                use = new int[lenUse];

                for (int i = 0; i < lenArr; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
                }
            }

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
}
