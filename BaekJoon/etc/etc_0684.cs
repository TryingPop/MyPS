using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. -
이름 : 배성훈
내용 : 습격자 초라기
    문제번호 : 1006번

    dp 문제다
    인덱스 문제로 2번 틀렸다 -> 1개 있는 경우 [0][0]과 [1][0]을 비교해야하는데, [0][0]과 [0][1]을 해서 1번,
    그리고 dp를 설정함에, board는 0 ~ n-1번까지인데, dp는 0 ~ n까지로 오는 인덱스 차이..

    그리고 처음과 끝점 이어진 부분 고려하는데 4번 더 틀렸다

    아이디어는 다음과 같다
    먼저 원형을 선형으로 펴서 생각했다
    처음부터 끝까지 탐색할 때, 정답인 경우가 2개로 나뉜다

    1. 처음과 끝이 끊어진 경우 
        즉, 처음과 끝을 2개로 묶은 경우가 전혀 없는 경우
            3   5
            5   5   5
            5   5   5

    2. 처음과 끝이 이어진 경우 
        위에만 이어진 경우
            3   5
            2   5   3
            5   5   5

        밑에만 이어진 경우
            3   5
            5   5   5
            2   5   3

        위 아래 둘다 이어진 경우
            2   5   3
            4   5   1

        -> 여기를 효율적으로 묶을 수 있는지 찾아봤는데 검색에서 나온건 
            전부 하나씩 일일히 탐색해서 똑같이 일일히 탐색했다

    이제 dp설정을 해야하는데
    두부장수 ? 문제처럼 맵을 선형으로 펴서 최대한 길게 세팅하는 방법을 생각해봤으나,
    매번 아래, 오른쪽과 이어졌는지 확인해야하고 10_000까지 들어와서 3^n으로 시간초과, 메모리 초과 날거 같이 보여 시도 안했다

    그래서 한칸씩 진행하게 상황을 나눠서 최소 팀수를 값으로 갖게 설정했다
    한 부대당 최대 2칸씩만 채울 수 있으므로 위에 + 1, 아래 + 1, 위 아래가 같은 경우
    이렇게 3개를 나눠서 했다

    처음에는 dp[2][i]을 i번째에 위 아래 모두 채운 경우로 표현하려고 했으나,
    =로 처음 채우는 경우를 표현하기 힘들어 그냥 비운 경우로 설정했다
        3   5
        2   3   5   
        4   1   5   
        (해당 경우다!)

    그래서 dp[2][i]를 i - 1번째까지 위 아래 모두 채운 경우로 설정했고,
                   i-1  i   
        O   O  ...  O   X   X   ... X
        O   O  ...  O   X   X   ... X

    dp[0][i]는 위에 i - 1번째까지 모두 채우고 i번째 위에만 채우고 아래는 안채운경우
                   i-1  i   
        O   O  ...  O   O   X   ... X
        O   O  ...  O   X   X   ... X

    그리고 dp[1][i]는 i - 1 번째까지 모두 채우고 i번째 아래만 채우고 위에는 안채운 경우
                   i-1  i   
        O   O  ...  O   X   X   ... X
        O   O  ...  O   O   X   ... X

    그리고 dp[0][i], dp[1][i], dp[2][i]의 역할은 다음과 같고, 들어가는 값은 채우는데 필요한 최소 팀수로 했다

    이렇게 설정하면 다음과 같은 점화식을 얻을 수 있다
    dp[2][i]에서 1개 이어 붙인 경우
    dp[0][i] = dp[2][i] + 1
    만약 board[0][i - 1] + board[0][i] <= w인 경우 (이어 붙일 수 있는 경우)
    dp[0][i] = Math.Min(dp[2][i] + 1, dp[1][i - 1] + 1)
    
    dp[1]도 비슷하다
    dp[1][i] = dp[2][i] + 1
    만약 board[1][i - 1] + board[1][i] <= w인 경우 (이어 붙일 수 있는 경우)
    dp[1][i] = Math.Min(dp[2][i] + 1, dp[0][i - 1] + 1)
    
    dp[2]인 경우는
    dp[2][i] = Math.Min(dp[0][i - 1] + 1, dp[1][i - 1] + 1)
    만약 세로로 이어 붙이는게 가능하다면
    board[0][i - 1] + board[1][i - 1] <= w
    dp[2][i] = Math.Min(dp[0][i - 1] + 1, dp[1][i - 1] + 1, dp[2][i - 1] + 1)

    그리고 =로 이어붙이는게 적은 경우
    board[1][i - 2] + board[1][i - 1] <= w && board[0][i - 2] + board[0][i - 1] <= w
    dp[2][i] = Math.Min(dp[0][i - 1] + 1, dp[1][i - 1] + 1, dp[2][i - 1] + 1, dp[2][i - 2] + 2)

    그러면 끝이 끊어진 경우
    dp[0][0]은 처음에 한 칸 채워야하니 1로 시작해야하고
    dp[1][0]은 마찬가지로 1, dp[2][0]은 0로 시작해서 dp를 채우면 된다
    그러면, 끊어진 경우의 최적해는 dp[2][n]에 값이 된다

    이제 처음과 끝이 이어진 경우
    위 아래 둘 다 이어진 경우면 처음과 끝을 빼고 끊어진 상태처럼 탐색하면 된다
    즉, 시작지점 인덱스 1부터, 끝지점 인덱스 n - 2번까지만 탐색하고
    그리고 여기에 + 2를 하면 최적해가 된다
    제외했으므로 dp[0][1] = 1, dp[1][1] = 1, dp[2][1] = 0으로 놓고 진행하면 된다

    제외한 방법이 아닌 탐색 방법을 이용한다면
    dp[2][0] = 2 <- 앞에 n - 1에서 이어져 있으니 2개다!
    그리고 앞에 2개가 배치되어 있으니 dp[0][1] = 3, dp[1][1] = 3,
    dp[2][1] = 2로 시작하면 된다 (모두 + 없이 처리한다면 속도가 20ms 더 느리다!)

    위에만 이어진 경우 이는 이어질 수 있는지부터 확인해야하나 여기서는 된다고 가정하고 보자!
    해당 경우 dp[0][0]은 이어져 있으니 처음에 채워져 있다!
    여기는 이어져 있으니, 1칸 이동해서 계산하면 된다
    위에가 이어져 있으니 앞에 1개가 배치되어져 있다
    dp[0][1] = 2, dp[1][1]은 밑을 -로 이어붙일 수 있으면 2개, 없으면 3개다 즉
    dp[1][1] = board[1][0] + board[1][1] <= w ? 2 : 3
    그리고 dp[2][1] 은 앞에 2개가 있다 
    n - 1에 부대가 배치되었기 때문에 dp[2][0] = 1로 시작해야한다(안하면 틀린다!)
    그런데, 모두 1 이상으로 시작해서 1씩 줄여 시작해도 결과에 이상이 없다!(주석 안치 풀이가 된다)
    
    마지막으로 아래만 이어진 경우는 위에만 이어진 경우에서 dp앞의 인덱스 0, 1을 바꿔서 생각하면 된다

    이렇게 제출하니 124ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0684
    {

        static void Main684(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[][] dp;
            int[][] board;

            int n, w;

            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    int ret = GetRet();
                    sw.Write($"{ret}\n");
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                n = ReadInt();
                w = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    board[0][i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    board[1][i] = ReadInt();
                }
            }

            int GetRet()
            {

                if (n == 1) return board[0][0] + board[1][0] <= w ? 1 : 2;

                dp[0][0] = 1;
                dp[1][0] = 1;
                dp[2][0] = 0;

                SetDp(1);

                int ret = dp[2][n];

                bool chk0 = board[0][0] + board[0][n - 1] <= w;
                bool chk1 = board[1][0] + board[1][n - 1] <= w;
                bool chk2 = chk0 && chk1;

                // dp[2][0] = 1;
                if (chk0)
                {

                    dp[0][1] = 2;
                    dp[1][1] = board[1][0] + board[1][1] <= w ? 1 : 2;
                    dp[2][1] = 1;

                    // dp[0][1] = 3;
                    // dp[1][1] = board[1][0] + board[1][1] <= w ? 2 : 3;
                    // dp[2][1] = 2;

                    SetDp(2);
                    ret = Math.Min(ret, dp[1][n - 1] + 1);
                    // ret = Math.Min(ret, dp[1][n - 1]);
                }

                if (chk1)
                {

                    dp[0][1] = board[0][0] + board[0][1] <= w ? 1 : 2;
                    dp[1][1] = 2;
                    dp[2][1] = 1;

                    // dp[0][1] = board[0][0] + board[0][1] <= w ? 2 : 3;
                    // dp[1][1] = 3;
                    // dp[2][1] = 2;

                    SetDp(2);
                    ret = Math.Min(ret, dp[0][n - 1] + 1);
                    // ret = Math.Min(ret, dp[0][n - 1]);
                }

                // dp[2][0] = 2;
                if (chk2)
                {

                    dp[0][1] = 1;
                    dp[1][1] = 1;
                    dp[2][1] = 0;

                    // dp[0][1] = 3;
                    // dp[1][1] = 3;
                    // dp[2][1] = 2;

                    SetDp(2);
                    ret = Math.Min(ret, dp[2][n - 1] + 2);
                    // ret = Math.Min(ret, dp[2][n - 1]);
                }

                return ret;
            }

            void SetDp(int _s)
            {

                for (int i = _s; i <= n; i++)
                {

                    dp[2][i] = Math.Min(dp[0][i - 1] + 1, dp[1][i - 1] + 1);
                    if (board[0][i - 1] + board[1][i - 1] <= w) dp[2][i] = Math.Min(dp[2][i], dp[2][i - 1] + 1);
                    if (i > 1 && ChkTop(i - 2) && ChkBot(i - 2)) dp[2][i] = Math.Min(dp[2][i], dp[2][i - 2] + 2);

                    if (i < n && ChkTop(i - 1)) dp[0][i] = Math.Min(dp[2][i] + 1, dp[1][i - 1] + 1);
                    else dp[0][i] = dp[2][i] + 1;

                    if (i < n && ChkBot(i - 1)) dp[1][i] = Math.Min(dp[2][i] + 1, dp[0][i - 1] + 1);
                    else dp[1][i] = dp[2][i] + 1;
                }
            }

            bool ChkTop(int _idx)
            {

                return board[0][_idx] + board[0][_idx + 1] <= w;
            }

            bool ChkBot(int _idx)
            {

                return board[1][_idx] + board[1][_idx + 1] <= w;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                dp = new int[3][];
                for (int i = 0; i < 3; i++)
                {

                    dp[i] = new int[10_000 + 1];
                }

                board = new int[2][];
                for (int i = 0; i < 2; i++)
                {

                    board[i] = new int[10_000];
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var nw = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nw[0];
            var w = nw[1];

            var a = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var b = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var shiftA = a.Skip(1).Append(a.First()).ToArray();
            var shiftB = b.Skip(1).Append(b.First()).ToArray();

            var list = new List<int>();

            list.Add(Solve(a, b, w));
            list.Add(Solve(shiftA.Prepend(0).ToArray(), b.Append(0).ToArray(), w));
            list.Add(Solve(a.Append(0).ToArray(), shiftB.Prepend(0).ToArray(), w));
            list.Add(Solve(shiftA, shiftB, w));

            sw.WriteLine(list.Min());
        }
    }

    public static int Solve(int[] a, int[] b, int w)
    {
        // dp1[i]: i-1까지 다 처리, i는 위 처리
        var dp1 = new int[a.Length];

        // dp2[i]: i-1까지 다 처리, i는 아래 처리
        var dp2 = new int[a.Length];

        // dp3[i]: i까지 다 처리
        var dp3 = new int[a.Length];

        dp1[0] = a[0] != 0 ? 1 : 0;
        dp2[0] = b[0] != 0 ? 1 : 0;

        if (a[0] == 0)
            dp3[0] = dp2[0];
        else if (b[0] == 0)
            dp3[0] = dp1[0];
        else
        {
            if (a[0] + b[0] <= w)
                dp3[0] = Math.Min(dp1[0], dp2[0]);
            else
                dp3[0] = 1 + Math.Min(dp1[0], dp2[0]);
        }

        for (var idx = 1; idx < a.Length; idx++)
        {
            dp1[idx] = dp3[idx - 1] + 1;
            if (a[idx] + a[idx - 1] <= w)
                dp1[idx] = Math.Min(dp1[idx], dp2[idx - 1] + 1);

            dp2[idx] = dp3[idx - 1] + 1;
            if (b[idx] + b[idx - 1] <= w)
                dp2[idx] = Math.Min(dp2[idx], dp1[idx - 1] + 1);

            dp3[idx] = Math.Min(dp1[idx] + 1, dp2[idx] + 1);

            if (a[idx] + b[idx] <= w)
                dp3[idx] = Math.Min(dp3[idx], dp3[idx - 1] + 1);

            if (a[idx - 1] != 0 && b[idx - 1] != 0 && a[idx] + a[idx - 1] <= w && b[idx] + b[idx - 1] <= w)
                if (idx == 1)
                    dp3[idx] = 2;
                else
                    dp3[idx] = Math.Min(dp3[idx], dp3[idx - 2] + 2);
        }

        if (a.Last() == 0)
            return dp2.Last();
        if (b.Last() == 0)
            return dp1.Last();

        return dp3.Last();
    }
}

