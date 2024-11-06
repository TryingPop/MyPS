using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 2
이름 : 배성훈
내용 : 0 만들기
    문제번호 : 7490번

    브루트 포스, 문자열, 구현, 백트래킹 문제다
    재귀 함수를 이용해 모든 경우의 수를 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_1016
    {

        static void Main1016(string[] args)
        {

            int MAX = 9;
            string OP = " +-";
            // ' ' -> '+' -> '-' -> 숫자 순서
            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] chk;
            int[] num;
            bool[] positive;
            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                int test = ReadInt();

                while (test-- > 0)
                {

                    n = ReadInt();

                    DFS();
                    sw.Write('\n');
                    sw.Flush();
                }

                sr.Close();
                sw.Close();
            }

            void DFS(int _depth = 1)
            {

                if (_depth == n)
                {

                    Calc();
                    return;
                }

                for (int i = 0; i < OP.Length; i++)
                {

                    chk[_depth * 2 - 1] = OP[i];
                    DFS(_depth + 1);
                }
            }

            void Calc()
            {

                for (int i = 0; i < n; i++)
                {

                    num[i] = 0;
                    positive[i] = true;
                }

                num[0] = 1;
                int idx = 0;
                for (int i = 0; i < n - 1; i++)
                {

                    int op = i * 2 + 1;
                    if (chk[op] == '+') num[++idx] = chk[op + 1] - '0';
                    else if (chk[op] == '-')
                    {

                        num[++idx] = chk[op + 1] - '0';
                        positive[idx] = false;
                    }
                    else num[idx] = num[idx] * 10 + chk[op + 1] - '0';
                }

                int sum = 0;
                for (int i = 0; i <= idx; i++)
                {

                    sum += positive[i] ? num[i] : -num[i];
                }

                if (sum != 0) return;

                for (int i = 0; i < n * 2 - 1; i++)
                {

                    sw.Write((char)chk[i]);
                }
                sw.Write('\n');
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                chk = new int[MAX * 2 - 1];
                num = new int[MAX];
                positive = new bool[MAX];

                for (int i = 0; i < MAX; i++)
                {

                    chk[i * 2] = i + '1';
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
using System.Diagnostics.CodeAnalysis;
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
public static void Loop(int n,Action<int> action){for(int i=0;i<n;i++)action(i);}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
record Value(string text,int now,int last) {
    public static readonly Value Start = new(string.Empty, 0, 1);
    public Value Add(int n) {
        string end = string.Join(' ',Math.Abs(last).ToString().ToCharArray());
        end = (last < 0 ? '-' : '+') + end;
        return new(
            text + end,
            now + last,
            n
        );
    } 
    public Value Merge(int n) => new(text, now, (last*10) + (last < 0 ? -n : n));
    public string? End() {
        var temp = this.Add(0);
        return temp.now is 0 ? temp.text[1..] : null; //앞에 +를 제거하기 위해 [1..]
    }
}
class Program {
    public static void Coding() {
        for (int tc = Cin; tc-->0; Cout = '\n')
        {
            int n = Cin;
            LinkedList<string> results = new();
            void dfs(int index,in Value value) {
                //끝
                if (index > n) {
                    if (value.End() is string ret) results.AddLast(ret);
                    return;
                } 
                //탐색
                dfs(index+1,value.Merge(index)); //공백
                dfs(index+1, value.Add(index)); //덧셈
                dfs(index+1, value.Add(-index)); //뺄셈
            }

            dfs(2,Value.Start);
            Coutln = string.Join('\n',results);
        }
    }
}
#elif other2
// #include <stdio.h>

int n;
int a[10];
char *x = " +-";

void Search(int c) {

	if (c == n) {

		int v = 0;

		int t = 1;

		int p = 1;

		for (int i = 2; i <= n; i++) {

			if (a[i - 1] == 0) {

				t = t * 10 + i;

			}
			else {

				if (p == 1)v += t;
				else
					v -= t;

				t = i;

				p = a[i - 1];

			}

		}

		if (t) {
			if (p == 1)v += t;
			else
				v -= t;
		}

		if (v == 0) {
			for (int i = 1; i <= n; i++) {
				printf("%d", i);
				if (i + 1 <= n)printf("%c", x[a[i]]);
			}printf("\n");
		}
        
		return;
	}

	for (int i = 0; i < 3; i++) {

		a[c] = i;

		Search(c + 1);

	}

}

int main() {

	int t;
	scanf("%d", &t);

	while (t--) {

		scanf("%d", &n);

		Search(1);

		printf("\n");

	}

}
#endif
}
