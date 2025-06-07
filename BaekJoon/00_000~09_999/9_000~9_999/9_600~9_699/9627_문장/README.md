# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 구현
  - 문자열
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
옛날 옛적에 수학을 공부하는 사람들만 사는 나라가 있었다. 이 나라에 살고있는 상근이와 창영이는 자명한 문장에 대해서 토론을 하고 있었다.<br/>
자명한 문장에는 숫자를 하나만 포함하고 있으며, 그 숫자는 문장을 이루는 글자의 개수와 같다. 예를 들면, "This sentence has thirtyone letters.”, “Blah blah seventeen”과 같다.<br/>
상근이는 창영이에게 자명한 문장에서 숫자만 지운 문장을 주었다. 창영이는 가장 작은 수를 문장에 넣어 올바른 자명한 문장을 만드는 프로그램을 작성하려고 한다.<br/>
문장은 word1 word2 word3 ... $ word_n-1 word_n과 같은 형식으로 이루어져 있고, $는 숫자를 넣을 곳을 나타낸다.<br/>
예를 들어, 상근이는 “this sentence has thirtyone letters”를 “this sentence has $ letters”로 바꾸어 창영이에게 전달해준다.<br/>
숫자는 다음과 같은 규칙을 지키면서 써야한다.<br/>

  - 1부터 10까지 숫자는 “one”, “two”, “three”, “four”, “five”, “six”, “seven”, “eight”, “nine”, “ten”로 쓴다.
  - 11부터 19까지 숫자는 “eleven”, “twelve”, “thirteen”, “fourteen”, “fifteen”, “sixteen”, “seventeen”, “eighteen”, “nineteen”로 쓴다.
  - 나머지 두 자리 숫자는 십의 자리를 쓰고 일의 자리를 쓴다. 만약 일의 자리가 0인 경우에는 십의 자리만 쓴다.
  - 십의 자리의 경우에 2부터 9까지는 “twenty”, “thirty”, “forty”, “fifty”, “sixty”, “seventy”, “eighty”, “ninety”로 쓴다.
  - 세 자리 숫자는 백의 자리를 쓰고, 나머지 두 자리를 쓴다. 두 자리가 0인 경우에는 백의 자리만 쓰면 된다.
  - 백의 자리의 경우에 1부터 9까지는 “onehundred”, “twohundred”, “threehundred”, “fourhundred”, “fivehundred”, “sixhundred”, “sevenhundred”, “eighthundred”, “ninehundred”로 쓴다.
  - 항상 세자리 이내로 문제를 풀 수 있다.

아래와 같이 숫자를 쓸 수 있다.<br/>

  - 68 = “sixty” + “eight” = “sixtyeight” 
  - 319 = “threehundred” + “nineteen” = “threehundrednineteen” 
  - 530 = “fivehundred” + “thirty” = “fivehundredthirty” 
  - 971 = “ninehundred” + “seventy” + “one” = “ninehundredseventyone”


## 입력
첫째 줄에 문장을 이루는 단어의 수 N (1 ≤ N ≤ 20)가 주어진다.<br/>
다음 N개 줄에는 문장을 이루는 단어가 주어진다. 입력으로 주어지는 단어는 길이가 최대 50이며 알파벳 소문자로만 이루어져 있다. 입력으로 주어지는 단어 중에 숫자를 나타내는 단어는 없다.<br/>
$는 하나만 주어진다.<br/>


## 출력
첫째 줄에 문장을 출력한다. 항상 답이 존재하는 경우만 주어지며, 숫자는 항상 1000보다 작다.<br/>


## 예제 입력
5<br/>
this<br/>
sentence<br/>
has<br/>
$<br/>
letters<br/>

## 예제 출력
this sentence has thirtyone letters<br/>


## 풀이
문장에 등장하는 알파벳의 갯수를 세는 것이다.<br/>
정답은 항상 존재하므로 존재하지 않는 경우의 코드를 신경쓸 필요가 없다.<br/>
그리고 가장 작은 값을 찾아야 해서 1부터 넣어보며 찾는다.<br/>

그러면 숫자를 단어로 바꾸는게 필요하다.<br/>
20이하는 특정 단어가 존재한다.<br/>
그리고 30, 40, 50, .., 90과, 100, 200, 300, ..., 900도 특정 단어가 존재한다.<br/>
이외는 위에서 선언된 단어들을 합쳐 만든다.<br/>

만들어지는 과정을 보면 먼저 100의자리 숫자를 출력한다.<br/>
없는 경우 빈 문자열을 출력한다. 이후 기존 숫자에서 100의 자리 숫자를 뺀다.<br/>
뺀 숫자가 20이하인 경우 해당 숫자를 문자열로 변경한다.<br/>
여기서도 0이면 빈 문자열을 출력하면 된다.<br/>
20을 넘어가는 숫자면 10의 자리 숫자를 문자열로 변경한다.<br/>
그리고 기존 숫자에서 10의 자리숫자를 뺀다.<br/>
이제 남은 1의 자리를 숫자를 문자열로 변경하면 된다.<br/>
이들을 이어붙이면 숫자를 알파벳으로 표현할 수 있다.<br/>

이후에 기존 문자열의 알파벳과 해당 만들어진 숫자 문자열을 합친개 해당 숫자가 되는지 확인한다.<br/>
가능한 경우 해당 숫자가 전체 숫자의 갯수이다.<br/>
이후에 입력 받은 문자열을 하나씩 출력하고 숫자를 만들어진 문자열로 출력하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/9627