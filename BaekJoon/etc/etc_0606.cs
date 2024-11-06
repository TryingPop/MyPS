using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 이분 매칭
    문제번호 : 1587번

    그리디 문제다
    문제 상황을 분석하는 문제다
    이분 매칭 이름의 문제이나, 이분 매칭을 쓰지 않는다!
    같은 그룹안에 인접한 노드번호끼리는 간선으로 연결되어져 있다

    아이디어는 다음과 같다
    그래서, A, B 그룹의 노드 수가 짝수인 경우 -> 그리디하게 자기 자신의 그룹으로 묶으면
    모든 노드들이 매칭되어 최대값이 나온다
    마찬가지로 그룹의 노드 수가 홀짝, 짝홀인 경우도 
    자기 그룹만 매칭해도 최대 매칭이 바로 나온다

    이제 홀수, 홀수인 경우만 따져보면 된다
    이 경우 홀수 번에서 홀수번 노드로 가는 간선이 적어도 1개 존재하면
    모두 매칭될 수 있다
    해당 간선의 노드만 잇고, 이외는 자기 노드끼리 이으면 최대 노드가 된다

    작은 경우로 구분해서 보면 모두 매칭 되는 경우 적어도 하나의 홀수 -> 홀수로 가는
    간선이 필요함을 알 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0606
    {

        static void Main606(string[] args)
        {
            StreamReader sr;
            int n, m;
            int[] line;

            Solve();

            void Solve()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));

                n = ReadInt();
                m = ReadInt();

                line = new int[4];
                int len = ReadInt();
                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt() % 2;
                    int b = ReadInt() % 2;

                    int idx = f * 2 + b;
                    line[idx]++;
                }

                sr.Close();

                int ret = n / 2 + m / 2;
                if (n % 2 == 1 && m % 2 == 1 && line[3] > 0) ret++;

                Console.WriteLine(ret);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
import java.util.*;
import java.io.*;
import java.math.*;
 
public class Main{
    static BufferedReader br = 
            new BufferedReader(new InputStreamReader(System.in));
    static BufferedWriter bw = 
            new BufferedWriter(new OutputStreamWriter(System.out));
    
    public static void main(String args[]) throws IOException{
    	// Scanner sc = new Scanner(System.in);
    	int a, b, n, i;
    	StringTokenizer st = new StringTokenizer(br.readLine(), " ");
    	a = Integer.parseInt(st.nextToken());
    	b = Integer.parseInt(st.nextToken());
    	n = Integer.parseInt(br.readLine());
    	int[][] E = new int[n][2];
    	for(i = 0; i < n; i++){
    		st = new StringTokenizer(br.readLine(), " ");
    		E[i][0] = Integer.parseInt(st.nextToken());
    		E[i][1] = Integer.parseInt(st.nextToken());
    	}
    	if(a%2 == 1 && b%2 == 1){
    		for(i = 0; i < n; i++){
    			if(E[i][0]%2 == 1 && E[i][1]%2 == 1){
    				System.out.println(a/2+b/2+1);
    				return;
    			}
    		}
    		System.out.println(a/2+b/2);
    	}else System.out.println(a/2+b/2);
    	bw.close();
    }
}
#endif
}
