using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 25
이름 : 배성훈
내용 : 서로 다른 부분 문자열의 개수 2
    문제번호 : 11479번

    문자열, 접미사 배열과 lcp 배열 문제다
    서로 다른 부분 문자열을 찾는 것인데
    lcp 를 이용하면 접두사와 겹치는 문자열 최대 길이들을 확인할 수 있다
    lcp에 해당하는 문자열은 중복을 의미한다

    즉, lcp의 값이 중복되는 문자열의 개수와 일치하게 된다
    전체 부분 문자열에서 lcp의 값을 빼주면 중복문자를 빼는 것과 같으므로
    이를 제출하니 이상없이 통과했다

    문자열의 길이가 최대 100만이므로 10^12승까지 가질 수 있어 long 자료형으로 했다
*/

namespace BaekJoon._56
{
    internal class _56_07
    {

        static void Main7(string[] args)
        {

            int len, d;
            int[] lcp, sfx, ord, calc;
            string str;

            Solve();

            void Solve()
            {

                Input();

                SetSFX();
                SetLCP();

                GetRet();
            }

            void GetRet()
            {

                long ret = (1L * len * (len + 1)) / 2;

                for (int i = 0; i < len; i++)
                {

                    ret -= lcp[i];
                }

                Console.Write(ret);
            }

            void SetSFX()
            {

                for (int i = 0; i < len; i++)
                {

                    sfx[i] = i;
                    ord[i] = str[i] - 'a';
                }

                for (d = 1; ; d <<= 1)
                {

                    Array.Sort(sfx, (x, y) => MyComp(x, y));

                    calc[sfx[0]] = 0;

                    for (int i = 1; i < len; i++)
                    {

                        calc[sfx[i]] = calc[sfx[i - 1]] + (MyComp(sfx[i], sfx[i - 1]) > 0 ? 1 : 0);
                    }

                    int[] temp = ord;
                    ord = calc;
                    calc = temp;

                    if (ord[sfx[len - 1]] == len - 1) break;
                }
            }

            void SetLCP()
            {

                for (int i = 0; i < len; i++)
                {

                    calc[sfx[i]] = i;
                }

                for (int k = 0, i = 0; i < len; i++)
                {

                    if (calc[i] == 0) continue;
                    for (int j = sfx[calc[i] - 1]; Math.Max(i, j) + k < len && str[i + k] == str[j + k]; k++) { }
                    lcp[calc[i]] = k > 0 ? k-- : 0;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                str = sr.ReadLine();

                len = str.Length;
                lcp = new int[len];
                sfx = new int[len];
                ord = new int[len];
                calc = new int[len];

                sr.Close();
            }

            int MyComp(int _idx1, int _idx2)
            {

                if (ord[_idx1] != ord[_idx2]) return ord[_idx1].CompareTo(ord[_idx2]);

                _idx1 += d;
                _idx2 += d;

                return (_idx1 < len && _idx2 < len) ? ord[_idx1].CompareTo(ord[_idx2]) : _idx2.CompareTo(_idx1);
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Main {

    static final int MAX_N = 1000010;

    static int[] rank, tempRank;
    static int[] suffix, tempSuffix;
    static int[] LCP;

    static void countSort(int K, int N) {
        int max = Math.max(256, N);
        int[] count = new int[MAX_N];

        for(int i = 0; i < N; ++i)
            count[(i + K < N) ? rank[i + K] : 0]++;

        int sum;
        for(int i = sum = 0; i < max; ++i) {
            int tmp = count[i];
            count[i] = sum;
            sum += tmp;
        }

        for(int i = 0; i < N; ++i) {
            int idx = (suffix[i] + K < N) ? rank[suffix[i] + K] : 0;
            tempSuffix[count[idx]++] = suffix[i];
        }

        for(int i = 0; i < N; ++i)
            suffix[i] = tempSuffix[i];
    }

    static void calcSuffixArray(String str) {
        int N = str.length();
        for(int i = 0; i < N; ++i) suffix[i] = i;
        for(int i = 0; i < N; ++i) rank[i] = str.charAt(i);
        for(int k = 1; k < N; k <<= 1) {
            countSort(k, N);
            countSort(0, N);

            int R = tempRank[suffix[0]] = 0;
            for(int i = 1; i < N; ++i) {
                if(rank[suffix[i]] == rank[suffix[i-1]]
                        && rank[suffix[i]+k] == rank[suffix[i-1]+k]) {
                    tempRank[suffix[i]] = R;
                } else {
                    tempRank[suffix[i]] = ++R;
                }
            }

            for(int i = 0; i < N; ++i) rank[i] = tempRank[i];
            if(rank[suffix[N-1]] == N-1) break;
        }
    }

    static void calcLCPArray(String str) {
        int N = str.length();
        for(int i = 0; i < N; ++i) {
            rank[suffix[i]] = i;
        }

        int L = 0;
        for(int i = 0; i < N; ++i) {
            int r = rank[i];
            if(r == 0) continue;
            int j = suffix[r-1];
            while(str.charAt(i + L) == str.charAt(j + L))
                ++L;
            LCP[r] = L;
            if(L > 0) --L;
        }
    }

    static void init(int N) {
        rank = new int[MAX_N];
        tempRank = new int[MAX_N];
        suffix = new int[MAX_N];
        tempSuffix = new int[MAX_N];
        LCP = new int[MAX_N];
    }

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        String input = br.readLine() + '$';
        int N = input.length();
        init(N);
        calcSuffixArray(input);
        calcLCPArray(input);
        
        long result = N - suffix[1] - 1;
        for(int i = 2; i < N; ++i) {
            result += (N - suffix[i] - LCP[i] - 1);
        }
        System.out.println(result);
        br.close();
    }
}

#elif other2
import java.io.*;
import java.util.*;
public class Main {
    static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    static BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
    static int N;
    static int[] r, nr, cnt, sa, idx, lcp, isa;
    static char[] line;
    static long ans = 0;
    public static void main(String[] args) throws Exception {
        line = br.readLine().toCharArray();
        N = line.length;
        getSA();
        getlcp();
        for(int i:sa) ans+=i+1;
        for(int i:lcp) ans-=i;
        bw.write(Long.toString(ans));
        bw.flush();
    }
    static void getSA(){
        int M = Math.max(257,N+1);
        sa = new int[N];
        r = new int[2*N];
        nr = new int[2*N];
        cnt = new int[M];
        idx = new int[N];
        for(int i=0;i<N;++i){
            sa[i] = i;
            r[i] = line[i];
        }
        for(int d=1;d<N;d<<=1){
            Arrays.fill(cnt,0);
            for(int i=0;i<N;++i) cnt[r[i+d]]++;
            for(int i=1;i<M;++i) cnt[i] += cnt[i-1];
            for(int i=N-1;i>=0;--i) idx[--cnt[r[i+d]]] = i;
            Arrays.fill(cnt,0);
            for(int i=0;i<N;++i) cnt[r[i]]++;
            for(int i=1;i<M;++i) cnt[i]+=cnt[i-1];
            for(int i=N-1;i>=0;--i) sa[--cnt[r[idx[i]]]] = idx[i];
            nr[sa[0]] = 1;
            for(int i=1;i<N;++i) nr[sa[i]] = nr[sa[i-1]] + ((r[sa[i-1]]<r[sa[i]]||(r[sa[i-1]]==r[sa[i]]&&r[sa[i-1]+d]<r[sa[i]+d]))?1:0);
            for(int i=0;i<N;++i) r[i] = nr[i];
            if(r[sa[N-1]]==N) break;
        }
    }
    static void getlcp(){
        lcp = new int[N];
        isa = new int[N];
        for(int i=0;i<N;++i) isa[sa[i]] = i;
        for(int k=0, i=0;i<N;++i){
            if(isa[i]>0){
                for(int j=sa[isa[i]-1];Math.max(i+k,j+k)<N&&line[i+k]==line[j+k];++k);
                lcp[isa[i]] = (k!=0 ? k-- : 0);
            }
        }
    }
}
#elif other3
def counting_sort(suffix_array,rank,d):
    
    m = max(n,256)

    counting = [0]*(m+1)

    counting[0] = d

    for i in range(d,n):
        
        counting[rank[i]] += 1
    
    for i in range(m+1):
        
        counting[i] += counting[i-1]
    
    second = [0]*n

    for i in range(n-1,-1,-1):
        
        if i+d >= n:
            
            ind = n
        
        else:
            
            ind = i+d

        counting[rank[ind]] -= 1
        second[counting[rank[ind]]] = i
    
    counting = [0]*(m+1)

    for i in range(n):
        
        counting[rank[i]] += 1
    
    for i in range(m+1):
        
        counting[i] += counting[i-1]
    
    for i in range(n-1,-1,-1):
        
        counting[rank[second[i]]] -= 1
        suffix_array[counting[rank[second[i]]]] = second[i]
    
    return suffix_array

def compare(rank,a,b,d):
    
    if rank[a] == rank[b] and rank[a+d] == rank[b+d]:
        
        return 0
    
    else:
        
        return 1

def get_suffix_array(suffix_array,rank):
    
    d = 1

    while d < 2*n:
        
        suffix_array = counting_sort(suffix_array,rank,d)

        new_rank = [0]*(n+1)

        new_rank[suffix_array[0]] = 1

        for i in range(1,n):
            
            new_rank[suffix_array[i]] = new_rank[suffix_array[i-1]] + compare(rank,suffix_array[i-1],suffix_array[i],d)
        
        if new_rank[suffix_array[n-1]] == n:
            
            break
        
        rank = new_rank
        d *= 2
    
    return suffix_array

def kasai(suffix_array,s):
    
    lcp = [0]*n
    reverse = [0]*n

    for i in range(n):
        
        reverse[suffix_array[i]] = i
    
    k = 0

    for i in range(n):
        
        if reverse[i] == 0:
            
            continue
        
        adj = suffix_array[reverse[i]-1]

        while i + k < n and adj + k < n and s[i+k] == s[adj+k]:
            
            k += 1
        
        lcp[reverse[i]] = k

        if k > 0:
            
            k -= 1
    
    return lcp

s = input()

n = len(s)

suffix_array = [i for i in range(n)]
rank = [ord(s[i]) - ord('a') + 1 for i in range(n)]
rank.append(0)

suffix_array = get_suffix_array(suffix_array,rank)
lcp = kasai(suffix_array,s)

print(n*(n+1)//2 - sum(lcp))
#endif
}
