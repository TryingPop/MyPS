using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 15
이름 : 배성훈
내용 : 숫자 맞추기
    문제번호 : 2494번

    48_03 아이디어와 같은 방법으로 했다
    이전에는 dp 2개로 돌려서 해결한 반면 여기서는 그냥 쭉 저장했다
    해당 값을 읽는데, 회전수와 증가, 감소가 활용된다!

    돌리는 횟수가 0 ~ 9이므로 이차원 배열 대신, 1차원으로 줄이고 맨 끝에 역할을 넣었다
    그런데 인덱스 연산을 해줘야해서 성능은 좋을지 모르지만, 코드 작성하는데 깔끔해 보이지 않는다

    다 풀고 나니 조금 더 메모리를 절약하는 방법이 보인다
        최단 경로 찾는데는 앞의 방법을 이용한다
        dp는, 회전수와 이전 idx를 활용한다

    그러면, bool 변수 1byte부분은 쓸모 없다!
    회전수에 기록되어져 있기 때문이다!

    또한, 이전 회전 수를 찾아가는 연산도 필요없다!
    그냥 적힌거만 읽으면 된다

    해당 부분은 내일 다시 작성하겠다!
*/

namespace BaekJoon._48
{
    internal class _48_04
    {

        static void Main4(string[] args)
        {

            int MAX = 100_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            string cur = sr.ReadLine();
            string pw = sr.ReadLine();

            sr.Close();

            // 튜플로 dp를 설정
            // 전체 회전값, 이전 회전값, 증가인지 감소인지 여부
            (int val, int before, bool isUp)[] dp = new (int val, int before, bool isUp)[(len + 1) * 10];

            Array.Fill(dp, (MAX, -1, false));
            dp[0].val = 0;

            {

                // 맨 꼭대기부터 1층 아래로 증가하는 층
                // 1층 연산
                int up = pw[0] - cur[0];
                if (up < 0) up += 10;
                else if (up > 9) up -= 10;

                int down = 10 - up;
                down = down == 10 ? 0 : down;

                dp[10] = (down, 0, false);
                dp[10 + up] = (up, 0, true);
            }


            for (int i = 2; i <= len; i++)
            {

                // 2층 ~ len 층까지 연산 시작
                int curIdx = i * 10;                    // 앞 idx
                int beforeIdx = curIdx - 10;            // 이전 층값 계승해야한다!
                int curUp = pw[i - 1] - cur[i - 1];     // 현재층 번호차

                // up비율을 0 ~ 9로 조절
                if (curUp < 0) curUp += 10;             
                else if (curUp > 9) curUp -= 10;

                for (int j = 0; j < 10; j++)
                {

                    // 이전 층이 존재하는 경우!만 연산한다
                    if (dp[curIdx - 10 + j].val == MAX) continue;

                    // 위층에서 증가 연산한거 조절 연산
                    int up = curUp - j;
                    if (up > 9) up -= 10;
                    else if (up < 0) up += 10;

                    int down = 10 - up;
                    down = down == 10 ? 0 : down;


                    // 감소 연산으로 돌려본다
                    int calc = down + dp[beforeIdx + j].val;
                    if (dp[curIdx + j].val > calc)
                    {

                        // 이전보다 기록된 회전 수가 작으면 저장
                        dp[curIdx + j] = (calc, j, false);
                    }

                    // 증가 연산
                    calc = up + dp[beforeIdx + j].val;
                    int idx = (j + up) % 10;

                    if (dp[curIdx + idx].val > calc)
                    {

                        // 이전보다 기록된 회전 수가 작으면 넣는다
                        dp[curIdx + idx] = (calc, j, true);
                    }
                }
            }

            // 저장완료, 이제 최소 값 찾는다
            int min = MAX;
            int findIdx = -1;                   // 최소값이 저장된 위치 저장
            for (int i = 0; i < 10; i++)
            {

                int idx = len * 10 + i;
                // 기록 안되었으니 넘긴다
                if (dp[idx].val == MAX) continue;

                // 기록된 곳에서 최소값 찾기
                if (dp[idx].val < min)
                {

                    min = dp[idx].val;
                    findIdx = i;
                }
            }

            // 회전 수 저장용도
            // 거꾸로 읽는 것을 강조하기 위해 Stack자료구조 활용했다
            Stack<int> rot = new Stack<int>(len);

            {

                int floor = 10 * len;
                
                while(floor > 9)
                {

                    // dp의 현재 idx
                    int curIdx = floor + findIdx;

                    findIdx = dp[curIdx].before;
                    int before = floor - 10;

                    // dp의 이전 idx
                    int beforeIdx = before + findIdx;

                    // 현재 최단 회전수 - 이전 최단 회전수 = 현재 층에서 움직인 횟수가 된다
                    int rotation = dp[curIdx].val - dp[beforeIdx].val;
                    // 증가로 돌았는지 감소로 돌았는지 확인
                    rotation = dp[curIdx].isUp ? rotation : -rotation;

                    rot.Push(rotation);
                    floor = before;
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(min);

                for (int i = 1; i <= len; i++)
                {

                    sw.Write(i);
                    sw.Write(' ');
                    sw.Write(rot.Pop());
                    sw.Write('\n');
                }
            }
        }
    }
#if other
using System.IO;
using System.Text;
using System;

class Programs
{
    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static int[] arr;
    static int[] answer;
    static int n;
    static int[,] dp = new int[10000, 10];
    static int[] rotateR = new int[10000];
    static int[] rotateL = new int[10000];
    static int[,] node=new int[10000,10];//2차원이어야하네..음...
    static int[,] result = new int[10000, 10];
  
