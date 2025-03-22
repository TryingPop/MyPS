using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 물건 팔기
    문제번호 : 1487번

    브루트포스 문제다.
    문제가 약간 불친절하다.
    배송비는 판매자 부담이며, 
    판매자에게 판매를 거부할 수도 있다.

    아이디어는 다음과 같다.
    팔 수 있는 인원이 같다면 최대한 비싸게 파는 것이 
    최대 이익을 가져다 줌을 그리디로 알 수 있다.
    구매인원이 바뀌는 최댓값인 구매자의 가격만 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1450
    {

        static void Main1450(string[] args)
        {

            int n;
            (int b, int p)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                int max = 0;

                for (int i = 0; i < n; i++)
                {

                    int chk = ChkPrice(arr[i].b);
                    if (chk < max) continue;
                    else if (max < chk)
                    {

                        max = chk;
                        ret = arr[i].b;
                    }
                    else
                        ret = Math.Min(ret, arr[i].b);
                }

                Console.Write(ret);

                int ChkPrice(int _p)
                {

                    int ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        if (arr[i].b < _p) continue;
                        int chk = _p - arr[i].p;
                        if (chk <= 0) continue;
                        ret += chk;
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new (int b, int p)[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
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
