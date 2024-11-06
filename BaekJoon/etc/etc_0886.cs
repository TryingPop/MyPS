using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 19
이름 : 배성훈
내용 : 검문
    문제번호 : 2981번

    수학, 정수론, 유클리드 호제법 문제다
    나머지가 같은 수를 찾으니 
    
    i < n인 i에 대해 ai = r(mod p)이다
    p가 만약 j < n인 j에 대해 aj = r(mod p)라 하면
    ai - aj = 0 (mod p) 이다
    즉, ai와 aj를 p로 나누었을 때 나머지가 같다면 
    ai - aj는 p로 나눠떨어진다
    모든 i < n 에 대해 ai = r(mod p)라하면
    ai - a0 = 0 (mod p)가 성립한다

    반대로 aj - a0 != 0 (mod p)인 j < n이 존재하면
    이는 aj와 a0를 각각 p로 나눈 나머지가 다르다는 말과 동형이므로
    각 i < n에 대해 ai - a0 = 0을 만드는 p를 찾으면 된다
    
    그래서 ai - a0의 gcd인 g 찾으면
    gcd의 정의로 ai - a0 = 0 (mod g) 가 성립하는 가장 큰 수가된다
    그래서 gcd를 찾고 gcd의 약수를 찾으면 된다
    gcd의 약수들은 모두 ai - a0를 나누는 수가 된다

    이렇게 gcd의 약수들 중 2 이상을 출력하면 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0886
    {

        static void Main886(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            List<int> GetFactor(int _n)
            {

                List<int> ret = new();

                for (int i = 1; i * i <= _n; i++)
                {

                    if (_n % i > 0) continue;
                    int j = _n / i;
                    ret.Add(i);
                    if (j != i) ret.Add(j);
                }

                ret.Sort();
                return ret;
            }

            void GetRet()
            {

                int gcd = arr[0];
                for (int i = 1; i < n - 1; i++)
                {

                    gcd = GetGCD(gcd, arr[i]);
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    List<int> ret = GetFactor(gcd);
                    for (int i = 1; i < ret.Count; i++)
                    {

                        sw.Write($"{ret[i]} ");
                    }
                }
            }

            int GetGCD(int _a, int _b)
            {

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n - 1];
                int sub = ReadInt();
                for (int i = 0; i < n - 1; i++)
                {

                    int cur = ReadInt();
                    arr[i] = Math.Abs(cur - sub);
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
using System;
using System.Collections.Generic;
using System.Text;

namespace C__study
{
    internal class backjoon334
    {
        public static int get_gcd(int a, int b)
        {
            while (b != 0)
            {
                int tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }

        public static void Main()
        {
            StringBuilder sb = new StringBuilder();
            int num = int.Parse(Console.ReadLine());
            int[] nums = new int[num];
            for (int i = 0; i < num; i++)
            {
                nums[i] = int.Parse(Console.ReadLine());
            }
            Array.Sort(nums);
            int include = nums[1] - nums[0];

            for (int i = 2; i < num; i++)
            {
                include = get_gcd(include, nums[i] - nums[i - 1]);
            }

            List<int> list = new List<int>();
            for (int i = 1; i * i <= include; i++)
            {
                if (include % i == 0)
                {
                    list.Add(i);
                    if (i != include / i)
                        list.Add(include / i);
                }
            }
            list.Remove(1);
            list.Sort();
            foreach (int divisor in list)
            {
                sb.Append(String.Format("{0} ", divisor));
            }
            Console.WriteLine(sb.ToString().Trim());
        }
    }
}
#elif other2
// #include<stdio.h>
void qsort(int s, int e, int* map){
	int l,t=s, p=e;
	int temp;
	if(s>=e) return;
	for(l=s;l<e;l++){
		if(map[l]<map[p]){
			temp=map[l];
			map[l]=map[t];
			map[t]=temp;
			t++;
		}
	}
	temp=map[p];
	map[p]=map[t];
	map[t]=temp;
	qsort(s,t-1,map);
	qsort(t+1,e,map);	
}
int gcd(int a, int b){
	int g;
	if(b%a!=0) g=b%a;
	else return a;
	return gcd(g,a);
}
int main(){
	int N, map[101]={0},b[101]={0},ans[500]={0},count=0;
	int i,j;
	scanf("%d",&N);
	for(i=0;i<N;i++){
		scanf("%d",&map[i]);
	}
	qsort(0,N-1,map);
	for(i=1;i<N;i++){
		b[i-1]=map[i]-map[i-1];
	}
	qsort(0,N-2,b);
	int g=b[0];
	for(i=1;i<N-1;i++) {
		g = gcd(g,b[i]);	
	}
	for(i=1;i*i<=g ;i++){
		if(g%i==0){
			ans[count++]=i;
			if(i!=g/i){
				ans[count++]=g/i;
			}
		}	
}
	
	qsort(0,count-1,ans);
	for(i=1;i<count;i++){
		printf("%d ",ans[i]);
	}
}
#endif
}
