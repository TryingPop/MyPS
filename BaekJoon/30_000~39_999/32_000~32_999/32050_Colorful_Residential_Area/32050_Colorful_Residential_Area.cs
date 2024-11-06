using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 27
이름 : 배성훈
내용 : Colorful Residential Area
    문제번호 : 32050번

    구현, 해 구성하기 문제다
    처음에는 처음과 끝이 같아야만 되는거 아닌가 ? 싶었고
        4
        1 2 1 2
    와 같은 반례로 바로 틀렸다

        0 2 1 0
        1 0 0 2
        2 0 0 1
        0 1 2 0
    과 같은 형태면 존재하나 해당 방법으로는 판별할 수 없다
    이후 시간을 들여 문제를 하나씩 분석했다

    먼저 상하좌우로 봤을 때 면이 같아야 한다
    대칭일 수 밖에 없다
    대칭성으로 사각형 테두리부터 안 테두리로 진행 방향을 정했다
    그리고 4 선분 중 하나의 선분만 찾고 나머지는 동시에 채워줬다
    
    채우는 방법은 위에 값이 있는 경우 현재 탐색하는 방향의 값을 채운다
    위에 없으면 위와 같은지 확인한다
    
    위와 같으면 값을 채우고 해당 모서리에 없다면 성립할 수 없다고 판단한다
    이렇게 좌우 끝값을 찾는다

    좌우 끝값이 구해지면 비어있지 않은 경우 위쪽 값을 대입한다
    이렇게 모든 면을 채우면 정답이 된다

    중간에 크기가 1인 경우 고려안하고
    잘못 구현해서 여러 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1004
    {

        static void Main1004(string[] args)
        {

#if MY_TEST
            int MAX = 50;
            string YES = "Yes\n";
            string NO = "No\n";

            StreamReader sr;
            StreamWriter sw;

            int[][] board;

            int n;
            int[] arr;
            bool[] use;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    InitBoard();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

#if CHK
                
                if (FillBoard())
                {

                    sw.Write(YES);
                    if (ChkBoard()) return;
                    sw.Write($"\n\n ##### Case ##### \n\n");

                    sw.Write($"{n}\n");
                    for (int i = 1; i <= n; i++)
                    {

                        sw.Write($"{arr[i]} ");
                    }

                    sw.Write("\n\n");

                    for (int r = 1; r <= n; r++)
                    {

                        for (int c = 1; c <= n; c++)
                        {

                            sw.Write($"{board[r][c]} ");
                        }

                        sw.Write('\n');
                    }
                }

                sw.Write(NO);

                bool ChkBoard()
                {

                    for (int r = 1; r <= n; r++)
                    {

                        for (int c = 1; c <= n; c++)
                        {

                            if (board[r][c] == 0) continue;

                            if (board[r][c] != arr[r]) 
                            {

                                sw.Write($"Left ({r}, {c}) is {arr[r]}!\n");
                                return false; 
                            }

                            break;
                        }

                        for (int c = n; c >= 1; c--)
                        {

                            if (board[r][c] == 0) continue;

                            if (board[r][c] != arr[n + 1 - r])
                            {

                                sw.Write($"Right ({r}, {c}) is {arr[n + 1 - r]}!\n");
                                return false;
                            }

                            break;
                        }
                    }

                    for (int c = 1; c <= n; c++)
                    {

                        for (int r = 1; r <= n; r++)
                        {

                            if (board[r][c] == 0) continue;

                            if (board[r][c] != arr[n + 1 - c])
                            {

                                sw.Write($"Up ({r}, {c}) is {arr[n + 1 - c]}!\n");
                                return false;
                            }

                            break;
                        }

                        for (int r = n; r >= 1; r--)
                        {

                            if (board[r][c] == 0) continue;

                            if (board[r][c] != arr[c])
                            {

                                sw.Write($"Down ({r}, {c}) is {arr[c]}!\n");
                                return false;
                            }

                            break;
                        }
                    }

                    return true;
                }
#else

                if (FillBoard())
                {

                    sw.Write(YES);

                    for (int r = 1; r <= n; r++)
                    {

                        for (int c = 1; c <= n; c++)
                        {

                            sw.Write($"{board[r][c]} ");
                        }

                        sw.Write('\n');
                    }
                }
                else sw.Write(NO);
#endif
            }

            bool FillBoard()
            {

                int left = n, right = 1;
                for (int i = 1; i <= n; i++)
                {

                    int j = n + 1 - i;
                    if (j < i) break;
                    if (Chk(i)) return false;
                }

                return true;

                bool Chk(int _r)
                {

                    int s = n + 1;
                    int e = 0;
                    for (int c = 1; c <= left; c++)
                    {

                        if (board[_r][c] == 0)
                        {

                            if (board[_r - 1][c] == 0 && arr[n + 1 - c] != arr[_r]) continue;
                        }
                        else if (board[_r][c] != arr[_r]) return true;

                        s = c;
                        break;
                    }

                    for (int c = n; c >= right; c--)
                    {

                        if (board[_r][c] == 0)
                        {

                            if (board[_r - 1][c] == 0 && arr[n + 1 - c] != arr[n + 1 - _r]) continue;
                        }
                        else if (board[_r][c] != arr[n + 1 - _r]) return true;

                        e = c;
                        break;
                    }

                    if (e < s) return true;

                    right = s;
                    left = e;

                    if (!use[_r])
                    {

                        use[_r] = true;
                        Draw(_r, s, arr[_r]);
                    }

                    if (!use[n + 1 - _r])
                    {

                        use[n + 1 - _r] = true;
                        Draw(_r, e, arr[n + 1 - _r]);
                    }

                    for (int i = s + 1; i < e; i++)
                    {

                        if (use[i]) continue;
                        use[i] = true;
                        Draw(_r, i, arr[n + 1 - i]);
                    }

                    return false;
                }

                void Draw(int _r, int _c, int _val)
                {

                    board[_r][_c] = _val;
                    board[n + 1 - _c][_r] = _val;
                    board[n + 1 - _r][n + 1 - _c] = _val;
                    board[_c][n + 1 - _r] = _val;
                }
            }

            void InitBoard()
            {

                for (int r = 1; r < n + 2; r++)
                {

                    use[r] = false;
                    for (int c = 1; c < n + 2; c++)
                    {

                        board[r][c] = 0;
                    }
                }
            }
            
            bool Input()
            {

                n = ReadInt();
                if (n == 0) return false;

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }
                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

                board = new int[MAX + 2][];
                for (int r = 0; r < board.Length; r++)
                {

                    board[r] = new int[MAX + 2];
                }

                arr = new int[MAX + 2];
                use = new bool[MAX + 2];
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
                    if (c == ' ' || c == '\n' || c == -1) return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
#else

            int MAX = 50;
            string YES = "Yes\n";
            string NO = "No\n";

            StreamReader sr;
            StreamWriter sw;

            int[][] board;

            int n;
            int[] arr;
            bool[] use;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    InitBoard();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {


                if (FillBoard())
                {

                    sw.Write(YES);

                    for (int r = 1; r <= n; r++)
                    {

                        for (int c = 1; c <= n; c++)
                        {

                            sw.Write($"{board[r][c]} ");
                        }

                        sw.Write('\n');
                    }
                }
                else sw.Write(NO);

            }

            bool FillBoard()
            {

                int left = n, right = 1;
                for (int i = 1; i <= n; i++)
                {

                    int j = n + 1 - i;
                    if (j < i) break;
                    if (Chk(i)) return false;
                }

                return true;

                bool Chk(int _r)
                {

                    int s = n + 1;
                    int e = 0;
                    for (int c = 1; c <= left; c++)
                    {

                        if (board[_r][c] == 0)
                        {

                            if (board[_r - 1][c] == 0 && arr[n + 1 - c] != arr[_r]) continue;
                        }
                        else if (board[_r][c] != arr[_r]) return true;

                        s = c;
                        break;
                    }

                    for (int c = n; c >= right; c--)
                    {

                        if (board[_r][c] == 0)
                        {

                            if (board[_r - 1][c] == 0 && arr[n + 1 - c] != arr[n + 1 - _r]) continue;
                        }
                        else if (board[_r][c] != arr[n + 1 - _r]) return true;

                        e = c;
                        break;
                    }

                    if (e < s) return true;

                    right = s;
                    left = e;

                    if (!use[_r])
                    {

                        use[_r] = true;
                        Draw(_r, s, arr[_r]);
                    }

                    if (!use[n + 1 - _r])
                    {

                        use[n + 1 - _r] = true;
                        Draw(_r, e, arr[n + 1 - _r]);
                    }

                    for (int i = s + 1; i < e; i++)
                    {

                        if (use[i]) continue;
                        use[i] = true;
                        Draw(_r, i, arr[n + 1 - i]);
                    }

                    return false;
                }

                void Draw(int _r, int _c, int _val)
                {

                    board[_r][_c] = _val;
                    board[n + 1 - _c][_r] = _val;
                    board[n + 1 - _r][n + 1 - _c] = _val;
                    board[_c][n + 1 - _r] = _val;
                }
            }

            void InitBoard()
            {

                for (int r = 1; r < n + 2; r++)
                {

                    use[r] = false;
                    for (int c = 1; c < n + 2; c++)
                    {

                        board[r][c] = 0;
                    }
                }
            }

            bool Input()
            {

                n = ReadInt();
                if (n == 0) return false;

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }
                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

                board = new int[MAX + 2][];
                for (int r = 0; r < board.Length; r++)
                {

                    board[r] = new int[MAX + 2];
                }

                arr = new int[MAX + 2];
                use = new bool[MAX + 2];
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
                    if (c == ' ' || c == '\n' || c == -1) return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
#endif
        }
    }

