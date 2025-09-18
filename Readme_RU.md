# Сервис Рекламных Локаций – C# Web API

Простой веб-сервис, который позволяет быстро находить подходящие рекламные платформы для указанного местоположения. Все данные хранятся **в памяти** (in-memory), а сервис оптимизирован для быстрого поиска.

# ! Возможные ошибки и решения перечислены в файле. Пожалуйста, ознакомьтесь с ними.

---

## Требования

- Проект создан с использованием .NET 9.0. Запуск на этой версии является наиболее безопасным.
- Любая IDE. (Проект разрабатывался в VS Code, поэтому инструкции ориентированы на него.)

---

## Установка

## Тестирование Backend через Swagger

1. Клонируйте репозиторий:

```bash
git clone https://github.com/TurkerM/AdPlatformsService.git
cd AdPlatformsService/AdPlatformService
```

2. Установите зависимости и соберите проект:

```bash
dotnet restore
dotnet build
```

3. Запустите сервис:

```bash
dotnet run
```

Сервис будет доступен по умолчанию на `http://localhost:5292`.

### В случае ошибок

Из-за unit-тестов некоторые автоматически создаваемые файлы могут вызвать проблемы. Можно выбрать один из двух способов исправления:

1. В каталоге AdPlatformService\ProjectTests выполните следующие команды:

```bash
powershell -Command "(Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs')[2]} | Set-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs'"
powershell -Command "(Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs')[11]} | Set-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs'"
```

2. Либо вручную удалите 3-ю строку файла `'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs'` и 12-ю строку файла `'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs'`, которые отображаются в сообщении об ошибке.

После выполнения одного из способов снова запустите веб-API:

```bash
dotnet run
```

---

## Использование API

API можно использовать через Swagger. После запуска приложения откройте URL ниже для просмотра и тестирования всех endpoint'ов:

- Все endpoints, необходимые параметры и примеры ответов доступны в Swagger.
  `http://localhost:5292/swagger`

## Обработка некорректных данных

- Сервис не падает при отсутствии или неправильном формате данных.
- Если в файле есть некорректные строки, они будут пропущены, а остальные данные загружены.
- При некорректных параметрах запроса местоположения возвращается `"No results found!"`.

---

## Unit-тесты

Unit-тесты находятся в папке `ProjectTests`. Для запуска:

1. Перейдите в каталог:

```bash
cd AdPlatformService\ProjectTests
```

2. Выполните команду:

```bash
dotnet test
```

### В случае ошибок

Из-за unit-тестов некоторые автоматически создаваемые файлы могут вызвать проблемы. Для исправления есть два способа:

1. В каталоге AdPlatformService\ProjectTests выполните следующие команды:

```bash
powershell -Command "(Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs')[2]} | Set-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs'"
powershell -Command "(Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs')[11]} | Set-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs'"
```

2. Либо вручную удалите 3-ю строку файла `'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs'` и 12-ю строку файла `'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs'`.

После этого снова выполните команду для тестирования методов:

```bash
dotnet test
```

## Требования для фронтенда

- Node.js >= 18
- npm >= 8

## Установка фронтенда

Если вы хотите протестировать API через frontend на React, выполните следующие шаги:

1. Убедитесь, что API работает на `http://localhost:5292`.

2. Перейдите в каталог:

```bash
cd AdPlatformsService/ad-platform-frontend
```

3. Установите зависимости:

```bash
npm install
```

4. Запустите frontend в режиме разработки:

```bash
npm run dev
```

Vite-сервис будет доступен по умолчанию на `http://localhost:5173`.

