using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 배열 돌리기 5
    문제번호 : 17470번

    구현 문제다
    아이디어는 다음과 같다
    회전 명령이 최대 200만번 입력되기에 매회 회전 시키면 시간 초과 날 것이다 (2500 * 200만 = 50억)
    그래서 입력된 명령과 같은 모양이면서 회전 시행 횟수가 적은 명령으로 변환해야한다

    여기서 명령을 다음과 같이 명명하자
        LRS : 좌우 대칭 (left - right symmetry ? 의미로 변수 명을 썼다)
        UDS : 상하 대칭 (up - down symmetry ? 의미로 변수 명을 썼다)
        LRR : 시계방향으로 90도 회전 (left - right rotation ? 의미로 변수 명을 썼다)
        QR  : 4등분으로 구간을 그룹화한 뒤 시계방향으로 90도 회전 (quater rotation ? 의미로 변수 명을 썼다)

    이렇게 명명하면, 문제에서 입력되는 숫자와 매칭시키면 LRS 는 2, UDS 는 1, LRR은 3, QR은 5가 된다

    그리고 A * B 로 A연산 후 B 연산한다는 의미로 *를 쓴다
    예를들어 LRS * LRS * QR 라하면, LRS(좌우 대칭) 이후 LRS(좌우 대칭)을 하고, QR(4등분 회전) 연산을 하는 것이다
    여기서는 2, 2, 5가 입력되는 것과 같다

    여기서 명령들의 관해 보자
        LRS * LRS, UDS * UDS, LRR * LRR * LRR * LRR, QR * QR * QR * QR 는 초기 형태와 같다

    그리고 LRS * UDS = UDS * LRS 가 성립한다
    이렇게 연산들의 관계를 찾았다
        QR * LRR = LRR * QR, 
        UDS * QR = QR * QR * QR * UDS, UDS * LRR = LRR * LRR * LRR * UDS(UDS를 LRS로 바꿔도 같은 규칙!)

    이러한 규칙을 찾았다
        그래서 규칙이 주어지면, LRS, UDS, LRR, QR 순으로 명령이 되게 변경하고
    주기로 나눈 나머지로 값을 변경해 연산 수를 줄였다
    이렇게 제출하니, 112ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0604
    {

        static void Main604(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[][][] board;
            int row, col;
            int[] op;

            Solve();

            void Solve()
            {

                Input();
                Move();
                Output();
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);

                row = ReadInt();
                col = ReadInt();

                int opLen = ReadInt();

                board = new int[2][][];
                board[0] = new int[row][];
                board[1] = new int[col][];
                for (int r = 0; r < row; r++)
                {

                    board[0][r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[0][r][c] = ReadInt();
                    }
                }

                for (int c = 0; c< col; c++)
                {

                    board[1][c] = new int[row];
                }


                op = new int[4];
                for (int i = 0; i < opLen; i++)
                {

                    int cur = ReadInt();

                    if (cur == 1)
                    {

                        op[0] = (op[0] + 1) % 2;
                        op[2] = (op[2] * 3) % 4;
                        op[3] = (op[3] * 3) % 4;
                    }
                    else if (cur == 2)
                    {

                        op[1] = (op[1] + 1) % 2;
                        op[2] = (op[2] * 3) % 4;
                        op[3] = (op[3] * 3) % 4;
                    }
                    else if (cur == 3) op[2] = (op[2] + 1) % 4;
                    else if (cur == 4) op[2] = (op[2] + 3) % 4;
                    else if (cur == 5) op[3] = (op[3] + 1) % 4;
                    else op[3] = (op[3] + 3) % 4;
                }
                sr.Close();
            }

            void Move()
            {

                if (op[0] == 1)
                {

                    op[0] = 0;
                    for (int c = 0; c < col; c++)
                    {

                        for (int r = 0; r < row; r++)
                        {

                            int other = row - 1 - r;

                            if (r > other) break;

                            int temp = board[0][r][c];
                            board[0][r][c] = board[0][other][c];
                            board[0][other][c] = temp;
                        }
                    }
                }

                if (op[1] == 1)
                {

                    op[1] = 0;
                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            int other = col - 1 - c;
                            if (c > other) break;

                            int temp = board[0][r][c];
                            board[0][r][c] = board[0][r][other];
                            board[0][r][other] = temp;
                        }
                    }
                }

                if (op[2] > 0)
                {

                    for (int i = 0; i < op[2]; i++)
                    {

                        for (int r = 0; r < row; r++)
                        {

                            for (int c = 0; c < col; c++)
                            {

                                board[1][c][row - 1 - r] = board[0][r][c];
                            }
                        }

                        int temp1 = row;
                        row = col;
                        col = temp1;

                        int[][] temp2 = board[0];
                        board[0] = board[1];
                        board[1] = temp2;
                    }

                    op[2] = 0;
                }

                if (op[3] > 0)
                {

                    int halfR = row / 2;
                    int halfC = col / 2;
                    for (int i = 0; i < op[3]; i++)
                    {

                        for (int r = 0; r < halfR; r++)
                        {

                            for (int c = 0; c < halfC; c++)
                            {

                                int temp = board[0][r][c];
                                board[0][r][c] = board[0][r + halfR][c];
                                board[0][r + halfR][c] = board[0][r + halfR][c + halfC];
                                board[0][r + halfR][c + halfC] = board[0][r][c + halfC];
                                board[0][r][c + halfC] = temp;
                            }
                        }
                    }

                    op[3] = 0;
                }
            }

            void Output()
            {

                sw = new(new BufferedStream(Console.OpenStandardOutput()));

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        sw.Write($"{board[0][r][c]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
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
	static int[][] arr, simple, ans;
	static int[] state = new int[3];
	static int N, M, R;
	
	public static void main(String[] args) throws Exception {
		StringBuilder sb = new StringBuilder();
		
		N = in.nextInt();
		M = in.nextInt();
		R = in.nextInt();
		
		int[][] input = new int[N][M];
		simple = new int[][] {{0,1},{3,2}};
		
		for(int i = 0; i < N; i++) {
			for(int j = 0; j < M; j++) {
				input[i][j] = in.nextInt();
			}
		}
		
		int hn = N / 2;
		int hm = M / 2;
		int[][][] part = new int[4][hn][hm];
		
		for(int i = 0; i < hn; i++) {
			for(int j = 0; j < hm; j++) {
				part[0][i][j] = input[i][j];
				part[1][i][j] = input[i][j+hm];
				part[2][i][j] = input[i+hn][j+hm];
				part[3][i][j] = input[i+hn][j];
				
			}
		}
		
		for(int i = 0; i < R; i++) {
			int x = in.nextInt();
			
			switch(x) {
			case 1: simple1(); break;
			case 2: simple2(); break;
			case 3: simple3(); break;
			case 4: simple4(); break;
			case 5: simple5(); break;
			case 6: simple6(); break;
			}
		}
		
		state[0] %= 2;
		state[1] %= 2;
		state[2] %= 4;
		
		if(state[2] % 2 == 1) {
			ans = new int[M][N];
		}
		else {
			ans = new int[N][M];
		}
		
		int R = ans.length;
		int C = ans[0].length;
		int hr = R / 2;
		int hc = C / 2;
		
		for(int p = 0; p < 2; p++) {
			for(int q = 0; q < 2; q++) {
				arr = part[simple[p][q]];
				
				for(int i = 0; i < state[2]; i++) {
					arr = rotateCW();
				}
				
				for(int i = 0; i < state[0]; i++) {
					upDown();
				}
				
				for(int i = 0; i < state[1]; i++) {
					leftRight();
				}
						
				for(int r = 0; r < hr; r++) {
					for(int c = 0; c < hc; c++) {
						ans[r+hr*p][c+hc*q] = arr[r][c];
					}
				}
			}
		}
		
		for(int i = 0; i < R; i++) {
			for(int j = 0; j < C; j++) {
				sb.append(ans[i][j]).append(" ");
			}
			sb.append("\n");
		}
		
		System.out.println(sb);
	}
	
	static void simple1() {
		int temp = simple[0][0];
		simple[0][0] = simple[1][0];
		simple[1][0] = temp;
		
		temp = simple[0][1];
		simple[0][1] = simple[1][1];
		simple[1][1] = temp;
		
		state[0]++;
	}
	
	static void simple2() {
		int temp = simple[0][0];
		simple[0][0] = simple[0][1];
		simple[0][1] = temp;
		
		temp = simple[1][0];
		simple[1][0] = simple[1][1];
		simple[1][1] = temp;
		
		state[1]++;
	}
	
	static void simple3() {
		simple5();
		
		if((state[0] % 2 == 1 && state[1] % 2 == 1) || (state[0] % 2 == 0 && state[1] % 2 == 0)) {
			state[2]++;
		}
		else {
			state[2] += 3;
		}
	}
	
	static void simple4() {
		simple6();
		
		if((state[0] % 2 == 1 && state[1] % 2 == 1) || (state[0] % 2 == 0 && state[1] % 2 == 0)) {
			state[2] += 3;
		}
		else {
			state[2]++;
		}
	}
	
	static void simple5() {
		int temp = simple[0][0];
		simple[0][0] = simple[1][0];
		simple[1][0] = simple[1][1];
		simple[1][1] = simple[0][1];
		simple[0][1] = temp;
	}
	
	static void simple6() {
		int temp = simple[0][0];
		simple[0][0] = simple[0][1];
		simple[0][1] = simple[1][1];
		simple[1][1] = simple[1][0];
		simple[1][0] = temp;
	}
	
	static void upDown() {
		int R = arr.length;
		
		for(int i = 0; i < R/2; i++) {
			int[] temp = arr[i];
			arr[i] = arr[R-1-i];
			arr[R-1-i] = temp;
		}
	}
	
	static void leftRight() {
		int R = arr.length;
		int C = arr[0].length;
		
		for(int c = 0; c < C/2; c++) {
			for(int r = 0; r < R; r++) {
				int temp = arr[r][c];
				arr[r][c] = arr[r][C-1-c];
				arr[r][C-1-c] = temp;
			}
		}
	}
	
	static int[][] rotateCW() {
		int C = arr.length;
		int R = arr[0].length;
		int[][] temp = new int[R][C];
		
		for(int i = 0; i < R; i++) {
			for(int j = 0; j < C; j++) {
				temp[i][j] = arr[C-1-j][i];
			}
		}
		
		return temp;
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

		long nextLong() throws Exception {
			long n = 0;
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
#elif other2
// #include <bits/stdc++.h>

using namespace std;
using vInt = vector<int>;
using matInt = vector<vInt>;
using pii = pair<int, int>;
using vPii = vector<pii>;
using LL = long long;
using vLL = vector<LL>;
using matLL = vector<vLL>;
using pLL = pair<LL, LL>;
using vPLL = vector<pLL>;
using vBool = vector<bool>;
using matBool = vector<vBool>;

// #define all(x) (x).begin(), (x).end()

matInt flip(matInt& input){
    matInt ans(input.size());
    for(int i=0; i<input.size(); i++){
        ans[i] = input[input.size()-1-i];
    }
    return ans;
}

matInt rotR(matInt& input){
    int N = input.size();
    int M = input[0].size();
    matInt ans(M, vInt(N));
    for(int i=0; i<N; i++){
        for(int j=0; j<M; j++){
            ans[j][N-1-i] = input[i][j];
        }
    }
    return ans;
}

matInt rotSR(matInt& input){
    int N = input.size();
    int M = input[0].size();
    matInt ans(M, vInt(N));
    for(int i=0; i<N/2; i++){
        for(int j=0; j<M/2; j++){
            ans[j][N/2-1-i] = input[i][j];
            ans[j][N-1-i] = input[i][M/2+j];
            ans[M/2+j][N/2-1-i] = input[N/2+i][j];
            ans[M/2+j][N-1-i] = input[N/2+i][M/2+j];
        }
    }
    return ans;
}

// fastio (ref: https://panty.run/fastio/)
const int bufSize = 1<<17;
char inbuf[bufSize];

char read() {
    static int idx = bufSize;
    static int nidx = bufSize;
    if(idx == nidx){
        nidx = fread(inbuf, 1, bufSize, stdin);
        if(!nidx) return 0; // already read all data (0 byte)
        idx = 0;
    }
    return inbuf[idx++];
}

int readInt() {
    int t, r = read() & 15;
    while ((t = read()) & 16) r = r * 10 + (t & 15);
    return r;
}

void solve(){
    int N = readInt();
    int M = readInt();
    int R = readInt();
    matInt map(N, vInt(M));
    for(int i=0; i<N; i++)
        for(int j=0; j<M; j++)
            map[i][j] = readInt();

    matInt transf(32, vInt(6));
    vInt method = {1, 5, 2, 6, 26, 14};
    for(int i=0; i<32; i++){
        for(int j=0; j<6; j++){
            transf[i][j] = i;
            if(method[j]&1){
                transf[i][j] ^= 1;
                transf[i][j] ^= ((transf[i][j]<<1)&20);
            }
            if(method[j]&2){
                transf[i][j] ^= ((transf[i][j]<<1)&4);
                transf[i][j] ^= 2;
            }
            if(method[j]&4){
                transf[i][j] ^= 4;
            }
            if(method[j]&8){
                transf[i][j] ^= ((transf[i][j]<<1)&16);
                transf[i][j] ^= 8;
            }
            if(method[j]&16)
                transf[i][j] ^= 16;
        }
    }

    int state = 0;
    for(int i=0; i<R; i++){
        int op = readInt();
        state = transf[state][--op];
    }

    if(state&1){
        map = flip(map);
    }
    for(int i=0; i<(state>>1)%4; i++)
        map = rotR(map);

    for(int i=0; i<(state>>3)%4; i++)
        map = rotSR(map);

    for(int i=0; i<map.size(); i++){
        for(int j=0; j<map[0].size(); j++)
            cout << map[i][j] << ' ';
        cout << '\n';
    }

}

int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr); cout.tie(nullptr);

    solve();
    
    return 0;
}
#endif
}
