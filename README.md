# SimpleMath
API для сложения и умножения 2 чисел. 

## Parallel requests limits
Ограничение максимального числа параллельно обрабатываемых запросов реализовано через `Middleware` (см. `SimpleMath.Web.Middleware.ParallelLimitingMiddleware`), конфигурирование - в `appsettings.json` в разделе `Constants` указать `ParallelRequestsLimit`, например:
```json
{
  "Constants": {
    "ParallelRequestsLimit": 10
  }
}
```

## Known issues
- [ ] Тестирование через Bombardier при вызове из кода дает рандомные результаты, причины неизвестны. Временный фикс - проверяем что отклоненные запросы есть, а не точное их количество.