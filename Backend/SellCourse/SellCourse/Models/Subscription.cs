using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public string Username { get; set; } = null!;

    public string Plan { get; set; } = null!;

    public decimal? Price { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    public string? PaymentLink { get; set; }

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