#if other
// #include<bits/stdc++.h>
// #include<array>
// #define ll long long

using namespace std;

int ans[55][55];

int main() {
	ios::sync_with_stdio(false), cin.tie(NULL), cout.tie(NULL);

	while (1) {
		int n; cin >> n;
		if (!n)return 0;
		
		vector<int>v(n);
		for (int i = 0; i < n; i++)cin >> v[i];

		for (int i = 0; i < n; i++)for (int j = 0; j < n; j++)ans[i][j] = 0;

		if (v[0] == v[n - 1]) {
			cout << "Yes\n";
			for (int i = 0; i < n; i++) {
				ans[n-1][i] = v[i];
				ans[i][0] = v[i];
				ans[0][n - 1 - i] = v[i];
				ans[n - 1 - i][n - 1] = v[i];
			}

			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					cout << ans[i][j] << " ";
				}cout << "\n";
			}
		}
		else {
			int sf = -1, sl = -1;
			for (int i = 0; i < n - 1; i++) {
				if (v[n - 1] == v[i]) {
					sl = i;
					break;
				}
			}

			for (int i = n - 1; i > 0; i--) {
				if (v[0] == v[i]) {
					sf = i;
					break;
				}
			}
			
			if (sf != -1 && sl != -1 && sl < sf) {
				cout << "Yes\n";

				for (int i = sl; i <=sf ; i++) {
					ans[n - 1][i] = v[i];
					ans[i][0] = v[i];
					ans[0][n - 1 - i] = v[i];
					ans[n - 1 - i][n - 1] = v[i];
				}

				for (int i = sf + 1; i < n - 1; i++) {
					ans[n - 1-sl][i] = v[i];
					ans[i][sl] = v[i];
					ans[sl][n - 1 - i] = v[i];
					ans[n - 1 - i][n - 1-sl] = v[i];
				}

				for (int i = 1; i < sl; i++) {
					ans[sf][i] = v[i];
					ans[i][n-1-sf] = v[i];
					ans[n-1-sf][n - 1 - i] = v[i];
					ans[n - 1 - i][sf] = v[i];
				}
				for (int i = 0; i < n; i++) {
					for (int j = 0; j < n; j++) {
						cout << ans[i][j] << " ";
					}cout << "\n";
				}

			}
			else {
				cout << "No\n";
			}

		}

	}

		

	return 0;
}

#endif
}
