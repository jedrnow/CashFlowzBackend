using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBudgetRepository _budgetRepository;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IBudgetRepository budgetRepository)
        {
            _transactionRepository = transactionRepository;
            _budgetRepository = budgetRepository;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            decimal balance = CalculateBalance(request.CreateTransactionInput.Incomes, request.CreateTransactionInput.Expenses);

            Transaction createdTransaction = CreateTransaction(balance, request);

            await _transactionRepository.AddTransaction(createdTransaction);

            await UpdateBudget(request.BudgetId, balance);

            await _transactionRepository.SaveChangesAsync();

            return createdTransaction.Id;
        }

        private decimal CalculateBalance(List<decimal> incomeAmountList, List<decimal> expenseAmountList)
        {
            decimal incomeSum = incomeAmountList.Count == 0 ? decimal.Zero : incomeAmountList.Sum();
            decimal expenseSum = expenseAmountList.Count == 0 ? decimal.Zero : expenseAmountList.Sum();

            return incomeSum - expenseSum;
        }

        private Transaction CreateTransaction(decimal balance, CreateTransactionCommand request)
        {
            Transaction createdTransaction = new(
                balance,
                request.CreateTransactionInput.Description,
                request.CreateTransactionInput.Date,
                request.UserId,
                request.BudgetId
                );

            AddIncomes(createdTransaction, request.CreateTransactionInput.Incomes);
            AddExpenses(createdTransaction, request.CreateTransactionInput.Expenses);

            return createdTransaction;
        }

        private void AddIncomes(Transaction transaction, List<decimal> incomeAmountList)
        {
            foreach(decimal incomeAmount  in incomeAmountList)
            {
                Income income = new(incomeAmount);

                transaction.AddIncome(income);
            }
        }

        private void AddExpenses(Transaction transaction, List<decimal> expenseAmountList)
        {
            foreach (decimal expenseAmount in expenseAmountList)
            {
                Expense expense = new(expenseAmount);

                transaction.AddExpense(expense);
            }
        }

        private async Task UpdateBudget(int budgetId, decimal balance)
        {
            Budget budgetToUpdate = await _budgetRepository.GetBudgetByIdToEdit(budgetId);
            budgetToUpdate.UpdateAmountOnAddTransaction(balance);
        }
    }
}
