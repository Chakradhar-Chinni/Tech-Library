
<<h2>> Styling an Angular application
Output image: using global styles for button, product name

1. Every angular app will have global css file at /src/styles.css
2. styles insides this file will be applicabel to all components
3. styles.css
    few css is added
4. component.html
   bold class is  applied to Heads button
   red class is applied to <div class="name


  ##/src/styles.css
.bold{
  font-weight: bold;
}

.red{
  color:red;
}


## src/app/catalog/catalog.component.html
  
  <div class="container">
  <div class="filters">
    <a class="button bold" (click)="filter='Heads'">Heads</a> //modified
    <a class="button" (click)="filter='Arms'">Arms</a>
    <a class="button" (click)="filter='Torsos'">Torsos</a>
    <a class="button" (click)="filter='Bases'">Bases</a>
    <a class="button" (click)="filter=''">All</a>
  </div>

  <ul class="products">
    <li class="product-item" *ngFor = "let product of getFilteredProducts()" >
      <div class="product">
        <div class="product-details">
          <img [src]=" getImageUrl(product)" [alt]="product?.name" />
          <div class="product-info">
            <div class="name red"> {{ product.name}} </div> //modified
            <div class="description"> {{product.description}} </div>
            <div class="category">Part Type: {{product.category}} </div>
          </div>
        </div>
        <div class="price">
          <div *ngIf="product.discount===0">{{product.price | currency:'GBP' }}</div>
          <div *ngIf="product.discount >0"class="discount">
            {{(product.price *(1-product.discount)) | currency}}
          </div>
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>






  


<<h2>> Problem with global styles.css file
Output image:  global style 
in the output image, notice that buttons in catalog & site header are responding to css

In the catalog template, I want to apply styling to <a> tags, so below style is added to global styles.css file

a{
  font-weight: bold;
  font-style: italic;
}

but this style is  auto-applied to site-header component <a> tags since its a global file. 

To avoid this, Angular provides css encapsulation. where each component would have its own css file










                                       
<<h2>> CSS Encapsulation
output image: css encapsulation to catalog component

1. <a> tag is added to css file

## src\app\catalog\catalog.component.css
a{
  font-weight: bold;
  font-style: italic;
}

.container {
  display: flex;
  flex-direction: column;
}

.filters {
  display: flex;
  justify-content: space-between;
  padding: 25px 200px;
}

.filters button {
  width: 100px;
}

.products {
  margin: 0 100px;
  border-top: 2px solid #999;
}

.product-item {
  border-bottom: 2px solid #999;
}

/* Product Details */
.product {
  display: flex;
  justify-content: space-between;
  padding: 20px 25px;
}

.product .product-details {
  display: flex;
  align-items: center;
}

.product img {
  width: 125px;
}

.product .product-info {
  margin-left: 25px;
}

.product .name {
  font-size: 22px;
  font-weight: bold;
}

.product .description {
  margin-top: 3px;
  font-size: 18px;
}

.product .category {
  margin-top: 20px;
  color: #777;
}

.product .price {
  display: flex;
  flex-direction: column;
  font-size: 25px;
  justify-content: space-around;
  align-items: center;
  min-width: 190px;
  color: #555;
  border-left: 2px solid #aaa;
  margin-left: 50px;
}

.product .price button {
  padding: 10px;
  width: 100px;
}



<<h2>> NgClass directive

ngClass is built-in directive to adynamically apply CSS classes to HTML elements based on component logic
    Syntax:  <div [ngClass]="{'class-name': condition}"></div>

--------------------------------------------Sample Code Example--------------------------------------------
<!-- app.component.ts -->
export class AppComponent {
  isActive = true;
}

<!-- app.component.html -->
<div [ngClass]="{'active': isActive, 'inactive': !isActive}">
  This div is conditionally styled.
</div>

    <!-- app.component.css -->
.active {
  color: green;
}
.inactive {
  color: red;
}
----------------------------------------------------------------------------------------
ngClass normal approach doesnot use method implementation
ngClass method approach uses getdiscountedClasses for method implementation

## src\app\catalog\catalog.component.css
.strikethrough
{
  text-decoration: line-through;
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

          <div [ngClass]="{'strikethrough' : product.discount>0}"> //new
            {{product.price | currency }}
          </div>

          <!-- method approach --> //new
            <!-- <div [ngClass]="getdiscountedClasses(product)">
              {{product.price | currency }}
            </div> -->

          <div *ngIf="product.discount >0" class="discount">
            {{(product.price *(1-product.discount)) | currency}}
          </div>
          
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>


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

    //new
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

   getImageUrl(product : IProduct)
   {
    return '/assets/images/robot-parts/'+product.imageName;
   }

   getFilteredProducts()
   {
    return this.filter === ''
    ? this.products
    : this.products.filter((product) => product.category === this.filter)
   }
}















<<h2>> NgStyle directive
ngClass is built-in directive to adynamically apply inline CSS to HTML elements based on component logic

--------------------------------------------Sample Code Example--------------------------------------------

----------------------------------------------------------------------------------------
