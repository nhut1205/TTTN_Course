using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using CNTT36_WebXemPhim.Models;

namespace CNTT36_WebXemPhim.Controllers.Payment.Stripe
{
    [Route("webhook")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly SellCourseContext _context;
        private readonly ILogger<WebhookController> _logger;
        private readonly string stripeSecretKey = "sk_test_51QCVud2NGY4X6iQWYgAPx5ulbSQ4HVOok0RST8XSyubZVJUPCTlkB1bxCnVCsQuWhWPv8G3tVMyEz4AICrkF0lp600UO4QLnaJ"; // Thêm API key của bạn

        public WebhookController(SellCourseContext context, ILogger<WebhookController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // Xác minh sự kiện từ Stripe
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    "whsec_230ee74678e5c1f7b658e8162f020b8e2dc3efedd0b7eab2adff169df713b7a6"
                );

                // Xử lý sự kiện invoice.payment_succeeded (Stripe Checkout)
                if (stripeEvent.Type == "invoice.payment_succeeded")
                {
                    var session = stripeEvent.Data.Object as Invoice;
                    if (session != null)
                    {
                        // Log toàn bộ session để kiểm tra chi tiết
                        _logger.LogInformation("Session details: " + session.ToString());

                        // Khai báo biến username bên ngoài khối if
                        var username = session.CustomerEmail;
                        // Lấy hosted_invoice_url từ session.Invoice nếu có
                        var hostedInvoiceUrl = session.HostedInvoiceUrl;
                        // Xử lý tiếp theo với gói đăng ký, lưu vào cơ sở dữ liệu, v.v.
                        var amount = session.AmountPaid;
                        var plan = "Vip"; // Gói mặc định
                        var startDate = DateTime.UtcNow; // Ngày giao dịch
                        var endDate = startDate.AddMonths(1); // Kết thúc sau 1 tháng
                        var status = "Available"; // Trạng thái

                        // Lưu vào cơ sở dữ liệu bao gồm hosted_invoice_url
                        var subscription = new Models.Subscription
                        {
                            Username = username,  // Sử dụng biến username đã khai báo
                            Plan = plan,
                            Price = amount,
                            StartDate = DateOnly.FromDateTime(startDate),
                            EndDate = DateOnly.FromDateTime(endDate),
                            Status = status,
                            PaymentLink = hostedInvoiceUrl // Lưu hosted_invoice_url vào PaymentLink
                        };

                        // Lưu vào cơ sở dữ liệu
                        _context.Subscriptions.Add(subscription);
                        await _context.SaveChangesAsync();

                        _logger.LogInformation($"Gói đăng ký đã được lưu thành công cho user {username} với SubscriptionId: {subscription.SubscriptionId} và PaymentLink: {hostedInvoiceUrl}");
                    }
                }
                else
                {
                    _logger.LogInformation($"Sự kiện không được xử lý: {stripeEvent.Type}");
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogError($"StripeException: {ex.Message}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}