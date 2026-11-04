using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using techninxa.Models;

namespace Techninxa.Hubs;

public class CompanyChatHub : Hub
{
	private readonly ApplicationDbContext _db;

	private static readonly ConcurrentDictionary<string, string> OnlineUsers = new ConcurrentDictionary<string, string>();

	public CompanyChatHub(ApplicationDbContext db)
	{
		_db = db;
	}

	public override async Task OnConnectedAsync()
	{
		HttpContext httpContext = Context.GetHttpContext();
		if (httpContext?.Session.GetString("CompanyChatLoggedIn") != "true")
		{
			Context.Abort();
			return;
		}
		string text = httpContext.Session.GetString("CompanyChatUsername");
		if (string.IsNullOrWhiteSpace(text))
		{
			Context.Abort();
			return;
		}
		text = text.Trim();
		OnlineUsers[Context.ConnectionId] = text;
		await Clients.All.SendAsync("UserOnline", text);
		await SendOnlineUsers();
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		if (OnlineUsers.TryRemove(Context.ConnectionId, out string username))
		{
			if (!OnlineUsers.Values.Any((string x) => x.Equals(username, StringComparison.OrdinalIgnoreCase)))
			{
				await Clients.All.SendAsync("UserOffline", username);
			}
			await SendOnlineUsers();
		}
		await base.OnDisconnectedAsync(exception);
	}

	public async Task SendMessage(string message)
	{
		HttpContext httpContext = Context.GetHttpContext();
		if (httpContext?.Session.GetString("CompanyChatLoggedIn") != "true")
		{
			return;
		}
		string text = httpContext.Session.GetString("CompanyChatUsername");
		if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(message))
		{
			text = text.Trim();
			message = message.Trim();
			if (message.Length <= 1000)
			{
				ChatMessage chatMessage = new ChatMessage
				{
					Username = text,
					Message = message,
					SentAt = DateTime.Now
				};
				_db.ChatMessages.Add(chatMessage);
				await ((DbContext)_db).SaveChangesAsync(default(CancellationToken));
				await Clients.All.SendAsync("ReceiveMessage", chatMessage.Id, chatMessage.Username, chatMessage.Message, chatMessage.SentAt.ToString("hh:mm tt"));
			}
		}
	}

	public async Task SendFileMessage(string fileName, string fileUrl, bool isImage)
	{
		HttpContext httpContext = Context.GetHttpContext();
		if (!(httpContext?.Session.GetString("CompanyChatLoggedIn") != "true"))
		{
			string text = httpContext.Session.GetString("CompanyChatUsername");
			if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(fileUrl))
			{
				text = text.Trim();
				string message = $"[FILE]|{fileName}|{fileUrl}|{isImage}";
				ChatMessage chatMessage = new ChatMessage
				{
					Username = text,
					Message = message,
					SentAt = DateTime.Now
				};
				_db.ChatMessages.Add(chatMessage);
				await ((DbContext)_db).SaveChangesAsync(default(CancellationToken));
				await Clients.All.SendAsync("ReceiveMessage", chatMessage.Id, chatMessage.Username, chatMessage.Message, chatMessage.SentAt.ToString("hh:mm tt"));
			}
		}
	}

	private async Task SendOnlineUsers()
	{
		List<string> arg = (from x in OnlineUsers.Values.Distinct(StringComparer.OrdinalIgnoreCase)
			orderby x
			select x).ToList();
		await Clients.All.SendAsync("OnlineUsers", arg);
	}
}
