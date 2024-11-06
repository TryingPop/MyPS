using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 1차원 2048
    문제번호 : 27514번

    구현, 그리디, 시뮬레이션 문제다
    그리디하게 접근해서 풀었다
    중앙에 뭐가 있던, 건너서 합성 가능하므로 카운팅하고
    한방에 합성시켰다

    그리고 정답이 2^62를 보장하므로 그냥 62까지만 계산하는 코드로 구현해 제출하니
    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0588
    {

        static void Main588(string[] args)
        {

            StreamReader sr;
            Dictionary<long, int> nTi;
            Dictionary<int, long> iTn;

            Solve();

            void Solve()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);

                SetDict();

                long n = ReadLong();
                int[] arr = new int[64];

                for (int i = 0; i < n; i++)
                {

                    long cur = ReadLong();
                    arr[nTi[cur]]++;
                }

                sr.Close();

                for (int i = 1; i < 63; i++)
                {

                    arr[i + 1] += arr[i] / 2;
                    arr[i] %= 2;
                }

                int ret = 0;
                for (int i = 63; i >= 1; i--)
                {

                    if (arr[i] == 0) continue;
                    ret = i;
                    break;
                }

                Console.WriteLine(iTn[ret]);
            }

            void SetDict()
            {

                nTi = new(64);
                iTn = new(64);

                nTi[0] = 0;
                iTn[0] = 0;

                long key = 1;
                for (int i = 1; i < 64; i++)
                {

                    nTi[key] = i;
                    iTn[i] = key;
                    key *= 2;
                }
            }

            long ReadLong()
            {

                int c;
                long ret = 0;

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
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.StringTokenizer;

public class Main {
	static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	static BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
	static StringTokenizer st;

	public static void main(String[] args) throws Exception {
		int n = Integer.parseInt(br.readLine());
		long sum = 0L;
		st = new StringTokenizer(br.readLine());
		for(int i=0;i<n;i++){
			sum += Long.parseLong(st.nextToken());
		}
		for(long l = 1L<<62;l>=1;l>>=1){
			if((sum&l)!=0L){
				bw.write(Long.toString(l));
				break;
			}
		}
		bw.flush();
	}
}
#endif
}
