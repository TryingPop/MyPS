/*
날짜 : 2024. 4. -
이름 : 배성훈
내용 : 선물이 넘쳐흘러
    문제번호 : 17259번

    구현, 시뮬레이션 문제다
    함수로 나눠서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0640
    {

        static void Main640(string[] args)
        {

            StreamReader sr;
            int n, m;
            int size;
            int[][] board;
            (int row, int col, int turn)[] worker;
            int[] dirR;
            int[] dirC;
            int ret = 0;

            Solve();

            void Solve()
            {

                Input();
                while(true)
                {

                    Move();
                    Work();
                    if (ChkEnd()) break;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();
                n = ReadInt();
                m = ReadInt();
                board = new int[size][];

                for (int r = 0; r < size; r++)
                {

                    board[r] = new int[size];
                }

                worker = new (int row, int col, int turn)[n];
                for (int i = 0; i < n; i++)
                {

                    worker[i] = (ReadInt(), ReadInt(), ReadInt() - 1);
                }

                dirR = new int[3] { 1, 0, -1};
                dirC = new int[3] { 0, 1, 0 };

                sr.Close();
            }

            bool ChkEnd()
            {

                if (m > 0) return false;

                for (int i = 0; i < size; i++)
                {

                    if (board[0][i] == 0 && board[size - 1][i] == 0 && board[i][size - 1] == 0) continue;
                    return false;
                }
                
                return true;
            }

            void Move()
            {

                for (int c = 1; c < size; c++)
                {

                    board[size - 1][c - 1] = board[size - 1][c];
                    board[size - 1][c] = 0;
                }

                for (int r = size - 2; r >= 0; r--)
                {

                    board[r + 1][size - 1] = board[r][size - 1];
                    board[r][size - 1] = 0;
                }

                for (int c = size - 2; c >= 0; c--)
                {

                    board[0][c + 1] = board[0][c];
                    board[0][c] = 0;
                }

                if (m > 0)
                {

                    m--;
                    board[0][0] = 1;
                }
            }

            void Work()
            {

                for (int i = 0; i < n; i++)
                {

                    if (board[worker[i].row][worker[i].col] != 0)
                    {

                        board[worker[i].row][worker[i].col]--;
                        continue;
                    }

                    for (int j = 0; j < 3; j++)
                    {

                        int chkR = worker[i].row + dirR[j];
                        int chkC = worker[i].col + dirC[j];
                        if ((chkR != size - 1 && chkR != 0 && chkC != size - 1)
                            || board[chkR][chkC] == 0) continue;

                        board[chkR][chkC] = 0;
                        board[worker[i].row][worker[i].col] = worker[i].turn;
                        ret++;
                        break;
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
import java.util.ArrayList;
import java.util.StringTokenizer;

public class Main {
	static int B, N, M;
	static boolean arr[][];
	static int dy[] = { 1, 0, -1 }, dx[] = { 0, 1, 0 };
	static ArrayList<Staff> list = new ArrayList<Staff>();
	static int ret, discard;

	static void place(int cur) {
		if (arr[B - 1][0])
			discard += 1;
		for (int i = 0; i < B - 1; i++)
			arr[B - 1][i] = arr[B - 1][i + 1];
		for (int i = B - 1; i > 0; i--)
			arr[i][B - 1] = arr[i - 1][B - 1];
		for (int i = B - 1; i > 0; i--)
			arr[0][i] = arr[0][i - 1];
		if (cur <= M)
			arr[0][0] = true;
		else
			arr[0][0] = false;
	}

	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		B = Integer.parseInt(st.nextToken());
		N = Integer.parseInt(st.nextToken());
		M = Integer.parseInt(st.nextToken());
		arr = new boolean[B][B];

		for (int i = 0, y, x, t; i < N; i++) {
			st = new StringTokenizer(br.readLine());
			y = Integer.parseInt(st.nextToken());
			x = Integer.parseInt(st.nextToken());
			t = Integer.parseInt(st.nextToken());
			list.add(new Staff(y, x, t));
		}

		for (int m = 1; 0 < M - ret - discard; m++) {
			place(m);
			for (Staff s : list) {
				if (s.c == 0) {
					for (int i = 0, y, x; i < 3; i++) {
						y = s.y + dy[i];
						x = s.x + dx[i];
						if (arr[y][x]) {
							s.c++;
							arr[y][x] = false;
							ret++;
							break;
						}
					}
				} else if (0 < s.c && s.c < s.t) {
					s.c++;
				}
				if (s.c == s.t) {
					s.c = 0;
				}
			}

		}
		System.out.println(ret);
	}

	static class Staff {
		int y, x, t, c = 0;

		public Staff(int y, int x, int t) {
			this.y = y;
			this.x = x;
			this.t = t;
		}
	}

}
#endif
}

