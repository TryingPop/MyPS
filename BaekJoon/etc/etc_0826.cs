using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 18
이름 : 배성훈
내용 : 하노이 탑
    문제번호 : 1914번

    재귀, 큰 수 연산 문제다
    8년도에 중 3일 때 경우의 수 규칙은 찾았으나 지수식 표현을 못해 헤맨 기억이 있다
    (지수 표현은 당시 고 1에서 배웠다)

    그리고 22년도에 처음 알고리즘 문제 접했을 때 하노이탑 문제로 엄청나게 헤맨 기억이 있다
    그때의 경험 덕에 이번에는 쉽게 풀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0826
    {

        static void Main826(string[] args)
        {

            StreamWriter sw;

            BigInteger[] cnt;
            int n;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                int CHK = 20;
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{cnt[n]}\n");


                if (n <= CHK) DFS(1, 3, 2, n);
                sw.Close();
            }


            void Init()
            {

                n = int.Parse(Console.ReadLine());
                cnt = new BigInteger[n + 1];
                
                for (int i = 1; i <= n; i++)
                {

                    cnt[i] = cnt[i - 1] * 2 + 1;
                }
            }

            void DFS(int _from, int _to, int _mid, int _num)
            {

                if (_num == 0) return;

                // _num을 _from에서 _to로 옮길려면 위에 있는것을
                // _mid로 옮겨야 한다
                DFS(_from, _mid, _to, _num - 1);

                // 이제 _num을 _to로 옮긴다
                sw.Write($"{_from} {_to}\n");

                // _mid에 있는것을 _to로 옮긴다
                DFS(_mid, _to, _from, _num - 1);
            }
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
// #include <cmath>
// #define K 40
using namespace std;
typedef unsigned long long int ulli;
char str[1<<22];

void calc_prt(int n) {
    ulli sig = 0, unsig = 1, LIMIT = 1e14;
    for (int i = 1; i <= n; i++) {
        unsig *= 2;
        sig *= 2;
        if (unsig > LIMIT) {
            sig += unsig / LIMIT;
            unsig = unsig % LIMIT;
        }
    }
    unsig--;
    if (sig > 0) printf("%llu%014llu", sig, unsig);
    else printf("%llu\n", unsig);
}

void move(int n, int start, int goal, char* str, int& idx) {
    if (n == 1) {
        str[idx++] = start + '0';
        str[idx++] = ' ';
        str[idx++] = goal + '0';
        str[idx++] = '\n';
        return;
    }
    int temp = 6 - (start + goal);
    move(n-1, start, temp, str, idx);
    move(1, start, goal, str, idx);
    move(n-1, temp, goal, str, idx);
}

int main() {
    int n;
    scanf("%d", &n);
    calc_prt(n);

    if (n <= 20) {
        int idx = 0;
        move(n, 1, 3, str, idx);
        printf("%s", str);
    }
    return 0;
}

#endif
}
