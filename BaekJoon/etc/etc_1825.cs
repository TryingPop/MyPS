using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 15
이름 : 배성훈
내용 : 바이너리 스도쿠
    문제번호 : 5923번

    비트마스킹, dp 문제다.
    첫 행부터 최소가 토글연산을한다.
    그러면 마지막 행까지 실행했을 때 최소가 됨이 그리디로 보장된다.
    
*/

namespace BaekJoon.etc
{
    internal class etc_1825
    {

        static void Main1825(string[] args)
        {

            int INF = 123;
            int BIT_3 = 1 << 3;
            int BIT_9 = 1 << 9;

            // board는 현재 비트 상태
            // group은 3 x 3의 변화 상태
            // cnt는 해당 비트에 1의 개수를 나타낸다.
            int[] board, group, cnt;
            
            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                // dp[i][j] : 현재 i, j 상태로 만드는 최솟값!
                // 여기서 i는 행의 3개씩 묶은 상태이다. 즉, 3 x 3 확인용이다.
                // j는 열의 상태다.
                int[][] dp = new int[BIT_3][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[BIT_9];
                    Array.Fill(dp[i], INF);
                }

                // 다음 dp
                int[][] next = new int[BIT_3][];
                for (int i = 0; i < next.Length; i++)
                {

                    next[i] = new int[BIT_9];
                    Array.Fill(next[i], INF);
                }
                
                dp[0][0] = 0;
                
                for (int r = 1; r <= 9; r++)
                {

                    SetNext(r - 1);

                    Swap(r % 3 == 0);
                }

                Console.Write(dp[0][0]);

                void Swap(bool _flag = false)
                {

                    for (int i = 0; i < BIT_3; i++)
                    {

                        for (int j = 0; j < BIT_9; j++)
                        {

                            int chk = next[i][j];
                            next[i][j] = INF;
                            // 이번 열이 홀수인 경우 다음 탐색에 사용하지 않는다.
                            if ((cnt[j] & 1) == 1) chk = INF;
                            // 3 x 3 단위이므로 3행이 끝났을 때 3 x 3이 짝수가 되어야 한다.
                            else if (_flag && i != 0) chk = INF;

                            dp[i][j] = chk;
                        }
                    }
                }

                void SetNext(int _r, bool flag = false)
                {

                    for (int g = 0; g < BIT_3; g++)
                    {

                        for (int s = 0; s < BIT_9; s++)
                        {

                            // g, s가 탐색할 필요가 없으면 탐색 X
                            if (dp[g][s] == INF) continue;
                            Find(g, s, board[_r]);
                        }
                    }
                }

                void Find(int _curGroup, int _curState, int _board)
                {

                    // 변환하는 자리
                    for (int change = 0; change < BIT_9; change++)
                    {

                        // 현재 보드 상태는 board이다.
                        // 그래서 change 비트로 바꾼다면 변경후 비트 상태가 된다.
                        int state = change ^ _board;

                        // 다음 그룹 값
                        int nextGroup = group[state] ^ _curGroup;
                        // 다음 열 상태는 현재 열에서 state를 연산하는 것과 같다.
                        int nextState = state ^ _curState;

                        // 최소값 담기
                        next[nextGroup][nextState] = Math.Min(next[nextGroup][nextState], 
                            dp[_curGroup][_curState] + cnt[change]);
                    }
                }
            }

