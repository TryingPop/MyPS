# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 수학
  - 정렬
  - 조합론

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
Bajtazar jest niezwykle dumny ze swojej kolekcji rzadkich monet. Zbierał je przez wiele lat, dbając o to, by żadne dwie nie były do siebie podobne. Obecnie ma n monet ponumerowanych tak, że i-ta moneta ma rozmiar dokładnie i.<br/>
Jako że kolekcja Bajtazara ostatnio się powiększyła, był on zmuszony kupić nowy klaser. Jest w nim dokładnie n przegród na monety, każda o określonym rozmiarze. Oczywiście żadnej monety nie można włożyć do zbyt małej przegrody. Nic nie stoi jednak na przeszkodzie, by włożyć ją do przegrody większej.<br/>
Bajtazar zastanawia się teraz do których przegród włożyć poszczególne monety. Po sprawdzeniu wielu kombinacji zaintrygowało go również pytanie, na ile sposobów może zapełnić klaser. Ponieważ liczba ta może być bardzo duża, Bajtazarowi wystarczy jej reszta z dzielenia przez 10^9+7. Napisz program, który zaspokoi jego ciekawość.<br/>


## 입력
Pierwszy wiersz standardowego wejścia zawiera jedną liczbę całkowitą: n (1 ≤ n ≤ 1,000,000). W następnym wierszu znajduje się n liczb całkowitych a_i (1 ≤ a_i ≤ n) pooddzielanych pojedynczymi odstępami. Liczba a_i mówi, jaką największą monetę można włożyć do i-tej przegrody.<br/>


## 출력
Twój program powinien wypisać na standardowe wyjście jedną liczbę całkowitą - resztę z dzielenia liczby sposobów zapełnienia klasera przez 10^9+7.<br/>


## 예제 입력
4<br/>
4 2 4 2<br/>


## 예제 출력
4<br/>


## 풀이
숫자를 정렬한 뒤 작은 것부터 넣을 수 있는 칸을 곱해가면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/8587