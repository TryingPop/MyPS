using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 25
이름 : 배성훈
내용 : Король и королева
    문제번호 : 22105번

    수학 문제다.
    8방향의 영역을 보면 각 영역은
    삼각형과 사각형의 합으로 표현이 가능하고,
    해당 경우는 3가지로 표현가능하다.
    해당 경우를 분석해 경우의 수를 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1218
    {

        static void Main1218(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n, m, a, b;
            long[] ret;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();
                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Dispose();
                sw.Dispose();
            }

            void GetRet()
            {

                int len = 0;

                Cnt(a - 1, b - 1);
                Cnt(n - a, b - 1);
                Cnt(a - 1, m - b);
                Cnt(n - a, m - b);

                Array.Sort(ret, 0, len);
                sw.Write($"{len} ");
                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{ret[i]} ");
                }

                sw.Write('\n');

                void Cnt(int _min, int _max)
                {

                    if (_max < _min)
                    {

                        int temp = _max;
                        _max = _min;
                        _min = temp;
                    }

                    long cnt = 1L * (_min - 1) * _min / 2;
                    if (cnt > 0) ret[len++] = cnt;
                    else cnt = 0;
                    cnt += 1L * (_max - _min) * _min;
                        if (cnt > 0) ret[len++] = cnt;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                ret = new long[8];
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                a = ReadInt();
                b = ReadInt();
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

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <vector>
// #include <algorithm>

using namespace std;

int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    int t;
    cin >> t;
    for(int a0 = 1; a0 <= t; ++a0){
        long long n, m, x, y;
        cin >> n >> m >> x >> y;
        vector<long long> v;
        if(y >= x){
            if(x >= 3){
                v.push_back((x - 2) * (x - 1) / 2);
            }
        }else{
            if(x >= 3 && y >= 2){
                v.push_back((y - 1) * (2 * x - 2 - y) / 2);
            }
        }
        if(m - y + 1 >= x){
            if(x >= 3){
                v.push_back((x - 2) * (x - 1) / 2);
            }
        }else{
            if(x >= 3 && m - y + 1 >= 2){
                v.push_back((m - y) * (2 * x - 3 - m + y) / 2);
            }
        }
        if(x >= y){
            if(y >= 3){
                v.push_back((y - 2) * (y - 1) / 2);
            }
        }else{
            if(y >= 3 && x >= 2){
                v.push_back((x - 1) * (2 * y - 2 - x) / 2);
            }
        }
        if(n - x + 1 >= y){
            if(y >= 3){
                v.push_back((y - 2) * (y - 1) / 2);
            }
        }else{
            if(y >= 3 && n - x + 1 >= 2){
                v.push_back((n - x) * (2 * y - 3 - n + x) / 2);
            }
        }
        if(y >= n - x + 1){
            if(n - x + 1 >= 3){
                v.push_back((n - x - 1) * (n - x) / 2);
            }
        }else{
            if(n - x + 1 >= 3 && y >= 2){
                v.push_back((y - 1) * (2 * n - 2 * x - y) / 2);
            }
        }
        if(m - y + 1 >= n - x + 1){
            if(n - x + 1 >= 3){
                v.push_back((n - x - 1) * (n - x) / 2);
            }
        }else{
            if(n - x + 1 >= 3 && m - y + 1 >= 2){
                v.push_back((m - y) * (2 * n - 2 * x - m + y - 1) / 2);
            }
        }
        if(x >= m - y + 1){
            if(m - y + 1 >= 3){
                v.push_back((m - y - 1) * (m - y) / 2);
            }
        }else{
            if(m - y + 1 >= 3 && x >= 2){
                v.push_back((x - 1) * (2 * m - 2 * y - x) / 2);
            }
        }
        if(n - x + 1 >= m - y + 1){
            if(m - y + 1 >= 3){
                v.push_back((m - y - 1) * (m - y) / 2);
            }
        }else{
            if(m - y + 1 >= 3 && n - x + 1 >= 2){
                v.push_back((n - x) * (2 * m - 2 * y - n + x - 1) / 2);
            }
        }
        sort(v.begin(), v.end());
        cout << v.size();
        for(long long s : v){
            cout << " " << s;
        }
        cout << "\n";
    }
    return 0;
}
#endif
}
