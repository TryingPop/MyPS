using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 4
이름 : 배성훈
내용 : 한빛미디어 (Hard)
    문제번호 : 31792번

    그리디, 이분 탐색, 트리를 사용한 집합과 맵 문제다.
    먼저 최소 페이지는 남은 것중 가격이 가장 작은 것부터 페이지에 최대한 넣는 경우 최소 페이지가 됨을 그리디로 알 수 있다.
    그래서 i원을 넘는 최소 값을 찾아줘야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1671
    {

        static void Main1671(string[] args)
        {

            int S = 1_000;
            int E = 1_000_000;
            int INF = 1_000_001;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int[] seg = new int[1 << 21];

            while (n-- > 0)
            {

                int op = ReadInt();

                if (op == 1)
                {

                    int chk = ReadInt();
                    Update(S, E, chk, 1);
                }
                else if (op == 2)
                {

                    int chk = ReadInt();
                    Update(S, E, chk, -1);
                }
                else
                {

                    int ret = 0;
                    int min = 0;

                    while ((min = GetVal(S, E, min * 2)) != INF)
                    {

                        ret++;
                    }

                    sw.Write($"{ret}\n");
                }
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

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] += _val;
                    if (seg[_idx] < 0) seg[_idx] = 0;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (_chk <= mid) Update(_s, mid, _chk, _val, _idx * 2 + 1);
                else Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);

                seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
            }

            int GetVal(int _s, int _e, int _min, int _idx = 0)
            {

                if (_e < _min || seg[_idx] == 0) return INF;
                else if (_s == _e) return _s;

                int mid = (_s + _e) >> 1;
                int chk = GetVal(_s, mid, _min, _idx * 2 + 1);
                if (chk != INF) return chk;

                return GetVal(mid + 1, _e, _min, _idx * 2 + 2);
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int main(){
    ios::sync_with_stdio(0);
    cin.tie(0);

    int n;
    cin >> n;

    multiset<int> s;
    while(n--){
        int cmd;
        cin >> cmd;
        if(cmd == 1){
            int p;
            cin >> p;
            s.insert(p);
        }
        if(cmd == 2){
            int p;
            cin >> p;
            auto itr = s.find(p);
            if(itr != s.end()) s.erase(itr);
        }
        if(cmd == 3){
            int cur = 0;
            int pn = 0;
            while(true){
                auto itr = s.lower_bound(cur*2);
                if(itr == s.end()) break;
                cur = *itr;
                pn++;
            }
            cout << pn << '\n';
        }
    }

    return 0;
}
#endif
}
