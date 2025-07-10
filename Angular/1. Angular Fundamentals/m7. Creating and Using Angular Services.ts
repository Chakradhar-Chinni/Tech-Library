<<h2>> Introduction

1. Services are used to business logic 
2. can implement using DI, Inject function

  
<<h2>> Creating an Angular Service - quick overview


 Step 1: Create the Service

You can generate a service using Angular CLI:

```bash
ng generate service message
```

Or manually:

```ts
// message.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root' // makes it available app-wide
})
export class MessageService {
  getMessage(): string {
    return 'Hello from the service!';
  }
}
```

---

### 🧩 Step 2: Use the Service in a Component

```ts
// app.component.ts
import { Component, OnInit } from '@angular/core';
import { MessageService } from './message.service';

@Component({
  selector: 'app-root',
  template: `<p>{{ message }}</p>`
})
export class AppComponent implements OnInit {
  message = '';

  constructor(private messageService: MessageService) {}

  ngOnInit() {
    this.message = this.messageService.getMessage();
  }
}
```

---

### ✅ Summary

- `@Injectable({ providedIn: 'root' })` makes the service available globally.
- You inject the service into a component using the constructor.
- Then you can call its methods like `this.messageService.getMessage()`.

Would you like to see an example using **Observables** or **sharing data between components** using a service?
