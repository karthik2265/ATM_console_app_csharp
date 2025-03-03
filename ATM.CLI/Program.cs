﻿using ATM.Models;
using ATM.Models.enums;
using ATM.Services;
using ATM.Services.DataModels;
using System;
using System.Linq;

namespace ATM.CLI
{
    internal class Program
    {

        static void Main()
        {

            //       1) setup a new bank
            //       2) staff login
            //       3) customer login

            ConsoleOutput.Welcome();
            ConsoleOutput.Mainmenu();


            Mainmenu option = (Mainmenu)(Convert.ToInt32(TakeUserInput.Input()));


            if (option == Mainmenu.SetupBank)
            {
                SetupBank();
            }

            if (option == Mainmenu.CustomerLogin)
            {
                CustomerLogin();
            }

            if (option == Mainmenu.StaffLogin)
            {
                Console.WriteLine("calling staff login method");
                StaffLogin();
            }

        }

        public static void Deposit(BankService manager, Customer customer, Customer BankSelfAccount, SQLService sqLService)
        {
            string depositInput = TakeUserInput.DepositAmount();
            InputValidation status = InputValidator.IsDepositable(depositInput);
            if (status == InputValidation.Success)
            {
                double depositAmount = Convert.ToDouble(depositInput);
                customer.Balance += depositAmount;
                sqLService.UpdateCustomerField(customer, "balance", Convert.ToString(customer.Balance));
                ConsoleOutput.SuccesfullyDeposited(depositAmount);
                manager.AddTransaction(customer, BankSelfAccount, depositAmount, TransactionType.Deposit, sqLService);
                //manager.DepositAmount(customer, depositAmount);
            }
            else if (status == InputValidation.InsufficientBalance) ConsoleOutput.InSufficientBalance(manager.GetBalance(customer));
            else ConsoleOutput.InvalidInput();
        }

        //public static void Withdraw(BankService manager, Customer customer, Customer bankSelfAccount)
        //{
        //    string withdrawInput = TakeUserInput.WithdrawAmount();
        //    InputValidation status = InputValidator.IsValidAmount(withdrawInput, manager.GetBalance(customer));
        //    if (status == InputValidation.Success)
        //    {
        //        double withdrawAmount = Convert.ToDouble(withdrawInput);
        //        ConsoleOutput.SuccesfullyWithdrawn(withdrawAmount);
        //        manager.AddTransaction(bankSelfAccount, customer, withdrawAmount, TransactionType.Withdraw);
        //        manager.WithdrawAmount(customer, withdrawAmount);
        //    }
        //    else if (status == InputValidation.InsufficientBalance) ConsoleOutput.InSufficientBalance(manager.GetBalance(customer));
        //    else ConsoleOutput.InvalidInput();
        //}

        //public static void Transfer(BankService manager, Customer sender)
        //{
        //    string recieverName = TakeUserInput.RecieverName();
        //    string recieverAccountId = TakeUserInput.RecieverAccountId();
        //    InputValidation status;
        //    status = InputValidator.UserExists(manager, recieverName, recieverAccountId);
        //    // if reciever exists
        //    if (status == InputValidation.Success)
        //    {
        //        // check if sender entered a valid amount to send to reciever
        //        Customer reciever = manager.GetCustomer(recieverName);
        //        string transferInput = TakeUserInput.AmountToTransfer();
        //        status = InputValidator.IsValidAmount(transferInput, manager.GetBalance(sender));
        //        if (status == InputValidation.Success)
        //        {
        //            double transferAmount = Convert.ToDouble(transferInput);
        //            ConsoleOutput.SuccessfulTransfer(transferAmount, recieverName);
        //            manager.AddTransaction(sender, reciever, transferAmount, TransactionType.Debit);
        //            manager.AddTransaction(reciever, sender, transferAmount, TransactionType.Credit);
        //            manager.TransferAmount(sender, reciever, transferAmount);
        //        }
        //        else if (status == InputValidation.InsufficientBalance) ConsoleOutput.InSufficientBalance(manager.GetBalance(sender));
        //        else ConsoleOutput.InvalidInput();
        //    }
        //    // if reciever doesnt exist
        //    else
        //    {
        //        ConsoleOutput.UserDoesntExist();
        //    }
        //}

