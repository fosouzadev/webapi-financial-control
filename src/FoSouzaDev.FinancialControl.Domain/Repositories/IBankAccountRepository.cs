﻿using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IBankAccountRepository :
    IAddRepository<BankAccount>,
    IGetRepository<BankAccount>,
    IUpdateRepository<BankAccount>,
    IRemoveRepository<BankAccount>
{
}