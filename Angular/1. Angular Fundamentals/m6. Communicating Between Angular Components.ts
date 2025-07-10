<<h2>> Introduction

Lets understand how components talk to each other. Parent Child Communication and vice versa


  
<<h2>> Communicating with Child Components Using Inputs

flow: 
s1. user click on any filter button
s2. catalog.component.ts reads the input to 'filter'  
s3. angular change detection runs and executes getFilteredProducts() in catalog.component.html
s4. product details are sent to product-details.ts file. notice @Input()  in product-details.ts. data is injected to this from below line in catalog.component.html
      <bot-product-details [product] = "product"></bot-product-details>
  

  1. create new component ng g c product-details (it creates 4 files)
  2. <div class="product-details"> move this class to product-details.component.html from catalog.component.html
  3. product-details.component.html 
      button click calls addtocart() which is added in catalog.component.ts file
  
    

## src\app\catalog\catalog.component.ts
  import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';

@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   products: IProduct[];
   filter : string = '';
   cart : IProduct[] = []; //new

   constructor()
   {
    this.products =
    [
      {
        id: 1,
        description:
          "A robot head with an unusually large eye and teloscpic neck -- excellent for exploring high spaces.",
        name: "Large Cyclops",
        imageName: "head-big-eye.png",
        category: "Heads",
        price: 1220.5,
        discount: 0.2,
      },
      {
        id: 17,
        description: "A spring base - great for reaching high places.",
        name: "Spring Base",
        imageName: "base-spring.png",
        category: "Bases",
        price: 1190.5,
        discount: 0,
      },
      {
        id: 6,
        description:
          "An articulated arm with a claw -- great for reaching around corners or working in tight spaces.",
        name: "Articulated Arm",
        imageName: "arm-articulated-claw.png",
        category: "Arms",
        price: 275,
        discount: 0,
      },
      {
        id: 2,
        description:
          "A friendly robot head with two eyes and a smile -- great for domestic use.",
        name: "Friendly Bot",
        imageName: "head-friendly.png",
        category: "Heads",
        price: 945.0,
        discount: 0.2,
      },
      {
        id: 3,
        description:
          "A large three-eyed head with a shredder for a mouth -- great for crushing light medals or shredding documents.",
        name: "Shredder",
        imageName: "head-shredder.png",
        category: "Heads",
        price: 1275.5,
        discount: 0,
      },
      {
        id: 16,
        description:
          "A single-wheeled base with an accelerometer capable of higher speeds and navigating rougher terrain than the two-wheeled variety.",
        name: "Single Wheeled Base",
        imageName: "base-single-wheel.png",
        category: "Bases",
        price: 1190.5,
        discount: 0.1,
      },
      {
        id: 13,
        description: "A simple torso with a pouch for carrying items.",
        name: "Pouch Torso",
        imageName: "torso-pouch.png",
        category: "Torsos",
        price: 785,
        discount: 0,
      },
      {
        id: 7,
        description:
          "An arm with two independent claws -- great when you need an extra hand. Need four hands? Equip your bot with two of these arms.",
        name: "Two Clawed Arm",
        imageName: "arm-dual-claw.png",
        category: "Arms",
        price: 285,
        discount: 0,
      },

      {
        id: 4,
        description: "A simple single-eyed head -- simple and inexpensive.",
        name: "Small Cyclops",
        imageName: "head-single-eye.png",
        category: "Heads",
        price: 750.0,
        discount: 0,
      },
      {
        id: 9,
        description:
          "An arm with a propeller -- good for propulsion or as a cooling fan.",
        name: "Propeller Arm",
        imageName: "arm-propeller.png",
        category: "Arms",
        price: 230,
        discount: 0.1,
      },
      {
        id: 15,
        description: "A rocket base capable of high speed, controlled flight.",
        name: "Rocket Base",
        imageName: "base-rocket.png",
        category: "Bases",
        price: 1520.5,
        discount: 0,
      },
      {
        id: 10,
        description: "A short and stubby arm with a claw -- simple, but cheap.",
        name: "Stubby Claw Arm",
        imageName: "arm-stubby-claw.png",
        category: "Arms",
        price: 125,
        discount: 0,
      },
      {
        id: 11,
        description:
          "A torso that can bend slightly at the waist and equiped with a heat guage.",
        name: "Flexible Gauged Torso",
        imageName: "torso-flexible-gauged.png",
        category: "Torsos",
        price: 1575,
        discount: 0,
      },
      {
        id: 14,
        description: "A two wheeled base with an accelerometer for stability.",
        name: "Double Wheeled Base",
        imageName: "base-double-wheel.png",
        category: "Bases",
        price: 895,
        discount: 0,
      },
      {
        id: 5,
        description:
          "A robot head with three oscillating eyes -- excellent for surveillance.",
        name: "Surveillance",
        imageName: "head-surveillance.png",
        category: "Heads",
        price: 1255.5,
        discount: 0,
      },
      {
        id: 8,
        description: "A telescoping arm with a grabber.",
        name: "Grabber Arm",
        imageName: "arm-grabber.png",
        category: "Arms",
        price: 205.5,
        discount: 0,
      },
      {
        id: 12,
        description: "A less flexible torso with a battery gauge.",
        name: "Gauged Torso",
        imageName: "torso-gauged.png",
        category: "Torsos",
        price: 1385,
        discount: 0,
      },
      {
        id: 18,
        description:
          "An inexpensive three-wheeled base. only capable of slow speeds and can only function on smooth surfaces.",
        name: "Triple Wheeled Base",
        imageName: "base-triple-wheel.png",
        category: "Bases",
        price: 700.5,
        discount: 0,
      },
    ];
   }

  
   addToCart(product: IProduct) //new
   {
    this.cart.push(product);
    console.log(`product ${product.name} added to cart`);
   }

   getdiscountedClasses(product : IProduct)
   {
    return{ strikethrough:product.discount>0 };
        /*
        if(product.discount > 0)
            {
              return 'strikethrough';
            }
            else
            {
              return '';
            }
        */
   }



   getFilteredProducts()
   {
    return this.filter === ''
    ? this.products
    : this.products.filter((product) => product.category === this.filter)
   }
}




