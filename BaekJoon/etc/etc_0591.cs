using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 삼각 초콜릿 포장 (Sweet)
    문제번호 : 31462번

    구현, 그리디 문제다
    빨간색 -> 파란색 순서로 탐색했다
    처음엔 위에서 아래로 빨강색과 파란색을 탐색했다
    이를 합칠 수 있지 않을까? 해서 빨간색 탐색에 수정없이 그냥 합쳐서 제출하니 1번 틀렸다

    실제로, 파란색 부분은 밑에서 위로 탐색하는 경우이므로 위에 2개를 지우는 형식인데
    위에서 아래로 탐색할 때에는 오른쪽과 오른쪽 아래를 지워야하므로 지우는 칸이 다르다!

    처음에는 그냥 구분지은 걸로 제출 해서 먼저 296ms에 통과하고,
    이후에 하나로 합쳐서 제출하니 268ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0591
    {

        static void Main591(string[] args)
        {

            StreamReader sr;
            Solve();

            void Solve()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);

                int n = int.Parse(sr.ReadLine());

                int calc = n % 12;
                bool ret = Find(n);
                sr.Close();

                Console.Write(ret ? 1 : 0);
            }

            bool Find(int _n)
            {

                int[][] board = new int[_n + 1][];
                for (int i = 1; i <= _n; i++)
                {

                    board[i] = new int[i];

                    for (int j = 0; j < i; j++)
                    {

                        int cur = sr.Read();
                        cur = cur == 'R' ? 1 : -1;
                        board[i][j] = cur;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                for (int i = 1; i <= _n; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        if (board[i][j] == 1)
                        {

                            if (i == _n) return false;
                            board[i][j] = 0;

                            for (int k = 0; k < 2; k++)
                            {

                                if (j + k > i || board[i + 1][j + k] != 1) return false;
                                board[i + 1][j + k] = 0;
                            }
                        }

                        else if (board[i][j] == -1)
                        {

                            if (i == 1) return false;
                            board[i][j] = 0;

                            for (int k = 0; k < 2; k++)
                            {

                                if (j + 1 >= i || board[i + k][j + 1] != -1) return false;
                                board[i + k][j + 1] = 0;
                            }
                        }
                    }
                }

                return true;
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringBuilder sb = new StringBuilder();
        int N = Integer.parseInt(br.readLine());
        char[][] map = new char[N][N];
        boolean[][] check = new boolean[N][N];
        for (int i = 0; i < N; i++) {
            String S = br.readLine();
            for (int j = 0; j <= i; j++)
                map[i][j] = S.charAt(j);
        }
        int ans = 1;
        loop:
        for (int i = 0; i < N; i++)
            for (int j = 0; j <= i; j++)
                if (map[i][j] == 'R' && !check[i][j]) {
                    if (i == N - 1) {
                        ans = 0;
                        break loop;
                    }
                    if (map[i + 1][j] == 'R' && map[i + 1][j + 1] == 'R' && !check[i + 1][j] && !check[i + 1][j + 1]) {
                        check[i + 1][j] = check[i + 1][j + 1] = true;
                    } else {
                        ans = 0;
                        break loop;
                    }
                }
        if (ans == 1) {
            loop:
            for (int i = 0; i < N; i++)
                for (int j = 0; j <= i; j++)
                    if (map[i][j] == 'B' && !check[i][j]) {
                        if (i == 0 || j == i || i == N - 1) {
                            ans = 0;
                            break loop;
                        }
                        if (map[i][j + 1] == 'B' && map[i + 1][j + 1] == 'B' && !check[i][j + 1] && !check[i + 1][j + 1]) {
                            check[i][j + 1] = check[i + 1][j + 1] = true;
                        } else {
                            ans = 0;
                            break loop;
                        }
                    }
        }
        System.out.println(ans);
    }
}
#endif
}
