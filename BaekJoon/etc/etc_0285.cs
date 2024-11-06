using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 소트
    문제번호 : 1071번

    그리디 알고리즘 문제다
    풀이 아이디어는 다음과 같다

    총 5가지 경우로 나뉜다
    먼저 서로 다른 남은 것의 개수를 알아야한다
    1개인 경우, 2개인 경우, 3개인 경우 나눌 수 있다
    2개 이상인 경우에 바로 인접한 큰 수인가? 확인해야한다

    1개인 경우는 간단하다
    해당 수를 쭉 출력하면 된다

    3개인 경우 가장 작은걸 쭉 출력하고,
    인접한 경우면 다다음 큰 수를 1번 출력해야한다
    인접한 경우가 아니면 다다음 큰수를 출력할 필요가 없다

    2개인 경우
    인접한 경우면 큰 것부터 쭉 출력해야한다
    인접한 경우가 아니면 작은 것을 쭉 출력한다

    이를 코드로 옮겨 제출하니 이상없이 통과했다

    나중에 생각해보니 코드 로직을 잘못짠거 같다
    while diff문 안에 for문을 넣는게 아닌, for문 안에 while diff문을 넣는게 좋아보인다
    다른 사람 풀이를 보니 더 짧게 표현된다
*/

namespace BaekJoon.etc
{
    internal class etc_0285
    {

        static void Main285(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] input = new int[n];
            int[] cnt = new int[1_001];
            for (int i = 0; i < n; i++)
            {

                input[i] = ReadInt(sr);
                cnt[input[i]]++;
            }

            sr.Close();

            Array.Sort(input);

            int diff = 0;
            {

                int before = -1;
                for (int i = 0; i < n; i++)
                {

                    if (before == input[i]) continue;
                    before = input[i];
                    diff++;
                }
            }

            int[] ret = new int[n];

            int idx = 0;
            int ptr1 = 0;
            while(diff > 0)
            {

                if (diff >= 3)
                {

                    for (int i = 0; i < 1_001; i++)
                    {

                        if (cnt[i] == 0) continue;

                        while (cnt[i] > 0)
                        {

                            ret[idx++] = i;
                            cnt[i]--;
                        }

                        if (cnt[i + 1] != 0)
                        {

                            for (int j = i + 2; j < 1_001; j++)
                            {

                                if (cnt[j] == 0) continue;

                                ret[idx++] = j;
                                cnt[j]--;
                                if (cnt[j] == 0) diff--;
                                break;
                            }
                        }
                        break;
                    }
                }
                else if (diff == 2)
                {

                    for (int i = 0; i < 1_001; i++)
                    {

                        if (cnt[i] == 0) continue;

                        if (cnt[i + 1] == 0)
                        {

                            while (cnt[i] > 0)
                            {

                                ret[idx++] = i;
                                cnt[i]--;
                            }
                        }
                        else
                        {

                            while (cnt[i + 1] > 0)
                            {

                                ret[idx++] = i + 1;
                                cnt[i + 1]--;
                            }
                        }

                        break;
                    }
                }
                else
                {

                    for (int i = 0; i < 1_001; i++)
                    {

                        if (cnt[i] == 0) continue;

                        while (cnt[i] > 0)
                        {

                            ret[idx++] = i;
                            cnt[i]--;
                        }
                        break;
                    }
                }

                diff--;
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < n; i++)
                {

                    sw.Write(ret[i]);
                    sw.Write(' ');
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
    int[] arr = new int[N+1];
    st = new StringTokenizer(br.readLine());
    for(int i=1;i<=N;i++) arr[i] = Integer.parseInt(st.nextToken());
    Arrays.sort(arr);
    for(int i=1,j,k;i<N;i++){
        if(arr[i]+1!=arr[i+1]) continue;
        k = i+1;
        while(k!=N+1&&arr[i+1]==arr[k]) k++;
        if(k==N+1) {
            j = i;
            while (arr[i] == arr[j]) j--;
            arr[j + 1]++;
            arr[i + 1]--;
        }else{
            arr[i+1] = arr[k];
            arr[k] = arr[i]+1;
        }
    }
#endif
}