## src\app\catalog\catalog.component.html
<div class="container">
  <div class="filters">
    <a class="button " (click)="filter='Heads'">Heads</a>
    <a class="button" (click)="filter='Arms'">Arms</a>
    <a class="button" (click)="filter='Torsos'">Torsos</a>
    <a class="button" (click)="filter='Bases'">Bases</a>
    <a class="button" (click)="filter=''">All</a>
  </div>

  <ul class="products">
    <li class="product-item" *ngFor = "let product of getFilteredProducts()" >
      <bot-product-details [product] = "product"></bot-product-details> //new
    </li>
  </ul>
</div>

  


## src\app\product-details\product-details.component.ts
  import { Component, Input} from '@angular/core';
import {IProduct } from '../catalog/product.model';

@Component({
  selector: 'bot-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent  {

  @Input() product! : IProduct; //new

  getImageUrl(product : IProduct) //new
   {
    return '/assets/images/robot-parts/'+product.imageName;
   }

   addToCart(product: IProduct) //new
   {

   }
}


## src\app\product-details\product-details.component.html //new
  <div class="product">
        <div class="product-details">
          <img [src]=" getImageUrl(product)" [alt]="product?.name" />
          <div class="product-info">
            <div class="name"> {{ product.name}} </div>
            <div class="description"> {{product.description}} </div>
            <div class="category">Part Type: {{product.category}} </div>
          </div>
        </div>
        
        <div class="price">
          <div [ngStyle]="{color: product.discount>0 ? 'red':''}"
                [ngClass]="{strikethrough: product.discount>0}">
            {{product.price | currency}}
          </div>

          <div *ngIf="product.discount >0" class="discount">
            {{(product.price *(1-product.discount)) | currency}}
          </div>

          <button class="cta" (click)="addToCart(product)">Buy</button>
        </div>
</div>














            

<<h2>> Communicating with Parent Components Using Outputs - Quick Example

1. Child.Component.ts
    - messageEvent is decorated with @Output indicates that data will be sent to parent component
    - new EventEmitter<string>() EventEmitter is a class that allows you to emit custom events like <string> <int> <object>
2. Parent.component.ts
    - "receiveMessage($event)" $event is a special variable that represents the data emitted by an event
    - $event contains the value emitted by the child — in this case, a string like "Hello from Child!".

    


##  child.component.ts
import { Component, Output, EventEmitter } from '@angular/core';
@Component({
  selector: 'app-child',
  template: `
    <button (click)="sendMessage()">Send Message to Parent</button>
  `
})
export class ChildComponent {
  @Output() messageEvent = new EventEmitter<string>();

  sendMessage() {
    this.messageEvent.emit('Hello from Child!');
  }
}


##  parent.component.ts
import { Component } from '@angular/core';
@Component({
  selector: 'app-parent',
  template: `
    <app-child (messageEvent)="receiveMessage($event)"></app-child>
    <p>Message from child: {{ message }}</p>
  `
})
export class ParentComponent {
  message = '';

  receiveMessage(msg: string) {
    this.message = msg;
  }
}
