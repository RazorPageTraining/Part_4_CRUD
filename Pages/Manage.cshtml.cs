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

        public async Task<IActionResult> OnGetInsert()
        {
            IsUpdate = false;

            if(User.IsInRole("Customer"))
            {
                products = await _context.Products.ToListAsync();

                InputCustPurchasing = new InputCustPurchasingModel();
            }
            else if(User.IsInRole("SystemAdmin"))
            {
                InputProduct = new InputProductModel();
            }

            return Page();
        }

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
    }
}