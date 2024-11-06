using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 4
이름 : 배성훈
내용 : 동방 프로젝트 (Small, Large)
    문제번호 : 14594, 14595번

    분리집합, 스위핑 문제다
    누적합과 그리디로 풀었다

    아이디어는 다음과 같다
    처음과 끝을 합이 0이되게 기입한다
    0이 되는 순간 계층이 나뉘는 끝점으로 보면 된다
    이렇게 0이되는 순간을 세어 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0677
    {

        static void Main677(string[] args)
        {

            StreamReader sr;
            int[] arr;
            int n;

            Solve();

            void Solve()
            {

                Input();

                int ret = 0;
                int chk = 0;
                for (int i = 1; i <= n; i++)
                {

                    chk += arr[i];
                    if (chk == 0) ret++;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                n = ReadInt();
                arr = new int[n + 1];

                int len = ReadInt();
                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    arr[f]++;
                    arr[b]--;
                }

                sr.Close();
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
import java.io.*;
import java.util.*;

public class Main {
	static BufferedReader br;
	static StringTokenizer st;

	public static void main(String[] args) throws IOException {
		br = new BufferedReader(new InputStreamReader(System.in));
		int n = Integer.parseInt(br.readLine());
		int m = Integer.parseInt(br.readLine());
		
		p[] arr = new p[m+1];
		for(int i=0;i<m;i++) {
			st = new StringTokenizer(br.readLine());
			arr[i] = new p(Integer.parseInt(st.nextToken()),Integer.parseInt(st.nextToken()));
		}
		arr[m] = new p(n,n);
		
		Arrays.sort(arr);
		
		int r = 0;
		int cnt = 0;
		for(int i=0;i<=m;i++) {
			int curl = arr[i].l;
			int curr = arr[i].r;
			
			if(r < curl) {
				cnt += curl-r-1;
				cnt++;
				r = curr;
			}
			else r = Math.max(r, curr);
		}
		System.out.println(cnt);
	}
	
	static class p implements Comparable<p>{
		int l,r;
		public p(int l,int r) {
			this.l = l;
			this.r = r;
		}
		@Override
		public int compareTo(p o) {
			return this.l-o.l;
		}
	}
}
#endif
}