        public static void CreateAccountForCustomer(BankService manager, SQLService sqlService)
        {
            string customerName = TakeUserInput.UserName();
            string password = TakeUserInput.Password();
            manager.AddAccount(customerName, password, sqlService);
            ConsoleOutput.AccountCreationSuccesful();
        }

        public static void CustomerLogin()
        {
            //CustomerDbContext cDbContext = new();
            //var newCustomer = new Customer("paul", "474", "2");
            //cDbContext.Customers.Add(newCustomer);
            //cDbContext.SaveChanges();
            //BankDbContext bankDbContext = new();
            //var banks = bankDbContext.Banks;
            //try
            //{
            //    foreach (var bank in banks.ToList<Bank>())
            //    {
            //        Console.WriteLine(bank);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}


            //var banks = sqlService.GetBanks();
            //var bankNames = banks.Select(x => x.Name).ToList();
            //string bankName = TakeUserInput.ChooseAnOption(bankNames, "Bank");
            //Bank bank = banks.Find(x => x.Name == bankName);
            //BankService bankService = new BankService(bank);
            //Customer bankSelfAccount = new(bankName, "password", bank.Id);

            //ConsoleOutput.LoginOrCreateAnAccount();

            //// take user input to create account or login
            //bool createAccount = Convert.ToInt32(TakeUserInput.Input()) == 1;


            //if (createAccount)
            //{
            //    // create a new account and add it to database
            //    CreateAccountForCustomer(bankService, sqlService);
            //}

            //// log in and ask user what he wants to do
            //string customerName = TakeUserInput.UserName();
            //string password = TakeUserInput.Password();

            //Customer currentlyLoggedInCustomer = bankService.Login(customerName, password, sqlService);

            //if (currentlyLoggedInCustomer == null)
            //{
            //    ConsoleOutput.InvalidInput();
            //}
            //else
            //{
            //    ConsoleOutput.UserLoggedIn(customerName);

            //    ConsoleOutput.CustomerMenu();

            //    CustomerMenu option = (CustomerMenu)Convert.ToInt32(TakeUserInput.Input());

            //    while (option != CustomerMenu.Quit)
            //    {
            //        if (option == CustomerMenu.Deposit)
            //        {
            //            Deposit(bankService, currentlyLoggedInCustomer, bankSelfAccount, sqlService);

            //        }
            //        else if (option == CustomerMenu.Withdraw)
            //        {
            //            //Withdraw(bankService, currentlyLoggedInCustomer, bankSelfAccount);
            //        }
            //        else if (option == CustomerMenu.Transfer)
            //        {
            //            //Transfer(bankService, currentlyLoggedInCustomer);
            //        }
            //        else if (option == CustomerMenu.History)
            //        {
            //            ConsoleOutput.TransactionHistory(bankService.GetTransactionHistory(currentlyLoggedInCustomer));
            //        }
            //        else
            //        {
            //            ConsoleOutput.EnterValidOption();
            //        }

            //        ConsoleOutput.CustomerMenu();
            //        option = (CustomerMenu)Convert.ToInt32(TakeUserInput.Input());
            //    }
            //}

        }


        //public static Staff LogInAndGetStaff(StaffService staffService)
        //{
        //    string name;
        //    string password;

        //    do
        //    {
        //        name = TakeUserInput.UserName();

        //    } while (staffService.StaffExists(name));
        //    do
        //    {
        //        password = TakeUserInput.Password();
        //    } while (staffService.StaffLogin(name, password));
        //    return staffService.GetStaff(name);
        //}

