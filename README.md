## Create Read Update Delete

| NO. | TITLE |
| ----------- | ----------- |
| 1 | [Introduction](https://github.com/RazorPageTraining/Part_1_Introduction) |
| 2 | [Installation](https://github.com/RazorPageTraining/Part_2_Installation) |
| 3 | [Identity](https://github.com/RazorPageTraining/Part_3_Identity ) |
| 4 | [Net 5 CRUD](https://github.com/muhamaddarulhadi/RazorPage) ***(Example ONLY)***|
| 5 | [How to run this project](#how-to-run-this-project) |
| 6 | [Insert Library](#insert-library) |
| 7 | [Edit Login Partial](#edit-login-partial) |
| 8 | [CRUD code](#crud-code) </br> - [View](#view) </br> - [Insert](#insert) </br> - [Update](#update) </br> - [Delete](#delete) |


</BR>

***

#### How to run this project

1. Download this project and open your terminal or VS Code.
2. On your terminal type this command, but first you need to make sure you know where your project folder is. For my case, I put it on my desktop
   
   > ![image](https://user-images.githubusercontent.com/47632993/146669384-e9b4b021-077d-4de5-acc3-e8aced9863b2.png)
    
   ```console 
   code -r TrainingRazor
   ``` 
   > ![image](https://user-images.githubusercontent.com/47632993/204572344-4005b6a9-2cdc-468c-804a-6338a70b0972.png)

3. "TrainingRazor" name on this command is the folder name of the project. The name depends on you.
4. System will open the project on your Code Editor.
5. Other than that, by using VS Code, you can just drag project folder on VS Code and and trust the author.     
6. If VS Code authorize you to insert some assets, just click ***Yes/Accept***    
7. [Back to Menu](#create-read-update-delete)
</BR>

***

#### Insert Library

1. Before we start to code, we need to download library called [Datatable](https://datatables.net/download/), just use default setting when download Datatable. Other than that, we need to download library called [FontAwesome](https://fontawesome.com/v4.7/get-started/).

   > What is [Datatable](https://datatables.net/)?

   > What is [FontAwesome](https://fontawesome.com/v4.7/)?

2. Insert the Datatable library and FontAwesome inside folder lib on wwwroot folder.
   
   > ![image](https://user-images.githubusercontent.com/47632993/146663424-e2b4257e-0943-4ee4-8d0d-0519adc045d1.png)

3. After that insert this code 
    
   ```HTML+Razor 
   <link rel="stylesheet" type="text/css" href="~/lib/DataTables/datatables.min.css"/>
   <link rel="stylesheet" type="text/css" href="~/lib/font-awesome-4.7.0/css/font-awesome.min.css" />   
   ```
       
   inside _Layout.cshtml on
   
   > ![image](https://user-images.githubusercontent.com/47632993/146326780-1329954c-bae1-4b38-81ca-816f3ac477cc.png)
   
   > ![image](https://user-images.githubusercontent.com/47632993/146663387-86f2a3c5-4f82-4e3b-96b1-0f52eddd9dde.png)


4. Then, we need to insert this code
   
   ```HTML+Razor 
   <script src="~/lib/DataTables/datatables.min.js"></script>
   ``` 

   inside  _Layout.cshtml on
    
   > ![image](https://user-images.githubusercontent.com/47632993/146662388-b66cc6a4-0757-45d4-90c1-efd33cd24029.png)

5. [Back to Menu](#create-read-update-delete)
</BR>

***

### Edit Login Partial

1. We need to edit file ***_LoginPartial.cshtml*** inside folder Pages > Shared. This is because there is some error when redirect link to logout.

   > ![image](https://user-images.githubusercontent.com/47632993/206103306-489240e6-dd5f-4324-b6a7-18a7abba45c5.png)
   
2. Open ***_LoginPartial.cshtml*** and edit this line as below :

   From :
   > ![image](https://user-images.githubusercontent.com/47632993/206102829-aca3e6d2-0ac8-4be8-ba0e-fb1d9c90c2d4.png)
   
   TO :
   > ![image](https://user-images.githubusercontent.com/47632993/206102919-476978e0-18b9-4460-8a14-a898a815fd17.png)
   
   ```HTML+Razor
   <form class="form-inline" asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl='@Url.Page("/Account/Login", new { area = "Identity" })' method="post" >
   ```
3. Save the file.
4. [Back to Menu](#create-read-update-delete)
</BR>

***

### [CRUD](https://www.sumologic.com/glossary/crud/#:~:text=CRUD%20Meaning%3A%20CRUD%20is%20an,%2C%20read%2C%20update%20and%20delete.) Code


#### View

1. For this project, we gonna use page Index to show one table that consist all data from database table that we have created.
2. Open file Index.cshtml and replace the code :

   ```HTML+Razor
   @page "{handler?}/{id?}"
   @model IndexModel
   @{
       ViewData["Title"] = "Home page";
   }

   <div>
       <div class="text-center mt-4">
           <h1>Training Razor</h1>
           <p>This Project consist of Create, Read, Update and Delete process.</p>
       </div>

       <div align="left" class="mt-5 mb-4">
           <a asp-page="Manage" asp-page-handler="Insert" class="btn btn-primary">Insert</a>
       </div>
       
       @if(TempData["error"]!=null)
       {
           <div class="alert alert-danger d-flex align-items-center alert-dismissible fade show" role="alert">
               <i class="fa fa-exclamation-triangle text-danger" aria-hidden="true"></i>
               &emsp;
               <div>
                   <span>@TempData["error"]</span>
                   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
               </div>
           </div>
       }

       @if(TempData["success"]!=null)
       {
           <div class="alert alert-success d-flex align-items-center alert-dismissible fade show" role="alert">
               <i class="fa fa-check-circle text-success" aria-hidden="true"></i>
               &emsp;
               <div>
                   <span>@TempData["success"]</span>
                   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
               </div>
           </div>
       }
       
       <div class="table-responsive">
           <table id="data" class="display table table-striped table-bordered">
               <thead>
                   <tr>
                       @if(User.IsInRole("SystemAdmin"))
                       {
                           <th class="text-center col-md-2 dont-sort">NO</th>
                           <th class="text-center col-md-5 sort-this">PRODUCT NAME</th>
                           <th class="text-center col-md-3">PRICE</th>
                           <th class="text-center col-md-2 dont-sort"></th>
                       }
                       else if(User.IsInRole("Customer"))
                       {
                           <th class="text-center col-md-2 dont-sort">NO</th>
                           <th class="text-center col-md-5 sort-this">PRODUCT NAME</th>
                           <th class="text-center col-md-3">QUANTITY</th>
                           <th class="text-center col-md-2">TOTAL PRICE</th>
                           <th class="text-center col-md-2 dont-sort"></th>
                       }
                   </tr>
               </thead>
               <tbody>
                   @if(User.IsInRole("SystemAdmin"))
                   {
                       @for(int i=0; i<Model.products.Count(); i++)
                       {
                           <tr>
                               <td class="text-center"></td>
                               <td class="text-center">@Model.products[i].Name</td>
                               <td class="text-center">RM @string.Format("{0:F2}", (@Model.products[i].Price))</td>
                               <form id="product" method="post">
                                   <td class="text-center">
                                       <a class="btn btn-link" asp-page="Manage" asp-page-handler="Update" asp-route-id="@Model.products[i].Id"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                       <button class="btn btn-link text-danger" asp-page-handler="Delete" asp-route-id="@Model.products[i].Id" type="submit"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                   </td>
                               </form>
                           </tr>
                       }
                   }
                   else if(User.IsInRole("Customer"))
                   {
                       @for(int i=0; i<Model.custProducts.Count(); i++)
                       {
                           <tr>
                               <td class="text-center"></td>
                               <td class="text-center">@Model.custProducts[i].Name</td>
                               <td class="text-center">@Model.custProducts[i].Quantity</td>
                               <td class="text-center">RM @string.Format("{0:F2}", (@Model.custProducts[i].TotalPrice))</td>
                               <form id="product" method="post">
                                   <td class="text-center">
                                       <a class="btn btn-link" asp-page="Manage" asp-page-handler="Update" asp-route-id="@Model.custProducts[i].Id"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                       <button class="btn btn-link text-danger" asp-page-handler="Delete" asp-route-id="@Model.custProducts[i].Id" type="submit"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                   </td>
                               </form>
                           </tr>
                       }
                   }
               </tbody>
           </table>
       </div>
   </div>

   @section Scripts {
       <script src="~/js/main.js"></script>
   }
   ```
   
   
3. If you got an error, it is because we still didn't edit the code on view-model. Open Index.cshtml.cs and paste this code :
   
   
   ```C#
   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using Microsoft.AspNetCore.Identity;

   using Microsoft.EntityFrameworkCore;

   using TrainingRazor.Data;
   using TrainingRazor.Models;

   namespace TrainingRazor.Pages;

   //THIS VIEW VIEW-MODEL
   public class IndexModel : BaseModel
   {
       private readonly ApplicationDbContext _context; //CONNECTION FOR DATABASE

       public IndexModel(ApplicationDbContext context,  UserManager<ApplicationUser> userManager)  : base(userManager) //REFER TO BASE CLASS MODEL
       {
           _context = context;
       }

       public List<CustProductModel> custProducts { get; set; } //ENTITY VARIABLE DECLARATION
       public List<Product> products { get; set; } //ENTITY VARIABLE DECLARATION

       public async Task<ActionResult> OnGet()
       {
           if(User.IsInRole("Customer"))   //LOAD DATA BY USER ROLE
           {
               var currentUser = await GetCurrentUser();   //GET CURRENT USER FROM METHOD CLASS

               custProducts = new List<CustProductModel>(); //CREATE NEW LIST

               //GET DATA FROM DATABASE
               var data = await _context.CustPurchaseds.Include(x => x.Creator)    //INCLUDE OTHER TABLE
                                                       .Include(x => x.Product)    //INCLUDE OTHER TABLE
                                                       .Where(x => x.Creator == currentUser)   //GET DATA ONLY FROM CURRENT USER AUTHENTICATION
                                                       .ToListAsync();

               if(data!=null)  //CHECK AVAILABILITY OF DATA
               {
                   for(int i=0; i< data.Count(); i++)  //LOOP DATA
                   {
                       var totalPrice = data[i].Product.Price * data[i].Quantity;  //CALCULATE TOTAL PRICE

                       //INSERT DATA INTO LIST
                       var custProduct = new CustProductModel()
                       {
                           Id = data[i].Id,
                           Name = data[i].Product.Name,
                           Quantity = data[i].Quantity ?? 0,
                           TotalPrice = totalPrice,
                       };

                       custProducts.Add(custProduct);
                   }
               }
           }
           else if(User.IsInRole("SystemAdmin"))   //LOAD DATA BY USER ROLE
           {
               products = new List<Product>();     //CREATE NEW LIST
               products = await _context.Products.ToListAsync();   //GET DATA FROM DATABASE
           }

           return Page();
       }
   }


   //BASE CLASS MODEL -- THIS MODEL CAN BE CALL ON ANOTHER VIEW-MODEL
   public class BaseModel : PageModel
   {
       private readonly UserManager<ApplicationUser> _userManager;

       public BaseModel(UserManager<ApplicationUser> userManager)
       {
           _userManager = userManager;
       }

       //METHOD CLASS TO GET CURRENT USER INFORMATION
       protected async Task<ApplicationUser> GetCurrentUser()
       {
           return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name); 
       }
   }
   ```

4. After that, we need to create a new Entity Model file called ***InputModel.cs*** inside Models folder. 

   > ![image](https://user-images.githubusercontent.com/47632993/206087823-3e5bcb21-7b44-4441-b2c1-e030d0bfd4ba.png)
   
5. Copy and paste this code inside ***InputModel.cs*** file.

   ```C#
   namespace TrainingRazor.Models
   {
       public class CustProductModel
       {
           public int Id { get; set; }
           public string Name { get; set; }
           public int? Quantity { get; set; }
           public decimal? TotalPrice { get; set; }
       }
   }
   ```
   
6. You can see that all the errors are gone.
7. After that, create one file inside js folder and name it main.js

   > ![image](https://user-images.githubusercontent.com/47632993/206089633-1f73cef4-52e6-4e85-9c15-4e5bd0e4057b.png)

8. Copy and paste this code inside main.js and save the file.
    
   ```JavaScript
   (function ($)
   {
       var t = $('#data').DataTable({
           pageLength: 5,
           "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "SEMUA"]],
           "order": [[ $('th.sort-this').index(), 'desc' ]],
           "columnDefs": [ 
               {
                   "searchable": false,
                   "orderable": false,
                   "targets": "dont-sort"
               },
           ],
       });

       t.on( 'order.dt search.dt', function () {
           t.column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
               cell.innerHTML = i+1;
           } );
       }).draw(); 
   })(jQuery);
   ```

9. You can try run the web application first to see whether there is error or not. Click FN + F5 on your keyboard or just F5, depends on your keyboard.

10. You can see that there are no rows on the table when you login as Customer role because we still didn't insert the data; table inside database is still empty. Moreover, if you login as Admin role, you can see that the table shows list of product that you insert when you migrate database on [Part_3_Identity](https://github.com/RazorPageTraining/Part_3_Identity).

11. [Back to Menu](#create-read-update-delete)
</BR>

#### Insert

1. Now, we gonna create a new page call Manage.
2. Right click on folder Pages, click New C#, then, click Razor Page.
   
   > ![image](https://user-images.githubusercontent.com/47632993/206109703-a6d9f556-57cf-4ec7-bfe3-83f8b17ef87e.png)

3. Below alert will popup; insert ***Manage*** and click Enter button on keyboard.

   > ![image](https://user-images.githubusercontent.com/47632993/206387433-8106d269-a3ff-4094-887d-31f42cb21dca.png)
   
4. You can see on Pages folder, there are 2 new file created called Manage.cshtml ***(View)*** and Manage.cshtml.cs ***(View-Model)***

   > ![image](https://user-images.githubusercontent.com/47632993/206110357-0daed18c-50a7-4598-a94c-1cadbc76838a.png)

5. Open Manage.cshtml and replace with this code :

   ```HTML+Razor
   @page "{handler?}/{id?}"
   @model ManageModel
   @{
       ViewData["Title"] = "Manage";
   }

   <div>
       <div class="mt-4"> 
           <h1>
               Insert Data
           </h1>
       </div>
       <div class="mt-2">
           <form method="post">
               @if(User.IsInRole("Customer"))
               {
                   <div class="form-group mt-3">
                       <label asp-for="InputCustPurchasing.ProductId" class="col-form-label"></label>
                       <select asp-for="InputCustPurchasing.ProductId" class="form-control">
                           <option price="0.00" value="">Please Choose</option>
                           @foreach(var item in Model.products)
                           {
                               <option price="@item.Price" value="@item.Id">@item.Name</option>
                           }
                       </select>
                       <span asp-validation-for="InputCustPurchasing.ProductId" class="text-danger"></span>
                   </div>
                   <div class="form-group mt-3">
                       <label asp-for="InputCustPurchasing.Quantity" class="col-form-label"></label>
                       <input asp-for="InputCustPurchasing.Quantity" class="form-control">
                       <span asp-validation-for="InputCustPurchasing.Quantity" class="text-danger"></span>
                   </div>
                   <div class="form-group mt-3">
                       <label for="total-price" class="col-form-label">Total Price (RM)</label>
                       <span id="total-price" class="form-control">0.00</span>
                   </div>
               }
               else if(User.IsInRole("SystemAdmin"))
               {
                   <div class="form-group mt-3">
                       <label asp-for="InputProduct.Name" class="col-form-label"></label>
                       <input asp-for="InputProduct.Name" class="form-control">
                       <span asp-validation-for="InputProduct.Name" class="text-danger"></span>
                   </div>
                   <div class="form-group mt-3">
                       <label asp-for="InputProduct.Price" class="col-form-label"></label>
                       <input asp-for="InputProduct.Price" class="form-control">
                       <span asp-validation-for="InputProduct.Price" class="text-danger"></span>
                   </div>
               }

               <div class="form-group mt-4">
                   <a asp-page="/Index" class="btn btn-primary mr-3">Back</a>
                   <button type="submit" asp-page-handler="Save" class="btn btn-success ml-3">Save</button>
               </div>
           </form>
       </div>
   </div>

   @section Scripts {
       @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
        <script src="~/js/manage.js"></script>
   }
   ```

6. Then, open Manage.cshtml.cs and replace with this code :
   
   ```C#
   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Identity;
   using Microsoft.EntityFrameworkCore;

   using TrainingRazor.Data;
   using TrainingRazor.Models;

   namespace TrainingRazor.Pages
   {
       public class ManageModel : BaseModel   //REFER TO BASE CLASS MODEL  
       {
           private readonly ApplicationDbContext _context; //CONNECTION FOR DATABASE

           public ManageModel(ApplicationDbContext context,  UserManager<ApplicationUser> userManager)  : base(userManager)  //REFER TO BASE CLASS MODEL
           {
               _context = context;
           }

           [BindProperty]  //MUST BIND INPUT MODEL FOR DATA TO REMAINS WHEN FORM IS POST
           public InputCustPurchasingModel InputCustPurchasing { get; set; } //ENTITY VARIABLE DECLARATION

           [BindProperty]
           public InputProductModel InputProduct { get; set; } //ENTITY VARIABLE DECLARATION
           public List<Product> products { get; set; }

           public async Task<IActionResult> OnGetInsert()
           {
               if(User.IsInRole("Customer"))
               {
                   products = await _context.Products.ToListAsync();

                   InputCustPurchasing = new InputCustPurchasingModel()
               }
               else if(User.IsInRole("SystemAdmin"))
               {
                   InputProduct = new InputProductModel();
               }

               return Page();
           }

           public async Task<ActionResult> OnPostSave()
           {
               if(User.IsInRole("Customer"))
               {  
                   var currentUser = await GetCurrentUser(); //CALL METHOD FROM BaseModel
                   
                   //INSERT INPUT DATA FROM INPUT ENTITY MODEL, INSIDE DATABASE ENTITY MODEL
                   var custPurchased = new CustPurchased()
                   {
                       Creator = currentUser,
                       ProductId = InputCustPurchasing.ProductId,
                       Quantity = InputCustPurchasing.Quantity
                   };

                   await _context.CustPurchaseds.AddAsync(custPurchased); //ADD DATA
               }
               else if(User.IsInRole("SystemAdmin"))
               {
                   var product = new Product()
                   {
                       Name = InputProduct.Name,
                       Price = InputProduct.Price
                   };

                   await _context.Products.AddAsync(product);
               }

               await _context.SaveChangesAsync();  //SAVE DATA INTO DATABASE
               
               TempData["success"] = "Success to insert";

               return RedirectToPage("Index");  //REDIRECT SYSTEM TO PAGE INDEX
           }
       }
   }
   ```

7. Insert this code inside file ***InputModel.cs*** :
   
   Above this code ***namespace TrainingRazor.Models***
   
   ```C#
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;
   ```
   
   > ![image](https://user-images.githubusercontent.com/47632993/206382419-5ba1b787-5e06-4477-b2cd-1badc7ab3749.png)

   
   Below CustProductModel class :
   
   ```C#
   public class InputCustPurchasingModel
   {
       public ApplicationUser Creator { get; set; }

       [Required(ErrorMessage = "Please select Product")]
       [Display(Name = "Product")]
       public int? ProductId { get; set; }

       [Required(ErrorMessage = "Please insert Quantity")]
       [Range(1, 999999999999, ErrorMessage = "Insert at least 1 quantity")]
       [Display(Name = "Quantity")]
       public int Quantity { get; set; }
   }

   public class InputProductModel
   {
       [StringLength(500)]
       [Required(ErrorMessage = "Please insert product name")]
       [Display(Name = "Product Name")]
       public string Name { get; set; }

       [Column(TypeName = "decimal(18, 2)")]
       [Required(ErrorMessage = "Please insert product price")]
       [Display(Name = "Product Price (RM)")]
       public decimal Price { get; set; }
   }
   ```
   
   > ![image](https://user-images.githubusercontent.com/47632993/206382545-26ecedf0-eecc-4c60-843f-78c4339b5a20.png)

7. Do not run the project yet, now, we gonna create a new js file inside js folder called ***manage.js***
   
   > ![image](https://user-images.githubusercontent.com/47632993/206129985-8e5ce5c7-4fa3-4f78-ac87-dca44968a7cd.png)

8. Copy and paste this code inside file ***manage.js***
   
   ```JavaScript
   (function ($)
   {   
       // KEY UP FUNCTION; CODE WILL RUN WHEN YOU ENTER VALUE ON KEYBOARD
       $("#InputCustPurchasing_Quantity").keyup( function (e)
       {   
           Calculate();  //CALL METHOD
       });

       // CHANGE FUNCTION; CODE WILL RUN IF THERE ARE SOME CHANGES ON DROPDOWN
       $("#InputCustPurchasing_ProductId").change( function (e)
       {
           Calculate();  //CALL METHOD 
       });
         
       // BLUR FUNCTION; WHEN INPUT LOSE FOCUS, FIXED THE VALUE TO 2 DECIMAL PLACES  
       $("#InputProduct_Price").blur(function(){
           var value = parseFloat(this.value);
           $(this).val(value.toFixed(2));
       });

   })(jQuery);

   // CALCULATE FUNCTION
   function Calculate() 
   {
       // GET VALUE FROM ATTRIBUTE IN ID BY FINDING THE ELEMENT OPTION THAT IS SELECTED
       var price = parseFloat($("#InputCustPurchasing_ProductId").find('option:selected').attr('price'));  
       var totalPrice = parseFloat($("#InputCustPurchasing_Quantity").val() * price);

       $("#total-price").html(totalPrice.toFixed(2));  // SHOW BACK THE VALUE ON ELEMENT BY ID
   }
   ```

9. Save everything and run project, try insert data on page Manage by click the ***Insert*** button. If everything is okay, you can see the data inserted inside table/database.
   
   - Click ***Insert*** button
      
     > ![image](https://user-images.githubusercontent.com/47632993/206383891-8e7fe064-75de-46e6-b5f6-baac16667e6a.png)
     
   - Insert required information and click ***Save*** button
     
     > ![image](https://user-images.githubusercontent.com/47632993/206384079-7b606093-67fe-4dec-80f3-284ad2cd65b2.png)
   
   - You can see your data inside table
   
     > ![image](https://user-images.githubusercontent.com/47632993/206384393-e84ab4aa-f387-4f5b-a06d-06fa0086f984.png)

10. You also can try login on Admin account and try insert a new product.

11. [Back to Menu](#create-read-update-delete)
</BR>

#### Update

1. Insert this code on ***InputModel.cs*** file
   
   ```C#
   public int Id { get; set; }
   ```
   
   Insert on both entity model class like this and save the file :
   
   > ![image](https://user-images.githubusercontent.com/47632993/206885694-175b84b3-74d2-4f7e-8a9f-b8f7f8011120.png)
   
   > ![image](https://user-images.githubusercontent.com/47632993/206885711-3c37dba1-30a3-4c06-ae78-ad5a0ae1bebe.png)

2. Open ***Manage.cshtml.cs*** file and insert this code

   Insert between handler ***OnGetInsert()*** method and declaration of ***products***
   
   ```C#
   public bool IsUpdate { get; set; }  //VARIABLE BOOL TO KNOW IF THE CONDITIO IS UPDATE OR NOT

   // WILL BE RUN ON PAGE LOADING WITHOUT LOADING WITH HANDLER
   public async Task<IActionResult> OnGet()
   {
       IsUpdate = false;
       products = await _context.Products.ToListAsync();
       InputCustPurchasing = new InputCustPurchasingModel();
       InputProduct = new InputProductModel();

       return Page();
   }
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206886938-0c769ddf-1a1c-4111-8d4d-ed70b50e2505.png)

   
   Then, insert this code inside ***OnGetInsert()*** handler method
   
   ```C#
   IsUpdate = false;
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887058-2e66f023-ef2b-4543-9609-5201f0c41260.png)

   
   After that, insert this code below ***OnGetInsert()*** handler method
   
   ```C#
   public async Task<IActionResult> OnGetUpdate(int? id)
   {
      if(id != null)
      {
          IsUpdate = true;

          if(User.IsInRole("Customer"))
          {
              products = await _context.Products.ToListAsync();
              var purchased = await _context.CustPurchaseds.FirstOrDefaultAsync(x => x.Id == id);
              InputCustPurchasing = new InputCustPurchasingModel()
              {
                  Id = purchased.Id,
                  ProductId = purchased.ProductId,
                  Quantity = purchased.Quantity ?? 0
              };
          }
          else if(User.IsInRole("SystemAdmin"))
          {
              var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
              InputProduct = new InputProductModel()
              {
                  Id = product.Id,
                  Name = product.Name,
                  Price = product.Price
              };
          }
      }
      else
      {
          return RedirectToPage("/Index");
      }

      return Page();
   }
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206890580-2fdbaba4-fafa-43f8-8790-9fe9474d8303.png)
   
   
   Moreover, insert this code below ***OnPostSave()*** handler method
   
   ```C#
   public async Task<ActionResult> OnPostUpdate()
   {
        if(User.IsInRole("Customer"))
        {
             var purchased = await _context.CustPurchaseds.FirstOrDefaultAsync(x => x.Id == InputCustPurchasing.Id);
                
             if(purchased!=null)
             {
                  purchased.ProductId = InputCustPurchasing.ProductId;    //UPDATE INFORMATION
                  purchased.Quantity = InputCustPurchasing.Quantity;
             }
        }
        else if(User.IsInRole("SystemAdmin"))
        {
             var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == InputProduct.Id);

             if(product!=null)
             {
                  product.Name = InputProduct.Name;
                  product.Price = InputProduct.Price;
             }
        }

        await _context.SaveChangesAsync();  //SAVE DATA INTO DATABASE
        
        TempData["success"] = "Success to update";

        return RedirectToPage("Index");  //REDIRECT SYSTEM TO PAGE INDEX
   }
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887247-f144b5e5-236c-4f62-a06e-c9a719797006.png)


3. Open ***Manage.cshtml*** and insert/edit this code

   Change this code from
   
   ```HTML+Razor
   <h1>
       Insert Data
   </h1>
   ```
   
   to this
   
   ```HTML+Razor
   <h1>
      @if(Model.IsUpdate)
      {
          <span>Update</span>
      }
      else
      {
          <span>Insert</span>
      }
       Data
   </h1>
   ```
   
   
   After that, insert this code
   
   ```HTML+Razor
   <input type="hidden" asp-for="InputCustPurchasing.Id">
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887524-cf47a2ca-03be-4677-8cf1-9d70d1ab58f2.png)


   Moreover, insert this code
   
   ```HTML+Razor
    <input type="hidden" asp-for="InputProduct.Id">
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887573-100c5a38-e459-4bf4-9da4-6c8e91d99210.png)

   
   Futhermore, change this code from
   
   ```HTML+Razor
   <div class="form-group mt-4">
       <a asp-page="/Index" class="btn btn-primary mr-3">Back</a>
       <button type="submit" asp-page-handler="Save" class="btn btn-success ml-3">Save</button>
   </div>
   ```
   
   to this
   
   ```HTML+Razor
   <div class="form-group mt-4">
       <a asp-page="/Index" class="btn btn-primary mr-3">Back</a>

       @if(Model.IsUpdate)
       {
           <button type="submit" asp-page-handler="Update" class="btn btn-success ml-3">Update</button>
       }
       else
       {
           <button type="submit" asp-page-handler="Save" class="btn btn-success ml-3">Save</button>
       }
   </div>
   ```
   
   
3. Besides that, you need to edit ***manage.js*** for to running Calculate function when page Manage is loading. You can just insert ***Calculate();*** as below
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887709-aff807fe-c0ef-4fe2-bbb9-f64501046241.png)


4. You can save everything and run the project, from the list that you have on page Index, you can click the edit button
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887738-7b0951cf-9603-444e-8c46-1ba91608631e.png)

   after that, you can see the page redirect to page Manage, and data that you inserted is on input field
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887787-e4a180fa-2cfa-40ca-814a-4cf555324d78.png)

   try to change the input data and click the ***Update*** button
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887810-5265489e-5277-435a-9805-d276dd781c9f.png)

   system will redirect to page Index and you can see the data are changing
   
   > ![image](https://user-images.githubusercontent.com/47632993/206887857-5b2a3219-ed30-474f-8a10-967b79192d15.png)


5. [Back to Menu](#create-read-update-delete)
</BR>

#### Delete

1. Edit you ***Index.cshtml*** by inserting this code

   ```HTML+Razor
   onclick="return ConfirmDelete()"
   ```
   
   on this code
   
   > ![image](https://user-images.githubusercontent.com/47632993/206894840-f6dc35a2-6d19-43eb-a03a-5d55faee1ad8.png)


2. After that, insert this code on ***Index.cshtml.cs*** below ***OnGet()*** handler method
   
   ```C#
   public async Task<IActionResult> OnPostDelete(int? id)
   {
        if(id!=null)
        {
            var currentUser = await GetCurrentUser();

            if(User.IsInRole("Customer"))
            {
                var purchased = await _context.CustPurchaseds.Include(x => x.Creator)
                                                              .FirstOrDefaultAsync(x => x.Id == id);

                if(purchased!=null)
                {
                    if(purchased.Creator == currentUser)    //CHECK WHETHER CURRENTUSER IS THE ONE THAT PURCHASED IT
                    {
                        _context.CustPurchaseds.Remove(purchased);
                    }
                }
                else
                {
                    TempData["error"] = "Purchased not found";

                    return RedirectToPage();
                }
            }
            else if(User.IsInRole("SystemAdmin"))
            {
                var purchased = await _context.CustPurchaseds.ToListAsync();
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if(product!=null)
                {   
                    for(int i = 0; i < purchased.Count; i++)    //LOOP THROUGH PURCHASED TABLE TO SEE WHETHER PRODUCTS BEEN PURCHASED
                    {
                        if(product.Id != purchased[i].ProductId)
                        {
                            _context.Products.Remove(product);
                        }
                        else
                        {
                            TempData["error"] = "Product been purchased";

                            return RedirectToPage();
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["success"] = "Success to delete";
        }

        return RedirectToPage();
   }
   ```
   
   
3. Futhermore, open you ***main.js*** file and insert this code

   ```JavaScript
   function ConfirmDelete()
   {
       return confirm("Are you sure you want to delete?");
   }
   ```
   
   like this
   
   > ![image](https://user-images.githubusercontent.com/47632993/206895124-0f9e5a9f-9d96-4be7-8e06-57343ea82424.png)


4. Run your project and see what happen.


5. [Back to Menu](#create-read-update-delete)
</BR>
