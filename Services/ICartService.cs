﻿

using CGullProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CGullProject.Services
{
    public interface ICartService
    {

        public Task<bool> AddItemToCart(Guid cartId, string itemId, int quantity);

        /// <summary>
        /// Get the cart instance, as well as a list of tuples that show all items in cart (by ID), the quantity of ea. respective item,
        /// and their respective running total.
        /// </summary>
        /// <param name="cartId"> The ID of the cart whose relevant data the controller is requesting </param>
        /// <returns></returns>
        public Task<Tuple<Cart, IEnumerable<CartItemView>>> GetCart(Guid cartId);

        public Task<TotalsDTO> GetTotals(Guid cartId);

        public Task<bool> ProcessPayment(Guid cartId, String cardNumber, DateOnly exp, string cardHolderName, string cvc);

        public Task<Guid> CreateNewCart(String cartName);
    }

    public enum ServiceResult
    {
        OK,

    }
}
