using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 멀티탭 스케줄링
    문제번호 : 1700번

    그리디 문제다
    멀티탭을 채우는데 비어있는 자리가 있으면 바로 채운다
    만약 다 찼을 경우 현재 뽑아야할 것 중에 가장 나중에 재사용 하는 것을 뽑았다
    만약 다음에 재사용 안하면 걔를 그냥 뽑고 그 자리에 현재걸 채운다

    다음 인덱스를 확인하기 위해서 사용한 자료구조는 큐 배열이다
*/

namespace BaekJoon.etc
{
    internal class etc_0247
    {

        static void Main247(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int multiLen = ReadInt(sr);
            int len = ReadInt(sr);

            Queue<int>[] idxs = new Queue<int>[len + 1];

            for (int i = 1; i <= len; i++)
            {

                idxs[i] = new(100);
            }

            int[] order = new int[len];
            for (int i = 0; i < len; i++)
            {

                int idx = ReadInt(sr);

                idxs[idx].Enqueue(i);
                order[i] = idx;
            }

            sr.Close();

            int[] multi = new int[multiLen];
            bool[] use = new bool[len + 1];
            int ret = 0;
            for (int i = 0; i < len; i++)
            {

                // 다음 인덱스를 맞춰줘야하기에 현재 인덱스는 제거
                // 해당 구문 없으면 인덱스 확인 전에 최신화 자꾸 해줘야한다
                idxs[order[i]].Dequeue();

                // 현재 멀티탭에서 사용 중이면
                // 두 번 꽂을 필요 없으니 넘긴다
                if (use[order[i]]) continue;

                // 빈자리 체크
                bool connect = false;
                for (int j = 0; j < multiLen; j++)
                {

                    if (multi[j] == 0) 
                    {

                        // 빈 자리 발견!
                        connect = true;
                        multi[j] = order[i];
                        use[order[i]] = true;
                        break;
                    }
                }

                if (connect) continue;

                // 멀티탭을 모두 사용 중이므로
                // 적어도 하나를 뽑아야하기에 먼저 뽑는다고 기록
                ret++;

                // 멀티탭 1번 항목부터 다음 재사용 인덱스 조사
                int maxNext = idxs[multi[0]].Count > 0 ? idxs[multi[0]].Peek() : -1;
                if (maxNext == -1)
                {

                    // 1번에 꽂혀있는 기기를 뒤에 사용하는 경우 없으면 
                    // 그냥 뽑고 현재 사용할 기기를 채운다
                    use[multi[0]] = false;
                    use[order[i]] = true;
                    multi[0] = order[i];
                    continue;
                }

                int maxIdx = 0;

                // 1번은 다음 사용하는 때를 확인했으니 이제 2번부터 조사
                for(int j = 1; j < multiLen; j++)
                {

                    if (idxs[multi[j]].Count > 0)
                    {

                        int next = idxs[multi[j]].Peek();
                        if (maxNext < next)
                        {

                            // 더 나중에 사용하는 경우
                            // 해당 인덱스 기록
                            maxNext = next;
                            maxIdx = j;
                        }
                    }
                    else
                    {

                        // 나중에 사용 안하는 전자 기기 발견
                        // 뽑고 현재 사용해야할 전자기기로 바꾼다
                        use[multi[j]] = false;
                        use[order[i]] = true;
                        multi[j] = order[i];
                        connect = true;
                        break;
                    }
                }

                if (connect) continue;

                // 나중에 모두 적어도 한번씩 쓴다
                // 이 경우 가장 나중에 쓰는걸 뽑는다
                use[multi[maxIdx]] = false;
                use[order[i]] = true;
                multi[maxIdx] = order[i];
            }

            // 뽑는 횟수 출력
            Console.WriteLine(ret);
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
}