#elif other2
using System.Text;
namespace problems3679
{
    class Program
    {
        static int T,N,W;
        static int tmp01, tmp11, tmp0N, tmp1N;
        
        static readonly int inf = 200000000;
        static int[,] tag = new int[2,10010];
        static int[] dp_f, dp_g, dp_h;
        
        static StringBuilder sb = new StringBuilder();
        
        static void Main()
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            T = int.Parse(sr.ReadLine());
            for (int t=0; t<T; t++)
            {
                var input = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                N=input[0]; 
                W=input[1];

                for (int i=0; i<2; i++)
                {
                    var row = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                    for (int j=0; j<N; j++)
                    {
                        tag[i,j+1]=row[j];
                    }
                }
                if (N==1)
                {
                    if ((tag[0,1]+tag[1,1])<=W) sb.Append(1);
                    else sb.Append(2);
                    sb.Append('\n');
                    continue;
                }
                tmp01 = tag[0,1];
                tmp11 = tag[1,1];
                tmp0N = tag[0,N];
                tmp1N = tag[1,N];
                int caseA, caseB, caseC, caseD;
                caseA=caseB=caseC=caseD=inf;
                bool c1 = (tag[1,1]+tag[1,N])<=W;
                bool c2 = (tag[0,1]+tag[0,N])<=W;
                //CaseA
                if(true)
                {
                    Reset_dp();
                    dp_f[1] = ((tag[0,1]+tag[1,1])<=W)? 1:2;
                    dp_g[1] = 1;
                    dp_h[1] = 1;
                    caseA = Dp('f',N);
                }
                //print_dp();
                //caseB
                if (c1)
                {
                    Reset_dp();
                    tag[1,1]=tag[1,N]=inf;
                    dp_f[1] = 1;
                    dp_g[1] = 0;
                    dp_h[1] = 1;
                    caseB = Dp('h',N)+1;

                }
                //print_dp();
                //caseC
                if(c2)
                {
                    Reset_dp();
                    tag[0,1]=tag[0,N]=inf;
                    dp_f[1] = 1;
                    dp_g[1] = 1;
                    dp_h[1] = 0;
                    caseC = Dp('g',N)+1;

                }
                //print_dp();
                //caseD
                if(c1&&c2)
                {
                    Reset_dp();
                    tag[0,1]=tag[0,N]=tag[1,1]=tag[1,N]=inf;
                    dp_f[2] = ((tag[0,2]+tag[1,2])<=W)? 1:2;
                    dp_g[2] = 1;
                    dp_h[2] = 1;
                    dp_f[1] = 0;
                    dp_g[1] = 0;
                    dp_h[1] = 0;
                    caseD = Dp('f',N-1)+2;

                }
                //print_dp();
                
                //sb.Append(caseA).Append(' ').Append(caseB).Append(' ').Append(caseC).Append(' ').Append(caseD).Append('\n');
                sb.Append(Math.Min(Math.Min(caseA,caseB),Math.Min(caseC,caseD))).Append('\n');
                /*
                for (int i=0; i<2; i++)
                {
                    for (int j=0; j<N; j++)
                    {
                        sb.Append(tag[i,j+1]).Append(' ');
                    }
                    sb.Append('\n');
                }
                */
                
            }

