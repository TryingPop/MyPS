using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 2
이름 : 배성훈
내용 : 구조조정
    문제번호 : 3132번

    날짜 : 2024. 7. 18 -> 4개월 동안 고정된 문제다;
    정렬, 애드혹, 그래프 이론 문제다
    트리가 주어진다 그래서 A노드에서 B노드로 가는 길은 유일하다
    우선 가장 깊은 리프(자식이 없는 노드)의 부모 L 에서 규칙을 찾아갔다

    L의 자식이 3개, 4개 있는 경우 2개이하로 줄이고
    자신보다 높은 사람을 1개 이하로 줄여야한다
    이에 이진트리가 가장 먼저 떠올랐고
    왼쪾에는자기보다 큰거, 오른쪽에는 자기보다 작은것 중 가장 큰거를 생각했다
    하지만 이는 리프의 부모에게만 유효할 뿐, 2 ~ 3단계 위의 조상으로 가면 순서가 꼬일거처럼 보였다
    그러면 한줄로 이으면 될까 생각했고
    L의 부모 에도 이상없이 조건을 만족해서 수학적귀납법으로 된다 판단했다
    
    아이디어는 다음과 같다
    자식은 iq가 높은 순으로 정렬해
    부모 - iq가 가장 높은 자식 - iq가 두 번째로 높은 자식 - ... - iq가 제일 낮은 자식
    순으로 자식을 세팅한다

    즉, A노드 iq : 77, A노드의 자식들이 150, 84 iq를 갖고 있으면
    기존에는
                        A(77)
                /       |   
            84          150
            |           |
        (84의 자식)    (150의 자식)


    다음과 같이 설정되어져 있다    
    위 규칙으로 수정하면
                        A
                        |
                        180
                /       |
          (150의 자식)  84
                        |
                    (84의 자식)

    이렇게 설정하면 자식 수는 2명으로 보장된다
    그리고 180 밑에 자기보다 작은 자식만 추가되기에
    많아야 자식이 1개도 보장된다
    마지막으로 이전 워크 그룹의 관계에서 자식 부모가 생성되어 모든 조건을 만족한다
    
    여러 자식들을 1개의 자식으로 만들기에 리프에서 시작하면 조건을 만족하면서 이상없이 작동한다
    (수학적 귀납법으로 증명도 된다)
    그리고 해당 규칙을 전체에 적용할 시 순서는 딱히 영향을 안주어 for문으로 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0149
    {

        static void Main149(string[] args)
        {

            StreamReader sr;

            int n;
            int[] iq;
            PriorityQueue<int, int>[] line;
            int[] p;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    if (p[i] == 0) continue;
                    sw.Write($"{p[i]} {i}\n");
                }
                sw.Close();
            }

            void GetRet()
            {

                for (int i = 1; i <= n; i++)
                {

                    int parent = i;
                    while (line[i].Count > 0)
                    {

                        int node = line[i].Dequeue();
                        p[node] = parent;
                        parent = node;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                p = new int[n + 1];
                iq = new int[n + 1];
                line = new PriorityQueue<int, int>[n + 1];
                
                for (int i = 1; i <= n; i++)
                {

                    iq[i] = ReadInt();
                    line[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    p[t] = f;

                    line[f].Enqueue(t, -iq[t]);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1  && c != ' ' && c != '\n')
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
// #include <vector>
// #include <algorithm>
using namespace std;

int n, arr[1024];
vector<int> conn[1024];

bool cmp(int a, int b){
    return arr[a] > arr[b];
}

int main(){
    scanf("%d", &n);
    for(int i = 1; i <= n; i++) scanf("%d", &arr[i]);
    for(int i = 0; i < n - 1; i++){
        int a, b;
        scanf("%d %d", &a, &b);
        conn[a].push_back(b);
    }
    for(int i = 1; i <= n; i++){
        sort(conn[i].begin(), conn[i].end(), cmp);
        for(int j = 0; j < conn[i].size(); j++){
            if(j == 0) printf("%d %d\n", i, conn[i][j]);
            else printf("%d %d\n", conn[i][j - 1], conn[i][j]);
        }
    }
}
#endif
}
