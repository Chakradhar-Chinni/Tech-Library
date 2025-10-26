plan_text = """6-Week Deep .NET Knowledge Plan

Week 1 – The .NET Runtime (CLR) & Memory Fundamentals
Goal: Understand what happens under the hood when your code runs.
Focus areas:
- CLR architecture (JIT, GC, assemblies, metadata)
- Value types vs reference types in memory
- Stack vs heap allocations
- Garbage collection (generations, LOH, finalizers)
- Boxing/unboxing performance implications

Do this:
- Read: “CLR via C#” (Jeffrey Richter) – Chapters 1–5
- Try: Write code that allocates large objects and observe GC behavior using dotMemory or PerfView
- Key question: How does .NET manage memory for async/await methods?

------------------------------------------------------------

Week 2 – Async, Multithreading & Task Parallel Library (TPL)
Goal: Build deep understanding of concurrency in .NET.
Focus areas:
- Thread vs Task vs async/await
- Synchronization context
- Deadlocks and race conditions
- Parallel LINQ and TPL Dataflow

Do this:
- Build a small app that runs multiple I/O-bound tasks and observe thread pool behavior.
- Read: Stephen Cleary’s Concurrency in C# Cookbook
- Key question: Why can calling .Result on a Task cause deadlocks in ASP.NET?

------------------------------------------------------------

Week 3 – Entity Framework Core Internals & Data Access Patterns
Goal: Go beyond CRUD — understand what EF is really doing.
Focus areas:
- Change tracking & DbContext lifecycle
- Expression trees and SQL translation
- No-tracking queries and performance
- Transactions and connection management

Do this:
- Use ToQueryString() to inspect EF-generated SQL.
- Profile EF Core queries using MiniProfiler or SQL Server Profiler.
- Key question: When should you prefer Dapper or raw SQL over EF Core?

------------------------------------------------------------

Week 4 – Dependency Injection, Middleware & ASP.NET Core Pipeline
Goal: Master the request flow and application architecture.
Focus areas:
- The request pipeline (middleware order, short-circuiting)
- DI container internals (lifetime scopes, service resolution)
- Scoped vs transient vs singleton — when each makes sense
- Filters, middleware, and pipeline customization

Do this:
- Write custom middleware for logging or exception handling.
- Read: Microsoft Docs – “ASP.NET Core Middleware” and “Dependency Injection in .NET”
- Key question: Why might injecting a scoped service into a singleton cause runtime issues?

------------------------------------------------------------

Week 5 – Design Patterns & Architecture
Goal: Strengthen architectural decision-making and design clarity.
Focus areas:
- Common .NET design patterns (Strategy, Factory, Decorator, Mediator)
- SOLID principles in real applications
- CQRS, Clean Architecture, Onion Architecture
- Trade-offs in modular monoliths vs microservices

Do this:
- Refactor a small project using MediatR or CQRS pattern.
- Watch: Jason Taylor’s Clean Architecture talk (YouTube)
- Key question: What are the trade-offs of using MediatR vs direct service calls?

------------------------------------------------------------

Week 6 – Performance, Debugging & Profiling
Goal: Become confident diagnosing and optimizing .NET apps.
Focus areas:
- Memory leaks & performance bottlenecks
- Using diagnostic tools (dotTrace, PerfView, BenchmarkDotNet)
- Understanding async call stacks in debugging
- Caching strategies & measuring performance

Do this:
- Benchmark a LINQ vs parallel LINQ scenario using BenchmarkDotNet
- Read: High Performance .NET Apps (Microsoft docs)
- Key question: What’s the cost of async state machines in tight loops?

------------------------------------------------------------

Ongoing Habits
- Read source code – check .NET runtime or ASP.NET Core repo on GitHub.
- Blog or journal what you learn — teaching helps solidify understanding.
- Simulate interview depth questions — ask “why” five times for every feature.
"""

with open("Deep_NET_Knowledge_6_Week_Plan.txt", "w", encoding="utf-8") as f:
    f.write(plan_text)

print("✅ File saved as 'Deep_NET_Knowledge_6_Week_Plan.txt'")
