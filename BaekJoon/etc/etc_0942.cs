using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 4
이름 : 배성훈
내용 : Programmer, Rank Thyself
    문제번호 : 4644번

    수학, 구현, 정렬 문제다
    sb를 써서 문자열을 읽어왔다
    그래서 using System.Text 구문이 필요하고
    이를 안해 한 번 틀렸다

    이후 출력 형식으로 1번 틀렸다
    이후 예제 출력을 보고 간격을 맞추니 이상없이 통과했다
    문자열 보간에는 ,?로 조건에 맞췄다
*/

namespace BaekJoon.etc
{
    internal class etc_0942
    {

        struct Team : IComparable<Team>
        {

            public string name;
            public int[] arr;
            public int exp;
            public int total;
            public int len;

            public void Input(string _name, int _s1, int _s2, int _s3, int _s4, int _s5, int _s6, int _s7)
            {

                if (arr == null) arr = new int[7];
                name = _name;
                arr[0] = _s1;
                arr[1] = _s2;
                arr[2] = _s3;
                arr[3] = _s4;
                arr[4] = _s5;
                arr[5] = _s6;
                arr[6] = _s7;

                GetEXP();
            }

            private void GetEXP()
            {

                len = 0;
                total = 0;
                for (int i = 0; i < 7; i++)
                {

                    if (arr[i] == 0) continue;
                    len++;
                    total += arr[i];
                }

                if (len == 0)
                {

                    exp = 1_000_000_000;
                    return;
                }

                double e = 1.0;
                for (int i = 0; i < 7; i++)
                {

                    if (arr[i] == 0) continue;
                    e *= arr[i];
                }

                e = Math.Pow(e, 1.0 / len);
                exp = (int)(e + 0.5 + 1e-9);
            }

            public override string ToString()
            {

                int e = exp == 1_000_000_000 ? 0 : exp;
                return $" {name,-10} {len} {total,4} {e,3} {arr[0],3} {arr[1],3} {arr[2],3} {arr[3],3} {arr[4],3} {arr[5],3} {arr[6],3}\n";
            }

            public bool IsSame(Team _other)
            {

                return _other.len == len && _other.total == total && _other.exp == exp;
            }

            public int CompareTo(Team _other)
            {

                if (_other.len != len) return _other.len.CompareTo(len);
                else if (_other.total != total) return total.CompareTo(_other.total);
                else if (_other.exp != exp) return exp.CompareTo(_other.exp);
                return name.CompareTo(_other.name);
            }
        }

