using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 계산 로봇
    문제번호 : 22342번

    dp 문제다
    슬라이딩 윈도우로 해결했다

    아이디어는 다음과 같다
    문제를 보면, 왼쪽에서 오른쪽으로 출력값 중 가장 큰게 저장값(= 입력값)이 된다
    그리고 저장값에 가중치를 더해 출력값으로 갖는다 초기에 입력값을 0으로 하니, 가중치가 쌓여가는 형태다

    저장방식과 순서에 의해 저장값 중 최대값은 가장 오른쪽에 위치할 수 밖에 없고
    저장 범위를 보면, 최대를 찾는다면 바로 왼쪽만 확인하면 된다

    이에, 바로 이전꺼만 확인하면서 가중치를 누적해갔다
    그리고, 마지막 오른쪽에 출력값을 확인하고 해당 좌표의 가중치만 빼서 제출하니 156ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0510
    {

        static void Main510(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);
            int row = ReadInt();
            int col = ReadInt();

            int[,] board = new int[row, col];
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = sr.Read() - '0';
                }

                sr.ReadLine();
            }

            sr.Close();
            int[] w = new int[row];
            int[] calc = new int[row];
            for (int i = 0; i < row; i++)
            {

                w[i] = board[i, 0];
            }

            for (int i = 1; i < col; i++)
            {

                if (row > 1)
                {

                    calc[0] = Math.Max(w[0], w[1]) + board[0, i];
                    calc[row - 1] = Math.Max(w[row - 1], w[row - 2]) + board[row - 1, i];
                    for (int j = 1; j < row - 1; j++)
                    {

                        calc[j] = Math.Max(w[j - 1], Math.Max(w[j], w[j + 1])) + board[j, i];
                    }
                }
                else calc[0] = w[0] + board[0, i];

                int[] temp = w;
                w = calc;
                calc = temp;
            }

            int ret = 0;
            for (int i = 0; i < row; i++)
            {

                int chk = w[i] - board[i, col - 1];
                ret = ret < chk ? chk : ret;
            }

            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());
        int N = Integer.parseInt(st.nextToken());
        int M = Integer.parseInt(st.nextToken());
        int[][] d = new int[N][M];
        for (int i = 0; i < N; i++) {
            String line = br.readLine();
            for (int j = 0; j < M; j++) {
                d[i][j] = line.charAt(j) - '0';
            }
        }
        int maxSave = 0;
        int[][] output = new int[N][M];
        for (int col = 0; col < M; col++) {
            for (int row = 0; row < N; row++) {
                int subMax = 0;
                if (col == 0) output[row][col] = d[row][col];
                else {
                    if (row - 1 >= 0) subMax = Math.max(subMax, output[row - 1][col - 1]);
                    subMax = Math.max(output[row][col - 1], subMax);
                    if (row + 1 < N) subMax = Math.max(subMax, output[row + 1][col - 1]);
                    maxSave = Math.max(subMax, maxSave);
                    output[row][col] = subMax + d[row][col];
                }
            }
        }
        System.out.println(maxSave);
    }
}
#elif other2
// #include <bits/stdc++.h>
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()
using namespace std;
typedef long long lint;
typedef pair<int, int> pi;
const int mod = 1e9 + 7;
const int MAXN = 2005;

int n, m;
char a[MAXN][MAXN];
int dp[MAXN][MAXN];

int main(){
	scanf("%d %d",&n,&m);
	for(int i = 0; i < n; i++){
		scanf("%s", a[i]);
	}
	for(int i = 0; i < n; i++) dp[0][i] = a[i][0] - '0';
	int ret = 0;
	for(int i = 1; i < m; i++){
		for(int j = 0; j < n; j++){
			dp[i][j] = dp[i-1][j];
			if(j > 0) dp[i][j] = max(dp[i][j], dp[i-1][j-1]);
			if(j+1 < n) dp[i][j] = max(dp[i][j], dp[i-1][j+1]);
			ret = max(ret, dp[i][j]);
			dp[i][j] += a[j][i] - '0';
		}
	}
	cout << ret << endl;
}

#endif
}
