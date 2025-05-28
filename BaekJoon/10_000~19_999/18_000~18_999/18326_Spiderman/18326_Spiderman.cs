using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 28
이름 : 배성훈
내용 : Spiderman 
    문제번호 : 18326번

    수학 문제다.
    에라토스테네스의 체 아이디어를 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1649
    {

        static void Main1649(string[] args)
        {

            int n, k;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int MAX = 1_000_000;
                
                int[] ret = new int[MAX + 1];

                Find();

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int pop = k == 0 ? -1 : 0;
                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{ret[arr[i]] + pop} ");
                }

                void Find()
                {

                    int[] cnt = new int[MAX + 1];

                    for (int i = 0; i < n; i++)
                    {

                        cnt[arr[i]]++;
                    }

                    for (int i = k + 1; i <= MAX; i++)
                    {

                        if (cnt[i] == 0) continue;
                        int cur = cnt[i];
                        for (int j = k; j <= MAX; j += i)
                        {

                            if (cnt[j] == 0) continue;
                            ret[j] += cur;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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

#if other
// #include <iostream>
using namespace std;

int c[1'000'003];
int p[1'000'003];
int main(void){
    const int MH = 1'000'000;
    ios::sync_with_stdio(0); cin.tie(0);
    int n,k; cin >> n >> k;
    int a[300'003];
    for (int i = 0; i < n; i++){
        int h; cin >> h;
        c[h]++;
        a[i] = h;
    }
    for (int i = k+1; i <= MH; i++){
        for (int j = k; j <= MH; j += i){
            p[j] += c[i];
        }
    }
    for (int i = 0; i < n; i++)
        cout << p[a[i]] - (k==0) << " ";
    cout << "\n";
}

#endif
}
