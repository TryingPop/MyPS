using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 값이 k인 트리 노드의 깊이
    문제번호 : 25511번

    트리 문제다
    BFS 탐색으로 모든 깊이를 구했고, 
    이후 필요한 깊이를 출력했다

    시간을 보니 n의 범위가 커보인다
    124ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0566
    {

        static void Main566(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n;
            int find;
            List<int>[] line;
            int[] depth;

            Solve();
            sr.Close();

            void Solve()
            {

                Input();
                BFS();
                Console.WriteLine(depth[find]);
            }

            void Input()
            {

                n = ReadInt();
                int k = ReadInt();

                line = new List<int>[n];
                for (int i = 0; i < n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < n - 1; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Add(b);
                    line[b].Add(f);
                }
                find = -1;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    if (k == cur) find = i;
                }
            }

            void BFS()
            {

                bool[] visit = new bool[n];
                depth = new int[n];
                Queue<int> q = new(n);

                q.Enqueue(0);
                visit[0] = true;

                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    int curDepth = depth[node];
                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        if (visit[next]) continue;
                        visit[next] = true;
                        depth[next] = curDepth + 1;

                        q.Enqueue(next);
                    }
                }
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
import java.io.*;
import java.util.*;

public class Main {

    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());

        int n = Integer.parseInt(st.nextToken());
        int k = Integer.parseInt(st.nextToken());

        int[] arr = new int[n];

        for (int i=0; i<n-1; i++) {
            st = new StringTokenizer(br.readLine());
            int a = Integer.parseInt(st.nextToken());
            int b = Integer.parseInt(st.nextToken());

            arr[b] = a;
        }
        arr[0] = -1;

        st = new StringTokenizer(br.readLine());
        int idx = 0;
        for (int i=0; i<n; i++) {
            if (Integer.parseInt(st.nextToken()) == k) {
                idx = i;
            }
        }

        System.out.println(dfs(idx, 0, arr));
    }

    public static int dfs(int idx, int depth, int[] arr) {
        if (arr[idx] == -1) return depth;
        return dfs(arr[idx], depth+1, arr);
    }

}
#endif
}
