﻿namespace CNTT36_WebXemPhim.Repository.IRepository
{
	public interface IEmailRepository
	{
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
