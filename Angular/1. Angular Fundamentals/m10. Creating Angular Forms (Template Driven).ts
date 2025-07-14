<<h2>> Introduction

Two types:
1. Template Driven Forms - html based - simple to develop
2. Reactive Forms - javascript based - for handling complex things, provides unit testing
3. from _course-resources folder copied user component, it has all 4 files and a service file



  

<<h2>> Adding Template-driven forms to an application
Output Image: html sign in form


1. this is a html sign-in form without any interactions to angular
2. input params will be bound & submitted data will be wired up by component in next module


## src\app\user\sign-in\sign-in.component.html
<div class="container">
  <form class="form">
    <img class="logo" src="/assets/images/logo.png" />
    <div class="sign-in">Sign In</div>
    <div class="sub-text">to acquire awesome bots</div>
    <input
      name="email"
      placeholder="Email Address"
      type="text"
    />
    <input
      name="password"
      placeholder="Password"
      type="password"
    />
    <div class="buttons">
      <button type="submit"class="button cta">
        Sign In
      </button>
    </div>
  </form>
</div>







<<h2>> Binding to Form Controls with ngModel
Output image: ngModel for sign-in


Notes:
[] -square braces - component to template data binding
() - paranthesis -  template to component data binding
[()] - 2 way data binding 

1. user.model.ts
   - IUserCredentials is created
2. sign-in.component.ts
   - credentials of Type  IUserCredentials creates
  - data passed to credentials values will be populated in browser as 2 way binding is used
3. sign-in.component.html
   - <input> use [(ngModel)] to capture user data to ts file. 



  
## src\app\user\user.model.ts
export interface IUser {
  firstName: string;
  lastName: string;
  email: string;
  password?: string;
}

export interface IUserCredentials {
  email: string;
  password: string;
}




## src\app\user\sign-in\sign-in.component.ts
import { Component } from '@angular/core';
import { IUserCredentials } from '../user.model';

@Component({
  selector: 'bot-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent {
  credentials : IUserCredentials= {email:'',password:''};

  constructor() { }

}




## src\app\user\sign-in\sign-in.component.html
<div class="container">
  <form class="form">
    <img class="logo" src="/assets/images/logo.png" />
    <div class="sign-in">Sign In</div>
    <div class="sub-text">to acquire awesome bots</div>
    <input
      name="email"
      [(ngModel)]="credentials.email"
      placeholder="Email Address"
      type="text"
    />
    {{credentials.email}}
    <input
      name="password"
      [(ngModel)]="credentials.password"
      placeholder="Password"
      type="password"
    />
    <div class="buttons">
      <button type="submit"class="button cta">
        Sign In
      </button>
    </div>
  </form>
</div>










<<h2>> Submitting a template driven form


## src\app\user\sign-in\sign-in.component.html
<div class="container">
  <form class="form" (ngSubmit)="signIn()">
    <img class="logo" src="/assets/images/logo.png" />
    <div class="sign-in">Sign In</div>
    <div class="sub-text">to acquire awesome bots</div>
    <input
      name="email"
      [(ngModel)] = "credentials.email"
      placeholder="Email Address"
      type="text"
    />
    {{credentials.email}}
    <input
      name="password"
      [(ngModel)] = "credentials.password"
      placeholder="Password"
      type="password"
    />
    <div class="buttons">
      <button type="submit"class="button cta">
        Sign In
      </button>
    </div>
  </form>
</div>





## src\app\user\sign-in\sign-in.component.ts

import { Component } from '@angular/core';
import { IUserCredentials } from '../user.model';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'bot-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent
{
  credentials : IUserCredentials= {email:'',password:''};

  constructor(private userService: UserService,private router: Router) { }

  signIn(){
    this.userService.signIn(this.credentials).subscribe({
      next: () => this.router.navigate(['/catalog'])
    });
  }

}



## src\app\user\user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';

import { IUser, IUserCredentials } from './user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private user: BehaviorSubject<IUser | null>;

  constructor(private http: HttpClient) {
    this.user = new BehaviorSubject<IUser | null>(null);
  }

  getUser(): Observable<IUser | null> {
    return this.user;
  }

  signIn(credentials: IUserCredentials): Observable<IUser> {
    return this.http
      .post<IUser>('/api/sign-in', credentials)
      .pipe(map((user: IUser) => {
        this.user.next(user);
        return user;
      }));
  }

  signOut() {
    this.user.next(null);
  }
}