    static int DFS(int current, int leftRotation)//쌓이는 값 위에서 아래로 총 회전할 값이 쌓임
    {
        if (current == n)
        {
            return 0;
        }
        if (dp[current, leftRotation] != -1)
        {
            return dp[current, leftRotation];
        }
        dp[current, leftRotation] = 0;
        //현재 번호에서 타겟 번호까지 최소값을 구함
        //양쪽방향을 구해서 최소값을 리턴
        //우측방향이니까 왼쪽 회전
        int next = (arr[current] + leftRotation) % 10;
        int rotationLeft = (answer[current]-next+20)%10;//이런 방법이 있다니!!
        ////좌측방향 우측회전
        int rotationRight =10-rotationLeft;//헐..
        rotateL[current] = rotationLeft;
        rotateR[current] = rotationRight;
        //5~6으로 돈다면?해결
        int l = DFS(current + 1, (leftRotation + rotationLeft) % 10);
        int r = DFS(current + 1, leftRotation);
        //회전 방향 둘중 하나를 선택하는 것이기 때문에
        int left = rotationLeft + l;
        int right = r + rotationRight;
        if(left<=right)
        {
            //양수 회전 전달
            node[current, leftRotation] = (leftRotation + rotationLeft) % 10;
            result[current, leftRotation] = rotationLeft;
        }
        else
        {
            //음수 회전 전달
            node[current, leftRotation] = leftRotation;
            result[current, leftRotation] =-rotationRight;
        }
      return dp[current, leftRotation] = Math.Min(rotationLeft + l, r + rotationRight);
    }
    //고민중에 떠오른 아이디어: union find까지는 필요없을듯?. 배열이 결국 값이 낮아지기에 큰값은 작은 값을 향하게 되어있음.
    //배열의 값은 왼쪽으로 회전한 회전수.
    //마지막 왼쪽회전값들을 리스트로 가져오고 차례대로 살펴보는 식으로? 아니야 동적계획법 dp로 이미 값을 구하는 방식이 맞음.
    /*
4
7951
1857
    */
    //회전 수에서 그다음 회전수로 진행 
    //부모가 자식 노드를 향하게 하지말고 반대로 자식노드가 부모를향하게 접근해보자.
    static void FindNode(int depth, int rotationLeft, int total, int target)
    {
        sw.WriteLine($"{depth + 1} {result[depth,rotationLeft]}");
        if (depth == n - 1)
        {
            return;
        }
        FindNode(depth + 1, node[depth,rotationLeft], total + Math.Abs(result[depth,rotationLeft]), target);
    }
    static void Main(String[] args)
    {
        n = int.Parse(sr.ReadLine());
        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                dp[i, j] = -1;
                node[i, j] = -1;
                result[i, j] = 0;
            }
        }
        arr = new int[n];
        answer = new int[n];
        string str = sr.ReadLine();
        for (int j = 0; j < n; j++)
        {
            arr[j] = Convert.ToInt32(str[j] - '0');
        }
        str = sr.ReadLine();
        for (int j = 0; j < n; j++)
        {
            answer[j] = Convert.ToInt32(str[j] - '0');
        }
     DFS(0, 0);
        sw.WriteLine(dp[0, 0]);
        FindNode(0, 0, 0, dp[0, 0]);
        sw.Dispose();
    }
}

#endif
}
