using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 왕위 계승
    문제번호 : 5021번

    위상 정렬, 해시 문제다
    풀고나니 힌트대로 풀었다

    아이디어는 다음과 같다
    이름을 입력 받기에 자식 부모 관계에 쓰는 이름을
    인덱스로 바꿔줄 필요가 있다 그래서 딕셔너리(해시)를 사용해 인덱스로 변경
    그리고 부모 - 자식 관계를 간선으로 봐서 단방향 간선의 개수로 위상을 설정
    그리고 위상 정렬 하면서 왕의 피?를 물려줬다
    왕의 피가 분수 형태인데 물려주는게 50%씩이므로, 
    해당 조건과 동형인 정수의 비트 연산으로 대체했다
    그리고 왕의 피 값이 큰 사람을 찾는다
    왕의 피를 가장 많이 물려받은 사람을 찾아 해당 사람을 출력하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0384
    {

        static void Main384(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            // 이름을 저장할 데이터의 인덱스로 바꿔준다
            // 중복될 수 있지만 가질 수 잇는 최대값으로 먼저 용량을 확보!
            Dictionary<string, int> nToi = new(n * 3);

            // 자식 관계
            string[][] inputs = new string[n][];
            string str = sr.ReadLine();

            // nToi에 등록된 이름의 개수
            int cnt = 0;
            // 처음 왕은 0번으로 설정
            nToi[str] = cnt++;

            for (int i = 0; i < n; i++)
            {

                // 0번 자식, 1, 2번은 부모
                inputs[i] = sr.ReadLine().Split(' ');
                for (int j = 0; j < 3; j++)
                {

                    // 간선관계에 있는 이름을 인덱스로 바꾼다
                    // 이미 인덱스가 할당된 이름이면 넘긴다
                    if (nToi.ContainsKey(inputs[i][j])) continue;
                    nToi[inputs[i][j]] = cnt++;
                }
            }

            // 찾을 이름
            string[] find = new string[m];
            for (int i = 0; i < m; i++)
            {

                find[i] = sr.ReadLine();
            }

            sr.Close();

            // 부모 자식 관계를 단방향 간선으로 설정
            List<int>[] lines = new List<int>[cnt];
            for (int i = 0; i < cnt; i++)
            {

                lines[i] = new();
            }

            // 위상
            int[] conn = new int[cnt];
            // 혈연농도
            long[] val = new long[cnt];

            // 1 / 2^ ? 씩 물려받으나
            // 소수점 오차를 겪기 싫어
            // 비트 연산으로 했다
            val[0] = 1L << n;
            for (int i = 0; i < n; i++)
            {

                // 부 -> 자식, 모 -> 자식인 간선 설정 
                lines[nToi[inputs[i][1]]].Add(nToi[inputs[i][0]]);
                lines[nToi[inputs[i][2]]].Add(nToi[inputs[i][0]]);

                // 연결된 간선의 개수를 위상으로 설정
                conn[nToi[inputs[i][0]]] += 2;
            }

            // 위상 정렬
            Queue<int> q = new(cnt);
            for (int i = 0; i < cnt; i++)
            {

                // 위상이 0인 것을 먼저 꺼내 확인
                if (conn[i] == 0) q.Enqueue(i);

                while (q.Count > 0)
                {

                    int node = q.Dequeue();
                    // 확인 된 것은 중복 방지로 -1로 설정
                    conn[node] = -1;

                    for (int j = 0; j < lines[node].Count; j++)
                    {

                        int idx = lines[node][j];
                        // 혈연이므로 왕의 피 전달
                        val[idx] += val[node] / 2L;
                        conn[idx]--;
                        
                        if (conn[idx] == 0) q.Enqueue(idx);
                    }
                }
            }

            // 이제 가장 많은 왕의 피를 물려받은 사람을 찾는다
            long max = 0;
            // 정답이 존재하는 입력만 들어오므로 일단 -1로 해도 상관없다
            int ret = -1;
            for (int i = 0; i < m; i++)
            {

                int idx = 0;
                // 가족 관계에 없는 새로운 이름이 들어올 수도 있다
                // 예제 2번째
                long cur = 0;
                // 가족관계에 있는 이름인 경우
                if (nToi.ContainsKey(find[i]))
                {

                    idx = nToi[find[i]];
                    cur = val[idx];
                }

                // 왕의 피가 진하면 갱신
                if (max < cur)
                {

                    ret = i;
                    max = cur;
                }
            }

            // 결과 출력
            Console.WriteLine(find[ret]);

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
import java.util.StringTokenizer;

public class Main{
	static int N, M, nextKing;
	static String name[][], decendCandidate[];
	static String king;
	
	static double dfs(String decend) {
		if(decend.equals(king))
			return 1;

		for(int i = 0; i < N; i++)
		{
			if(decend.equals(name[i][0]))
			{
				return 0.5*dfs(name[i][1]) + 0.5*dfs(name[i][2]);
			}

		}
		
		return 0;
		
	}
	
	public static void main(String[] args) throws IOException {
		// TODO Auto-generated method stub

		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		N = Integer.parseInt(st.nextToken());
		M = Integer.parseInt(st.nextToken());
		
		name = new String[N][3];
		decendCandidate = new String[M];
		
		king = br.readLine();

		for(int n = 0; n < N; n++)
		{
			st = new StringTokenizer(br.readLine());
			for(int i = 0; i < 3; i++)
			{
				name[n][i] = st.nextToken();
			}
			
		}
		
		for(int m = 0; m < M; m++)
		{
			decendCandidate[m] = br.readLine();
		}
	
		double max = 0.0;
		for(int m = 0; m < M; m++)
		{
			if((dfs(decendCandidate[m])-max)> 0)
			{
				max = dfs(decendCandidate[m]);
				nextKing = m;
			}

		}
		
		System.out.println(decendCandidate[nextKing]);
		
		
	}

}
#endif
}
