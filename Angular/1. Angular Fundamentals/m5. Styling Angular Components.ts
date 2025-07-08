
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










<<h2>> NgStyle directive
  
