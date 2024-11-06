using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 30
이름 : 배성훈
내용 : 전구 끄기
    문제번호 : 14927번

    그리디, 브루트포스, 비트마스킹 문제다
    처음에는 첫 줄의 상태를 나타내는 2^18승 만큼 할당해야하나 싶어
    그리디로 방향을 잡았다

    그런데 처음에는 Max로 잘못해 한 번 틀렸고
    다음으로 로직은 맞는데 틀려서
    혹시나 하는 마음에 공백 처리를 해주니 통과했다
    다른 사람 제출 현황을 보니 format 에러가 있었다

    아이디어는 다음과 같다
    위에 줄에 껐다 켰다 하면서 가능한 상태들을 찾는다
    그리고 각 경우에 대해 두번째 줄부터 위에 전구가 커지면 꺼주는 형식으로 진행한다
    그리고 마지막 줄에 모두 꺼졌다면 해당 횟수를 기록하고
    기록된 횟수 중 최소값이 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1007
    {

        static void Main1007(string[] args)
        {

            int IMPO = 100_000;
            StreamReader sr;

            int n;
            int[][] bulb, chk;

            bool[] use;
            int[] order;

            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                order = new int[n];
                use = new bool[n];

                dirR = new int[5] { 0, -1, 0, 1, 0 };
                dirC = new int[5] { 0, 0, -1, 0, 1 };

                int ret = DFS();
                if (ret == IMPO) ret = -1;
                Console.Write(ret);
            }

            int DFS(int _depth = 0, int _s = 0)
            {

                if (_depth == n) 
                {

                    Copy();
                    return Calc(_s); 
                }

                int ret = DFS(_depth + 1, _s);

                OnOff(0, _depth, bulb);
                ret = Math.Min(ret, DFS(_depth + 1, _s + 1));
                OnOff(0, _depth, bulb);
                return ret;
            }

            int Calc(int _s)
            {

                int ret = _s;

                for (int r = 1; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (chk[r - 1][c] == 0) continue;
                        OnOff(r, c, chk);
                        ret++;
                    }
                }

                for (int c = 0; c < n; c++)
                {

                    if (chk[n - 1][c] == 1) return IMPO;
                }

                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= n || _c >= n;
            }

            void OnOff(int _r, int _c, int[][] _bulb)
            {

                for (int i = 0; i < 5; i++)
                {

                    int nR = _r + dirR[i];
                    int nC = _c + dirC[i];

                    if (ChkInvalidPos(nR, nC)) continue;
                    _bulb[nR][nC] ^= 1;
                }
            }

            void Copy()
            {

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        chk[r][c] = bulb[r][c];
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                bulb = new int[n][];
                chk = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    bulb[r] = new int[n];
                    chk[r] = new int[n];

                    for (int c = 0; c < n; c++)
                    {

                        bulb[r][c] = ReadInt();
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryInt()) { }

                return ret;

                bool TryInt()
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
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Data;


namespace Algorithm
{
    class Program
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();
            Random random = new Random();


            int n = int.Parse(sr.ReadLine());
            int[,] jungu = new int[n+2,n+2];
            
            for(int i = 1; i <=n; i++)
            {
                List<int> input = Array.ConvertAll(sr.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries), int.Parse).ToList();
                for(int j = 1; j<=n;j++)
                {
                    jungu[i,j] = input[j-1];
                }
            }
            int min = 10000;

            for(int ii = 0; ii < Math.Pow(2,n); ii++)
            {
                int[,] jungu2 = (int[,])jungu.Clone();
                int cnt = 0;
                bool isTrue = true;
                string s = new string(Convert.ToString(ii,2).Reverse().ToArray());
                for(int jj = 0; jj < s.Length; jj++)
                {
                    if(s[jj] == '1')
                    {
                        Trig(jungu2,1,jj+1);
                        cnt++;
                    }
                }
                for(int i = 2; i <= n; i++)
                {
                    for(int j = 1; j <=n; j++)
                    {
                        if(jungu2[i-1,j] == 1)
                        {
                            Trig(jungu2, i, j);
                            cnt++;
                        }
                    }
                }
                for(int j = 1; j<= n; j++)
                {
                    if(jungu2[n,j] == 1)
                    {
                        isTrue = false;
                        break;
                    }
                }

                if(isTrue)
                {
                    min = Math.Min(min, cnt);
                }
            }

            if(min == 10000)
            {
                sb.Append(-1);
            }
            else
                sb.Append(min);
            
            sw.WriteLine(sb);
            sr.Close();
            sw.Close(); 
        }

        static void Trig(int[,] jungu, in int x, in int y)
        {
            int[] dx = new int[]{-1,0,0,0,1};
            int[] dy = new int[]{0,-1,0,1,0};
            for(int i = 0; i < 5; i++)
            {
                jungu[x+dx[i],y+dy[i]] ^= 1;
            }
        }
    }
}
#elif other2
// #include <cstdio>
// #include <algorithm>
// #include <cstring>
using namespace std;

int n;
int a[20];
int b[20];
int main(){
    scanf("%d",&n);
    for(int i=1;i<=n;i++){
        int x = 0;
        for(int j=1;j<=n;j++){
            int t;
            scanf("%d",&t);
            x = (x|t)<<1;
        }
        a[i] = x;
    }
    int ans = n*n+1;
    for(int bs=0;bs<(1<<n);bs++){
        memcpy(b,a,sizeof(a));
        int cnt = __builtin_popcount(bs);
        b[1] ^= bs ^ (bs<<1) ^ (bs<<2);
        b[2] ^= bs<<1;
        for(int i=1;i<n;i++){
            int t = b[i] & ((2<<n)-2);
            cnt += __builtin_popcount(t);
            b[i+1] ^= (t>>1) ^ t ^ (t<<1);
            b[i+2] ^= t;
        }
        if(!(b[n] & ((2<<n)-2)))ans = min(ans, cnt);
    }
    printf("%d\n", ans == n*n+1 ? -1 : ans);
}

#endif
}
