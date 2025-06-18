using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 호반우가 학교에 지각한 이유 3
    문제번호 : 30470번

    스택, 자료구조, 구현, 시뮬레이션 문제다
    풀이 아이디어가 안떠올라 풀이를 갈피를 못잡다가
    스택이라는 힌트를 보고 풀게되었다

    자료구조는 따로 만드는거보다 있는걸 써서 풀었다
    사용한건 우선순위 큐이다 

    주된 아이디어는 다음과 같다
    나무를 넣을 때 가장 높은걸 맨 앞에 오게한다
    넣을 때 갯수는 1이다

    그리고 마법으로 나무를 잘라야할 때,
    큰 것부터 꺼낸다

    그리고 개수를 세면서 해당 나무를 모두 조건의 크기로 만들어
    넣는다

    그런데, 매번 개수만큼 넣기에는 시간이 오래 걸려
    개수를 저장하는 메모리를 써서

    넣을 때, 꺼낸 갯수를 기록하고 높이에 합친 개수를 해서 1번만 넣었다
    나중에 꺼낼 때도 개수만큼 세어줘야한다

    그리고 나무가 없는 경우나 자른 나무가 0인 경우 넣지않고
    바로 탈출하는 코드도 작성했다

    이렇게 제출하니 344ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0296
    {

        static void Main296(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            PriorityQueue<(int height, int cnt), int> q = new(n, Comparer<int>.Create((x, y) => y.CompareTo(x)));
            // Peek()을 쓰기에 하나 넣고 시작한다
            q.Enqueue((0, 0), 0);
            for (int i = 0; i < n; i++)
            {

                int type = ReadInt(sr);

                if (type == 1)
                {

                    // 넣을 땐, 1개만 넣는다
                    int height = ReadInt(sr);
                    q.Enqueue((height, 1), height);
                }
                else
                {

                    // 나무 자르기
                    int magic = ReadInt(sr);
                    // 아무것도 없으면 그냥 넘긴다
                    if (q.Peek().height == 0) continue;

                    // 최대 높이의 나무로 마법 높이 계산
                    int max = q.Peek().height;
                    int cnt = 0;

                    int inf = max - magic;
                    // 0인 경우면 꺼내기만 하고 넘길 생각이다
                    inf = inf < 0 ? 0 : inf;
                    while(q.Peek().height != 0 && q.Peek().height >= inf)
                    {

                        var node = q.Dequeue();
                        cnt += node.cnt;
                    }

                    if (inf == 0) continue;
                    // 마법으로 만든 높이가 0 이 아닌 경우
                    // 바꾼 높이와 개수를 넣는다
                    q.Enqueue((inf, cnt), inf);
                }
            }
            sr.Close();

            long ret = 0;
            while(q.Count > 0)
            {

                var node = q.Dequeue();
                long calc = node.height;
                calc *= node.cnt;

                ret += calc;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
//통나무
class Log {
	int len;
	long cnt;
	
	public Log(int len, long cnt) {
		this.len = len;
		this.cnt = cnt;
	}
}
public class Main {

	static Log[] log;
	static int sz=0, top=-1;
	
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		int N = Integer.parseInt(br.readLine());
		log = new Log[N+1];
		while(N-->0) {
			String s = br.readLine();
			char a = s.charAt(0);
			int b = Integer.parseInt(s.substring(2));
			if(a=='1') push(b,1);
			else {
				if(sz==0) continue;
				int tmp = Math.max(log[top].len-b, 0);
				if(tmp==0) {
					clear();
					continue;
				}
				int cnt = 0;
				while(sz!=0 && log[top].len > tmp) {
					cnt += log[top].cnt;
					pop();
				}
				if(cnt!=0) push(tmp, cnt);
			}
		}
		long sum = 0;
		for(int i=0; i<=top; ++i) {
			sum += log[i].len * log[i].cnt;
		}
		System.out.print(sum);
	}
	static void push(int n, int c) {
		log[++top] = new Log(n,c);
		sz++;
	}
	static void pop() {
		top--;
		sz--;
	}
	static void clear() {
		top = -1;
		sz = 0;
	}
}
#endif
}
