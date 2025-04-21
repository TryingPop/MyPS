using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 21
이름 : 배성훈
내용 : 수열과 헌팅
    문제번호 : 20495번

    정렬, 이분탐색 문제다.
    아이디어는 다음과 같다.
    해당 수가 가장 앞서는 경우는 다른 수들이 최댓값을 갖고
    자신이 최솟값을 가질 때 가장 앞서게 된다.
    같은 경우 앞서게 하면 된다.

    반면 가장 뒤에 있는 경우는 다른 수들이 최솟값을 갖고
    자신이 최댓값을 가질 때 가장 뒤에 있게 된다.
    같은 경우 뒤에가게 하면 된다.

    이렇게 구현하니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1563
    {

        static void Main1563(string[] args)
        {

            int[] min, max, sortedMin, sortedMax;
            int n;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    int l = BinarySearch(sortedMax, min[i], true) + 1;
                    int r = BinarySearch(sortedMin, max[i], false) + 1;

                    sw.Write($"{l} {r}\n");
                }

                int BinarySearch(int[] _arr, int _val, bool _isLower)
                {

                    int l = 0;
                    int r = n - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (_arr[mid] < _val) l = mid + 1;
                        else if (_arr[mid] > _val) r = mid - 1;
                        else if (_isLower) r = mid - 1;
                        else l = mid + 1;
                    }

                    return _isLower ? r + 1 : l - 1;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                min = new int[n];
                max = new int[n];
                sortedMin = new int[n];
                sortedMax = new int[n];

                for (int i = 0; i < n; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();

                    int m = a - b;
                    int M = a + b;
                    min[i] = m;
                    max[i] = M;

                    sortedMin[i] = m;
                    sortedMax[i] = M;
                }

                Array.Sort(sortedMin);
                Array.Sort(sortedMax);

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

                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;

                        return false;
                    }
                }
            }
        }
    }
}
