﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Commands;
using SimpulBlog.Infrastructure.Queries;

namespace SimpulBlog.API.Controllers
{
    [Route("api")]
    public class AbstractController : Controller
    {
        private readonly IMediator mediatr;
        private long userId => GetLoggedUserId();

        protected AbstractController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        private long GetLoggedUserId()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userIdString = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (Int64.TryParse(userIdString, out long userId))
                {
                    return userId;
                }
            }

            return -1;
        }


        protected async Task<T> Handle<T>(IRequest<T> request)
        {
            if (request is IAuthQuery)
            {
                (request as IAuthQuery).UserId = userId;
            }

            if (request is IAuthCommand)
            {
                (request as IAuthCommand).UserId = userId;
            }

            return await mediatr.Send(request);
        }
    }
}