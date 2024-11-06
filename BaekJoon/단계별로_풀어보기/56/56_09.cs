using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 27
이름 : 배성훈
내용 : 문자열 집합 판별
    문제번호 : 9250번

    문자열, 트라이, 아호 코라식 알고리즘 문제다
    트라이 자료구조를 이용해서 해당 문자열에 원하는 문자열들이 들어가 있는지를 판별한다

    해당 사이트를 참고했다
    https://pangtrue.tistory.com/305
*/

namespace BaekJoon._56
{
    internal class _56_09
    {

        static void Main9(string[] args)
        {

            string YES = "YES\n";
            string NO = "NO\n";

            StreamReader sr;
            StreamWriter sw;

            Trie root;
            Solve();
            void Solve()
            {

                Init();

                SetTrie();

				GetRet();
            }

            bool Find(string _str)
            {

				// 문자열 찾기
                Trie current = root;
                bool ret = false;

                for (int j = 0; j < _str.Length; j++)
                {

                    int next = _str[j] - 'a';
                    while(current != root && current[next] == null)
                    {

                        current = current.fail;
                    }

                    if (current[next] != null) current = current[next];

					if (current.isEnd) return true;
                }

				return false;
            }

			void GetRet()
			{

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int len = int.Parse(sr.ReadLine());

                for (int i = 0; i < len; i++)
				{

					string chk = sr.ReadLine();

					if (Find(chk)) sw.Write(YES);
					else sw.Write(NO);
				}

				sr.Close();
				sw.Close();
            }

            void SetTrie()
            {

                Queue<Trie> q = new();
                root.fail = root;

                q.Enqueue(root);

                while (q.Count > 0)
                {

                    Trie node = q.Dequeue();

                    for (int i = 0; i < 26; i++)
                    {

                        Trie next = node[i];
                        if (next == null) continue;

                        if (node == root) next.fail = root;
                        else
                        {

                            Trie fail = node.fail;

                            while (fail != root && fail[i] == null)
                            {

                                fail = fail.fail;
                            }

                            if (fail[i] != null) fail = fail[i];
                            next.fail = fail;
                        }

                        if (next.fail.isEnd) next.isEnd = true;

                        q.Enqueue(next);
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int n = int.Parse(sr.ReadLine());
                root = new();
                for (int i = 0; i < n; i++)
                {

                    root.Insert(sr.ReadLine());
                }
            }
        }

        class Trie
        {

            private Trie[] go;
            public Trie fail;
            public bool isEnd;


            public Trie()
            {

                go = new Trie[26];
                isEnd = false;
                fail = null;
            }

            public Trie this[int _idx]
            {

                set { go[_idx] = value; }
                get { return go[_idx]; }
            }

            public void Insert(string _str)
            {

                Trie next = this;

                for (int i = 0; i < _str.Length; i++)
                {

                    int cur = _str[i] - 'a';
                    if (next[cur] == null) next[cur] = new();
                    next = next[cur];
                }

                next.isEnd = true;
            }
        }
    }

#if other
import java.io.*;
import java.util.*;
public class Main {
	private static class Trie {
		Trie[] child;
		Trie fail;
		boolean isEnd;

		Trie() {
			child = new Trie[26];
			isEnd = false;
		}

		void insert(String s, int index) {
			if (s.length() == index) {
				isEnd = true;
				return;
			}

			if (child[s.charAt(index) - 'a'] == null) {
				child[s.charAt(index) - 'a'] = new Trie();
			}

			child[s.charAt(index) - 'a'].insert(s, ++index);
		}
	}

	public static void main(String[] args) {
		FastIO io = new FastIO();

		Trie root = new Trie();
		root.fail = root;

		int N = io.nextInt();
		for (int i = 0; i < N; i++) {
			String s = io.nextString();
			root.insert(s, 0);
		}

		Queue<Trie> queue = new LinkedList<>();
		queue.add(root);

		Trie current, next, dest;
		while (!queue.isEmpty()) {
			current = queue.poll();

			for (int i = 0; i < 26; i++) {
				next = current.child[i];

				if (next == null) continue;

				if (current == root) {
					next.fail = root;
				} else {
					dest = current.fail;
					while (dest != root && dest.child[i] == null) {
						dest = dest.fail;
					}

					if (dest.child[i] != null) {
						next.fail = dest.child[i];
						next.isEnd |= next.fail.isEnd;
					} else {
						next.fail = root;
					}
				}

				queue.add(next);
			}
		}

		int Q = io.nextInt();
		boolean isYes;
		for (int i = 0; i < Q; i++) {
			String q = io.nextString();
			current = root;

			isYes = false;
			int length = q.length();
			for (int j = 0; j < length; j++) {
				while (current != root && current.child[q.charAt(j) - 'a'] == null) {
					current = current.fail;
				}

				if (current.child[q.charAt(j) - 'a'] != null) {
					current = current.child[q.charAt(j) - 'a'];
					if (current.isEnd) {
						isYes = true;
						break;
					}
				}
			}

			if (isYes) {
				io.println("YES");
			} else {
				io.println("NO");
			}
		}

		io.flushBuffer();
	}

	private static class FastIO {
		private static final int EOF = -1;

		private static final byte ASCII_minus = 45;
		private static final byte ASCII_space = 32;
		private static final byte ASCII_n = 10;
		private static final byte ASCII_0 = 48;
		private static final byte ASCII_9 = 57;

		private static final byte ASCII_A = 65;
		private static final byte ASCII_Z = 90;
		private static final byte ASCII_a = 97;
		private static final byte ASCII_z = 122;

		private final DataInputStream din;
		private final DataOutputStream dout;

		private byte[] inbuffer;
		private int inbufferpointer, bytesread;
		private byte[] outbuffer;
		private int outbufferpointer;

		private byte[] bytebuffer;

		private FastIO() {
			this.din = new DataInputStream(System.in);
			this.dout = new DataOutputStream(System.out);

			this.inbuffer = new byte[65536];
			this.inbufferpointer = 0;
			this.bytesread = 0;
			this.outbuffer = new byte[65536];
			this.outbufferpointer = 0;

			this.bytebuffer = new byte[10001];
		}

		private byte read() {
			if (inbufferpointer == bytesread)
				fillBuffer();
			return bytesread == EOF ? EOF : inbuffer[inbufferpointer++];
		}

		private void fillBuffer() {
			try {
				bytesread = din.read(inbuffer, inbufferpointer = 0, inbuffer.length);
			} catch (Exception e) {
				throw new RuntimeException(e);
			}
		}

		private void write(byte b) {
			if (outbufferpointer == outbuffer.length)
				flushBuffer();
			outbuffer[outbufferpointer++] = b;
		}

		private void flushBuffer() {
			if (outbufferpointer != 0) {
				try {
					dout.write(outbuffer, 0, outbufferpointer);
				} catch (Exception e) {
					throw new RuntimeException(e);
				}
				outbufferpointer = 0;
			}
		}

		private int nextInt() {
			byte b;
			while(isSpace(b = read()))
				;
			int result = b - '0';
			while (isDigit(b = read()))
				result = result * 10 + b - '0';
			return result;
		}

		private String nextString() {
			byte b;
			int index = 0;

			while (isChar(b = read())) {
				bytebuffer[index++] = b;
			}

			return new String(bytebuffer, 0, index);
		}

		private void println(long i) {
			if (i == 0) {
				write(ASCII_0);
			} else {
				if (i < 0) {
					write(ASCII_minus);
					i = -i;
				}
				int index = 0;
				while (i > 0) {
					bytebuffer[index++] = (byte) ((i % 10) + ASCII_0);
					i /= 10;
				}
				while (index-- > 0) {
					write(bytebuffer[index]);
				}
			}
			write(ASCII_n);
		}

		private void println(String s) {
			for (int i = 0; i < s.length(); i++) {
				write((byte) s.charAt(i));
			}
			write(ASCII_n);
		}

		private boolean isSpace(byte b) {
			return b <= ASCII_space && b >= 0;
		}

		private boolean isDigit(byte b) {
			return b >= ASCII_0 && b <= ASCII_9;
		}

		private boolean isChar(byte b) {
			return (ASCII_A <= b && b <= ASCII_Z) || (ASCII_a <= b && b <= ASCII_z);
		}
	}
}

#elif other2
import io, os, sys
input=io.BytesIO(os.read(0,os.fstat(0).st_size)).readline

C_SIZE = 26
C_TO_I = lambda x: x - 97
class Trie:
  def __init__(self) :
    self.go = [None] * C_SIZE
    self.fail = None # 실패시 돌아갈 노드
    self.output = False

  def add(self, k) :
    if not k :
      self.output = True
      return

    v = C_TO_I(k[0])
    if self.go[v] is None :
      self.go[v] = Trie()
    self.go[v].add(k[1:])

from collections import deque 
class AhoCorasick:
  def __init__(self, trie: Trie): # 전처리 O( sum(len(p) for p in P ) )
    self.trie = trie
    self.trie.fail = self.trie
    
    Q = deque([self.trie]) 
    while Q :
      u = Q.popleft()
      for i in range(C_SIZE) :
        v = u.go[i]
        if not v: continue
        
        if u == self.trie :
          v.fail = self.trie
        else :
          w = u.fail
          while w != self.trie and not w.go[i] :
            w = w.fail
          if w.go[i] :
            w = w.go[i]
          v.fail = w
        v.output |= v.fail.output
        Q.append(v)
      
  def __contains__(self, s: str): # O( len(s) )
    u = self.trie
    for c in s :
      i = C_TO_I(c)
      while u != self.trie and not u.go[i] :
        u = u.fail
      if u.go[i] :
        u = u.go[i]
      if u.output :
        return True
    return False

def sol() :
  N = int(input())
  trie = Trie()
  for _ in range(N) :
    trie.add(input().rstrip())

  Q = int(input())
  S = [input().rstrip() for _ in range(Q)]
  ac = AhoCorasick(trie)

  ans = []
  for s in S:
    ans.append("YES" if s in ac else "NO")
  
  sys.stdout.write("\n".join(ans))
  
sol()
#elif other3
//#include<bits/stdc++.h>
// #include<stdio.h>
// #include<algorithm>
// #include<vector>
// #include<queue>
// #include<iostream>
// #include<string>
using namespace std;
// #define ll long long
// #define S 100100
// #define L 26
int n,k,t,m,s,l,ans,T,x,y,q;
string str;
int fnd[S],to[S][L],fail[S];
int trie_size;
int add_str(string str,int num=1){
  int cur=0;
  for(char c:str){
    if(!to[cur][c-'a']){
      to[cur][c-'a']=++trie_size;
    }
    cur=to[cur][c-'a'];
  }
  fnd[cur]=num;
  return cur;
}
void make_aho(){
  queue<int> Q;
  for(int i=0;i<L;i++) if(to[0][i]) Q.push(to[0][i]);
  
  while(!Q.empty()){
    int cur=Q.front(); Q.pop();
    int back=fail[cur];
    if(!fnd[cur]) fnd[cur]=fnd[back];
    for(int i=0;i<L;i++){
      if(to[cur][i]){
        fail[to[cur][i]]=to[back][i];
        Q.push(to[cur][i]);
      }
      else to[cur][i]=to[back][i];
    }
  }
}
int find_str(string str){
  int cur=0;
  for(char c:str){
    cur=to[cur][c-'a'];
    if(fnd[cur]) return fnd[cur];
  }
  return 0;
}
int main(){
  ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
//  scanf("%d",&T);
//  while(T--){

  int n,m,t;
  cin>>n;
  for(int i=0;i<n;i++){
    cin>>str;
    add_str(str);
  }
  make_aho();
  cin>>m;
  for(int i=0;i<m;i++){
    cin>>str;
    puts(find_str(str)?"YES":"NO");
  }
  
  
    
//  printf("%d\n",ans);
// puts(ans?"Yes":"No");

//  }
}

#endif
}
