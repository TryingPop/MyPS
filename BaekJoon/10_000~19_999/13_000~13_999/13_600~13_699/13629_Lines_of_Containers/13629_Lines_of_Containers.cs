using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 21
이름 : 배성훈
내용 : Line of Containers
    문제번호 : 13629번

    수학, 정렬, 순열 사이클 분할
    최소 이동횟수 부분을 캐치 못해 한 번 틀렸다
    아이디어는 다음과 같다
    row 이동과 col 이동으로 보드를 바꿀 시 행과 열은 서로 독립적임을 알 수 있다
    즉, col 변환한다 해서 row에 있는 숫자들의 집합이 변하지 않는다!
    마찬가지로 row 변환한다 해서 col에 있는 숫자들의 집합이 변하지 않는다

    이말을 3 x 3 사이즈로 보면 다음과 같다
        5 ? ?
        2 ? ?
        8 ? ?
    인 경우 행만 놓고 보면
    5 옆에 있는 ? ? 는 4 또는 6이어야 한다!
    2 옆에 있는 ? ? 는 1 또는 3이어야 한다!
    8 옆에 있는 ? ? 는 7 또는 9이어야 한다!

    만약 여기서 5 옆에 1 ~ 3 또는 7 ~ 9 사이의 숫자가 나오면
    어떻게 이동해도 정방향을 맞출 수 없다!

    열 역시 마찬가지로 보면 된다
    행끼리 위치가 같은지 열끼리 위치가 같은지 
    이 둘만 검증했다

    그리고 최소이동은
    순열의 사이클의 개수를 찾고
    각 사이클의 원소 - 1개를 누적해서 찾으니 이상없이 통과한다
    다만 처음에 그냥 row 별로 맞추면 되는거 아니야 ? 하다가 최소가 보장이 안되어 1번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1068
    {

        static void Main1068(string[] args)
        {

            int[][] board;
            int row, col;

            Solve();
            void Solve()
            {

                Input();

                if (ChkInvalidBoard())
                {

                    Console.Write('*');
                    return;
                }

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;

                bool[] visitR = new bool[row];
                bool[] visitC = new bool[col];

                for (int r = 0; r < row; r++)
                {

                    int curR = GetR(board[r][0]);
                    if (visitR[curR]) continue;
                    visitR[curR] = true;
                    ret += DFSR(curR);
                }

                for (int c = 0; c < col; c++)
                {

                    int curC = GetC(board[0][c]);
                    if (visitC[curC]) continue;
                    visitC[curC] = true;
                    ret += DFSC(curC);
                }

                Console.Write(ret);

                int DFSC(int _cur)
                {

                    int nextC = GetC(board[0][_cur]);
                    if (visitC[nextC]) return 0;
                    visitC[nextC] = true;
                    int ret = 1 + DFSC(nextC);
                    return ret;
                }

                int DFSR(int _cur)
                {

                    int nextR = GetR(board[_cur][0]);
                    if (visitR[nextR]) return 0;
                    visitR[nextR] = true;
                    int ret = 1 + DFSR(nextR);
                    return ret;
                }
            }

            int GetR(int _val)
            {

                return _val / col;
            }

            int GetC(int _val)
            {

                return _val % col;
            }

            bool ChkInvalidBoard()
            {

                for (int r = 0; r < row; r++)
                {

                    int chk = GetR(board[r][0]);

                    for (int c = 1; c < col; c++)
                    {

                        if (chk != GetR(board[r][c])) return true;
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    int chk = GetC(board[0][c]);
                    
                    for (int r = 1; r < row; r++)
                    {

                        if (chk != GetC(board[r][c])) return true;
                    }
                }

                return false;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt() - 1;
                    }
                }

                sr.Close();

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
    }

#if other
// #include <iostream>
// #include <algorithm>
// #include <set>
// #include <map>
// #include <vector>
// #include <queue>
// #include <list>
// #include <cstdio>
// #include <cstring>
// #include <iomanip>
// #include <cmath>
// #include <bitset>
// #include <unordered_set>
// #include <climits>
// #include <complex>
using namespace std;
typedef long long ll;

const int MAX_N=300005;
const int INF=1e9;
const ll MOD=1000000007;

int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n,m,arr[305][305],p=1; //p는 가능여부
    cin>>n>>m;
    for(int i=0;i<n;i++){
        for(int j=0;j<m;j++){
            cin>>arr[i][j];
            arr[i][j]--;
        }
    }
    // 행마다 확인
    int row[305], col[305], visited[305];
    for(int c=0;c<m;c++){row[c]=arr[0][c]%m;}
    // 열마다 확인
    for(int r=0;r<n;r++){col[r]=arr[r][0]/m;}
    for(int r=0;r<n;r++){
        for(int c=0;c<m;c++){
            if(arr[r][c]%m != row[c]){p=0;}
            if(arr[r][c]/m != col[r]){p=0;}
        }
    }
    if(p==0){cout<<"*\n";}
    else{
        fill(visited, visited+305, 0);
        // row 정렬
        int cycle=0;
        for(int s=0;s<m;s++){
            if(visited[s]){continue;}
            cycle++; visited[s]=1;
            for(int cur=row[s];cur!=s;cur=row[cur]){visited[cur]=1;}
        }
        fill(visited, visited+305, 0);
        for(int s=0;s<n;s++){
            if(visited[s]){continue;}
            cycle++; visited[s]=1;
            for(int cur=col[s];cur!=s;cur=col[cur]){visited[cur]=1;}
        }
        cout<<n+m-cycle<<"\n";
    }

    return 0;
}
#endif
}
