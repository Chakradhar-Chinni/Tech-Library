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
