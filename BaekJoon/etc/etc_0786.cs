using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 2
이름 : 배성훈
내용 : 두 배
    문제번호 : 31963번

    수학, 그리디 알고리즘 문제다
    아이디어는 다음과 같다
    인접한 항끼리 비교를 한다
    비교하는 방법은 앞이 크다면 뒤 숫자를 2배수를 해서 
    언제 오름차순이 완성되는지 확인한다

    반면 앞이 작다면 앞의 숫자를 2배수하면서 
    언제 오름차순이 되는지 확인한다
    
    배열을 1개만 쓸거기에 앞이 큰 경우의 2배수에 저장되는 값은 양수로
    뒤가 큰 경우는 음수로 저장했다

    그리고 앞에서부터 탐색하는데,
    오름차순이되게 2를 몇번 곱해야 하는지 확인한다
    이는 앞에서 저장했으므로,
    해당 값을 꺼낸다

    그리고 앞에 2배수가 뒤의 탐색에 영향을 끼치기에 바로앞 2배수가 얼마나 되었는지 확인한다
    앞이 큰 경우면 누적하고, 뒤가 큰 경우면 앞에 누적된 2배수를 몇번까지 삭감가능한지 확인한다
    삭감은 0 미만으로는 할 수 없어 0미만이면 0으로 생각한다
    이렇게 찾아가면서 누적한 값이 정답이된다
    다만, 내림차순으로 배열인 경우면 1, 2, 3, 4, ... , 25만이되는데
    누적합으로 구하면 int 범위를 벗어나 long으로 했다
    성능향상?을 바란다면 mul 배열을 만들지 않고 찾는 연산과 함께 진행하고
    2배수 연산이기에 비트 연산으로 진행하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0786
    {

        static void Main786(string[] args)
        {

            StreamReader sr;

            int len;
            int[] arr;
            int[] mul;

            Solve();

            void Solve()
            {

                Input();

                SetMul();

                long ret = GetRet();

                Console.Write(ret);
            }

            long GetRet()
            {

                long ret = 0;
                long before = 0;

                for (int i = 1; i < len; i++)
                {

                    long add = mul[i] + before;
                    if (add < 0) add = 0;
                    ret += add;
                    before = add;
                }

                return ret;
            }

            void SetMul()
            {

                mul = new int[len];

                for (int i = 1; i < len; i++)
                {

                    int f = arr[i - 1];
                    int b = arr[i];
                    if (b < f)
                    {

                        while (b < f)
                        {

                            mul[i]++;
                            b *= 2;
                        }
                    }
                    else
                    {

                        mul[i] = 1;
                        while (f <= b)
                        {

                            mul[i]--;
                            f *= 2;
                        }
                    }
                }
            }

            void Input() 
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                len = ReadInt();

                arr = new int[len];

                for (int i = 0; i < len; i++)
                {

                    arr[i] = ReadInt();
                }

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
// #include <iostream>
// #include <algorithm>
using namespace std;

int A[250010],B[250010];
int main()
{
    ios::sync_with_stdio(0);
    cin.tie(0);
    cout.tie(0);
    long long N,S=0;
    cin>>N;
    for(long long i=0;i<N;i++) cin>>A[i];
    for(long long i=1;i<N;i++)
    {
        if(A[i]==A[i-1]) B[i] = B[i-1];
        else if(A[i]<A[i-1])
        {
            long long n=0,k=A[i];
            while(k<A[i-1])
            {
                k *= 2;
                n++;
            }
            B[i] = B[i-1] + n;
        }
        else
        {
            long long z=0,n=0,k=A[i-1];
            while(k<=A[i])
            {
                k *= 2;
                n++;
            }
            n--;
            B[i] = max(B[i-1]-n,z);
        }
        S+= B[i];
    }
    cout<<S;
}
#elif other2
import java.io.*;
import java.util.*;

public class Main {

    public static void main(String[] args) throws IOException {
        Solution.createSolution()
                .start();
    }
}

class Solution{

    int N;
    int[] A;

    int countSum = 0;
    private void readInput() throws IOException{
        N = Integer.parseInt(br.readLine());

        A = new int[N];
        st = new StringTokenizer(br.readLine());
        for(int i=0; i<N; i++){
            A[i] = Integer.parseInt(st.nextToken());
        }
    }

    private void solve(){
        for(int i=1; i<N; i++){
            if(A[i-1]<=A[i]) continue;

            while(A[i-1]>A[i]) {
                A[i] = A[i]<<1;
                countSum++;
            }
        }
    }

    private void writeOutput() throws IOException{
        bw.write(Integer.toString(countSum));
        bw.flush();
    }

    public void start() throws IOException{
        readInput();
        solve();
        writeOutput();
    }

    private final BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    private final BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
    private StringTokenizer st;

    private Solution(){}
    public static Solution createSolution(){
        return new Solution();
    }
}
#endif
}
