using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 4
이름 : 배성훈
내용 : 막대 만들기
    문제번호 : 28437번

    dp, 수학, 정수론 문제다
    시간이 긴가민가 하는 마음으로 제출했다
    그러니 이상없이 통과한다

    찾아보니 O(n log n) = n + n / 2 + n / 3 + ... + n / n 이라 한다
*/

namespace BaekJoon.etc
{
    internal class etc_1022
    {

        static void Main1022(string[] args)
        {

            int N = 100_000;

            StreamReader sr;
            StreamWriter sw;

            int n, q;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void SetArr()
            {

                for (int i = 1; i <= N; i++)
                {

                    for (int j = i << 1; j <= N; j += i)
                    {

                        arr[j] += arr[i];
                    }
                }
            }

            void GetRet()
            {

                q = ReadInt();

                for (int i = 0; i < q; i++)
                {

                    int idx = ReadInt();
                    sw.Write($"{arr[idx]} ");
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[N + 1];
                for (int i = 0; i < n; i++)
                {

                    int num = ReadInt();
                    arr[num]++;
                }
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
// #include <algorithm>
// #include <iostream>
// #include <vector>
using namespace std;

int main()
{
    int N, Q;
    cin >> N;
    vector<int> A(N);
    for (int &a : A)
        cin >> a;
    cin >> Q;
    vector<int> L(Q);
    for (int &l : L)
        cin >> l;

    int MX = max(*max_element(A.begin(), A.end()), *max_element(L.begin(), L.end()));
    vector<int64_t> D(MX + 1);
    for (int a : A)
        D[a]++;

    for (int j = 1; j <= MX; j++)
        for (int i = 2 * j; i <= MX; i += j)
            D[i] += D[j];
    for (int l : L)
        cout << D[l] << ' ';
    cout << endl;
}
#endif
}
