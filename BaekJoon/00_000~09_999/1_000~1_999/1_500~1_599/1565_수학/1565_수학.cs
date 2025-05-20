using System;
using System.IO;

/*
날짜 : 2025. 5. 15
이름 : 배성훈
내용 : 수학
    문제번호 : 1565번

    유클리드 호제법 문제다.
    M에 있는 모든 수의 약수여야 하므로 우리가 찾는 수는 M의 gcd를 나눌 수 있어야 한다.
    그리고 D에 있는 모든 수의 배수이므로 우리가 찾는 수는 D의 lcm의 배수여야한다.

    lcm은 long 범위를 초과할 수 있다.

    먼저 M의 값이 10억이하이므로 M의 GCD를 찾는다.
    이후 찾은 GCD로 D의 모든 수를 나누는지 확인한다.
    여기서 못나누면 존재하지 않는 경우다.
    존재하는 경우 D의 lcm은 GCD를 나누는게 보장되므로 
    int 범위 안이므로 LCM을 찾는다.
    연산 중간에 long 범위를 갈 수 있으므로 long으로 연산을 진행했다.

    이제 LCM의 배수이면서 GCD의 약수인 수를 찾으면 된다.
    이는 소인수분해를 해서 약수의 갯수를 세어주는 방식으로 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1629
    {

        static void Main1629(string[] args)
        {

            int n;
            int gcd;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = 0; i < n; i++)
                {

                    // d의 모든 원소를 나누는지 확인
                    if (gcd % arr[i] != 0)
                    {

                        // 못나누면 존재 X인 경우이다.
                        Console.Write(0);
                        return;
                    }
                }

                // lcm이 10^9이하임이 보장
                // lcm 찾기
                int lcm = 1;

                for (int i = 0; i < n; i++)
                {

                    int temp = GetGCD(arr[i], lcm);
                    lcm = lcm * (arr[i] / temp);
                }

                gcd /= lcm;
                int ret = 1;
                for (int i = 2; i * i <= gcd; i++)
                {

                    
                    if (gcd % i != 0) continue;
                    int cnt = 2;
                    gcd /= i;

                    while (gcd % i == 0)
                    {

                        gcd /= i;
                        cnt++;
                    }

                    ret *= cnt;
                }

                if (gcd != 1) ret *= 2;

                Console.Write(ret);
            }

            int GetGCD(int _f, int _t)
            {

                while (_t > 0)
                {

                    int temp = _f % _t;
                    _f = _t;
                    _t = temp;
                }

                return _f;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                int m = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                gcd = ReadInt();

                for (int i = 1; i < m; i++)
                {

                    gcd = GetGCD(gcd, ReadInt());
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
