using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 30
이름 : 배성훈
내용 : 발전소
    문제번호 : 1102번
*/

namespace BaekJoon.etc
{
    internal class etc_0744
    {

        static void Main744(string[] args)
        {

            int INF = 2_000;

            StreamReader sr;
            int[][] board;          // i -> j 기동 하는데 드는 비용
            int[][] dp;             // i개 켰는 상태, j 는 이전 상태 -> 현재 상태
                                    // 배열 2개만 있어도 충분하다
            int n;
            int initState;
            int max, cur;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                if (max <= cur)
                {

                    // 안켜도 되는 경우!
                    Console.Write(0);
                    return;
                }
                else if (cur == 0)
                {

                    // 모두 꺼져있어 가동 불가능한 상황
                    Console.Write(-1);
                    return;
                }

                Init();

                Find();

                Console.Write(ret);
            }

            void Find()
            {

                int len = 1 << n;

                for (int i = cur; i < max; i++)
                {

                    for (int j = 0; j < len; j++)
                    {

                        // 불가능한 상태
                        if (dp[i][j] == INF) continue;
                        // 가능한 상태에 대해서만 다음경우 탐색
                        SetNext(i, j);
                    }
                }

                ret = INF;
                for (int i = 0; i < len; i++)
                {

                    ret = Math.Min(ret, dp[max][i]);
                }
            }

            void SetNext(int _idx, int _state)
            {

                int cur = dp[_idx][_state];

                for (int i = 0; i < n; i++)
                {
                    
                    // i번째 포함 X, 즉 추가 가능
                    if (((1 << i) & _state) == 0)
                    {

                        int next = _state | (1 << i);
                        int chk = dp[_idx + 1][next];

                        for (int j = 0; j < n; j++)
                        {

                            // 현재 상태에서 기동 안된거면 켤 수 없다
                            if (((1 << j) & _state) == 0) continue;

                            // 최소값인지 확인
                            chk = Math.Min(chk, cur + board[j][i]);
                        }

                        // 최소값 갱신시도하고 넣는다
                        dp[_idx + 1][next] = chk;
                    }
                }
            }

            void Init()
            {

                dp = new int[n + 1][];
                for (int i = cur; i <= max; i++)
                {

                    dp[i] = new int[1 << n];
                    Array.Fill(dp[i], INF);
                }

                dp[cur][initState] = 0;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                board = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                initState = 0;
                cur = 0;
                for (int i = 0; i < n; i++)
                {

                    if (sr.Read() == 'N') continue;
                    initState |= 1 << i;
                    cur++;
                }

                if (sr.Read() == '\r') sr.Read();

                max = ReadInt();
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
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator char[](IO _)=>reader.ReadLine().ToArray();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        int count = Cin;
        // [여기서][저기를] : 수리하는 비용
        int[][] cost = new int[count][];
        for(int y=0;y<count;y++) {
            cost[y] = Cin;
        }

        int available = 0;
        int start = 0;
        {
            string input = Cin;
            for(int x=0;x<count;x++) {
                if (input[x] is 'Y') {
                    available |= 1 << x;
                    start++;
                }
            }
        }

        // end개 이상의 발전소가 살아있으면 됨
        int end = Cin;
        //이게 0이면 모든 발전소가 죽은거임! 못살림
        if (end > 0 && start is 0) {
            //시말서 쓰자~
            Cout = -1;
            return;
        }
        //이미 충족됬다면?
        if (start >= end) {
            Cout = 0;
            return;
        }

        int?[,] memo = new int?[end,1<<count];
        int dp(int enable,int bitlist) {
            //더이상 발전소를 살릴 필요가 없을경우
            if (enable >= end) {
                return 0;
            }
            if (memo[enable,bitlist] is int min) {
                return min;
            }
            min = int.MaxValue;
            //살릴만한 다른 발전소 찾아보기
            for(int me = 0;me < count;me++) {
                if ((bitlist & (1<<me)) == 0) continue;
                for(int other=0;other<count;other++) {
                    int shift = 1 << other;
                    //이미 살려낸 발전소라면 건너뛰기
                    if ((bitlist & shift) == shift) continue;
                    //아직 안살린 발전기가 있다면
                    min = Math.Min(
                        min,
                        dp(enable+1,bitlist|shift) + cost[me][other]
                    );
                }
            }
            memo[enable,bitlist] = min;
            return min;
        }

        Cout = dp(start,available);
    }
}
#elif other2
// #nullable disable

using System;
using System.IO;
using System.Linq;
using System.Numerics;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var cost = new int[n][];
        for (var idx = 0; idx < n; idx++)
            cost[idx] = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var initialMask = sr
            .ReadLine()
            .Select((v, index) => v == 'N' ? 0 : (1 << index))
            .Sum();

