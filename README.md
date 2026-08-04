# PayloadParserAPI

## Wymagania

- .NET 8 SDK
- Visual Studio 2022 lub Visual Studio Code

## Uruchomienie aplikacji

1. Sklonuj repozytorium:

```bash
git clone <adres_repozytorium>
```

2. Przejdź do katalogu projektu:

```bash
cd PayloadParserAPI
```

3. Przywróć pakiety NuGet:

```bash
dotnet restore
```

4. Uruchom aplikację:

```bash
dotnet run --project PayloadParserAPI
```

Po uruchomieniu API będzie dostępne pod adresem wyświetlonym w konsoli, np.:

```
https://localhost:5001
```

Swagger jest dostępny pod adresem:

```
https://localhost:5001/swagger
```

## Alternatywnie (Visual Studio)

1. Otwórz plik rozwiązania (`.sln`).
2. Ustaw projekt Web API jako projekt startowy.
3. Naciśnij **F5** lub **Ctrl+F5**.
