using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : 상범이의 우울
    문제번호 : 2811번

    구현, 브루트포스, 시뮬레이션, 누적합 문제다
    N번 확인을 4번해서 끝냈다

    아이디어는 다음과 같다
    먼저 정신병 발생하면 1을 아니면 0으로 입력 받았다
    다음으로 정신병이 연속해서 발현하는 기간을 누적해서 맨 앞에다가 저장했다
    그리고 최대 정신병 걸린 기간을 확인하며 기록했다
    (역순으로 탐색해서 앞에 쌓이게 했다)

    이제 역순으로 탐색하며 정신병 발현 시작인지 확인한다
    정신병 발현이 시작된다면 다음번부터 2배수의 날자동안 꽃을 주기 시작한다(역순이므로 이전날이 된다)
    꽃을 주는건 따로 bool 배열을 둬서 yes면 true 아니면 false로 했다
    정신병 발현이 만약 최대인 경우만 따로 3배수로 확인을한다
    그리고 3배수에서 가장 꽃을 많이 주는 날을 따로 저장한다

    이제 꽃을 준 날을 다시 확인한다
    그리고나서 3배수에서 준 최대값을 더해주고 반환하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0492
    {

        static void Main492(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();

            int[] arr = new int[n];
            
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                if (cur < 0) arr[i] = 1;
            }
            sr.Close();

            int max = 0;
            for (int i = n - 2; i >= 0; i--)
            {

                if (arr[i] > 0 && arr[i + 1] > 0)
                {

                    arr[i] = arr[i + 1] + 1;
                    arr[i + 1] = 0;
                }

                if (max < arr[i]) max = arr[i];
            }

            bool[] give = new bool[n];
            int trueNum = 0;
            int maxNum = 0;
            int chk = 0;
            int chkMax = 0;

            for (int i = n - 1; i >= 0; i--)
            {

                if (trueNum > 0)
                {

                    trueNum--;
                    give[i] = true;
                }

                if (maxNum > 0)
                {

                    maxNum--;
                    if (maxNum < max)
                    {

                        if (!give[i])
                        {

                            chk++;
                            chkMax = chkMax < chk ? chk : chkMax;
                        }
                    }
                }

                int cur = arr[i];
                
                if (cur > 0)
                {

                    int addNum = cur * 2;
                    trueNum = trueNum < addNum ? addNum : trueNum;
                    if (cur == max) 
                    { 
                        
                        maxNum = 3 * cur;
                        chk = 0;
                    }
                }
            }

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                if (give[i]) ret++;
            }

            ret += chkMax;
            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
#if other
import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;

public class Main {
    private void solution() throws Exception {
        int n = nextInt();
        int[] arr = new int[n+1];
        int cnt = 0;
        ArrayList<Integer> maxIdxs = new ArrayList<>();
        int max = 0;
        for (int i = 0; i < n+1; i++) {
            int cur = i==n ? 1 : nextInt();
            if (cur >= 0) {
                if (i-cnt >= 0)
                    arr[i-cnt] = cnt;
                if (max < cnt) {
                    max = cnt;
                    maxIdxs = new ArrayList<>();
                }
                if (max <= cnt)
                    maxIdxs.add(i-cnt);
                cnt = 0;
            } else {
                cnt++;
            }
        }

        boolean[] day = new boolean[n];
        cnt = 0;
        for (int i = 0; i < n; i++) {
            if (arr[i] == 0) continue;
            for (int j = Math.max(0, i-2*arr[i]); j <= Math.max(0, i-1); j++) {
                if (i != 0 && !day[j]) {
                    cnt++;
                    day[j] = true;
                }
            }
        }

        int maxCnt = -1;
        for (int i : maxIdxs) {
            int tmpCnt = 0;
            for (int j = Math.max(0, i-3*arr[i]); j <= Math.max(0, i-1); j++) {
                if (i != 0 && !day[j])
                    tmpCnt++;
            }
            if (tmpCnt > maxCnt) {
                maxCnt = tmpCnt;
            }
        }

        System.out.println(cnt+maxCnt);
    }

    public static void main(String[] args) throws Exception {
        initFI();
        new Main().solution();
    }

    private static final int DEFAULT_BUFFER_SIZE = 1 << 16;
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
        boolean neg = (c == '-');
        if (neg) c = read();
        do {
            ret = ret * 10 + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        if (neg) return -ret;
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
#elif other2
import java.util.*;
import java.io.*;

// https://www.acmicpc.net/problem/2811

class Main {
    static int stoi(String s) {
        return Integer.parseInt(s);
    }

    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st;

        int N = stoi(br.readLine());
        int[] day = new int[N+1];
        boolean[] flower = new boolean[N+1];
        int length = 0;
        int count = 0;
        int max = 0;
        int result = 0;

        st = new StringTokenizer(br.readLine());
        for(int i=0; i<N; i++)
            day[i] = stoi(st.nextToken());

        // 우울 기간 찾기
        for(int i=N-1; i>=0; i--) {
            if(day[i] < 0) {
                length++;
                continue;
            }

            // 우울한 기간의 *2 만큼 꽃 주기(중복된 날 x)
            for(int j=i+1-length*2; j<=i; j++) {
                if(j < 0 || flower[j])  
                    continue;
                flower[j] = true;
                count++;
            }

            // 최대 우울기간 찾아두기
            if(max < length) 
                max = length;

            length = 0;
        }

        // 최대 우울 기간은 *3 만큼 꽃준거 계산
        int maxDay = 0;
        for(int i=N-1; i>=0; i--) {
            if(day[i] < 0) {
                length++;
                continue;
            }
            
            // 가장 우울한 기간의 *3 만큼 꽃 주기
            // 가장 우울한 기간은 여러개일 수 있으므로 체크 안하고 갯수만 세기
            if(length == max) {
                for(int j=i+1-length*3; j<=i; j++) {
                    if(j < 0 || flower[j])
                        continue;
                    maxDay++;
                }
            }

            if(result < count + maxDay)
                result = count + maxDay;

            maxDay = length = 0;
        }

        System.out.println(result);
    }
}
#endif
}
