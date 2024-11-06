using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 거짓말
    문제번호 : 1043번

    분리 집합 문제다

    파티와 인원의 경우의 수가 적기에 인원에 초점을 맞춰 풀었다
    벨만 포드 알고리즘 과정에 쓰이는 로직을 썼다

    노드들의 경로를 조사할 때, 노드들의 개수 횟수만큼 반복해서 연결지어가면
    모든 연결된 노드들을 확인할 수 있다는 사실이다

    그래서 블랙리스트 그룹을 간선으로해서
    노드들을 조사해가면서 파티들을 m - 1 번 모든 간선들을 조사하면서 하나씩 이어갔다
    그리고 이어진 그룹들을 확인하여 제출하니 이상없이 통과했다

    다 풀고 나서 힌트를 보니, 분리집합이 있었고
    파티를 기준으로 삼아 풀어가면 유니온 파인드로 풀 수 있어 보인다
    다른 사람 풀이를 보니, 실제로 유니온 파인드로 풀었다
    
    그래서 유니온 파인드로 풀어봤다
*/

namespace BaekJoon.etc
{
    internal class etc_0290
    {

        static void Main290(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);
#if first
            bool[] blackList = new bool[n + 1];

            int blackmem = ReadInt(sr);
            for (int i = 0; i < blackmem; i++)
            {

                int chk = ReadInt(sr);
                blackList[chk] = true;
            }

            int[][] party = new int[m][];
            bool[] allBlackParty = new bool[m];
            for (int i = 0; i < m; i++)
            {

                int pmem = ReadInt(sr);

                party[i] = new int[pmem];
                bool isBlack = false;
                for (int j = 0; j < pmem; j++)
                {

                    int chk = ReadInt(sr);
                    party[i][j] = chk;

                    if (blackList[chk]) isBlack = true;
                }

                if (!isBlack) continue;
                for (int j = 0; j < pmem; j++)
                {

                    blackList[party[i][j]] = true;
                }
                allBlackParty[i] = true;
            }

            sr.Close();

            // 벨만 - 포드 알고리즘의 아이디어를 사용
            // 파티 이어가기
            for (int i = 0; i < m - 1; i++)
            {

                for (int j = 0; j < m; j++)
                {

                    // 블랙리스트를 포함한 파티의 경우 제외
                    if (allBlackParty[j]) continue;
                    int len = party[j].Length;
                    bool isBlack = false;

                    for (int k = 0; k < len; k++)
                    {

                        // 블랙리스트 포함여부 확인
                        if (blackList[party[j][k]]) isBlack = true;
                    }

                    if (!isBlack) continue;

                    for (int k = 0; k < len; k++)
                    {
                        
                        // 블랙리스트를 포함하면 블랙리스트 잇기
                        blackList[party[j][k]] = true;
                    }

                    // 블랙리스트 그룹으로 변경
                    allBlackParty[j] = true;
                }
            }

            int ret = m;
            for (int i = 0; i < m; i++)
            {

                if (allBlackParty[i]) ret--;
            }

            Console.WriteLine(ret);
#else

            // 유니온 파인드를 이용
            int[] man = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                man[i] = i;
            }

            int blackmem = ReadInt(sr);
            for (int i = 0; i < blackmem; i++)
            {

                int id = ReadInt(sr);
                // 진실을 아는 사람은 0번으로 했다
                man[id] = 0;
            }

            int[] party = new int[m];
            int[] chk = new int[n + 1];
            Stack<int> s = new();
            for (int i = 0; i < m; i++)
            {

                int len = ReadInt(sr);

                // 파티 참여 최소 인원 1명이 보장
                chk[0] = ReadInt(sr);

                for (int j = 1; j < len; j++)
                {

                    // 사람들간 그룹 잇기
                    chk[j] = ReadInt(sr);
                    Union(man, chk[j - 1], chk[j], s);
                }

                // 파티의 그룹 번호 저장
                party[i] = man[chk[0]];
            }
            sr.Close();

            int ret = m;

            for (int i = 0; i < m; i++)
            {

                // 그룹 번호 최신화!
                party[i] = Find(man, party[i], s);
                // 진실을 아는 사람이 있으면 해당 파티를 뺀다
                if (party[i] == 0) ret--;
            }

            Console.WriteLine(ret);
#endif


        }

        static void Union(int[] _group, int _user1, int _user2, Stack<int> _s)
        {

            int user1 = Find(_group, _user1, _s);
            int user2 = Find(_group, _user2, _s);

            if (user2 < user1)
            {

                int temp = user1;
                user1 = user2;
                user2 = temp;
            }

            _group[user2] = user1;
        }

        static int Find(int[] _group, int _chk, Stack<int> _s)
        {

            while (_group[_chk] != _chk)
            {

                _s.Push(_chk);
                _chk = _group[_chk];
            }

            while(_s.Count > 0)
            {

                _group[_s.Pop()] = _chk;
            }

            return _chk;
        }
        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static int[] parent;
    static bool[] knows;
    static int Find(int node)
    {
        if (parent[node] == node) return node;
        return Find(parent[node]);
    }
    static void Union(int a, int b)
    {
        a = Find(a);
        b = Find(b);
        if (knows[a])
        {
            parent[b] = a;
        }
        else
        {
            parent[a] = b;
        }
    }

    public static void Main(string[] args)
    {
        int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int N = input[0], M = input[1];
        parent = new int[N + 1];
        knows = new bool[N + 1];
        int[] parties = new int[M];
        for (int i = 1; i <= N; ++i) parent[i] = i;
        List<int>[] guests = new List<int>[M];
        input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        for (int i = 1; i <= input[0]; ++i) knows[input[i]] = true;
        for(int i = 0; i < M; ++i)
        {
            input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            for(int j = 2; j <= input[0]; ++j)
            {
                Union(input[j], input[1]);
            }
            parties[i] = input[1];
        }
        int cnt = 0;
        for(int i = 0; i < M; ++i)
        {
            if (!knows[Find(parties[i])]) ++cnt;
        }
        Console.WriteLine(cnt);
    }
}
#elif other2
int[] NM = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    int N = NM[0]; int M = NM[1];
    int[] Ks = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    int[] parent = new int[N + 1];
    //int[] rank = new int[N + 1];
    int[][] parties = new int[M][];
    for (int i = 1; i <= N; i++)
    {
        parent[i] = i;
    }
    for (int i = 1; i < Ks.Length; i++)
    {
        //진실을 아는 사람들은 0번 집합에 묶는다.
        parent[Ks[i]] = 0;
    }

    for (int i = 0; i < M; i++)
    {
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        parties[i] = new int[arr.Length - 1];
        parties[i][0] = arr[1];
        for (int j = 1; j < arr.Length - 1; j++)
        {
            parties[i][j] = arr[j + 1];
            Union(arr[j], arr[j + 1]);
        }
    }
    int cnt = M;
    for (int i = 0; i < M; i++)
    {
        for (int j = 0; j < parties[i].Length; j++)
        {
            if (Find(parties[i][j]) == 0)
            {
                cnt--;
                break;
            }
        }
    }
    Console.WriteLine(cnt);
    int Find(int x)
    {
        if (x == parent[x])
            return x;
        else
        {
            int p = Find(parent[x]);
            parent[x] = p;
            return p;
        }
    }
    void Union(int x, int y)
    {
        x = Find(x);
        y = Find(y);

        if (x == y)
            return;

        if (x < y)
        {
            parent[y] = x;
        }
        else
        {
            parent[x] = y;
        }
    }
#endif
}
