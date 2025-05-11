using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 11
이름 : 배성훈
내용 : 띠 정렬하기
    문제번호 : 30644번

    정렬, 좌표 압축 문제다.
    아이디어는 다음과 같다.
    구간 내부에 순서를 뒤집을 수 있어 내림차순인 경우면 자를 필요가 없다.
    잘라야 하는 경우는 인접한 항이 2이상 차이날 때 이다.

    그래서 정렬한 뒤 기존 인덱스의 차이가 2 이상인 경우 자르는 횟수를 추가해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1623
    {

        static void Main1623(string[] args)
        {

            int n;
            (int val, int idx)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.val.CompareTo(y.val));

                int ret = 0;
                for (int i = 1; i < n; i++)
                {

                    int diff = Math.Abs(arr[i].idx - arr[i - 1].idx);
                    if (diff == 1) continue;
                    ret++;
                }

                Console.Write(ret);
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

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
