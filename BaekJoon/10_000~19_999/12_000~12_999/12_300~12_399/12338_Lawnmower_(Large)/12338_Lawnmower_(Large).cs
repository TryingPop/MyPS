using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 3
이름 : 배성훈
내용 : Lawnmower (Large, Small)
    문제번호 : 12338번, 12337번

    브루트포스, 애드 혹 문제다.
    아이디어는 다음과 같다.

    열과 행을 비교하며 가장 큰 값으로 깎는다.
    이제 각 좌표를 비교하는데 
    열과 행 모두 해당 값보다 작은게 있으면 못만든다.

    이는 귀류법을 적용해 보면 쉽게 증명된다.
    간단히 작은게 존재한다고 하면, 
    해당 열과 행은 해당 값으로 밀어야 한다.
    해당 값보다 큰 값이 존재해 모순이 됨을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1242
    {

        static void Main1242(string[] args)
        {

            int SIZE = 100;

            string HEAD = "Case #";
            string MID = ": ";
            string YES = "YES\n";
            string NO = "NO\n";

            StreamReader sr;
            StreamWriter sw;

            int[][] board;
            int n, m;
            int[] row, col;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 1; i <= t; i++)
                {

                    Input();

                    bool ret = GetRet();
                    sw.Write($"{HEAD}{i}{MID}");
                    sw.Write(ret ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            bool GetRet()
            {

                for (int r = 0; r < n; r++)
                {

                    int max = 1;
                    for (int c = 0; c < m; c++)
                    {

                        max = Math.Max(max, board[r][c]);
                    }

                    row[r] = max;
                }

                for (int c = 0; c < m; c++)
                {

                    int max = 1;
                    for (int r = 0; r < n; r++)
                    {

                        max = Math.Max(max, board[r][c]);
                    }

                    col[c] = max;
                }

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < m; c++)
                    {

                        if (board[r][c] < row[r] && board[r][c] < col[c]) return false;
                    }
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[SIZE][];
                for (int i = 0; i < SIZE; i++)
                {

                    board[i] = new int[SIZE];
                }

                row = new int[SIZE];
                col = new int[SIZE];
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < m; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }
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
// #include <stdio.h>
// #include <algorithm>
using namespace std;

int n, m, arr[110][110], mxc[110], mxr[110];

int main(){
    int t;
    scanf("%d", &t);
    for(int tt = 1; tt <= t; tt++){
        scanf("%d %d", &n, &m);
        for(int i = 0; i < n; i++) mxr[i] = 0;
        for(int i = 0; i < m; i++) mxc[i] = 0;
        for(int i = 0; i < n; i++){
            for(int j = 0; j < m; j++){
                scanf("%d", &arr[i][j]);
                mxr[i] = max(mxr[i], arr[i][j]);
                mxc[j] = max(mxc[j], arr[i][j]);
            }
        }
        // for(int i = 0; i < n; i++) printf("%d ", mxr[i]); printf("\n");
        // for(int i = 0; i < m; i++) printf("%d ", mxc[i]); printf("\n");
        bool valid = true;
        for(int i = 0; i < n; i++){
            for(int j = 0; j < m; j++){
                if(arr[i][j] < mxr[i] && arr[i][j] < mxc[j]) valid = false;
            }
        }
        printf("Case #%d: %s\n", tt, valid ? "YES" : "NO");
    }
}
#endif
}
