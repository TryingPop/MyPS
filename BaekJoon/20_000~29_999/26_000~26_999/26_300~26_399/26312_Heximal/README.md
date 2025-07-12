# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 수학
  - 이분 탐색
  - 분할 정복을 이용한 거듭 제곱
  - 임의 정밀도 / 큰 수 연산

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 1024 MB

## 문제
The base-6 numeral system is also called the heximal numeral system. We say a string h_kh_{k-1} ... h_1h_0 is a heximal number if h_i ∈ {0, 1, 2, 3, 4, 5} for every i ∈ {0, 1, ... , k} and h_k = 0 implies k = 0. The value represented by h_kh_{k-1} … h_1h_0 in the heximal numeral system is ∑^{k}_{i=0}{h_i6^i}. For example, the value of the heximal number 12345 equals the value of the decimal number 1865 = 1 × 6^4 + 2 × 6^3 + 3 × 6^2 + 4 × 6 + 5.<br/>
Harry asks you to convert a very large base-10 number N to base-6. Since the conversion result can be very long, it is too hard for Harry to verify the result by himself. So, you just need to tell Harry the length of the conversion result. For example, if N = 1865, then you just need to tell Harry the length of the conversion result is 5.<br/>


## 입력
The input contains exactly one integer N in decimal.<br/>


## 출력
Output the length of the base-6 representation of N.<br/>


## 제한
  - 0 ≤ N < 10^500000.


## 예제 입력
1865<br>


## 예제 출력
5<br>


## 풀이
양수인 n을 6진법으로 나타냈을때의 길이는 n ≥ 6^k를 만족하는 가장 큰 k와 같다.<br/>
그래서 이분탐색으로 n ≥ 6^k를 만족하는 가장 큰 k를 찾으면 된다.<br/>
그리고 거듭제곱은 분할 정복을 이용한 거듭제곱을 활용하면 log N에 찾을 수 있다.<br/>
C#의 BigInteger로 큰 수를 나타내고 곱셈연산을 하니 시간 초과로 통과하지 못한다.<br/>
그래서 python 언어로 제출해 풀었다. C#의 코드를 python으로 바꾼거 뿐이다.<br/> 


## 문제 링크
https://www.acmicpc.net/problem/26312