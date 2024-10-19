Feature: Customer Manager via API

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
@mytag
Scenario: Create, Update, Delete customers and list all customers via API
    Given via API a new customer with the following details
      | FirstName   | LastName        | Email                         | PhoneNumber | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | 123         | 1990-08-23   | NL91ABNA0417164300 |
    When via API the operator attempts to create the customer
    Then via API an error message should be shown indicating "Invalid phone number"

    Given via API a new customer with the following details
      | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | +12124567890  | 1990-08-23   | 132164555645465            |
    When via API the operator attempts to create the customer
    Then via API an error message should be shown indicating "Invalid Bank account format"

    Given via API a new customer with the following details
      | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi           | +12124567890  | 1990-08-23   | NL91ABNA0417164300 |
    When via API the operator attempts to create the customer
    Then via API an error message should be shown indicating "Invalid email"
    Given via API a new customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi | ahmad.aghamohammadi@gmail.com | +12124567890 | 1990-08-23   | NL91ABNA0417164300        |
    When via API the operator attempts to create the customer
    Then via API the customer should be added successfully
    Given via API a new customer with the following details
      | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad2       | Aghamohammadi2 | ahmad.aghamohammadi2@gmail.com | +12124567892 | 1990-08-24   | NL91ABNA0417164300        |
    When via API the operator attempts to create the customer
    Then via API the customer should be added successfully

    Given via API a new customer with the following details
      | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad3      | Aghamohammadi3 | ahmad.aghamohammadi@gmail.com | +12124567890 | 1990-08-11   | NL91ABNA0417164300        |
    When via API the operator attempts to create the customer
    Then via API an error message should be shown indicating "Email already exists"

    Given via API a new customer with the following details
      | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi | ahmad.aghamohammadi@gmail.com | +12124567890 | 1990-08-23   | NL91ABNA0417164300        |
    When via API the operator attempts to create the customer
    Then via API an error message should be shown indicating "Customer with the same name and date of birth already exists"





    Given via API update customer with the following details
      | Id   | FirstName   | LastName        | Email                         | PhoneNumber | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | 123         | 1990-08-23   | NL91ABNA0417164300 |
    When via API the operator attempts to update the customer
    Then via API an error message should be shown indicating "Invalid phone number"

    Given via API update customer with the following details
      | Id   | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | +12124567890  | 1990-08-23   | 132164555645465            |
    When via API the operator attempts to update the customer
    Then via API an error message should be shown indicating "Invalid Bank account format"

    Given via API update customer with the following details
      | Id   | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi           | +12124567890  | 1990-08-23   | NL91ABNA0417164300 |
    When via API the operator attempts to update the customer
    Then via API an error message should be shown indicating "Invalid email"    
    Given via API update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | AhmadEdited       | AghamohammadiEdited | ahmad.aghamohammadiEdited@gmail.com | +12124567896 | 1990-08-26   | NL91ABNA0417164300        |
    When via API the operator attempts to update the customer
    Then via API the customer should be updated successfully

    Given via API update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | AhmadEdited      | AghamohammadiEdited | ahmad.aghamohammadi2@gmail.com | +12124567896 | 1990-08-26   | NL91ABNA0417164300        |
    When via API the operator attempts to update the customer
    Then via API an error message should be shown indicating "Email already exists"

    Given via API update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad2       | Aghamohammadi2 | ahmad2.aghamohammadi44@gmail.com | +12124567890 | 1990-08-24   | NL91ABNA0417164300        |
    When via API the operator attempts to update the customer
    Then via API an error message should be shown indicating "Customer with the same name and date of birth already exists"

    Given via API update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad2       | Aghamohammadi2 | ahmad2.aghamohammadi44@gmail.com | +12124567890 | 1990-08-24   | NL91ABNA0417164300        |
    When via API the operator attempts to update the customer
    Then via API an error message should be shown indicating "Customer with the same name and date of birth already exists"


    
    When via API the operator lists the customers
    Then via API the customer list should be shown successfully with 2 items

    Given via API a customer with Id "51a9d76c-b28e-4659-aad5-965515e15e16" exists in the system
    When via API the operator deletes the customer
    When via API the operator lists the customers
    Then via API the customer list should be shown successfully with 1 items
