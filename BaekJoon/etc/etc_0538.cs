using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 학회원
    문제번호 : 3865번

    DFS, BFS, 문자열, 해시 문제다
    문자열 말고 int로 바꿔 풀려다가 잦은 에러로 4번 틀렸다
    해도 확률적이라 의미 있나 싶어 그냥 문자열로 해결했다
    4번 틀린 이유가, \r로 탈출코드 짜다가 백준에서는 \r\n이 아닌 \n이기에 무한 루프 탈출못해 시간초과와,
    네임 스페이스 선언으로 컴파일에러, 어디서 나온지 모르는 인덱스 에러, 그리고 추측하기로 문자열 중복으로 틀린거로
    이유가 모두 다르다!

    아이디어는 다음과 같다
    먼저 문자를 숫자로 변형한다 여기서 사전 자료구조가 쓰였다

    학회에는 많아야 10명이 포함되고 학회는 100이하 이므로 11배 해주면 된다
    그런데 11보다는 12가 좋아 1200으로 세팅했다 초기화도 12 * n만큼만 해줬다 

    탐색을 위해 그룹에서 시작해서 다른 그룹이나 사람으로 가는 간선을 놓았다
    학회는 따로 bool 배열을 둬서 true false 처리했다 최대 크기가 1200밖에 안되기에 bool 배열로했다

    마지막으로 간선을 다 연결하면 0에서 시작해(제일 처음 주어지는 학회!)
    사람 수를 세어 제출하니 이상없이 72ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0538
    {

        static void Main538(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            Dictionary<string, int> dic = new(1_200);
            List<int>[] line = new List<int>[1_200];
            bool[] visit = new bool[1_200];
            bool[] group = new bool[1_200];

            for (int i = 0; i < 1_200; i++)
            {

                line[i] = new(10);
            }

            Solve();
            sr.Close();
            sw.Close();

            void Solve()
            {

                while (true)
                {

                    int n = ReadInt();
                    if (n == 0) break;

                    int idx = 0;

                    Init(n);
                    for (int i = 0; i < n; i++)
                    {

                        string[] temp = sr.ReadLine().Trim().Split(':');
                        string g = temp[0];

                        int f;
                        if (dic.ContainsKey(g)) f = dic[g];
                        else 
                        {

                            dic[g] = idx;
                            f = idx++;
                        }
                        group[f] = true;

                        temp = temp[1].Split(',');
                        for (int j = 0; j < temp.Length; j++)
                        {

                            string str = temp[j];
                            if (j == temp.Length - 1) str = str.Substring(0, str.Length - 1);

                            int b;
                            if (dic.ContainsKey(str)) b = dic[str];
                            else
                            {

                                dic[str] = idx;
                                b = idx++;
                            }

                            line[f].Add(b);
                        }
                    }

                    int ret = DFS();
                    sw.WriteLine(ret);
                }
            }

            int DFS(int _n = 0)
            {

                int ret = group[_n] ? 0 : 1;
                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;
                    ret += DFS(next);
                }

                return ret;
            }

            void Init(int _n)
            {

                dic.Clear();

                int len = _n * 12;
                for (int i = 0; i < len; i++)
                {

                    visit[i] = false;
                    group[i] = false;
                    line[i].Clear();
                }
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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringBuilder sb = new StringBuilder();

        while(true) {
            int tc = Integer.parseInt(br.readLine());
            if(tc == 0) break;

            HashSet<String> ans = new HashSet<>();
            HashSet<String> chk = new HashSet<>();
            HashMap<String, ArrayList<String>> map = new HashMap<>();
            String[] key = new String[tc];

            for(int i=0; i<tc; i++) {
                String exp = br.readLine();
                StringTokenizer st = new StringTokenizer(exp, ":");
                key[i] = st.nextToken();
                exp = st.nextToken();
                st = new StringTokenizer(exp, ",.");

                map.put(key[i], new ArrayList<>());
                ArrayList<String> list = map.get(key[i]);

                while(st.hasMoreTokens()) {
                    list.add(st.nextToken());
                }
            }

            Queue<String> q = new LinkedList<>();
            q.offer(key[0]);
            chk.add(key[0]);

            while(!q.isEmpty()) {
                String cur = q.poll();
                if(ans.contains(cur)) continue;
                if(map.get(cur) == null)
                    ans.add(cur);
                else {
                    ArrayList<String> t = map.get(cur);
                    for (String s : t) {
                        if(chk.contains(s)) continue;
                        q.offer(s);
                        chk.add(s);
                    }
                }
            }
            sb.append(ans.size()).append("\n");
        }
        System.out.println(sb.toString());
    }
}
#elif other2
import java.io.*;
import java.util.*;

public class Main {

    /**
     * 해당 학회의 총 인원수를 반환하는 함수
     */

    public static int getTotalPerson(String target) {

        // 결과값 변수 선언
        int result = 0;

        // 아직 방문하지 않은 경우에 대해서만 처리
        if (!visitLog.contains(target)) {

            // 방문 기록
            visitLog.add(target);

            // 사람이 아닌 학회일 경우
            if (clubGraph.containsKey(target)) {

                // 해당 학회의 방문하지 않은 구성원에 대해 재귀함수를 호출, 결과값에 합산
                for (String eachPerson : clubGraph.get(target)) {

                    // 구성원이 학회라면 재귀함수 호출
                    if (clubGraph.containsKey(eachPerson)) {
                        result += getTotalPerson(eachPerson);

                    // 학생이라면 아직 방문하지 않았을 경우 결과값 1 추가
                    } else {

                        if (!visitLog.contains(eachPerson)) {

                            visitLog.add(eachPerson);
                            result += 1;

                        }

                    }

                }

            }

        }

        // 결과값을 반환
        return result;

    }

    public static int strToInt(String inputString) {
        return Integer.parseInt(inputString);
    }

    // 학회 수, 첫 학회, 학회 관계 그래프, 학생 기록 집합 정적 변수 선언
    static int clubNumber;
    static String firstClub;
    static HashMap<String, ArrayList<String>> clubGraph = new HashMap<>();
    static Set<String> visitLog = new HashSet<>();

    public static void main(String[] args) throws IOException {

        BufferedReader input = new BufferedReader(new InputStreamReader(System.in));

        // 학회의 수 입력, 입력값이 0이라면 반복 종료
        while ((clubNumber = strToInt(input.readLine())) != 0) {

            clubGraph.clear();
            visitLog.clear();

            for (int i = 0; i < clubNumber; i++) {

                // 줄 단위 입력
                StringTokenizer clubInfo = new StringTokenizer(input.readLine(),"[:,.]");

                // 학회 이름 변수 선언
                String clubName = clubInfo.nextToken();

                // 첫 학회라면 따로 변수에 기록
                if (i == 0) {
                    firstClub = clubName;
                }

                // 기존에 없던 학회라면 추가
                if (!clubGraph.containsKey(clubName)) {
                    clubGraph.put(clubName, new ArrayList<>());
                }

                // 나머지를 전부 학회의 원소로 추가
                while (clubInfo.hasMoreTokens()) {
                    clubGraph.get(clubName).add(clubInfo.nextToken());
                }

            }

            // 함수를 호출하여 결과값을 출력 스택에 추가
            System.out.println(getTotalPerson(firstClub));

        }

    }

}
#endif
}
