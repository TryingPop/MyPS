# C#

## 난이도 : 플레티넘 5

## 알고리즘 분류
  - 자료 구조
  - 그리디 알고리즘
  - 우선순위 큐

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
You are planning to spend your holidays touring Europe, staying each night in a different city for N consecutive nights. You have already chosen the hotel you want to stay in for each city, so you know the price Pi of the room you’ll be staying at during the i-th night of your holidays, for i = 1, . . . , N.<br/>
You will book your accommodation through a website that has a very convenient rewards program, which works as follows. After staying for a night in a hotel you booked through this website you are awarded one point, and at any time you can exchange K of these points in your account for a free night in any hotel (which will however not give you another point).<br/>
For example, consider the case with N = 6 and K = 2 where the prices for the rooms are P1 = 10, P2 = 3, P3 = 12, P4 = 15, P5 = 12 and P6 = 18. After paying for the first four nights you would have four points in your account, which you could exchange to stay for free the remaining two nights, paying a total of P1 + P2 + P3 + P4 = 40 for your accommodation. However, if after the first three nights you use two of the three points you earned to stay the fourth night for free, then you can pay for the fifth night and use the final two points to get the sixth one for free. In this case, the total cost of your accommodation is P1 + P2 + P3 + P5 = 37, so this option is actually more convenient.<br/>
You want to make a program to find out what the minimum possible cost for your holidays’ accommodation is. You can safely assume that all hotels you want to stay always will have a room available for you, and that the order of the cities you are going to visit cannot be altered.<br/>

## 입력
The first line of input contains two integers N and K, representing the total number of nights your holidays will last, and the number of points you need in order to get a free night (1 ≤ N, K ≤ 105 ). The second line contains N integers P1, P2, . . . , PN , representing the price of the rooms you will be staying at during your holidays (1 ≤ Pi ≤ 104 for i = 1, 2, . . . , N).<br/>

## 출력
Output a line with one integer representing the minimum cost of your accommodation for all of your holidays.<br/>

## 예제 입력
6 2<br/>
10 3 12 15 12 18<br/>

## 예제 출력
37<br/>

## 풀이
우선순위 큐에 쿠폰이 생긴 직후 쿠폰의 개수만큼 가장 비싼 경우들만 저장한다.<br/>
쿠폰이 k개 될때마다 우선순위 큐의 크기를 1개씩 늘려줬다.<br/>
그리고 가장 비싼 값들만 제외해 최소값을 찾았다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/13873