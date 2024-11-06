using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 민서의 응급 수술
    문제번호 : 20955번

    트리, 분리집합 문제다
    유니온 파인드 알고리즘을 써서 풀었다

    아이디어는 다음과 같다
    간선이 있을 때, 그룹을 최초로 연결하는 간선은 살린다
    그리고 이후 같은 그룹을 잇는 간선들은 끊어버려야 하므로 시행횟수 1이 증가한다

    다음으로 현재 존재하는 그룹의 수를 세고, 
    그룹간 연결에 시행횟수가 1씩 추가된다 그룹간 연결은,
    서로다른 그룹의 개수 - 1이다

    이렇게 결론을 도출하니 76ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0593
    {

        static void Main593(string[] args)
        {

            StreamReader sr;
            int n;
            Stack<int> s;
            int[] group;
            Solve();

            void Solve()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
                n = ReadInt();
                int len = ReadInt();

                s = new(n);
                group = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                int ret = 0;
                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    f = Find(f);
                    b = Find(b);

                    if (f == b)
                    {

                        ret++;
                        continue;
                    }

                    Union(f, b);
                }

                bool[] chk = new bool[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    int cur = Find(i);

                    if (chk[cur]) continue;
                    chk[cur] = true;
                    ret++;
                }
                ret--;

                sr.Close();

                Console.WriteLine(ret);
            }

            void Union(int _f, int _b)
            {

                if (_b < _f)
                {

                    int temp = _f;
                    _f = _b;
                    _b = temp;
                }

                group[_b] = _f;
            }
            
            int Find(int _chk)
            {

                while (group[_chk] != _chk)
                {

                    s.Push(_chk);
                    _chk = group[_chk];
                }

                while(s.Count > 0)
                {

                    group[s.Pop()] = _chk;
                }

                return _chk;
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
	private static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	private static StringBuilder sb = new StringBuilder();
	private static StringTokenizer tokens;

	private static int N;	//	정점 개수
	private static int M;	//	간선 개수
	private static int[] parents;	//	i번째 정점이 속한 정점의 우두머리
	private static int ds;			//	분리 집합 개수
	private static int cycle;		//	사이클 발생 횟수
	private static int ans;			//	최소 연산 횟수
	
	public static void main(String[] args) throws IOException {
		tokens = new StringTokenizer(br.readLine());
		N = Integer.parseInt(tokens.nextToken());
		M = Integer.parseInt(tokens.nextToken());
		
		parents = new int[N + 1];
		ds = N;
		
		for(int i = 1; i <= N; i++)
			parents[i] = i;
		
		for(int i = 0; i < M; i++) {
			tokens = new StringTokenizer(br.readLine());
			int x = Integer.parseInt(tokens.nextToken());
			int y = Integer.parseInt(tokens.nextToken());
			
			union(x, y);
		}
		
		ans += cycle;	//	cycle 제거해야 함
		
		if(ds > 1)	//	분리 집합이 두 개 이상일 경우
			ans += (ds - 1);
		
		System.out.println(ans);
	} //	main-end
	
	private static int findParent(int x) {
		if(x == parents[x])
			return x;
		
		return parents[x] = findParent(parents[x]);
	}
	
	private static void union(int x, int y) {
		x = findParent(x); 
		y = findParent(y);
		
		if(x != y) {	//	x, y가 같은 집합에 속하지 않을 경우
			parents[y] = x;
			ds--;
		} else
			cycle++;	//	사이클 발생
	}
} //	Main-class-end
#endif
}
