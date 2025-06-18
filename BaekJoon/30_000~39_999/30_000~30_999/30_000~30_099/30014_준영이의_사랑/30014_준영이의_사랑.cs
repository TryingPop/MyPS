using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 18
이름 : 배성훈
내용 : 준영이의 사랑
    문제번호 : 30014번

    그리디, 정렬 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1711
    {

        static void Main1711(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => y.CompareTo(x));
                int[] maxArr = new int[n];

                maxArr[0] = arr[0];
                maxArr[1] = arr[1];
                maxArr[2] = arr[2];
                int ret = arr[0] * arr[1] + arr[1] * arr[2] + arr[2] * arr[0];

                for (int i = 3; i < n; i++)
                {

                    int sum = ret;
                    ret += arr[i] * maxArr[0] + arr[i] * maxArr[i - 1] - maxArr[0] * maxArr[i - 1];
                    int insert = 0;

                    for (int j = 1; j < i; j++)
                    {

                        int pop = maxArr[j] * maxArr[j - 1];
                        int add = maxArr[j] * arr[i] + maxArr[j - 1] * arr[i];

                        if (ret < sum + add - pop)
                        {

                            ret = sum + add - pop;
                            insert = j;
                        }
                    }

                    Insert(insert, arr[i]);
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{ret}\n");
                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{maxArr[i]} ");
                }

                void Insert(int _idx, int _val)
                {

                    for (int i = n - 1; i >= _idx + 1; i--)
                    {

                        maxArr[i] = maxArr[i - 1];
                    }

                    maxArr[_idx] = _val;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
    }

#if other
// #include <stdio.h>
typedef long long ll;
int main(){
	ll n, i, j, num, s=0, start=0;
	scanf("%lld", &n);
	ll a[4000]={0}, cnt[1001]={0}, b[4000]={0};
	for(i=0; i<n; i++){
		scanf("%lld", &num);
		cnt[num]++;
	}
	num=0;
	for(i=1; i<1001; i++){
		for(j=0; j<cnt[i]; j++){
			a[num++]=i;
		}
	}
	for(i=0; i<n; i++){
		if(i&1){
			b[2000+i/2+1]=a[n-i-1];
		}
		else{
			b[2000-i/2]=a[n-i-1];
		}
	}
	for(i=0; i<4000; i++){
		if(!b[i]) continue;
		if(!start) start=i;
		if(!b[i+1]) s+=b[i]*b[start];
		else s+=b[i]*b[i+1];
	}
	printf("%lld\n", s);
	for(i=0; i<4000; i++){
		if(!b[i]) continue;
		printf("%lld ", b[i]);
	} 
	return 0;
}
#endif
}