        static void Main942(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            StringBuilder sb;

            Team[] teams;
            int n;

            Solve();
            void Solve()
            {

                Init();

                int t = 0;
                while(Input())
                {

                    sw.Write($"CONTEST {++t}\n");
                    Array.Sort(teams, 0, n);
                    int rank = 1;
                    int add = 0;

                    sw.Write($"{rank:00}{teams[0]}");

                    for (int i = 1; i < n; i++)
                    {

                        if (teams[i - 1].IsSame(teams[i]))
                        {

                            add++;
                        }
                        else
                        {

                            rank += add + 1;
                            add = 0;
                        }

                        sw.Write($"{rank:00}{teams[i]}");
                    }
                }

                sr.Close();
                sw.Close();
            }

            bool Input()
            {

                n = ReadInt();
                if (n == 0) return false;

                for (int i = 0; i < n; i++)
                {

                    string name = ReadString();
                    teams[i].Input(name, ReadInt(), ReadInt(), ReadInt(), ReadInt(), ReadInt(), ReadInt(), ReadInt());
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                teams = new Team[20];
                sb = new(20);
            }

            string ReadString()
            {

                sb.Clear();
                int c;
                while((c = sr.Read()) != ' ')
                {

                    sb.Append((char)c);
                }

                return sb.ToString();
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
// #include <stdio.h>
// #include <stdlib.h>
// #include <math.h>
// #include <string.h>

typedef struct team {
    char name[16];
    int svd, sum, aveg;
    int prob[7];
} team;

team tm[32];

int cmp1(const void* a, const void* b) {
    team ai = *(team*)a;
    team bi = *(team*)b;
    if (ai.svd != bi.svd) return ai.svd < bi.svd ? 1 : -1;
    if (ai.sum != bi.sum) return ai.sum > bi.sum ? 1 : -1;
    if (ai.aveg != bi.aveg) return ai.aveg > bi.aveg ? 1 : -1;
    return strcmp(ai.name, bi.name);
}

int main(void) {
    int n, r;
    double gsum;
    //freopen("E:\\PS\\ICPC\\North America\\Mid Central\\mcpc2002\\rank\\rank.in", "r", stdin);
    //freopen("E:\\PS\\ICPC\\North America\\Mid Central\\mcpc2002\\rank\\rank_t.out", "w", stdout);
    for (int tt = 1;; tt++) {
        scanf("%d", &n);
        if (n == 0) break;
        for (int i = 0; i < n; i++) {
            scanf("%s", tm[i].name);
            gsum = 0.0;
            for (int j = 0; j < 7; j++) {
                scanf("%d", &tm[i].prob[j]);
                tm[i].sum += tm[i].prob[j];
                if (tm[i].prob[j] > 0) {
                    tm[i].svd++;
                    gsum += log(tm[i].prob[j]);
                }
            }
            if (tm[i].svd > 0) {
                gsum /= tm[i].svd;
                tm[i].aveg = exp(gsum) + 0.500000001;
            }
        }
        qsort(tm, n, sizeof(team), cmp1);
        printf("CONTEST %d\n", tt);
        r = 1;
        for (int i = 0; i < n; i++) {
            if (i > 0 && (tm[i].svd != tm[i - 1].svd || tm[i].sum != tm[i - 1].sum ||
                tm[i].aveg != tm[i - 1].aveg)) r = i + 1;
            printf("%02d %-10s %1d %4d %3d ", r, tm[i].name, tm[i].svd, tm[i].sum, tm[i].aveg);
            for (int j = 0; j < 7; j++) {
                printf("%3d%c", tm[i].prob[j], j == 6 ? '\n' : ' ');
            }
        }
        for (int i = 0; i < n; i++) {
            tm[i].aveg = 0, tm[i].sum = 0, tm[i].svd = 0;
        }
    }
    return 0;
}
#elif other2
import java.io.*;
import java.util.*;
public class Main {
	public static void main(String[] args) throws IOException	{
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		int casea = 0;
		while(true){
			casea++;
			int num = Integer.parseInt(br.readLine());
			if(num == 0)
				break;
			System.out.println("CONTEST " + casea);
			PriorityQueue<State> q = new PriorityQueue<State>();
			while(num-- > 0)	{
				StringTokenizer st = new StringTokenizer(br.readLine());
				String name = st.nextToken();
				ArrayList<Integer> list = new ArrayList<Integer>();
				while(st.hasMoreTokens())	{
					list.add(Integer.parseInt(st.nextToken()));
				}
				q.add(new State(list, name));
			}
			State last = null;
			int rank = 1;
			for(int curr = 1; !q.isEmpty(); curr++)	{
				State next = q.poll();
				if(last == null || !(last.solved == next.solved && last.g == next.g && last.total == next.total))	{
					rank = curr;
				}
				String rankIn = Integer.toString(rank);
				while(rankIn.length() < 2)
					rankIn = '0' + rankIn;
				last = next;
				System.out.println(rankIn + " " + next);
			}
		}
	}
	static class State implements Comparable<State> {
		public ArrayList<Integer> list;
		public int solved;
		public int total;
		public int g;
		public String name;
		public State(ArrayList<Integer> go, String a)	{
			name = new String(a);
			list = new ArrayList<Integer>();
			total = 0;
			solved = 0;
			double mean = 0;
			for(int out: go)	{
				list.add(out);
				if(out != 0)	{
					total += out;
					solved++;
					mean += Math.log(out);
				}
			}
			mean /= solved;
			g = (int) Math.round(Math.exp(mean));
		}
		public int compareTo(State s)	{
			if(solved != s.solved)
				return s.solved - solved;
			if(total != s.total)
				return total - s.total;
			if(g != s.g)
				return g-s.g;
			return name.compareTo(s.name);
		}
		public String toString()	{
			StringBuilder sb = new StringBuilder();
			String nameIn = name;
			while(nameIn.length() < 10)
				nameIn += ' ';
			sb.append(nameIn);
			sb.append(' ');
			sb.append(solved);
			sb.append(' ');
			String nextIn = Integer.toString(total);
			while(nextIn.length() < 4)
				nextIn = ' ' + nextIn;
			sb.append(nextIn);
			nextIn = Integer.toString(g);
			while(nextIn.length() < 4)
				nextIn = ' ' + nextIn;
			sb.append(nextIn);
			
			for(int out: list)	{
				nextIn = Integer.toString(out);
				while(nextIn.length() < 4)
					nextIn = ' ' + nextIn;
				sb.append(nextIn);
			}
			return sb.toString();
		}
	}
}
#endif
}
