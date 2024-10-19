using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.AcceptanceTests.Hooks;
using Mc2.CrudTest.Application.Features.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.Application.Features.Customer.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Features.Customer.Commands.UpdateCustomer;
using Mc2.CrudTest.Application.Features.Customer.Queries;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Presentation.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechTalk.SpecFlow.CommonModels;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CustomerStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IMediator _mediator;

    public CustomerStepDefinitions(ScenarioContext scenarioContext, Driver driver)
    {
        _scenarioContext = scenarioContext;
        _mediator = driver.GetMediator();

    }

    // Scenario: Insert customer with an invalid phone number
    [Given("a new customer with the following details")]
    public void GivenNewCustomerWithFollowingDetails(Table table)
    {
        var customerData = table.Rows.First();
        var command = new CreateCustomerCommand
        {
            Id = (customerData.ContainsKey("Id") && !string.IsNullOrEmpty(customerData["Id"])) ? Guid.Parse(customerData["Id"]) : null ,
            FirstName = customerData["FirstName"],
            LastName = customerData["LastName"],
            Email = customerData["Email"],
            PhoneNumber = customerData["PhoneNumber"],
            DateOfBirth = DateTime.Parse(customerData["DateOfBirth"]),
            BankAccountNumber = customerData["BankAccountNumber"]
        };

        _scenarioContext["CreateCustomerCommand"] = command;
    }

    [When("the operator attempts to create the customer")]
    public async Task WhenTheOperatorAttemptsToCreateTheCustomerAsync()
    {
        var command = _scenarioContext["CreateCustomerCommand"] as CreateCustomerCommand;

        try
        {
            var result = await _mediator.Send(command);
            _scenarioContext["CustomerId"] = result;
            _scenarioContext["Error"] = null;
        }
        catch (Exception ex)
        {
            _scenarioContext["Error"] = ex.Message;
        }        
    }

    [Then(@"an error message should be shown indicating ""(.*)""")]
    public void ThenAnErrorMessageShouldBeShownIndicating(string expectedErrorMessage)
    {
        var errorMessage = _scenarioContext["Error"] as string;

        Assert.IsNotNull(errorMessage, "Expected an error message.");
        Assert.That(errorMessage, Does.Contain(expectedErrorMessage), "Error message does not match expected message.");

    }


    [Then("the customer should be added successfully")]
    public void ThenTheCustomerShouldBeAddedSuccessfully()
    {
        var customerId = _scenarioContext["CustomerId"];

        Assert.IsNotNull(customerId, "Customer was not added successfully.");
        Assert.IsInstanceOf<Guid>(customerId, "Customer ID should be a valid GUID.");
    }


    [Given("update customer with the following details")]
    public void GivenUpdateCustomerWithFollowingDetails(Table table)
    {
        var customerData = table.Rows.First();
        var command = new UpdateCustomerCommand
        {
            Id = Guid.Parse(customerData["Id"]),
            FirstName = customerData["FirstName"],
            LastName = customerData["LastName"],
            Email = customerData["Email"],
            PhoneNumber = customerData["PhoneNumber"],
            DateOfBirth = DateTime.Parse(customerData["DateOfBirth"]),
            BankAccountNumber = customerData["BankAccountNumber"]
        };

        _scenarioContext["UpdateCustomerCommand"] = command;
    }

    [When("the operator attempts to update the customer")]
    public async Task WhenTheOperatorAttemptsToUpdateTheCustomerAsync()
    {
        var command = _scenarioContext["UpdateCustomerCommand"] as UpdateCustomerCommand;

        try
        {
            var result = await _mediator.Send(command);
            _scenarioContext["Result"] = result;
            _scenarioContext["Error"] =null;
        }
        catch (Exception ex)
        {
            _scenarioContext["Error"] = ex.Message;
        }
    }

    [Then("the customer should be updated successfully")]
    public void ThenTheCustomerShouldBeUpdatedSuccessfully()
    {
        var result = _scenarioContext["Result"].ToString(); 

        Assert.IsNotNull(result, "Customer was not updated successfully.");
        Assert.AreEqual("True", result, "The result does not match the expected value."); 
    }


    [When("the operator lists the customers")]
    public async Task WhenTheOperatorListsTheCustomers()
    {
        var result = await _mediator.Send(new GetCustomersQuery());
        _scenarioContext["CustomerList"] = result;
    }

    [Then(@"the customer list should be shown successfully with (\d+) items")]
    public void ThenTheCustomerListShouldBeShownSuccessfully(int expectedCount)
    {
        var customers = (List<CustomerDto>)_scenarioContext["CustomerList"];
        Assert.IsNotNull(customers, "The customer list is null.");
        Assert.AreEqual(expectedCount, customers.Count, $"Expected {expectedCount} customers, but found {customers.Count}.");
    }


    [Given(@"a customer with Id ""(.*)"" exists in the system")]
    public void GivenCustomerWithIdExistsInTheSystem(string customerId)
    {
        _scenarioContext["CustomerId"] = customerId;
    }

    [When(@"the operator deletes the customer")]
    public async Task WhenTheOperatorDeletesTheCustomer()
    {
        
        try
        {
            var customerId = _scenarioContext["CustomerId"].ToString();
            var command = new DeleteCustomerCommand { Id = Guid.Parse(customerId) };
            await _mediator.Send(command);
            _scenarioContext["DeleteResult"] = true;

        }
        catch (Exception ex)
        {
            _scenarioContext["DeleteResult"] = false;
        }

    }

    [Then("the customer should be deleted successfully")]
    public void ThenTheCustomerShouldBeDeletedSuccessfully()
    {
        var result = _scenarioContext["DeleteResult"];
        Assert.IsTrue((bool)result, "The customer was not deleted successfully.");
    }

   
}
