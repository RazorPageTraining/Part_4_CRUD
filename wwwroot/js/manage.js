(function ($)
{   
    Calculate();
    
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