using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 5
이름 : 배성훈
내용 : Lavaspar
    문제번호 : 20287번

    브루트포스 문제다.
    조건대로 구현한 뒤 각 칸을 조사하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1375
    {

        static void Main1375(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int row, col;
            int[][] board;

            int[][] findStrs;
            int n;

            int[][] state;

            Input();

            GetRet();
            
            void GetRet()
            {

                state = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    state[r] = new int[col];
                }

                int[] cur = new int[27];
                int[] dirR = { 0, 1, 1, -1 };
                int[] dirC = { 1, 0, 1, 1 };

                for (int i = 0; i < n; i++)
                {

                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            ChkAnagram(r, c, i);
                        }
                    }
                }

                int ret = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        for (int i = 0; i < n; i++)
                        {

                            if ((state[r][c] & (1 << i)) == 0) continue;

                            if (state[r][c] != (1 << i)) ret++;
                            break;
                        }
                    }
                }

                Console.Write(ret);

                void ChkAnagram(int _r, int _c, int _i)
                {

                    for (int i = 0; i < 4; i++)
                    {

                        Array.Fill(cur, 0);

                        for (int j = 0; j < findStrs[_i][0]; j++)
                        {

                            int nR = _r + j * dirR[i];
                            int nC = _c + j * dirC[i];

                            if (ChkInvalidPos(nR, nC)) break;
                            cur[0]++;
                            cur[board[nR][nC]]++;
                        }

                        bool flag = true;

                        for (int j = 0; j <= 26; j++)
                        {

                            if (cur[j] != findStrs[_i][j])
                            {

                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {

                            for (int j = 0; j < findStrs[_i][0]; j++)
                            {

                                int nR = _r + j * dirR[i];
                                int nC = _c + j * dirC[i];

                                state[nR][nC] |= 1 << _i;
                            }
                        }
                    }

                    bool ChkInvalidPos(int _r, int _c) => _r < 0 || _r >= row || _c < 0 || _c >= col;
                }
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();
                board = new int[row][];
                
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    string input = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = input[c] - 'A' + 1;
                    }
                }

                n = ReadInt();
                findStrs = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    findStrs[i] = new int[27];

                    string input = sr.ReadLine();
                    findStrs[i][0] = input.Length;
                    for (int j = 0; j < input.Length; j++)
                    {

                        findStrs[i][input[j] - 'A' + 1]++;
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
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <string.h>
// #include <algorithm>
using namespace std;

int r, c, n, l;
char s[45][45], word[30];
int cnt[45][45], alpha[26];
bool check[45][45];
int dx[] = {-1, -1, 0, 1, 1, 1, 0, -1}, dy[] = {0, 1, 1, 1, 0, -1, -1, -1};

bool in_border(int x, int y){
    return 0 <= x && x < r && 0 <= y && y < c;
}

void validate(int x, int y, int d){
    int alpha_cnt[26];
    fill(alpha_cnt, alpha_cnt + 26, 0);
    for(int i = 0; i < l; i++){
        int nx = x + i * dx[d], ny = y + i * dy[d];
        if(!in_border(nx, ny)) return;
        alpha_cnt[s[nx][ny] - 'A']++;
    }
    for(int i = 0; i < 26; i++){
        if(alpha[i] != alpha_cnt[i]) return;
    }
    for(int i = 0; i < l; i++){
        int nx = x + i * dx[d], ny = y + i * dy[d];
        check[nx][ny] = true;
    }
}

void search(){
    for(int i = 0; i < r; i++){
        fill(check[i], check[i] + c, false);
    }
    for(int i = 0; i < r; i++){
        for(int j = 0; j < c; j++){
            for(int k = 0; k < 8; k++){
                validate(i, j, k);
            }
        }
    }
    for(int i = 0; i < r; i++){
        for(int j = 0; j < c; j++){
            if(check[i][j]) cnt[i][j]++;
        }
    }
}

int main(){
    scanf("%d %d", &r, &c);
    for(int i = 0; i < r; i++) scanf("%s", s[i]);
    scanf("%d", &n);
    for(int i = 0; i < n; i++){
        scanf("%s", word);
        l = strlen(word);
        fill(alpha, alpha + 26, 0);
        for(int j = 0; j < l; j++) alpha[word[j] - 'A']++;
        search();
    }
    int ans = 0;
    for(int i = 0; i < r; i++){
        for(int j = 0; j < c; j++){
            // printf("%d ", cnt[i][j]);
            if(cnt[i][j] >= 2) ans++;
        }
        // printf("\n");
    }
    printf("%d", ans);
}
#endif
}
