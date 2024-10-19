using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.Application.Common.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow;

namespace Mc2.CrudTest.AcceptanceTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly Driver _driver;

        private readonly IUnitOfWork _unitOfWork;

        public Hooks(Driver driver)
        {
            _driver = driver;
            _unitOfWork = _driver.GetDbContext();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            // Clean up the database or reset state before each scenario
            var customers = await _unitOfWork.Customers.GetAllAsync();
            _unitOfWork.Customers.DeleteRange(customers);
            await _unitOfWork.SaveChangesAsync();
        }



        [AfterScenario]
        public void AfterScenario()
        {
            // Additional clean up if necessary after each scenario
        }
    }
}