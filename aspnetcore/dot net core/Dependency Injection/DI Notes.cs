1. Controllers are created per request by default (scoped-like behavior) and destroyed after the request completes. this happens regardless of service lifetime
2. A singleton is created once when the app starts and destroyed only when the app shuts down (or host stops). Disposal occurs only once.
3. Scoped destroys at end of each request, recreates again for next request. However, the same scoped instance is used for sub requests.
4. Transient destroys at end of each request, recreates again for next request. FOr sub requests, new transient is created.
5. return Ok() marks logical end of code, immediately Dispose() gets called  statement implies end of each request 
6. after disposal our app no longer references it, so it becomes eligible for GC
Note: don't call GC manually as it interrupts program execution, only in critical cases where tons of memory is release and GC is required call GC, else don't do.
  CLR knows when to do GC



after starting app, I hit the api 3 times, 
1. singleton gave the same GUID 3 times
2. scoped,transient generated new GUIDs
3. COnsole logs
  - Controller gets destroyed at end of each request
  - Scoped,transient also destroyed after each request
  - Singleton destroyed at app exit


GET: https://localhost:7093/api/home - 1st time
{
  "singleton": "13a37699-39bf-4043-bddc-95f20a86c476",
  "scoped": "7883cf0e-1fc9-44bc-81a7-c7af019065a3",
  "transient": "87bf34df-cf99-405f-907a-ab5d44be3b1a"
}

https://localhost:7093/api/home - 2nd time
{
  "singleton": "13a37699-39bf-4043-bddc-95f20a86c476",
  "scoped": "1126559a-5497-48b7-b4c8-b8a6ee5dcf72",
  "transient": "95f5d6c9-5ee0-48af-a0b9-3952be10213b"
}

GET: https://localhost:7093/api/home - 3rd time
{
  "singleton": "13a37699-39bf-4043-bddc-95f20a86c476",
  "scoped": "91d0c727-7f20-430f-8e17-cad1f2be5ba0",
  "transient": "26d70004-6635-47f6-a181-597b9bdb346c"
}

console logs:

HomeController instance created
HomeController instance Destroyed
TransientService Destroyed 87bf34df-cf99-405f-907a-ab5d44be3b1a - Transient
ScopedService Destroyed 7883cf0e-1fc9-44bc-81a7-c7af019065a3 - Scoped
HomeController instance created
HomeController instance Destroyed
TransientService Destroyed 95f5d6c9-5ee0-48af-a0b9-3952be10213b - Transient
ScopedService Destroyed 1126559a-5497-48b7-b4c8-b8a6ee5dcf72 - Scoped
HomeController instance created
HomeController instance Destroyed
TransientService Destroyed 26d70004-6635-47f6-a181-597b9bdb346c - Transient
ScopedService Destroyed 91d0c727-7f20-430f-8e17-cad1f2be5ba0 - Scoped
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
SingletonService Destroyed 13a37699-39bf-4043-bddc-95f20a86c476 - Singleton
