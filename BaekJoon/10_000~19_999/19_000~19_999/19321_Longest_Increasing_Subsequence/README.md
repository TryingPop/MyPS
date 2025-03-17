# C#

## 난이도 : 플레티넘 3

## 알고리즘 분류
  - 정렬
  - 해 구성하기

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
Given f_1, f_2, ..., f_n, find a permutation p_1, p_2, ..., p_n of integers 1, 2, ..., n such that, for each i, the length of the longest strictly increasing subsequence ending with p_i is f_i.<br/>


## 입력
The first line contains an integer n (1 ≤ n ≤ 10^5).<br/>
The second line contains n integers f_1, f_2, ..., f_n (1 ≤ f_i ≤ n). It is guaranteed that, for the given input, at least one such permutation p_1, p_2, ..., p_n exists.<br/>


## 출력
On the first line, print n integers p_1, p_2, ..., p_n. These numbers must form a permutation of integers 1, 2, ..., n. If there are several possible answers, print any one of them.<br/>


## 예제 입력
7<br/>
1 2 3 2 4 4 3<br/>

## 예제 출력
1 3 5 2 7 6 4<br/>


## 풀이
문제에서 순열이 보장된다 했으므로 불가능 판별을 할 필요는 없다.<br/>
가능하다고 보고 시작한다.<br/>

그리디로 i번째에 pi로 채운다.<br/>
그러면 위 예제는 1 2 3 2 4 4 3 이 된다.<br/> 
그러면 i로 끝나는 가장 긴 엄격하게 증가하는 부분 수열이 됨을 알 수있다.<br/>
이제 중복을 제거를 해줘야 한다.<br/>
뒤에서부터 조사하는데, 뒤에 자신과 같은 숫자가 존재한다면 10^(중복된 갯수)만큼 누적해준다.<br/>


|중복 허용 수열|1|2|3|2|4|4|3|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|중복 갯수|0|1|1|0|1|0|0|
|변형된 수열|1|2.1|3.1|2|4.1|4|3|


이제 해당 수를 크기 순으로 1씩 증가하며 값을 재배치한다.<br/>
1 2.1 3.1 2 4.1 4 3은 오름차순으로 1 2 2.1 3 3.1 4 4.1이된다.<br/>
이를 각각 1 2 3 4 5 6 7로 바꾼다.<br/>
3.1은 5로 바꾼다.<br/>


|중복 허용 수열|1|2|3|2|4|4|3|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|변형된 수열|1|2.1|3.1|2|4.1|4|3|
|최종 수열|1|3|5|2|7|6|4|


그리고 최종 수열은 문제 조건을 만족하는 수열이됨을 알 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/19321