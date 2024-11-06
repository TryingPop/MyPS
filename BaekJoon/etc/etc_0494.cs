using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : 패턴
    문제번호 : 3164번

    수학, 애드혹 문제다
    검은 점이 - 의 경우와 | 의 경우 그리고 / 경로에 있는 있는 3가지 경우로 나눴다
    서로 맞물리는 점이 존재한다!
    잘 분석하면 2 ~ 3개의 식으로 정답을 바로 내릴 수 있을거 같다

    그리고 100만 범위까지 들어오므로 최대값은 100만^2 = 10^12의 절반에 근접하므로 int범위를 쉽게 벗어난다!
*/

namespace BaekJoon.etc
{
    internal class etc_0494
    {

        static void Main494(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            input[2]--;
            input[3]--;

            long ret = 0;
            int s1 = 2 * (input[0] / 2) + 1;
            for (int i = s1; i <= input[2]; i += 2)
            {

                // 세로 세기 |
                int add = Math.Min(input[3], i) - Math.Min(input[1] - 1, i);
                ret += add;
            }

            int s2 = 2 * (input[1] / 2) + 1;
            for (int i = s2; i <= input[3]; i += 2)
            {

                // 가로 세기 -
                int add = Math.Min(input[2], i) - Math.Min(input[0] - 1, i);
                ret += add;
            }

            int e = Math.Min(input[2], input[3]);
            int s = Math.Max(s1, s2);
            for (int i = s; i <= e; i+= 2)
            {

                // 대각선 / 중복되는 경우이므로 뺀다
                ret--;
            }

            Console.WriteLine(ret);
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
using namespace std;
int main () {
	int sx,sy,lx,ly;
	scanf("%d %d %d %d",&sx,&sy,&lx,&ly);
	lx--,ly--;
	int M=max(sx,sy);
	int MM=max(lx,ly);
	if(M%2==0) ++M;
	long long ans=0;
	for(int i=M;i<=MM;i+=2){
		if(i<=lx && i<=ly) ans+=(i-sx+i-sy+1);
		else if(i<=lx) ans+=(ly-sy+1);
		else if(i<=ly) ans+=(lx-sx+1);
	}
	printf("%lld",ans);
	return 0;
}
#elif other2
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {
    private void solution() throws Exception {
        StringTokenizer st = new StringTokenizer(new BufferedReader(new InputStreamReader(System.in)).readLine());
        int x1 = Integer.parseInt(st.nextToken());
        int y1 = Integer.parseInt(st.nextToken());
        int x2 = Integer.parseInt(st.nextToken());
        int y2 = Integer.parseInt(st.nextToken());

        long sum = 0;
        for (int i = y1%2==1?y1+1:y1+2; i <= y2; i+=2) {
            if (i <= x1) continue;
            sum += Math.min(i, x2) - x1;
        }
        for (int i = x1%2==1?x1+1:x1+2; i <= x2; i+=2) {
            if (i-1 <= y1) continue;
            sum += Math.min(i-1, y2) - y1;
        }
        System.out.println(sum);
    }

    public static void main(String[] args) throws Exception {
        new Main().solution();
    }
}
#endif
}
