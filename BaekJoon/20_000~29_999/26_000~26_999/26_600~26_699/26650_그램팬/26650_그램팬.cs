using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 그램팬
    문제번호 : 26650번

    구현, 문자열 문제다.
    두 포인터 알고리즘을 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1593
    {

        static void Main1593(string[] args)
        {

            string str;
            int[] cnt;

            Input();

            GetRet();

            void GetRet()
            {

                int idx = 'A';
                cnt = new int[255];

                long ret = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == idx)
                    {

                        // 갯수 증가
                        cnt[idx]++;
                        if (idx == 'Z') ret += cnt['A'];
                    }
                    else if (str[i] == idx + 1 && cnt[idx] > 0)
                    {

                        // 다음 문자
                        idx++;
                        cnt[idx]++;
                        if (idx == 'Z') ret += cnt['A'];
                    }
                    else
                    {

                        // 비워야 하는경우
                        Empty();
                        if (str[i] == idx) cnt[idx]++; 
                    }
                }

                Console.Write(ret);

                void Empty()
                {

                    for (int i = 'A'; i <= idx; i++)
                    {

                        cnt[i] = 0;
                    }

                    idx = 'A';
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine();
            }
        }
    }

#if other
// #include <cstdio>

int main(void) {
    char S[100001]; scanf("%s", S);
    int idx = 0;
    long long R = 0, A = 0, Z = 0;
    char cmp = 'A' - 1;
    for (; S[idx]; idx++) {
        if (cmp == S[idx]) {
            if (S[idx] == 'A') A++;
            else if (S[idx] == 'Z') Z++;
        }
        else if (cmp + 1 == S[idx]) {
            if (S[idx] == 'A') A = 1;
            else if (S[idx] == 'Z') Z = 1;
            cmp++;
        }
        else {
            R += A * Z;
            A = 0; Z = 0; cmp = 'A' - 1;
            if (S[idx] == 'A') A = 1, cmp = 'A';
        }
    }
    R += Z * A;
    printf("%lld", R);
}
#endif
}
