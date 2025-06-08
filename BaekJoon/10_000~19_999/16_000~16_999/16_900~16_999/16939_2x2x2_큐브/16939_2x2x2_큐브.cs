using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 29
이름 : 배성훈
내용 : 2x2x2 큐브
    문제번호 : 16939번

    구현 문제다.
    1회전에 모든 면이 맞춰지는지 확인하면 된다.
    그런데 1회전에 변환되는 면은 무조건 4개고 
    이동하지 않는 면은 서로 맞은편에 있다.
    그래서 1회전에 맞춰진다면 회전하지 않는 면들은 색이 맞춰져야한다.
    그리고 맞춰지지 않은 면이 4개 인지도 확인해 불가능한 경우를 먼저 판별한다.
    이외는 직접 회전하면서 확인한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1137
    {

        static void Main1137(string[] args)
        {

            const int UP = 0;
            const int FRONT = 1;
            const int DOWN = 2;
            const int LEFT = 3;
            const int RIGHT = 4;
            const int BACK = 5;

            // 윗면 회전
            int[] ROT_UP = { 4, 5, 16, 17, 20, 21, 12, 13 };
            int[] ROT_DOWN = { 6, 7, 18, 19, 22, 23, 14, 15 };

            // 오른쪽면 회전
            int[] ROT_RIGHT = { 1, 3, 5, 7, 9, 11, 22, 20 };
            int[] ROT_LEFT = { 0, 2, 4, 6, 8, 10, 23, 21 };

            // 앞면 회전
            int[] ROT_FRONT = { 2, 3, 16, 18, 9, 8, 15, 13 };
            int[] ROT_BACK = { 0, 1, 17, 19, 11, 10, 14, 12 };

            int[] arr; 
            Solve();
            void Solve()
            {

                arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

                Console.Write(GetRet() ? 1 : 0);
            }

            bool GetRet()
            {

                int type = ChkTwoSide();

                int[] copy = new int[arr.Length];

                switch (type)
                {

                    case UP:
                        Rotate(ROT_UP);
                        if (ChkAllSide()) return true;

                        Rotate(ROT_DOWN);
                        return ChkAllSide();

                    case FRONT:
                        Rotate(ROT_FRONT);
                        if (ChkAllSide()) return true;

                        Rotate(ROT_BACK);
                        return ChkAllSide();

                    case RIGHT:
                        Rotate(ROT_RIGHT);
                        if (ChkAllSide()) return true;

                        Rotate(ROT_LEFT);
                        return ChkAllSide();

                    default:
                        return false;
                }

                // src를 dst로 복사
                void Copy(int[] _src, int[] _dst)
                {

                    for (int i = 0; i < _src.Length; i++)
                    {

                        _dst[i] = _src[i];
                    }
                }

                // arr 회전 시도 
                void Rotate(int[] _rot)
                {

                    Copy(arr, copy);

                    int temp1 = arr[_rot[0]];
                    int temp2 = arr[_rot[1]];

                    for (int i = 1; i < 4; i++)
                    {

                        arr[_rot[i * 2 - 2]] = arr[_rot[i * 2]];
                        arr[_rot[i * 2 - 1]] = arr[_rot[i * 2 + 1]];
                    }

                    arr[_rot[6]] = temp1;
                    arr[_rot[7]] = temp2;
                }

                // 모든 면의 색이 같은지 확인
                bool ChkAllSide()
                {

                    bool ret = true;
                    for (int i = 0; i < 6; i++)
                    {

                        if (ChkSide(i)) continue;
                        ret = false;
                        break;
                    }

                    Copy(copy, arr);
                    return ret;
                }

                // 맞은편 두 개의 면 확인
                int ChkTwoSide()
                {

                    int ret = -1;
                    int cnt = 0;
                    if (ChkSide(UP))
                    {

                        if (ChkSide(DOWN)) 
                        { 
                            
                            cnt++; 
                            ret = UP; 
                        }
                        else return -1;
                    }

                    if (ChkSide(FRONT))
                    {

                        if (ChkSide(BACK)) 
                        { 
                            
                            cnt++; 
                            ret = FRONT; 
                        }
                        else return -1;
                    }

                    if (ChkSide(RIGHT))
                    {

                        if (ChkSide(LEFT))
                        {

                            cnt++;
                            ret = RIGHT;
                        }
                        else return -1;
                    }

                    return cnt == 1 ? ret : -1;
                }

                // 1개의 면의 색상이 일치하는지 확인
                bool ChkSide(int _side)
                {

                    int s = _side * 4;
                    int cur = arr[s];
                    for (int i = 1; i < 4; i++)
                    {

                        if (cur != arr[s + i]) return false;
                    }

                    return true;
                }
            }
        }
    }

#if other
e = 48, a[24], b[24], c[3][8] = {
	{ 14,15,6,7,18,19,22,23 },
	{ 1,3,5,7,9,11,22,20 },
	{ 2,3,16,18,9,8,15,13 }
};
main(t) {
	for (int i = 0; i < 24; i++)
		scanf("%d", &a[i]);
	for (int i = 2; i < 24; i += 4) {
		memcpy(b, a, 96);
		for (int j = 0; j < 8; j++)
			b[c[i / 8][j]] = a[c[i / 8][(j + i) % 8]];
		for (int j = t = 0; j < 24; j++)
			t += b[j] != b[j / 4 * 4];
		e += !t;
	}
	putchar(e);
}
#elif other2
// #include<stdio.h>
int a[8]={5, 6, 17, 18, 21, 22, 13, 14};
int b[8]={1, 3, 5, 7, 9, 11, 24, 22};
int c[8]={3, 4, 17, 19, 10, 9, 16, 14};
int cube[25];
int rot(int *arr, int t){
	int tmp[25];
	for(int i=1;i<25;++i)	tmp[i]=cube[i];
	for(int i=0;i<8;++i){
		int j=i+t;
		if(j<0)	j+=8;
		else if(j>7)	j-=8;
		tmp[arr[j]]=cube[arr[i]];
	}
	for(int i=1;i<25;i+=4)for(int j=1;j<4;++j){
		if(tmp[i]!=tmp[i+j])	return 0;
	}
	return 1;
}
int sol(){
	if(rot(a, 2))	return 1;
	if(rot(a, -2))	return 1;
	if(rot(b, 2))	return 1;
	if(rot(b, -2))	return 1;
	if(rot(c, 2))	return 1;
	if(rot(c, -2))	return 1;
	return 0;
}
int main(){
	for(int i=1;i<25;++i)	scanf("%d", &cube[i]);
	printf("%d\n", sol());
	return 0;
}
#endif
}
