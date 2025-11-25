Managerial rounds are story-driven, not fact-driven.
Interviewers look for how you behave, how you think, and how you solve problems, not just what you know.
Follow STAR pattern: Situation, Task, Action, Result (pure tech. qns don't follow STAR format)

What not to do in Manager or Techno Manager Round
/******

 1. Don’t Give Generic or Vague Answers

 Avoid lines like “I always give my best”, “I am a team player”, “I work hard”.
 Always give a specific example (preferably STAR), even if question doesn’t explicitly ask.

 2. Don’t Bad-Mouth Your Current Company or Manager

 No complaints about workload, politics, culture, etc.
 Keep tone neutral and professional.

 3. Don’t Show Lack of Ownership

 Avoid sentences like:

   “That wasn’t my responsibility.”
   “The other team delayed it.”
 Even if others caused issues, focus on your corrective actions.

 4. Don’t Over-explain or Ramble

 Managerial rounds value clarity + structure.
 Keep answers structured; avoid story-telling without a point.

 5. Don’t Admit Skills You Can’t Defend

 If you claim something (“I know microservices deeply”), you must be ready for follow-ups.

 6. Don’t Sound Like You Only Code

 Manager wants:

   Planning
   Collaboration
   Communication
   Risk handling
   Ownership
 Avoid answers that only focus on coding tasks.

 7. Don’t Blame Teams or Processes

 Instead of blame → talk about alignment, communication, and mitigation steps.

 8. Don’t Avoid Talking About Failures

 Managers expect self-awareness.
 You can discuss failures → but show what you learned and changed.

 9. Don’t Answer Without Business Reasoning

 Show you understand:

   Priorities
   Impact
   Value delivered
 Not just technical execution.

 10. Don’t Skip STAR When It Matters

 Behavioural + ownership + leadership questions → use STAR for clarity.
 Technical decision + architecture reasoning → use CORED (Context → Options → Reasoning → Execution → Decision).

 11. Don’t Show Zero Curiosity

 If asked, “How would you improve this process?” → don’t say “it looks fine”.
 Managers want proactive mindset.
*//////////////

STAR Format Tips and Tricks: 
/*

 ⭐ **All STAR Tips (Combined List)**

 **A. Project Ownership – STAR Tips**

* Show initiative: API contracts, architecture decisions, validations, logging.
* Show responsibility: testing, deployment, monitoring.
* Highlight end-to-end ownership across design → development → deployment → support.

---

 **B. Delivery & Planning – STAR Tips**

* Mention root-cause analysis when delays occur.
* Communicate early — don’t wait until the end of the sprint.
* Break tasks into smaller deliverables.
* Always describe your mitigation approach.
* Show how you kept dependent teams unblocked.

---

 **C. Technical Decision Making – STAR Tips**

* Show how you evaluated multiple options before choosing one.
* Talk about trade-offs (performance, maintainability, scalability).
* Show alignment with .NET best practices (Clean Architecture, EF Core, DI, SOLID).
* Mention business impact of your technical decisions.

---

 **D. Production Issues – STAR Tips**

* Focus on quick diagnosis: logs, Application Insights, SQL bottlenecks.
* Mention communication cadence with managers during incidents.
* Explain the temporary fix AND the permanent fix.
* Emphasize stability and performance improvements.

---

 **E. Behaviour + Ownership – STAR Tips**

* Never blame individuals or teams — stay neutral.
* Show emotional maturity and professionalism.
* Demonstrate conflict resolution and collaboration.
* Show how you accepted responsibility and provided solutions.
* Highlight proactive mindset (anticipating issues before they escalate).


*/


# ✅ **1. Project Ownership & Delivery – 20 Questions**
/*

# ✅ **1. Describe an end-to-end .NET project you owned completely.**

 **Situation**

My team needed an internal automation system to replace manual processing.

 **Task**

I was responsible for the complete lifecycle—requirements, .NET Core API development, database, testing, deployment, and support.

 **Action**

* Gathered requirements and finalized API contracts.
* Built .NET Core APIs using Clean Architecture + EF Core.
* Added validation, logging (Serilog), caching, and integration tests.
* Coordinated CI/CD pipeline setup and led UAT + production deployment.
* Monitored production logs and optimized performance.

 **Result**

Delivered within the sprint, reduced manual effort by **40%**, and achieved stable production performance with zero P1 issues.

---

# ✅ **2. How do you ensure smooth API delivery when multiple modules depend on your work?**

 **Situation**

Several teams needed my APIs to proceed with their UI and service development.

 **Task**

Ensure dependent teams remained unblocked and integration was smooth.

 **Action**

* Finalized API contracts early and shared Swagger + Postman collections.
* Provided mock endpoints so teams could start parallel development.
* Communicated contract changes immediately.
* Performed early UAT deployments for integration testing.

 **Result**

No team was blocked, and integration was completed without last-minute rework.

---

# ✅ **3. What steps do you take when a sprint is at risk of delay?**

 **Situation**

A sprint was at risk due to requirement gaps and environment issues.

 **Task**

Prevent sprint failure and ensure transparency.

 **Action**

* Identified root cause immediately.
* Informed PO/SM the same day.
* Split features into critical and non-critical deliverables.
* Prioritized core functionality and got help for secondary tasks.
* Worked with infra to resolve environment issues.

 **Result**

Delivered all critical items on time, avoided escalations, and retained sprint predictability.

---

# ✅ **4. Tell me about a last-minute production issue you handled.**

 **Situation**

A production API was timing out during peak load due to a slow SQL query.

 **Task**

Resolve the issue within a 30-minute SLA.

 **Action**

* Analyzed logs (Serilog + App Insights) to identify the failing SP.
* Found missing index and coordinated a hotfix deployment with DBA.
* Validated performance and ensured no regression.
* Updated management every 10–15 minutes.

 **Result**

Service restored in **20 minutes**, query became **60% faster**, and no further incidents occurred.

---

# ✅ **5. How do you communicate delays or blockers to management?**

 **Situation**

A dependent team had not provided their final API contract, blocking integration.

 **Task**

Communicate the impact clearly and manage expectations.

 **Action**

* Escalated early with full context (completed 80%, pending integration).
* Presented what I had already done and what was impacted.
* Proposed mitigation: proceed with mock data temporarily.
* Provided revised ETA.

 **Result**

Management had clarity, UI team remained unblocked, and integration was completed the same day updated contract arrived.

---

# ✅ **6. How do you ensure requirements are fully understood before coding?**

 **Situation**

Requirements had multiple edge cases that weren’t documented.

 **Task**

Understand all functional and non-functional expectations before coding.

 **Action**

* Conducted detailed walkthrough with PO.
* Converted requirements into API contracts and workflow diagrams.
* Identified missing cases and clarified them upfront.
* Documented assumptions and got confirmations.

 **Result**

Avoided rework, delivered correct functionality in first attempt, and reduced QA defects.

---

# ✅ **7. Explain a hard technical decision you made and justified.**

 **Situation**

A reporting module was slow due to heavy EF Core LINQ queries.

 **Task**

Choose the best data-access approach for performance.

 **Action**

* Profiled EF queries using SQL Profiler.
* Benchmarked Stored Procedures vs LINQ.
* Evaluated maintainability and load patterns.
* Recommended SPs for reporting, keeping business logic outside.

 **Result**

Report execution improved by **70%**, and the architecture remained clean.

---

# ✅ **8. How do you handle when QA keeps finding issues in your work?**

 **Situation**

QA reported repeated defects around validations and error handling.

 **Task**

Reduce recurring issues and improve code quality.

 **Action**

* Analyzed patterns of defects.
* Added missing validations + central exception handling.
* Strengthened unit and integration tests.
* Collaborated with QA to clarify acceptance criteria.
* Did peer review before every check-in.

 **Result**

Defects reduced significantly in the following sprint, and QA confidence improved.

---

# ✅ **9. Tell me how you ensured quality in your last .NET release.**

 **Situation**

It was a major API release involving multiple modules.

 **Task**

Ensure quality across all API layers.

 **Action**

* Added unit tests, integration tests, and contract tests.
* Enforced code reviews + SonarQube rules.
* Performed load testing in staging.
* Used feature toggles for safe rollout.
* Validated logs for hidden failures.

 **Result**

Release went live with zero major defects and improved overall reliability.

---

# ✅ **10. What do you do when business requirements keep changing?**

 **Situation**

Business requested multiple scope changes mid-sprint.

 **Task**

Manage change without destabilizing the sprint.

 **Action**

* Documented each change and assessed effort/impact.
* Presented options: extend timeline, drop low-priority items, or phase delivery.
* Ensured PO approved the final scope.
* Updated estimates and sprint plan.

 **Result**

Delivered a stable MVP on time, while remaining changes were planned for the next sprint.

---

# ✅ **11. How do you ensure cross-team dependencies don’t break delivery?**

 **Situation**

Multiple modules depended on shared API payloads.

 **Task**

Coordinate and ensure compatibility across teams.

 **Action**

* Finalized contracts early and versioned them.
* Maintained dependency tracker in Jira.
* Conducted integration checkpoints weekly.
* Provided mock services during delays.

 **Result**

No unexpected integration failures, and delivery stayed on schedule.

---

# ✅ **12. Tell me about a technical debt issue you resolved.**

 **Situation**

Legacy synchronous repository calls were slowing down API throughput.

 **Task**

Improve performance and modernize the code.

 **Action**

* Refactored code to async/await.
* Removed redundant queries and optimized EF Core mappings.
* Added caching for frequently accessed data.

 **Result**

Response time improved from **1.8s to 300ms**, and API handled 3× more load.

---

# ✅ **13. Explain a time you led a release from planning to deployment.**

 **Situation**

Quarterly major release with multiple teams contributing.

 **Task**

Own planning, coordination, testing, and deployment.

 **Action**

* Created detailed release plan and timelines.
* Coordinated dev + QA for testing coverage.
* Validated UAT signoffs and executed migration scripts.
* Managed production deployment and post-release monitoring.

 **Result**

Release completed successfully with zero downtime and no P1/P2 issues.

---

# ✅ **14. How do you track and manage risks in your project?**

 **Situation**

A project had multiple API + dependency risks early.

 **Task**

Mitigate risks before they impacted delivery.

 **Action**

* Created a risk register with severity.
* Identified high-impact risks upfront.
* Defined mitigation strategies (fallback APIs, mocks, backups).
* Reviewed risks daily in stand-ups.
* Escalated critical items early.

 **Result**

All risks were mitigated, and the project delivered smoothly.

---

# ✅ **15. How do you balance speed vs quality in API delivery?**

 **Situation**

A high-priority feature had a tight timeline.

 **Task**

Deliver fast without compromising stability.

 **Action**

* Built minimal functional API first.
* Added validations, tests, and optimizations incrementally.
* Reused existing components.
* Automated repetitive tasks (code templates, scripts).

 **Result**

Delivered within timeline and passed QA with minimal defects.

---

# ✅ **16. How do you push back when requirements are unrealistic?**

 **Situation**

Stakeholders wanted a complex integration in one sprint.

 **Task**

Negotiate realistically without conflict.

 **Action**

* Presented complexity, effort breakdown, and risks.
* Proposed phased delivery with clear milestones.
* Suggested alternatives to reduce complexity.
* Provided data from previous sprints to justify.

 **Result**

Stakeholders agreed to phased approach, and delivery was smooth.

---

# ✅ **17. What’s your approach when a junior developer breaks a build?**

 **Situation**

A junior dev pushed untested code and broke the CI pipeline.

 **Task**

Fix the issue quickly and ensure it doesn’t repeat.

 **Action**

* Helped identify the faulty commit and reverted it.
* Walked the junior through proper testing steps.
* Updated branch protection rules.
* Conducted a short training on CI/CD best practices.

 **Result**

Build restored quickly, and similar incidents reduced significantly.

---

# ✅ **18. Tell me about a time you improved an existing .NET solution.**

 **Situation**

Controllers had duplicate logic and poor separation of concerns.

 **Task**

Improve maintainability and structure.

 **Action**

* Introduced service and repository layers.
* Applied Clean Architecture patterns.
* Extracted reusable components and DTOs.
* Added proper DI setup.

 **Result**

Reduced duplication by **40%**, improved testability, and made onboarding easier.

---

# ✅ **19. How do you prepare for a major release deployment?**

 **Situation**

A major release involved DB changes + new APIs.

 **Task**

Ensure a safe deployment with minimal risk.

 **Action**

* Verified UAT signoff and staging tests.
* Reviewed migration scripts and planned rollback.
* Ensured logging dashboards were ready.
* Conducted a pre-deployment checklist meeting.

 **Result**

Deployment completed successfully with zero rollback and stable performance.

---

# ✅ **20. How do you ensure your team sticks to coding standards?**

 **Situation**

Team members followed inconsistent coding practices.

 **Task**

Enforce consistent standards across the codebase.

 **Action**

* Created coding guideline document (naming, DI, layering).
* Enforced mandatory PR reviews.
* Integrated SonarQube quality gates.
* Conducted small knowledge-sharing sessions.

 **Result**

Code quality improved, defects reduced, and PR cycles became faster and cleaner.

---

*/


---

# ✅ **2. Technical Decision Making – 20 Questions**

/*

# 1. How do you decide between .NET API vs Azure Functions?

 **Situation**

We needed to choose a hosting model for a new service: always-on APIs vs event-driven serverless.

 **Task**

Select the model that meets latency, scale, cost, and operational needs.

 **Action**

Evaluated requirements: expected traffic pattern (steady vs spiky), cold-start tolerance, execution time, integration needs, and cost. For long-running or complex request/response flows I preferred .NET Web API; for event-driven, short-lived tasks triggered by queues/timers, I preferred Azure Functions. Also checked local development and deployment constraints.

 **Result**

Chose Azure Functions for short, sporadic background processing (reduced cost by ~60% in a PoC). Kept .NET API for user-facing endpoints to guarantee low latency and richer middleware control.

---

# 2. Why would you choose EF Core over Dapper (or vice-versa)?

 **Situation**

We needed a data access strategy for a mixed workload service: complex queries and many CRUD operations.

 **Task**

Pick the ORM/tool that balances productivity and performance.

 **Action**

Benchmarked typical queries, considered developer productivity, maintainability, and control. Chose EF Core where model mapping, change tracking, and migrations mattered; chose Dapper for high-performance, hand-tuned queries and reporting paths. Used both in different modules when appropriate.

 **Result**

Delivered faster development for CRUD endpoints using EF Core, while critical reporting endpoints using Dapper met latency SLAs (report queries improved by ~3×).

---

# 3. When do you implement caching in .NET?

 **Situation**

An API experienced repeated identical read requests causing DB load.

 **Task**

Reduce DB load and improve response times without breaking consistency.

 **Action**

Identified hot endpoints and data freshness requirements. Implemented MemoryCache for single-instance, low-latency cases and Redis for cross-instance distributed caching. Added appropriate TTLs and cache invalidation on writes.

 **Result**

Reduced DB read load by ~50% and cut average response time for cached endpoints from 400ms to ~60ms.

---

# 4. How do you choose between SQL vs NoSQL for a project?

 **Situation**

New feature required storing user events and transactional data.

 **Task**

Decide the database type that fits consistency, query patterns, scalability, and schema needs.

 **Action**

Analyzed access patterns: strong ACID transactions and joins → SQL; flexible schema, high write throughput, or document-centric queries → NoSQL. Considered scaling plan, operational complexity, and reporting needs. Chose hybrid where transactional data stayed in SQL and event logs used a NoSQL store.

 **Result**

Achieved transactional integrity for payments and scalable event storage for analytics, keeping reporting simple and costs controlled.

---

# 5. Explain a time you converted a monolith to microservices.

 **Situation**

A legacy .NET monolith caused long deployment cycles and impeded team autonomy.

 **Task**

Break the monolith into services without disrupting production.

 **Action**

Prioritized domain boundaries, extracted one bounded-context at a time starting with a low-risk module. Created APIs, added contract tests, introduced message-based integration for async flows, and set up independent CI/CD pipelines. Kept the monolith running and gradually cut over traffic.

 **Result**

Reduced deployment time for the extracted module from hours to minutes, enabled parallel team releases, and improved reliability during deployments.

---

# 6. How do you decide what belongs in a controller vs service vs repository?

 **Situation**

Codebase had fat controllers with business logic and DB calls mixed.

 **Task**

Refactor to improve separation of concerns and testability.

 **Action**

Defined boundaries: controllers handle HTTP, validation, and mapping; services contain business rules and orchestration; repositories handle data access. Applied dependency injection and added unit tests around services. Enforced rules in code reviews.

 **Result**

Controllers became thin, service logic was testable in isolation, and PR review speed increased while bugs from incorrect layering decreased.

---

# 7. What metrics do you consider before optimizing an API?

 **Situation**

An endpoint showed intermittent slow responses in production.

 **Task**

Identify whether to optimize and where to focus.

 **Action**

Collected metrics: p95/p99 latency, throughput (req/sec), CPU/memory, DB query times, error rates, and request traces. Used Application Insights and SQL Profiler to pinpoint hotspots, then prioritized fixes with highest impact.

 **Result**

Targeted optimization (index + query rewrite) reduced p99 latency by ~70% for that endpoint.

---

# 8. How do you choose the right logging framework for a .NET project?

 **Situation**

We needed centralized logging across services for diagnostics and monitoring.

 **Task**

Select a logging framework that supports structured logs, sinks, and performance.

 **Action**

Evaluated Serilog, NLog, and built-in ILogger: features, structured logging support, enrichers, sinks (Elasticsearch, App Insights), and community support. Chose Serilog for structured logs and rich sinks, integrated with correlation IDs and enrichment.

 **Result**

Improved troubleshooting speed (mean time to diagnose reduced) and enabled efficient log-based alerting and dashboards.

---

# 9. What is your approach to designing scalable endpoints?

 **Situation**

Service needed to handle rising traffic as user base grew.

 **Task**

Design endpoints that scale and remain maintainable.

 **Action**

Applied principles: stateless endpoints, pagination, limiting payload sizes, proper HTTP caching, and idempotency for write operations. Used async patterns, connection pooling, and partitioning strategies. Designed endpoints for graceful degradation and added load tests to validate.

 **Result**

System scaled horizontally with minimal changes; during a spike we handled ~3× traffic without service degradation.

---

# 10. When do you use asynchronous programming?

 **Situation**

API endpoints called external services and DB concurrently causing thread blocking.

 **Task**

Improve throughput and responsiveness.

 **Action**

Used async/await for IO-bound operations (DB calls, HTTP requests). Ensured proper ConfigureAwait usage in libraries, avoided blocking calls, and profiled thread pool usage under load.

 **Result**

Increased request throughput and decreased thread pool starvation, improving overall responsiveness under load.

---

# 11. Why would you choose Web API over gRPC or SignalR?

 **Situation**

We needed to expose services to multiple clients including web, mobile, and third-party integrations.

 **Task**

Select the appropriate communication protocol.

 **Action**

Considered client compatibility, latency, and communication model. Chose RESTful Web API for broad client compatibility and human-readable contracts; selected gRPC for high-performance internal RPC and SignalR for real-time push scenarios.

 **Result**

Delivered public APIs via Web API for easy consumption, used gRPC internally for low-latency microservice calls, and employed SignalR for live notifications — each chosen for its strengths.

---

# 12. When would you use stored procedures instead of LINQ?

 **Situation**

A reporting workload required complex joins and heavy aggregations.

 **Task**

Decide when to use SPs for performance versus maintainability with LINQ.

 **Action**

Benchmarked complex queries: if DB-side processing yields significant performance gains or reduces data transfer (heavy aggregations), used stored procedures; for typical CRUD and maintainable query composition, used EF Core/LINQ.

 **Result**

Critical reports executed faster with SPs while day-to-day features stayed maintainable with LINQ.

---

# 13. How do you decide when to introduce a message queue (RabbitMQ/Kafka)?

 **Situation**

Synchronous calls caused cascading failures during peak load.

 **Task**

Introduce async processing to decouple components and improve resilience.

 **Action**

Assessed need for durability, ordering, throughput, and consumer patterns. For high-throughput event streams and analytics used Kafka; for typical task queues and easy routing used RabbitMQ. Implemented retry and dead-lettering patterns.

 **Result**

Decoupling removed direct dependencies, smoothed traffic spikes, and reduced failure cascades during load events.

---

# 14. How do you pick between vertical vs horizontal scaling?

 **Situation**

Our service faced periodic load spikes and resource saturation.

 **Task**

Choose a cost-effective scaling approach that meets reliability needs.

 **Action**

Analyzed bottlenecks: CPU/memory bound vs stateful constraints. Preferred horizontal scaling (stateless instances behind load balancer) for web APIs; used vertical scaling for single-node DBs or when licensing/architecture limited horizontal options. Also considered autoscaling policies and cost.

 **Result**

Implemented horizontal scaling for the API layer (autoscale rules) and scaled DB vertically with read replicas, achieving target SLA at optimized cost.

---

# 15. What is your approach to selecting retry policies or circuit breakers?

 **Situation**

Inter-service transient failures caused increased latency and retries.

 **Task**

Improve resiliency without overwhelming failing services.

 **Action**

Applied Polly to implement retry with exponential backoff for transient faults and circuit breakers for sustained failures. Tuned retry counts, backoff intervals, and circuit thresholds based on SLA and observed failure rates. Added logging and metrics for visibility.

 **Result**

Reduced cascading retries, improved system stability, and provided clearer alerting when downstream systems failed.

---

# 16. What performance counters matter for .NET APIs?

 **Situation**

We needed to monitor and diagnose production performance.

 **Task**

Pick key telemetry for proactive monitoring.

 **Action**

Monitored request latency (p50/p95/p99), throughput, error rates, GC metrics (Gen 0/1/2 collections, heap size), thread pool usage, CPU, memory, and DB latency. Used these metrics to trigger alerts and guide optimization.

 **Result**

Early detection of GC pressure and DB slowdowns prevented major incidents and informed capacity planning.

---

# 17. How do you pick between Redis vs MemoryCache?

 **Situation**

Caching was required across services in a distributed deployment.

 **Task**

Choose caching that fits scale and persistence needs.

 **Action**

Used MemoryCache for simple, per-instance caching with extremely low latency and no cross-instance needs. Used Redis for distributed caching, session storage, and pub/sub across multiple instances. Considered eviction policies and TTLs.

 **Result**

Chosen caches reduced latency and DB load; Redis enabled consistent caching across instances while MemoryCache served low-latency local needs.

---

# 18. How do you decide if something needs DI or factory pattern?

 **Situation**

Component creation logic varied based on runtime parameters and dependencies.

 **Task**

Choose a pattern that keeps code testable and maintainable.

 **Action**

Used DI for standard dependency management and lifetime control; used factory pattern when creation required runtime parameters or polymorphic instantiation. Kept factories injectable to preserve testability.

 **Result**

Code remained decoupled and testable; factories handled complex instantiation without polluting design with service locator patterns.

---

# 19. What do you check before approving a database schema change?

 **Situation**

A schema change was proposed that could impact production performance.

 **Task**

Validate change for correctness, performance, and rollback safety.

 **Action**

Reviewed migration scripts for locking behavior, estimated migration time, validated indexes, checked backward compatibility, ran migration in staging, ensured backups, and prepared rollback/long-running migration strategies (online migrations or phased rollout).

 **Result**

Approved safe migrations that avoided long locks and prevented production outages.

---

# 20. How do you decide whether an issue is API-side or DB-side bottleneck?

 **Situation**

An endpoint showed high latency and unclear root cause.

 **Task**

Pinpoint whether the bottleneck is in the API code or the database.

 **Action**

Collected distributed traces and timings (API middleware, DB call durations), profiled application CPU and thread usage, examined query execution plans and DB wait stats. If DB queries consumed most time, focused on indexes/query fixes; if API CPU or serialization dominated, optimized code paths or caching.

 **Result**

Accurate diagnosis led to the correct fix: an index addition improved the same endpoint’s p99 by 70% versus an unnecessary application-level rewrite.




*/

---

# ✅ **3. Architecture & Best Practices – 20 Questions**

1. What architecture did your last .NET project use (clean, n-tier, hexagonal)?
2. How do you enforce clean architecture in your team?
3. How do you design a multi-layer .NET Core API?
4. What are your logging and monitoring best practices?
5. How do you ensure modularization in .NET solutions?
6. What’s your approach to handling exceptions globally?
7. How do you secure your APIs end-to-end?
8. Explain how you implement pagination correctly.
9. What role does caching play in high-performance APIs?
10. How do you manage configuration for different environments?
11. How do you perform load testing on APIs?
12. How do you structure your solution for microservices?
13. What is your versioning strategy for APIs?
14. How do you ensure testability of your .NET code?
15. What are your best practices for EF Core queries?
16. How do you avoid N+1 query issues?
17. How do you handle secrets and sensitive config?
18. Explain your strategy for DTOs vs domain models.
19. How do you ensure compatibility between services?
20. What patterns do you commonly use? (Repository, CQRS, Mediator, etc.)

---

# ✅ **4. Delivery, Planning, Ownership – 20 Questions**

1. How do you plan sprints when your module is complex?
2. How do you break large tasks for better predictability?
3. How do you prioritize bugs vs features?
4. How do you ensure your estimates are realistic?
5. Describe your approach to daily standups.
6. How do you handle tasks that look vague?
7. How do you ensure the team meets sprint commitments?
8. How do you deal with dependency delays?
9. How do you perform RCA for delays?
10. What is your strategy for handling unplanned work?
11. How do you capture and manage technical debt?
12. How do you assign tasks to team members effectively?
13. How do you report project progress to stakeholders?
14. How do you ensure no one is overloaded?
15. How do you ensure good coordination between dev, QA, BA?
16. How do you handle holiday or resource constraints?
17. What’s your method for planning API changes safely?
18. How do you prepare for sprint reviews?
19. How do you plan integration testing?
20. How do you negotiate deadlines with stakeholders?

---

# ✅ **5. Behaviour + Ownership Combination – 20 Questions**

1. Tell me about a time you took ownership beyond your role.
2. Describe a situation where you disagreed with your manager.
3. How do you handle pressure when deadlines are tight?
4. Tell me a time when you failed and what you learned.
5. Describe a time you solved a big problem no one else could.
6. How do you handle uncooperative teammates?
7. Tell me about a time you motivated a struggling team member.
8. How do you handle situations where you are blamed for issues?
9. Tell me how you deal with requirements that are unclear.
10. Describe a time you worked with a challenging client.
11. Tell me about a time when you simplified a complex problem.
12. How do you manage conflicts inside your team?
13. What would your previous manager describe about your ownership style?
14. How do you react when your code is criticized?
15. Tell me about a time you went out of the way to support a team member.
16. Describe the most stressful situation you handled.
17. How do you help juniors become independent?
18. Describe a mistake you made and how you fixed it.
19. How do you ensure transparency in your work?
20. Tell me a time when your decision significantly improved the project.

---

# ✅ **6. Managerial “What If” Scenarios – 20 Questions**

1. What if your API is timing out under load?
2. What if DB latency suddenly increases?
3. What if your deployment fails mid-way?
4. What if a senior developer refuses to follow coding standards?
5. What if the client demands a feature that breaks architecture?
6. What if you inherit a poorly designed .NET application?
7. What if a sprint ends but your team still has incomplete tasks?
8. What if QA reports critical bugs just before release?
9. What if your team is dependent on another team that delays work?
10. What if a high-priority prod issue happens at 2 AM?
11. What if your team continuously overestimates or underestimates tasks?
12. What if a team member wants to resign in the middle of a release?
13. What if a junior developer pushes vulnerable code?
14. What if business asks for a 1-day delivery for a 5-day task?
15. What if performance drops after a .NET Core version upgrade?
16. What if management asks you to cut scope without cutting deadlines?
17. What if code quality drops across the team?
18. What if a merge causes unexpected regression?
19. What if you notice a design flaw very late in the project?
20. What if a stakeholder disagrees with your technical solution?

---

If you want, I can now generate:

✔ **Answers for all or for specific categories**
✔ **Your story-based answers tailored to your actual .NET experience**
✔ **A cheat-sheet PDF for final day revision**

Which one do you want next?
