## Create Read Update Delete

| NO. | TITLE |
| ----------- | ----------- |
| 1 | [Introduction](https://github.com/RazorPageTraining/Part_1_Introduction) |
| 2 | [Installation](https://github.com/RazorPageTraining/Part_2_Installation) |
| 3 | [Identity](https://github.com/RazorPageTraining/Part_3_Identity ) |
| 4 | [Net 5 CRUD](https://github.com/muhamaddarulhadi/RazorPage) ***(Example ONLY)***|
| 5 | [How to run this project](#how-to-run-this-project) |
| 6 | [Insert Library](#insert-library) |
| 7 | [CRUD code](#crud-code) </br> - [View](#view) </br> - [Insert](#insert) </br> - [Update](#update) </br> - [Delete](#delete) |


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

3. "TrainingRazor" name on this command is the folder name of the project. The name is depends on you.
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

   <div class="text-center col-md-12">
       <h1 class="display-3 mt-3">Training Razor</h1>
       <p>This Project consist of Create, Read, Update and Delete process.</p>

       <div align="left" class="mt-5 mb-4">
           <a asp-page="Manage" asp-page-handler="Insert" class="btn btn-primary">Insert</a>
       </div>
       <div class="table-responsive">
           <table id="data" class="display table table-striped table-bordered">
               <thead>
                   <tr>
                       <th class="text-center col-md-2">NO</th>
                       <th class="text-left col-md-5">PRODUCT NAME</th>
                       <th class="text-center col-md-3">PRODUCT QUANTITY</th>
                       <th class="text-center col-md-2">TOTAL PRICE</th>
                   </tr>
               </thead>
               <tbody>
                   @for(int i=0; i<Model.custProducts.Count(); i++)
                   {
                       <tr>
                           <td class="text-center"></td>
                           <td class="text-left">@Model.custProducts[i].Name</td>
                           <td class="text-center">@Model.custProducts[i].Quantity</td>
                           <td class="text-center">@Model.custProducts[i].TotalPrice</td>
                           <form id="product" method="post">
                               <td class="text-center">
                                   <a class="btn btn-link" asp-page="Manage" asp-page-handler="Update" asp-route-id="@Model.custProducts[i].Id"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                   <button class="btn btn-link text-danger" asp-page-handler="Delete" asp-route-id="@Model.custProducts[i].Id" type="submit"><i class="fa fa-trash" aria-hidden="true"></i></button>
                               </td>
                           </form>
                       </tr>
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

   //THIS VIEW CLASS MODEL
   public class IndexModel : BaseModel
   {
       private readonly ApplicationDbContext _context; //CONNECTION FOR DATABASE

       public IndexModel(ApplicationDbContext context,  UserManager<ApplicationUser> userManager)  : base(userManager) //REFER TO BASE CLASS MODEL
       {
           _context = context;
       }

       public List<CustProduct> custProducts { get; set; } //ENTITY VARIABLE DECLARATION

       public async Task<ActionResult> OnGet()
       {
           var currentUser = await GetCurrentUser();   //GET CURRENT USER FROM METHOD CLASS

           custProducts = new List<CustProduct>(); //CREATE NEW LIST

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
                   var custProduct = new CusProduct()
                   {
                       Id = data[i].Id,
                       Name = data[i].Product.Name,
                       Quantity = data[i].Quantity,
                       PrivacyModel = totalPrice,
                   };

                   custProducts.Add(custProduct);
               }
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
       public class CustProduct
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

10. You can see that there are no rows on the table because we still didn't insert the data; table inside database is still empty.

11. [Back to Menu](#create-read-update-delete)
