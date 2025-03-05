using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 5
이름 : 배성훈
내용 : Lygtys
    문제번호 : 7269번

    수학 문제다.
    수식을 풀어서 계산했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1376
    {

        static void Main1376(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            long n = ReadLong();

            long[] arr = new long[n];
            long an = 0;
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadLong();
                an += arr[i];
            }

            an -= arr[n - 1] + arr[n - 1];
            an /= (n - 2);

            for (int i = 0; i < n - 1; i++)
            {

                sw.WriteLine(arr[i] - an);
            }

            sw.Write(an);


            long ReadLong()
            {

                long ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

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
// #include <iostream>
using namespace std;

int main(void){
    ios::sync_with_stdio(0); cin.tie(0);
    long long a[1'000'002];
    int n; cin >> n;
    long long t = 0;
    for (int i = 1; i < n; i++){
        cin >> a[i];
        t += a[i];
    }
    long long s; cin >> s;
    int an = (t - s) / (n-2);
    for (int i = 1; i < n; i++)
        cout << a[i]-an << "\n";
    cout << an << "\n";
}

#endif
}
