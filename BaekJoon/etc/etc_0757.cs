using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 14
이름 : 배성훈
내용 : Zbiór
    문제번호 : 8678번

    수학, 정수론, 사칙연산 문제다
    앞의 숫자의 약수들이 뒤의 숫자의 약수들에 포함되는지 확인하는 문제다
    처음에는 앞의 약수들을 구해 진행했는데, 시간초과떳다;
    이후 그냥 앞의 숫자가 뒤의 숫자를 나누면 되는거 아닌가 의문이 들었고
    해당 방법으로 바꾸니 180ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0757
    {

        static void Main757(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            string YES = "TAK\n";
            string NO = "NIE\n";

            int n, m;
            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();

                while (test-- > 0)
                {

                    n = ReadInt();
                    m = ReadInt();

                    if (m % n == 0) sw.Write(YES);
                    else sw.Write(NO);
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int main()
{
    ios::sync_with_stdio(0); cin.tie(0);
    int t; cin >> t;
    while(t--) {
        int a,b; cin >> a >> b;
        cout << (b%a==0 ? "TAK\n" : "NIE\n");
    }
    return 0;
}
#endif
}
