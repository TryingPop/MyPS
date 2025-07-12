using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 식당 메뉴
    문제번호 : 26043번

    자료구조, 정렬, 큐 문제다
    두 포인터 알고리즘을 써서 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0272
    {

        static void Main272(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            // 음식 먹은거 확인 50만이라 큐 3개나 보다는
            // 50만 3번 탐색하고 배열 3개 설정 하는게 
            // 더 효율적이라 생각해서 배열 3개로 구현
            int[] ret = new int[n + 1];             // id에 따른 결과 -> 1 : 원하는거 먹었다, 2 : 밥은 먹었는데 원하는게 아니다, 3 : 굶었다, 0 : 유령
            int[] want = new int[n + 1];            // id에 따라 원하는 음식

            int[] line = new int[n];                // 줄에 서있는 학생 id
            int[] food = new int[3];                // 음식 <- 2개밖에 없다 1, 2
                                                    // 0, 1로 처리해도되나 매번 빼는거보다 그냥 1개 더 잡았다

            // 두 포인터 
            int curLineIdx = 0;                     // 현재 줄 idx
            int maxLine = 0;                        // 줄 선 사람 수
            int maxFood = 0;                        // 완성된 음식 개수
            for (int i = 0; i < n; i++)
            {

                int op = ReadInt(sr);

                if (op == 1)
                {

                    // 줄 세우기
                    // 학생 id
                    int id = ReadInt(sr);
                    // 먹고 싶은거
                    int select = ReadInt(sr);

                    // 먹고 싶은거 저장
                    want[id] = select;
                    // 줄에 idx저장해서 세운다
                    line[curLineIdx++] = id;
                    // 줄에 사람 있다고 표현
                    maxLine++;
                }
                else
                {

                    // 음식 완성
                    int comp = ReadInt(sr);
                    // 완성된 음식 추가
                    food[comp]++;

                    // 음식 있다고 표현
                    maxFood++;
                }

                if (maxLine > 0 && maxFood > 0)
                {

                    // 음식 있고 사람 있다
                    // 매번 확인하기에
                    // 줄에 사람이 1명이거나, 음식이 1개인 경우다!
                    // 1번만 연산하면 된다
                    // 맨 앞 사람 불러온다
                    int lIdx = curLineIdx - maxLine;
                    // 그 사람의 id 조사
                    int hIdx = line[lIdx];
                    // id로 먹고 싶은거 확인
                    int wantFood = want[hIdx];

                    if (food[wantFood] > 0)
                    {

                        // 먹고 싶은게 있는 경우
                        food[wantFood]--;
                        // 먹고 싶은걸 먹었다고 표현
                        ret[hIdx] = 1;
                    }
                    else
                    {

                        // 음식은 있지만 먹고싶은게 없는 경우
                        int otherFood = wantFood == 1 ? 2 : 1;
                        // 싫어도 먹어야한다!
                        food[otherFood]--;
                        // 싫은 음식 먹었다고 표현
                        ret[hIdx] = 2;
                    }

                    // 음식과 사람 1개씩 줄인다
                    maxLine--;
                    maxFood--;
                }
            }

            sr.Close();

            while(maxLine > 0)
            {

                // 줄은 섰는데 밥 못먹은 사람들
                // 굶었다고 표현
                int lIdx = curLineIdx - maxLine;
                int hIdx = line[lIdx];
                ret[hIdx] = 3;
                maxLine--;
            }


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                // 이제 원하는 음식 먹은 사람 id 순으로 출력
                bool isEmpty = true;
                for (int i = 1; i <= n; i++)
                {

                    if (ret[i] == 1)
                    {

                        isEmpty = false;
                        sw.Write(i);
                        sw.Write(' ');
                    }
                }

                if (isEmpty) sw.WriteLine("None");
                else sw.Write('\n');

                // 원하는 음식 못먹은 사람 id순으로 출력
                isEmpty = true;
                for (int i = 1; i <= n; i++)
                {

                    if (ret[i] == 2)
                    {

                        isEmpty = false;
                        sw.Write(i);
                        sw.Write(' ');
                    }
                }

                if (isEmpty) sw.WriteLine("None");
                else sw.Write('\n');

                // 굶은 사람 id순으로 출력
                isEmpty = true;
                for (int i = 1; i <= n; i++)
                {

                    if (ret[i] == 3)
                    {

                        isEmpty = false;
                        sw.Write(i);
                        sw.Write(' ');
                    }
                }

                if (isEmpty) sw.WriteLine("None");
                else sw.Write('\n');
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
from collections import deque
import sys
input = lambda: sys.stdin.readline().rstrip()
ints = lambda: map(int, input().split())

A = []
B = []
C = deque()

for _ in range(int(input())):
    t, *args = ints()
    if t == 1:
        a, b = args
        C.append((a, b))
    elif t == 2:
        a, b = C.popleft()
        (A if b == args[0] else B).append(a)
print(*sorted(A or [None]))
print(*sorted(B or [None]))
print(*sorted([i[0] for i in C] or [None]))
#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayDeque;

public class Main {

	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringBuilder sb = new StringBuilder();
		StringBuilder sb1 = new StringBuilder();
		ArrayDeque<Student> q = new ArrayDeque<>();
		int[] A = new int[500001]; //1:좋아하는 메뉴먹은 학생 2:싫어하는 메뉴먹은 학생 3:못먹은 학생
		int n = Integer.parseInt(br.readLine());
		String s;
		int tmp;
		while(n-->0) {
			s = br.readLine();
			if(s.charAt(0)=='1') {
				tmp = Integer.parseInt(s.substring(2, s.length()-2));
				q.add(new Student(tmp,s.charAt(s.length()-1)-'0'));
			}
			else {
				tmp = s.charAt(s.length()-1)-'0';
				if(q.peek().like!=tmp) A[q.peek().num] = 2; //유형 B
				else A[q.peek().num] = 1; //유형 A
				q.poll();
			}
		}
		//유형 C
		while(!q.isEmpty()) A[q.poll().num] = 3;
		for(int i=1; i<=500000; ++i) if(A[i]==1) sb1.append(i).append(" ");
		sb.append(sb1.length()==0?"None":sb1).append("\n");
		sb1.setLength(0);
		for(int i=1; i<=500000; ++i) if(A[i]==2) sb1.append(i).append(" ");
		sb.append(sb1.length()==0?"None":sb1).append("\n");
		sb1.setLength(0);
		for(int i=1; i<=500000; ++i) if(A[i]==3) sb1.append(i).append(" ");
		sb.append(sb1.length()==0?"None":sb1);
		System.out.print(sb);
	}
}
class Student {
	int num;
	int like;
	public Student(int num, int like) {
		this.num = num;
		this.like = like;
	}
}
#endif
}
