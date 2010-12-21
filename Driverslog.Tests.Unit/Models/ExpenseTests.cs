using System;
using Driverslog.Models;
using Victoria.Data;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Models {
    public class ExpenseTests {

        [Fact]
        public void should_set_id_when_instanciated() {
            var expense = new Expense();
            Assert.True(expense.Id != Guid.Empty);
        }

        [Fact]
        public void should_not_be_valid_with_empty_title() {
            var expense = new Expense{Title = string.Empty};
            Assert.True(!expense.IsValid());
        }

        [Fact]
        public void should_be_valid_with_title_set() {
            var expense = new Expense{Title = "Title"};
            Assert.True(expense.IsValid());
        }

        [Fact]
        public void should_include_name_of_invalid_field_in_validation_result() {
            var expense = new Expense();
            expense.IsValid();

            Assert.True(expense.ValidationMessages.ContainsKey("Title"));
        }

        [Fact]
        public void should_not_throw_on_no_validation_messages() {
            Assert.True(new Expense().ValidationMessages != null);
        }
    }
}