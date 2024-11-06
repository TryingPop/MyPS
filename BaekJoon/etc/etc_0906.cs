using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 24
이름 : 배성훈
내용 : 끝말잇기
    문제번호 : 2320번

    dp, 비트마스킹 문제다
    선택한 문자를 비트마스킹 하고,
    최대 경우의 수를 구한다
*/

namespace BaekJoon.etc
{
    internal class etc_0906
    {

        static void Main906(string[] args)
        {

            StreamReader sr;
            int n;
            string[] str;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int chk = DFS(i, 1 << i);
                    ret = Math.Max(ret, chk);
                }

                Console.Write(ret);
            }

            int DFS(int _n, int _state)
            {

                if (dp[_n][_state] != -1) return dp[_n][_state];
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    if ((_state & (1 << i)) != 0) continue;
                    else if (str[_n].Last() != str[i].First()) continue;

                    int chk = DFS(i, _state | (1 << i));

                    ret = Math.Max(ret, chk);
                }

                ret += str[_n].Length;
                return dp[_n][_state] = ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                str = new string[n];

                for (int i = 0; i < n; i++)
                {

                    str[i] = sr.ReadLine();
                }

                dp = new int[n + 1][];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = new int[1 << n];

                    Array.Fill(dp[i], -1);
                }
                sr.Close();
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main(string[] args)
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var n = Int32.Parse(sr.ReadLine());

        var words = new string[n];
        for (var idx = 0; idx < n; idx++)
            words[idx] = sr.ReadLine();

        var dp = new int[n, 1 << n];
        var maxScore = 0;

        for (var word = 0; word < n; word++)
        {
            dp[word, 1 << word] = words[word].Length;
            maxScore = Math.Max(maxScore, dp[word, 1 << word]);
        }

        for (var mask = 1; mask < (1 << n); mask++)
        {
            for (var lastWord = 0; lastWord < n; lastWord++)
            {
                if ((mask & (1 << lastWord)) == 0)
                    continue;

                for (var nextWord = 0; nextWord < n; nextWord++)
                {
                    if ((mask & (1 << nextWord)) != 0)
                        continue;

                    if (words[lastWord].Last() != words[nextWord].First())
                        continue;

                    var newScore = dp[lastWord, mask] + words[nextWord].Length;
                    dp[nextWord, mask | (1 << nextWord)] = Math.Max(dp[nextWord, mask | (1 << nextWord)], newScore);
                    maxScore = Math.Max(maxScore, newScore);
                }
            }
        }

        sw.WriteLine(maxScore);
    }
}
#elif other2
// #include <iostream>
// #include <algorithm>

const int   MAX = 16;

int size, max;
int dp[MAX];
int visited[MAX];
std::string arr[MAX];

void find_value(int cur_idx, std::string &cur)
{
    int     cur_size = dp[cur_idx];
    char    &cur_back = cur.back();

    visited[cur_idx] |= (1 << cur_idx);
    for (int i = 0; i < size; ++i)
    {
        std::string &cmp = arr[i];
        if (cmp[0] != cur_back
            || visited[cur_idx] & (1 << i))
            continue ;

        int total_size = cur_size + cmp.size();

        if (dp[i] <= total_size)
        {
            dp[i] = total_size;
            visited[i] = visited[cur_idx] | (1 << cur_idx);
            find_value(i, arr[i]);
        }

        if (max < dp[i])
            max = dp[i];
    }
}

int main(void)
{
    std::cin.tie(0)->sync_with_stdio(0);

    std::cin >> size;
    for (int i = 0; i < size; ++i)
        std::cin >> arr[i];

    std::sort(arr, arr + size);

    for (int i = 0; i < size; ++i)
    {
        std::string &cur = arr[i];
        int         cur_size = cur.size();

        dp[i] = std::max(dp[i], cur_size);
        if (max < dp[i])
            max = dp[i];
        find_value(i, cur);
    }

    std::cout << max << '\n';
    return (0);
}

#elif other3
import java.io.*;

public class Main {
    static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    static BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
    static int N;
    static String[] words;
    static boolean[] visit;
    static int[][] already;
    static int ans;
    public static void main(String[] args) throws IOException {
       set();
       solve();

       bw.flush();
       br.close();
       bw.close();
    }
    private static void set() throws IOException {
        N = Integer.parseInt(br.readLine());
        words = new String[N];
        already = new int[N][(1<<N)+1];
        for(int i=0; i<N; i++){
            words[i] = br.readLine();
        }
    }
    static void solve() throws IOException {
        for(int i=0; i<N; i++){
            visit = new boolean[N];
            ans = Math.max(dfs(i,1<<i),ans);
        }
        bw.write(ans+"");
    }
    static int dfs(int index, int having){
        visit[index] = true;
        if(already[index][having]!=0) return already[index][having];

        for(int i=0; i<N; i++){
            // 방문 하지 않았고 뒷 단어 == 첫 단어
            if(!visit[i] && words[index].charAt(words[index].length()-1)==words[i].charAt(0)){
                already[index][having] = Math.max(already[index][having],dfs(i,having|1<<i));
            }
        }
        visit[index] = false;
        return already[index][having]+=words[index].length();
    }
}
#endif
}
