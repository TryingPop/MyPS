using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 빨강~ 빨강~ 파랑! 파랑! 달콤한 솜사탕!
    문제번호 : 28140번

    이분 탐색 문제다
    if문 조건을 잘못 설정해 한 번 틀렸다
    입력이 많아서 그런지 아슬아슬하게 통과한다

    아이디어는 다음과 같다
    가장 l 이상인 idx를 이분탐색을 통해 순차적으로 찾았다
    그리고 r을 초과하거나 없는 경우 -1이 되게 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0667
    {

        static void Main667(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, q;

            int[] blue;
            int[] red;
            int len1, len2;

            Solve();

            void Solve()
            {

                Input();

                for (int i = 0; i < q; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    int ret1 = GetVal(red, len1, f, b);
                    int ret2 = -1, ret3 = -1, ret4 = -1;
                    if (ret1 != -1) ret2 = GetVal(red, len1, ret1 + 1, b);

                    if (ret2 != -1) ret3 = GetVal(blue, len2, ret2 + 1, b);
                    if (ret3 != -1) ret4 = GetVal(blue, len2, ret3 + 1, b);

                    if (ret4 == -1) sw.Write("-1\n");
                    else sw.Write($"{ret1} {ret2} {ret3} {ret4}\n");

                    if (i % 5_000 == 4_999) sw.Flush();
                }

                sw.Close();
                sr.Close();
            }

            int GetVal(int[] _arr, int _len, int _l, int _r)
            {


                int l = 0;
                int r = _len - 1;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (_arr[mid] < _l) l = mid + 1;
                    else r = mid - 1;
                }

                int ret = r + 1;
                if (ret >= _len || _arr[ret] > _r) ret = -1;
                else ret = _arr[r + 1];

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                n = ReadInt();
                q = ReadInt();

                blue = new int[n];
                red = new int[n];

                len1 = 0;
                len2 = 0;

                for (int i = 0; i < n; i++)
                {

                    int c = sr.Read();
                    if (c == 'R') red[len1++] = i;
                    else if (c == 'B') blue[len2++] = i;
                }

                if (sr.Read() == '\r') sr.Read();
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
// #include <iostream>
using namespace std;

// #define fi first
// #define se second

pair<int,int> R[1000000], B[1000000];

int main(){

	ios::sync_with_stdio(0);
	cin.tie(0);

	int n, q;
	string s;
	cin >> n >> q >> s;

	int p, pp;
	p=-1, pp=-1;
	for(int i=0; i<n; i++){
		if(s[i]=='B'){
			pp = p;
			p = i;
		}
		B[i].fi = pp;
		B[i].se = p;
	}
	p=-1, pp=-1;
	for(int i=n-1; i>=0; i--){
		if(s[i]=='R'){
			pp = p;
			p = i;
		}
		R[i].fi = p;
		R[i].se = pp;
	}

	int l, r;
	while(q--){
		cin >> l >> r;
		if(R[l].fi==-1 || R[l].se==-1 || B[r].fi ==-1 || B[r].se==-1 || R[l].se>=B[r].fi){
			cout << "-1\n";
		}
		else{
			cout << R[l].fi << ' ' << R[l].se << ' ' << B[r].fi << ' ' << B[r].se << '\n';
		}
	}


	return 0;
}


#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());
        int n = Integer.parseInt(st.nextToken());
        int q = Integer.parseInt(st.nextToken());
        String s = br.readLine();

        int[] lastRed = new int[n];
        Arrays.fill(lastRed, -1);
        int[] lastBlue = new int[n];
        Arrays.fill(lastBlue, -1);

        for (int i = 1; i < n; i++) {
            lastRed[i] = (s.charAt(i - 1) == 'R' ? i - 1 : lastRed[i - 1]);
            lastBlue[i] = (s.charAt(i - 1) == 'B' ? i - 1 : lastBlue[i - 1]);
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < q; i++) {
            st = new StringTokenizer(br.readLine());
            int l = Integer.parseInt(st.nextToken());
            int r = Integer.parseInt(st.nextToken());

            int a, b, c, d;
            if ((d = (s.charAt(r) == 'B' ? r : lastBlue[r])) != -1 && (c = lastBlue[d]) != -1 &&
                    (b = lastRed[c]) != -1 && (a = lastRed[b]) != -1 && a >= l)
                sb.append(a).append(" ").append(b).append(" ").append(c).append(" ").append(d).append("\n");
            else
                sb.append(-1).append("\n");
        }
        System.out.print(sb);
    }
}
#elif other3
// #!/usr/bin/env python
import os
import sys
from io import BytesIO, IOBase


def main():
    n,q = map(int,input().split())
    s = input()
    nextR = []
    curR = float("inf")
    for i in range(n - 1, -1, -1):
        nextR.append(curR)
        if s[i] == "R":
            curR = i
    nextR.append(curR)
    nextR.reverse()
    leftB = []
    curB = -float("inf")
    for i in range(n):
        leftB.append(curB)
        if s[i] == "B":
            curB = i
    leftB.append(curB)

    for _ in range(q):
        a,b = map(int,input().split())
        if nextR[a - 1 + 1] != float("inf") and leftB[b + 1] != -float("inf") and nextR[nextR[a - 1 + 1] + 1] < leftB[leftB[b + 1]]:
            print(nextR[a - 1 + 1], nextR[nextR[a - 1 + 1] + 1], leftB[leftB[b + 1]], leftB[b + 1])
        else:
            print(-1)


    // #region fastio

BUFSIZE = 8192


class FastIO(IOBase):
    newlines = 0

    def __init__(self, file):
        self._file = file
        self._fd = file.fileno()
        self.buffer = BytesIO()
        self.writable = "x" in file.mode or "r" not in file.mode
        self.write = self.buffer.write if self.writable else None

    def read(self):
        while True:
            b = os.read(self._fd, max(os.fstat(self._fd).st_size, BUFSIZE))
            if not b:
                break
            ptr = self.buffer.tell()
            self.buffer.seek(0, 2), self.buffer.write(b), self.buffer.seek(ptr)
        self.newlines = 0
        return self.buffer.read()

    def readline(self):
        while self.newlines == 0:
            b = os.read(self._fd, max(os.fstat(self._fd).st_size, BUFSIZE))
            self.newlines = b.count(b"\n") + (not b)
            ptr = self.buffer.tell()
            self.buffer.seek(0, 2), self.buffer.write(b), self.buffer.seek(ptr)
        self.newlines -= 1
        return self.buffer.readline()

    def flush(self):
        if self.writable:
            os.write(self._fd, self.buffer.getvalue())
            self.buffer.truncate(0), self.buffer.seek(0)


class IOWrapper(IOBase):
    def __init__(self, file):
        self.buffer = FastIO(file)
        self.flush = self.buffer.flush
        self.writable = self.buffer.writable
        self.write = lambda s: self.buffer.write(s.encode("ascii"))
        self.read = lambda: self.buffer.read().decode("ascii")
        self.readline = lambda: self.buffer.readline().decode("ascii")


sys.stdin, sys.stdout = IOWrapper(sys.stdin), IOWrapper(sys.stdout)
input = lambda: sys.stdin.readline().rstrip("\r\n")

    // #endregion

if __name__ == "__main__":
    main()
#endif
}
