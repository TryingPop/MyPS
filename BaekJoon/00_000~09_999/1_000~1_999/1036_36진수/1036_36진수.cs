using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 9
이름 : 배성훈
내용 : 36진수
    문제번호 : 1036번

    그리디, 수학, 큰 수 연산 문제다.
    정렬 부분을 잘못해 계속해서 틀렸다.
    처음에는 1자리만 비교하면 되겠지? 하고 했는데,
    다음자리가 앞의 자리 연산에 영향을 줄 수 있다.

    입력
        11
        MMMM
        MMMM
        MMMM
        MMMM
        MMMM
        MMMM
        MMMM
        MMMM
        MMMM
        MMMM
        VMMMM
        1

    정답
        15ZZZP

    다음과 같은 반례를 보면 된다.
    그래서 결국 큰 수 연산을 해주는 BigInteger 구조체를 썼다.
*/

namespace BaekJoon.etc
{
    internal class etc_1389
    {

        static void Main1389(string[] args)
        {

            int MAX_DIGIT = 55;
            int MAX_NUMBER = 36;
            int Z = MAX_NUMBER - 1;

            int n, m;
            int[][] cnt;
            int[] num, maxDigit, maxCnt;
            
            Input();

            GetRet();

            Output();

            void Output()
            {

                int s = 0;
                for (int i = MAX_DIGIT - 1; i >= 0; i--)
                {

                    if (num[i] == 0) continue;
                    s = i;
                    break;
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = s; i >= 0; i--)
                {

                    sw.Write(NumToChar(num[i]));
                }
            }

            void GetRet()
            {

                ChangeZ();

                SetNum();
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                cnt = new int[MAX_DIGIT][];
                for (int i = 0; i < MAX_DIGIT; i++)
                {

                    cnt[i] = new int[MAX_NUMBER];
                }

                for (int i = 0; i < n; i++)
                {

                    string input = sr.ReadLine().Trim();

                    int digit = 0;
                    for (int j = input.Length - 1; j >= 0; j--)
                    {

                        int idx = CharToNum(input[j]);
                        cnt[digit++][idx]++;
                    }
                }

                m = int.Parse(sr.ReadLine());

                FixCnt();
            }

            void FixCnt()
            {

                for (int j = 0; j < MAX_NUMBER; j++)
                {

                    for (int i = 0; i < MAX_DIGIT - 1; i++)
                    {

                        cnt[i + 1][j] += cnt[i][j] / MAX_NUMBER;
                        cnt[i][j] %= MAX_NUMBER;
                    }
                }
            }

            void ChangeZ()
            {

                int[] sort = new int[MAX_NUMBER];

                for (int j = 0; j < MAX_NUMBER; j++)
                {

                    sort[j] = j;
                }

                BigInteger[] changes = new BigInteger[MAX_NUMBER];
                for (int j = 0; j < MAX_NUMBER; j++)
                {

                    int change = Z - j;
                    for (int i = MAX_DIGIT - 1; i >= 0; i--)
                    {

                        changes[j] = changes[j] * MAX_NUMBER + change * cnt[i][j];
                    }
                }

                Array.Sort(sort, (x, y) => changes[y].CompareTo(changes[x]));

                if (m == MAX_NUMBER) m--;

                for (int idx = 0; idx < m; idx++)
                {

                    int change = sort[idx];
                    if (change == Z)
                    {

                        m++;
                        continue;
                    }

                    for (int i = MAX_DIGIT - 1; i >= 0; i--) 
                    {

                        if (cnt[i][change] == 0) continue;
                        cnt[i][Z] += cnt[i][change];
                        cnt[i][change] = 0;
                    }
                }

                FixCnt();
            }

            void SetNum()
            {

                num = new int[MAX_DIGIT];
                for (int i = 0; i < MAX_DIGIT; i++)
                {

                    for (int j = 1; j < MAX_NUMBER; j++)
                    {

                        num[i] += cnt[i][j] * j;
                    }
                }

                for (int i = 0; i < MAX_DIGIT - 1; i++)
                {

                    num[i + 1] += num[i] / MAX_NUMBER;
                    num[i] %= MAX_NUMBER;
                }
            }

            char NumToChar(int _num)
            {

                if (_num < 10) return (char)('0' + _num);
                else return (char)('A' + _num - 10);
            }

            int CharToNum(char _num)
            {

                if ('A' <= _num && _num <= 'Z') return _num - 'A' + 10;
                else return _num - '0';
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>

inline int chrToint(char c) { return c <= '9' ? c-48 : c-55; }

inline char intTochr(int n) { return n <= 9 ? n+48 : n+55; }

inline double myPow(int n, int r)

{

    double res = 1;

    

    while (r--) res *= n;

    return res;

}

int main()

{

    int n, k;

    int cnt[55][36]{};

    scanf("%d", &n);

    

    char str[51];

    while (n--)

    {

        scanf(" %s", str);

        

        int p = 0;

        for (int i = strlen(str)-1; i >= 0; --i)

            cnt[p++][chrToint(str[i])]++;

    }

    

    scanf("%d", &k);

    

    int alp[36]{};

    while (k--)

    {

        int idx = -1;

        double val, max = 0;

        

        for (int i = 0; i < 35; ++i)

        {

            if (alp[i]) continue;

            

            val = 0;

            for (int j = 0; j < 54; ++j)

                if (cnt[j][i])

                    val += cnt[j][i] * (35-i) * myPow(36, j);

            

            if (max < val)

            {

                idx = i;

                max = val;

            }

        }

        

        if (idx >= 0)

        {

            alp[idx] = 1;

            

            for (int i = 0; i < 55; ++i)

            {

                cnt[i][35] += cnt[i][idx];

                cnt[i][idx] = 0;

            }

        }

        else break;

    }

    

    int sum;

    char ans[55]{};

    for (int i = 0; i < 55; ++i)

    {

        sum = 0;

        for (int j = 0; j < 36; ++j) sum += j*cnt[i][j];

        

        ans[i+1] = (ans[i]+sum) / 36;

        ans[i] = intTochr((ans[i]+sum) % 36);

    }

    

    int flg = 0;

    for (int i = 54; i >= 0; --i)

    {

        if (ans[i] > '0' || i == 0) flg++;

        if (flg) putchar(ans[i]);

    }

    

    return 0;

}
#endif
}
