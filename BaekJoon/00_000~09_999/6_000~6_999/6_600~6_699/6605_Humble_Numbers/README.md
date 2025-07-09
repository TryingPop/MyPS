# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 자료 구조
  - 브루트포스 알고리즘
  - 우선순위 큐

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
A number whose only prime factors are 2,3,5 or 7 is called a humble number. The sequence 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 15, 16, 18, 20, 21, 24, 25, 27, ... shows the first 20 humble numbers.<br/>
Write a program to find and print the nth element in this sequence.<br/>


## 입력
The input consists of one or more test cases. Each test case consists of one integer n (1 ≤ n ≤ 5,842) with 1. Input is terminated by a value of zero (0) for n.<br/>


## 출력
For each test case, print one line saying "The nth humble number is number.". Depending on the value of n, the correct suffix "st", "nd", "rd", or "th" for the ordinal number nth has to be used like it is shown in the sample output.<br/>


## 예제 입력
1<br/>
2<br/>
3<br/>
4<br/>
11<br/>
12<br/>
13<br/>
21<br/>
22<br/>
23<br/>
100<br/>
1000<br/>
5842<br/>
0<br/>


## 예제 출력
The 1st humble number is 1.<br/>
The 2nd humble number is 2.<br/>
The 3rd humble number is 3.<br/>
The 4th humble number is 4.<br/>
The 11th humble number is 12.<br/>
The 12th humble number is 14.<br/>
The 13th humble number is 15.<br/>
The 21st humble number is 28.<br/>
The 22nd humble number is 30.<br/>
The 23rd humble number is 32.<br/>
The 100th humble number is 450.<br/>
The 1000th humble number is 385875.<br/>
The 5842nd humble number is 2000000000.<br/>


## 풀이
문제에서 n번째로 작은 겸손한 수를 찾아야 한다.<br/>


1 또는 소인수분해 했을 때 2, 3, 5, 7로 이루어진 수들을 겸손한 수라 한다.<br/>
겸손한 수란, 소인수가 오직 2, 3, 5, 7로만 이루어진 수를 말한다. 즉, 어떤 수 m 이 겸손한 수라면, m=2^a x 3^b x 5^c x 7^d 와 같이 표현되며, a,b,c,d≥0 인 정수이다.<br/>


p가 겸손한 수라면 p x 2, p x 3, p x 5, p x 7도 겸손한 수이다.<br/>
나눗셈 알고리즘으로 정수에서 인수분해는 유일하고, m이 겸손한 수라면, m = 2^a x 3^b x 5^c x 7^d, 0 ≤ a, b, c, d이므로<br/>
우선, 1을 겸손한 수로 보고 시작하여, 현재 겸손한 수 p에 대해 p×2,p×3,p×5,p×7을 계산하여 새로운 겸손한 수 후보로 우선순위 큐에 추가한다.<br/>
이 과정을 반복하면, 모든 겸손한 수를 오름차순으로 생성할 수 있다.<br/>
하지만 중복 생성을 방지하기 위해, 이미 큐에 추가된 수는 HashSet을 이용해 관리했다.<br/>


2에서 x 3을 해서 6이 나오고, 3에서 x 2를 해서 6이 나오므로 중복 탐색만 주의하면 된다.<br/>


작은 수를 먼저 연산해야하므로 우선순위 큐를 이용해 다음 겸손한 수들을 관리했다.<br/>
우리가 찾아야할 겸손한 수는 5842개 이므로 먼저 모두 찾아주고 배열에 저장했다.<br/>


이제 쿼리가 주어지면 해당 배열의 값을 읽어 겸손한 수를 제출했다.<br/>
예제에서 보듯이 5842번째 겸손한 수는 2_000_000_000이다.<br/>
5841번째 수에서 5842번째를 찾을 때, x 2, x 3, x 5, x 7연산을 하기에 오버플로가 나지 않게 저장하는 배열과 우선순위 큐를 long 자료형으로 찾았다.<br/>


그리고 n번째를 영어로 출력하는데, st, nd, rd, th를 출력해야 한다.<br/>
사용하는 규칙은 다음과 같다.<br/>


  - st는 1의 자리가 1인데, 10의 자리가 1이여서는 안된다.
  - nd 역시 1의 자리가 2이고, 10의 자리가 1이여서는 안된다.
  - rd 역시 1의 자리가 3이고, 10의 자리가 1이여서는 안된다.
  - 이외 경우는 th로 출력하면 된다.


해당 경우 초기에 겸손한 수를 찾는데 매번 4방향으로 조사한다.<br/>
해시인 HashSet 자료구조에 우선순위 큐에 들어간 원소를 기록해 다시 우선순위 큐에 못넣게 막는다면 매번 O(1)에 해결이 된다.<br/>
중복을 제외한 겸손한 수 탐색은 n = 5842개이고, 각 수마다 방향은 4이므로 4n개의 수를 조사한다.<br/>
그리고 우선순위 큐로 4 x n개의 수를 관리하기에 시간 복잡	도는 O(4n x log (4n)) = O(n log n)이 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/6605