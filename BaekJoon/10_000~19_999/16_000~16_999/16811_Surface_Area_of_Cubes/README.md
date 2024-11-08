# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 자료 구조
  - 기하학
  - 해시를 사용한 집합과 맵
  - 트리를 사용한 집합과 맵
  - 3차원 기하학

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
Taro likes a signle player game "Surface Area of Cubes".<br/>
In this game, he initially has an A x B x C rectangular solid formed by A \times B \times C$ unit cubes (which are all 1 by 1 by 1 cubes). The center of each unit cube is placed at 3-dimensional coordinates (x,y,z) where x,y,z are all integers (0 ≤ x ≤ A-1, 0 ≤ y ≤ B-1, 0 ≤ z ≤ C-1). Then, N distinct unit cubes are removed from the rectangular solid by the game master.<br/>
After the N cubes are removed, he must precisely tell the total surface area of this object in order to win the game.<br/>
Note that the removing operation does not change the positions of the cubes which are not removed. Also, not only the cubes on the surface of the rectangular solid but also the cubes at the inside can be removed. Moreover, the object can be divided into multiple parts by the removal of the cubes. Notice that a player of this game also has to count up the surface areas which are not accessible from the outside.<br/>
Taro knows how many and which cubes were removed but this game is very difficult for him, so he wants to win this game by cheating! You are Taro's friend, and your job is to make a program to calculate the total surface area of the object on behalf of Taro when you are given the size of the rectangular solid and the coordinates of the removed cubes.<br/>

## 입력
The input is formatted as follows.<br/>
The first line of a test case contains four integers A,B,C, and $N (1 ≤ A,B,C ≤ 10^8, 0 ≤ N ≤ min {1,000, A x B x C - 1}).<br/>
Each of the next N lines contains non-negative integers x,y, and z which represent the coordinate of a removed cube. You may assume that these coordinates are distinct.<br/>

## 출력
Output the total surface area of the object from which the N cubes were removed.<br/>

## 예제 입력
2 2 2 1<br/>
0 0 0<br/>

## 예제 출력
24<br/>

## 풀이
자르는 도형의 면으로 접근했다.<br/>
해당 면이 가장 바깥이면 포함된것으로 간주하고 아니면 포함되었는지 확인했다.<br/>
포함되었다면 빼주고 안되면 더해준다.<br/>
그리고 면들을 카운팅해서 제출하니 이상없이 통과한다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/16811