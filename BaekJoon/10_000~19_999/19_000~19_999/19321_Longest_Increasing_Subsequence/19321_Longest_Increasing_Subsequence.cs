using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 28
이름 : 배성훈
내용 : Longest Increasing Subsequence 
    문제번호 : 19321번

    정렬 문제다.
    가장긴 순 증가하는 부분수열을 만들어 풀었다.
    예를들어 i번째에서 길이 j인 수열이 된다면
    앞에서부터 1, 2, 3, ..., j가 되게 선택하면 된다.
    순 증가이므로 1, 2, 2, 3인 경우는 고려하지 않는다.

    1, 2, 2, 3인 경우를 만들지 않기 위해서는
    앞에 있는 같은 수에 따라 소수점 끝자리를 1씩 추가해 누적해준다.
    위의 1, 2, 2, 3인 경우 1, 2, 2.1, 3이된다.
    1,  2,  2,  2, 3, 2, 2, 3, 3, 2는
    1, 2, 2.1, 2.11, 3, 2.111, 2.1111, 3.1, 3.11, 2.11111로 생각한다.  
    그러면 i번째에서 끝나는 가장 긴 증가하는 부분수열이 j가되게 조절할 수 있다.
    이제 이를 정수로 만들어주면 된다.

    이는 길이에 따라 정렬하고, 길이가 같으면 인덱스가 앞서는것과 동치이다.
    그래서 해당 방법으로 정렬해 값을 새로 부여하고,
    다시 인덱스를 정렬해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1279
    {

        static void Main1279(string[] args)
        {

            int S, E;
            int n;
            (int val, int idx)[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr, (x, y) =>
                {

                    int ret = x.val.CompareTo(y.val);
                    if (ret == 0) ret = y.idx.CompareTo(x.idx);
                    return ret;
                });

                for (int i = 0; i < n; i++)
                {

                    arr[i].val = i + 1;
                }

                Array.Sort(arr, (x, y) => x.idx.CompareTo(y.idx));

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{arr[i].val} ");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new (int val, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), i);
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
}
