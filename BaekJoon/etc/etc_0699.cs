using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 16
이름 : 배성훈
내용 : 룩 배치하기
    문제번호 : 9525번

    이분 매칭 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0699
    {

        static void Main699(string[] args)
        {

            StreamReader sr;
            int[][][] board;
            int size;

            HashSet<int>[] line;
            int[] match;
            bool[] visit;

            int len1, len2;

            Solve();

            void Solve()
            {

                Input();

                LinkLine();

                int ret = 0;
                for (int i = 1; i <= len1; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                foreach (int next in line[_n])
                {

                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void LinkLine()
            {

                len1 = 0;
                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (board[r][c][0] == 1) continue;

                        board[r][c][1] = ++len1;
                        for (int i = c + 1; i < size; i++)
                        {

                            if (board[r][i][0] == 1) break;
                            board[r][i][1] = len1;
                            c++;
                        }
                    }
                }

                len2 = 0;
                for (int c = 0; c < size; c++)
                {

                    for (int r = 0; r < size; r++)
                    {

                        if (board[r][c][0] == 1) continue;

                        board[r][c][2] = ++len2;

                        for (int j = r + 1; j < size; j++)
                        {

                            if (board[j][c][0] == 1) break;
                            board[j][c][2] = len2;
                            r++;
                        }
                    }
                }

                line = new HashSet<int>[len1 + 1];
                match = new int[len2 + 1];
                visit = new bool[len2 + 1];

                for (int i = 1; i <= len1; i++)
                {

                    line[i] = new();
                }

                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (board[r][c][0] == 1) continue;
                        line[board[r][c][1]].Add(board[r][c][2]);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = int.Parse(sr.ReadLine());

                board = new int[size][][];
                for (int i = 0; i < size; i++)
                {

                    board[i] = new int[size][];
                    for (int j = 0; j < size; j++)
                    {

                        board[i][j] = new int[3];
                        int cur = sr.Read();
                        if (cur == '.') continue;

                        board[i][j][0] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;

public class Main {
    public static ArrayList<Integer>[] g;
    public static int[] match;
    public static boolean[] visit;
    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int N = Integer.parseInt(br.readLine());
        char[][] map = new char[N][N];
        int[][] row = new int[N][N];
        for (int i = 0; i < N; i++) {
            map[i] = br.readLine().toCharArray();
        }
        int r = 1;
        boolean check = false;
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                if(map[i][j] == 'X'){
                    if(check) {
                        r++;
                        check = false;
                    }
                } else{
                    row[i][j] = r;
                    check = true;
                }
            }
            if(check) {
                r++;
                check = false;
            }
        }
        g = new ArrayList[r+1];
        visit = new boolean[r+1];
        for (int i = 0; i <= r; i++) {
            g[i] = new ArrayList();
        }
        int l = 1;
        check = false;
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                if(map[j][i] == 'X'){
                    if(check) {
                        l++;
                        check = false;
                    }
                } else{
                    g[row[j][i]].add(l);
                    check = true;
                }
            }
            if(check) {
                l++;
                check = false;
            }
        }
        match = new int[l+1];
        int res = 0;
        for (int i = 1; i <= r; i++) {
            Arrays.fill(visit,false);
            if(dfs(i))
                res++;
        }
        System.out.println(res);
    }

    public static boolean dfs(int v){
        visit[v] = true;
        for (int i = 0; i < g[v].size(); i++) {
            int t = g[v].get(i);
            if(match[t] == 0 || (!visit[match[t]] && dfs(match[t]))){
                match[t] = v;
                return true;
            }
        }
        return false;
    }
}
#endif
}
