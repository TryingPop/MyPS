using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 27
이름 : 배성훈
내용 :  2^K-Flip
    문제번호 : 34148번

    수학, 애드 혹, 누적 합 문제다.
    아이디어는 다음과 같다.
    q를 쿼리의 개수이다.
    i번 자리를 포함하는 쿼리가 k개 존재한다면 1이 나올 수 있는 개수는 다음과 같다.
    k = 0인경우 i번째 자리 값이 1인 경우만 2^q개 존재한다.
    i번째 자리값이 0인경우 모두 0일 수 밖에없고 0개다.

    k > 0인 경우
    i번째 자리가 1인 경우 kC0 + kC2 + ... + kC2j이고 여기서 2j = k이거나 2j + 1 = k인 경우다.
    그리고 해당 값은 kC0 + kC2 + ... + kC2j = 2^q - 1이 성립한다.
    반면 i번째 자리가 0인 경우 kC1 + kC3 + ... kC2j-1 이고 여기서 2j - 1= k 이거나 2j = k인 경우다.
    마찬가지로 해당 가능한 경우는 kC1 + ... + kC2j = 2^q - 1이 성립한다.

    그래서 포함된 개수를 찾아 주어 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1918
    {

        static void Main1918(string[] args)
        {

            int MOD = 998_244_353;
            int n, q;
            int[] cnt, arr;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = 1; i <= n; i++)
                {

                    cnt[i] = cnt[i - 1] + cnt[i];
                }

                int mul1 = 1;
                for (int i = 1; i < q; i++)
                {

                    mul1 = (mul1 * 2) % MOD;
                }

                int mul2 = (mul1 * 2) % MOD;

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (cnt[i] == 0)
                    {

                        if (arr[i] == 1) ret = (ret + mul2) % MOD;
                    }
                    else
                        ret = (ret + mul1) % MOD;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                q = ReadInt();

                arr = new int[n + 1];
                cnt = new int[n + 2];

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                for (int i = 0; i < q; i++)
                {

                    int s = ReadInt();
                    int e = ReadInt();

                    cnt[s]++;
                    cnt[e + 1]--;
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
