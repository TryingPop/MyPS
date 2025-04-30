# C#

## 난이도 : 플레티넘 5

## 알고리즘 분류
  - 수학
  - 정수론
  - 많은 조건 분기

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 2048 MB

## 문제
ICPC Square is a hotel provided by the ICPC Committee for the accommodation of the participants. It consists of N floors (numbered from 1 to N). This hotel has a very unique elevator. If a person is currently at floor x, by riding the elevator once, they can go to floor y if and only if y is a multiple of x and y - x ≤ D.<br/>
You are currently at floor S. You want to go to the highest possible floor by riding the elevator zero or more times. Determine the highest floor you can reach.<br/>


## 입력
A single line consisting of three integers N D S (2 ≤ N ≤ 10^12; 1 ≤ D ≤ N - 1; 1 ≤ S ≤ N).<br/>


## 출력
Output a single integer representing the highest floor you can reach by riding the elevator zero or more times.<br/>


## 예제 입력
64 35 3<br/>


## 예제 출력
60<br/>


## 힌트
First, ride the elevator from floor 3 to floor 15. This is possible because 15 is a multiple of 3 and 15 - 3 ≤ 35. Then, ride the elevator from floor 15 to floor 30. This is possible because 30 is a multiple of 15 and 30 - 15 ≤ 35. Finally, ride the elevator from floor 30 to floor 60. This is possible because 60 is a multiple of 30 and 60 - 30 ≤ 35.<br/>


## 풀이
먼저 이동할 수 있는 층은 s의 배수 층이다.<br/>
그래서 k x s층의 형태로 표현할 수 있고 여기서 k의 상한을 찾아야 한다.<br/>


그래서 n과 d를 s로 나눈다.<br/>
나눈 몫을 각각 n'(= n / s), d'(= d / s)라 한다.<br/>


이제 k의 상한은 n'와 d' * 2중 낮은 것이 된다.<br/>
n'와 d' * 2 중 낮은 것을 sup이라 하자.<br/>


이제 sup이 갈 수 있는지 확인하면 된다.<br/>
만약 sup이 짝수라면 sup ≤ d' * 2이므로 sup / 2 ≤ d'이다.<br/>
이는 sup / 2 - 1 < sup / 2 ≤ d'이므로 1에서 sup / 2로 이동하고 sup / 2에서 sup으로 이동하면 된다.<br/>
즉 상한 층에 갈 수 있다.<br/>


이제 짝수가 아닌 경우 sup이 홀수인 경우를 보자.<br/>
sup / t 가 정수인 가장 작은 t를 찾는다.<br/>
그러면 t < t'일 때 sup / t' < sup / t이다.<br/>
그래서 sup - sup / t < sup - sup / t'이다.<br/>
이는 sup / t에서 이동 불가능하다면 sup / t'에서도 이동불가능함을 알 수 있다.<br/>
그래서 1이 아닌 t를 찾고 도달할 수 있는지 확인한다.<br/>
만약 도달할 수 있다면 sup 층에 도달가능하고, 아니라면 짝수와 같은 로직으로 불가능하다로 결론 내리면 된다.<br/>


sup - 1은 짝수이고 sup이 짝수와 같은 논리로 무조건 이동할 수 있다.<br/>
그래서 sup이 도달 불가능할 때는 sup - 1로 가면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/33120