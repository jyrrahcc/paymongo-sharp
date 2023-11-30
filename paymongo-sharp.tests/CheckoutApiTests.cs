﻿// MIT License
// 
// Copyright (c) 2023 Russell Camo (@russkyc)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Paymongo.Sharp;
using Paymongo.Sharp.Checkouts.Entities;
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Interfaces;

namespace paymongo_sharp.tests;

public class CheckoutApiTests
{
    private readonly IPaymongoClient _client;

    public CheckoutApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        var publicKey = Env.GetString("PUBLIC_KEY");
        
        _client = new PaymongoClient(publicKey: publicKey, secretKey: secretKey);
    }
    
    [Fact]
    async Task CreateCheckoutSessionWithMinimalDetails()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "item_name",
                    Quantity = 1,
                    Currency = Currency.Php,
                    Amount = 3500
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya
            }
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        Assert.NotNull(checkoutResult);
        Assert.NotEmpty(checkoutResult.CheckoutUrl);
        Assert.Equal(CheckoutStatus.Active,checkoutResult.Status);
        Assert.Equal(checkout.LineItems.Count(),checkoutResult.LineItems.Count());
        Assert.Equal(checkout.PaymentMethodTypes.Count(),checkoutResult.PaymentMethodTypes.Count());

    }
    
    [Fact]
    async Task CreateAndRetrieveCheckoutSessionWithMinimalDetails()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "item_name",
                    Quantity = 1,
                    Currency = Currency.Php,
                    Amount = 3500
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya
            }
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        Assert.NotNull(checkoutResult);
        Assert.NotEmpty(checkoutResult.CheckoutUrl);
        Assert.Equal(CheckoutStatus.Active,checkoutResult.Status);
        Assert.Equal(checkout.LineItems.Count(),checkoutResult.LineItems.Count());
        Assert.Equal(checkout.PaymentMethodTypes.Count(),checkoutResult.PaymentMethodTypes.Count());
        
        Checkout getCheckoutResult = await _client.Checkouts.RetrieveCheckoutAsync(checkoutResult.Id);
        
        // Assert
        Assert.NotNull(getCheckoutResult);
        Assert.NotEmpty(getCheckoutResult.CheckoutUrl);
        Assert.Equal(CheckoutStatus.Active,getCheckoutResult.Status);
        Assert.Equal(checkout.LineItems.Count(),getCheckoutResult.LineItems.Count());
        Assert.Equal(checkout.PaymentMethodTypes.Count(),getCheckoutResult.PaymentMethodTypes.Count());

    }
    
    [Fact]
    async Task CreateAndExpireCheckoutSessionWithMinimalDetails()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "item_name",
                    Quantity = 1,
                    Currency = Currency.Php,
                    Amount = 3500
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya
            }
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        Assert.NotNull(checkoutResult);
        Assert.NotEmpty(checkoutResult.CheckoutUrl);
        Assert.Equal(CheckoutStatus.Active,checkoutResult.Status);
        Assert.Equal(checkout.LineItems.Count(),checkoutResult.LineItems.Count());
        Assert.Equal(checkout.PaymentMethodTypes.Count(),checkoutResult.PaymentMethodTypes.Count());
        
        Checkout getCheckoutResult = await _client.Checkouts.ExpireCheckoutAsync(checkoutResult.Id);
        
        // Assert
        Assert.NotNull(getCheckoutResult);
        Assert.NotEmpty(getCheckoutResult.CheckoutUrl);
        Assert.Equal(CheckoutStatus.Expired,getCheckoutResult.Status);
        Assert.Equal(checkout.LineItems.Count(),getCheckoutResult.LineItems.Count());
        Assert.Equal(checkout.PaymentMethodTypes.Count(),getCheckoutResult.PaymentMethodTypes.Count());

        

    }
    
    [Fact]
    async Task CreateCheckoutSessionWithFullDetails()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Never gonna..",
            CancelUrl = "http://127.0.0.1",
            SuccessUrl = "http://127.0.0.1",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "Give You Up",
                    Images = new []
                    {
                        "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                    },
                    Quantity = 1000,
                    Currency = Currency.Php,
                    Amount = 100
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya,
                PaymentMethod.BillEase,
                PaymentMethod.Dob,
                PaymentMethod.GrabPay,
                PaymentMethod.DobUbp
            },
            Billing = new Billing
            {
                Name = "TestName",
                Email = "test@paymongo.com",
                Phone = "9063364572",
                Address = new Address
                {
                    Line1 = "TestAddress1",
                    Line2 = "TestAddress2",
                    PostalCode = "4506",
                    State = "TestState",
                    City = "TestCity",
                    Country = "PH"
                }
            },
            Metadata = new CheckoutMetadata
            {
                Notes = "TestNotes",
                CustomerNumber = "9063364572",
                Remarks = "TestRemarks"
            },
            SendEmailReceipt = true,
            ShowDescription = true,
            ShowLineItems = true
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        Assert.NotNull(checkoutResult);
        Assert.Equivalent(checkout.LineItems,checkoutResult.LineItems, true);
        Assert.Equivalent(checkout.Billing,checkoutResult.Billing, true);
        Assert.Equivalent(checkout.Metadata,checkoutResult.Metadata, true);
        Assert.Equal(CheckoutStatus.Active,checkoutResult.Status);

    }
    
    [Fact]
    async Task CreateAndRetrieveCheckoutSessionWithFullDetails()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            CancelUrl = "http://127.0.0.1",
            SuccessUrl = "http://127.0.0.1",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "Give You Up",
                    Images = new []
                    {
                        "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                    },
                    Quantity = 1000,
                    Currency = Currency.Php,
                    Amount = 100
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya,
                PaymentMethod.BillEase,
                PaymentMethod.Dob,
                PaymentMethod.GrabPay,
                PaymentMethod.DobUbp
            },
            Billing = new Billing
            {
                Name = "TestName",
                Email = "test@paymongo.com",
                Phone = "9063364572",
                Address = new Address
                {
                    Line1 = "TestAddress1",
                    Line2 = "TestAddress2",
                    PostalCode = "4506",
                    State = "TestState",
                    City = "TestCity",
                    Country = "PH"
                }
            },
            Metadata = new CheckoutMetadata
            {
                Notes = "TestNotes",
                CustomerNumber = "9063364572",
                Remarks = "TestRemarks"
            },
            SendEmailReceipt = true,
            ShowDescription = true,
            ShowLineItems = false
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        // Assert
        Assert.NotNull(checkoutResult);
        Assert.Equivalent(checkout.LineItems,checkoutResult.LineItems, true);
        Assert.Equivalent(checkout.Billing,checkoutResult.Billing, true);
        Assert.Equivalent(checkout.Metadata,checkoutResult.Metadata, true);
        Assert.Equal(CheckoutStatus.Active,checkoutResult.Status);
        
        Checkout getCheckoutResult = await _client.Checkouts.RetrieveCheckoutAsync(checkoutResult.Id);
        
        // Assert
        Assert.NotNull(getCheckoutResult);
        Assert.Equivalent(checkout.LineItems,getCheckoutResult.LineItems, true);
        Assert.Equivalent(checkout.Billing,getCheckoutResult.Billing, true);
        Assert.Equivalent(checkout.Metadata,getCheckoutResult.Metadata, true);
        Assert.Equal(CheckoutStatus.Active,getCheckoutResult.Status);

    }
    
    [Fact]
    async Task CreateAndExpireCheckoutSessionWithFullDetails()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            CancelUrl = "http://127.0.0.1",
            SuccessUrl = "http://127.0.0.1",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "Give You Up",
                    Images = new []
                    {
                        "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                    },
                    Quantity = 1000,
                    Currency = Currency.Php,
                    Amount = 100
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya,
                PaymentMethod.BillEase,
                PaymentMethod.Dob,
                PaymentMethod.GrabPay,
                PaymentMethod.DobUbp
            },
            Billing = new Billing
            {
                Name = "TestName",
                Email = "test@paymongo.com",
                Phone = "9063364572",
                Address = new Address
                {
                    Line1 = "TestAddress1",
                    Line2 = "TestAddress2",
                    PostalCode = "4506",
                    State = "TestState",
                    City = "TestCity",
                    Country = "PH"
                }
            },
            Metadata = new CheckoutMetadata
            {
                Notes = "TestNotes",
                CustomerNumber = "9063364572",
                Remarks = "TestRemarks"
            },
            SendEmailReceipt = true,
            ShowDescription = true,
            ShowLineItems = false
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        Assert.NotNull(checkoutResult);
        Assert.Equivalent(checkout.LineItems,checkoutResult.LineItems, true);
        Assert.Equivalent(checkout.Billing,checkoutResult.Billing, true);
        Assert.Equivalent(checkout.Metadata,checkoutResult.Metadata, true);
        Assert.Equal(CheckoutStatus.Active,checkoutResult.Status);
        
        Checkout getCheckoutResult = await _client.Checkouts.ExpireCheckoutAsync(checkoutResult.Id);
        
        // Assert
        Assert.NotNull(getCheckoutResult);
        Assert.Equivalent(checkout.LineItems,getCheckoutResult.LineItems, true);
        Assert.Equivalent(checkout.Billing,getCheckoutResult.Billing, true);
        Assert.Equivalent(checkout.Metadata,getCheckoutResult.Metadata, true);
        Assert.Equal(CheckoutStatus.Expired,getCheckoutResult.Status);

    }
}