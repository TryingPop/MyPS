using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 리유나는 세일러복을 좋아해
    문제번호 : 18138번

    이분 매칭 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0605
    {

        static void Main605(string[] args)
        {

            StreamReader sr;

            int n, m;

            List<int>[] line;
            bool[] visit;
            int[] match;

            Solve();

            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }
                return false;
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));

                n = ReadInt();
                m = ReadInt();

                line = new List<int>[n + 1];
                for(int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                int[] c = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    c[i] = ReadInt();
                }

                for (int i = 1; i <= m; i++)
                {

                    int cur = ReadInt();
                    for (int j = 1; j <= n; j++)
                    {


                        if ((cur <= (c[j] * 3) / 4 && (int)Math.Ceiling(c[j] / 2f) <= cur) 
                            || (cur <= (c[j] * 5) / 4 && c[j] <= cur)) line[j].Add(i);
                    }
                }

                match = new int[m + 1];
                visit = new bool[m + 1];

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;

public class Main {
    private static int n, m;
    private static int[] nArr, mArr, owner;
    private static ArrayList<Integer>[] edge;
    private static boolean[] v;

    private static void init() throws Exception {
        n = nextInt();
        m = nextInt();

        nArr = new int[n];
        mArr = new int[m];

        edge = new ArrayList[n];
        for (int i = 0; i < n; i++) edge[i] = new ArrayList<>();

        for (int i = 0; i < n; i++) nArr[i] = nextInt();
        for (int i = 0; i < m; i++) mArr[i] = nextInt();
    }

    private static void makeEdge() {
        for (int i = 0; i < n; i++) {
            int cur = nArr[i];
            double standard1 = cur*0.5;
            double standard2 = cur*0.75;
            double standard4 = cur*1.25;

            for (int j = 0; j < m; j++) {
                double target = mArr[j];
                if (standard1<=target && target<=standard2 || cur<=target && target<=standard4)
                    edge[i].add(j);
            }
        }
    }

    private static boolean matching(int num) {
        ArrayList<Integer> e = edge[num];
        for (int i = 0; i < e.size(); i++) {
            int target = e.get(i);
            if (v[target]) continue;
            v[target] = true;

            if (owner[target] == -1 || matching(owner[target])) {
                owner[target] = num;
                return true;
            }
        }

        return false;
    }

    private static void solution() throws Exception {
        init();
        makeEdge();

        nArr = mArr = null; // only need for 'makeEdge'

        owner = new int[m];
        Arrays.fill(owner, -1);

        for (int i = 0; i < n; i++) {
            v = new boolean[m];
            matching(i);
        }

        int answer = 0;
        for (int i = 0; i < m; i++) {
            if (owner[i] != -1)
                answer++;
        }
        System.out.println(answer);
    }

    public static void main(String[] args) throws Exception {
        initFI();
        solution();
    }

    private static final int DEFAULT_BUFFER_SIZE = 1 << 16;
    private static final int MAX_CHAR_LEN_FOR_NEXT_LINE = 20;
    private static DataInputStream inputStream;
    private static byte[] buffer;
    private static int curIdx, maxIdx;

    private static void initFI() {
        inputStream = new DataInputStream(System.in);
        buffer = new byte[DEFAULT_BUFFER_SIZE];
        curIdx = maxIdx = 0;
    }

    private static int nextInt() throws IOException {
        int ret = 0;
        byte c = read();
        while (c <= ' ') c = read();
        do {
            ret = ret * 10 + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return ret;
    }

    private static byte read() throws IOException {
        if (curIdx == maxIdx) {
            maxIdx = inputStream.read(buffer, curIdx = 0, DEFAULT_BUFFER_SIZE);
            if (maxIdx == -1) buffer[0] = -1;
        }
        return buffer[curIdx++];
    }
}
#endif
}
