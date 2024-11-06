using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 세 친구
    문제번호 : 17089번

    브루트포스 문제다
    처음에는 조합 형태로 삼중 포문으로 
    a 친구 ? a - b 친구 ? b - c & a - c 친구로 탐색해서 제출하니 260ms에 통과했다

    다른 사람 시간과 비교해보니 매우 느린 시간이였고,
    이에 접근 방법을 수정했다

    아이디어는 다음과 같다
    a를 선택한다 -> 그리고 다음 포문에서는 a의 친구들만 확인한다
    그리고 b의 친구들만 확인해서 a와 친구인지만 확인한다
    만약 망가진 문제, 자기자신과 친구인 애가 있다면 자기자신을 체크해야한다
    (조건에서 같은 친구관계가 두 번 이상 주어지는 경우는 없다고 했다)

    그리고 해당 a의 탐색이 끝나면 a를 포함한 경우는 다 확인했다
    이제 a를 또 탐색하지 않게 하기 위해서 visit 배열을 써도 되고,
    혹은 그냥 가는 길을 애초에 안만들어도 된다
    (작은 인덱스에서 큰 인덱스로 가는 길만 만든다!)

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0484
    {

        static void Main484(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 8);
            int n = ReadInt();
            int m = ReadInt();

            // HashSet<int>[] line = new HashSet<int>[n + 1];
            List<int>[] line = new List<int>[n + 1];
            HashSet<int>[] chk = new HashSet<int>[n + 1];

            int[] friendCnt = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                line[i] = new();
                chk[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int b = ReadInt();

                if (b < f)
                {

                    int temp = f;
                    f = b;
                    b = temp;
                }

                line[f].Add(b);
                chk[f].Add(b);

                friendCnt[f]++;
                friendCnt[b]++;
            }
            sr.Close();

            int ret = 15_000;
            /*
            for (int a = 1; a <= n; a++)
            {

                for (int b = a + 1; b <= n; b++)
                {

                    if (!line[a].Contains(b)) continue;
                    for (int c = b + 1; c <= n; c++)
                    {

                        if (!line[a].Contains(c) || !line[b].Contains(c)) continue;
                        int calc = friendCnt[a] + friendCnt[b] + friendCnt[c];
                        ret = calc < ret ? calc : ret;
                    }
                }
            }
            */


            for (int a = 1; a <= n; a++)
            {

                for (int i = 0; i < line[a].Count; i++)
                {

                    int b = line[a][i];
                    for (int j = 0; j < line[b].Count; j++)
                    {

                        int c = line[b][j];
                        if (!chk[a].Contains(c)) continue;

                        int calc = friendCnt[a] + friendCnt[b] + friendCnt[c];
                        ret = calc < ret ? calc : ret;
                    }
                }
            }

            if (ret == 15_000) Console.WriteLine(-1);
            else Console.WriteLine(ret - 6);

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
    static int N,M;
    static ArrayList<ArrayList<Integer>> graph_list = new ArrayList<>();
    static Stack<Integer> result = new Stack<>();
    static ArrayList<Integer> ans = new ArrayList<>();
    public static void main(String args[]) throws IOException {
      BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
      StringTokenizer st = new StringTokenizer(br.readLine());
      N = Integer.parseInt(st.nextToken());
      M = Integer.parseInt(st.nextToken());
      for(int i=0; i<=N; i++) graph_list.add(new ArrayList<>());
      for(int i=0; i<M; i++) {
          StringTokenizer n_st = new StringTokenizer(br.readLine());
          int a = Integer.parseInt(n_st.nextToken());
          int b = Integer.parseInt(n_st.nextToken());
          graph_list.get(a).add(b);
          graph_list.get(b).add(a);
      }
      DFS();
      if(ans.size()==0) System.out.println(-1);
      else {
          System.out.println(Collections.min(ans));
      }
    }
    
    static void DFS() {
        if(result.size()==3) {
            if(graph_list.get(result.get(0)).contains(result.get(2))) {
                // A,C가 친구라면 A, B, C 모두 친구
                ans.add(graph_list.get(result.get(0)).size() + graph_list.get(result.get(1)).size() + graph_list.get(result.get(2)).size()-6);
            }
            return;
        }
        if(result.size()==0) {
            for(int i=1; i<=N; i++) {
                result.push(i);
                DFS();
                result.pop();
            }
        } else {
            int top = result.peek();
            for(int i=0; i<graph_list.get(top).size(); i++) {
                int n = graph_list.get(top).get(i);
                if(top<n) {
                    result.push(n);
                    DFS();
                    result.pop();
                }
            }
        }
    }
}
#endif
}
