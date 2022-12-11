using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using TrainingRazor.Data;
using TrainingRazor.Models;

namespace TrainingRazor.Pages;

//THIS VIEW VIEW-MODEL
public class IndexModel : BaseModel  //REFER TO BASE CLASS MODEL
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
