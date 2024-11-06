using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 4
이름 : 배성훈
내용 : 팀 배정
    문제번호 : 18768번

    그리디, 정렬 문제다
    A, B 팀 인원차의 최대값 k가 주어지기에
    A, B 팀에 각각 들어갈 최소 인원이 주어진다

    최소 인원은 f = (A에서의 점수) - (B에서의 점수) 차이로 찾으면 된다
    f(a) > f(b)이면
    a를 A팀에 b를 B팀에 보내는게 a를 B팀에 b를 A팀에 보내는 것보다 좋다
    이는 계산해보면 된다
    
    이후 귀납적으로 접근하면
    f의 값이 작은 순서로 A팀에 우선적으로 배치하고 큰 순서로 B에 배치한다

    이후 중앙에 대해 양수면 A에 보내고, 
    음수면 B에 보내 점수 계산하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_1026
    {

        static void Main1026(string[] args)
        {

            int MAX = 100_000;
            StreamReader sr;
            (int val, int add)[] arr;
            Comparer<(int val, int add)> comp;
            int n, k;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 0; i < t; i++)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
            }

            void GetRet()
            {

                Array.Sort(arr, 0, n, comp);

                int min = (n - k) >> 1;
                if (((n - k) & 1) == 1) min++;
                int max = (n + k) >> 1;

                long ret = 0;

                for (int i = 0; i < min; i++)
                {

                    ret += arr[i].val;
                    ret += arr[n - 1 - i].val + arr[n - 1 - i].add;
                }

                for (int i = min; i < max; i++)
                {

                    if (arr[i].add < 0) ret += arr[i].val;
                    else ret += arr[i].val + arr[i].add;
                }

                Console.Write($"{ret}\n");
            }

            void Input()
            {

                n = ReadInt();
                k = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    arr[i].val = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    int b = ReadInt();
                    arr[i].add = b - arr[i].val;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                arr = new (int val, int add)[MAX];
                comp = Comparer<(int val, int add)>.Create((x, y) => x.add.CompareTo(y.add));
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n') 
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
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    record Stat(int A,int B) {
        public int Distance => Math.Abs(A-B);
    }
    public static void Coding() {
        for(Cin(out int tc);tc-->0;) {
            Cin(out int n,out int k);
            PriorityQueue<Stat,int> a_team = new();
            PriorityQueue<Stat,int> b_team = new();
            {
                Cin(out int[] A);
                Cin(out int[] B);
                for(int i=0;i<n;i++) {
                    Stat stat = new(A[i],B[i]);
                    //만약 A에 더 재능있으면
                    if (A[i] > B[i]) {
                        a_team.Enqueue(stat,stat.Distance);
                    }
                    //B가 더 재능있으면. (같아도 상관없음.)
                    else {
                        b_team.Enqueue(stat,stat.Distance);
                    }
                }
            }
            //둘의 차이를 조정
            for(int a_count,b_count;Math.Abs((a_count=a_team.Count) - (b_count=b_team.Count)) > k;) {
                //A가 더 많다면
                if (a_count > b_count) {
                    //a 하나 빼서 b에 넣기
                    var ret = a_team.Dequeue();
                    b_team.Enqueue(ret,ret.Distance);
                }
                //B가 더 많다면
                else {
                    //b 빼고 a
                    var ret = b_team.Dequeue();
                    a_team.Enqueue(ret,ret.Distance);
                }
            }
            // //설마?
            // if (a_team.Count == 0) {
            //     a_team.Enqueue(b_team.Dequeue(),0);
            // } else if (b_team.Count == 0) {
            //     b_team.Enqueue(a_team.Dequeue(),0);
            // }
            //합 구하기
            long sum=0;
            while(a_team.Count > 0) sum += a_team.Dequeue().A;
            while(b_team.Count > 0) sum += b_team.Dequeue().B;
            //출력
            Coutln = sum;
        }
    }
}
#elif other2
// #include <iostream>
// #include <vector>
// #include <algorithm>

using namespace std;

int main() {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);

	int T;
	cin >> T;
	for (int i = 0; i < T; i++) {
		int n, k;
		cin >> n >> k;
		vector<int> A;
		vector<int> B;
		for (int j = 0; j < n; j++) {
			int num;
			cin >> num;
			A.push_back(num);
		}
		for (int j = 0; j < n; j++) {
			int num;
			cin >> num;
			B.push_back(num);
		}

		long long answer = 0;
		int A_win = 0; //B보다 A가 큰 경우
		int B_win = 0;
		vector<int> sub;
		for (int j = 0; j < n; j++) {
			answer += max(A[j], B[j]);
			if (A[j] > B[j])
				A_win++;
			else
				B_win++;
			sub.push_back(A[j] - B[j]); // A와 B의 차이
		}
		
		if (max(A_win, B_win) - min(A_win, B_win) > k) {
			//A가 더 많을 때
			if (max(A_win, B_win) == A_win) {
				sort(sub.begin(), sub.end()); //오름차순 정렬
				//0이상인 위치 찾기
				int j = 0;
				for (j = 0; j < n; j++) {
					if (sub[j] > 0)
						break;
				}
				while (A_win - B_win > k) {
					answer -= sub[j];
					A_win--;
					B_win++;
					j++;
				}
			}
			//B가 더 많을 때
			else{
				sort(sub.begin(), sub.end(), greater<int>()); //내림차순 정렬
				//0이하인 위치 찾기
				int j = 0;
				for (j = 0; j < n; j++) {
					if (sub[j] <= 0)
						break;
				}
				while (B_win - A_win > k) {
					answer += sub[j];
					A_win++;
					B_win--;
					j++;
				}
			}
		}
		cout << answer << "\n";
	}

	return 0;
}
#endif
}
