using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 7
이름 : 배성훈
내용 : 전화번호 목록
    문제번호 : 5052번

    트라이, 정렬 문제다.
    트라이를 이용해 해결했다.
    혹은 사전 순 정렬한 뒤 인접한것끼리 접두어가 되는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1752
    {

        static void Main1752(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n";
            int HEAD;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            (int[] next, bool isEnd)[] nodes;
            int newIdx;
            int[] arr;
            int len;
            bool ret;

            Init();

            int q = ReadInt();
            while (q-- > 0)
            {

                newIdx = 0;
                HEAD = GetNewNode();
                ret = true;

                Input();

                sw.Write(ret ? Y : N);
            }

            void Input()
            {

                int n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    FillArr();
                    if (ret) TrieAdd();
                }
            }

            void Init()
            {

                // 전화번호 갯수는 많아야 1만개 자리는 10개씩이므로 많아야 10만개이다.
                nodes = new (int[] next, bool isEnd)[100_001];
                // 전화번호는 최대 10자리
                arr = new int[10];
                len = 0;
            }

            // 트라이에 Arr의 값 넣기
            void TrieAdd()
            {

                int curNode = HEAD;
                bool flag = true;

                // 길이 만큼만 채워넣는다.
                int l = 0;
                while (l < len)
                {

                    int num = arr[l++];
                    if (nodes[curNode].next[num] == 0)
                    {

                        flag = false;
                        int newNode = GetNewNode();
                        nodes[curNode].next[num] = newNode;
                    }

                    curNode = nodes[curNode].next[num];
                    if (nodes[curNode].isEnd)
                    {

                        ret = false;
                        return;
                    }
                }

                if (flag)
                    // 있는 루트만 오면 종료!
                    ret = false;
                else
                    // 없는 루트 개척했으니 끝을 표시
                    nodes[curNode].isEnd = true;
            }

            void FillArr()
            {

                // 읽은 전화번호를 arr에 저장
                int c;
                len = 0;
                while ((c = sr.Read()) != -1 && c != '\n')
                {

                    if (c == '\r') continue;
                    arr[len++] = c - '0';
                }
            }

            int GetNewNode()
            {

                // 새 노드 가져오기
                int ret = newIdx;
                ref var node = ref nodes[newIdx++];
                if (node.next == null) node.next = new int[10];
                else Array.Fill(node.next, 0);

                node.isEnd = false;
                return ret;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include<iostream>
// #include<cstring>
using namespace std;
int trie[100000][10];
bool fin[100000];
int main(){
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);
	char str[12];
	int N, T;
	cin>>T;
	while(T--){
		cin>>N;
		int n=0;
		memset(trie[0], 0, sizeof(trie[0]));
		bool chk=1;
		for(int i=0;i<N;++i){
			cin>>str;
			if(!chk)	continue;
			int t=0;
			for(int j=0;chk && str[j];++j){
				int cur=str[j]-'0';
				if(!trie[t][cur]){
					++n;
					fin[n]=0;
					memset(trie[n], 0, sizeof(trie[n]));
					trie[t][cur]=n;
				}
				t=trie[t][cur];
				if(fin[t])	chk=0;
			}
			if(chk){
				fin[t]=1;
				for(int j=0;j<10;++j){
					if(trie[t][j]>0){
						chk=0;
						break;
					}
				}
			}
		}
		if(chk)	cout<<"YES\n";
		else	cout<<"NO\n";
	}
	return 0;
}
#elif other2

#endif
}
