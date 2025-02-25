using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 1
이름 : 배성훈
내용 : 내 생각에 A번인 단순 dfs 문제가 이 대회에서 E번이 되어버린 건에 관하여 (Easy)
    문제번호 : 18251번

    dfs, dp 문제다.
    먼저 트리를 사각형 좌표로 바꾼다.
    그러면 높이의 범위는 log N이 된다.
    이후 up, down을 설정하여 kadane 알고리즘으로 최대가 되는 값을 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1237
    {

        static void Main1237(string[] args)
        {

            long INF = -1_000_000_000_000_000;
            int MAX;
            (int val, int x, int y)[] tree;
            int n;
            Solve();
            void Solve()
            {

                Input();

                SetPos();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(tree, (x, y) => x.x.CompareTo(y.x));
                long ret = INF;

                for (int down = 0; down <= MAX; down++)
                {

                    for (int up = down; up <= MAX; up++)
                    {

                        long sum = 0;
                        for (int i = 0; i < n; i++)
                        {

                            if (ChkInvalidY(up, down, tree[i].y)) continue;
                            sum = Math.Max(sum + tree[i].val, tree[i].val);
                            ret = Math.Max(sum, ret);
                        }
                    }
                }


                Console.Write(ret);

                bool ChkInvalidY(int _up, int _down, int _val)
                {

                    return _val < _down || _up < _val;
                }
            }

            void SetPos()
            {

                int x = 0;
                MAX = 0;
                DFS();
                void DFS(int _cur = 0, int _y = 0)
                {

                    if (_cur >= n) return;

                    DFS(_cur * 2 + 1, _y + 1);
                    tree[_cur].x = x++;
                    tree[_cur].y = _y;
                    DFS(_cur * 2 + 2, _y + 1);

                    MAX = Math.Max(_y, MAX);
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                tree = new (int val, int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    tree[i].val = ReadInt();
                }

                sr.Close();
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

                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }

#if other
// #include "bits/stdc++.h"

constexpr int myLog2(int N) {
    
    int res = 0;
    while (N > 1) {
        
        ++res;
        N >>= 1;
    }
    return res;
}

constexpr int N_MAX = 262144, H_MAX = myLog2(N_MAX);

int N, H;

std::array<std::array<long long, H_MAX>, H_MAX> max, currentSum;
std::array<int, N_MAX> tree;


void fastIO() {
    
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(nullptr);
    std::cout.tie(nullptr);
}

void get_input() {
    
    std::cin >> N;
    H = myLog2(N + 1);
    for (int idx = 1; idx <= N; ++idx) std::cin >> tree[idx];
}

void DFS(int cursor = 1, int height = 0) {
    
    if (height == H) return;
    
    DFS(cursor * 2, height + 1);
    
    for (int left = 0; left <= height; ++left) {
        
        for (int right = height; right < H; ++right) {
            
            if (tree[cursor] > 0) {
                
                currentSum[left][right] += tree[cursor];
                max[left][right] = std::max(max[left][right], currentSum[left][right]);
            }
            else {
                
                if (currentSum[left][right] + tree[cursor] < 0) currentSum[left][right] = 0;
                else currentSum[left][right] += tree[cursor];
            }
        }
    }
    
    DFS(cursor * 2 + 1, height + 1);
}

void solve() {
    
    DFS();
    
    long long res = 0;
    
    for (int left = 0; left < H; ++left) {
        
        for (int right = left; right < H; ++right) {
            
            res = std::max(res, max[left][right]);
        }
    }
    
    if (res == 0) res = *std::max_element(tree.begin() + 1, tree.begin() + N + 1);
    
    std::cout << res;
}



int main(int argc, char** argv) {
    
    fastIO();
    get_input();
    solve();
    
    return 0;
}
#endif
}
