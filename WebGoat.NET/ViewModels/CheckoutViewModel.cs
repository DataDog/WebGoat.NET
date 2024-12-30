// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebGoat.NET.Models;

namespace WebGoat.NET.ViewModels;

public class CheckoutViewModel
{
    [ValidateNever]
    public Cart Cart { get; set; }
    
    [ValidateNever]
    public Customer Customer { get; set; }
    
    [Required(ErrorMessage = "Company Name is required")]
    [Display(Name = "Company Name")]
    public required string CompanyName { get; init; }

    [Required(ErrorMessage = "Address is required")]
    [Display(Name = "Address")]
    public required string Address { get; init; }

    [Required(ErrorMessage = "City is required")]
    [Display(Name = "City")]
    public required string City { get; init; }

    [Required(ErrorMessage = "Postal Code is required")]
    [Display(Name = "Postal Code")]
    public required string PostalCode { get; init; }

    [Required(ErrorMessage = "Country is required")]
    [Display(Name = "Country")]
    public required string Country { get; init; }

    // Shipping Method
    [Required(ErrorMessage = "Shipping Provider is required")]
    [Display(Name = "Shipping Provider")]
    public required string ShippingProvider { get; init; }

    // Payment Information
    [Required(ErrorMessage = "Credit Card Number is required")]
    [CreditCard(ErrorMessage = "Invalid credit card number")]
    [Display(Name = "Credit Card Number")]
    public required string CreditCardNumber { get; init; }

    [Required(ErrorMessage = "Card Holder Name is required")]
    [Display(Name = "Card Holder Name")]
    public required string CardHolderName { get; init; }

    [Required(ErrorMessage = "Expiration Month is required")]
    [Range(1, 12, ErrorMessage = "Expiration Month must be between 1 and 12")]
    [Display(Name = "Expiration Month")]
    public required int ExpirationMonth { get; init; }

    [Required(ErrorMessage = "Expiration Year is required")]
    [Range(2024, 2100, ErrorMessage = "Expiration Year must be between 2024 and 2100")]
    [Display(Name = "Expiration Year")]
    public required int ExpirationYear { get; init; }

    [Required]
    [RegularExpression(@"\d{3,4}", ErrorMessage = "Invalid CVV")]
    public required string Cvv { get; init; }
}