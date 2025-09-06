using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 30
이름 : 배성훈
내용 : Kamenčići
    문제번호 : 23402번

    dp, 게임이론 문제다.
    dp[l][r][a][b] = val를
    왼쪽에 선택할 카드가 l, 
    오른쪽에 선택할 카드가 r,
    그리고 선턴이 선택한 C의 개수가 a,
    후턴이 선택한 C의 개수가 b이다.
    그리고 val는 승리 여부를 나타낸다.
    이 경우 n^2 x k^2이고 n, k = 100이상이 될 수 있으므로 메모리를 많이 먹는다.

    여기서 l, r이 남은 경우 선택한 C의 개수는 같으므로
    tot[l][r]에 전체 C의 개수를 저장하면 n^2 x k + n^2으로 줄일 수 있다.

    그래서 dp[i][j][k] = val를 
    왼쪽이 i, 오른쪽이 j, 현재 턴을 하는 사람이 선택한 C의 개수를 k로 dp를 설정했다.

    선택을 하는데 C가 k개가되는 경우면 패배이므로 해당 경우로 선택하지 않아야 한다.
    그래서 어느 경우도 선택하지 않는다는 의미로 패배를 초기값으로 한다.

    반면 상대가 패배로 판명하는건 어떤길로 가도 C가 k개가 되는 길이므로
    1개라도 있으면 해당 경우로 전달하면 현재 턴을 잡은 사람이 이기게 된다.
    이렇게 dfs 탐색을 해서 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1851
    {

        static void Main1851(string[] args)
        {

            int n, k;
            string str;
            int[][][] arr;  // 승리 여부
            int[][] tot;    // 해당 구간이 남을 때 선택된 C의 전체 개수

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                int[] addL = { 1, 0 };
                int[] addR = { 0, -1 };

                int ret = DFS(0, n - 1);
                Console.Write(ret == 1 ? "DA" : "NE");

                // 0인 경우 : 패배
                // 1인 경우 : 승리
                int DFS(int _l, int _r, int _k = 0)
                {

                    ref int ret = ref arr[_l][_r][_k];
                    if (ret != -1) return ret;

                    // 패배하는 경로는 건너뛰므로 패배한다고 본다.
                    ret = 0;
                    for (int i = 0; i < 2; i++)
                    {

                        // 먼저 왼쪽
                        int nL = _l + addL[i];
                        int nR = _r + addR[i];

                        int add = Chk(_l, _r, i) ? 1 : 0;
                        tot[nL][nR] = tot[_l][_r] + add;

                        // k개 되면 지는 것이므로 해당 경로로 안한다.
                        int chk = _k + add;
                        if (chk == k) continue;
                        int nK = tot[nL][nR] - chk;

                        chk = DFS(nL, nR, nK);
                        // 상대가 지는 경로라면 내가 이긴다!
                        if (chk == 0) return ret = 1;
                    }

                    return ret;

                    bool Chk(int _l, int _r, int _i)
                        => (_i == 0 ? str[_l] : str[_r]) == 'C';
                }
            }

            void SetArr()
            {

                arr = new int[n][][];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = new int[n][];
                    for (int j = 0; j < n; j++)
                    {

                        arr[i][j] = new int[k + 1];
                        Array.Fill(arr[i][j], -1);
                    }
                }

                tot = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    tot[i] = new int[n];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);
                str = sr.ReadLine();
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int main(){
	ios_base::sync_with_stdio(false), cin.tie(nullptr);
	int n, k;
	cin >> n >> k;
	string s;
	cin >> s;
	vector<int> psum(n+1, 0);
	for(int i = 0; i < n; i++){
		psum[i+1] = psum[i] + int(s[i] == 'C');
	}
	vector<vector<int> > dp(n+1, vector<int>(n+1, 0));
	for(int j = 0; j <= n; j++){
		for(int i = j; i >= 0; i--){
			int outside = psum[n] - psum[j] + psum[i] - psum[0];
			if(outside >= 2*k-1){
				dp[i][j] = k;
				continue;
			}
			int v = outside - min(dp[i+1][j], dp[i][j-1]) + 1;
			v = min(max(v, 0), k);
			dp[i][j] = v;
		}
	}
	cout << ((dp[0][n] == 0) ? "NE" : "DA") << '\n';
}
#endif
}
