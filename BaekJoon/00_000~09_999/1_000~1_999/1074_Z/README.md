# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 분할 정복
  - 재귀

## 제한조건
  - 시간 제한 : 0.5초
  - 메모리 제한 : 512 MB

## 문제
한수는 크기가 2^N × 2^N인 2차원 배열을 Z모양으로 탐색하려고 한다. 예를 들어, 2×2배열을 왼쪽 위칸, 오른쪽 위칸, 왼쪽 아래칸, 오른쪽 아래칸 순서대로 방문하면 Z모양이다.<br/>



	0 1
	2 3


N > 1인 경우, 배열을 크기가 2^N-1 × 2^N-1로 4등분 한 후에 재귀적으로 순서대로 방문한다.<br/>
다음 예는 2^2 × 2^2 크기의 배열을 방문한 순서이다.<br/>


	0 1 4 5
	2 3 6 7
	8 9 12 13
	10 11 14 15


N이 주어졌을 때, r행 c열을 몇 번째로 방문하는지 출력하는 프로그램을 작성하시오.<br/>

## 입력
첫째 줄에 정수 N, r, c가 주어진다.<br/>

## 출력
r행 c열을 몇 번째로 방문했는지 출력한다.<br/>

## 제한
  - 1 ≤ N ≤ 15
  - 0 ≤ r, c < 2^N

## 예제 입력
10 512 512<br/>

## 예제 출력
786432<br/>

## 풀이
4제곱 간격으로 칸을 묶어서 보면 1개와 같이 Z 이동임을 알 수 있다.<br/>
그래서 분할 정복으로 접근했다.<br/>
그러면 나눠진 4구간 중 왼쪽 위와 오른쪽 위의 차는 구간에 있는 칸의 개수와 같음을 알 수 있다.<br/>
이렇게 재귀와 각 구간별 차에 관계식을 이용해 풀었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/1074