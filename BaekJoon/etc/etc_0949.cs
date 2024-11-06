using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 7
이름 : 배성훈
내용 : 고득점
    문제번호 : 3663번

    그리디 문제다
    ... 끝부분이 시작부분보다 작아져 계속해서 틀렸다;
    아이디어는 다음과 같다
    
    예를들어
        SAO
    이라는 문자열을 보자
    S에서 시작해서 왼쪽으로 이동해 O로 바꾸는게 좋다

    그리고 다음 경우를 보자
        AAABBBA
        1234567
    인 경우 1 -> 2 -> 3 -> 4로 가는거보다
    1 -> 7 -> 6 순으로 이동한 뒤 6번째 글자 B,
    5번째 글자 B, 4번째 글자 B 순으로 바꾸는게 최소같다

    그래서 시작지점과 방향을 구하는게 중요해보인다
    이후 시작지점 이동하는데 최소비용과 끝지점으로 가는데 최소비용을 찾은 뒤
    변환하는 수를 더해 주니 최소로 통과했다

    다만, 끝지점이 시작지점보다 작은 경우를 고려 못해 계속해서 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0949
    {

        static void Main949(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int len;
            int[] cnt;
            int sum;

            Solve();
            void Solve()
            {

                Input();

                for (int i = 0; i < n; i++)
                {

                    SetArr();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int ret = 123_456;

                for (int s = 0; s < len; s++)
                {

                    int e = s + len - 1;
                    while (s < e && cnt[e] == 0) { e--; }

                    int chk = e - s;

                    chk += GetDis(s, e);
                    ret = Math.Min(ret, chk);
                }

                ret += sum;
                sw.Write($"{ret}\n");
            }

            int GetDis(int _s, int _e)
            {

                if (len <= _s) _s -= len;
                if (len <= _e) _e -= len;

                return Math.Min(Math.Min(_s, _e), Math.Min(len - _s, len - _e));
            }

            void SetArr()
            {

                string str = sr.ReadLine().Trim();
                len = str.Length;

                sum = 0;
                for (int i = 0; i < len; i++)
                {

                    int val = Math.Min(str[i] - 'A', 'Z' - str[i] + 1);
                    cnt[i] = val;
                    cnt[i + len] = val;

                    sum += val;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine().Trim());
                cnt = new int[2_001];
            }
        }
    }
}
