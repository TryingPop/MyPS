using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 22
이름 : 배성훈
내용 : 반복 패턴
    문제번호 : 166229번

    문자열, z알고리즘이다
    z알고리즘을 제대로 이해 못해서 이상하게 계속 틀렸다
    
    z 알고리즘의 값은 i번째부터 시작하는 부분 접미사에 대해
    전체 문자열과 앞에서부터 연속으로 일치하는 최장 길이를 저장한다

    그래서 해당 문제에서는 z배열의 값과 해당 접미사의 길이가 일치하지 않으면
    그 접미사는 의미 없는 접미사이니 넘겨야한다

    이후 반복이 처음부터 끝까지 같아야하기에
    abab 는 2개의 경우 만족한다

    ababa 같이 부분적으로 이어져 있는건 2, 4의 경우 가능한데
    끝이 확실하게 맺음이 되는지 확인해야함을 주의하자!
*/

namespace BaekJoon._56
{
    internal class _56_04
    {

        static void Main4(string[] args)
        {

            int n, k;
            int[] zArr;
            int[] str;

            Solve();

            void Solve()
            {

                Input();

                int ret = GetRet();

                Console.Write(ret);
            }

            int GetRet()
            {

                zArr = Z();

                if (k >= n) return n;
                int max = n + k;
                int ret = 0;

                for (int i = 1; i < zArr.Length; i++)
                {

                    if (n - i != zArr[i]) continue;
                    int chk = i * ((n - 1) / i) + i;
                    if (max < chk) continue;

                    if (ret < i) ret = i;
                }

                return ret;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                string[] temp = sr.ReadLine().Split();

                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);

                string chk = sr.ReadLine().Trim();
                str = new int[n];

                for (int i = 0; i < n; i++)
                {

                    str[i] = chk[i];
                }

                sr.Close();
            }

            int[] Z()
            {

                int[] arr = new int[n];

                int l = 0;
                int r = 0;

                arr[0] = n;
                for (int i = 1; i < n; i++)
                {

                    if (i <= r) arr[i] = Math.Min(r - i, arr[i - l]);

                    while (i + arr[i] < n && str[i + arr[i]] == str[arr[i]])
                    {

                        arr[i]++;
                    }

                    if (i > r) l = i;
                    r = Math.Max(r, i + arr[i] - 1);
                }

                return arr;
            }
        }
    }

#if other
// #include <stdio.h>
// #include <string.h>

int N;
int Z[100000];
char str[100001];

void z()
{
	int L = 0, R = 0, i, j;

	Z[0] = N;
	for(i=1;i<N;i++)
	{
		if(i > R)
		{
			L = i;
			R = i;
			
			while(R < N && str[R - L] == str[R])
			{
				R++;
			}
			
			Z[i] = R - L;
			R--;
		}
		else
		{
			j = i - L;
			
			if(Z[j] < R - i + 1)
			{
				Z[i] = Z[j];
			}
			else
			{
				L = i;
				while(R < N && str[R - L] == str[R])
				{
					R++;
				}
				
				Z[i] = R - L;
				R--;
			}
		}
	}
}

int main()
{
	int K, flag, ans = 0, i, j;

	scanf("%d %d %s", &N, &K, str);

	z();

	if(N <= K)
	{
		ans = N;
	}
	else
	{
		for(i=(N+K)/2;i>=1;i--)
		{
			flag = 1;
	
			for(j=0;j<N;j+=i)
			{
				if(Z[j] != N - j)
				{
					flag = 0;
					break;
				}
			}
	
			if(flag && j <= N + K)
			{
				ans = i;
				break;
			}
		}
	}

	printf("%d\n", ans);

	return 0;
}
#elif other2
import java.util.*;
import java.io.*;

public class Main {
    static int[] getPi(String str) {
        int len = str.length();
        int[] pi = new int[len];
        int j = 0;
        for (int i = 1; i < len; i++) {
            while (j > 0 && str.charAt(i) != str.charAt(j)) {
                j = pi[j - 1];
            }
            if (str.charAt(i) == str.charAt(j))
                pi[i] = ++j;
        }
        return pi;
    }

    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());
        int n = Integer.parseInt(st.nextToken());
        int k = Integer.parseInt(st.nextToken());
        String str = br.readLine();

        int answer = 0;
        if (n <= k) {
            answer = n;
        } else {
            int pLen = 0, pCnt;
            int[] pi = getPi(str);
            int cur = n-1;
            while(true) {
                pLen = n - pi[cur];
                pCnt = (n + k) / pLen;
                if(pCnt >= 2 && pLen*pCnt >= n)
                    answer = Math.max(answer, pLen);
                if (2*pi[cur] <= cur+1) break;
                cur = pi[cur]-1;
            }
        }
        System.out.println(answer);
    }
}
#endif
}
