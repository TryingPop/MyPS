# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 구현
  - 정렬

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 128 MB

## 문제
A level is being designed for a new platform game. The locations of the platforms have been chosen. Contrary to popular opinion, platforms can't float in the air, but need pillars for support. More precisely, each of the two ends of the platform needs to be supported by a pillar standing on the floor or on a different platform.<br/>
You will be given the locations of the platforms in a coordinate system as in the left image below. Each platform's location is determined by its altitude (vertical distance from the ground) and the start and end coordinates in the horizontal direction. Each support pillar is placed half a unit from the end of a platform, as in the right image.<br/>
Determine the total length of pillars needed to support all the platforms.<br/>


## 입력
The first line contains the integer N, 1 ≤ N ≤ 100, the number of platforms.<br/>
Each of the following N lines contains the position of one platform, three coordinates Y, X1 and X2. The first number is the altitude, the other two the horizontal coordinates. All coordinates will be positive integers less than 10000 satisfying X2 > X1+1 (i.e. the length of each platform will be at least 2).<br/>
The input will be such that no two platforms overlap.<br/>


## 출력
Output the total length of pillars needed to support all the platforms.<br/>


## 예제 입력
3<br/>
1 5 10<br/>
3 1 5<br/>
5 3 7<br/>


## 예제 출력
14<br/>


## 풀이
밑에 가로 판이 있다면 해당 판과의 높이차만큼 기둥을 놓게 된다.<br/>
그래서 높이가 낮은거부터 판을 깔면서 진행하고 해당 범위에 판이 있다고 기록하면 정답을 구해갈 수 있다.<br/>
판의 가로 범위가 0 ~ 1만 이하이고, 입력되는 판의 갯수가 100개로 작아 일일히 기록하며 찾아갔다.<br/>
만약 판의 갯수가 10만개 이상에 범위가 10억까지 간다면, 좌표 압축과 세그먼트 트리를 이용해 풀 것이다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/1276