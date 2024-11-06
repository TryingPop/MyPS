using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 주작 주 주작
    문제번호 : 12792번

    수학, 정수론 문제다
    arr이 1-1대응인 줄 알고 최솟값을 찾는 식으로 했는데
    1 - 1대응이 아닌 경우가 존재해(16% 부근) 시간 초과로 2번 틀렸다

    그리고, 조금 고민을 하니 구지 최소값을 찾아야하나? 생각이 들었다
    그냥 적당히 2 ~ 20억 사이의 아무 수나 제출하면 된다기에 소수의 특징을 이용했다
    범위가 100만이므로, 100만 이상의 수 중에 아무 소수인 100만 3을 제출하니 이상없이 통과했다

    다른 사람 풀이를 확인해보니 똑같은 풀이가 존재했다
*/

namespace BaekJoon.etc
{
    internal class etc_0663
    {

        static void Main663(string[] args)
        {

            StreamReader sr;
            int n;
            int ret;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                n = ReadInt();
                ret = 0;

                for (int i = 1; i <= n; i++)
                {

                    int cur = ReadInt();
                    if (cur == i)
                    {

                        ret = -1;
                        break;
                    }
                }

                sr.Close();

                if (ret == -1) Console.WriteLine(-1);
                else Console.WriteLine(1_000_003);
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
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {

	public static void main(String[] args) throws Exception {
		
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		int n = Integer.parseInt(br.readLine());
		StringTokenizer st = new StringTokenizer(br.readLine());
		for(int i = 1;i<=n;i++)
			if(i == Integer.parseInt(st.nextToken())) {
				System.out.println(-1);
				return;
			}

		System.out.println(1000003);
	}

}
#elif other2
// #include<cstdio>
char buf[1 << 18];
int idx, nidx;
static inline char read()
{
	if (idx == nidx)
	{
		nidx = fread(buf, 1, 1 << 18, stdin);
		idx = 0;
	}
	return buf[idx++];
}
static inline int readInt()
{
	int sum = 0;
	char now = read();
	while (now == ' ' || now == '\n')
		now = read();
	while (now != ' '&&now != '\n')
	{
		sum *= 10;
		sum += now - '0';
		now = read();
	}
	return sum;
}
int main()
{
	int N,t;
	scanf("%d", &N);
	for (int i = 1; i <= N; i++)
	{
		t = readInt();
		if (i == t)
		{
			printf("-1");
			return 0;
		}
	}
	printf("1000003");
}
#endif
}
