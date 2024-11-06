using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 15
이름 : 배성훈
내용 : Bathroom Stalls (Small1, Small2, Large)
    문제번호 : 14792번, 14793번, 14794번

    수학, 자료구조 문제다
    Small1부터 풀었다
    Small1에서 처음과 끝만 필요함을 알았고
    길이를 우선순위 큐에 저장한 뒤 꺼내어 읽었다
    그런데 출력에서 케이스 번호 부분을 1로 고정시켜 4번 틀렸고
    이후 수정하니 이상없이 전부 통과했다

    그리고 Small2에 해당 코드를 제출하니 시간초과 떴고
    조금 생각해보니 길이가 같은 것들은 결과가 같았다

    이에 같은것들은 개수를 배열에 저장해놓고 묶어서 계산했고
    이렇게 Small2를 통과했다

    이후 Large를 보니 long 범위라 배열에 저장은 불가능해보였고
    dictionary에 저장해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0970
    {

        static void Main970(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            long n, k;
            PriorityQueue<long, long> pq;
            Dictionary<long, long> dic;

            long ret1, ret2;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                for (int t = 1; t <= test; t++)
                {

                    Input();

                    GetRet();

                    sw.Write($"Case #{t}: {ret1} {ret2}\n");
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                ret1 = 0;
                ret2 = 0;

                while (pq.Count > 0)
                {

                    long node = pq.Dequeue();
                    long l = (node - 1) >> 1;
                    long r = node >> 1;
                    k -= dic[node];


                    if (k <= 0)
                    {

                        ret1 = Math.Max(l, r);
                        ret2 = Math.Min(l, r);
                        pq.Clear();
                        dic.Clear();
                        break;
                    }

                    if (0 <= l)
                    {

                        if (!dic.ContainsKey(l)) 
                        { 

                            pq.Enqueue(l, l); 
                            dic[l] = 0;
                        }
                        dic[l] += dic[node];
                    }

                    if (0 <= r)
                    {

                        if (!dic.ContainsKey(r)) 
                        { 
                            
                            pq.Enqueue(r, r); 
                            dic[r] = 0;
                        }
                        dic[r] += dic[node];
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pq = new(1_000_000, Comparer<long>.Create((x, y) => y.CompareTo(x)));
                dic = new(1_000);
            }

            void Input()
            {

                n = ReadLong();
                k = ReadLong();

                pq.Enqueue(n, n);
                dic[n] = 1;
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
// #include <stdio.h>

int main() {
    int ti, t;
    scanf("%d", &t);
    
    for(ti=0;ti<t;ti++) {
        long long int n, k, li = 1, stalls;
        scanf("%lld %lld", &n, &k);
        
        while (k >= li)
            li *= 2;
        li /= 2;
        stalls = (n - (k - li)) / li;
        
        printf("Case #%d: %lld %lld\n", ti+1, stalls/2, (stalls-1)/2);
    }
    
    return 0;
}
#endif
}
