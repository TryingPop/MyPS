using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 23
이름 : 배성훈
내용 : Przyciski
    문제번호 : 8576번

    구현, 자료구조, 해시 문제다
    문제를 이해하는데 시간이 많이 걸렸다
    버튼은 초기에 0으로 되어져 있으며
    n은 버튼 종류의 갯수이고 1 ~ n까지 존재한다
    1 ~ n번 버튼을 누르면 1 ~ n 번 값이 1 증가한다
    n + 1번 버튼을 누르는 경우 가장 높은 값으로 갱신된다

    m은 버튼을 누른 횟수이고
    p_i는 i번째 누른 버튼을 의미한다

    예를들어
    5 5
    1 2 6 1 1
    인경우
    1번 버튼을 먼저 누른다
    1번 값이 1 증가한다
        1 0 0 0 0
    2번 버튼을 누른다
    2번 값이 1 증가한다
        1 1 0 0 0
    6번 버튼을 누른다
    1 ~ 5번 값 모두 가장 큰 값 1로 변한다
        1 1 1 1 1
    1번 버튼을 누른다
    1번 값이 증가한다
        2 1 1 1 1
    1번 버튼을 누른다
    1번 값이 증가한다
        3 1 1 1 1

    아이디어는 다음과 같다
    가장 큰 값을 저장하고
    n + 1 버튼 눌렀을 때 갱신되는 값도 저장하니 이상없이 풀린다
*/

namespace BaekJoon.etc
{
    internal class etc_1073
    {

        static void Main1073(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] arr;
            int n, m;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int save = 0;
                int max = 0;

                for (int i = 0; i < m; i++)
                {

                    int cur = ReadInt();
                    if (cur == n + 1) save = max;
                    else
                    {

                        if (arr[cur] < save) arr[cur] = save;
                        arr[cur]++;
                        if (max < arr[cur]) max = arr[cur];
                    }
                }

                sr.Close();

                for (int i = 1; i <= n; i++)
                {

                    if (arr[i] < save) arr[i] = save;
                    sw.Write($"{arr[i]} ");
                }

                sw.Close();

            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr = new int[n + 1];
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
// #include <iostream>
using namespace std;

int main(void){
    ios::sync_with_stdio(0); cin.tie(0);
    pair<int,int> a[1'000'005];
    int n,m; cin >> n >> m;
    int s = 1, maxv = 0, mm = 0;

    while (m--){
        int v; cin >> v;
        if (v == n + 1){
            mm += maxv;
            s++;
            maxv = 0;
        }
        else {
            auto [p,q] = a[v];
            if (p < s){
                a[v].first = s;
                q = 0;
            }
            maxv = max(maxv, ++q);
            a[v].second = q;
        }
    }
    for (int i = 1; i <= n; i++){
        auto [p,q] = a[i];
        if (p < s)
            q = 0;
        cout << q + mm << " ";
    }
    cout << "\n";
}

#endif
}
