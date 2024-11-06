using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 불우이웃돕기
    문제번호 : 1414번

    최소 스패닝 트리, 문자열 문제다
    위상 정렬과 유니온 파인드 알고리즘을 이용해 최소 신장 트리를 만들었다
*/

namespace BaekJoon.etc
{
    internal class etc_0649
    {

        static void Main649(string[] args)
        {

            StreamReader sr;
            int n;
            PriorityQueue<(int f, int b, int dst), int> q;
            int[] group;
            Stack<int> s;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                group = new int[n + 1];
                s = new(n);
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                MST();

                Console.WriteLine(ret);
            }

            void MST()
            {

                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    int f = Find(node.f);
                    int b = Find(node.b);

                    if (f == b) continue;
                    ret -= node.dst;
                    group[f] = b;
                }

                int chk = Find(1);
                for (int i = 2; i <= n; i++)
                {

                    int cur = Find(i);
                    if (chk == cur) continue;
                    ret = -1;
                    break;
                }
            }

            int Find(int _chk)
            {

                while(_chk != group[_chk])
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

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                q = new(n * n);
                ret = 0;
                int[] alphabet = new int[128];
                for (int i = 0; i < 26; i++)
                {

                    int idx1 = 'a' + i;
                    int idx2 = 'A' + i;

                    alphabet[idx1] = 1 + i;
                    alphabet[idx2] = 27 + i;
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        int dst = alphabet[sr.Read()];
                        if (dst == 0) continue;
                        ret += dst;
                        q.Enqueue((i, j, dst), dst);
                    }

                    if (sr.Read() == '\r') sr.Read();
                }
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c!=' ' && c != '\n')
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
public static string? Cin()=>reader.ReadLine();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static void Cin(out int num)=>num=int.Parse(Cin());
public static void Cin(out string str)=>str=Cin();
public static void Cin(out string a,out string b,char c=' '){var r=Cin().Split(c);a=r[0];b=r[1];}
public static void Cin(out int[] numarr,char c= ' ')=>numarr=Array.ConvertAll(Cin().Split(c),int.Parse);
public static void Cin(out string[] strarr,char c=' ')=>strarr=Cin().Split(c);
public static void Cin(out double d)=>d=double.Parse(Cin());
public static void Cin(out string t,out int n){var s=Cin().Split();n=int.Parse(s[1]);t=s[0];}
public static void Cin(out int a,out int b,char c= ' '){Cin(out int[] s);(a,b)=(s[0],s[1]);}
public static void Cin(out int a,out int b,out int c,char e=' '){Cin(out int[] s);(a,b,c)=(s[0],s[1],s[2]);}
public static void Cin(out int a,out int b,out int c,out int d,char e = ' '){Cin(out int[] arr,e);(a,b,c,d)=(arr[0],arr[1],arr[2],arr[3]);}
public static void Cin(out int n,out string t) {var s=Cin().Split();n=int.Parse(s[0]);t=s[1];}
public static void Cin<T>(Func<string,T> c,out T a,out T b) {var s=Cin().Split();a=c(s[0]);b=c(s[1]);}
public static void Cin<T>(Func<string,T> c,out T[] a){a=Array.ConvertAll(Cin().Split(),x=>c(x));}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    static int Parsing(char c) {
        if (c is '0') return 0;
        if (c <= 'Z') return c-'A'+27;
        return c-'a'+1;
    }
    record LineInfo(int a,int b,int len);
    public static void Coding() {
        PriorityQueue<LineInfo,int> queue = new();
        Cin(out int count);
        int sum=0;
        for(int y=0;y<count;y++) {
            Cin(out string str);
            for(int x=0;x<count;x++) {
                int n = Parsing(str[x]);
                if (n is 0) continue;
                sum += n;
                queue.Enqueue(new(x,y,n),n);
            }
        }
        int[] parent = Enumerable.Range(0,count).ToArray();
        int find(int x){
            int px = parent[x];
            return px == x ? x : parent[x] = find(px);
        }
        bool union(int a,int b) {
            (a,b)=(find(a),find(b));
            if(a == b) return false;
            parent[a]=parent[b]=Math.Min(a,b);
            return true;
        }
        int use = 0;
        while(queue.Count is not 0) {
            var ret = queue.Dequeue();
            if (union(ret.a,ret.b)) {
                use += ret.len;
            }
        }
        for(int x=1;x<count;x++) if(union(x,x-1)) {
            Cout = -1;
            return;
        }
        Cout = sum - use;
    }
}
#elif other2
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Main {

	public static void main(String[] args) throws Exception {
		
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		int N = Integer.parseInt(br.readLine());
		int[][] d = new int[N][N];
		int[] minEdge = new int[N];
		
		int[] num = new int['z' + 1];
		int start = 'A' - 1;
		for (int i = 1; i <= 26; i++) {
			num[start + i] = i + 26;
		}
		start = 'a' - 1;
		for (int i = 1; i <= 26; i++) {
			num[start + i] = i;
		}
		
		int sum = 0;
		for (int i = 0; i < N; i++) {
			String line = br.readLine();
			for (int j = 0; j < line.length(); j++) {
				int n = num[line.charAt(j)];
				if (n == 0)
					continue;
				if (d[i][j] == 0)
					d[i][j] = n;
				else
					d[i][j] = d[i][j] > n ? n : d[i][j];					
				if (d[j][i] == 0)
					d[j][i] = n;
				else
					d[j][i] = d[j][i] > n ? n : d[j][i];
				sum += n;
			}
			minEdge[i] = Integer.MAX_VALUE;
		}
		
		minEdge[0] = 0;
		boolean[] visited = new boolean[N];
		
		for (int c = 0; c < N; c++) {
			int min = Integer.MAX_VALUE;
			int minV = 0;
			boolean flag = false;
			
			for (int i = 0; i < N; i++) {
				if (!visited[i] && min > minEdge[i]) {
					min = minEdge[i];
					minV = i;
					flag = true;
				}
			}
			if (!flag) {
				System.out.println(-1);
				return;
			}
			
			visited[minV] = true;
			sum -= min;
			
			for (int i = 0; i < N; i++) {
				if (!visited[i] && d[minV][i] != 0 && d[minV][i] < minEdge[i])
					minEdge[i] = d[minV][i];
			}
		}
		System.out.println(sum);
	}//end main
}//end class
#endif
}
