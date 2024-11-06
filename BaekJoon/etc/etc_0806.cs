using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 10
이름 : 배성훈
내용 : 소셜네트워크
    문제번호 : 3098번

    구현, 그래프 이론 문제다
    브루트포스로 구했다

    기여를 보니 혹은 트리에서 거리가 2^k 이하로 
    찾아도 된다고 한다
    이 경우 플로이드 워셜을 써서 전체 거리를 찾아 풀거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0806
    {

        static void Main806(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[][] line;
            int conn;
            List<int> ret;
            Solve();

            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret.Count}\n");
                for (int i = 0; i < ret.Count; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }

                sw.Close();
            }

            void GetRet()
            {

                int day = 1;
                int escape = (n * (n - 1)) / 2;

                ret = new(n);

                while(conn < escape)
                {

                    int add = 0;
                    for (int i = 1; i <= n; i++)
                    {

                        for (int j = 1; j <= n; j++)
                        {

                            // 직전에 연결된 곳에서 새로 이어줘야 한다
                            // BFS 식이다
                            if (line[i][j] != day || i == j) continue;
                            
                            // i 이하만 찾는다 
                            for (int k = 1; k < i; k++)
                            {

                                // 새롭게 추가해야하는지 판별
                                if (line[j][k] == 0 || day < line[j][k] || line[i][k] != 0) continue;
                                line[i][k] = day + 1;
                                line[k][i] = day + 1;
                                add++;
                            }
                        }
                    }

                    day++;
                    conn += add;
                    ret.Add(add);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                conn = 0;
                line = new int[n + 1][];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new int[n + 1];
                }

                int len = ReadInt();
                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f][b] = 1;
                    line[b][f] = 1;

                    conn++;
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
using System.IO;
using System.Text;

namespace ConsoleApp2
{

    class Program
    {

        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();

            string inputNumbers = Console.ReadLine();
            string[] inputNumbersSlice = inputNumbers.Split();

            int[,] peopleRelations = new int[50, 50];

            int passPeople = 0;

            int peopleAmount = int.Parse(inputNumbersSlice[0]);
            int beginningPeopleRelationAmount = int.Parse(inputNumbersSlice[1]);

            int untilAllFriendSpendTime = 0;

            for (int i = 0; i < beginningPeopleRelationAmount; i++)
            {
                using (StringReader reader = new StringReader(Console.ReadLine()))
                {
                    string[] inputNumbers2Slice = reader.ReadLine().Split();
                    int[] numbers = new int[2] { int.Parse(inputNumbers2Slice[0]), int.Parse(inputNumbers2Slice[1]) };
                    peopleRelations[numbers[0] - 1, numbers[1] - 1] = 1;
                    peopleRelations[numbers[1] - 1, numbers[0] - 1] = 1;
                }
            }
            while (passPeople != peopleAmount)
            {


                int createRelations = 0;
                for (int i = 0; i < peopleAmount; i++)
                {
                    for (int j = 0; j < peopleAmount; j++)
                    {
                        if (peopleRelations[i, j] == 1 && i != j)
                        {
                            for (int k = 0; k < peopleAmount; k++)
                            {
                                if (peopleRelations[j, k] == 1 && peopleRelations[i, k] == 0 && i != k && peopleRelations[k, i] == 0)
                                {
                                    peopleRelations[i, k] = 2;
                                    createRelations++;
                                }
                            }
                        }
                    }
                }
                builder.Append(createRelations + "\n");
                untilAllFriendSpendTime++;

                for (int i = 0; i < peopleAmount; i++)
                {
                    for (int j = 0; j < peopleAmount; j++)
                    {
                        if (peopleRelations[i, j] == 2)
                        {
                            peopleRelations[i, j] = 1;
                            peopleRelations[j, i] = 1;
                        }
                    }
                }

                for (int i = 0; i < peopleAmount; i++)
                {
                    int RelationsAmount = 0;
                    for (int j = 0; j < peopleAmount; j++)
                    {
                        if (peopleRelations[i, j] == 1 && i != j)
                        {
                            RelationsAmount++;
                        }
                    }
                    if (RelationsAmount == peopleAmount - 1)
                    {
                        passPeople++;
                    }
                    else
                    {
                        passPeople = 0;
                        break;

                    }
                }

            }
            Console.WriteLine(untilAllFriendSpendTime);
            Console.WriteLine(builder);
        }
    }
}
#elif other2
// #include <cstdio>

using namespace std;

int main()
{
    int n,m;
    int b[55][55]={};

    scanf("%d %d", &n, &m);
    for(int i=0; i<m; i++)
    {
        int A,B;
        scanf("%d %d", &A, &B);
        b[A][B]=1;
        b[B][A]=1;

    }

    int d=1;
    int ans[1300]={};
    while(1)
    {
        int cnt=0;
        for(int i=1; i<=n; i++)
        {
            for(int j=1; j<=n; j++)
            {
                if(b[i][j]==0 || b[i][j]>d)
                    continue;
                for(int k=1; k<=n; k++)
                {
                    if(b[j][k]==0 || k==j)
                        continue;
                    if(b[j][k]>d || k==i)
                        continue;
                    if(b[i][k]==0)
                    {
                        b[i][k]=d+1;
                        b[k][i]=d+1;
                        cnt++;
                    }
                }
            }
        }
        if(cnt==0)
            break;
        d++;
        ans[d] = cnt;
    }

    printf("%d\n", d-1);

    for(int i=2; i<=d; i++)
    {
        printf("%d\n", ans[i]);
    }

    return 0;
}
#elif other3
 
import java.util.Scanner;

public class Main {
    static int N, M;
    static int[][] graph;
    static int[][] visit;
    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        N = in.nextInt(); M = in.nextInt();
        in.nextLine();
        graph = new int[N+1][N+1];
        visit = new int[N+1][N+1];
        for(int i=0;i<M;i++) {
            String[] arr = new String[2];
            arr = in.nextLine().split(" ");
            graph[Integer.parseInt(arr[0])][Integer.parseInt(arr[0])] = 1;
            graph[Integer.parseInt(arr[1])][Integer.parseInt(arr[1])] = 1;
            graph[Integer.parseInt(arr[0])][Integer.parseInt(arr[1])] = 1;
            graph[Integer.parseInt(arr[1])][Integer.parseInt(arr[0])] = 1;
        } 
        find();
    }
    public static void find() {
        int day = 0;
        boolean notAllFriends = true;
        int[] rel = new int[N*(N-1)/2];
        while(notAllFriends) {
            notAllFriends = false;
            for(int i=1;i<=N;i++) {
                for(int j=1;j<=N;j++) {
                    if(graph[i][j]==0) {
                        notAllFriends = true;
                    }
                }
            }
            if(!notAllFriends) {
                break;
            }
            day++;
            rel[day] = 0;
            for(int i=1;i<=N;i++) {
                for(int j=1;j<=N;j++) {
                    if(i!=j&&graph[i][j]==1&& visit[i][j]!=1) {
                        for(int k=1;k<=N;k++) {
                            if(graph[i][k]==0&&graph[j][k]==1&&visit[j][k]!=1) {
                                graph[i][k] = 1;
                                visit[i][k] = 1;
                                rel[day]++;
                            }
                        }
                    }
                }
            }
            for(int i=1;i<=N;i++) {
                for(int j=1;j<=N;j++) {
                    visit[i][j] = 0;
                }
            } 
        }
        System.out.println(day);
        for(int i=1;i<=day;i++) {
            System.out.println(rel[i]/2);
        }
    }
}

#endif
}