            sw.Write(sb);
            sw.Close();
            sr.Close();

        }
        static void print_dp()
        {
            foreach (int item in dp_f) sb.Append(item).Append(' ');
                sb.Append('\n');
                foreach (int item in dp_g) sb.Append(item).Append(' ');
                sb.Append('\n');
                foreach (int item in dp_h) sb.Append(item).Append(' ');
                sb.Append('\n');
        }
        static void Reset_dp()
        {
            tag[0,1] = tmp01;
            tag[1,1] = tmp11;
            tag[0,N] = tmp0N;
            tag[1,N] = tmp1N;
            dp_f = Enumerable.Repeat<int>(-1,N+1).ToArray<int>();
            dp_g = Enumerable.Repeat<int>(-1,N+1).ToArray<int>();
            dp_h = Enumerable.Repeat<int>(-1,N+1).ToArray<int>();
            dp_f[0]=dp_g[0]=dp_h[0]=0;
        }
        static int Dp(char foo, int idx)
        {
            
            switch (foo)
            {
                case 'f':
                    if (dp_f[idx]!=-1) return dp_f[idx];
                    int f;
                    bool c1 = ((tag[0,idx-1]+tag[0,idx])<=W)&&((tag[1,idx-1]+tag[1,idx])<=W);
                    bool c2 = (tag[0,idx]+tag[1,idx])>W;
                    if (c1&&c2) f = Math.Min(Math.Min(Dp('g',idx)+1,Dp('h',idx)+1),Dp('f',idx-2)+2);
                    else if (c1&&(!c2)) f = Math.Min(Math.Min(Dp('g',idx)+1,Dp('h',idx)+1),Math.Min(Dp('f',idx-1)+1,Dp('f',idx-2)+2));
                    else if (c2) f = Math.Min(Dp('g',idx)+1,Dp('h',idx)+1);
                    else f = Math.Min(Math.Min(Dp('g',idx)+1,Dp('h',idx)+1),Dp('f',idx-1)+1);
                    dp_f[idx] = f;
                    return dp_f[idx];
                case 'g':
                    if (dp_g[idx]!=-1) return dp_g[idx];
                    dp_g[idx]=(tag[1,idx-1]+tag[1,idx]>W)? Dp('f',idx-1)+1: Dp('h',idx-1)+1;
                    return dp_g[idx];

                case 'h':
                    if (dp_h[idx]!=-1) return dp_h[idx];
                    dp_h[idx]=(tag[0,idx-1]+tag[0,idx]>W)? Dp('f',idx-1)+1: Dp('g',idx-1)+1;
                    return dp_h[idx];
                default:
                return inf;                
            }
        }
    }
}
#elif other3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  1006번 습격자 초라기
 */
