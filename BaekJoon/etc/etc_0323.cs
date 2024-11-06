using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 로마 숫자 만들기
    문제번호 : 16922번

    수학, 조합론, 구현, 백트래킹, 브루트포스 문제다
    처음에는 중복조합이지 싶어 중복조합 값으로 풀었으나
    예제 10과 다른 결과를 보여줬다

    그래서, 그냥 해쉬셋 들고, 중복을 제외하고 값을 넣어 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0323
    {

        static void Main323(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = int.Parse(sr.ReadLine());
            int[] cnt = { 0, 0, 0, 0 };
            int[] rom = { 1, 5, 10, 50 };
            HashSet<int> set = new HashSet<int>(1_000);
            DFS(n, 0);
            Console.WriteLine(set.Count);
            void DFS(int _n, int _b)
            {

                if (_n == 0)
                {

                    int ret = 0;
                    for (int i = 0; i < 4; i++)
                    {

                        ret += cnt[i] * rom[i];
                    }

                    set.Add(ret);
                    return;
                }

                for (int i = _b; i < 4; i++)
                {

                    int save = cnt[i];
                    for (int j = 1; j <= _n; j++)
                    {

                        cnt[i]++;
                        DFS(_n - j, i + 1);
                    }

                    cnt[i] = save;
                }
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
class Program
{
   static int[] l = { 1, 5, 10, 50 };
   static bool[] v = new bool[1001];
   static List<int> sum = new List<int>();
   public static int dfs(int s,int i,int k){
      if(k>0){
         int sum = 0;
         for(;i<4;i++){
             sum += dfs(s+l[i],i,k-1);
         }
         return sum;
     }
     else{
         if(!v[s]){
             v[s] = true;
             return 1;
         }
         else return 0;     
      }
   }
   static void Main(String[] args)
   {
      int N = int.Parse(Console.ReadLine());
      Console.WriteLine(dfs(0,0,N));
   }
}

#endif
}
