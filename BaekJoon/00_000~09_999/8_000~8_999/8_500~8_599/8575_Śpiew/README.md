# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 수학
  - 그리디 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
Na lekcji śpiewu uczniowie Bajtazara siedzą w jednym, długim rzędzie. Nie wszystkie krzesła są w nim zapełnione i pomiędzy poszczególnymi uczniami mogą występować wolne miejsca.<br/>
Na lekcji uczniowie potrzebują śpiewników, ale nie każdy musi trzymać śpiewnik. Nauczyciel musi się zatroszczyć tylko o to, aby każdy uczeń bez śpiewnika siedział bezpośrednio obok ucznia ze śpiewnikiem. Ponieważ uczniowie na każdej lekcji siadają w inny sposób a śpiewników jest dosyć mało, Bajtazar poprosił Ciebie, swojego przyjaciela, o napisanie programu, który dla danego rozmieszczenia uczniów wyznaczy minimalną liczbę potrzebnych im śpiewników, aby ułatwić Bajtazarowi rozdawanie śpiewników.<br/>


## 입력
W pierwszym wierszu standardowego wejścia znajduje się jedna liczba naturalna n (1 ≤ n ≤ 1,000,000) oznaczająca liczbę miejsc w rzędzie. W drugim wierszu znajduje się ciąg n znaków opisujących kolejne miejsca:<br/>

  - znak "W" oznacza miejsce wolne,
  - znak "Z" oznacza miejsce zajęte przez ucznia.


## 출력
Twój program powinien wypisać na wyjście jedną liczbę całkowitą oznaczającą minimalną liczbę śpiewników, które można rozdać uczniom tak, aby każdy miał śpiewnik lub siedział obok kogoś ze śpiewnikiem.<br/>


## 예제 입력
9<br/>
ZWZZZWZZW<br/>


## 예제 출력
3<br/>


## 풀이
이미 책을 읽을 수 있는 학생을 비어있는 자리라 생각해도 된다.<br/>


그리고 인접한 사람이 없는 경우 무조건 해당 학생에게 책을 줘야 한다.<br/>
반면 인접한 학생이 있는 경우면 다음 학생에게 책을 준다.<br/>
다음 학생에게 주면 다다음 학생이 있는 경우 해당 학생도 책을 읽을 수 있기 때문이다.<br/>


이렇게 책을 나눠주는 것이 그리디로 최소임을 알 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/8575