namespace BackJun_Pool2 {
    class Num1006 {
        int         N, W;                   //N : (구역의 개수)/2, W : 특수 소대원 수(1 ≤ N ≤ 10000, 1 ≤ W ≤ 10000)
        int         total;
        int[]       infor;                  //사용자 입력(input)받는 변수.
        Zone[]      zone;
        Stack<int>  stack;


        struct Zone {
            public bool            is_assign;      //배치됨
            public int             degree;         //차수(간선의 수)
            public List<int>       adjust;         //간선
            public int             enemy;
        }
        
        private void init() {
            infor = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();     
            N = infor[0];
            W = infor[1];

            total               = N * 2;
            zone                = new Zone[total+1];    // 1부터 시작이라(편의를위해) +1을 해준다.
            stack               = new Stack<int>();


            int temp = 0;
            infor = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();    //적군들 입력.
            for(int i = 1; i<=N; i++) {
                zone[i]             = new Zone();
                zone[i].is_assign   = false;
                zone[i].degree      = 0;
                zone[i].adjust      = new List<int>();
                zone[i].enemy       = infor[temp];
                temp++;
            }

            temp = 0;
            infor = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();    //적군들 입력.
            for(int i = N+1; i<=total; i++) {
                zone[i]             = new Zone();
                zone[i].is_assign   = false;
                zone[i].degree      = 0;
                zone[i].adjust      = new List<int>();
                zone[i].enemy       = infor[temp];
                temp++;
            }
        }