            void SetArr()
            {

                cnt = new int[BIT_9];
                group = new int[BIT_9];

                Array.Fill(cnt, -1);
                cnt[0] = 0;
                for (int i = 0; i < BIT_9; i++)
                {

                    for (int j = 0; j < 9; j++)
                    {

                        if ((i & (1 << j)) != 0) continue;
                        cnt[i | (1 << j)] = cnt[i] + 1;
                    }

                    for (int j = 0, k = i; j < 3; j++, k >>= 3)
                    {

                        int chk = k & 0b_111;
                        // // 홀수인 경우
                        if (chk is 0b_001 or 0b_010 or 0b_100 or 0b_111) group[i] |= 1 << j;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                board = new int[9];

                for (int i = 0; i < 9; i++)
                {

                    string str = sr.ReadLine();

                    for (int j = 0; j < 9; j++)
                    {

                        board[i] |= (str[j] - '0') << j;
                    }
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;

typedef long long ll;
typedef pair<int,int> pii;


int n;
char A[10][10];
int main() {
    ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
    for(int i=0;i<9;i++) {
        cin>>A[i];
    }

    int r[3] = {0,0,0};
    int c[3] = {0,0,0};
    int g[9] = {0,};
    for(int j=0;j<9;j++) {
        int cnt=0;
        for(int i=0;i<9;i++) cnt+=A[i][j]-'0';
        if(cnt%2) c[j/3]++;
    }
    for(int i=0;i<9;i++) {
        int cnt=0;
        for(int j=0;j<9;j++) cnt+=A[i][j]-'0';
        if(cnt%2) r[i/3]++;
    }
    for(int k=0;k<9;k++) {
        int x=k/3*3, y=k%3*3;
        int cnt=0;
        for(int i=x;i<x+3;i++) {
            for(int j=y;j<y+3;j++) {
                cnt+=A[i][j]-'0';
            }
        }
        if(cnt%2) g[k]++;
        //cout<<g[k]<<'\n';
    }
    //cout<<r[0]<<r[1]<<r[2]<<c[0]<<c[1]<<c[2]<<'\n';
    int ans = 0;
    for(int i=0;i<9;i++) {
        ans += g[i];
        int x=i/3, y=i%3;
        if(g[i]) {
            if(r[x]) r[x]--;
            else r[x]++;
            if(c[y]) c[y]--;
            else c[y]++;
        }
    }
    //cout<<r[0]<<r[1]<<r[2]<<c[0]<<c[1]<<c[2]<<'\n';
    cout<<ans+max(r[0]+r[1]+r[2],c[0]+c[1]+c[2]);
    return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
int p8[1<<9];
int p3[1 << 3];
int bo[9][9];
int ro[9];
int dp[9][1<<9][1<<3];

void inits(){
    for(int i = 0; i < (1 << 9); i++){
        p8[i] = p8[i/2] + i%2;
        
    }
    for(int i = 0; i < (1 <<3); i++){
        p3[i] = p3[i/2] + i%2;
        p3[i] %= 2;
    }
}

int main(){
    inits();
    string S;
    for(int i = 0; i < 9; i++){
        cin >> S;
        for(int j = 0; j < 9; j++){
            bo[i][j] = S[j] - '0';
            ro[i] += bo[i][j] * (1 << j);
        }
        //cout << ro[i] << '\n';
        
    }
    //cout << sizeof(dp) << '\n';
    memset(dp, 0x01, sizeof(dp));

    //cout << dp[0][111][3] << '\n';

    //cout << p3[7] << p8[7];
   // cout << (7|3) << '\n';
    for(int i = 0; i < 9; i++){
        for(int ct = 0; ct < (1 << 9); ct++){
            if(i == 0){
                int rs = ct^ro[i];
                int rt0 =  rs% 8;
                int rt1 = (rs /8) %8;
                int rt2 = (rs /64) % 8;
                int sg = p3[rt2] * 4 + p3[rt1] * 2 + p3[rt0];
                dp[0][rs][sg] = p8[ct];
                continue;
            }
            for(int rt = 0; rt < (1 << 9); rt++){
                int rs = rt ^ ro[i];
                if(p8[rs]%2 == 0){
                    int rt0 = (rs) % 8;
                    int rt1 = (rs /8) %8;
                    int rt2 = (rs /64) % 8;
                    int sg = p3[rt2] * 4 + p3[rt1] * 2 + p3[rt0];
                    
                    if(i%3 == 0){
                        dp[i][ct^rs][sg] = min(dp[i][ct^rs][sg], dp[i-1][ct][0] + p8[rt]);
                    }
                    else{
                        for(int ss = 0; ss < (1 << 3); ss++){
                            dp[i][ct^rs][sg ^ ss] = min(dp[i-1][ct][ss] + p8[rt], dp[i][ct^rs][sg ^ ss]);
                        }
                    }
                }
            }
        }
    }
    
    cout << dp[8][0][0];

}
#endif
}
