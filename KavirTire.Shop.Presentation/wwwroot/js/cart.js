const MaximumNumberOfPurchases = $("#MaximumNumberOfPurchases").val();
const ApplyMaximumNumberOfPurchases = $("#ApplyMaximumNumberOfPurchases").val();
const NumberOfPurchaseItems = $("#NumberOfPurchaseItems").val();
const ApplyNumberOfPurchaseItems = $("#ApplyNumberOfPurchaseItems").val();
const ApplyPurchaseInterval = $("#ApplyPurchaseInterval").val();
const CustomerPreviousPurchaseCountInPurchaseInterval = $("#CustomerPreviousPurchaseCountInPurchaseInterval").val();


const TirePostCost = $("#TirePostCost").val();

$(document).ready(function () {
    updateView();
});

function retrieveCart() {
    const cartItemsString = localStorage.getItem("cart");
    if (!cartItemsString) {
        return {
            cartItems: [],
            CartItemsPrice: 0,
            TirePostCost: 0,
            TotalPrice: 0,
            TotalQuantity: 0,
        };
    }
    return JSON.parse(cartItemsString);
}

function updateCart(cart) {
    var cartItemsPrice = 0;
    var tirePostCost = 0;
    var total = 0;
    var totalQuantity = 0;

    if (cart.cartItems.length > 0) {
        cart.cartItems.forEach((item) => {
            cartItemsPrice += item.Quantity * item.Price;
            totalQuantity += item.Quantity;
        });
        tirePostCost = TirePostCost * totalQuantity;
        total = cartItemsPrice + tirePostCost;
    }
    cart.CartItemsPrice = cartItemsPrice;
    cart.TirePostCost = tirePostCost;
    cart.TotalPrice = total;
    cart.TotalQuantity = totalQuantity;

    localStorage.setItem("cart", JSON.stringify(cart));
    updateView();
}

function updateView() {
    var cart = retrieveCart();
    if (cart == null || cart.cartItems.length <= 0) {
        $("#empty-basket").show();
        $("#basket-container").hide();
    } else {
        $("#empty-basket").hide();
        $("#basket-container").show();
        $("#cart-total-count").text(cart.cartItems.length);
        $("#cart-total-price").text(cart.CartItemsPrice.toLocaleString());
        $("#delivery-fee").text(cart.TirePostCost.toLocaleString());
        $("#total-price").text(cart.TotalPrice.toLocaleString());
        $("#basket-items").empty();
        cart.cartItems.forEach((item) => {
            const basketItem = `<div class="basket-item" data-cart-item-id="${item.Id}">
                        <div  class="del" title="حذف از سبد خرید">
                            <span class="idn-remove remove-cart-item"></span>
                        </div>
                        <div class="basket-item-title">${item.Title}</div>
                        <div class="d-flex align-items-center justify-content-between">
                        <div class="basket-item-price col-md-7 col-xs-5">
                            <span class="item-price">${item.Price.toLocaleString()}</span> <span>ريال</span>
                        </div>
                        <div class="basket-item-action col-md-5 col-xs-7">
                            <span class="inc">+</span>
                            <span class="quantity">${item.Quantity}</span>
                            <span class="dec">-</span>
                        </div>
                        </div>
                    </div>`;
            $("#basket-items").append(basketItem);
        });
    }


}

$(".add-to-cart").on("click", function () {
    const Id = $(this).data("id");
    const Title = $(this).data("title");
    const Price = $(this).data("price");

    const cart = retrieveCart();
    const existingCartItem = cart.cartItems.find(c => c.Id === Id);
    if (existingCartItem != null) {
        incrementCartItem(existingCartItem)
    } else {
        addCartItem(cart, {
            Id: Id,
            Title: Title,
            Price: Price,
            Quantity: 1
        });
    }
    updateCart(cart);
});
$('body').on('click', '.inc', function () {
    const Id = $(this).closest('.basket-item').data("cart-item-id");
    const cart = retrieveCart();
    const existingCartItem = cart.cartItems.find(c => c.Id === Id);
    if (existingCartItem != null) {
        incrementCartItem(existingCartItem);
        updateCart(cart);
    }
})
$('body').on('click', '.dec', function () {
    const Id = $(this).closest('.basket-item').data("cart-item-id");
    const cart = retrieveCart();
    const existingCartItem = cart.cartItems.find(c => c.Id === Id);
    if (existingCartItem != null) {
        if (existingCartItem.Quantity > 1) {
            existingCartItem.Quantity -= 1;
        } else {
            cart.cartItems = cart.cartItems.filter(c => c.Id !== existingCartItem.Id);
        }
        updateCart(cart);
    }
})
$('body').on('click', '.remove-cart-item', function () {
    const Id = $(this).closest('.basket-item').data("cart-item-id");
    const cart = retrieveCart();
    const existingCartItem = cart.cartItems.find(c => c.Id === Id);
    if (existingCartItem != null) {
        cart.cartItems = cart.cartItems.filter(c => c.Id !== existingCartItem.Id);
        updateCart(cart);
    }
})

function addCartItem(cart, cartItem) {
    if (ApplyNumberOfPurchaseItems === "0" || (ApplyNumberOfPurchaseItems === "1" && NumberOfPurchaseItems > cart.cartItems.length)) {
        if (validateUserPurchaseLimit()) {
            cart.cartItems.push(cartItem);
        }
    } else {
        toastr.error(`حداکثر تعداد محصولات سبد خرید ${NumberOfPurchaseItems} عدد میباشد`)
    }
}

function incrementCartItem(cartItem) {
    if (ApplyMaximumNumberOfPurchases === "0" || (ApplyMaximumNumberOfPurchases === "1" && cartItem.Quantity < MaximumNumberOfPurchases)) {
        if (validateUserPurchaseLimit()) {
            cartItem.Quantity += 1;
        }
    } else {
        toastr.error(`حداکثر تعداد اقلام یک محصول ${MaximumNumberOfPurchases} عدد میباشد`)
    }
    return cartItem;
}

function validateUserPurchaseLimit() {
    const cart = retrieveCart();
    var allowedPurchaseQuantityForCustomer = MaximumNumberOfPurchases - CustomerPreviousPurchaseCountInPurchaseInterval;
    if (ApplyPurchaseInterval === "0" || (ApplyPurchaseInterval === "1" && cart.TotalQuantity < allowedPurchaseQuantityForCustomer)) {
        return true;
    } else {
        if (allowedPurchaseQuantityForCustomer === 0) {
            toastr.error(`در حال حاضر خرید محصول برای شما ممکن نیست. جهت کسب اطلاعات بیشتر به حساب کاربری خود مراجعه کنید.`)
        } else {
            toastr.error(`حداکثر تعداد محصولات سبد خرید شما ${allowedPurchaseQuantityForCustomer} عدد میباشد`)
        }
        return false;
    }
}


$("#submit-cart-btn").on("click", function () {
    $(this).prop("disabled",true);
    $(this).addClass("loading");
    
    const cart = retrieveCart();
    let data = {
        Products:[]
    };
    cart.cartItems.forEach((c) => data.Products.push(
        {
            ProductId: c.Id,
            Quantity: c.Quantity
        }
    ));
    $.ajax({
        url: '/SubmitCart',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (result) {
         location.href = `/purchase-summary/${result.invoiceId}`;
        },
        error: function (xhr, status, error) {
            
            toastr.error(xhr.responseJSON?.detailedMessage ?? "خطای سیستمی");
            $("#submit-cart-btn").prop("disabled",false);
            $("#submit-cart-btn").removeClass("loading");
        }
    });
});
