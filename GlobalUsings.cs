// System
global using System.Text;


// Authentication
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using MoneyGo.Application.Common.Auth;
global using MoneyGo.Infrastructure.Auth;

global using MoneyGo.Application.Common;
global using MoneyGo.Application.Common.LoginUser;
global using MoneyGo.Application.Common.RegisterUser;

// CQRS, Mediator
global using MediatR;
global using Microsoft.AspNetCore.Mvc;

global using MoneyGo.Application.Customers.Commands.CustomerCommands;
global using MoneyGo.Application.Customers.Commands.DeleteCustomer;
global using MoneyGo.Application.Customers.Commands.UpdateCustomer;

global using MoneyGo.Application.Customers.DTOs;

global using MoneyGo.Application.Customers.Queries.GetCustomerById;
global using MoneyGo.Application.Customers.Queries.GetCustomersByUserId;

global using MoneyGo.Application.Transactions.Commands.AddCreditTransaction;
global using MoneyGo.Application.Transactions.Commands.AddPaymentTransaction;

global using MoneyGo.Application.Transactions.DTOs;

global using MoneyGo.Application.Transactions.Queries.GetBalanceById;
global using MoneyGo.Application.Transactions.Queries.GetTransactionsById;