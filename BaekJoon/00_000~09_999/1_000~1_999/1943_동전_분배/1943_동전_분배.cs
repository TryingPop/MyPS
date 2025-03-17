using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 15
이름 : 배성훈
내용 : 동전 분배
    문제번호 : 1943번

    dp, 배낭문제다.
    약간의 최적화가 필요하다.
    단순히 갯수만큼 카운팅하면 시간초과 난다.
    그래서 있는 경우 한번에 처리하니 이상없이 통과한다.

    더 빠른 방법은 없을까 하고 다른 사람의 풀이를 보니
    2배수를 이용한 방법이 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_1275
    {

        static void Main1275(string[] args)
        {

            string YES = "1\n";
            string NO = "0\n";

            int MAX_SUM = 100_000;
            int MAX_N = 100;

            StreamReader sr;
            StreamWriter sw;
            int n;
            (int val, int cnt)[] arr;
            int sum;
            bool[] possible;

            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < 3; i++)
                {

                    Input();
                    if (GetRet()) sw.Write(YES);
                    else sw.Write(NO);
                }

                sr.Close();
                sw.Close();
            }

            bool GetRet()
            {

                if ((sum & 1) == 1) return false;
                int max = 0;
                possible[0] = true;
                int half = sum >> 1;

                for (int i = 0; i < n; i++)
                {

                    ref int cnt = ref arr[i].cnt;
                    for (int j = 1; j <= cnt; j <<= 1)
                    {

                        cnt -= j;
                        int val = arr[i].val * j;
                        for (int k = max; k >= 0; k--)
                        {

                            if (!possible[k]) continue;
                            int next = k + val;
                            possible[next] = true;
                        }

                        AddMax(val);
                    }

                    if (cnt == 0) continue;
                    for (int k = max; k >= 0; k--)
                    {

                        if (!possible[k]) continue;
                        int next = k + arr[i].val * cnt;
                        possible[next] = true;
                    }

                    AddMax(arr[i].val * cnt);
                    cnt = 0;
                }


                return possible[half];

                void AddMax(int _add)
                {

                    max += _add;
                    if (max > half) max = half;
                }
            }

            void Input()
            {

                n = ReadInt();
                sum = 0;

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
                    sum += arr[i].val * arr[i].cnt;
                }

                Array.Fill(possible, false, 0, sum);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                possible = new bool[MAX_SUM + 1];
                arr = new (int val, int cnt)[MAX_N];
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

#if other
// #include<bits/stdc++.h>
using namespace std;
int main()
{
    int N,M,i;
    bitset<100001>C;
    for(int T=3;T;T--)
    {
        M=0;
        int A,B;
        C.reset();C=1;
        for(cin>>N,i=1;i<=N;i++)
        {
            cin>>A>>B;
            M+=A*B;
            for(int j=0;B>=(1<<j);B-=1<<j++)C|=C<<(A<<j);C|=C<<A*B;
        }
        if(!(M&1)&&C.test(M>>1))puts("1");else puts("0");
    }
}

#endif
}
