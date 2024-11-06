using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 21
이름 : 배성훈
내용 : 문자열과 쿼리
    문제번호 : 13713번

    문자열, Z 알고리즘 문제다

    문자열 s의 각각의 접미사 대해 그 접미사와 전체 문자열을 앞에서부터 비교하면 
    몇 글자가 일치하는지 구하고, 일치하는 글자의 수를 z(k)라고 하면 
    z(k)가 Z 알고리즘에서 구하는 값이 된다. 
    각각의 z(k)를 저장하는 배열은 보통 Z 배열(Z Array) 또는 Z 박스(Z Box)라고 한다.
    그러면 Z 배열은 최대 공통 접두사의 길이를 나타내게 된다
    https://00ad-8e71-00ff-055d.tistory.com/93

    그래서 코드를 보면서 접미사를 구하게 할려고 했는데,
    인덱스 실수, 최대 최소를 잘못설정해 2번 틀렸다

    처음에 string을 뒤집어서 하려고 했으나 
    C#에서는 다른 문자열을 생성하기에 해당 방법을 안썼다
*/

namespace BaekJoon._56
{
    internal class _56_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            
            int l, r;
            int[] z;

            Solve();

            void Solve()
            {

                Init();

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int idx = ReadInt();
                    sw.Write($"{z[idx - 1]}\n");
                }

                sw.Close();
                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                string str = sr.ReadLine();

                z = MyZ(str);
            }

            int[] MyZ(string _str)
            {

                int len = _str.Length;
                int[] arr = new int[len];
                arr[len - 1] = len;

                int l = len - 1;
                int r = len - 1;

                for (int i = len - 2; i >= 0; i--)
                {

                    if (i >= l) arr[i] = Math.Min(i - l, arr[len - 1 - r + i]);
                    while (i - arr[i] >= 0 && _str[i - arr[i]] == _str[len - 1 - arr[i]])
                    {

                        arr[i]++;
                    }

                    if (i < l) r = i;
                    l = Math.Min(l, i - arr[i] + 1);
                }

                return arr;
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


            int[] Z(string _str)
            {

                int len = _str.Length;
                int[] arr = new int[len];

                int l = 0;      // 확인한 왼쪽
                int r = 0;      // 확인한 오른쪽

                arr[0] = len;
                for (int i = 1; i < len; i++)
                {

                    // 앞의 정보가 있는 경우
                    // 앞에서 구한 값을 확인한다
                    if (i <= r) arr[i] = Math.Min(r - i, arr[i - l]);

                    // 문자를 하나씩 비교하며 arr[i]를 구한다
                    while (i + arr[i] < len && _str[i + arr[i]] == _str[arr[i]])
                    {

                        arr[i]++;
                    }
                    // 뒤쪽 문자들을 모두 확인한 경우 해당 영역 탈출로 l 갱신
                    if (i > r) l = i;
                    r = Math.Max(r, i + arr[i] - 1);
                }

                return arr;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

using i64 = long long;

vector<int> Z_function(const string& s) {
	int n = s.size(), l = -1, r = -1;
	vector<int> Z(n); Z[0] = n;
	for (int i = 1; i < n; i++) {
		if (i <= r) Z[i] = min(r - i + 1, Z[i - l]);
		while (i + Z[i] < n && s[Z[i]] == s[i + Z[i]]) Z[i]++;
		if (r < i + Z[i] - 1) l = i, r = i + Z[i] - 1;
	}
	return Z;
}

int main() {
	fastio;
	string s; cin >> s; reverse(s.begin(), s.end());
	auto Z = Z_function(s);
	int q; cin >> q;
	for (int t; q-- && cin >> t; cout << Z[s.size() - t] << '\n');
}
#elif other2
import io, os, sys
input=io.BytesIO(os.read(0,os.fstat(0).st_size)).readline

def genZ(S):
  n = len(S)
  Z = [0] * n
  l = r = 0

  for i in range(1, n):
    z = Z[i - l]
    if i + z >= r:
      z = max(r - i, 0)
      while i + z < n and S[z] == S[i + z]:
        z += 1

      l, r = i, i + z

    Z[i] = z

  Z[0] = n
  return Z

def sol() :
  S = input().rstrip()
  Z = genZ(S[::-1])
  ans = [Z[-int(input())] for _ in range(int(input()))]
  sys.stdout.write('\n'.join(map(str, ans)))

sol()
#elif other3
import java.io.*;
import java.util.*;

public class Main {
    static Reader r = new Reader();
    static StringBuilder sb = new StringBuilder();

    static int[] Z(String s){
        // Z algorithm
        // Z[i] = the largest k s.t. s[0:k] = s[i:i+k]
        int n = s.length(), l=0, r=0;
        int[] Z = new int[n];
        Z[0] = n;
        for(int i=1;i<n;i++){
            if(i>r){
                l = r = i;
                while(r<n && s.charAt(r-l) == s.charAt(r)) r++;
                Z[i] = r-l; r--;
                continue;
            }
            if(Z[i-l]<r-i+1){
                Z[i] = Z[i-l]; continue;
            }
            l = i;
            while(r<n && s.charAt(r-l) == s.charAt(r)) r++;
            Z[i] = r-l; r--;
        }
        return Z;
    }

    public static void main(String args[]) throws IOException{
        String s0 = r.readLine();
        int n = s0.length();
        char[] tmp = new char[n];
        for(int i=0;i<n;i++)
            tmp[n-1-i] = s0.charAt(i);
        String s = String.valueOf(tmp);

        int[] z = Z(s);
        int m = r.readInt();
        while(m-->0)
            sb.append(z[n-r.readInt()]).append("\n");
        System.out.println(sb);
    }
}

class Reader {
    final private int BUFFER_SIZE = 1 << 16;
    private DataInputStream din;
    private byte[] buffer;
    private int bufferPointer, bytesRead;

    public Reader() {
        din = new DataInputStream(System.in);
        buffer = new byte[BUFFER_SIZE];
        bufferPointer = bytesRead = 0;
    }

    public String readLine() throws IOException {
        byte[] buf = new byte[1000010]; // line length
        int cnt = 0, c;
        while((c=read())!=-1){
            if(c=='\n'){
                if(cnt!=0) break;
                else continue;
            }
            buf[cnt++] = (byte)c;
        }
        return new String(buf, 0, cnt);
    }

    public int readInt() throws IOException {
        int ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    public long readLong() throws IOException {
        long ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    private void fillBuffer() throws IOException {
        bytesRead = din.read(buffer, bufferPointer = 0, BUFFER_SIZE);
        if(bytesRead == -1) buffer[0] = -1;
    }

    private byte read() throws IOException {
        if(bufferPointer == bytesRead) fillBuffer();
        return buffer[bufferPointer++];
    }

    public void close() throws IOException {
        if(din==null) return;
        din.close();
    }
}
#endif 
}