        private void setAdjest_for_Assign() {
            int updown, left, right;

            for(int i = 1; i<=total; i++) {
                if(i <= N) {
                    updown = i+N;
                    left  = ((i-1) < 1)? N: i-1;
                    right = ((i+1) > N)? 1: i+1;
                } else {
                    updown = i-N;
                    left  = ((i-1) < N+1) ? total : i-1;
                    right = ((i+1) > total) ? N+1 : i+1;
                }

                
                //한 특수소대는 한 개 혹은 두 개의 구역을 커버할 수 있다. // 특수소대 인원(W) - 두 구역의 합이 W보다 작거나 같으면 둘다 커버 가능.
                if(zone[i].enemy + zone[updown].enemy <= W) {
                    zone[i].adjust.Add(updown);
                    zone[i].degree++;
                    zone[updown].degree++;
                }

                if(zone[i].enemy + zone[left].enemy <= W) {
                    zone[i].adjust.Add(left);
                    zone[i].degree++;
                    zone[left].degree++;
                }

                if(zone[i].enemy + zone[right].enemy <= W) {
                    zone[i].adjust.Add(right);
                    zone[i].degree++;
                    zone[right].degree++;
                }
            }
        }

        private int assign() {
            int cnt = 0;
            
            for(int i = 1; i<=total; i++) {
                if(zone[i].degree > 2) {
                    stack.Push(i);
                }
            }

            for(int i = 1; i<=total; i++) {
                if(zone[i].degree <= 2) {
                    stack.Push(i);          //우선순위
                }
            }

            
            while(stack.Count != 0){
                int now = stack.Pop();

                if(zone[now].is_assign == true) {
                    continue;
                }

                if(zone[now].degree == 0) {
                    zone[now].is_assign = true;
                    cnt++;

                } else if(zone[now].degree == 2) {  // && zone[now].degree == 1) 경우가 나올 수 없다. 2짝으로 연결되며 연결되며 degree는 +2카운트 된다. 해제도 -2해제.

                    int next = findNext(now);
                    if(next != -1) {
                        zone[now].is_assign = true;
                        zone[next].is_assign = true;
                        cnt++;
                        removeAll_Adjust(next);
                        removeAll_Adjust(now);
                    }

                } else {
                    //degree > 2

                    int next = findNext(now);
                    if(next != -1) {
                        zone[now].is_assign = true;
                        zone[next].is_assign = true;
                        cnt++;
                        removeAll_Adjust(next);
                        removeAll_Adjust(now);
                    }

                }
            }

            return cnt;
        }
        private int findNext(int now) {
            if(zone[now].adjust == null) {
                return -1;
            }
            foreach(int next in zone[now].adjust) {
                if(zone[next].is_assign == false) {
                    return next;
                }
            }
            return -1;
        }
        private void removeAll_Adjust(int target) {
            foreach(int next in zone[target].adjust) {
                zone[next].degree -= 2;         //연결된 모든 간선들을 제거한다. 서로 연결된 상태라 -2카운트 한다.

                if(zone[next].degree <= 2 && zone[next].is_assign == false) {
                    stack.Push(next);
                }
            }
        }

        private void runSolution() {
            int min = 0;
            init();
            setAdjest_for_Assign();
            min = assign();
            Console.WriteLine("{0}", min);//출력
        }

        public void execute(int case_T) {

            for(int i = 0; i<case_T; i++) {
                runSolution();
            }
        }


        static void Main(string[] args) {

            new Num1006().execute(int.Parse(Console.ReadLine()));
        }
    }
}

#endif
}
