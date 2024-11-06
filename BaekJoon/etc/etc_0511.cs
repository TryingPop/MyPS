using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 색칠하기
    문제번호 : 13265번

    BFS, DFS, 이분 그래프 문제다
    비연결 그래프 반례를 떠올리지 못해 3번 틀렸다
    연결 되어져 있다고 생각하고 1에서만 시작했다.
    -> 87%에서 막혔다
    반례는 다음과 같다
        1
        4 3
        2 4
        4 3
        3 2

    이후에 수정하니 148ms에 통과했다

    중복으로 들어올거 같아 HashSet으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0511
    {

        static void Main511(string[] args)
        {

            string YES = "possible\n";
            string NO = "impossible\n";

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 1024 * 8);

            int test = ReadInt();
            int[] color = new int[1_001];
            bool[] visit = new bool[1_001];
            HashSet<int>[] line = new HashSet<int>[1_001];
            Queue<int> q = new(1_000);

            for (int i = 1; i <= 1_000; i++)
            {

                line[i] = new();
            }

            while(test-- > 0)
            {

                int n = ReadInt();
                int m = ReadInt();

                for (int i = 0; i <m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Add(b);
                    line[b].Add(f);
                }

                bool ret = AddColor(n);

                for (int i = 1; i <= n; i++)
                {

                    color[i] = 0;
                    line[i].Clear();
                    visit[i] = false;
                }

                sw.Write(ret ? YES : NO);
            }

            sr.Close();
            sw.Close();

            bool AddColor(int _n)
            {

                q.Clear();
                for (int i = 1; i <= _n; i++)
                {

                    if (visit[i]) continue;
                    visit[i] = true;
                    q.Enqueue(i);
                    color[i] = 1;

                    while (q.Count > 0)
                    {

                        int node = q.Dequeue();
                        int cur = color[node];
                        foreach (int next in line[node])
                        {

                            int chk = color[next];
                            if (chk == cur) return false;
                            if (visit[next]) continue;
                            visit[next] = true;
                            color[next] = -cur;
                            q.Enqueue(next);
                        }
                    }
                }
                return true;
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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayDeque;
import java.util.Arrays;
import java.util.Queue;
import java.util.StringTokenizer;

public class Main {

    static int[] visited;

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringBuilder sb = new StringBuilder();


        int T = Integer.parseInt(br.readLine());

        for (int testCase = 0; testCase < T; testCase++) {
            StringTokenizer st = new StringTokenizer(br.readLine());

            int N = Integer.parseInt(st.nextToken());
            int M = Integer.parseInt(st.nextToken());

            Circle[] map = new Circle[N + 1];

            for (int i = 1; i <= N; i++) {
                map[i] = new Circle(i, null);
            }

            for (int i = 0; i < M; i++) {
                st = new StringTokenizer(br.readLine());

                int first = Integer.parseInt(st.nextToken());
                int second = Integer.parseInt(st.nextToken());

                map[first].nextCircle = new Circle(second, map[first].nextCircle);
                map[second].nextCircle = new Circle(first, map[second].nextCircle);
            }
            visited = new int[map.length];
            boolean check = true;
            for (int i = 1; i <= N; i++) {
                if (visited[i] != 0) {
                    continue;
                }
                if(!solve(map, i, 1, 0)){
                    check = false;
                    break;
                }
            }

            sb.append(check ? "possible" : "impossible").append("\n");
        }
        System.out.println(sb);
    }

    private static boolean solve(Circle[] map, int index , int sort, int parent) {

        visited[index] = sort;
        for(Circle nextCircle = map[index].nextCircle; nextCircle != null; nextCircle = nextCircle.nextCircle){
            if (visited[nextCircle.index] != 0) {
                if (visited[nextCircle.index] == sort) {
                    return false;
                }
                continue;
            }
            if(!solve(map, nextCircle.index, sort == 1 ? 2 : 1, index)){
                return false;
            }
        }
        return true;
    }

    private static class Circle {

        int index;
        Circle nextCircle;

        public Circle(int index, Circle nextCircle) {
            this.index = index;
            this.nextCircle = nextCircle;
        }
    }

}

#endif
}
