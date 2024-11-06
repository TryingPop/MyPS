using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : 방 번호
    문제번호 : 1082번

    dp, 그리디 문제다
    그리디 알고리즘으로 해결했다

    아이디어는 다음과 같다
    수가 큰것은 먼저 자리수부터 확인하고 앞자리부터 큰지 확인하기에
    길이와 사용되는 수들의 개수가 필요하다

    먼저 만들 수 있는 숫자의 가장 긴 길이를 찾는다
    해당 길이에는 0으로만 이루어진 길이는 0으로 취급한다

    길이 찾는건 0이 아닌 가장 싼 숫자 1개를 살 수 있는지 확인하고 산다
    만약 못사면 길이는 0으로 종결짓고 종료한다

    만약 숫자를1개 샀다면 0을 포함한 가장 싼 숫자를 최대한 산다
    여기서 구매한 개수가 최대 길이가 된다

    이제 길이를 갖고 0을 제외한 숫자부터 가장 큰 수로 
    올릴 수 있으면 값을 올린다

    이는 0을 제외한 숫자가 0을 포함한 가장 싼 가격보다는
    같거나 크기 때문에 올리는데 들어가는 가격이 크지는 않다

    그래서 해당 수를 최대한 올리고
    남은 가격으로 이제 0을 포함한 나머지 숫자를 최대한 올린다

    이렇게 최대한 올렸다면, 해당 개수를 역순으로 출력하면 가장 큰 수가 된다
    앞에서 만약 길이가 0이면 0 개만 산걸로 생각해 출력한다
    (해당 문제는 적어도 하나 살 수 있다)
*/
namespace BaekJoon.etc
{
    internal class etc_0892
    {

        static void Main892(string[] args)
        {

            StreamReader sr;
            int n, m;
            int[] p;
            int[] cnt;
            int len, r;
            int min1, min2;
            int minVal1, minVal2;

            Solve();
            void Solve()
            {

                Input();

                GetLength();

                RemoveR();

                GetRet();
            }

            void RemoveR()
            {

                /*
                
                숫자 증가 시도
                */
                if (len == 0) return;

                for (int i = n - 1; i > minVal1; i--)
                {

                    if (r < p[i] - min1) continue;

                    int add = r / (p[i] - min1);
                    if (cnt[minVal1] < add) add = cnt[minVal1];
                    cnt[minVal1] -= add;
                    cnt[i] += add;

                    r -= add * (p[i] - min1);
                }

                for (int i = n - 1; i > minVal2; i--)
                {

                    if (r < p[i] - min2) continue;

                    int add = r / (p[i] - min2);
                    if (cnt[minVal2] < add) add = cnt[minVal2];
                    cnt[minVal2] -= add;
                    cnt[i] += add;

                    r -= add * (p[i] - min2);
                }
            }

            void GetRet()
            {

                if (len == 0) cnt[0]++;

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = n - 1; i >= 0; i--)
                    {

                        if (cnt[i] == 0) continue;
                        for (int j = 0; j < cnt[i]; j++)
                        {

                            sw.Write(i);
                        }
                    }
                }
            }

            void GetLength()
            {

                /*
                 
                가장 긴 길이 찾기 
                */
                cnt = new int[n];

                len = 0;

                r = m;
                minVal1 = 0;
                minVal2 = 0;
                min1 = 51;      // 0 포함 X
                min2 = p[0];    // 0 포함

                for (int i = 1; i < n; i++)
                {

                    if (p[i] < min1) min1 = p[i];
                    if (p[i] < min2) min2 = p[i];
                }

                if (r < min1) return; 

                for (int i = n - 1; i > 0; i--)
                {

                    if (min1 == p[i])
                    {

                        minVal1 = i;
                        r-= p[i];
                        cnt[i]++;
                        break;
                    }
                }

                len = 1;

                for (int i = n - 1; i >= 0; i--)
                {

                    if (p[i] == min2)
                    {

                        int add = r / min2;
                        len += add;
                        cnt[i] += add;
                        r -= min2 * add;
                        minVal2 = i;
                        break;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                p = new int[n];

                for (int i = 0; i < n; i++)
                {

                    p[i] = ReadInt();
                }

                m = ReadInt();
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
int n = int.Parse(Console.ReadLine());
int[] price = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
int minPrice = 50;
int minIndex = -1;
int minPrice2 = 50;
int minIndex2 = -1;
int idx = 0;
int[] r = new int[51];

for (int i = 0; i < n; i++)
{
    if (i > 0 && price[i] <= minPrice)
    {
        minPrice = price[i];
        minIndex = i;
    }
    if (price[i] <= minPrice2)
    {
        minPrice2 = price[i];
        minIndex2 = i;
    }
}

int m = int.Parse(Console.ReadLine());

if (m < minPrice)
{
    Console.WriteLine('0');
    return;
}

r[0] = minIndex;
m -= minPrice;
idx++;

while (m >= minPrice2)
{
    m -= minPrice2;
    r[idx] = minIndex2;
    idx++;
}

for (int i = 0; i < idx; i++)
{
    for (int j = n - 1; j > r[i]; j--)
    {
        if (m + price[r[i]] - price[j] >= 0)
        {
            m += price[r[i]];
            m -= price[j];
            r[i] = j;
            break;
        }
    }
}

for (int i = 0; i < idx; i++)
{
    Console.Write(r[i]);
}
#elif other2
using System.Linq.Expressions;
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        int count = Cin;
        if (count is 1){
            Cout = 0;
            return;
        }
        int[] expense = Cin;
        //최대한 큰 숫자로
        int min = count-1, min_without_zero = count-1;
        for(int i=count-2;i>0;i--) {
            if (expense[i] < expense[min]) min = i;
            if (expense[i] < expense[min_without_zero]) min_without_zero = i;
        }
        if (expense[0] < expense[min]) min = 0;
        int money = Cin;
        //최대한 길게
        LinkedList<int> list = new();
        //근데 돈이쓰?
        if (money < expense[min_without_zero]) {
            //0 이외의 가장 싼 숫자를 뽑을 돈이 없다는건.
            // 결국 0을 뽑을 돈만 있다는것
            Cout = 0;
            return;
        } else {
            list.AddFirst(min_without_zero);
            money -= expense[min_without_zero];
        }
        while(money >= expense[min]) {
            list.AddLast(min);
            money -= expense[min];
        }
        //업글
        int[] result = list.ToArray();
        for(int i=0;money>0 && i < result.Length;i++) {
            for(int n=count-1;n>result[i];n--) {
                int upgrade = expense[n]-expense[result[i]];
                if (upgrade <= money) {
                    money -= upgrade;
                    result[i] = n;
                }
            }
        }
        //출력
        Cout = string.Concat(result);
    }
}
#elif other3
// #include<stdio.h>
int main(){
    int N,P[10],M,p,i,j;
    for(scanf("%d",&N),i=0,p=50;i<N;p=i&&P[i]<p?P[i]:p,i++)scanf("%d",P+i);
    for(scanf("%d",&M),i=M<p?M:M-p;i>=(p<*P?p:*P);i-=p<P[0]?p:P[0]);
    if(M<p)printf("0");
    else{
        for(j=N;--j&&P[j]-p>i;);
        for(printf("%d",j),i-=P[j]-p,M-=P[j],p=p<*P?p:*P;M>=p;printf("%d",j),i-=P[j]-p,M-=P[j])
            for(j=N-1;P[j]-p>i;j--);
    }
}
#endif
}
