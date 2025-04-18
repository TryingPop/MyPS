using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 14
이름 : 배성훈
내용 : 휴게소 세우기
    문제번호 : 1477번

    이분 탐색 문제다.
    끝지점을 체크안해서 한 번 틀렸다.
    아이디어는 다음과 같다.

    휴대소간 최대 거리를 dis라 할 때 dis에서 m개 이하로 설치 가능하다면
    dis + 1에서는 당연히 m개 이하로 설치 가능하다.

    그래서 각 지점에서 거리 dis 초과로 거리 차이나면 휴게소를 세우며 휴대소간 간격이 dis 이하가 되게 한다.
    그래서 세운 휴게소의 갯수가 m개 이하인지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1544
    {

        static void Main1544(string[] args)
        {

            int n, m, l;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr);

                int left = 1;
                int right = l;

                while (left <= right)
                {

                    int mid = (left + right) >> 1;
                    if (ChkInvalid(mid)) right = mid - 1;
                    else left = mid + 1;
                }

                Console.Write(right + 1);

                bool ChkInvalid(int _dis)
                {

                    int cnt = 0;
                    for (int i = 1; i < arr.Length; i++)
                    {

                        int prev = arr[i - 1];
                        if (prev + 1 == arr[i]) continue;

                        while (prev + _dis < arr[i])
                        {

                            cnt++;
                            prev += _dis;
                        }
                    }

                    return cnt <= m;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                l = ReadInt();

                arr = new int[n + 2];

                arr[n + 1] = l;
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
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

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