        //public static bool UpdateAccountStatus(StaffService staffService)
        //{
        //    string accId = TakeUserInput.AccountId();
        //    if (staffService.FindAccount(accId))
        //    {
        //        ConsoleOutput.UpdateAccountStatusOptions();
        //        AccountStatus accStatus = (AccountStatus)Convert.ToInt32(TakeUserInput.Input());
        //        staffService.UpdateAccountStatus(accId, accStatus);
        //    }
        //    else
        //    {
        //        ConsoleOutput.UserDoesntExist();
        //    }
        //    return true;
        //}

        public static bool UpdateAcceptedCurrency(StaffService staffService)
        {
            string currencyName = TakeUserInput.CurrencyName();
            double exchangeRate = Convert.ToDouble(TakeUserInput.ExchangeRate());
            staffService.UpdateCurrencyAndExchangerate(currencyName, exchangeRate);
            ConsoleOutput.SuccesfullyUpdated();
            return true;
        }

        public static bool UpdateServiceCharges(StaffService staffService)
        {
            ConsoleOutput.UpdateServiceChargesOptions();
            BankServiceCharges choosenOption = (BankServiceCharges)Convert.ToInt32(TakeUserInput.Input());
            double newCharge = Convert.ToDouble(TakeUserInput.NewServiceCharge());
            staffService.UpdateServiceCharges(choosenOption, newCharge);
            return true;
        }

        //public static bool ShowTransactionHistory(StaffService staffService)
        //{
        //    string accId = TakeUserInput.AccountId();
        //    Customer customer = staffService.GetCustomer(accId);
        //    ConsoleOutput.TransactionHistory(staffService.GetTransactionHistory(customer));
        //    return true;
        //}

        public static void StaffLogin()
        {
            AtmDbContext sDbContext = new();
            //sDbContext.Staffs.Add(new Staff("karthik", "qwerty", "1"));
            //sDbContext.SaveChanges();
            var s = sDbContext.Staffs;

            var x = s.FirstOrDefault(x => x.Name == "karthik");
            Console.WriteLine(x.Name);
            //StaffService staffService = new StaffService("1", "alpha");

            //Staff currentlyLoggedInStaff = LogInAndGetStaff(staffService);

            //ConsoleOutput.UserLoggedIn(currentlyLoggedInStaff.Name);


            //ConsoleOutput.StaffMenu();

            //StaffMenu option = (StaffMenu)Convert.ToInt32(TakeUserInput.Input());

            //while (option != StaffMenu.Quit)
            //{
            //    if (option == StaffMenu.CreateAccount)
            //    {
            //        CreateAccountForCustomer(staffService, sqlService);
            //    }
            //    else if (option == StaffMenu.UpdateAccountStatus)
            //    {
            //        UpdateAccountStatus(staffService);
            //    }
            //    else if (option == StaffMenu.UpdateAcceptedCurrency)
            //    {
            //        UpdateAcceptedCurrency(staffService);
            //    }
            //    else if (option == StaffMenu.UpdateServiceCharges)
            //    {
            //        UpdateServiceCharges(staffService);
            //    }
            //    else if (option == StaffMenu.ShowTransactionHistory)
            //    {
            //        //ShowTransactionHistory(staffService);
            //    }
            //    else if (option == StaffMenu.RevertTransaction)
            //    {
            //        //string txnId = TakeUserInput.TransactionId();
            //        //staffService.RevertTransaction(txnId);
            //    }
            //    else
            //    {
            //        ConsoleOutput.EnterValidOption();
            //    }
            //    option = (StaffMenu)Convert.ToInt32(TakeUserInput.Input());
            //}


        }

        public static void SetupBank()
        {
            //string bankName = TakeUserInput.BankName();
            //string bankId = TakeUserInput.BankId();
            //BankDbContext dbContext = new();
            //var b = new Bank(bankId, bankName, Currency.INR);
            //dbContext.Banks.Add(b);
            //dbContext.SaveChanges();
        }
    }
}
