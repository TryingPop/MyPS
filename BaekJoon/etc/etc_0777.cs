using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 27
이름 : 배성훈
내용 : 다이아몬드 광산
    문제번호 : 1028번

    dp, 누적합 문제다
    고민해도 안떠올라 검색했다
    위아래 좌우 대각선들을 먼저 찾는다
    그리고 해당 지점으로 오른쪽 위아래(<) 방향으로 
    방향으로 뻗어가면서 다이아몬드가 되는지 확인한다
*/

namespace BaekJoon.etc
{
    internal class etc_0777
    {

        static void Main777(string[] args)
        {

            StreamReader sr;

            int row, col;
            int[][] board;

            int[][] ru, lu, rd, ld;
            Solve();
            void Solve()
            {

                Input();

                FillArr();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        /*
                        // 중복되는 부분
                        // 아래쪽 검사
                        // 0 0 1 0 0
                        // 0 1 0 1 0
                        // 1 0 * 0 1
                        // 0 1 0 1 0
                        // 0 0 * 0 0
                        // * 부분을 아래로 이동하며 다이아몬드가 되는지 확인한다
                        for (int i = 1; i <= Math.Min(ld[r][c], rd[r][c]); i++)
                        {

                            int nR = r + 2 * (i - 1);

                            if (nR > row) break;
                            if (board[nR][c] > 0
                                && lu[nR][c] >= i
                                && ru[nR][c] >= i) ret = Math.Max(ret, i);
                        }
                        */

                        // 오른쪽 검사
                        // 0 0 1 0 0
                        // 0 1 0 1 0
                        // 1 0 * 0 *
                        // 0 1 0 1 0
                        // 0 0 1 0 0
                        // * 부분을 오른쪽으로 이동하며 다이아몬드가 되는지 확인한다
                        for (int i = 1; i <= Math.Min(ru[r][c], rd[r][c]); i++)
                        {

                            int nC = c + 2 * (i - 1);

                            if (nC > col) break;
                            if (board[r][nC] > 0
                                && lu[r][nC] >= i
                                && ld[r][nC] >= i) ret = Math.Max(ret, i);
                        }
                    }
                }

                Console.Write(ret);
            }

            void FillArr()
            {

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        // 위에 대각선 몇개까지 이어졌는지 확인
                        // 0 0 1
                        // 0 1 0
                        // 1 0 0 
                        // 의 맵인 경우 ru는
                        // 0 0 1
                        // 0 2 0
                        // 3 0 0
                        // 이된다
                        // 이는 오른쪽 위로 3개까지 뻗어갈 수 있다는 말이다
                        if (board[r][c] == 0) continue;
                        lu[r][c] = lu[r - 1][c - 1] + 1;
                        ru[r][c] = ru[r - 1][c + 1] + 1;
                    }
                }

                for (int r = row; r > 0; r--)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        // 아래 대각선 몇개까지 이어졌는지 확인
                        if (board[r][c] == 0) continue;
                        ld[r][c] = ld[r + 1][c - 1] + 1;
                        rd[r][c] = rd[r + 1][c + 1] + 1;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row + 2][];

                lu = new int[row + 2][];
                ru = new int[row + 2][];
                ld = new int[row + 2][];
                rd = new int[row + 2][];

                for (int r = 0; r < row + 2; r++)
                {

                    lu[r] = new int[col + 2];
                    ru[r] = new int[col + 2];

                    ld[r] = new int[col + 2];
                    rd[r] = new int[col + 2];

                    board[r] = new int[col + 2];
                }

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        board[r][c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
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
var input = new StreamReader(Console.OpenStandardInput());

var size = input.ReadLine().Split().Select(int.Parse).ToArray();

var diagonals = new int[size[0], size[1], 2]; // 0 for down-left, 1 for down-right
for (int r = 0; r < size[0]; r++)
{
    var row = input.ReadLine();
    for (int c = 0; c < size[1]; c++)
    {
        if (row[c] == '0')
            continue;

        int dlLen = r - 1 < 0 || c + 1 >= size[1] ? 1 : diagonals[r - 1, c + 1, 0] + 1;
        int drLen = r - 1 < 0 || c - 1 < 0 ? 1 : diagonals[r - 1, c - 1, 1] + 1;

        diagonals[r, c, 0] = dlLen;
        diagonals[r, c, 1] = drLen;
    }
}
input.Close();

int maxSize = 0;
for (int r = size[0] - 1; r >= 0; r--)
{
    for (int c = size[1] - 1; c >= 0; c--)
    {
        // Check diamonds from the very bottom.
        int s = Math.Min(diagonals[r, c, 0], diagonals[r, c, 1]);

        // Skip smaller or equal lengths.
        if (s <= maxSize)
            continue;
        
        // Console.WriteLine($"Checking for coordinate({r},{c})...");

        // if it's one, it's one because thats what one is.
        if (s == 1)
        {
            maxSize = 1;
            continue;
        }
        
        for (int ds = s; ds > maxSize; ds--)
        {
            int top = r - ds + 1;
            int left = c - ds + 1;
            int right = c + ds - 1;

            // Out of index.
            if (top < 0 || left < 0 || right >= size[1])
                continue;

            if (diagonals[top, left, 0] >= ds && diagonals[top, right, 1] >= ds)
                maxSize = ds;
        }
    }
}

Console.WriteLine(maxSize);
#elif other2
// #include <cstdio>
// #define bs 1 << 15
// #define M(a, b) a < b ? a : b
// #define lp(x,k) for(x=1;x<=k;x++)

char rbuf[bs];
int l=0,r=0;
inline char read() {
    if(l==r) { r=(int)fread(rbuf, 1, bs, stdin); l=0; }
    return rbuf[l++];
}

int main() {
    short d[2][752][752];
    int i,j,r,c,k,m=0;
    
    scanf("%d %d\n",&r,&c);
    lp(i,r) { lp(j,c) { d[0][i][j] = d[1][i][j] = read()-48; } read(); }
    
    for(i=r;i;i--) for(j=c;j;j--)
    { d[0][i][j]*=(1+d[0][i+1][j-1]); d[1][i][j]*=(1+d[1][i+1][j+1]); }
            
    
    lp(i,r) lp(j,c) for (k=M(d[0][i][j],d[1][i][j]);k>=m;k--)
    if (d[1][i+k-1][j-k+1]>=k&&d[0][i+k-1][j+k-1]>=k) { m=k; break; }
                
    printf("%d\n",m);
}

#endif
}
