OperatDo v1.0.2-beta, 30 grudnia 2016
---
Importuj zeskanowane operaty do bazy danych Ośrodka

# Pomoc

1. Wczytać zeskanowane operaty z dysku
2. Odczytać operaty z bazy danych Ośrodka
2.1 Wybrać operaty, które znaleziono w bazie danych Ośrodka
2.2 Zapisać raport z operatami, których nie znaleziono
3. Odczytać dokumenty operatów z bazy danych Ośrodka
3.1 Wybrać operaty, które nie posiadają żadnych dokumentów w bazie danych Ośrodka
3.2 Zapisać raport z operatami, które posiadają dokumenty
4. Zapisać operaty w bazie danych Ośrodka

> OperatDo.exe db id[:asIdZasobu or :asUid]

> OperatDoQuery.exe db id

# Historia

Do zrobienia

* [ ] wybór użytkownika przypisanego do importu
* [ ] symulacja importu operatów

2016-12-30 v1.0.2-beta

* poprawiony: widok operatów (zakryty ostatni element na liście)

2016-12-29 v1.0.1-beta

* poprawione: jawne zwalniane zasobów połączenia do bazy danych
* aktualizacja: domyślny katalog ze skanami w pliku konfiguracyjnym

2016-12-29 v1.0-beta

* nowość: import operatu do bazy danych Ośrodka
* nowość: wczytywanie zeskanowanych operatów z dysku
* nowość: odczytywanie operatów z bazy danych Ośrodka
* nowość: odczytywanie dokumentów operatu z bazy danych Ośrodka
* nowość: sortowanie i numerowanie dokumentów
* nowość: rodzaj dokumentu ze słownika PZG_SLOWNIK
* nowość: skrypty do usuwania dokumentów i plików

2016-12-16 v1.0-alfa

* propozycja projektu
