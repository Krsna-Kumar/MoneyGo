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