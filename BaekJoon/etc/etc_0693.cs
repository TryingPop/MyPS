using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 14
이름 : 배성훈
내용 : 고기잡이
    문제번호 : 7573번

    브루트포스 문제다
    그리디 알고리즘을 섞어 4방향으로 고기를 잡을 수 있는지 판별했다
*/

namespace BaekJoon.etc
{
    internal class etc_0693
    {

        static void Main693(string[] args)
        {

            StreamReader sr;
            int n, l, m;
            (int r, int c)[] fish;
            int[] arr1;
            int[] arr2;

            Solve();

            void Solve()
            {

                Input();

                int ret = GetRet();

                Console.WriteLine(ret);
            }

            int GetRet()
            {

                int ret = 0;

                int len = l / 2;
                
                for (int w = 1; w < len; w++)
                {

                    int h = len - w;
                    if (w > n || h > n) continue;

                    int chk = Cnt(w, h);
                    ret = ret < chk ? chk : ret;
                }

                return ret;
            }

            int Cnt(int _w, int _h)
            {

                int ret = 0;

                int bX = -1;
                int bY = -1;

                for (int i = 0; i < m; i++)
                {

                    if (bX == arr1[i]) continue;

                    for (int j = 0; j < m; j++)
                    {

                        if (bY == arr2[j]) continue;

                        int chk = CntFish(_w, _h, arr1[i], arr2[j]);
                        ret = ret < chk ? chk : ret;

                        chk = CntFish(_w, _h, arr1[i] - _w, arr2[j]);
                        ret = ret < chk ? chk : ret;

                        chk = CntFish(_w, _h, arr1[i], arr2[j] - _h);
                        ret = ret < chk ? chk : ret;

                        chk = CntFish(_w, _h, arr1[i] - _w, arr2[j] - _h);
                        ret = ret < chk ? chk : ret;

                        bY = arr2[j];
                    }

                    bX = arr1[i];
                }

                return ret;
            }

            int CntFish(int _w, int _h, int _r, int _c)
            {

                int ret = 0;

                for (int i = 0; i < m; i++)
                {

                    if (fish[i].r < _r || _r + _w < fish[i].r) continue;
                    if (fish[i].c < _c || _c + _h < fish[i].c) continue;

                    ret++;
                }
                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                l = ReadInt();
                m = ReadInt();

                fish = new (int r, int c)[m];

                arr1 = new int[m];
                arr2 = new int[m];

                for (int i = 0; i < m; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();
                    fish[i] = (x, y);
                    arr1[i] = x;
                    arr2[i] = y;
                }

                Array.Sort(arr1);
                Array.Sort(arr2);

                sr.Close();
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
public class Main {
	static Reader in = new Reader();
	static int[][] fishes;
	static int N, I, M, ans;
	
	public static void main(String[] args) throws Exception {
		N = in.nextInt();
		I = in.nextInt() / 2;
		M = in.nextInt();
		
		fishes = new int[M][2];
		
		for(int i = 0; i < M; i++) {
			fishes[i][0] = in.nextInt() - 1;
			fishes[i][1] = in.nextInt() - 1;
		}
		
		for(int i = 0; i < M; i++) {
			for(int j = 0; j < M; j++) {
				for(int k = 1; k < I; k++) {
					if(fishes[j][0] < fishes[i][0] || fishes[j][0] > fishes[i][0] + k || fishes[i][1] < fishes[j][1] || fishes[i][1] > fishes[j][1] + I - k) continue;
					fishnet(fishes[i][0], fishes[j][1], k, I-k);
				}
			}
		}
		
		System.out.println(ans);
	}
	
	static void fishnet(int r, int c, int dr, int dc) {
		int cnt = 0;
		
		for(int i = 0; i < M; i++) {
			int fr = fishes[i][0];
			int fc = fishes[i][1];
			
			if(r <= fr && fr <= r + dr && c <= fc && fc <= c + dc) cnt++;
		}
		
		ans = Math.max(ans, cnt);
	}
	
	static class Reader {
		final int SIZE = 1 << 13;
		byte[] buffer = new byte[SIZE];
		int index, size;

		int nextInt() throws Exception {
			int n = 0;
			byte c;
			while ((c = read()) <= 32)
				;
			boolean neg = c == '-' ? true : false;
			if (neg)
				c = read();
			do
				n = (n << 3) + (n << 1) + (c & 15);
			while (isNumber(c = read()));
			if (neg)
				return -n;
			return n;
		}
		
		boolean isNumber(byte c) {
			return 47 < c && c < 58;
		}

		byte read() throws Exception {
			if (index == size) {
				size = System.in.read(buffer, index = 0, SIZE);
				if (size < 0)
					buffer[0] = -1;
			}
			return buffer[index++];
		}
	}
}
#endif
}
