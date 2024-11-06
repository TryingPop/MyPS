using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 번데기
    문제번호 : 15721번

    구현, 브루트포스, 시뮬레이션 문제다
    시뮬레이션 돌려서 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0660
    {

        static void Main660(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            int type = int.Parse(Console.ReadLine());

            int ret = 0;
            int cur = 2;
            while (true)
            {

                // 번 - 데기
                m--;
                if (m == 0)
                {

                    if (type == 1) ret++;                    
                    break;
                }

                ret += 2;

                // 번 - 데기
                m--;
                if (m == 0)
                {

                    if (type == 1) ret++;
                    break;
                }

                ret += 2;


                // 번 * n - 1 - 데기 * n - 1
                if (m <= cur)
                {

                    if (type == 0) ret += m - 1;
                    else ret += (cur + m - 1);
                    break;
                }

                m -= cur;
                ret += 2 * cur;
                cur++;
            }

            ret %= n;

            Console.WriteLine(ret);
        }
    }

#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main {
    static int cnt = 0,rounds=0;
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int a = Integer.parseInt(br.readLine());
        int t = Integer.parseInt(br.readLine());
        int type = Integer.parseInt(br.readLine());
        game(t,type,1);
        //System.out.println(rounds+" "+cnt);
        System.out.println((rounds-1)%a);
    }
    public static void game(int n,int type,int round){
        //System.out.println(rounds);
        for(int i=0;i<1;i++){
            rounds+=1;
            if(type==0){
                cnt+=1;
                if(cnt==n){
                    return;
                }
            }

        }
        for(int i=0;i<1;i++){
            rounds+=1;
            if(type==1){
                cnt+=1;
                if(cnt==n){
                    return;
                }
            }
        }
        for(int i=0;i<1;i++){
            rounds+=1;
            if(type==0){
                cnt+=1;
                if(cnt==n){
                    return;
                }
            }

        }
        for(int i=0;i<1;i++){
            rounds+=1;
            if(type==1){
                cnt+=1;
                if(cnt==n){
                    return;
                }
            }
        }
        for(int i=0;i<=round;i++){
            rounds+=1;
            if(type==0){
                cnt+=1;
                if(cnt==n){
                    return;
                }
            }
        }
        for(int i=0;i<=round;i++){
            rounds+=1;
            if(type==1){
                cnt+=1;
                if(cnt==n){
                    return;
                }
            }
        }
        game(n,type,round+1);
    }
}
#endif
}
