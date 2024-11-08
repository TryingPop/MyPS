# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
세 마을의 좌표가 (x1, y1, z1), (x2, y2, z2), (x3, y3, z3)이라고 가정해보자. 이때, 세 마을을 친밀도는 아래와 같이 구할 수 있다.<br/>
친밀도 = d12 + d23 (dij = |xi - xj| + |yi - yj| + |zi - zj|)<br/>
마을이 주어졌을 때, 가장 작은 세 마을의 친밀도를 구하는 프로그램을 작성하시오.<br/>

## 입력
첫째 줄에 마을의 수 N (3 ≤ N ≤ 10,000)이 주어진다. 다음 N개 줄에는 마을의 위치 (x, y, z)가 주어진다. (-1000 ≤ x,y,z ≤ 1000)<br/>

## 출력
세 마을의 친밀도 중 가장 작은 값을 출력한다.<br/>

## 예제 입력
9<br/>
0 0 1<br/>
0 0 2<br/>
0 0 3<br/>
0 0 4<br/>
0 0 6<br/>
0 0 8<br/>
0 0 7<br/>
0 0 9<br/>
0 0 10<br/>

## 예제 출력
2<br/>

## 풀이
단순히 i, j의 거리인 dij를 모두 구해 dis[i][j]에 저장하려고하면 N = 10_000이므로 1억 크기의 배열이 필요하다.<br/>
메모리 제한이 128mb라 메모리 초과가 자명하다.<br/>
관점을 달리 d12 + d23을 보면 dij + djk 형식이다.<br/>
이는 가장 짧은 서로 다른 2개의 크기만 필요함을 알 수 있다.<br/>
각 i를 고정시키고 가장 짧은 가장 짧은 길이를 2개 찾고 합치면 해당 i를 중간으로 하는 가장 짧은 길이가 된다.<br/>
이 중 가장 작은 값은 정답이 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/9763