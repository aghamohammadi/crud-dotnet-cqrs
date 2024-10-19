Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
@mytag
Scenario: Create, Update, Delete customers and list all customers
    Given a new customer with the following details
      | FirstName   | LastName        | Email                         | PhoneNumber | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | 123         | 1990-08-23   | NL91ABNA0417164300 |
    When the operator attempts to create the customer
    Then an error message should be shown indicating "Invalid phone number"

    Given a new customer with the following details
      | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | +12124567890  | 1990-08-23   | 132164555645465            |
    When the operator attempts to create the customer
    Then an error message should be shown indicating "Invalid Bank account format"

    Given a new customer with the following details
      | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi           | +12124567890  | 1990-08-23   | NL91ABNA0417164300 |
    When the operator attempts to create the customer
    Then an error message should be shown indicating "Invalid email"
    Given a new customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi | ahmad.aghamohammadi@gmail.com | +12124567890 | 1990-08-23   | NL91ABNA0417164300        |
    When the operator attempts to create the customer
    Then the customer should be added successfully
    Given a new customer with the following details
      | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad2       | Aghamohammadi2 | ahmad.aghamohammadi2@gmail.com | +12124567892 | 1990-08-24   | NL91ABNA0417164300        |
    When the operator attempts to create the customer
    Then the customer should be added successfully

    Given a new customer with the following details
      | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad3      | Aghamohammadi3 | ahmad.aghamohammadi@gmail.com | +12124567890 | 1990-08-11   | NL91ABNA0417164300        |
    When the operator attempts to create the customer
    Then an error message should be shown indicating "Email already exists"

    Given a new customer with the following details
      | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | Ahmad       | Aghamohammadi | ahmad.aghamohammadi@gmail.com | +12124567890 | 1990-08-23   | NL91ABNA0417164300        |
    When the operator attempts to create the customer
    Then an error message should be shown indicating "Customer with the same name and date of birth already exists"





    Given update customer with the following details
      | Id   | FirstName   | LastName        | Email                         | PhoneNumber | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | 123         | 1990-08-23   | NL91ABNA0417164300 |
    When the operator attempts to update the customer
    Then an error message should be shown indicating "Invalid phone number"

    Given update customer with the following details
      | Id   | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi@gmail.com  | +12124567890  | 1990-08-23   | 132164555645465            |
    When the operator attempts to update the customer
    Then an error message should be shown indicating "Invalid Bank account format"

    Given update customer with the following details
      | Id   | FirstName   | LastName        | Email                         | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad       | Aghamohammadi   | ahmad.aghamohammadi           | +12124567890  | 1990-08-23   | NL91ABNA0417164300 |
    When the operator attempts to update the customer
    Then an error message should be shown indicating "Invalid email"    
    Given update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | AhmadEdited       | AghamohammadiEdited | ahmad.aghamohammadiEdited@gmail.com | +12124567896 | 1990-08-26   | NL91ABNA0417164300        |
    When the operator attempts to update the customer
    Then the customer should be updated successfully

    Given update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | AhmadEdited      | AghamohammadiEdited | ahmad.aghamohammadi2@gmail.com | +12124567896 | 1990-08-26   | NL91ABNA0417164300        |
    When the operator attempts to update the customer
    Then an error message should be shown indicating "Email already exists"

    Given update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad2       | Aghamohammadi2 | ahmad2.aghamohammadi44@gmail.com | +12124567890 | 1990-08-24   | NL91ABNA0417164300        |
    When the operator attempts to update the customer
    Then an error message should be shown indicating "Customer with the same name and date of birth already exists"

    Given update customer with the following details
      | Id   | FirstName   | LastName | Email                | PhoneNumber    | DateOfBirth  | BankAccountNumber |
      | 51a9d76c-b28e-4659-aad5-965515e15e16       | Ahmad2       | Aghamohammadi2 | ahmad2.aghamohammadi44@gmail.com | +12124567890 | 1990-08-24   | NL91ABNA0417164300        |
    When the operator attempts to update the customer
    Then an error message should be shown indicating "Customer with the same name and date of birth already exists"


    
    When the operator lists the customers
    Then the customer list should be shown successfully with 2 items

    Given a customer with Id "51a9d76c-b28e-4659-aad5-965515e15e16" exists in the system
    When the operator deletes the customer
    When the operator lists the customers
    Then the customer list should be shown successfully with 1 items
