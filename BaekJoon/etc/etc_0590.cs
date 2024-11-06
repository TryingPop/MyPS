using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 홀수 찾아 삼만리
    문제번호 : 30786번

    수학, 애드 혹 문제다
    그리디하게 풀었다

    아이디어는 다음과 같다
    거리함수가 택시거리 함수이므로,
    홀수가 나오는 경우를 먼저 분석했다
    그러니 좌표가 홀홀, 짝짝 <-> 홀짝, 짝홀
    이렇게 이동하는 경우만 홀수 거리가 나온다

    이외 홀홀, 짝짝 <-> 홀홀, 짝짝이나 홀짝, 짝홀 <-> 홀짝, 짝홀인 경우
    짝수거리가 나온다

    전체 누적 거리가 홀수가되게 이동해야하므로, 
    홀짝, 짝홀 그룹에 적어도 1개의 원소와 홀홀, 짝짝 그룹에 적어도 1개의 원소가 있어야
    홀수가 나올 수 있다

    그리디하게 홀짝 이동을 1번만 해서 홀수가 나오게 만들었다
    그리고 홀홀, 짝짝 -> 홀홀, 짝짝으로 이동을 먼저 한다(해당 구간 이동 합은 짝수)
    홀홀, 짝짝 -> 홀짝, 짝홀로 1번 이동(해당 구간 이동 합은 홀수)
    홀짝, 짝홀 -> 홀짝, 짝홀로 종료하면 (해당 구간 이동 합은 짝수)

    이렇게 경로를 구상하면, 짝수 + 홀수 + 짝수 = 홀수가된다
    그리고 제출하니 이상없이 통과했다

    입력이 최대 30만이고, 출력도 최대 30만이므로 양이 많아 152ms가 나왔다
    
*/

namespace BaekJoon.etc
{
    internal class etc_0590
    {

        static void Main590(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();
            void Solve()
            {

                sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);
                sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 8);

                int n = ReadInt();

                int[] group1 = new int[n];
                int[] group2 = new int[n];

                int len1 = 0;
                int len2 = 0;

                for (int i = 1; i <= n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    if ((x + y) % 2 == 0) group1[len1++] = i;
                    else group2[len2++] = i;
                }

                if (len1 == 0 || len2 == 0)
                {

                    sw.WriteLine("NO");
                }
                else
                {

                    sw.WriteLine("YES");
                    for (int i = 0; i < len1; i++)
                    {

                        sw.Write($"{group1[i]} ");
                    }

                    for (int i = 0; i < len2; i++)
                    {

                        sw.Write($"{group2[i]} ");
                    }
                }

                sr.Close();
                sw.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using namespace std; using ii = pair<int,int>; using ll = long long; using vi = vector<int>;
// #define rep(i,a,b) for (auto i = (a); i <= (b); ++i)
// #define per(i,a,b) for (auto i = (b); i >= (a); --i)
// #define all(x) begin(x), end(x)
// #define siz(x) int((x).size())
// #define Mup(x,y) x = max(x,y)
// #define mup(x,y) x = min(x,y)
// #define fi first
// #define se second
// #define dbg(...) fprintf(stderr,__VA_ARGS__)

int main() {
    cin.tie(0)->sync_with_stdio(0);
    int n, a=0;
    cin >> n;
    int odd=0, even=0;
    rep(i,1,n) {
        int x, y;
        cin >> x >> y;
        if ((x^y)&1) odd=i;
        else even=i;
    }
    if (odd and even) {
        cout << "YES\n";
        cout << odd <<  ' ';
        rep(i,1,n) if (i != odd && i != even)
            cout << i << ' ';
        cout << even;
    } else {
        cout << "NO";
    }
}
#endif
}
