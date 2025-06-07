# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 구현
  - 자료 구조
  - 해시를 사용한 집합과 맵

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
Bajtek znalazł ciekawą zabawkę. Zabawka ta ma n+1 przycisków. Nad każdym z n pierwszych przycisków znajduje się mały licznik, początkowo wskazujący zero. Naciśnięcie przycisku pod licznikiem zwiększa wskazywaną przezeń liczbę o 1.<br/>
Zabawka szybko by się Bajtkowi znudziła, gdyby nie kuriozalne działanie przycisku o numerze n+1. Po jego użyciu wszystkie n liczników zaczyna wskazywać największą z widocznych dotąd na zabawce wartości. Na przykład, jeżeli n = 5 i kolejne liczniki wskazują liczby 0, 0, 1, 2, 0, to po naciśnięciu przycisku o numerze 6 wszystkie liczniki będą wskazywać 2.<br/>
Wiedząc, które przyciski wybierał kolejno Bajtek, chcemy poznać wartości wszystkich liczników po zakończeniu zabawy.<br/>

## 입력
Pierwszy wiersz standardowego wejścia zawiera dwie liczby całkowite n, m (1 ≤ n, m ≤ 10^6), oznaczające kolejno liczbę liczników na zabawce i liczbę operacji wykonanych przez Bajtka. Drugi wiersz wejścia zawiera m liczb całkowitych p_1, p_2, ... , p_m (1 ≤ p_i ≤ n+1), oznaczających numery kolejnych przycisków wciskanych przez Bajtka.<br/>

## 출력
Pierwszy i jedyny wiersz standardowego wyjścia powinien zawierać n liczb całkowitych, oddzielonych pojedynczymi odstępami, oznaczających wartości znajdujące się na kolejnych licznikach po zakończeniu zabawy.<br/>

## 예제 입력
5 7<br/>
3 4 4 6 1 4 4<br/>

## 예제 출력
3 2 2 4 2<br/>

## 풀이
특수 키를 누르면 모두 현재 기기의 최대값으로 일괄 변환된다.<br/>
그래서 매번 버튼을 누를 때마다 최대값을 확인했다.<br/>
특수키를 누르면 모든 값이 갱신되는데 범위가 100만까지 있고 버튼 누르는 횟수도 100만이다.<br/>
단순히 매번 갱신하면 10^12로 시간초과날 수 있다.<br/>
그래서 갱신된 최대값도 관리했다.<br/>
버튼을 누를 때 갱신될 최대값보다 작으면 갱신될 최대값 + 1을 주고 크다면 기본 값에 + 1해주면 된다.<br/>
그리고 최대값은 버튼 누를 때 누르고 난 뒤의 값이 최대값보다 큰지 확인하며 갱신했다.<br/>
이후 마지막 출력에서는 해당 버튼을 갱신 후 누른적이 없어 갱신될 최대값보다 작으면 갱신될 최대값으로 채웠다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/8576