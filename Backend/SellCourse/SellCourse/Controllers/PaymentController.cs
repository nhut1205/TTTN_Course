using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.Models;


namespace CNTT36_WebXemPhim.Controllers.Payment.Stripe
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SellCourseContext _context;

        public PaymentController(IConfiguration configuration, SellCourseContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Start")]
        public async Task<IActionResult> StartPayment([FromBody] PaymentRequestDto paymentRequest)
        {
            var client = new StripeClient("sk_test_51QCVud2NGY4X6iQWYgAPx5ulbSQ4HVOok0RST8XSyubZVJUPCTlkB1bxCnVCsQuWhWPv8G3tVMyEz4AICrkF0lp600UO4QLnaJ");
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(paymentRequest.Price),
                            Currency = "vnd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = paymentRequest.Plan
                            }
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "http://localhost:5500/TTTN_Course/Frontend/pages/payment_success.html",
                CancelUrl = "http://localhost:5500/TTTN_Course/Frontend/pages/payment_fail.html"
            };

            var service = new SessionService(client);
            Session session = await service.CreateAsync(options);

            return Ok(new { url = session.Url });
        }

        private string GetUserIdFromSession()
        {
            return HttpContext.User.Identity?.Name ?? "default_username";
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent()
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Payments:Stripe:SecretKey"];

                var options = new PaymentIntentCreateOptions
                {
                    Amount = 79000,
                    Currency = "vnd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("webhook")]
        [ApiController]
        public class WebhookController : ControllerBase
        {
            private readonly SellCourseContext _context;
            private readonly IConfiguration _configuration;
            private readonly ILogger<WebhookController> _logger;

            public WebhookController(SellCourseContext context, IConfiguration configuration, ILogger<WebhookController> logger)
            {
                _context = context;
                _configuration = configuration;
                _logger = logger;
            }


            private string GetUserIdFromSession()
            {
                return HttpContext.User.Identity?.Name ?? "default_username";
            }
        }
    }
}
