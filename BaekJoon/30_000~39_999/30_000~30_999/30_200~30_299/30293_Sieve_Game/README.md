# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 수학
  - 그리디 알고리즘
  - 정수론
  - 소수 판정
  - 에라토스테네스의 체

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Alice, after mastering the sieve of Eratosthenes, excitedly created a puzzle game that made use of it.<br/>
The rules of the puzzle game are as follows:<br/>

  - An array p_1, p_2, ..., p_N is given where all p_i is initially 0.
  - A target array t_1, t_2, ..., t_N is given. Her goal is to make p_i=t_i for all 1 ≤ i ≤ N.
  - Each time, she can perform one of the following two operations:
    - Choose i and increase p_j by 1 for every 1 ≤ j ≤ N that is a multiple of i.
    - Choose i and decrease p_j by 1 for every 1 ≤ j ≤ N that is a multiple of i.
  - She can repeat this process as much as she wants.

Alice aims to solve the puzzle using the fewest operations, showcasing her puzzle-solving skill. Please help Alice find the minimum number of operations to solve the puzzle.<br/>


## 입력
The first line contains one integer, N.<br/>
The second line contains space-separated N integers — elements of the array t.<br/>


## 출력
Print out the minimum number of operations to solve the puzzle. If the puzzle is unsolvable, print -1.<br/>


## 제한
  - 1 ≤ N ≤ 200,000
  - -10^9 ≤ t_i ≤ 10^9 (1 ≤ i ≤ N)
  - All values in the input are integers.


## 예제 입력
4<br/>
1 1 1 0<br/>


## 예제 출력
2<br/>


## 풀이
i < j인 경우 j가 i에 영향을 주지 못한다.<br/>
반면 j % i == 0인 경우 i는 j에 영향을 줄 수 있다.<br/>
작은 인덱스에서부터 최소로 0을 맞춰가는 것이 그리디로 최소임이 보장된다.<br/>
에라토스테네스의 체 시간 복잡도가 N log N인데, log N = 1 + 1 / 2 + 1 / 3 + ... + 1 / N에서 나온다.<br/>
그래서 작은 인덱스부터 에라토스테네스의 체처럼 배수로 값을 변경해 가도 시간내에 풀릴거라 판단했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/30293