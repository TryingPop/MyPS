using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 소가 길을 건너간 이유 2
    문제번호 : 14468번

    브루트 포스로 풀었다
    문제 이해가 중요하다

    ? 1 ? 2 ? 1 ? 2 ?인 1, 2 쌍이 몇 개인지 찾는 문제다
    여기서 ?는 문자가 있어도 되고 없어도 된다

    원형 길이긴 하나
    한쪽 구간만 확인하면 된다

    from -> to을 확인하고 다른 문자에서 포함되어있는지 판별로
    to -> from을 대체했다
*/

namespace BaekJoon.etc
{
    internal class etc_0129
    {

        static void Main129(string[] args)
        {

            string str = Console.ReadLine();

            // 교차 판별용 배열
            bool[][] chk = new bool[26][];

            for (int i = 0; i < 26; i++)
            {

                chk[i] = new bool[26];
            }

            // 해당 알파벳 끝부분에서 탐색하는거 방지
            bool[] visit = new bool[26];

            for (int i = 0; i < 52; i++)
            {

                char cur = str[i];
                int curIdx = cur - 'A';
                if (visit[curIdx]) continue;
                visit[curIdx] = true;

                for (int j = i + 1; j < 52; j++)
                {

                    if (str[j] == str[i]) break;
                    int nextIdx = str[j] - 'A';
                    chk[curIdx][nextIdx] = true;
                }
            }

            int ret = 0;
            for (int i = 0; i < 26; i++)
            {

                for (int j =0; j < 26; j++)
                {

                    if (chk[i][j] && chk[j][i]) ret++;
                }
            }

            ret /= 2;

            Console.WriteLine(ret);
        }
    }

#if other
string s = Console.ReadLine();
int[,] cow = new int[26,2];
for (int i = 0; i < s.Length; i++)
{
    int now = s[i] - 'A';
    if (cow[now, 0] == 0) cow[now, 0] = i + 1;
    else cow[now, 1] = i + 1;
}

int ans = 0;
int n = 26;
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        int s1 = cow[i, 0];
        int e1 = cow[i, 1];
        int s2 = cow[j, 0];
        int e2 = cow[j, 1];
        if (s1 < s2 && s2 < e1 && e1 < e2) ans++;
    }
}
Console.WriteLine(ans);
#endif
}
