using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 25
이름 : 배성훈
내용 : Pizza delivery
    문제번호 : 6697번

    브루트포스 문제다.
    단순히 O(N^4)로 접근하니 2.4초로 통과된다...
    여기서 N은 row, col 중 큰 값이 된다.
    그런데 다른 사람의 풀이를 보니, dp를 이용했다.

    조금 고민하니 분배법칙으로 누적합 아이디어가 떠올랐고,
    누적합으로 접근하니 O(N^3)으로 줄일 수 있었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1916
    {

        static void Main1916(string[] args)
        {

            long INF = 1_234_567_890_123_456;
            string TAIL = " blocks\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();
            int row, col;
            int[][] arr;
            int[][] cSum, rSum;

            Init();

            while (t-- > 0)
            {

                Input();

                FillSum();

                GetRet();
            }

            void GetRet()
            {

                long ret = INF;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        ret = Math.Min(Find(r, c), ret);
                    }
                }

                sw.Write(ret);
                sw.Write(TAIL);

                long Find(int r1, int c1)
                {

                    long ret = 0;

                    for (int r = 0; r < row; r++)
                    {

                        long dis = Math.Abs(r - r1);
                        ret += dis * cSum[r][col - 1];
                    }

                    for (int c = 0; c < col; c++)
                    {

                        long dis = Math.Abs(c - c1);
                        ret += dis * rSum[row - 1][c];
                    }

                    return ret;
                }
            }

            void FillSum()
            {

                for (int r = 0; r < row; r++)
                {

                    cSum[r][0] = arr[r][0];
                    for (int c = 1; c < col; c++)
                    {

                        cSum[r][c] = cSum[r][c - 1] + arr[r][c];
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    rSum[0][c] = arr[0][c];
                    for (int r = 1; r < row; r++)
                    {

                        rSum[r][c] = rSum[r - 1][c] + arr[r][c];
                    }
                }
            }

            void Input()
            {

                col = ReadInt();
                row = ReadInt();

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        arr[r][c] = ReadInt();
                    }
                }
            }

            void Init()
            {

                arr = new int[100][];
                cSum = new int[100][];
                rSum = new int[100][];
                for (int i = 0; i < 100; i++)
                {

                    arr[i] = new int[100];
                    cSum[i] = new int[100];
                    rSum[i] = new int[100];
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

                    while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
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
// #include <algorithm>
// #include <vector>
// #include <numeric>
// #define MAX 102

using namespace std;

int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    int t;
    cin >> t;
    for(int a0 = 1; a0 <= t; ++a0){
        int x, y;
        cin >> x >> y;
        int d[MAX][MAX];
        for(int i = 0; i < y; ++i){
            for(int j = 0; j < x; ++j){
                cin >> d[i][j];
            }
        }
        vector<int> vx;
        for(int i = 0; i < y; ++i){
            int sum = 0;
            for(int j = 0; j < x; ++j){
                sum += d[i][j];
            }
            vx.push_back(sum);
        }
        partial_sum(vx.begin(), vx.end(), vx.begin());
        int sum = vx.back();
        int cx = lower_bound(vx.begin(), vx.end(), (sum + 1) / 2) - vx.begin();
        vector<int> vy;
        for(int j = 0; j < x; ++j){
            int sum = 0;
            for(int i = 0; i < y; ++i){
                sum += d[i][j];
            }
            vy.push_back(sum);
        }
        partial_sum(vy.begin(), vy.end(), vy.begin());
        sum = vy.back();
        int cy = lower_bound(vy.begin(), vy.end(), (sum + 1) / 2) - vy.begin();
        int dist = 0;
        for(int i = 0; i < y; ++i){
            for(int j = 0; j < x; ++j){
                dist += d[i][j] * (abs(cx - i) + abs(cy - j));
            }
        }
        cout << dist << " blocks\n";
    }
    return 0;
}
#endif
}