        var p = Int32.Parse(sr.ReadLine());

        if (initialMask == 0 && p != 0)
        {
            sw.WriteLine(-1);
            return;
        }
        if (BitOperations.PopCount((uint)initialMask) >= p)
        {
            sw.WriteLine(0);
            return;
        }

        var dp = new int?[1 << n];
        var min = default(int?);

        dp[initialMask] = 0;

        for (var mask = 0; mask < (1 << n); mask++)
            for (var ppIndexTo = 0; ppIndexTo < n; ppIndexTo++)
            {
                // Already turned on
                if ((mask & (1 << ppIndexTo)) != 0)
                    continue;

                var minCost = default(int?);
                for (var ppIndexFrom = 0; ppIndexFrom < n; ppIndexFrom++)
                {
                    // Not turned on
                    if ((mask & (1 << ppIndexFrom)) == 0)
                        continue;

                    // Invalid state
                    if (!dp[mask].HasValue)
                        continue;

                    minCost = Math.Min(minCost ?? Int32.MaxValue, dp[mask].Value + cost[ppIndexFrom][ppIndexTo]);
                }

                if (minCost.HasValue)
                {
                    var newMask = mask | (1 << ppIndexTo);
                    dp[newMask] = Math.Min(dp[newMask] ?? Int32.MaxValue, minCost.Value);

                    if (BitOperations.PopCount((uint)newMask) >= p)
                        min = Math.Min(min ?? Int32.MaxValue, minCost.Value);
                }
            }

        sw.WriteLine(min ?? -1);
    }
}
#elif other3
using System;
using System.Linq;

namespace 발전소_1
{
    class Program
    {
        static int P, N, Y;
        static int[] Solve;
        static int[,] Map;
        static void Main(string[] args)
        {
            //Console.WriteLine($"{(1 << 3) - (1 << (3 - 2))}");

            N = int.Parse(Console.ReadLine());
            Map = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                int[] input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j < N; j++)
                {
                    Map[i, j] = input[j];
                }
            }
            string bit = Console.ReadLine();
            Y = 0;
            int start = 0;
            for (int i = 0; i < N; i++)
            {
                if ((bit[i] == 'Y'))
                {
                    start += 1 << i; //켜져있는 발전소
                    Y++;
                }
            }
            P = int.Parse(Console.ReadLine());

            Solve = Enumerable.Repeat<int>(-1, 1 << N).ToArray<int>(); //비용 최소값 배열

            Solve[start] = 0;

            for(int i = 0; i < (1 << N); i++)
            {
                if (Solve[i] == -1) continue; //초기값을 찾는다.
                for(int j = 0; j < N; j++)
                {
                    if ((i & (1 << j)) == 0) continue; //고장나지 않은 발전소를 찾는다.
                        
                    for(int k = 0; k < N; k++)
                    {
                        if (k == j) continue;

                        Solve[i | (1 << k)] = Solve[i | (1 << k)]>-1? Math.Min(Solve[i | (1 << k)],Solve[i] + Map[j,k]): Solve[i] + Map[j, k];
                    }
                }
            }

            int ans = -1;
            for (int i = 0; i < (1 << N); i++)
            {
                if (Solve[i] == -1) continue;
                int count = 0;
                for (int j = 0; j < N; j++)
                {
                    if ((i & (1 << j))!=0) count++;
                }
                if (count >= P)
                {
                    if (ans == -1 || ans > Solve[i]) ans = Solve[i]; // ans를 -1로 했기 때문에 처음에 scope들어오기 위해 조건 추가
                }
            }

            Console.Write($"{ans}");

            //foreach (int i in Solve)
            //Console.WriteLine($"{i}");
        }
    }
}

#endif
}
