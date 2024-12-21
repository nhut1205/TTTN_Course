using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class PaymentTransaction
{
    public int TransactionId { get; set; }

    public int SubscriptionId { get; set; }

    public DateOnly TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;
}
